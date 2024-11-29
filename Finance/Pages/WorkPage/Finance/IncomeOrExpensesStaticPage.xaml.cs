using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.CustomControl;
using Finance.View;

namespace Finance.Pages.WorkPage.Finance;

public partial class IncomeOrExpensesStaticPage : ContentPage
{
	public int IdDate;

    Loading loading { get; set; }

    public IncomeOrExpensesStaticPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            var revenues = DBModel.GetCollectionModel<Revenues>(new Dictionary<string, object>() { { "IdDate", IdDate }, { "IdUser", InfoAccount.IdUser } });
            var result = DBModel.GetCollectionModel<Result>(new Dictionary<string, object>() { { "IdDate", IdDate }, { "IdUser", InfoAccount.IdUser } });

            string sql = String.Format(@"SELECT ag.*, 
                                SUM(case WHEN a.`IsAsset` then a.`Sum` end) SumAssets, 
                                SUM(case WHEN not a.`IsAsset` then -a.`Sum` end) SumPasive, 
                                SUM(case WHEN a.`IsAsset` then 1 end) CountAssets, 
                                SUM(case WHEN not a.`IsAsset` then 1 end) CountPasive FROM `AssetsGroup` ag 
                            INNER JOIN `GroupingAssets` ga ON ga.`IdGroupAssets` = ag.`Id` 
                            INNER JOIN `Assets` a ON (a.`Id`,a.`IdUser`) = (ga.`IdAssets`,ag.`IdUser`) OR (a.`Id`,a.`IdUser`) = (ga.`IdAssets`,'{0}')
                            INNER JOIN `DateJournal` d ON d.`Id` = ga.`IdDate` AND d.`Id` = {1}
                            WHERE ag.`IdUser` = '{0}' OR ag.`IdUser` is NULL
                            GROUP BY ag.`Id`,ag.`Name`,ag.`Commit`,ag.`Icon`,ag.`IdUser`;", InfoAccount.IdUser, IdDate);

            var ViewAssetsGroup = DBModel.GetCollectionModel<View.AssetsGroup>(sql);

            if (revenues is null || revenues.Count == 0) return;

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                try
                {
                    Income.BindingContext = revenues.FirstOrDefault(i => i.IsRevenues);
                    Expenses.BindingContext = revenues.FirstOrDefault(i => !i.IsRevenues);
                    Itog.BindingContext = result.FirstOrDefault();
                    BindableLayout.SetItemsSource(asGrVSL, ViewAssetsGroup);
                }
                catch (Exception)
                {
                    return;
                }
            });
        }));
    }
}
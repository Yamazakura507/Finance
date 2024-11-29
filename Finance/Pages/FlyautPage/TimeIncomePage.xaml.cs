using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.CustomControl;
using Finance.Models;
using Finance.Pages.WorkPage.Finance;
using Finance.Pages.WorkPage.Finance.Assets;

namespace Finance.Pages.FlyautPage;

public partial class TimeIncomePage : FlyoutPage
{
    Loading loading { get; set; }

    public TimeIncomePage()
	{
		InitializeComponent();

        flyoutPage.collectionView.SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            var item = e.CurrentSelection.FirstOrDefault() as DateUser;
            var model = DBModel.GetModel<DateUser>(item.Id);

            if (item != null)
            {
                await MainThread.InvokeOnMainThreadAsync(()=> 
                {
                   Detail = new NavigationPage(new IncomeOrExpensesStaticPage() { IdDate = model.IdDate });

                    if (!((IFlyoutPageController)this).ShouldShowSplitMode) IsPresented = false;
                });
            }
        }));
    }
}
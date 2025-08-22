using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;
using Finance.Pages.MenuPage;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Assets;

public partial class AssetsGroupPage : ContentPage
{
    ObservableCollection<View.AssetsGroup> ViewAssetsGroup;
    Loading loading;

    public AssetsGroupPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
            {
                try
                {
                    string sql = String.Format(@"SELECT ag.*, 
                                SUM(case WHEN a.`IsAsset` then a.`Sum` end) SumAssets, 
                                SUM(case WHEN not a.`IsAsset` then -a.`Sum` end) SumPasive, 
                                SUM(case WHEN a.`IsAsset` then 1 end) CountAssets, 
                                SUM(case WHEN not a.`IsAsset` then 1 end) CountPasive FROM `AssetsGroup` ag 
                            INNER JOIN `GroupingAssets` ga ON ga.`IdGroupAssets` = ag.`Id` 
                            INNER JOIN `Assets` a ON (a.`Id`,a.`IdUser`) = (ga.`IdAssets`,ag.`IdUser`) OR (a.`Id`,a.`IdUser`) = (ga.`IdAssets`,'{0}')
                            INNER JOIN `DateJournal` d ON d.`Id` = ga.`IdDate` AND d.Year = YEAR(NOW()) AND d.Month = MONTH(NOW())
                            WHERE ag.`IdUser` = '{0}' OR ag.`IdUser` is NULL
                            GROUP BY ag.`Id`,ag.`Name`,ag.`Description`,ag.`Icon`,ag.`IdUser`;", InfoAccount.IdUser);

                    ViewAssetsGroup = DBModel.GetCollectionModel<View.AssetsGroup>(sql);

                    if (ViewAssetsGroup is null || ViewAssetsGroup.Count() == 0) throw new Exception("” вас отсутствуют групировки финансов");
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(asGrVSL, ViewAssetsGroup));
                    }
                }
                catch (Exception ex)
                {
                    MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
                }
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
        
    }

    private async void AddGr_Pressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationPage(new GroupAddPage()));
    }

    async private void EditGrAssets_Pressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationPage(new AssetGroupEditorPage()));
    }

    async private void AddAsset_Pressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationPage(new TimeAssetsPage()));
    }

    async private void AssetsGroup_Tapped(object sender, TappedEventArgs e)
    {
        await MainThread.InvokeOnMainThreadAsync(async() => await Navigation.PushAsync(new NavigationPage(new TimeAssetsPage() { BindingContext = ((ContentView)sender).BindingContext })));
    }
}
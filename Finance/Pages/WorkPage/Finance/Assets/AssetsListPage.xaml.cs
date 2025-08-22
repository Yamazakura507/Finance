using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Assets;

public partial class AssetsListPage : ContentPage
{
    public int? IdDate { get; set; } = null;

    ObservableCollection<View.Assets> ViewAssets;
    Loading loading { get; set; }

    public AssetsListPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        this.ToolbarItems.Clear();

        #if WINDOWS
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        #elif ANDROID || IOS
            TimeNavBt.IsVisible = true;
        #endif

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                string sql = $@"SELECT a.* FROM `GroupingAssets` ga
                                            INNER JOIN `Assets` a ON a.`Id` = ga.`IdAssets`
                                            WHERE ga.`IdGroupAssets` = '{((View.AssetsGroup)this.BindingContext).Id}'{(IdDate is null ? String.Empty : $"AND ga.`IdDate` = '{IdDate}'")}";

                ViewAssets = DBModel.GetCollectionModel<View.Assets>(sql);

                if (ViewAssets is null || ViewAssets.Count() == 0) throw new Exception("У вас отсутствуют финансовые объекты");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(asVSL, ViewAssets));
                }
            }
            catch (Exception ex) { }
        }));
    }
    private void DeleteAsset_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        var assets = (View.Assets)((ImageButton)sender).BindingContext;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.Assets>(assets.Id).DeleteModel<Models.Assets>()));

        ViewAssets.Remove(assets);
    }

    async private void AddAsset_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new TimeAssetsPage() { BindingContext = null }));

    private void Back_Clicked(object? sender, EventArgs e) => ((Page)this.Parent.Parent).BackButtonInNavClick();

    private void Asset_Tapped(object sender, TappedEventArgs e)
    {
        var asset = (View.Assets)((ContentView)sender).BindingContext;

        if (!String.IsNullOrEmpty(asset.Description)) this.Messege(asset.Description, ProviderType.Info);
    }

    private void AndroidVisualTimeBt_Pressed(object sender, EventArgs e) => ((TimeAssetsPage)this.Parent.Parent).IsPresented = true;
}
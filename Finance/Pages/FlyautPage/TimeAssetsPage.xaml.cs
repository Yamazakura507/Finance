using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.CustomControl;
using Finance.Models;
using Finance.Pages.WorkPage.Finance.Assets;

namespace Finance.Pages.FlyautPage;

public partial class TimeAssetsPage : FlyoutPage
{
    Loading loading { get; set; }

    public TimeAssetsPage()
	{
		InitializeComponent();

        #if ANDROID || IOS
                this.IsPresented = true;
        #endif

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
                    if (this.BindingContext is null) 
                        Detail = new NavigationPage(new AssetsEditorPage() { IdDate = model.IdDate });
                    else
                        Detail = new NavigationPage(new AssetsListPage() { IdDate = model.IdDate, BindingContext = this.BindingContext });

                    if (!((IFlyoutPageController)this).ShouldShowSplitMode) IsPresented = false;
                });
            }
        }));
    }

    private void FlyoutPage_Loaded(object sender, EventArgs e)
    {
        if (this.BindingContext != null)  
                Detail = new NavigationPage(new AssetsListPage() { BindingContext = this.BindingContext });
    }
}
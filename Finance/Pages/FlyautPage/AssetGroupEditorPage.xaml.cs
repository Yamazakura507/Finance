using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.CustomControl;
using Finance.Pages.FlyautPage.FlyautModel;
using Finance.Pages.WorkPage.Finance.Assets;

namespace Finance.Pages.FlyautPage;

public partial class AssetGroupEditorPage : FlyoutPage
{
    Loading loading { get; set; }

    public AssetGroupEditorPage()
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
            var item = e.CurrentSelection.FirstOrDefault() as View.AssetsGroup;
            var model = DBModel.GetModel<Models.AssetsGroup>(item.Id);

            if (item != null)
            {
                await MainThread.InvokeOnMainThreadAsync(()=> 
                {
                    Detail = new NavigationPage(new GroupAddPage() { BindingContext = model });

                    if (!((IFlyoutPageController)this).ShouldShowSplitMode) IsPresented = false;
                });
            }
        }));
    }
}
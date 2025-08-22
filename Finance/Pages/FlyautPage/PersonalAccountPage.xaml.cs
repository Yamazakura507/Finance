using Finance.Classes.AppSettings;
using Finance.Pages.FlyautPage.FlyautModel;
using Finance.Pages.WorkPage;

namespace Finance.Pages.FlyautPage;

public partial class PersonalAccountPage : FlyoutPage
{
	public PersonalAccountPage()
	{
		InitializeComponent();

        flyoutPage.collectionView.SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyautAccountModel;
        if (item != null)
        {
            Page page = (Page)Activator.CreateInstance(item.TargetType);

            if (item.TargetType == typeof(SettingsPage))
                page.BindingContext = InfoAccount.User;

            Detail = new NavigationPage(page);
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;
        }
    }
}
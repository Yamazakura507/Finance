using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Pages.Tabbed;

namespace Finance.Pages.MenuPage;

public partial class PersonalAccountMenuPage : ContentPage
{
	public PersonalAccountMenuPage()
	{
		InitializeComponent();
	}

	private async void Exit_Pressed(object sender, EventArgs e)
	{
		StartParametrs.ClearParametr();
        App.Current.MainPage = new MainTabbedPage();
    } 

    private void LbExit_Tapped(object sender, TappedEventArgs e) => Exit_Pressed(null, null);
}
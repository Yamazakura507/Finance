using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.WorkPage.Setting;
using System.Collections.ObjectModel;

namespace Finance.Pages.Tabbed;

public partial class AccountTabbed : TabbedPage
{
    Loading loading { get; set; }
    ObservableCollection<View.LibLink> ViewLinks;

    public AccountTabbed()
	{
		InitializeComponent();
	}

    private void TabbedPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                ViewLinks = DBModel.GetCollectionModel<View.LibLink>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser }, { "IsMainMenu", true } }, default, default, new Dictionary<string, OrderType>() { { "Id", OrderType.Desc } });

                if (ViewLinks is null || ViewLinks.Count() == 0) return;
                else
                {
                    foreach (View.LibLink link in ViewLinks)
                    {
                        BrouserPage page = new BrouserPage(link.Link, true) { Title = link.Name, IconImageSource = link.Icon };
                        MainThread.BeginInvokeOnMainThread(() => this.Children.Add(page));
                    }
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => await this.Messege(ex.Message, ProviderType.Error));
            }
        }));
    }
}
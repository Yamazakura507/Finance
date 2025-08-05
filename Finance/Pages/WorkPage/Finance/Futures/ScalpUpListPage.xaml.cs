using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;
using Finance.Pages.Tabbed;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Futures;

public partial class ScalpUpListPage : ContentPage
{
    ObservableCollection<View.Scalping> ViewScalping;
    Loading loading { get; set; }

    public ScalpUpListPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {

                ViewScalping = DBModel.GetCollectionModel<View.Scalping>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewScalping is null || ViewScalping.Count() == 0) throw new Exception("У вас отсутствуют скальп хранилища");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(scalpVSL, ViewScalping));
                }
            }
            catch (Exception ex) 
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
    private void DeletePlan_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        var scalp = (View.Scalping)((ImageButton)sender).BindingContext;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.Scalping>(scalp.Id).DeleteModel<Models.Scalping>()));

        ViewScalping.Remove(scalp);
    }

    async private void AddPlan_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new ScalpUpInfoPage() { BindingContext = null }));

    async private void Plan_Tapped(object sender, TappedEventArgs e)
    {
        var scalp = (View.Scalping)((ContentView)sender).BindingContext;

        await Navigation.PushAsync(new NavigationPage(new ScalpUpInfoPage() { BindingContext = DBModel.GetModel<Models.Scalping>(scalp.Id) }));
    }
}
using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;
using Finance.Pages.Tabbed;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Planing;

public partial class PlaningLisPage : ContentPage
{
    ObservableCollection<View.PlanningJournal> ViewPlan;
    Loading loading { get; set; }

    public PlaningLisPage()
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

                ViewPlan = DBModel.GetCollectionModel<View.PlanningJournal>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewPlan is null || ViewPlan.Count() == 0) throw new Exception("У вас отсутствуют планы");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(debVSL, ViewPlan));
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

        var plan = (View.PlanningJournal)((ImageButton)sender).BindingContext;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.PlanningJournal>(plan.Id).DeleteModel<Models.PlanningJournal>()));

        ViewPlan.Remove(plan);
    }
    async private void AddPlan_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new PlaningInfoPage() { BindingContext = null }));
    async private void Plan_Tapped(object sender, TappedEventArgs e)
    {
        var plan = (View.PlanningJournal)((ContentView)sender).BindingContext;

        await Navigation.PushAsync(new NavigationPage(new PlaningInfoPage() { BindingContext = DBModel.GetModel<Models.PlanningJournal>(plan.Id) }));
    }
}
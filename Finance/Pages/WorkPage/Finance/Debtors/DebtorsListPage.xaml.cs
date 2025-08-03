using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Debtors;

public partial class DebtorsListPage : ContentPage
{
    public int? IdDate { get; set; } = null;

    ObservableCollection<View.Debtor> ViewDebtors;
    Loading loading { get; set; }

    public DebtorsListPage()
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

                ViewDebtors = DBModel.GetCollectionModel<View.Debtor>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } },default,default, new Dictionary<string, bool>() { { "IdStatusDebtor",  true} });

                if (ViewDebtors is null || ViewDebtors.Count() == 0) throw new Exception("У вас отсутствуют должники");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(debVSL, ViewDebtors));
                }
            }
            catch (Exception ex) 
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
    private void DeleteDebtor_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        var debtor = (View.Debtor)((ImageButton)sender).BindingContext;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.Debtor>(debtor.Id).DeleteModel<Models.Debtor>()));

        ViewDebtors.Remove(debtor);
    }
    async private void AddDebtor_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new EditorDebtor() { BindingContext = null }));
    async private void Debtor_Tapped(object sender, TappedEventArgs e)
    {
        var debtor = (View.Debtor)((ContentView)sender).BindingContext;

        await Navigation.PushAsync(new NavigationPage(new EditorDebtor() { BindingContext = DBModel.GetModel<Models.Debtor>(debtor.Id) }));
    }
}
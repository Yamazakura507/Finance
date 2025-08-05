using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace Finance.Pages.WorkPage.Finance.Debtors;

public partial class DebtorsListPage : ContentPage
{
    int offset = 0;
    int count = 0;

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

                ViewDebtors = DBModel.GetCollectionModel<View.Debtor>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } },StartParametrs.LenListPage,default, new Dictionary<string, OrderType>() { { "IdStatusDebtor", OrderType.Asc } });
                count = DBModel.Counter<Models.Debtor>();
                offset = StartParametrs.LenListPage;

                if (ViewDebtors is null || ViewDebtors.Count() == 0) throw new Exception("У вас отсутствуют должники");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        BindableLayout.SetItemsSource(debVSL, ViewDebtors);
                        btAddItem.IsVisible = count > StartParametrs.LenListPage;
                    });
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

    private void btAddItem_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                var items = DBModel.GetCollectionModel<View.Debtor>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } }, StartParametrs.LenListPage, offset, new Dictionary<string, OrderType>() { { "IdStatusDebtor", OrderType.Asc } });
                offset += StartParametrs.LenListPage;

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    for (int i = 0; i < items.Count; i++) ViewDebtors.Add(items[i]);
                    btAddItem.IsVisible = count >= offset;
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
}
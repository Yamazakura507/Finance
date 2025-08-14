using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Futures;

public partial class OrdersListPage : ContentPage
{
    bool is_long_taped = false;
    int offset = 0;
    int count = 0;
    int idSc = -1;

    ObservableCollection<View.ScalpingEntries> ViewOrders;
    Loading loading { get; set; }

    public OrdersListPage()
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
                idSc = ((Models.Scalping)this.BindingContext).Id;

                ViewOrders = DBModel.GetCollectionModel<View.ScalpingEntries>(
                    new Dictionary<string, object>() { { "IdScalping",  idSc} }, 
                    StartParametrs.LenListPage, 
                    default, 
                    new Dictionary<string, OrderType>() { { "IdStatusScalping", OrderType.Desc }, { "DateExit", OrderType.Desc } });

                count = DBModel.Counter<Models.ScalpingEntries>(new Dictionary<string, object>() { { "IdScalping", idSc } }, false);
                offset = StartParametrs.LenListPage;

                if (ViewOrders is null || ViewOrders.Count() == 0) throw new Exception("У вас отсутствуют сделки");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        BindableLayout.SetItemsSource(debVSL, ViewOrders);
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
    private void DeleteTask_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        View.ScalpingEntries enteries = sender.ContextConvert<View.ScalpingEntries>();

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.ScalpingEntries>(enteries.Id).DeleteModel<Models.ScalpingEntries>()));

        ViewOrders.Remove(enteries);
    }
    async private void AddOrder_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new OrderInfoPage() { BindingContext = null, IdScalp = idSc }));
    async private void Order_Tapped(object sender, TappedEventArgs e)
    {
        var order = (View.ScalpingEntries)((Grid)sender).BindingContext;

        var r = DBModel.GetModel<Models.ScalpingEntries>(order.Id);

        await Navigation.PushAsync(new NavigationPage(new OrderInfoPage() { BindingContext = DBModel.GetModel<Models.ScalpingEntries>(order.Id), IdScalp = idSc }));
    }

    private void btAddItem_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                var items = DBModel.GetCollectionModel<View.ScalpingEntries>(
                    new Dictionary<string, object>() { { "IdScalping", idSc } }, 
                    StartParametrs.LenListPage, 
                    offset, 
                    new Dictionary<string, OrderType>() { { "IdStatusScalping", OrderType.Desc }, { "DateExit", OrderType.Desc } });

                offset += StartParametrs.LenListPage;

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    for (int i = 0; i < items.Count; i++) ViewOrders.Add(items[i]);
                    btAddItem.IsVisible = count >= offset;
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }

    private void CloseOrderMenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        View.ScalpingEntries enterise;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                enterise = sender.ContextConvert<View.ScalpingEntries>();

                int id = await this.SheetPicker<StatusScalping>("Выбор статуса сделки", ErrProvider);

                if (id == 0) return;

                DBModel.GetModel<Models.ScalpingEntries>(enterise.Id).IdStatusScalping = id;
                enterise.IdStatusScalping = id;
            }
            catch (Exception ex)
            {
                ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
            }
        }));
    }

    private void CopyMenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        View.ScalpingEntries enterise;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            int newId;

            enterise = sender.ContextConvert<View.ScalpingEntries>();

            using (Mysql ms = new Mysql())
            {
                newId = ms.GetValue<int>($@"SELECT copy_enteries('{enterise.Id}');");
            }

            View.ScalpingEntries newEnterise = DBModel.GetModel<View.ScalpingEntries>(newId);

            MainThread.BeginInvokeOnMainThread(() => ViewOrders.Add(newEnterise));
        }));
    }
}
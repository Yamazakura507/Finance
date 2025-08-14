using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Org.BouncyCastle.Utilities;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Futures;

public partial class ScalpActiveListPage : ContentPage
{
    private bool IsSelected = false;
    public int SelectedIdScalpingActive = -1;

    int offset = 0;
    int count = 0;

    ObservableCollection<View.ScalpingActive> ViewScalpActiv;
    Loading loading { get; set; }

    public ScalpActiveListPage()
    {
        InitializeComponent();

        IsSelected = false;
    }

    public ScalpActiveListPage(bool isSelected = false)
    {
        InitializeComponent();

        IsSelected = isSelected;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (IsSelected)
        {
            AddOrder.IsVisible = false;

            #if !ANDROID && !IOS
                ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
                toolbarItem.Clicked += Back_Clicked;
                this.ToolbarItems.Add(toolbarItem);
            #endif
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                ViewScalpActiv = DBModel.GetCollectionModel<View.ScalpingActive>(
                    new Dictionary<string, object>() { { "IdUser",  InfoAccount.IdUser} }, 
                    StartParametrs.LenListPage, 
                    default, 
                    new Dictionary<string, OrderType>() { { "Id", OrderType.Desc }, { "Name", OrderType.Asc } });

                count = DBModel.Counter<View.ScalpingActive>(default, true);
                offset = StartParametrs.LenListPage;

                if (ViewScalpActiv is null || ViewScalpActiv.Count() == 0) throw new Exception("У вас отсутствуют фьючерсные активы");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        BindableLayout.SetItemsSource(debVSL, ViewScalpActiv);
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

        View.ScalpingActive active;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            active = sender.ContextConvert<View.ScalpingActive>();

            DBModel.GetModel<Models.ScalpingActive>(active.Id).DeleteModel<Models.ScalpingActive>();

            MainThread.BeginInvokeOnMainThread(() => ViewScalpActiv.Remove(active));
        }));
    }
    async private void AddOrder_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new ScalpActiveInfoPage() { BindingContext = null }));
    async private void ScalpActive_Tapped(object sender, TappedEventArgs e)
    {
        View.ScalpingActive active = (View.ScalpingActive)((Grid)sender).BindingContext;

        if (!IsSelected)
        {
            await Navigation.PushAsync(new NavigationPage(new ScalpActiveInfoPage() { BindingContext = DBModel.GetModel<Models.ScalpingActive>(active.Id) }));
        }
        else
        {
            SelectedIdScalpingActive = active.Id;
            this.BackButtonInNavClick();
        }
    }

    private void btAddItem_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                var items = DBModel.GetCollectionModel<View.ScalpingActive>(
                    new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } },
                    StartParametrs.LenListPage, 
                    offset,
                    new Dictionary<string, OrderType>() { { "Id", OrderType.Desc }, { "Name", OrderType.Asc } });

                offset += StartParametrs.LenListPage;

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    for (int i = 0; i < items.Count; i++) ViewScalpActiv.Add(items[i]);
                    btAddItem.IsVisible = count >= offset;
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }

    async private void EditTypeCommissionMenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                View.ScalpingActive active;

                active = sender.ContextConvert<View.ScalpingActive>();

                int id = await this.SheetPicker<View.TypeCommission>("Выбор статуса сделки", ErrProvider);

                if (id == 0) return;

                DBModel.GetModel<Models.ScalpingActive>(active.Id).IdTypeCommission = id;
                active.IdTypeCommission = id;
            }
            catch (Exception ex)
            {
                ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
            }
        }));
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();
}
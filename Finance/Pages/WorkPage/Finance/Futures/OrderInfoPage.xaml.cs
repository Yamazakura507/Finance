using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Finance.Pages.WorkPage.Finance.Futures;

public partial class OrderInfoPage : ContentPage
{
    private int? SelectedIdScalpingActive = null;
    private View.ScalpingActive ScalpActive = null;

    ObservableCollection<View.BeastStatus> ViewBeastStatus { get; set; }
    ObservableCollection<View.TypeCommission> ViewTypeCommission { get; set; }
    ObservableCollection<View.Broker> ViewBroker { get; set; }
    ObservableCollection<View.StatusScalping> ViewStatusScalping { get; set; }

    public int IdScalp { get; set; }

    Loading loading { get; set; }

    public OrderInfoPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
                    colSPP.Width = GridLength.Auto;
        #else
        colSPP.Width = 300;

        if (!this.ToolbarItems.Any(i => i.ClassId == "backButton"))
        {
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back), ClassId = "backButton" };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        }   
        #endif

        if (this.BindingContext is null && !this.ToolbarItems.Any(i => i.ClassId == "saveButton"))
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), ClassId = "saveButton", Text = "Добавить сделку" };
            toolbarItemSave.Clicked += AddOrder_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                ViewBeastStatus = DBModel.GetCollectionModel<View.BeastStatus>();
                ViewTypeCommission = DBModel.GetCollectionModel<View.TypeCommission>();
                ViewBroker = DBModel.GetCollectionModel<View.Broker>();
                ViewStatusScalping = DBModel.GetCollectionModel<View.StatusScalping>();

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    pickerBeastStatus.ItemsSource = ViewBeastStatus;
                    pickerTypeCommission.ItemsSource = ViewTypeCommission;
                    pickerBroker.ItemsSource = ViewBroker;
                    pickerStatusScalp.ItemsSource = ViewStatusScalping;

                    if (this.BindingContext != null)
                    {
                        Models.ScalpingEntries scalpingEntries = (Models.ScalpingEntries)this.BindingContext;

                        pickerBeastStatus.SelectedIndex = pickerBeastStatus.Items.IndexOf(scalpingEntries.BeastStatus.Name);
                        pickerStatusScalp.SelectedIndex = pickerStatusScalp.Items.IndexOf(scalpingEntries.StatusScalping.Name);
                        pickerTypeCommission.SelectedIndex = pickerTypeCommission.Items.IndexOf(
                            scalpingEntries.IdTypeCommission is null ? scalpingEntries.IdScalpingActive is null ? "Валютный контракт" : scalpingEntries.ScalpingActive.TypeCommission.Name : scalpingEntries.TypeCommission.Name);
                        pickerBroker.SelectedIndex = pickerBroker.Items.IndexOf($"{scalpingEntries.Broker.Name}({scalpingEntries.Broker.Commission.ToString("F2")} руб.)");
                    }
                    else if(this.SelectedIdScalpingActive is null)
                    {
                        pickerBeastStatus.SelectedIndex = 0;
                        pickerTypeCommission.SelectedIndex = 0;
                        pickerBroker.SelectedIndex = 0;
                        pickerStatusScalp.SelectedIndex = 0;
                        ClearActiveOrder.IsVisible = false;
                    }
                });
            }
            catch (Exception ex)
            {
                ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
            }
        }));
    }

    private void AddOrder_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.Scalping));
            if (!CheckInsOrder()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_scalp_entries('-1','{IdScalp}','{((View.BeastStatus)pickerBeastStatus.SelectedItem).Id}',@IdTyCom,'{((View.Broker)pickerBroker.SelectedItem).Id}','{((View.StatusScalping)pickerStatusScalp.SelectedItem).Id}',NULL,@IdScAct,@PriceEn,'{AsCountLotEntry.Text}',@PriceEx,@CntInFut,@GO,@Name,@PrStep);", new[]
                    {
                        new MySqlParameter("@IdTyCom", this.SelectedIdScalpingActive is null ? ((View.TypeCommission)pickerTypeCommission.SelectedItem).Id : DBNull.Value),
                        new MySqlParameter("@IdScAct", this.SelectedIdScalpingActive is null ? -1 : this.SelectedIdScalpingActive),
                        new MySqlParameter("@CntInFut", this.SelectedIdScalpingActive is null ? AsCntInFut.Text : DBNull.Value),
                        new MySqlParameter("@GO", this.SelectedIdScalpingActive is null ? AsGO.Text : DBNull.Value),
                        new MySqlParameter("@Name", this.SelectedIdScalpingActive is null ? AsName.Text : DBNull.Value),
                        new MySqlParameter("@PrStep", this.SelectedIdScalpingActive is null ? AsPrSt.Text : DBNull.Value),
                        new MySqlParameter("@PriceEn", AsEntrySum.Text),
                        new MySqlParameter("@PriceEx", AsExitSum.Text)
                    });

                await MainThread.InvokeOnMainThreadAsync(() => this.BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void sum_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsDecimalNumberEntry();

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void ClearActiveOrder_Pressed(object sender, EventArgs e)
    {
        if (this.BindingContext is null)
        {
            this.SelectedIdScalpingActive = null;
            AddActiveTriggerStyle(true);
        }
        else
        {
            ((Models.ScalpingEntries)this.BindingContext).IdScalpingActive = null;
            this.RefrasModelContext<Models.ScalpingEntries>();
        }
    }

    async private void ActiveOrder_Pressed(object sender, EventArgs e)
    {
        ScalpActiveListPage activePage = new ScalpActiveListPage(true);

        activePage.Unloaded += (_,_) =>
        {
            if (activePage.SelectedIdScalpingActive != -1)
            {
                if (this.BindingContext is null)
                {
                    this.SelectedIdScalpingActive = activePage.SelectedIdScalpingActive;
                    AddActiveTriggerStyle(false);
                }
                else
                {
                    ((Models.ScalpingEntries)this.BindingContext).IdScalpingActive = activePage.SelectedIdScalpingActive;
                    this.RefrasModelContext<Models.ScalpingEntries>();
                }
            }
        };

        await Navigation.PushAsync(new NavigationPage(activePage));
    }

    private void count_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsIntNumberEntry();

    private void pickerBeastStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.BeastStatus, Models.ScalpingEntries>(typePicker, "IdBeastStatus");

        if (this.BindingContext is null)
        {
            View.BeastStatus beasStatus = (View.BeastStatus)typePicker.SelectedItem;

            AsBeastStatusProvider.Message = beasStatus.Description;

            BeastStatusImage.Source = ConverFiles.ToImageConvert(beasStatus.Id == 1 ? Properties.Resources.bull : Properties.Resources.bear);

            if (this.ScalpActive != null)
            {
                AsGO.Placeholder = (((View.BeastStatus)pickerBeastStatus.SelectedItem).Id == 1 ? ScalpActive.GOLong.ToString() : ScalpActive.GOShort.ToString()) + " - актив";
            }
        }
    } 

    private void entry_Completed(object sender, EventArgs e) => this.RefrasModelContext<Models.ScalpingEntries>();

    private void pickerTypeCommission_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.TypeCommission, Models.ScalpingEntries>(typePicker, "IdTypeCommission");

        if (this.BindingContext is null)
        {
            View.TypeCommission typeCommission = (View.TypeCommission)typePicker.SelectedItem;

            AsTypeComProvider.Message = String.Format("Коммисия биржи:\nКомисия открытия позиции: {0:P4}\nКомисия закрытия позиции: {1:P4}\nОбщая комисия по позиции: {2:P4}",
                                                    typeCommission.OpenCommission, typeCommission.CloseCommission, typeCommission.OpenCommission + typeCommission.CloseCommission);

            AsTypeComImg.Source = ConverFiles.ToImageConvert(typeCommission.Icon);
        }
    }

    private void pickerBroker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.Broker, Models.ScalpingEntries>(typePicker, "IdBroker");

        if (this.BindingContext is null)
        {
            View.Broker broker = (View.Broker)typePicker.SelectedItem;

            AsBrokerProvider.Message = broker.Description;

            BrokerImg.Source = ConverFiles.ToImageConvert(broker.Icon);
        }
    }

    private void pickerStatusScalp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.StatusScalping, Models.ScalpingEntries>(typePicker, "IdStatusScalping");

        if (this.BindingContext is null)
        {
            View.StatusScalping statusScalping = (View.StatusScalping)typePicker.SelectedItem;

            statusScalpProvider.Message = statusScalping.Description;
        }
    }
    
    private bool CheckInsOrder()
    {
        bool result = true;

        if (String.IsNullOrEmpty(AsEntrySum.Text))
        {
            AsEntrySumProvider.WorkProvider(ProviderType.Alert, "Цена открытия обязательное поле для заполнения");

            result = false;
        }
        if (String.IsNullOrEmpty(AsExitSum.Text))
        {
            AsExitSumProvider.WorkProvider(ProviderType.Alert, "Цена закрытия обязательное поле для заполнения");

            result = false;
        }
        if (String.IsNullOrEmpty(AsCountLotEntry.Text))
        {
            AsEntryCountLotProvider.WorkProvider(ProviderType.Alert, "Количество лотов обязательное поле для заполнения");

            result = false;
        }

        if (this.SelectedIdScalpingActive is null)
        {
            if (String.IsNullOrEmpty(AsName.Text))
            {
                AsNameProvider.WorkProvider(ProviderType.Alert, "Наименование обязательное поле для заполнения");

                result = false;
            }
            if (String.IsNullOrEmpty(AsCntInFut.Text))
            {
                AsCntInFutProvider.WorkProvider(ProviderType.Alert, "Маржинальность обязательное поле для заполнения");

                result = false;
            }
            if (String.IsNullOrEmpty(AsGO.Text))
            {
                AsGOProvider.WorkProvider(ProviderType.Alert, "Гарантийное обеспечение обязательное поле для заполнения");

                result = false;
            }
            if (String.IsNullOrEmpty(AsPrSt.Text))
            {
                AsPrStProvider.WorkProvider(ProviderType.Alert, "Шаг цены обязательное поле для заполнения");

                result = false;
            }
        }

        return result;
    }

    private void AddActiveTriggerStyle(bool isClear)
    {
        try
        {
            ScalpActive = this.SelectedIdScalpingActive is null ? null : DBModel.GetModel<View.ScalpingActive>(this.SelectedIdScalpingActive);

            AsName.IsEnabled = AsCntInFut.IsEnabled = pickerTypeCommission.IsEnabled = AsGO.IsEnabled = AsPrSt.IsEnabled = isClear;
            vScVSL.IsVisible = ClearActiveOrder.IsVisible = !isClear;

            AsNameActive.Text = ScalpActive is null ? "Нет привязки актива" : ScalpActive.Name;

            AsName.Text = AsCntInFut.Text = AsGO.Text = AsPrSt.Text = null;
            AsName.BackgroundColor = AsCntInFut.BackgroundColor = AsGO.BackgroundColor = AsPrSt.BackgroundColor =
                isClear ? Colors.Transparent : Colors.DarkOliveGreen;
            AsName.PlaceholderColor = AsCntInFut.PlaceholderColor = AsGO.PlaceholderColor = AsPrSt.PlaceholderColor =
                isClear ? (Application.Current.RequestedTheme == AppTheme.Light ? (Color)Application.Current.Resources["Gray200"] : (Color)Application.Current.Resources["Gray500"]) : Colors.LightGoldenrodYellow;

            AsName.Placeholder = isClear ? "Наименование" : ScalpActive.Name + " - актив";
            AsCntInFut.Placeholder = isClear ? "Маржинальность" : ScalpActive.CountInFutures.ToString() + " - актив";
            AsPrSt.Placeholder = isClear ? "Шаг цены" : ScalpActive.PriceStep.ToString() + " - актив";
            AsGO.Placeholder = isClear ? "Гаран. обесп." : (((View.BeastStatus)pickerBeastStatus.SelectedItem).Id == 1 ? ScalpActive.GOLong.ToString() : ScalpActive.GOShort.ToString()) + " - актив";
            pickerTypeCommission.SelectedIndex = isClear ? 0 : pickerTypeCommission.Items.IndexOf(ScalpActive.TypeCommission.Name);

            pickerTypeCommission.BackgroundColor = isClear ? pickerBeastStatus.BackgroundColor : Colors.DarkOliveGreen;
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }
}
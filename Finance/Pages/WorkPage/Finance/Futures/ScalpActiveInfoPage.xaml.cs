using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using MySqlConnector;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Futures;

public partial class ScalpActiveInfoPage : ContentPage
{
    ObservableCollection<View.TypeCommission> ViewTypeCommission { get; set; }

    Loading loading { get; set; }

    public ScalpActiveInfoPage(bool isReturnNewActive = false)
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
             colSPP.Width = GridLength.Auto;
        #else
            colSPP.Width = 300;
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        #endif

        if (this.BindingContext is null)
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить актив" };
            toolbarItemSave.Clicked += AddOrder_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                ViewTypeCommission = DBModel.GetCollectionModel<View.TypeCommission>();

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    pickerTypeCommission.ItemsSource = ViewTypeCommission;

                    if (this.BindingContext != null)
                    {
                        Models.ScalpingActive scalpingActive = (Models.ScalpingActive)this.BindingContext;

                        pickerTypeCommission.SelectedIndex = pickerTypeCommission.Items.IndexOf(scalpingActive.TypeCommission.Name);
                    }
                    else
                    {
                        pickerTypeCommission.SelectedIndex = 0;
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
            DBModel.CheckPolice(false, typeof(Models.ScalpingActive));
            if (!CheckInsActive()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                {
                    string sql = @$"INSERT INTO `ScalpingActive` (`Name`, `GOShort`, `GOLong`, `IdTypeCommission`, `PriceStep`, `CountInFutures`, `IdUser`) 
                                    VALUES ('{AsName.Text}',@ShortGO,@LongGO,'{((View.TypeCommission)pickerTypeCommission.SelectedItem).Id}',@PrSt,'{AsCntInFut.Text}','{InfoAccount.IdUser}');";

                    ms.ExecSql(sql, new[]
                    {
                        new MySqlParameter("@ShortGO", AsGOShortSum.Text),
                        new MySqlParameter("@LongGO", AsGOLongSum.Text),
                        new MySqlParameter("@PrSt", AsPriceStepSum.Text)
                    });
                }

                await MainThread.InvokeOnMainThreadAsync(() => ((Page)this.Parent).BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private bool CheckInsActive()
    {
        bool result = true;

        if (String.IsNullOrEmpty(AsName.Text))
        {
            AsNameProvider.WorkProvider(ProviderType.Alert, "Наименование обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsGOLongSum.Text))
        {
            AsGOLongProvider.WorkProvider(ProviderType.Alert, "ГО покупателя обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsGOShortSum.Text))
        {
            AsGOShortProvider.WorkProvider(ProviderType.Alert, "ГО продовца обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsPriceStepSum.Text))
        {
            AsPriceStepProvider.WorkProvider(ProviderType.Alert, "Шаг цены обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsCntInFut.Text))
        {
            AsCntInFutProvider.WorkProvider(ProviderType.Alert, "Маржинальность обязательное поле для заполнения");

            result = false;
        }

        return result;
    }

    private void sum_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsDecimalNumberEntry();

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void count_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsIntNumberEntry();

    private void entry_Completed(object sender, EventArgs e) => this.RefrasModelContext<Models.ScalpingActive>();

    private void pickerTypeCommission_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.TypeCommission, Models.ScalpingActive>(typePicker, "IdTypeCommission");

        if (this.BindingContext is null)
        {
            View.TypeCommission typeCommission = (View.TypeCommission)typePicker.SelectedItem;

            AsTypeComProvider.Message = String.Format("Коммисия биржи:\nКомисия открытия позиции: {0:P4}\nКомисия закрытия позиции: {1:P4}\nОбщая комисия по позиции: {2:P4}",
                                                    typeCommission.OpenCommission, typeCommission.CloseCommission, typeCommission.OpenCommission + typeCommission.CloseCommission);

            AsTypeComImg.Source = ConverFiles.ToImageConvert(typeCommission.Icon);
        }
    }
}
using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using System.Collections.ObjectModel;
using Finance.Classes.AppSettings;

namespace Finance.Pages.WorkPage.Finance.Debtors;

public partial class EditorDebtor : ContentPage
{
    ObservableCollection<View.DebtorStatus> ViewDebtorStatus;
    Loading loading { get; set; }

    public EditorDebtor()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
            colSPP.Width = GridLength.Auto;
        #else
            colSPP.Width = 200;
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        #endif

        if (this.BindingContext is null)
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить должника" };
            toolbarItemSave.Clicked += AddAssets_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                ViewDebtorStatus = DBModel.GetCollectionModel<View.DebtorStatus>();

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    StatusPicker.ItemsSource = ViewDebtorStatus;
                    StatusPicker.SelectedIndex = this.BindingContext is null ? 0 : StatusPicker.Items.IndexOf(((Debtor)this.BindingContext).DebtorStatus.Name);
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void pickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (StatusPicker.SelectedItem is null || this.BindingContext is null) return;

        ((Models.Debtor)this.BindingContext).IdStatusDebtor = ((View.DebtorStatus)StatusPicker.SelectedItem).Id;
    }

    private void AddAssets_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.Debtor));
            if (!CheckInsDebtor()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_debtor(-1,'{AsName.Text}',@Sum,'{((View.DebtorStatus)StatusPicker.SelectedItem).Id}',@Commit,'{InfoAccount.IdUser}')", new[]
                    {
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(AsCommit.Text) ? DBNull.Value : AsCommit.Text),
                        new MySqlParameter("@Sum", AsSum.Text)
                    });

                await MainThread.InvokeOnMainThreadAsync(() => this.BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void sum_TextChanged(object sender, TextChangedEventArgs e) => TextBoxRestrictions.IsDecimalNumberEntry((Entry)sender);

    private bool CheckInsDebtor()
    {
        bool result = true;

        if (String.IsNullOrEmpty(AsName.Text))
        {
            AsNameProvider.WorkProvider(ProviderType.Alert, "Наименование обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsSum.Text) || Convert.ToDecimal(AsSum.Text.Replace('.', ',')) <= 0)
        {
            AsSumProvider.WorkProvider(ProviderType.Alert, "Сумма должна превышать нулевое значение");

            result = false;
        }

        return result;
    }
}
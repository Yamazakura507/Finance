using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Credit;

public partial class CreditInfoPage : ContentPage
{
    ObservableCollection<View.CreditStatus> ViewCreditStatus;
    Loading loading;

	public CreditInfoPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
            colSPP.Width = GridLength.Auto;
        #else
            colSPP.Width = 200;
        #endif

        startDate.MinimumDate = DateTime.Now.AddYears(-80);
        endDate.MaximumDate = DateTime.Now.AddYears(80);

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                ViewCreditStatus = DBModel.GetCollectionModel<View.CreditStatus>();
                await MainThread.InvokeOnMainThreadAsync(() => pickerStatus.ItemsSource = ViewCreditStatus);

                if (this.BindingContext is null)
                {
                    ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить кредит" };
                    toolbarItem.Clicked += AddCredit_Clicked;

                    MainThread.BeginInvokeOnMainThread(() => 
                    { 
                        ((Page)this.Parent).ToolbarItems.Add(toolbarItem);
                        pickerStatus.SelectedIndex = pickerStatus.Items.IndexOf("Черновик");
                        ErrProvider.WorkProvider(ProviderType.Info, "Расчет будет произведен после добавления!");
                    });
                
                    return;
                }

                await MainThread.InvokeOnMainThreadAsync(() => 
                {
                    var credit = (Models.Credit)this.BindingContext;

                    if (credit.EndDate == credit.RealEndDate) realEndDate.IsVisible = false;
                    if (credit.OverpaymentSum == credit.OverpaymentSumReal) realOverSum.IsVisible = false;

                    pickerStatus.SelectedIndex = pickerStatus.Items.IndexOf(((Models.Credit)this.BindingContext).CreditStatus.Name);
                });
            }
            catch (Exception ex) 
            {
                ErrProvider.WorkProvider(ProviderType.Error,ex.Message);
            }
        }));


    }

    private void AddCredit_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.Credit));
            if (!CheckInsCredit()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_credit(@StartDate,@EndDate,@Purpoce,'{DBModel.ConvertToMySqlDecimal(percent.Text)}',@Commit,'{DBModel.ConvertToMySqlDecimal(startSum.Text)}','{((View.CreditStatus)pickerStatus.SelectedItem).Id}','-1',@IdUser)", new[]
                    {
                        new MySqlParameter("@StartDate", startDate.Date),
                        new MySqlParameter("@EndDate", endDate.Date),
                        new MySqlParameter("@Purpoce", String.IsNullOrEmpty(purpose.Text) ? DBNull.Value : purpose.Text),
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(commit.Text) ? DBNull.Value : commit.Text),
                        new MySqlParameter("@IdUser", InfoAccount.IdUser)
                    });
            }));

            ((Page)this.Parent).BackButtonInNavClick();
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void pickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pickerStatus.SelectedItem is null || this.BindingContext is null) return;

        ((Models.Credit)this.BindingContext).IdStatusCredit = ((View.CreditStatus)pickerStatus.SelectedItem).Id;
    }

    private bool CheckInsCredit()
    {
        bool check = true;

        if ((new DateTime(1,1,1)+(endDate.Date-startDate.Date)).Year-1 > 80) 
        {
            DateProvider.WorkProvider(ProviderType.Alert, "Вероятно вы всретитесь с создателем раньше чем расплатитесь с долгом.\nРазница даты выдачи займа и даты выплаты займа не должна превышать 80 лет!");
            check = false;
        }

        if (check && (endDate.Date.Year - startDate.Date.Year) * 12 + endDate.Date.Month - startDate.Date.Month + (endDate.Date.Day <= startDate.Date.Day ? 0 : -1) < 1)
        {
            DateProvider.WorkProvider(ProviderType.Alert, "Слишком короткий срок займа.\nМинимальный срок займа 1 месяц!");
            check = false;
        }

        if (String.IsNullOrEmpty(startSum.Text) || Convert.ToDecimal(startSum.Text.Replace('.',',')) < 10000)
        {
            SumProvider.WorkProvider(ProviderType.Alert, "Слишком маленькая сумма займа.\nМинимальная сумма займа 10 000₽!");
            check = false;
        }

        if (String.IsNullOrEmpty(percent.Text))
        {
            ErrProvider.WorkProvider(ProviderType.Alert, "Процентная ставка обязательное поле для заполнения!");
            check = false;
        }

        return check;
    }

    private void percent_TextChanged(object sender, TextChangedEventArgs e) => TextBoxRestrictions.IsDecimalNumberEntry((Entry)sender);
}
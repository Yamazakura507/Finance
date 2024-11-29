using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.Enums;
using Finance.CustomControl;

namespace Finance.Pages.WorkPage.Credit;

public partial class LoanPaymentsInfoPage : ContentPage
{
    private int selFirst = -1;
    private Models.Credit credit;
    public int idCredit = -1;
    Loading loading;

    public LoanPaymentsInfoPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        this.ToolbarItems.Clear();

        #if ANDROID || IOS
            colSPP.Width = GridLength.Auto;
        #else
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
            colSPP.Width = 200;
        #endif

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                if (this.BindingContext is null)
                {
                    ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить платёж" };
                    toolbarItem.Clicked += AddLoanPayment_Clicked;

                    credit = DBModel.GetModel<Models.Credit>(idCredit);

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        datePay.MaximumDate = credit.EndDate;
                        datePay.MinimumDate = credit.StartDate;

                        this.ToolbarItems.Add(toolbarItem);
                        pickerStatus.SelectedIndex = 0;
                        ErrProvider.WorkProvider(ProviderType.Info, "Расчет будет произведен после добавления!");
                    });

                    return;
                }
            }
            catch (Exception ex)
            {
                ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
            }
        }));
    }

    private void AddLoanPayment_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.LoanPayments));
            if (!CheckInsCredit()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                   await ms.ExecSqlAsync($"SELECT ins_upd_loan_payments(-1,'{idCredit}',@IdAssets,@Sum,@IsBasic,@Commit,@IsTerm,@DatePay);", new[]
                    {
                        new MySqlParameter("@IsTerm", pickerStatus.SelectedIndex == 0 ? DBNull.Value : pickerStatus.SelectedIndex == 1),
                        new MySqlParameter("@IsBasic", pickerStatus.SelectedIndex == 0),
                        new MySqlParameter("@IdAssets", DBNull.Value),
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(commit.Text) ? DBNull.Value : commit.Text),
                        new MySqlParameter("@Sum", sum.Text),
                        new MySqlParameter("@DatePay", datePay.Date)
                    });
            }));

            this.BackButtonClick();
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonClick();

    private void pickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pickerStatus.SelectedItem is null) return;

        new Loading().LoadingBackgorundWorker.RunWorkerAsync(new Thread(async() =>
        {
            int sel = ((Picker)sender).SelectedIndex;

            if (sel == 0)
            {
                string sumr = String.Empty;
                string comand = "CALL future_payments_auto_time('{0}');SELECT vfpt.`Sum` FROM `view_future_payments_tmp` vfpt ORDER BY vfpt.`DatePay` ASC LIMIT 1;";

                if (this.BindingContext != null)
                {
                    Models.LoanPayments loanPayments = (Models.LoanPayments)this.BindingContext;

                    loanPayments.IsBasic = true;
                    loanPayments.IsTerm = null;

                    if (selFirst == 0 || (sel == 0 && selFirst == -1)) sumr = loanPayments.Sum.ToString();
                    else sumr = DBModel.ResultRequest(String.Format(comand, loanPayments.IdCredit)).ToString();
                }
                else
                {
                    sumr = DBModel.ResultRequest(String.Format(comand, this.idCredit)).ToString();
                }

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    sum.Text = sumr;
                    sum.IsEnabled = false;
                });
            }
            else 
            {
                if (this.BindingContext != null)
                {
                    Models.LoanPayments loanPayments = (Models.LoanPayments)this.BindingContext;

                    loanPayments.IsBasic = false;
                    loanPayments.IsTerm = sel == 1;
                }

                if (selFirst != -1)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        sum.Text = String.Empty;
                        sum.IsEnabled = true;
                    });
                }
            }

            if (selFirst == -1) selFirst = sel;
        }));
    }

    private void sum_TextChanged(object sender, TextChangedEventArgs e) => TextBoxRestrictions.IsDecimalNumberEntry((Entry)sender);

    private bool CheckInsCredit()
    {
        bool check = true;

        if (String.IsNullOrEmpty(sum.Text) || Convert.ToDecimal(sum.Text.Replace('.', ',')) <= 0)
        {
            SumProvider.WorkProvider(ProviderType.Alert, "Слишком маленькая сумма платежа.\nМинимальная сумма платежа 1₽!");
            check = false;
        }

        if (check && credit.Sum < Convert.ToDecimal(sum.Text.Replace('.', ',')))
        {
            SumProvider.WorkProvider(ProviderType.Alert, "Слишком большая сумма платежа.\nСумма платежа превышает сумму остатка задолжности!");
            check = false;
        }

        return check;
    }
}
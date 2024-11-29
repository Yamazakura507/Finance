using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.View.CALL;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Credit;

public partial class FuturePaymentsPage : ContentPage
{
    ObservableCollection<view_future_payments_tmp> ViewLoanPayments;
    Loading loading;

    public FuturePaymentsPage()
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
                ViewLoanPayments = DBModel.GetCollectionModel<view_future_payments_tmp>($"CALL future_payments_auto_time('{((Models.Credit)this.BindingContext).Id}');SELECT * FROM `view_future_payments_tmp` vfpt ORDER BY vfpt.`DatePay`;");

                if (ViewLoanPayments is null || ViewLoanPayments.Count() == 0) throw new Exception("У вас отсутствуют платежи");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(loanVSL, ViewLoanPayments));
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
}
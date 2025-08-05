using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.Enums;
using Finance.CustomControl;
using SkiaSharp.Extended.UI.Controls;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Credit;

public partial class LoanPaymentsPage : ContentPage
{
    ObservableCollection<View.LoanPayments> ViewLoanPayments;
    Loading loading;

    public LoanPaymentsPage()
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
                var credit = (Models.Credit)this.BindingContext;

                addBt.IsVisible = credit.Sum > 0;
                ViewLoanPayments = DBModel.GetCollectionModel<View.LoanPayments>(new Dictionary<string, object>() { { "IdCredit", credit.Id } }, default,default, new Dictionary<string, OrderType>() { { "DatePay", OrderType.Desc } });

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

    async private void AddImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoanPaymentsInfoPage() { idCredit = ((Models.Credit)this.BindingContext).Id });
    }

    private void DeleteImageButton_Clicked(object sender, EventArgs e)
    {
        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            View.LoanPayments loanPymentsView = (View.LoanPayments)((ContentView)((Grid)((ImageButton)sender).Parent).Parent).BindingContext;
            Models.LoanPayments loanPayments = DBModel.GetModel<Models.LoanPayments>(loanPymentsView.Id);

            loanPayments.DeleteModel<Models.LoanPayments>();
            MainThread.BeginInvokeOnMainThread(() => ViewLoanPayments.Remove(loanPymentsView));
        }));
    }

    async private void LoanPaymens_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoanPaymentsInfoPage() { BindingContext = DBModel.GetModel<Models.LoanPayments>(((View.LoanPayments)((ContentView)sender).BindingContext).Id) });
    }
}
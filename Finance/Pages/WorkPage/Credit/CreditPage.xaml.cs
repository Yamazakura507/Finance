using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.Tabbed;
using SkiaSharp.Extended.UI.Controls;
using System;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Credit;

public partial class CreditPage : ContentPage
{
    ObservableCollection<View.Credit> ViewCredits;
    bool IsDeleting = false;
    Loading loading;

	public CreditPage()
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
                ViewCredits = DBModel.GetCollectionModel<View.Credit>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewCredits is null || ViewCredits.Count() == 0) throw new Exception("У вас отсутствуют платежи");
                else MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(creditsList,ViewCredits));
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }

    async private void AddImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationPage(new CreditInfoTappedPage()));
    }

    private void DeleteImageButton_Clicked(object sender, EventArgs e)
    {
        if (IsDeleting) ((ImageButton)sender).ScaleTo(1,500,Easing.Linear);
        else ((ImageButton)sender).ScaleTo(0.9,500,Easing.Linear);

        IsDeleting = !IsDeleting;
    }

    private void Credit_Tapped(object sender, TappedEventArgs e)
    {
        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async() =>
        {
            ContentView cw = (ContentView)sender;
            View.Credit creditView = (View.Credit)cw.BindingContext;
            Models.Credit credit = DBModel.GetModel<Models.Credit>(creditView.Id);

            if (IsDeleting)
            {
                SKLottieView lottie = (SKLottieView)((Grid)cw.Content).Children[0];
                MainThread.BeginInvokeOnMainThread(() => lottie.IsAnimationEnabled = lottie.IsVisible = true);

                credit.DeleteModel<Models.Credit>();
                MainThread.BeginInvokeOnMainThread(() => ViewCredits.Remove(creditView));
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(async() => await Navigation.PushAsync(new NavigationPage(new CreditInfoTappedPage() { BindingContext = credit })));
            }
        }));
    }
}
using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using SkiaSharp.Extended.UI.Controls;

namespace Finance.Pages;

public partial class RegistrationPage : ContentPage
{
    Loading loading;
    StartParametrs parametrs = new StartParametrs();
    private string rand = null;

    public RegistrationPage()
	{
		InitializeComponent();
    }

    private void RegistrBtOnPressed(object sender, EventArgs e)
    {
        bool isEmpty = false;

        loginProvider.IsVisible = passwordProvaider.IsVisible = errorProvider.IsVisible = isEmpty;

        try
        {
            if (!TextBoxRestrictions.TextEmptyTextBox(Login))
            {
                loginProvider.WorkProvider(ProviderType.Info, "Ћогин об€зательное поле дл€ заполнени€");
                isEmpty = true;
            }

            if (!TextBoxRestrictions.TextEmptyTextBox(Password))
            {
                passwordProvaider.WorkProvider(ProviderType.Info, "ѕароль об€зательное поле дл€ заполнени€");
                isEmpty = true;
            }

            if (!TextBoxRestrictions.TextEmptyTextBox(Email))
            {
                emailProvider.WorkProvider(ProviderType.Info, "Email об€зательное поле дл€ заполнени€");
                isEmpty = true;
            }
            else
            {
                if (rand is null)
                {
                    rand = this.EmailTextBoxCheck(Email, Login.Text);
                    GPC.IsVisible = true;
                    RegistrBt.Text = "ѕродолжить";
                    isEmpty = true;
                }
                else
                {
                    if (!this.ContinueEmailTextBoxCheck(rand, CodeMail))
                    {
                        codeMailProvaider.WorkProvider(ProviderType.Info, "Ќе верный или пустой код подтверждени€");
                        isEmpty = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorProvider.WorkProvider(ProviderType.Error, ex.Message);
            isEmpty = true;
        }

        if (isEmpty) return;

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                int idUser = Convert.ToInt32(DBModel.ResultRequest($"SELECT ins_upd_user('{Login.Text}','{Password.Text}','{Email.Text}',-1)"));

                if (idUser == -2)
                {
                    throw new Exception("ѕользователь с указаным логином и паролем уже существует");
                }
                else 
                {
                    if (cbStorageUser.IsChecked) parametrs.IdAutorizateUser = idUser;
                    MainThread.BeginInvokeOnMainThread(new Action(() => this.Messege("¬ы зарегестрированы!", ProviderType.Info)));

                    this.MailGoodRegistration(Email.Text, new Dictionary<string, string>()
                    {
                        { "@Login", Login.Text }
                    });

                    ((TabbedPage)App.Current.MainPage).CurrentPage = new AutorizationPage();
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(new Action(() => errorProvider.WorkProvider(ProviderType.Error, ex.Message)));
            }
        }));

        GC.Collect();
    }

    private void HideAnimationOnTapped(object sender, TappedEventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
        LottieAnimation.HideAnimationStart(btlPass);
    }

    private new void Loaded(object sender, EventArgs e)
    {
            #if ANDROID || IOS
                colSA.Width = colSPL.Width = colSPP.Width = colSPE.Width = colSC.Width = colSPC.Width = GridLength.Auto;
                lbCb.Margin = new Thickness(0, 0, 0, 0);
            #else
                colSA.Width = colSPL.Width = colSPP.Width = colSPE.Width = colSC.Width = colSPC.Width = 200;
                GA.Margin = GP.Margin = GL.Margin = GE.Margin = GCb.Margin = GPC.Margin = new Thickness(colSPL.Width.Value / 2, 10, 0, 0);
                lbRegistr.Margin = new Thickness(0, 0, colSPL.Width.Value / 2, 10);
                GA.MaximumWidthRequest = GP.MaximumWidthRequest = GL.MaximumWidthRequest = GE.MaximumWidthRequest = GCb.MaximumWidthRequest = GPC.MaximumWidthRequest = colSPL.Width.Value + 400;
            #endif

        LottieAnimation.HideAnimationStop(btlPass);
        LottieAnimation.HideAnimationStop(btlEmail);
        cbStorageUser.IsChecked = true;
    }

    private void LabelCbOnTapped(object sender, TappedEventArgs e) => cbStorageUser.IsChecked = !cbStorageUser.IsChecked;

    private void EmailButtonOnTapped(object sender, TappedEventArgs e)
    {
        if (TextBoxRestrictions.TextEmptyTextBox(Email))
        {
            rand = this.EmailTextBoxCheck(Email, Login.Text);
            LottieAnimation.HideAnimationStart(btlEmail, 4100);
        }
        else 
        {
            emailProvider.WorkProvider(ProviderType.Info, "Email об€зательное поле дл€ заполнени€");
        }
    }
}
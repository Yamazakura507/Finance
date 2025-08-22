using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.CustomControl;

namespace Finance.Pages.WorkPage;

public partial class SettingsPage : ContentPage
{
    Loading loading;
    private string rand = null;

    public SettingsPage()
	{
		InitializeComponent();
	}

    private new void Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
            colSLL.Width = colSPL.Width = colSPP.Width = colSPE.Width = GridLength.Auto;
        #else
            colSLL.Width = colSPL.Width = colSPP.Width = colSPE.Width = 200;
            GLL.Margin = GP.Margin = GL.Margin = GE.Margin = new Thickness(colSPL.Width.Value / 2, 10, 0, 0);
            GLL.MaximumWidthRequest = GP.MaximumWidthRequest = GL.MaximumWidthRequest = GE.MaximumWidthRequest = colSPL.Width.Value + 400;
        #endif

        LottieAnimation.HideAnimationStop(btlPass);
    }

    private void HideAnimationOnTapped(object sender, TappedEventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
        LottieAnimation.HideAnimationStart(btlPass);
    }

    private void LenghtListPage_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            Entry entry = (Entry)sender;
            TextBoxRestrictions.IsIntNumberEntry(entry);

            if (TextBoxRestrictions.TextEmptyTextBox(entry))
            {
                StartParametrs.LenListPage = Convert.ToInt32(entry.Text);
            }
        }
        catch (Exception) { }
    }
}
using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.CustomControl;

namespace Finance.Pages.WorkPage.Support;

public partial class SupportInfoPage : ContentPage
{
    private bool isLoaded = false;
    private int id;
    Loading loading { get; set; }

    public SupportInfoPage(int id)
	{
		InitializeComponent();
        this.id = id;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
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
            var supprot = DBModel.GetModel<Models.Support>(id);
            await MainThread.InvokeOnMainThreadAsync(() => this.BindingContext = supprot);

            if (supprot.IsAnswer && !supprot.IsReadAnswer && !InfoAccount.IsAdmin)
            {
                InfoAccount.CountSupport -= 1;
                supprot.IsReadAnswer = true;
            }

            isLoaded = true;
        }));
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void appe_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (isLoaded) ans.Text = String.Empty;
    }
}
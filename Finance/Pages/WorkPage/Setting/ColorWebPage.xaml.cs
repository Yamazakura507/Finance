using Finance.Classes;
using Finance.CustomControl;
using ColorLib.ColorEnum;
using Color = ColorLib.Color;

namespace Finance.Pages.WorkPage.Setting;

public partial class ColorWebPage : ContentPage
{
    private string hex;
    Loading loading { get; set; }

    public ColorWebPage(string hex)
    {
        InitializeComponent();
        this.hex = hex;
    }

    async private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>()
        {
            { "hex", hex.Replace("#", String.Empty) },
        };

        web.Source = Color.UrlConcatColor(ColorApi.id, parametrs, true);

        #if ANDROID || IOS
        #else
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        #endif
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

}
using Finance.Classes;

namespace Finance.Pages.WorkPage.Setting;

public partial class BrouserPage : ContentPage
{
    public bool IsUpdate { get; set; } = false;
    public Uri NewLink { get; set; } = null;

    private string link;
    private bool isRead;

    public BrouserPage(bool isRead = false)
    {
        InitializeComponent();
        this.isRead = isRead;
    }

    public BrouserPage(string link, bool isRead = false)
    {
        InitializeComponent();
        this.link = link;
        this.IsUpdate = true;
        this.isRead = isRead;
    }

    async private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (link != null)
        {
            web.Source = new Uri(link);
        }

        if (!this.isRead)
        {
            #if ANDROID || IOS
            #else
                ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
                toolbarItem.Clicked += Back_Clicked;
                this.ToolbarItems.Add(toolbarItem);
            #endif

            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Выбрать" };
            toolbarItemSave.Clicked += ToolbarItemSave_Clicked; ;
            this.ToolbarItems.Add(toolbarItemSave);
        }
    }

    async private void ToolbarItemSave_Clicked(object? sender, EventArgs e)
    {
        this.NewLink = new Uri(await web.EvaluateJavaScriptAsync("window.location.href"));

        this.BackButtonInNavClick();
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();
}
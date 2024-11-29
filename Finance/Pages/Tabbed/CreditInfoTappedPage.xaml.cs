using Finance.Classes;

namespace Finance.Pages.Tabbed;

public partial class CreditInfoTappedPage : TabbedPage
{
	public CreditInfoTappedPage()
	{
		InitializeComponent();
	}

    private void TabbedPage_Loaded(object sender, EventArgs e)
    {
        this.ToolbarItems.Clear();

        #if WINDOWS
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        #endif

        if (this.BindingContext is null)
        {
            this.Children.RemoveAt(2);
            this.Children.RemoveAt(1);
        }
        else if (((Models.Credit)this.BindingContext).Sum <= 0) 
        {
            this.Children.RemoveAt(2);
        }
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();
}
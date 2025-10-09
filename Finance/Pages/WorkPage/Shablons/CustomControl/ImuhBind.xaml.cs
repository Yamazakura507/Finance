using Finance.Classes;
using Finance.Models;
using Finance.Pages.WorkPage.Imuh;

namespace Finance.Pages.WorkPage.Shablons.CustomControl;

public partial class ImuhBind : ContentView
{
    public static readonly BindableProperty OwnerProperty = BindableProperty.Create(nameof(Estate), typeof(Estate), typeof(ImuhBind), null);
    
    public Estate Estate
    {
        get => (Estate)GetValue(ImuhBind.OwnerProperty);
        set
        {
            SetValue(ImuhBind.OwnerProperty, value);
        } 
    }

    public ImuhBind()
	{
		InitializeComponent();
	}

    async private void TapSelectOwner_Tapped(object sender, TappedEventArgs e)
    {
        EstateListPage ownersList = new EstateListPage(true);

        ownersList.Unloaded += (_, _) =>
        {
            if (ownersList.SelectedIdEstate != -1)
            {
                this.Estate = DBModel.GetModel<Estate>(ownersList.SelectedIdEstate);
            }
        };

        await Navigation.PushAsync(new NavigationPage(ownersList));
    }

    private void Delete_Task(object sender, EventArgs e)
    {
        ((VerticalStackLayout)this.Parent).Remove(this);
    }

    private void ContentView_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
                colSPP.Width = GridLength.Auto;
        #else
                colSPP.Width = 200;
        #endif
    }
}
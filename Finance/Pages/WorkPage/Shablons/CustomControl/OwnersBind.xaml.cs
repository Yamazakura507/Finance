using Finance.Classes;
using Finance.Models;
using Finance.Pages.WorkPage.Imuh;

namespace Finance.Pages.WorkPage.Shablons.CustomControl;

public partial class OwnersBind : ContentView
{
    public static readonly BindableProperty OwnerProperty = BindableProperty.Create(nameof(Owner), typeof(Owners), typeof(OwnersBind), null);
    
    public Owners Owner
    {
        get => (Owners)GetValue(OwnersBind.OwnerProperty);
        set
        {
            SetValue(OwnersBind.OwnerProperty, value);
        } 
    }

    public OwnersBind()
	{
		InitializeComponent();
	}

    async private void TapSelectOwner_Tapped(object sender, TappedEventArgs e)
    {
        OwnersList ownersList = new OwnersList(true);

        ownersList.Unloaded += (_, _) =>
        {
            if (ownersList.SelectedIdOwners != -1)
            {
                this.Owner = DBModel.GetModel<Owners>(ownersList.SelectedIdOwners);
            }
        };

        await Navigation.PushAsync(new NavigationPage(ownersList));
    }

    private void ContentView_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
                colSPP.Width = GridLength.Auto;
        #else
                        colSPP.Width = 200;
        #endif
    }

    private void Delete_Task(object sender, EventArgs e)
    {
        ((VerticalStackLayout)this.Parent).Remove(this);
    }
}
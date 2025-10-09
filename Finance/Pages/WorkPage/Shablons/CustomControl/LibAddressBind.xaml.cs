using Finance.Classes;
using Finance.Pages.WorkPage.Setting;

namespace Finance.Pages.WorkPage.Shablons.CustomControl;

public partial class LibAddressBind : ContentView
{
    public static readonly BindableProperty OwnerProperty = BindableProperty.Create(nameof(Address), typeof(View.LibAddress), typeof(LibAddressBind), null);
    
    public View.LibAddress Address
    {
        get => (View.LibAddress)GetValue(LibAddressBind.OwnerProperty);
        set
        {
            SetValue(LibAddressBind.OwnerProperty, value);
        } 
    }

    public LibAddressBind()
	{
		InitializeComponent();
	}

    async private void TapSelectOwner_Tapped(object sender, TappedEventArgs e)
    {
        AddressPage addressList = new AddressPage(true);

        addressList.Unloaded += (_, _) =>
        {
            if (addressList.SelectedIdAddress != -1)
            {
                this.Address = DBModel.GetModel<View.LibAddress>(addressList.SelectedIdAddress);
            }
        };

        await Navigation.PushAsync(new NavigationPage(addressList));
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
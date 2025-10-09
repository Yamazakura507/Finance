using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using Finance.Pages.WorkPage.Setting;
using System.Text;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Finance.Pages.WorkPage.Imuh;

public partial class EditorOwners : ContentPage
{
    private int maxYearHuman = 122;
    private int minYearOfPass = 14;
    private int? SelectedIdAddressBith = null;
    private int? SelectedIdAddressReg = null;

    Loading loading { get; set; }

    public EditorOwners()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
            colSPP.Width = GridLength.Auto;
            lbCb.Margin = new Thickness(0, 0, 0, 0);
        #else
            if (!this.ToolbarItems.Any(i => i.ClassId == "backButton"))
            {
                colSPP.Width = 200;
                ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back), ClassId = "backButton" };
                toolbarItem.Clicked += Back_Clicked;
                this.ToolbarItems.Add(toolbarItem);
            }
        #endif

        if (this.BindingContext is null && !this.ToolbarItems.Any(i => i.ClassId == "saveButton"))
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), ClassId = "saveButton", Text = "Создать собственика" };
            toolbarItemSave.Clicked += AddOwner_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        AsDateBithPass.MinimumDate = DateTime.Now.AddYears(-maxYearHuman);
        AsDateBithPass.MaximumDate = DateTime.Now.AddYears(-minYearOfPass);
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void AddOwner_Clicked(object? sender, EventArgs e)
    {
        try
        {
            if (!CheckInsEstate()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                Dictionary<string, object> insParVal = new Dictionary<string, object>()
                {
                    { "FullName", AsFullName.Text },
                    { "SeriesPass", AsSeriesPass.Text },
                    { "NumberPass", AsNumPass.Text },
                    { "DateRegPass", AsDateRegPass.Date.ConvertToMySqlDate() },
                    { "DepartmentCodePass", AsDepartmentCodePass.Text},
                    { "OrgIssuedPass", AsOrgIssuedPass.Text},
                    { "Birthday", AsDateBithPass.Date.ConvertToMySqlDate() },
                    { "IdBirthdayAddress", this.SelectedIdAddressBith},
                    { "IdRegAddress", this.SelectedIdAddressReg},
                    { "OwnerIsUser", cbIsUser.IsChecked},
                    { "Phone", String.IsNullOrEmpty(AsPhone.Text) ? null : AsPhone.Text},
                    { "PostIndex", String.IsNullOrEmpty(AsPostCode.Text) ? null : AsPostCode.Text},
                    { "Email", String.IsNullOrEmpty(AsEmail.Text) ? null : AsEmail.Text},
                    { "IdUser", InfoAccount.IdUser}
                };

                int id = DBModel.InsertModel<Models.Owners,int>(insParVal,"Id");

                await MainThread.InvokeOnMainThreadAsync(() => this.BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private bool CheckInsEstate()
    {
        bool result = true;
        StringBuilder builderPass = new StringBuilder();

        if (String.IsNullOrEmpty(AsFullName.Text))
        {
            AsFullNameProvider.WorkProvider(ProviderType.Alert, "ФИО обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(this.AsSeriesPass.Text) || this.AsSeriesPass.Text.Length < 4)
        {
            builderPass.Append("Серия обязательное поле для заполнения");
            builderPass.AppendLine("Серия это строка из 4 цифр");
        }

        if (String.IsNullOrEmpty(this.AsNumPass.Text) || this.AsNumPass.Text.Length < 6)
        {
            builderPass.AppendLine("Номер обязательное поле для заполнения");
            builderPass.AppendLine("Номер это строка из 6 цифр");
        }

        if (builderPass.Length > 0) 
        {
            AsPassProvider.WorkProvider(ProviderType.Alert, builderPass.ToString());

            result = false;
        }

        if (String.IsNullOrEmpty(AsDepartmentCodePass.Text) || this.AsDepartmentCodePass.Text.Length < 7)
        {
            AsDepartmentCodePassProvider.WorkProvider(ProviderType.Alert, "Код обязательное поле для заполнения\nКод это строка из 6 цифр");

            result = false;
        }

        if (SelectedIdAddressBith is null)
        {
            AsBithAddressProvider.WorkProvider(ProviderType.Alert, "Место рождение это обязательная привязка для заполнения");

            result = false;
        }

        if (SelectedIdAddressReg is null)
        {
            AsRegAddressProvider.WorkProvider(ProviderType.Alert, "Адрес регистрации это обязательная привязка для заполнения");

            result = false;
        }

        if (AsPhone.Text != null && AsPhone.Text.Length < 18) AsPhone.Text = String.Empty;
        if (AsPostCode.Text != null && AsPostCode.Text.Length < 6) AsPostCode.Text = String.Empty;
        if (AsEmail.Text != null && !(AsEmail.Text.Contains("@") && AsEmail.Text.Contains("."))) AsEmail.Text = String.Empty;

        return result;
    }

    async private void TapGestureRecognizerAddress(object sender, TappedEventArgs e)
    {
        bool isBith = ((HorizontalStackLayout)sender).ClassId == "BA";
        AddressPage addressList = new AddressPage(true);

        addressList.Unloaded += (_, _) =>
        {
            if (addressList.SelectedIdAddress != -1)
            {
                if (this.BindingContext is null)
                {
                    if (isBith)
                    {
                        this.SelectedIdAddressBith = addressList.SelectedIdAddress;
                        AsAddressBith.Text = DBModel.ResultRequest<string, LibAddress>(default, "Address", addressList.SelectedIdAddress);
                    }
                    else
                    {
                        this.SelectedIdAddressReg = addressList.SelectedIdAddress;
                        AsAddressReg.Text = DBModel.ResultRequest<string, LibAddress>(default, "Address", addressList.SelectedIdAddress);
                    }
                }
                else
                {
                    if (isBith)
                    {
                        ((Models.Owners)this.BindingContext).IdBirthdayAddress = addressList.SelectedIdAddress;
                    }
                    else
                    {
                        ((Models.Owners)this.BindingContext).IdRegAddress = addressList.SelectedIdAddress;
                    }
                }
            }
        };

        await Navigation.PushAsync(new NavigationPage(addressList));
    }

    private void TapGestureRecognizerChkLbIsUser(object sender, TappedEventArgs e)
    {
        cbIsUser.IsChecked = !cbIsUser.IsChecked;
    }

    private void AsPostCode_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsIntNumberEntry();

    private void AsPhone_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsPhoneEntry();

    private void AsDepartmentCodePass_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsIntNumberEntry(@"-");

    private void AsDateBithPass_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (AsDateBithPass != null && AsDateRegPass != null)
        {
            AsDateRegPass.MinimumDate = AsDateBithPass.Date.AddYears(14);
        }
    }
}
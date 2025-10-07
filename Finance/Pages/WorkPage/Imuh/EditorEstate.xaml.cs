using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Imuh;

public partial class EditorEstate : ContentPage
{
    ObservableCollection<View.EstateStatus> ViewEstateStatus;
    ObservableCollection<View.EstateType> ViewEstateType;
    private int? SelectedIdOwners = null;
    private int? SelectedIdImuh = null;

    Loading loading { get; set; }

    public EditorEstate()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
                    colSPP.Width = GridLength.Auto;
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
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), ClassId = "saveButton", Text = "Привязать собственика" };
            toolbarItemSave.Clicked += AddEstate_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                ViewEstateStatus = DBModel.GetCollectionModel<View.EstateStatus>();
                ViewEstateType = DBModel.GetCollectionModel<View.EstateType>();

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    StatusPicker.ItemsSource = ViewEstateStatus;
                    StatusPicker.SelectedIndex = this.BindingContext is null ? 0 : StatusPicker.Items.IndexOf(((Estate)this.BindingContext).EstateStatus.Name);

                    TypePicker.ItemsSource = ViewEstateType;
                    TypePicker.SelectedIndex = this.BindingContext is null ? 0 : TypePicker.Items.IndexOf(((Estate)this.BindingContext).EstateType.Name);
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void pickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.EstateStatus, Models.Estate>(typePicker, "IdStatusEstate");

        if (this.BindingContext is null)
        {
            View.EstateStatus type = (View.EstateStatus)typePicker.SelectedItem;

            AsStatusPickerProvider.Message = type.Description;
        }
    }

    private void AddEstate_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.Estate));
            if (!CheckInsEstate()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                int idType = ((View.EstateType)TypePicker.SelectedItem).Id;

                Dictionary<string, object> insParVal = new Dictionary<string, object>()
                {
                    { "Name", AsName.Text },
                    { "Description", String.IsNullOrEmpty(AsCommit.Text) ? DBNull.Value : AsCommit.Text },
                    { "IdStatusEstate", ((View.EstateStatus)StatusPicker.SelectedItem).Id},
                    { "IdTypeEstate", idType},
                    { "IdOwner", this.SelectedIdOwners},
                    { "IdCar", idType == 1 ? this.SelectedIdImuh : DBNull.Value},
                    { "IdUser", InfoAccount.IdUser}
                };

                DBModel.InsertModel<Models.Estate,int>(insParVal,"Id");

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

        if (String.IsNullOrEmpty(AsName.Text))
        {
            AsNameProvider.WorkProvider(ProviderType.Alert, "Наименование обязательное поле для заполнения");

            result = false;
        }

        if (this.SelectedIdOwners is null)
        {
            AsOwnerProvider.WorkProvider(ProviderType.Alert, "Собственик обязательная привязка для заполнения");

            result = false;
        }

        if (this.SelectedIdImuh is null)
        {
            AsTypePickerProvider.WorkProvider(ProviderType.Alert, "Имущество обязательная привязка для заполнения");

            result = false;
        }

        return result;
    }

    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.EstateType, Models.Estate>(typePicker, "IdTypeEstate");

        if (this.BindingContext is null)
        {
            View.EstateType type = (View.EstateType)typePicker.SelectedItem;

            AsTypePickerProvider.Message = type.Description;

            ImuhImg.Source = ConverFiles.ToImageConvert(type.Icon is null ? Properties.Resources.imuh : type.Icon, type.Icon is null);

            ImuhLb.Text = "Не выбран";
        }

        this.SelectedIdImuh = null;
    }

    async private void OwnerSelect_Pressed(object sender, EventArgs e)
    {
        OwnersList ownersList = new OwnersList(true);

        ownersList.Unloaded += (_,_) => 
        {
            if (ownersList.SelectedIdOwners != -1)
            {
                if (this.BindingContext is null)
                {
                    this.SelectedIdOwners = ownersList.SelectedIdOwners;
                    AsNameOwner.Text = DBModel.ResultRequest<string, Owners>(default, "FullName", ownersList.SelectedIdOwners);
                }
                else
                {
                    ((Models.Estate)this.BindingContext).IdOwner = ownersList.SelectedIdOwners;
                }
            }
        };

        await Navigation.PushAsync(new NavigationPage(ownersList));
    }

    async private void ImuhImg_Clicked(object sender, EventArgs e)
    {
        switch (((View.EstateType)TypePicker.SelectedItem).Id)
        {
            case 1:
                await SelecteCar();
                break;
        }
    }

    async private Task SelecteCar()
    {
        CarListPage imuhList = new CarListPage(true);

        imuhList.Unloaded += (_, _) =>
        {
            if (imuhList.SelectedIdCar != -1)
            {
                if (this.BindingContext is null)
                {
                    this.SelectedIdImuh = imuhList.SelectedIdCar;
                    ImuhLb.Text = DBModel.ResultRequest<string, Car>(default, "Make Model YearOfManufacture", imuhList.SelectedIdCar, ' ');

                    ImuhImg.Source = ImageSource.FromUri(CarInfo.LinkImageMakeCar(ImuhLb.Text.Split(' ')[0]));
                }
                else
                {
                    ((Models.Estate)this.BindingContext).IdCar = imuhList.SelectedIdCar;
                }
            }
        };

        await Navigation.PushAsync(new NavigationPage(imuhList));
    }
}
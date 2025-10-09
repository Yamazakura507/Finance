using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using Finance.Pages.WorkPage.Setting;
using System.Collections.ObjectModel;
using System.Text;

namespace Finance.Pages.WorkPage.Imuh;

public partial class EditorCars : ContentPage
{
    private readonly DateTime minGosDate = new DateTime(1991,1,1);
    private int? SelectedIdColor = null;

    Loading loading { get; set; }
    ObservableCollection<View.TypeCar> ViewTypeCar;

    public EditorCars()
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
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), ClassId = "saveButton", Text = "Добавить авто" };
            toolbarItemSave.Clicked += AddCar_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                ViewTypeCar = DBModel.GetCollectionModel<View.TypeCar>();

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    TypePicker.ItemsSource = ViewTypeCar;
                    TypePicker.SelectedIndex = this.BindingContext is null ? 0 : TypePicker.Items.IndexOf(((Car)this.BindingContext).TypeCar.Name);
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));

        AsPTSDateReg.MinimumDate = minGosDate;
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void AddCar_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.Estate));
            if (!CheckInsEstate()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                Dictionary<string, object> insParVal = new Dictionary<string, object>()
                {
                    { "Make", AsMake.Text },
                    { "Model", AsModel.Text },
                    { "VIN", AsVIN.Text },
                    { "IdTypeCar", ((View.TypeCar)TypePicker.SelectedItem).Id },
                    { "YearOfManufacture", AsYearOfManufacture.Text},
                    { "Mileage", String.IsNullOrEmpty(AsMileage.Text) ? null : AsMileage.Text},
                    { "EnginePower", AsEnginePower.Text.ConvertToMySqlDecimal() },
                    { "WorkingVolume", AsWorkingVolume.Text.ConvertToMySqlDecimal() },
                    { "IdColor", this.SelectedIdColor},
                    { "EngineModel", AsEngineModel.Text},
                    { "EngineNumber", AsEngineNumber.Text},
                    { "ChassisNumber", String.IsNullOrEmpty(AsChassisNumber.Text) ? null : AsChassisNumber.Text},
                    { "BodyNumber", String.IsNullOrEmpty(AsBodyNumber.Text) ? null : AsBodyNumber.Text},
                    { "Price", String.IsNullOrEmpty(AsPrice.Text) ? null : AsPrice.Text.ConvertToMySqlDecimal()},
                    { "StateNumber", AsStateNumber.Text},
                    { "PTSSeries", AsPTSSeries.Text},
                    { "PTSNumber", AsPTSNumber.Text},
                    { "PTSOrgIssuedDoc", AsPTSOrgIssuedDoc.Text},
                    { "PTSDateReg", AsPTSDateReg.Date.ConvertToMySqlDate()},
                    { "STSSeries", AsSTSSeries.Text},
                    { "STSNumber", AsSTSNumber.Text},
                    { "STSOrgIssuedDoc", AsSTSOrgIssuedDoc.Text},
                    { "STSDateReg", AsSTSDateReg.Date.ConvertToMySqlDate()},
                    { "IdUser", InfoAccount.IdUser}
                };

                int id = DBModel.InsertModel<Models.Car,int>(insParVal,"Id");

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
        StringBuilder builder = new StringBuilder();

        if (String.IsNullOrEmpty(AsMake.Text))
        {
            AsMakeProvider.WorkProvider(ProviderType.Alert, "Марка обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsModel.Text))
        {
            AsModelProvider.WorkProvider(ProviderType.Alert, "Модель обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsVIN.Text) || AsVIN.Text.Length < 17)
        {
            AsVINProvider.WorkProvider(ProviderType.Alert, "VIN номер обязательное поле для заполнения\nVIN номер это строка из 17 символов");

            result = false;
        }

        if (String.IsNullOrEmpty(AsYearOfManufacture.Text) || AsYearOfManufacture.Text.Length < 4)
        {
            AsYearOfManufactureProvider.WorkProvider(ProviderType.Alert, "Год производства обязательное поле для заполнения\nГод производства это строка из 4 цифр");

            result = false;
        }

        if (String.IsNullOrEmpty(this.AsEnginePower.Text))
        {
            builder.Append("Мощность ДВС обязательное поле для заполнения");
        }
        if (String.IsNullOrEmpty(this.AsWorkingVolume.Text))
        {
            builder.AppendLine("Объем ДВС обязательное поле для заполнения");
        }
        if (builder.Length > 0) 
        {
            AsEngineStateProvider.WorkProvider(ProviderType.Alert, builder.ToString());

            result = false;
            builder.Clear();
        }

        if (String.IsNullOrEmpty(this.AsEngineModel.Text))
        {
            builder.Append("Модель ДВС обязательное поле для заполнения");
        }
        if (String.IsNullOrEmpty(this.AsEngineNumber.Text))
        {
            builder.AppendLine("Номер ДВС обязательное поле для заполнения");
        }
        if (builder.Length > 0)
        {
            AsEngineUrNumsProvider.WorkProvider(ProviderType.Alert, builder.ToString());

            result = false;
            builder.Clear();
        }

        if (String.IsNullOrEmpty(AsStateNumber.Text) || AsStateNumber.Text.Length < 11)
        {
            AsStateNumberProvider.WorkProvider(ProviderType.Alert, "ЕГРН обязательное поле для заполнения\nЕГРН это строка от 8 до 9 символов");

            result = false;
        }

        if (String.IsNullOrEmpty(this.AsPTSSeries.Text) || this.AsPTSSeries.Text.Length < 5)
        {
            builder.Append("Серия ПТС обязательное поле для заполнения\nСерия ПТС это строка из 4 символов");
        }
        if (String.IsNullOrEmpty(this.AsPTSNumber.Text) || this.AsPTSNumber.Text.Length < 6)
        {
            builder.AppendLine("Номер ПТС обязательное поле для заполнения\nНомер ПТС это строка из 6 символов");
        }
        if (builder.Length > 0)
        {
            AsPTSNumProvider.WorkProvider(ProviderType.Alert, builder.ToString());

            result = false;
            builder.Clear();
        }

        if (String.IsNullOrEmpty(AsPTSOrgIssuedDoc.Text))
        {
            AsPTSOrgIssuedDocProvider.WorkProvider(ProviderType.Alert, "Организация выдавшая ПТС обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(this.AsSTSSeries.Text) || this.AsSTSSeries.Text.Length < 5)
        {
            builder.Append("Серия СТС обязательное поле для заполнения\nСерия СТС это строка из 4 символов");
        }
        if (String.IsNullOrEmpty(this.AsSTSNumber.Text) || this.AsSTSNumber.Text.Length < 6)
        {
            builder.AppendLine("Номер СТС обязательное поле для заполнения\nНомер СТС это строка из 6 символов");
        }
        if (builder.Length > 0)
        {
            AsSTSNumProvider.WorkProvider(ProviderType.Alert, builder.ToString());

            result = false;
            builder.Clear();
        }

        if (String.IsNullOrEmpty(AsSTSOrgIssuedDoc.Text))
        {
            AsSTSOrgIssuedDocProvider.WorkProvider(ProviderType.Alert, "Организация выдавшая СТС обязательное поле для заполнения");

            result = false;
        }

        if (this.SelectedIdColor is null)
        {
            AsColorProvider.WorkProvider(ProviderType.Alert, "Цвет это обязательная привязка для заполнения");

            result = false;
        }

        if (AsChassisNumber.Text != null && AsChassisNumber.Text.Length < 17) AsChassisNumber.Text = String.Empty;
        if (AsBodyNumber.Text != null && AsBodyNumber.Text.Length < 17) AsBodyNumber.Text = String.Empty;

        return result;
    }

    async private void TapGestureRecognizerColor(object sender, TappedEventArgs e)
    {
        ColorsPage colorsList = new ColorsPage(true);

        colorsList.Unloaded += (_, _) =>
        {
            if (colorsList.SelectedIdColor != -1)
            {
                if (this.BindingContext is null)
                {
                    this.SelectedIdColor = colorsList.SelectedIdColor;
                    View.LibColors color = DBModel.GetModel<View.LibColors>(colorsList.SelectedIdColor);
                    AsColorName.Text = color.Name;
                    AsColorBr.BackgroundColor = color.Colors;
                }
                else
                {
                    ((Models.Car)this.BindingContext).IdColor = colorsList.SelectedIdColor;
                }

                AsColorBr.IsVisible = true;
                ImgColor.IsVisible = false;
            }
        };

        await Navigation.PushAsync(new NavigationPage(colorsList));
    }

    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker typePicker = (Picker)sender;

        this.PickerSelector<View.TypeCar, Models.Car>(typePicker, "IdTypeCar");

        if (this.BindingContext is null)
        {
            View.TypeCar type = (View.TypeCar)typePicker.SelectedItem;

            switch (type.Id)
            {
                case 1:
                    ImgType.Source = CarInfo.PassangerCarIcon;
                    break;
                case 2:
                    ImgType.Source = CarInfo.TruckIcon;
                    break;
                case 3:
                    ImgType.Source = CarInfo.BusIcon;
                    break;
            }
            
            AsTypeProvider.Message = type.Description;
        }
    }

    private void Int_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsIntNumberEntry();

    private void Decimal_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).IsDecimalNumberEntry();

    private void BtCopyVin_Clicked(object sender, EventArgs e)
    {
        if (((ImageButton)sender).ClassId.Equals("CN"))
        {
            AsChassisNumber.Text = AsVIN.Text;
        }
        else
        {
            AsBodyNumber.Text = AsVIN.Text;
        }
    }

    private void StateNumGosCar_TextChanged(object sender, TextChangedEventArgs e) => ((Entry)sender).EntryCheckToBlockEGRNFormat();

    private void AsDigitNull_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        if (String.IsNullOrEmpty(entry.Text))
        {
            entry.Text = null;
        }
        else
        {
            Decimal_TextChanged(sender, e);
        }
    }

    private void AsUppder_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        if (entry.Text.Any(i => !Char.IsUpper(i)))
        {
            entry.Text = entry.Text.ToUpper();
        }
    }

    private void AsPTSDateReg_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (AsPTSDateReg != null && AsSTSDateReg != null)
        {
            AsSTSDateReg.MinimumDate = AsPTSDateReg.Date;
        }
    }
}
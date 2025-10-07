using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.Tabbed;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Imuh;

public partial class CarListPage : ContentPage
{
    private bool IsSelected = false;
    ObservableCollection<View.Car> ViewCar;
    Loading loading { get; set; }

    public int SelectedIdCar = -1;

    public CarListPage()
    {
        InitializeComponent();
        this.IsSelected = false;
    }

    public CarListPage(bool isSelected)
    {
        InitializeComponent();

        this.IsSelected = isSelected;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (IsSelected)
        {
            AddCar.IsVisible = false;

            #if !ANDROID && !IOS
                ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
                toolbarItem.Clicked += Back_Clicked;
                this.ToolbarItems.Add(toolbarItem);
            #endif
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {

                ViewCar = DBModel.GetCollectionModel<View.Car>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewCar is null || ViewCar.Count() == 0) throw new Exception("У вас отсутствует автомобили");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(debVSL, ViewCar));
                }
            }
            catch (Exception ex) 
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
    private void DeleteTask_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        View.Car car;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            car = sender.ContextConvert<View.Car>();

            DBModel.DeleteModel<Models.Car>(car.Id);

            MainThread.BeginInvokeOnMainThread(() => ViewCar.Remove(car));
        }));
    }
    async private void AddCar_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new EditorCars() { BindingContext = null }));
    async private void Car_Tapped(object sender, TappedEventArgs e)
    {
        View.Car car = sender.ContextConvert<View.Car>();

        if (!IsSelected)
        {
            await Navigation.PushAsync(new NavigationPage(new EditorCars() { BindingContext = DBModel.GetModel<Models.Car>(car.Id) }));
        }
        else
        {
            SelectedIdCar = car.Id;
            this.BackButtonInNavClick();
        }
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();
}
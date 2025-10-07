using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.Tabbed;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Imuh;

public partial class EstateListPage : ContentPage
{
    ObservableCollection<View.Estate> ViewEstate;
    Loading loading { get; set; }

    public EstateListPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {

                ViewEstate = DBModel.GetCollectionModel<View.Estate>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewEstate is null || ViewEstate.Count() == 0) throw new Exception("У вас отсутствует собственость");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(debVSL, ViewEstate));
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

        View.Estate estaet;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            estaet = sender.ContextConvert<View.Estate>();

            DBModel.GetModel<Models.Estate>(estaet.Id).DeleteModel<Models.Estate>();

            MainThread.BeginInvokeOnMainThread(() => ViewEstate.Remove(estaet));
        }));
    }
    async private void AddEstate_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new EditorEstate() { BindingContext = null }));
    async private void Estate_Tapped(object sender, TappedEventArgs e)
    {
        View.Estate estate = sender.ContextConvert<View.Estate>();

        await Navigation.PushAsync(new NavigationPage(new EditorEstate() { BindingContext = DBModel.GetModel<Models.Estate>(estate.Id) }));
    }

    private void EditStatusEstateMenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                View.Estate estate;

                estate = sender.ContextConvert<View.Estate>();

                int id = await this.SheetPicker<View.EstateStatus>("Выбор статуса собственности", ErrProvider);

                if (id == 0) return;

                DBModel.GetModel<Models.Estate>(estate.Id).IdStatusEstate = id;
                estate.IdStatusEstate = id;
            }
            catch (Exception ex)
            {
                ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
            }
        }));
    }
}
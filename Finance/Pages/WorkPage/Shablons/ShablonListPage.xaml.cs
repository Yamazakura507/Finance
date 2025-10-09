using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.WorkPage.Shablons.Enums;
using System.Collections.ObjectModel;
using ShablonDoc = Finance.Models.ShablonDoc;

namespace Finance.Pages.WorkPage.Shablons;

public partial class ShablonListPage : ContentPage
{
    ObservableCollection<View.ShablonDoc> ViewShablon;
    Loading loading { get; set; }

    public ShablonListPage()
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

                ViewShablon = DBModel.GetCollectionModel<View.ShablonDoc>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewShablon is null || ViewShablon.Count() == 0) throw new Exception("У вас отсутствуют шаблоны");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(debVSL, ViewShablon));
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

        View.ShablonDoc shablon;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            shablon = sender.ContextConvert<View.ShablonDoc>();

            DBModel.DeleteModel<ShablonDoc>(shablon.Id);

            MainThread.BeginInvokeOnMainThread(() => ViewShablon.Remove(shablon));
        }));
    }
    async private void AddShablon_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new ShablonEdit() { BindingContext = null }));
    async private void Shablon_Tapped(object sender, TappedEventArgs e)
    {
        View.ShablonDoc shablon = sender.ContextConvert<View.ShablonDoc>();

        await Navigation.PushAsync(new NavigationPage(new ShablonEdit() { BindingContext = DBModel.GetModel<ShablonDoc>(shablon.Id) }));
    }
    async private void PntingShablonDocMenuFlyoutItem_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(
                                                                                                new NavigationPage(
                                                                                                    new ShablonPrinting() 
                                                                                                    { 
                                                                                                        BindingContext = DBModel.GetModel<ShablonDoc>(sender.ContextConvert<View.ShablonDoc>().Id), 
                                                                                                        TypeDoc = TypeDoc.DocDkpAvto 
                                                                                                    }
                                                                                                ));

}
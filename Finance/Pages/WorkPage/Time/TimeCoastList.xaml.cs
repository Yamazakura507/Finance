using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Time;

public partial class TimeCoastList : ContentPage
{
    Loading loading { get; set; }
    ObservableCollection<View.TimeCoast> ViewTime;

    public TimeCoastList()
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
                ViewTime = DBModel.GetCollectionModel<View.TimeCoast>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewTime is null || ViewTime.Count() == 0) throw new Exception("������ ������� ����");
                else MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(timeVSL, ViewTime));
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error,ex.Message));
            }
        }));

    }

    private void DeleteButton_Pressed(object sender, EventArgs e)
    {
        var support = ((View.TimeCoast)((ContentView)((Grid)((ImageButton)sender).Parent).Parent).BindingContext);
        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.TimeCoast>(support.Id).DeleteModel<Models.TimeCoast>()));

        ViewTime.Remove(support);
    }

    async private void Time_Tapped(object sender, TappedEventArgs e)
    {
        var view = (View.TimeCoast)((ContentView)sender).BindingContext;
        var model = DBModel.GetModel<Models.TimeCoast>(view.Id);

        await Navigation.PushAsync(new NavigationPage(new TimeCoastInfo() { BindingContext = model }));
    }

    async private void btAddTime_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new TimeCoastInfo()));
}
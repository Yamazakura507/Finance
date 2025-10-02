using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using MapLib.MapModel;
using System.Collections.ObjectModel;
using System.Text;

namespace Finance.Pages.WorkPage.Setting;

public partial class AddressPage : ContentPage
{
    Loading loading { get; set; }
    ObservableCollection<View.LibAddress> ViewAddress;

    public AddressPage()
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
                ViewAddress = DBModel.GetCollectionModel<View.LibAddress>(default,default,default,new Dictionary<string, OrderType>() { { "Id", OrderType.Desc } });

                if (ViewAddress is null || ViewAddress.Count() == 0) return;
                else MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(supportVSL, ViewAddress));
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => await this.Messege(ex.Message, ProviderType.Error));
            }
        }));

        LottieAnimation.HideAnimationStop(btlSendSupport);
        LottieAnimation.HideAnimationStop(mapSendSupport);
    }

    private void SendSupport_Tapped(object sender, TappedEventArgs e)
    {
        LottieAnimation.HideAnimationStart(btlSendSupport, 2400);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async() =>
        {
            await InsertAddress(NewSupport.Text);
        }));
    }

    private void DeleteButton_Pressed(object sender, EventArgs e)
    {
        View.LibAddress address = sender.ContextConvert<View.LibAddress>();
        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                DBModel.GetModel<Models.LibAddress>(address.Id).DeleteModel<Models.LibAddress>();
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => await this.Messege(ex.Message, ProviderType.Error));
            }
            
        }));

        ViewAddress.Remove(address);
    }

    async private void SupportPress_Tapped(object sender, TappedEventArgs e) => await EditAddress(sender);

    async private Task EditAddress(object sender, MapObject map = null)
    {
        try
        {
            View.LibAddress address = sender.ContextConvert<View.LibAddress>();
            Models.LibAddress modelAddress = DBModel.GetModel<Models.LibAddress>(address.Id);

            if (map is null)
            {
                string newAddress = await this.InputMessege("Редактирование адреса", address.Address);

                if (!String.IsNullOrEmpty(newAddress))
                {
                    modelAddress.Address = newAddress;
                    modelAddress.AddressType = null;
                    modelAddress.Latitude = null;
                    modelAddress.Lontitude = null;
                    address.Address = newAddress;
                    address.AddressType = null;
                }
            }
            else
            {
                address = ViewAddress.First(i => i.Id == address.Id);

                modelAddress.Latitude = map.Latitude;
                modelAddress.Lontitude = map.Lontitude;
                modelAddress.AddressType = map.AddressType;
                address.AddressType = map.AddressType;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (await this.QuestionMessege("Изменить адрес?", "НЕТ", "ДА"))
                    {
                        modelAddress.Address = map.DisplayName;
                        address.Address = map.DisplayName;
                    }
                });
            }
        }
        catch (Exception ex)
        {
            this.Messege(ex.Message, ProviderType.Info);
        }
    }

    async private Task InsertAddress(string address = null, MapObject map = null)
    {
        try
        {
            StringBuilder builder = new StringBuilder(); 
            if (String.IsNullOrEmpty(address) && map is null) throw new Exception();

            using (var ms = new Mysql())
            {
                string valdef = $") VALUES ('{address ?? map.DisplayName}','{InfoAccount.IdUser}'";
                builder.Append("INSERT INTO `LibAddress` (`Address`, `IdUser`");

                if (map is null) builder.Append(valdef);
                else
                {
                    builder.Append(", `Latitude`, `Lontitude`, `AddressType`");
                    builder.Append(valdef);
                    builder.Append($",'{map.Latitude}','{map.Lontitude}','{map.AddressType}'");
                } 

                builder.Append(");SELECT LAST_INSERT_ID();");
                int idAddress = DBModel.ResultRequest<int>(builder.ToString());
                View.LibAddress vAddress = DBModel.GetModel<View.LibAddress>(idAddress);

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (map is null)
                    {
                        ViewAddress.Add(vAddress);
                    }
                    
                    NewSupport.Text = String.Empty;
                });
            }
        }
        catch (Exception ex) { await MainThread.InvokeOnMainThreadAsync(() => this.Messege("Не удалось добавить адрес", ProviderType.Error)); }
    }

    async private void MenuFlyoutItem_Clicked_IsEdit(object sender, EventArgs e) => await EditAddress(sender);

    async private void MenuFlyoutItem_Clicked_Map(object sender, EventArgs e)
    {
        View.LibAddress address = sender.ContextConvert<View.LibAddress>();
        MapPage map = new MapPage(address);
        map.BindingContext = sender;
        map.NavigatedFrom += Map_NavigatedFrom;

        await Navigation.PushAsync(new NavigationPage(map));
    }

    async private void TapGestureRecognizer_Tapped_Map(object sender, TappedEventArgs e)
    {
        LottieAnimation.HideAnimationStart(mapSendSupport, 2000);

        MapPage map = new MapPage();
        map.NavigatedFrom += Map_NavigatedFrom;

        await Navigation.PushAsync(new NavigationPage(map));
    }

    private void Map_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
    {
        MapPage map = (MapPage)sender;

        if (map.MapObject != null)
        {
            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                if (map.IsUpdate)
                {
                    await EditAddress(map.BindingContext, map.MapObject);
                }
                else
                {
                    await InsertAddress(default, map.MapObject);
                }
            }));
        }
    }
}
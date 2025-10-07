using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Setting;

public partial class ColorsPage : ContentPage
{
    Loading loading { get; set; }
    ObservableCollection<View.LibColors> ViewColors;

    private bool IsSelected = false;
    public int SelectedIdColor = -1;

    public ColorsPage()
    {
        InitializeComponent();

        this.IsSelected = false;
    }

    public ColorsPage(bool isSelected)
    {
        InitializeComponent();

        this.IsSelected = isSelected;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (IsSelected)
        {
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
                ViewColors = DBModel.GetCollectionModel<View.LibColors>(default,default,default,new Dictionary<string, OrderType>() { { "Id", OrderType.Desc } });

                if (ViewColors is null || ViewColors.Count() == 0) return;
                else MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(supportVSL, ViewColors));
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => await this.Messege(ex.Message, ProviderType.Error));
            }
        }));

        LottieAnimation.HideAnimationStop(btlSendSupport);
        LottieAnimation.HideAnimationStop(btlWebColor);
    }

    private void SendSupport_Tapped(object sender, TappedEventArgs e)
    {
        LottieAnimation.HideAnimationStart(btlSendSupport, 2400);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async() =>
        {
            await InsertColor(colorPicker.PickedColor);
        }));
    }

    private void DeleteButton_Pressed(object sender, EventArgs e)
    {
        View.LibColors color = sender.ContextConvert<View.LibColors>();
        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                DBModel.GetModel<Models.LibColors>(color.Id).DeleteModel<Models.LibColors>();
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => await this.Messege(ex.Message, ProviderType.Error));
            }
            
        }));

        ViewColors.Remove(color);
    }

    async private void ColorPress_Tapped(object sender, TappedEventArgs e)
    {
        if (!IsSelected)
        {
            await EditColor(sender);
        }
        else
        {
            View.LibColors color = sender.ContextConvert<View.LibColors>();

            SelectedIdColor = color.Id;
            this.BackButtonInNavClick();
        }
    }

    async private Task EditColor(object sender, bool isColor = false)
    {
        try
        {
            View.LibColors color = sender.ContextConvert<View.LibColors>();
            Models.LibColors modelColor = DBModel.GetModel<Models.LibColors>(color.Id);

            if (!isColor)
            {
                string newColor = await this.InputMessege("Редактирование цвета", color.Name);

                if (!String.IsNullOrEmpty(newColor))
                {
                    modelColor.Name = newColor;
                    color.Name = newColor;
                }
            }
            else
            {
                string hex = colorPicker.PickedColor.ToHex();

                modelColor.MyColor = hex;
                color.MyColor = hex;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (await this.QuestionMessege("Изменить наименование?", "НЕТ", "ДА"))
                    {
                        string name = (await ColorLib.Color.GetColorIsHex(hex)).Name.Value;

                        modelColor.Name = name;
                        color.Name = name;
                    }
                });
            }
        }
        catch (Exception ex)
        {
            this.Messege(ex.Message, ProviderType.Info);
        }
    }

    async private Task InsertColor(Color colors)
    {
        try
        {
            string hex = colorPicker.PickedColor.ToHex();
            string color = (await ColorLib.Color.GetColorIsHex(hex)).Name.Value;

            if (String.IsNullOrEmpty(color) || String.IsNullOrEmpty(hex)) throw new Exception();

            using (var ms = new Mysql())
            {
                int idColor = DBModel.ResultRequest<int>($"INSERT INTO `LibColors` (`Name`, `IdUser`, `MyColor`) VALUES ('{color}','{InfoAccount.IdUser}','{hex}');SELECT LAST_INSERT_ID();");
                View.LibColors vColor = DBModel.GetModel<View.LibColors>(idColor);

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    ViewColors.Add(vColor);
                });
            }
        }
        catch (Exception ex) { await MainThread.InvokeOnMainThreadAsync(() => this.Messege("Не удалось добавить адрес", ProviderType.Error)); }
    }

    async private void MenuFlyoutItem_Clicked_IsEdit(object sender, EventArgs e) => await EditColor(sender);

    async private void MenuFlyoutItemСolorEdit_Clicked(object sender, EventArgs e) => await EditColor(sender, true);

    async private void TapGestureRecognizerWebColor_Tapped(object sender, TappedEventArgs e)
    {
        LottieAnimation.HideAnimationStart(btlSendSupport, 2000);

        await Navigation.PushAsync(new NavigationPage(new ColorWebPage(colorPicker.PickedColor.ToHex())));
    }

    async private void MenuFlyoutItemColorInformation_Clicked(object sender, EventArgs e)
    {
        View.LibColors color = sender.ContextConvert<View.LibColors>();

        await Navigation.PushAsync(new NavigationPage(new ColorWebPage(color.MyColor)));
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();
}
using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using MapLib.MapModel;
using System.Collections.ObjectModel;
using System.Text;

namespace Finance.Pages.WorkPage.Setting;

public partial class LinksPage : ContentPage
{
    private bool IsSelected = false;

    Loading loading { get; set; }
    ObservableCollection<View.LibLink> ViewLinks;

    public int SelectedIdAddress = -1;

    public LinksPage()
    {
        InitializeComponent();

        this.IsSelected = false;
    }

    public LinksPage(bool isSelected)
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
                ViewLinks = DBModel.GetCollectionModel<View.LibLink>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } }, default,default,new Dictionary<string, OrderType>() { { "IsMainMenu", OrderType.Desc }, { "Id", OrderType.Desc } });

                if (ViewLinks is null || ViewLinks.Count() == 0) return;
                else MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(supportVSL, ViewLinks));
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => await this.Messege(ex.Message, ProviderType.Error));
            }
        }));

        LottieAnimation.HideAnimationStop(btlSendSupport);
        LottieAnimation.HideAnimationStop(linkSendSupport);
    }

    private void SendSupport_Tapped(object sender, TappedEventArgs e)
    {
        LottieAnimation.HideAnimationStart(btlSendSupport, 2400);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async() =>
        {
            await InsertLink(new Uri(NewSupport.Text));
        }));
    }

    private void DeleteButton_Pressed(object sender, EventArgs e)
    {
        View.LibLink link = sender.ContextConvert<View.LibLink>();
        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                DBModel.GetModel<Models.LibLink>(link.Id).DeleteModel<Models.LibLink>();
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () => await this.Messege(ex.Message, ProviderType.Error));
            }
            
        }));

        ViewLinks.Remove(link);
    }

    async private void SupportPress_Tapped(object sender, TappedEventArgs e)
    {
        if (!IsSelected)
        {
            await EditLink(sender);
        }
        else
        {
            View.LibLink link = sender.ContextConvert<View.LibLink>();

            SelectedIdAddress = link.Id;
            this.BackButtonInNavClick();
        }
    }

    async private Task EditLink(object sender, string linkRel = null)
    {
        try
        {
            View.LibLink link = sender.ContextConvert<View.LibLink>();
            Models.LibLink modelLink = DBModel.GetModel<Models.LibLink>(link.Id);

            if (String.IsNullOrEmpty(linkRel))
            {
                string newName = await this.InputMessege("Редактирование наименования ссылки", link.Name);

                if (!String.IsNullOrEmpty(newName))
                {
                    modelLink.Name = newName;
                    link.Name = newName;
                }
            }
            else
            {
                link = ViewLinks.First(i => i.Id == link.Id);

                string[] namesLink = new Uri(linkRel).Host.Split('.');
                string name = namesLink.Length > 2 ? namesLink[1] : namesLink[0];

                modelLink.Name = name.ToUpper();
                modelLink.Link = linkRel;
                link.Name = name.ToUpper();
                link.Link = linkRel;
            }
        }
        catch (Exception ex)
        {
            this.Messege(ex.Message, ProviderType.Info);
        }
    }

    async private Task InsertLink(Uri linkRel = null)
    {
        try
        { 
            if (linkRel is null) throw new Exception();

            string[] namesLink = linkRel.Host.Split('.');
            string name = namesLink.Length > 2 ? namesLink[1] : namesLink[0];

            Dictionary<string, object> insParVal = new Dictionary<string, object>()
            {
                { "Name", name.ToUpper() },
                { "Link", linkRel.AbsoluteUri },
                { "IsMainMenu", false },
                { "IdUser", InfoAccount.IdUser}
            };

            int idLink = DBModel.InsertModel<Models.LibLink, int>(insParVal, "Id");
            View.LibLink vLink = DBModel.GetModel<View.LibLink>(idLink);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                ViewLinks.Add(vLink);

                NewSupport.Text = String.Empty;
            });
        }
        catch (Exception ex) { await MainThread.InvokeOnMainThreadAsync(() => this.Messege("Не удалось добавить адрес", ProviderType.Error)); }
    }

    async private void MenuFlyoutItem_Clicked_IsEdit(object sender, EventArgs e) => await EditLink(sender);

    async private void MenuFlyoutItem_Clicked_Brouser(object sender, EventArgs e)
    {
        View.LibLink link = sender.ContextConvert<View.LibLink>();
        BrouserPage linkPage = new BrouserPage(link.Link);
        linkPage.BindingContext = sender;
        linkPage.NavigatedFrom += Brouser_NavigatedFrom;

        await Navigation.PushAsync(new NavigationPage(linkPage));
    }

    async private void TapGestureRecognizer_Tapped_Link(object sender, TappedEventArgs e)
    {
        LottieAnimation.HideAnimationStart(linkSendSupport, 2000);

        BrouserPage linkPage = new BrouserPage();
        linkPage.NavigatedFrom += Brouser_NavigatedFrom;

        await Navigation.PushAsync(new NavigationPage(linkPage));
    }

    private void Brouser_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
    {
        BrouserPage linkPage = (BrouserPage)sender;

        if (linkPage.NewLink != null)
        {
            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                if (linkPage.IsUpdate)
                {
                    await EditLink(linkPage.BindingContext, linkPage.NewLink.AbsoluteUri);
                }
                else
                {
                    await InsertLink(linkPage.NewLink);
                }
            }));
        }
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void MenuFlyoutItem_Clicked_IsMain(object sender, EventArgs e)
    {
        View.LibLink link = sender.ContextConvert<View.LibLink>();
        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            bool isMain = ((MenuFlyoutItem)sender).Text.Contains("✔");
            DBModel.GetModel<Models.LibLink>(link.Id).IsMainMenu = isMain;
            link.IsMainMenu = isMain;
        }));
    }
}
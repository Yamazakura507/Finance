using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Imuh;

public partial class OwnersList : ContentPage
{
    private bool IsSelected = false;
    ObservableCollection<View.Owners> ViewOwners;

    public int SelectedIdOwners = -1;

    Loading loading { get; set; }

    public OwnersList()
    {
        InitializeComponent();

        this.IsSelected = false;
    }

    public OwnersList(bool isSelected)
    {
        InitializeComponent();

        this.IsSelected = isSelected;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (IsSelected)
        {
            AddOwner.IsVisible = false;

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

                ViewOwners = DBModel.GetCollectionModel<View.Owners>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } });

                if (ViewOwners is null || ViewOwners.Count() == 0) throw new Exception("У вас отсутствуют собственики");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(debVSL, ViewOwners));
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

        View.Owners owners;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            owners = sender.ContextConvert<View.Owners>();

            DBModel.GetModel<Models.Owners>(owners.Id).DeleteModel<Models.Owners>();

            MainThread.BeginInvokeOnMainThread(() => ViewOwners.Remove(owners));
        }));
    }
    async private void AddOwners_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new EditorOwners() { BindingContext = null }));
    async private void Owner_Tapped(object sender, TappedEventArgs e)
    {
        View.Owners owners = sender.ContextConvert<View.Owners>();

        if (!IsSelected)
        {
            await Navigation.PushAsync(new NavigationPage(new EditorOwners() { BindingContext = DBModel.GetModel<Models.Owners>(owners.Id) }));
        }
        else
        {
            SelectedIdOwners = owners.Id;
            this.BackButtonInNavClick();
        }
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();
}
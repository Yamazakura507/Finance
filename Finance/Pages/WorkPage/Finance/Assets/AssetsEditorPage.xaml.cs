using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using SkiaSharp.Extended.UI.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace Finance.Pages.WorkPage.Finance.Assets;

public partial class AssetsEditorPage : ContentPage
{
    public int? IdDate { get; set; } = null;

    ObservableCollection<View.AssetsGroup> ViewAssetsGroup;
    Loading loading { get; set; }

    public AssetsEditorPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        this.ToolbarItems.Clear();

        #if ANDROID || IOS
            colSPP.Width = GridLength.Auto;
            lbCb.Margin = new Thickness(0, 0, 0, 0);
        #else
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
            colSPP.Width = 200;
        #endif

        if (this.BindingContext is null)
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить финансовый объект" };
            toolbarItemSave.Clicked += AddAssets_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }
        else
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.delete_cash), Text = "Удалить финансовый объект" };
            toolbarItemSave.Clicked += DeleteAssets_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                var flowTypes = DBModel.GetCollectionModel<FlowType>();
                var transferStatus = DBModel.GetCollectionModel<TransferStatus>();

                string sql = String.Format(@"SELECT * FROM `AssetsGroup` ag WHERE ag.`IdUser` = '{0}' OR ag.`IdUser` is NULL", InfoAccount.IdUser);
                ViewAssetsGroup = DBModel.GetCollectionModel<View.AssetsGroup>(sql);

                await MainThread.InvokeOnMainThreadAsync(() => 
                {
                    FlowTypePicker.ItemsSource = flowTypes;
                    TransferPicker.ItemsSource = transferStatus;
                    FlowTypePicker.SelectedIndex = 0;
                    TransferPicker.SelectedIndex = 0;
                    BindableLayout.SetItemsSource(asGrVSL, ViewAssetsGroup);
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }

    private void Back_Clicked(object? sender, EventArgs e)
    {
        if (this.Parent.Parent.GetType() != typeof(NavigationPage)) ((Page)this.Parent.Parent).BackButtonInNavClick();
        else this.BackButtonInNavClick();
    }

    private void AddAssets_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.Assets));
            if (!CheckInsAsset()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                string strGroupAsset = String.Join(",", asGrVSL.Where(i => ((CheckBox)((Grid)((Border)((Grid)i).Children[0]).Content).Children[2]).IsChecked).Select(i => ((View.AssetsGroup)((Grid)i).BindingContext).Id));

                using (var ms = new Mysql())
                    await ms.ExecSqlAsync($"SELECT ins_upd_asset_or_pasive('{IdDate}','{((FlowType)FlowTypePicker.SelectedItem).Id}','{DBModel.ConvertToMySqlDecimal(AsSum.Text)}','{AsName.Text}',@Use,@IsStability,@IsAsset,-1,'{strGroupAsset}',@StatusTransfer,'{InfoAccount.IdUser}')", new MySqlParameter[]
                    {
                        new MySqlParameter("@Use", String.IsNullOrEmpty(AsCommit.Text) ? DBNull.Value : AsCommit.Text),
                        new MySqlParameter("@IsStability", cbStability.IsChecked),
                        new MySqlParameter("@IsAsset", cbIsAsset.IsChecked),
                        new MySqlParameter("@StatusTransfer", cbIsAutoTransfer.IsChecked ? DBNull.Value : ((TransferStatus)TransferPicker.SelectedItem).Id)
                    });
            }));

            ((Page)this.Parent.Parent).BackButtonInNavClick();
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void DeleteAssets_Clicked(object? sender, EventArgs e)
    {
        try
        {
            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                ((Models.Assets)this.BindingContext).DeleteModel<Models.Assets>();
            }));

            ((Page)this.Parent.Parent).BackButtonInNavClick();
        }
        catch (Exception ex) { }
    }

    private bool CheckInsAsset()
    {
        bool result = true;

        foreach (Grid item in asGrVSL)
        {
            if (((CheckBox)((Grid)((Border)item.Children[0]).Content).Children[2]).IsChecked)
            {
                result = true;
                break;
            }

            result = false;
        }

        if (!result)
        {
            AsGrProvider.WorkProvider(ProviderType.Alert, "Выберите хотябы одну группу");
        }

        if (IdDate is null) 
        {
            ErrProvider.WorkProvider(ProviderType.Alert, "Выберите дату заполнения");
            result = false;
        }

        if (String.IsNullOrEmpty(AsName.Text))
        {
            AsNameProvider.WorkProvider(ProviderType.Alert, "Наименование обязательное поле для заполнения");

            result = false;
        }

        if (String.IsNullOrEmpty(AsSum.Text) || Convert.ToDecimal(AsSum.Text.Replace('.', ',')) <= 0)
        {
            AsSumProvider.WorkProvider(ProviderType.Alert, "Сумма должна превышать нулевое значение");

            result = false;
        }

        return result;
    }

    private void lbcbIsStability_Tapped(object sender, TappedEventArgs e) => cbStability.IsChecked = !cbStability.IsChecked;

    private void lbcbIsAsset_Tapped(object sender, TappedEventArgs e) => cbIsAsset.IsChecked = !cbIsAsset.IsChecked;

    private void sum_TextChanged(object sender, TextChangedEventArgs e) => TextBoxRestrictions.IsDecimalNumberEntry((Entry)sender);

    async private void AddGroupAssetsImageButton_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new GroupAddPage()));

    private void PressAssetGroup_Tapped(object sender, TappedEventArgs e)
    {
        var grid = (Grid)((SKLottieView)sender).Parent;

        LottieAnimation.HideAnimationStart((SKLottieView)sender, 1730);

        var check = (CheckBox)grid.Children[2];
        check.IsChecked = !check.IsChecked;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) => cbIsAutoTransfer.IsChecked = !cbIsAutoTransfer.IsChecked;

    private void cbIsAutoTransfer_CheckedChanged(object sender, CheckedChangedEventArgs e) => TransferPicker.IsEnabled = !cbIsAutoTransfer.IsChecked;
}
using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;

namespace Finance.Pages.WorkPage.Finance.Assets;

public partial class GroupAddPage : ContentPage
{
    private bool isImg = false;
    Loading loading { get; set; }

    public GroupAddPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
            colSPP.Width = GridLength.Auto;
        #else
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
            colSPP.Width = 200;
        #endif

        if (this.BindingContext is null)
        {
            #if ANDROID || IOS
                if (this.Parent.Parent.GetType() == typeof(AssetGroupEditorPage))
                {
                    GroupNavBt.IsVisible = true;
                }
            #endif

            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить финансовую группу" };
            toolbarItemSave.Clicked += AddAssetsGroup_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);

            addBtImg.Source = ConverFiles.ToImageConvert(Properties.Resources.add_file);
        }
        else 
        {
            #if ANDROID || IOS
                GroupNavBt.IsVisible = true;
            #endif

            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.delete_cash), Text = "Удалить финансовую группу" };
            toolbarItemSave.Clicked += DeleteAssetsGroup_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }
    }

    private void Back_Clicked(object? sender, EventArgs e)
    {
        if (this.Parent.Parent.GetType() != typeof(NavigationPage)) ((Page)this.Parent.Parent).BackButtonInNavClick();
        else this.BackButtonInNavClick();
    }

    private void AddAssetsGroup_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.AssetsGroup));
            if (!CheckInsGroup()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                    await ms.ExecSqlAsync($"INSERT INTO `AssetsGroup`(`Name`, `Description`, `Icon`, `IdUser`) VALUES ('{GrName.Text}',@Commit,@Icon,'{InfoAccount.IdUser}')", new MySqlParameter[] 
                    {
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(GrCommit.Text) ? DBNull.Value : GrCommit.Text),
                        new MySqlParameter("@Icon", isImg ? await ConverFiles.ConvertImageSourceToBytesAsync(addBtImg.Source) : DBNull.Value)
                    });
            }));

            this.BackButtonInNavClick();
        }
        catch (Exception ex)
        {
            GrNameProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void DeleteAssetsGroup_Clicked(object? sender, EventArgs e) 
    {
        try
        {
            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                ((Models.AssetsGroup)this.BindingContext).DeleteModel<Models.AssetsGroup>();
            }));

            ((Page)this.Parent.Parent).BackButtonInNavClick();
        }
        catch (Exception ex) { }
    }

    private bool CheckInsGroup()
    {
        if (String.IsNullOrEmpty(GrName.Text))
        {
            GrNameProvider.WorkProvider(ProviderType.Alert,"Наименование обязательное поле для заполнения");

            return false;
        } 

        return true;
    }

    async private void addBtImg_Pressed(object sender, EventArgs e)
    {
        var file = await FilePicker.PickAsync(PickOptions.Images);

        if (file != null)
        {
            addBtImg.Source = ConverFiles.ToImageConvert(ConverFiles.ToByteConvert(file.FullPath));
            isImg = true;

            if (this.BindingContext != null) ((Models.AssetsGroup)this.BindingContext).Icon = await ConverFiles.ConvertImageSourceToBytesAsync(addBtImg.Source);
        }
    }

    private void AndroidVisualGroupBt_Pressed(object sender, EventArgs e) => ((AssetGroupEditorPage)this.Parent.Parent).IsPresented = true;
}
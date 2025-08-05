using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance.Futures;

public partial class ScalpUpInfo : ContentPage
{
    View.Scalping ViewSc;

    Loading loading { get; set; }

    public ScalpUpInfo()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    { 
        #if ANDROID || IOS
            colSPP.Width = GridLength.Auto;
        #else
            colSPP.Width = 200;
        #endif

        if (this.BindingContext is null)
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить массив" };
            toolbarItemSave.Clicked += AddPlan_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {

                ViewSc = DBModel.GetModel<View.Scalping>(((Models.Scalping)this.BindingContext).Id);

                if (ViewSc is null) throw new Exception("У вас отсутствуют скальп хранилища");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => vScVSL.BindingContext = ViewSc);
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }

    private void AddPlan_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.Scalping));
            if (!CheckInsScalp()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_scalping('-1','{InfoAccount.IdUser}','{AsName.Text}',@Desc)", new[]
                    {
                        new MySqlParameter("@Desc", String.IsNullOrEmpty(AsCommit.Text) ? DBNull.Value : AsCommit.Text)
                    });

                await MainThread.InvokeOnMainThreadAsync(() => ((Page)this.Parent).BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private bool CheckInsScalp()
    {
        bool result = true;

        if (String.IsNullOrEmpty(AsName.Text))
        {
            AsNameProvider.WorkProvider(ProviderType.Alert, "Наименование обязательное поле для заполнения");

            result = false;
        }

        return result;
    }
}
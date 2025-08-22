using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;

namespace Finance.Pages.WorkPage.Planing;

public partial class PlanInfo : ContentPage
{
    Loading loading { get; set; }

    public PlanInfo()
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
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить план" };
            toolbarItemSave.Clicked += AddPlan_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }
    }

    private void AddPlan_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.PlanningJournal));
            if (!CheckInsPlan()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                    ms.ExecSql($"INSERT INTO `PlanningJournal`(`Name`, `Description`, `TargetAmount`, `IdUser`) VALUES ('{AsName.Text}',@Commit,@Sum,'{InfoAccount.IdUser}')", new[]
                    {
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(AsCommit.Text) ? DBNull.Value : AsCommit.Text),
                        new MySqlParameter("@Sum", AsSum.Text)
                    });

                await MainThread.InvokeOnMainThreadAsync(() => ((Page)this.Parent).BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void sum_TextChanged(object sender, TextChangedEventArgs e) => TextBoxRestrictions.IsDecimalNumberEntry((Entry)sender);

    private bool CheckInsPlan()
    {
        bool result = true;

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
}
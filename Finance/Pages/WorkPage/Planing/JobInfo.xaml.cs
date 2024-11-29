using CommunityToolkit.Maui.Views;
using MySqlConnector;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;

namespace Finance.Pages.WorkPage.Planing;

public partial class JobInfo : ContentPage
{
    public int IdTask { get; set; }

    Loading loading { get; set; }

    public JobInfo()
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
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить работу" };
            toolbarItemSave.Clicked += AddTask_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }
    }

    private void AddTask_Clicked(object? sender, EventArgs e)
    {
        try
        {
            DBModel.CheckPolice(false, typeof(Models.PlanningJournal));
            if (!CheckInsJobs()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                using (var ms = new Mysql())
                {
                    ms.ExecSql(@$"INSERT INTO `Jobs`(`Name`, `Commit`, `SumResult`, `IsDone`) VALUES ('{AsName.Text}',@Commit,@Sum,@IsDone);
                                  INSERT INTO `TaskJobs`(`IdTask`, `IdJob`) VALUES ('{IdTask}',LAST_INSERT_ID())", new[]
                    {
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(AsCommit.Text) ? DBNull.Value : AsCommit.Text),
                        new MySqlParameter("@Sum", AsSum.Text),
                        new MySqlParameter("@IsDone", cbDone.IsChecked)
                    });
                }

                await MainThread.InvokeOnMainThreadAsync(() => this.BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void sum_TextChanged(object sender, TextChangedEventArgs e) => TextBoxRestrictions.IsDecimalNumberEntry((Entry)sender);
    private void LabelCbOnTapped(object sender, TappedEventArgs e) => cbDone.IsChecked = !cbDone.IsChecked;
    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private bool CheckInsJobs()
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
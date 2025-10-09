using CommunityToolkit.Maui.Views;
using ShablonDoc = Finance.Models.ShablonDoc;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;

namespace Finance.Pages.WorkPage.Shablons;

public partial class ShablonEdit : ContentPage
{
    public byte[]? FileData { get; set; } = null;

    Loading loading { get; set; }

    public ShablonEdit()
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
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Добавить шаблон" };
            toolbarItemSave.Clicked += AddShablon_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }
    }

    private void AddShablon_Clicked(object? sender, EventArgs e)
    {
        try
        {
            if (!CheckIns()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                Dictionary<string, object> insParVal = new Dictionary<string, object>()
                {
                    { "Name", AsName.Text },
                    { "Description", String.IsNullOrEmpty(AsCommit.Text) ? DBNull.Value : AsCommit.Text },
                    { "Doc", FileData},
                    { "IdUser", InfoAccount.IdUser}
                };

                DBModel.InsertModel<ShablonDoc, int>(insParVal, "Id");

                await MainThread.InvokeOnMainThreadAsync(() => this.BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private bool CheckIns()
    {
        bool result = true;

        if (String.IsNullOrEmpty(AsName.Text))
        {
            AsNameProvider.WorkProvider(ProviderType.Alert, "Наименование обязательное поле для заполнения");

            result = false;
        }

        if (FileData is null)
        {
            ErrProvider.WorkProvider(ProviderType.Alert, "Файл шаблона обязательная привязка для заполнения");

            result = false;
        }

        return result;
    }

    async private void AddBtFile_Pressed(object sender, EventArgs e)
    {
        FilePickerFileType customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "org.openxmlformats.wordprocessingml.document" } }, // UTType values
                    { DevicePlatform.Android, new[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".docx" } },
                    { DevicePlatform.macOS, new[] { "docx" } }, // UTType values
                });

        PickOptions options = new()
        {
            PickerTitle = "Выбор DOCX файла",
            FileTypes = customFileType
        };

        FileResult file = await FilePicker.PickAsync(options);

        if (file != null)
        {
            if (this.BindingContext is null)
            {
                FileData = File.ReadAllBytes(file.FullPath);
                addBtFile.Source = ConverFiles.ToImageConvert(Properties.Resources.shablon);
            }
            else
            {
                ((ShablonDoc)this.BindingContext).Doc = File.ReadAllBytes(file.FullPath);
            }
        }
    }
}
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using Finance.Pages.WorkPage.Shablons.CustomControl;
using Finance.Pages.WorkPage.Shablons.Enums;
using Finance.Pages.WorkPage.Shablons.Models;
using DocxDocWorkingLib;
using Finance.Classes.AppSettings.PermissionRequest;

namespace Finance.Pages.WorkPage.Shablons;

public partial class ShablonPrinting : ContentPage
{
    public TypeDoc TypeDoc;

    Loading loading { get; set; }

    public ShablonPrinting()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
            AsTargetNull.Margin = new Thickness(0, 0, 0, 0);
            BarHorzontal.HeightRequest = 60;
        #else
            if (!this.ToolbarItems.Any(i => i.ClassId == "backButton"))
            {
                ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back), ClassId = "backButton" };
                toolbarItem.Clicked += Back_Clicked;
                this.ToolbarItems.Add(toolbarItem);
            }
        #endif

        if (!this.ToolbarItems.Any(i => i.ClassId == "saveButton"))
        {
            ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), ClassId = "saveButton", Text = "Сохранить документ" };
            toolbarItemSave.Clicked += SaveShablon_Clicked;
            this.ToolbarItems.Add(toolbarItemSave);
        }
    }

    private void SaveShablon_Clicked(object? sender, EventArgs e)
    {
        try
        {
            if (!IsCheck()) return;

            loading = new Loading();

            this.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                string fileName = Enum.GetName(TypeDoc.GetType(), TypeDoc);
                MemoryStream streamDoc = null;
                ShablonDoc shablonDoc = (ShablonDoc)this.BindingContext;

                if (TypeDoc.Equals(TypeDoc.DocDkpAvto))
                {
                    DocDkpInfo docDkpInfo = CraftObjDKpAvto();

                    streamDoc = (MemoryStream)docDkpInfo.ReportCraft(shablonDoc.Doc, cbTargetNull.IsChecked ? AsTargetNull.Text ?? default : default);

                    if (docDkpInfo.CarInfo is null)
                    {
                        fileName += ".docx";
                    }
                    else
                    {
                        fileName += $"_{docDkpInfo.CarInfo.Make}-{docDkpInfo.CarInfo.Model}.docx";
                    }
                }

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    PermissionsRequests.PermissionStorageRequest();
                    FileSaverResult fileSaverResult = await FileSaver.Default.SaveAsync(fileName, streamDoc);

                    if (fileSaverResult.IsSuccessful)
                    {
                        await this.Messege($"Файл удачно сохранен", ProviderType.Info);
                    }
                    else
                    {
                        await this.Messege(fileSaverResult.Exception.Message, ProviderType.Info);
                    }
                });

                streamDoc.Dispose();

                await MainThread.InvokeOnMainThreadAsync(() => this.BackButtonInNavClick());
            }));
        }
        catch (Exception ex)
        {
            ErrProvider.WorkProvider(ProviderType.Error, ex.Message);
        }
    }

    private bool IsCheck()
    {
        bool result = true;

        foreach (IView view in VLBind.Children.Skip(2))
        {
            if (view.GetType().Equals(typeof(OwnersBind)) && ((OwnersBind)view).Owner is null)
            {
                ((OwnersBind)view).AsNameProvider.WorkProvider(ProviderType.Alert, "Привязка собственика не указана");
                result = false;
            }
            else if(view.GetType().Equals(typeof(ImuhBind)) && ((ImuhBind)view).Estate is null)
            {
                ((ImuhBind)view).AsNameProvider.WorkProvider(ProviderType.Alert, "Привязка собствености не указана");
                result = false;
            }
            else if (view.GetType().Equals(typeof(LibAddressBind)) && ((LibAddressBind)view).Address is null)
            {
                ((LibAddressBind)view).AsNameProvider.WorkProvider(ProviderType.Alert, "Привязка адреса не указана");
                result = false;
            }
        }

        return result;
    }

    private DocDkpInfo CraftObjDKpAvto()
    {
        DocDkpInfo docDkpInfo = new DocDkpInfo()
        {
            DocDate = AsDocDate.Date
        };

        foreach (IView view in VLBind.Children.Skip(2))
        {
            if (view.GetType().Equals(typeof(OwnersBind)))
            {
                docDkpInfo.ByerInfo = ((OwnersBind)view).Owner;
            }
            else if (view.GetType().Equals(typeof(ImuhBind)))
            {
                docDkpInfo.SellerInfo = DBModel.GetModel<Owners>(((ImuhBind)view).Estate.IdOwner);
                docDkpInfo.CarInfo = DBModel.GetModel<Car>(((ImuhBind)view).Estate.IdCar);
            }
            else if (view.GetType().Equals(typeof(LibAddressBind)))
            {
                docDkpInfo.DocAddress = ((LibAddressBind)view).Address;
            }
        }

        return docDkpInfo;
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

    private void AddBindOwner_Clicked(object sender, EventArgs e) => VLBind.Add(new OwnersBind());

    private void AddBindImuh_Clicked(object sender, EventArgs e) => VLBind.Add(new ImuhBind());

    private void AddBindAddress_Clicked(object sender, EventArgs e) => VLBind.Add(new LibAddressBind());

    private void cbTargetNull_CheckedChanged(object sender, CheckedChangedEventArgs e) => AsTargetNull.Text = cbTargetNull.IsChecked ? "Отсутствует" : null;
}
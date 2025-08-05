using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.FlyautPage;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Finance;

public partial class MoneTransferListPage : ContentPage
{
    ObservableCollection<View.MoneyTransfers> ViewMoneyTransfer;
    int offset = 0;
    int count = 0;
    Loading loading { get; set; }

    public MoneTransferListPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async() =>
        {
            try
            {
                ViewMoneyTransfer = DBModel.GetCollectionModel<View.MoneyTransfers>($"SELECT mt.* FROM `MoneyTransfers` mt INNER JOIN `Assets` a ON a.`IdUser` = '{InfoAccount.IdUser}' AND a.`Id` = mt.`IdAssets` ORDER BY mt.`TimeTransfer` DESC LIMIT {StartParametrs.LenListPage}");
                count = Convert.ToInt32(DBModel.ResultRequest($"SELECT COUNT(mt.`Id`) FROM `MoneyTransfers` mt INNER JOIN `Assets` a ON a.`IdUser` = '{InfoAccount.IdUser}' AND a.`Id` = mt.`IdAssets`"));
                offset = StartParametrs.LenListPage;

                if (ViewMoneyTransfer is null || ViewMoneyTransfer.Count() == 0) throw new Exception("” вас отсутствуют денежные операции");
                else
                {
                    await MainThread.InvokeOnMainThreadAsync(() => 
                    { 
                        BindableLayout.SetItemsSource(mtVSL, ViewMoneyTransfer);
                        btAddItem.IsVisible = count > StartParametrs.LenListPage;
                    });
                }
            }
            catch (Exception ex) 
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
    private void DeleteAsset_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        var moneyTransfer = (View.MoneyTransfers)((ImageButton)sender).BindingContext;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.MoneyTransfers>(moneyTransfer.Id).DeleteModel<Models.MoneyTransfers>()));

        ViewMoneyTransfer.Remove(moneyTransfer);
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                var items = DBModel.GetCollectionModel<View.MoneyTransfers>($"SELECT mt.* FROM `MoneyTransfers` mt INNER JOIN `Assets` a ON a.`IdUser` = '{InfoAccount.IdUser}' AND a.`Id` = mt.`IdAssets` ORDER BY mt.`TimeTransfer` DESC LIMIT {StartParametrs.LenListPage} OFFSET {offset}");
                offset += StartParametrs.LenListPage;

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    for (int i = 0; i < items.Count; i++) ViewMoneyTransfer.Add(items[i]);
                    btAddItem.IsVisible = count >= offset;
                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
}
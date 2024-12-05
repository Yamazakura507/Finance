using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.CustomControl;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage;

public partial class GroupCharPage : ContentPage
{
    ObservableCollection<View.AssetsGroupChart> ViewGroupChart;
    Loading loading { get; set; }

    public GroupCharPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        #if ANDROID || IOS
          
        #else
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        #endif

        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                ViewGroupChart = DBModel.GetCollectionModel<View.AssetsGroupChart>($@"CREATE TEMPORARY TABLE IF NOT EXISTS `GroupChart` AS (SELECT ag.* FROM `AssetsGroup` ag
                                                                    INNER JOIN `GroupingAssets` ga ON ga.`IdGroupAssets` = ag.`Id`
                                                                    WHERE ag.`IdUser` = '{InfoAccount.IdUser}'
                                                                    GROUP BY ag.`Id`
                                                                    ORDER BY ag.`Name` ASC);
                                                                  SELECT * FROM `GroupChart`;");

                await MainThread.InvokeOnMainThreadAsync(() => BindableLayout.SetItemsSource(groupChartList, ViewGroupChart));
            }
            catch (Exception) { }
        }));
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();
}
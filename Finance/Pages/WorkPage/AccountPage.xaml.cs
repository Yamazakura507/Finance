using CommunityToolkit.Maui.Views;
using Microcharts;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Models;
using Finance.View;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage;

public partial class AccountPage : ContentPage
{

    ObservableCollection<Quadrants> ViewQuadrants;
    Loading loading { get; set; }

    public AccountPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
        {
            try
            {
                var income = DBModel.GetCollectionModel<Revenues>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } }, 24, default, new Dictionary<string, bool>() { { "IdDate", false } });

                var active = income.Where(i => i.IsRevenues).Select(i => new ChartEntry((float)i.Sum)
                {
                    ValueLabel = i.Sum.ToString(),
                    Label = i.DateStr,
                    TextColor = SKColor.Parse("#8000ff1e"),
                    ValueLabelColor = SKColor.Parse("#8000ff1e"),
                    Color = SKColor.Parse("#8000ff1e")
                });

                var pasive = income.Where(i => !i.IsRevenues).Select(i => new ChartEntry((float)i.Sum)
                {
                    ValueLabel = i.Sum.ToString(),
                    Label = i.DateStr,
                    TextColor = SKColor.Parse("#80ff0000"),
                    ValueLabelColor = SKColor.Parse("#80ff0000"),
                    Color = SKColor.Parse("#80ff0000")
                });

                var result = DBModel.GetCollectionModel<Result>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } }, 12, default, new Dictionary<string, bool>() { { "IdDate", false } });

                var profit = result.Select(i => new ChartEntry((float)i.Profit)
                {
                    ValueLabel = i.Profit.ToString(),
                    Label = i.DateStr,
                    TextColor = SKColor.Parse("#80ffdd00"),
                    ValueLabelColor = SKColor.Parse("#80ffdd00"),
                    Color = SKColor.Parse("#80ffdd00")
                });

                Random rnd = new Random();

                SKColor[] colors = new SKColor[result.Count];

                for (int i = 0; i < colors.Length; i++)
                    colors[i] = SKColor.FromHsv(rnd.Next(360), rnd.Next(100), rnd.Next(100));

                var profitEvrefing = result.Select(i => new ChartEntry((float)i.ProfitForEvrifing)
                {
                    ValueLabel = i.ProfitForEvrifing.ToString(),
                    Label = i.DateStr,
                    TextColor = SKColor.Parse("#80C2EEFF"),
                    ValueLabelColor = SKColor.Parse("#80C2EEFF"),
                    Color = colors[result.IndexOf(i)]
                });

                ViewQuadrants = new ObservableCollection<Quadrants>(DBModel.GetCollectionModel<QuadrantsUser>(new Dictionary<string, object>() { { "IdUser", InfoAccount.IdUser } }).Select(i => i.Quadrants));

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    chartInc.Chart = new LineChart()
                    {
                        Entries = active,
                        BackgroundColor = SKColor.FromHsl(203,100,5)
                    };

                    chartExc.Chart = new LineChart()
                    {
                        Entries = pasive,
                        BackgroundColor = SKColor.FromHsl(203, 100, 5)
                    };

                    chartResPr.Chart = new LineChart()
                    {
                        Entries = profit,
                        BackgroundColor = SKColor.FromHsl(203, 100, 5)
                    };

                    chartResPrE.Chart = new DonutChart()
                    {
                        Entries = profitEvrefing,
                        BackgroundColor = SKColor.Empty
                    };

                    BindableLayout.SetItemsSource(quadrantsVSL, ViewQuadrants);
                });
            }
            catch (Exception)
            {
                return;
            }
        }));
    }

    private void Quadrants_Tapped(object sender, TappedEventArgs e) => this.Messege(((Quadrants)((ContentView)sender).BindingContext).Commit, ProviderType.Info);

    private void SupportBt_Pressed(object sender, EventArgs e) => this.Messege(((Quadrants)((ImageButton)sender).BindingContext).DownLimit, ProviderType.Info);
}
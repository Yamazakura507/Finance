using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.CustomControl;
using MapLib.EnumMap;
using MapLib.MapModel;
using Map = MapLib.Map;

namespace Finance.Pages.WorkPage.Setting;

public partial class MapPage : ContentPage
{
    public MapObject MapObject { get; set; } = null;
    public bool IsUpdate = false;

    Loading loading { get; set; }
    Models.LibAddress libAddress = null;

    public MapPage(View.LibAddress libAddress)
    {
        InitializeComponent();

        this.libAddress = DBModel.GetModel<Models.LibAddress>(libAddress.Id);
        this.IsUpdate = true;
    }

    public MapPage()
    {
        InitializeComponent();
    }

    async private void ContentPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            MapApi mapApi = MapApi.search;
            Dictionary<string, string> parametrs = new Dictionary<string, string>()
            {
                { "accept-language", "ru"}
            };

            if (this.libAddress != null)
            {
                if (this.libAddress.Lontitude is null)
                {
                    parametrs.Add("q", libAddress.Address.Replace(", ", "+"));
                }
                else
                {
                    mapApi = MapApi.reverse;
                    parametrs.Add("lat", libAddress.Latitude);
                    parametrs.Add("lon", libAddress.Lontitude);
                }
            }

            Uri url = Map.UrlConcatMap(parametrs, mapApi, true);
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                web.Source = url;
                StarteButton(mapApi);
            });
        }));
    }

    private void StarteButton(MapApi mapApi)
    {
        #if ANDROID || IOS
        #else
            ToolbarItem toolbarItem = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.back) };
            toolbarItem.Clicked += Back_Clicked;
            this.ToolbarItems.Add(toolbarItem);
        #endif

        ToolbarItem toolbarItemSave = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.save), Text = "Выбрать" };
        toolbarItemSave.Clicked += ToolbarItemSave_Clicked;
        this.ToolbarItems.Add(toolbarItemSave);

        ToolbarItem toolbarItemMode = new ToolbarItem() { IconImageSource = ConverFiles.ToImageConvert(Properties.Resources.address), Text = mapApi.Equals(MapApi.reverse) ? "Поиск по адресу" : "Поиск по координатам" };
        toolbarItemMode.Clicked += ToolbarItemMode_Clicked;
        MainThread.InvokeOnMainThreadAsync(() => this.ToolbarItems.Add(toolbarItemMode));
    }

    private void ToolbarItemMode_Clicked(object? sender, EventArgs e)
    {
        ToolbarItem tbi = (ToolbarItem)sender;
        Dictionary<string, string> parametrs = new Dictionary<string, string>()
        {
            { "accept-language", "ru"}
        };
        Uri url;

        if (tbi.Text.Contains("адрес"))
        {
            tbi.Text = "Поиск по координатам";
            url = Map.UrlConcatMap(parametrs, MapApi.search, true);
        }
        else 
        {
            tbi.Text = "Поиск по адресу";
            url = Map.UrlConcatMap(parametrs, MapApi.reverse, true);
        }

        web.Source = url;
    }

    async private void ToolbarItemSave_Clicked(object? sender, EventArgs e)
    {
        MapObject[] mapsObject = null;
        string url = await web.EvaluateJavaScriptAsync("window.location.href");
        string searh = Map.UrlToSearh(url);
        
        if (searh is null)
        {
            string[] coordinates = Map.UrlToCoordinate(url);

            if (coordinates[0] != null)
            {
                mapsObject = await Map.GetCoordinateMap(coordinates);
            }
        }
        else
        {
            mapsObject = await Map.GetSearhMap(searh);
        }

        if (mapsObject != null)
        {
            if (mapsObject.Length == 1)
            {
                MapObject = mapsObject[0];
            }
            else if (mapsObject.Length > 1)
            {
                string selAddress = await this.SheetMessege("Выбор адреса", mapsObject.Select(i => String.Concat(i.DisplayName.Substring(0,30),"...")).ToArray());

                if (!String.IsNullOrEmpty(selAddress) && selAddress != "ОТМЕНА")
                {
                    MapObject = mapsObject.First(i => i.DisplayName.Contains(selAddress.Replace("...","")));
                }
                else
                {
                    return;
                }
            }
        }

        this.BackButtonInNavClick();
    }

    private void Back_Clicked(object? sender, EventArgs e) => this.BackButtonInNavClick();

}
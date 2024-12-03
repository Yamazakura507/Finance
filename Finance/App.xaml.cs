using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.AppSettings;
using Finance.CustomControl;
using Finance.Pages.Tabbed;

namespace Finance
{
    public partial class App : Application
    {
        public static Page MyAppShell { get; set; }
        public static NetworkAccess network { get; set; }

        public App()
        {
            InitializeComponent();

            MyAppShell = Autorizate(StartParametrs.IdAutorizateUser) ? new Pages.FlyautPage.PersonalAccountPage() : new MainTabbedPage();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            Connectivity_ConnectivityChanged(null, null);
            MainPage = MyAppShell;
        }

        private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            network = e is null ? Connectivity.NetworkAccess : e.NetworkAccess;

            if (network != NetworkAccess.Internet) 
            {
                var logingNoNetwork = new Loading(true);
                MyAppShell.ShowPopup(logingNoNetwork);

                logingNoNetwork.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
                 {
                     while (network != NetworkAccess.Internet) 
                     {
                         Thread.Sleep(500);
                     }
                 }));

                GC.Collect();
            }
        }

        public static bool Autorizate(int idUser)
        {
            try
            {
                if (Convert.ToBoolean(DBModel.ResultRequest($"SELECT COUNT(*) <> 0 FROM `Users` u WHERE u.`Id` = '{idUser}'")))
                {
                    InfoAccount.IdUser = idUser;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
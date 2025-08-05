using Finance.Classes.Enums;

namespace Finance.Classes.AppSettings
{
    public static class StartParametrs
    {
        public static int IdAutorizateUser 
        {
            get
            {
                return Preferences.Get(nameof(IdAutorizateUser), 0);
            } 
            set 
            {
                Preferences.Set(nameof(IdAutorizateUser), value);
            }
        }

        public static int LenListPage
        {
            get
            {
                return Preferences.Get(nameof(LenListPage), 10);
            }
            set
            {
                Preferences.Set(nameof(LenListPage), value);
            }
        }

        public static void ClearParametr() => Preferences.Clear();
    }
}

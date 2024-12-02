namespace Finance.Classes.AppSettings
{
    public class StartParametrs
    {
        public int IdAutorizateUser 
        {
            get
            {
                return Preferences.Default.Get("IdAutorizateUser", 0);
            } 
            set 
            {
                Preferences.Default.Set("IdAutorizateUser", value);
            }
        }

        public static void ClearParametr() => Preferences.Default.Clear();
    }
}

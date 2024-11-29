using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Finance.Classes.AppSettings
{
    public class StartParametrs
    {
        private static string FileName = Path.Combine(String.Join("\\",Environment.ProcessPath.Split('\\').SkipLast(1)), "temp.json");

        private int idAutorizateUser;
        private bool isGet = false;

        public int IdAutorizateUser 
        {
            get
            {
                return !this.isGet ? GetParametrs<int>("IdAutorizateUser") : this.idAutorizateUser;
            } 
            set 
            {
                this.idAutorizateUser = value;
                if (!this.isGet)
                {
                    Save();
                }
            }
        }

        public StartParametrs()
        {
            int idAutorizateUser = 0;

            if (File.Exists(FileName))
            {
                idAutorizateUser = GetParametrs<int>("IdAutorizateUser");
            }

            this.idAutorizateUser = idAutorizateUser;
        }

        [JsonConstructor]
        public StartParametrs(int idAutorizateUser)
        {
            isGet = true;
            this.IdAutorizateUser = idAutorizateUser;
        }

        private T GetParametrs<T>(string fildsName)
        {
            try
            {
                StartParametrs parametrs;

                isGet = true;

                //#if ANDROID || IOS
                //    FileName = Path.Combine(String.Join("\\", Environment.ProcessPath.Split('\\').SkipLast(1)), "tempFinance.json");
                //#endif

                using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
                {
                    parametrs = JsonSerializer.Deserialize<StartParametrs>(fs) ?? new StartParametrs(0);
                }

                isGet = false;

                return (T)typeof(StartParametrs).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).ToArray().First(i => i.Name.Equals(fildsName, StringComparison.InvariantCultureIgnoreCase)).GetValue(parametrs);
            }
            catch (IOException)
            {
                isGet = false;
                return (T)typeof(StartParametrs).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).ToArray().First(i => i.Name.Equals(fildsName, StringComparison.InvariantCultureIgnoreCase)).GetValue(this);
            }
            catch (Exception ex)
            {
                isGet = false;
                Save();
                return (T)typeof(StartParametrs).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).ToArray().First(i => i.Name.Equals(fildsName, StringComparison.InvariantCultureIgnoreCase)).GetValue(this);
            }
        }

        private async void Save()
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync(fs, this);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ClearParametr() => File.Delete(FileName);
    }
}

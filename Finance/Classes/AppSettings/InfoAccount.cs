using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Finance.Classes.AppSettings
{
    public static class InfoAccount
    {
        private static int idUser;

        public static int IdUser 
        { 
            get 
            { 
                return idUser; 
            }
            set 
            {
                var RstristionUsers = DBModel.GetCollectionModel<RestrictionsUser>(new Dictionary<string, object>() { { "IdUser", value } });

                User = RstristionUsers.FirstOrDefault().User;
                IsAdmin = RstristionUsers.Any(i => i.IdRestrictions == 2);
                CountSupport =  Convert.ToInt32(DBModel.ResultRequest($"SELECT COUNT(*) FROM `Support` s WHERE {(IsAdmin ? "not s.IsAnswer" : "s.`IdUser` = '1' AND s.`IsAnswer` AND not s.`IsReadAnswer`")}"));
                idUser = value;
            }
        }

        public static Users User { get; set; }
        public static bool IsAdmin { get; set; } = false;
        public static int CountSupport { get; set; }
    }
}

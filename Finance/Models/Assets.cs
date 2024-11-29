using MySqlConnector;
using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class Assets : DBModel
    {
        private int id;
        private bool isStability;
        private int idFlowType;
        private decimal sum;
        private string name;
        private string use;
        private bool isAssets;
        private int? idUser;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Assets>("Id", value);
                }

                AssetsGroup = ResultRequest($"SELECT GROUP_CONCAT(ga.`IdGroupAssets` SEPARATOR ',') FROM `GroupingAssets` ga WHERE ga.`IdAssets` = '{value}' GROUP BY ga.`IdGroupAssets`").ToString();
                id = value;
            }
        }
        public bool IsStability
        {
            get => !IsGet ? GetParametrs<bool>("IsStability", this.GetType()) : isStability;
            set
            {
                isStability = value;
                if (!IsGet)
                {
                    SetParametrs<Assets>("IsStability", value);
                }
            }
        }
        public int IdFlowType
        {
            get => !IsGet ? GetParametrs<int>("IdFlowType", this.GetType()) : idFlowType;
            set
            {
                idFlowType = value;
                if (!IsGet)
                {
                    SetParametrs<Assets>("IdFlowType", value);
                }

                FlowType = GetModel<FlowType>(value);
            }
        }
        public decimal Sum
        {
            get => !IsGet ? GetParametrs<decimal>("Sum", this.GetType()) : sum;
            set
            {
                sum = value;
                if (!IsGet)
                {
                    SetParametrs<Assets>("Sum", value);
                }
            }
        }
        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {       
                    if (!IsGet)
                    {
                        SetParametrs<Assets>("Name", value is null ? DBNull.Value : value);
                    }
                    name = value;
            }
        }
        public string Use
        {
            get => !IsGet ? GetParametrs<string>("Use", this.GetType()) : use;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Assets>("Use", value is null ? DBNull.Value : value);
                }
                use = value;
            }
        }
        public bool IsAsset
        {
            get => !IsGet ? GetParametrs<bool>("IsAsset", this.GetType()) : isAssets;
            set
            {
                isAssets = value;
                if (!IsGet)
                {
                    SetParametrs<Assets>("IsAsset", value);
                }
            }
        }
        public int? IdUser
        {
            get => !IsGet ? GetParametrs<int?>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Assets>("IdUser", value is null ? DBNull.Value : value);
                }

                User = value is null ? null : GetModel<Users>(value);
                idUser = value;
            }
        }

        [XmlIgnore]
        public FlowType FlowType { get; private set; }
        [XmlIgnore]
        public Users User { get; private set; }
        [XmlIgnore]
        private string AssetsGroup { get; set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "IsStability", "IdFlowType", "IsAssets", "Sum" }.Contains(param))
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_asset_or_pasive((SELECT du.`IdDate` FROM `DateUser` du WHERE du.IdUser=@IdUser ORDER BY du.`Id` LIMIT 1),'{idFlowType}',@Sum,@Name,@Use,@IsStab,@IsAs,'{id}','{AssetsGroup}',NULL,@IdUser)", new[]
                    {
                        new MySqlParameter("@Name", String.IsNullOrEmpty(name) ? DBNull.Value : name),
                        new MySqlParameter("@Use", String.IsNullOrEmpty(use) ? DBNull.Value : use),
                        new MySqlParameter("@Sum", sum),
                        new MySqlParameter("@IsStab", isStability),
                        new MySqlParameter("@IsAs", isAssets),
                        new MySqlParameter("@IdUser", idUser is null ? DBNull.Value : idUser)
                    }, 2);
            }
            else if (new[] { "Name", "Use" }.Contains(param))
                base.SetParametrs<T>(param, value, id);
            else
                return;
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            using (var ms = new Mysql())
            {
                if (Id is null && WhereCollection is null)
                {
                    ms.ExecSql($"CALL delete_assets('{id}')");
                }
                else
                {
                    ms.ExecSql($"CALL delete_assets('{Id}')");
                }
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Assets>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Assets>(parametrs, Id, WhereCollection);
            }
        }
    }
}

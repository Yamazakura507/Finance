using MySqlConnector;
using Finance.Classes;

namespace Finance.Models
{
    public class Assets : Abstract.AbstractModelStatus<Assets>
    {
        private bool isStability;
        private int idFlowType;
        private decimal sum;
        private bool isAssets;

        public new int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (id != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Assets>("Id", value);
                    }

                    AssetsGroup = ResultRequest($"SELECT GROUP_CONCAT(ga.`IdGroupAssets` SEPARATOR ',') FROM `GroupingAssets` ga WHERE ga.`IdAssets` = '{value}' GROUP BY ga.`IdGroupAssets`").ToString();
                    id = value;
                }
            }
        }
        public bool IsStability
        {
            get => !IsGet ? GetParametrs<bool>("IsStability", this.GetType()) : isStability;
            set
            {
                if (isStability != value)
                {
                    isStability = value;
                    if (!IsGet)
                    {
                        SetParametrs<Assets>("IsStability", value);
                    }
                }
            }
        }
        public int IdFlowType
        {
            get => !IsGet ? GetParametrs<int>("IdFlowType", this.GetType()) : idFlowType;
            set
            {
                if (idFlowType != value)
                {
                    idFlowType = value;
                    if (!IsGet)
                    {
                        SetParametrs<Assets>("IdFlowType", value);
                    }

                    FlowType = GetModel<FlowType>(value);
                }
            }
        }
        public decimal Sum
        {
            get => !IsGet ? GetParametrs<decimal>("Sum", this.GetType()) : sum;
            set
            {
                if (sum != value)
                {
                    sum = value;
                    if (!IsGet)
                    {
                        SetParametrs<Assets>("Sum", value);
                    }
                }
            }
        }
        public bool IsAsset
        {
            get => !IsGet ? GetParametrs<bool>("IsAsset", this.GetType()) : isAssets;
            set
            {
                if (isAssets != value)
                {
                    isAssets = value;
                    if (!IsGet)
                    {
                        SetParametrs<Assets>("IsAsset", value);
                    }
                }
            }
        }

        public FlowType FlowType { get; private set; }
        private string AssetsGroup { get; set; }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "IsStability", "IdFlowType", "IsAssets", "Sum" }.Contains(param))
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_asset_or_pasive((SELECT du.`IdDate` FROM `DateUser` du WHERE du.IdUser=@IdUser ORDER BY du.`Id` LIMIT 1),'{idFlowType}',@Sum,@Name,@Use,@IsStab,@IsAs,'{id}','{AssetsGroup}',NULL,@IdUser)", new[]
                    {
                        new MySqlParameter("@Name", String.IsNullOrEmpty(name) ? DBNull.Value : name),
                        new MySqlParameter("@Use", String.IsNullOrEmpty(description) ? DBNull.Value : description),
                        new MySqlParameter("@Sum", sum),
                        new MySqlParameter("@IsStab", isStability),
                        new MySqlParameter("@IsAs", isAssets),
                        new MySqlParameter("@IdUser", idUser is null ? DBNull.Value : idUser)
                    }, 2);
            }
            else if (new[] { "Name", "Description" }.Contains(param))
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
    }
}

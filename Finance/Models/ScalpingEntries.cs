using Finance.Classes;
using MySqlConnector;

namespace Finance.Models
{
    public class ScalpingEntries : DBModel
    {
        private int id;
        private int idStatusScalping;
        private int idBeastStatus;
        private int idBroker;
        private int? countInFutures;
        private int countLot;
        private string name;
        private decimal? goPirPeace;
        private decimal priceEntry;
        private decimal priceExit;
        private decimal pointZero;
        private decimal? priceStep;
        private int? idTypeCommission;
        private int idTax;
        private int idScalping;
        private int? idScalpingActive;
        private decimal margin;
        private decimal marginBeforeTax;
        private decimal commissing;
        private decimal? taxSum;
        private DateTime? dateExit;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("Id", value);
                }
                id = value;
            }
        }

        public int CountLot
        {
            get => !IsGet ? GetParametrs<int>("CountLot", this.GetType()) : countLot;
            set
            {
                countLot = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("CountLot", value);
                }
            }
        }

        public int? CountInFutures
        {
            get => !IsGet ? GetParametrs<int?>("CountInFutures", this.GetType()) : countInFutures;
            set
            {
                countInFutures = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("CountInFutures", value is null ? DBNull.Value : value);
                }
            }
        }

        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {
                name = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("Name", value is null ? DBNull.Value : value);
                }
            }
        }

        public decimal? GOPirPeace
        {
            get => !IsGet ? GetParametrs<decimal?>("GOPirPeace", this.GetType()) : goPirPeace;
            set
            {
                goPirPeace = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("GOPirPeace", value is null ? DBNull.Value : value);
                }
            }
        }

        public decimal? PriceStep
        {
            get => !IsGet ? GetParametrs<decimal?>("PriceStep", this.GetType()) : priceStep;
            set
            {
                priceStep = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("PriceStep", value is null ? DBNull.Value : value);
                }
            }
        }

        public decimal PriceEntry
        {
            get => !IsGet ? GetParametrs<decimal>("PriceEntry", this.GetType()) : priceEntry;
            set
            {
                priceEntry = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("PriceEntry", value);
                }
            }
        }

        public decimal PriceExit
        {
            get => !IsGet ? GetParametrs<decimal>("PriceExit", this.GetType()) : priceExit;
            set
            {
                priceExit = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("PriceExit", value);
                }
            }
        }

        public decimal PointZero
        {
            get => !IsGet ? GetParametrs<decimal>("PointZero", this.GetType()) : pointZero;
            set
            {
                pointZero = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("PointZero", value);
                }
            }
        }

        public DateTime? DateExit
        {
            get => !IsGet ? GetParametrs<DateTime?>("DateExit", this.GetType()) : dateExit;
            set
            {
                dateExit = value;
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("DateExit", value is null ? DBNull.Value : value);
                }
            }
        }

        public int? IdTypeCommission
        {
            get => !IsGet ? GetParametrs<int?>("IdTypeCommission", this.GetType()) : idTypeCommission;
            set
            {
                idTypeCommission = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("IdTypeCommission", value is null ? DBNull.Value : value);
                }

                TypeCommission = value is null ? null : GetModel<TypeCommission>(value);
            }
        }

        public int IdTax
        {
            get => !IsGet ? GetParametrs<int>("IdTax", this.GetType()) : idTax;
            set
            {
                idTax = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("IdTax", value);
                }

                Tax = GetModel<Tax>(value);
            }
        }

        public int IdStatusScalping
        {
            get => !IsGet ? GetParametrs<int>("IdStatusScalping", this.GetType()) : idStatusScalping;
            set
            {
                idStatusScalping = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("IdStatusScalping", value);
                }

                StatusScalping = GetModel<StatusScalping>(value);
            }
        }

        public int IdBeastStatus
        {
            get => !IsGet ? GetParametrs<int>("IdBeastStatus", this.GetType()) : idBeastStatus;
            set
            {
                idBeastStatus = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("IdBeastStatus", value);
                }

                BeastStatus = GetModel<BeastStatus>(value);
            }
        }

        public int IdBroker
        {
            get => !IsGet ? GetParametrs<int>("IdBroker", this.GetType()) : idBroker;
            set
            {
                idBroker = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("IdBroker", value);
                }

                Broker = GetModel<Broker>(value);
            }
        }

        public int IdScalping
        {
            get => !IsGet ? GetParametrs<int>("IdScalping", this.GetType()) : idScalping;
            set
            {
                idScalping = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("IdScalping", value);
                }

                Scalping = GetModel<Scalping>(value);
            }
        }

        public int? IdScalpingActive
        {
            get => !IsGet ? GetParametrs<int?>("IdScalpingActive", this.GetType()) : idScalpingActive;
            set
            {
                idScalpingActive = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("IdScalpingActive", value is null ? DBNull.Value : value);
                }

                ScalpingActive = value is null ? null : GetModel<ScalpingActive>(value);
            }
        }

        public decimal Margin
        {
            get => !IsGet ? GetParametrs<decimal>("Margin", this.GetType()) : margin;
            set
            {
                margin = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("Margin", value);
                }
            }
        }

        public decimal MarginBeforeTax
        {
            get => !IsGet ? GetParametrs<decimal>("MarginBeforeTax", this.GetType()) : marginBeforeTax;
            set
            {
                marginBeforeTax = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("MarginBeforeTax", value);
                }
            }
        }

        public decimal Commissing
        {
            get => !IsGet ? GetParametrs<decimal>("Commissing", this.GetType()) : commissing;
            set
            {
                commissing = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("Commissing", value);
                }
            }
        }

        public decimal? TaxSum
        {
            get => !IsGet ? GetParametrs<decimal?>("TaxSum", this.GetType()) : taxSum;
            set
            {
                taxSum = value;
                if (!IsGet)
                {
                    SetParametrs<ScalpingEntries>("TaxSum", value);
                }
            }
        }

        public TypeCommission TypeCommission { get; private set;}

        public Tax Tax { get; private set;}

        public Scalping Scalping { get; private set;}

        public ScalpingActive ScalpingActive { get; private set;}

        public BeastStatus BeastStatus { get; private set;}

        public StatusScalping StatusScalping { get; private set;}

        public Broker Broker { get; private set;}

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "Id", "IdTax", "Margin", "MarginBeforeTax", "Commissing", "TaxSum" }.Contains(param))
                return;
            else
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_scalp_entries('{id}','{idScalping}','{idBeastStatus}',@IdTyCom,'{idBroker}','{idStatusScalping}','{idTax}',@IdScAct,@PriceEn,'{countLot}',@PriceEx,@CntInFut,@GO,@Name,@PrStep);", new[]
                    {
                        new MySqlParameter("@IdTyCom", idTypeCommission is null ? DBNull.Value : idTypeCommission),
                        new MySqlParameter("@IdScAct", idScalpingActive is null ? -1 : idScalpingActive),
                        new MySqlParameter("@CntInFut", countInFutures is null ? DBNull.Value : countInFutures),
                        new MySqlParameter("@GO", goPirPeace is null ? DBNull.Value : goPirPeace),
                        new MySqlParameter("@Name", String.IsNullOrEmpty(name) ? DBNull.Value : name),
                        new MySqlParameter("@PrStep", priceStep is null ? DBNull.Value : priceStep),
                        new MySqlParameter("@PriceEn", priceEntry),
                        new MySqlParameter("@PriceEx", priceExit)
                    });
            }
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.DeleteModel<ScalpingEntries>(this.Id);
            }
            else
            {
                base.DeleteModel<ScalpingEntries>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<ScalpingEntries>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<ScalpingEntries>(parametrs, Id, WhereCollection);
            }
        }
    }
}

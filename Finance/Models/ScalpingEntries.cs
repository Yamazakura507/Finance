using Finance.Classes;
using MySqlConnector;

namespace Finance.Models
{
    public class ScalpingEntries : Abstract.AbstractModelStatus<ScalpingEntries>
    {
        private int idStatusScalping;
        private int idBeastStatus;
        private int idBroker;
        private int? countInFutures;
        private int countLot;
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

        public int CountLot
        {
            get => !IsGet ? GetParametrs<int>("CountLot", this.GetType()) : countLot;
            set
            {
                if (countLot != value)
                {
                    countLot = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("CountLot", value);
                    }
                }
            }
        }

        public int? CountInFutures
        {
            get => !IsGet ? GetParametrs<int?>("CountInFutures", this.GetType()) : countInFutures;
            set
            {
                if (countInFutures != value)
                {
                    countInFutures = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("CountInFutures", value is null ? DBNull.Value : value);
                    }
                }
            }
        }

        public decimal? GOPirPeace
        {
            get => !IsGet ? GetParametrs<decimal?>("GOPirPeace", this.GetType()) : goPirPeace;
            set
            {
                if (goPirPeace != value)
                {
                    goPirPeace = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("GOPirPeace", value is null ? DBNull.Value : value);
                    }
                }
            }
        }

        public decimal? PriceStep
        {
            get => !IsGet ? GetParametrs<decimal?>("PriceStep", this.GetType()) : priceStep;
            set
            {
                if (priceStep != value)
                {
                    priceStep = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("PriceStep", value is null ? DBNull.Value : value);
                    }
                }
            }
        }

        public decimal PriceEntry
        {
            get => !IsGet ? GetParametrs<decimal>("PriceEntry", this.GetType()) : priceEntry;
            set
            {
                if (priceEntry != value)
                {
                    priceEntry = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("PriceEntry", value);
                    }
                }
            }
        }

        public decimal PriceExit
        {
            get => !IsGet ? GetParametrs<decimal>("PriceExit", this.GetType()) : priceExit;
            set
            {
                if (priceExit != value)
                {
                    priceExit = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("PriceExit", value);
                    }
                }
            }
        }

        public decimal PointZero
        {
            get => !IsGet ? GetParametrs<decimal>("PointZero", this.GetType()) : pointZero;
            set
            {
                if (pointZero != value)
                {
                    pointZero = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("PointZero", value);
                    }
                }
            }
        }

        public DateTime? DateExit
        {
            get => !IsGet ? GetParametrs<DateTime?>("DateExit", this.GetType()) : dateExit;
            set
            {
                if (dateExit != value)
                {
                    dateExit = value;
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("DateExit", value is null ? DBNull.Value : value);
                    }
                }
            }
        }

        public int? IdTypeCommission
        {
            get => !IsGet ? GetParametrs<int?>("IdTypeCommission", this.GetType()) : idTypeCommission;
            set
            {
                if (idTypeCommission != value)
                {
                    idTypeCommission = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("IdTypeCommission", value is null ? DBNull.Value : value);
                    }

                    TypeCommission = value is null ? null : GetModel<TypeCommission>(value);
                }
            }
        }

        public int IdTax
        {
            get => !IsGet ? GetParametrs<int>("IdTax", this.GetType()) : idTax;
            set
            {
                if (idTax != value)
                {
                    idTax = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("IdTax", value);
                    }

                    Tax = GetModel<Tax>(value);
                }
            }
        }

        public int IdStatusScalping
        {
            get => !IsGet ? GetParametrs<int>("IdStatusScalping", this.GetType()) : idStatusScalping;
            set
            {
                if (idStatusScalping != value)
                {
                    idStatusScalping = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("IdStatusScalping", value);
                    }

                    StatusScalping = GetModel<StatusScalping>(value);
                }
            }
        }

        public int IdBeastStatus
        {
            get => !IsGet ? GetParametrs<int>("IdBeastStatus", this.GetType()) : idBeastStatus;
            set
            {
                if (idBeastStatus != value)
                {
                    idBeastStatus = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("IdBeastStatus", value);
                    }

                    BeastStatus = GetModel<BeastStatus>(value);
                }
            }
        }

        public int IdBroker
        {
            get => !IsGet ? GetParametrs<int>("IdBroker", this.GetType()) : idBroker;
            set
            {
                if (idBroker != value)
                {
                    idBroker = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("IdBroker", value);
                    }

                    Broker = GetModel<Broker>(value);
                }
            }
        }

        public int IdScalping
        {
            get => !IsGet ? GetParametrs<int>("IdScalping", this.GetType()) : idScalping;
            set
            {
                if (idScalping != value)
                {
                    idScalping = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("IdScalping", value);
                    }

                    Scalping = GetModel<Scalping>(value);
                }
            }
        }

        public int? IdScalpingActive
        {
            get => !IsGet ? GetParametrs<int?>("IdScalpingActive", this.GetType()) : idScalpingActive;
            set
            {
                if (idScalpingActive != value)
                {
                    idScalpingActive = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("IdScalpingActive", value is null ? DBNull.Value : value);
                    }

                    ScalpingActive = value is null ? null : GetModel<ScalpingActive>(value);
                }
            }
        }

        public decimal Margin
        {
            get => !IsGet ? GetParametrs<decimal>("Margin", this.GetType()) : margin;
            set
            {
                if (margin != value)
                {
                    margin = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("Margin", value);
                    }
                }
            }
        }

        public decimal MarginBeforeTax
        {
            get => !IsGet ? GetParametrs<decimal>("MarginBeforeTax", this.GetType()) : marginBeforeTax;
            set
            {
                if (marginBeforeTax != value)
                {
                    marginBeforeTax = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("MarginBeforeTax", value);
                    }
                }
            }
        }

        public decimal Commissing
        {
            get => !IsGet ? GetParametrs<decimal>("Commissing", this.GetType()) : commissing;
            set
            {
                if (commissing != value)
                {
                    commissing = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("Commissing", value);
                    }
                }
            }
        }

        public decimal? TaxSum
        {
            get => !IsGet ? GetParametrs<decimal?>("TaxSum", this.GetType()) : taxSum;
            set
            {
                if (taxSum != value)
                {
                    taxSum = value;
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingEntries>("TaxSum", value);
                    }
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

        private new string Description { get; set; }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}

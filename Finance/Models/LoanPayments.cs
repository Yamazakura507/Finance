using MySqlConnector;
using Finance.Classes;

namespace Finance.Models
{
    public class LoanPayments : Abstract.AbstractModelStatus<LoanPayments>
    {
        private int idCredit;
        private int? idAssets;
        private decimal sum;
        private bool isBasic;
        private bool? isTerm;
        private DateTime? datePay;
        private decimal? overflowSum;
        private decimal beginSum;
        private decimal remnantSum;

        public int IdCredit
        {
            get => !IsGet ? GetParametrs<int>("IdCredit", this.GetType()) : idCredit;
            set
            {
                if (idCredit != value)
                {
                    idCredit = value;
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("IdCredit", value);
                    }

                    Credit = GetModel<Credit>(value);
                }
            }
        }
        public int? IdAssets
        {
            get => !IsGet ? GetParametrs<int?>("IdAssets", this.GetType()) : idAssets;
            set
            {
                if (idAssets != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("IdAssets", value is null ? DBNull.Value : value);
                    }

                    Assets = value is null ? null : GetModel<Assets>(value);
                    idAssets = value;
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
                        SetParametrs<LoanPayments>("Sum", value);
                    }
                }
            }
        }
        public bool IsBasic
        {
            get => !IsGet ? GetParametrs<bool>("IsBasic", this.GetType()) : isBasic;
            set
            {
                if (isBasic != value)
                {
                    isBasic = value;
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("IsBasic", value);
                    }
                }
            }
        }
        public bool? IsTerm
        {
            get => !IsGet ? GetParametrs<bool?>("IsTerm", this.GetType()) : isTerm;
            set
            {
                if (isTerm != value)
                {
                    isTerm = value;
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("IsTerm", value is null ? DBNull.Value : value);
                    }
                }
            }
        }
        public DateTime? DatePay
        {
            get => !IsGet ? GetParametrs<DateTime?>("DatePay", this.GetType()) : datePay;
            set
            {
                if (datePay != value)
                {
                    datePay = value;
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("DatePay", value is null ? DBNull.Value : value);
                    }
                }
            }
        }
        public decimal? OverflowSum
        {
            get => !IsGet ? GetParametrs<decimal?>("OverflowSum", this.GetType()) : overflowSum;
            set
            {
                if (overflowSum != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("OverflowSum", value is null ? DBNull.Value : value);
                    }
                    overflowSum = value;
                }
            }
        }
        public decimal BeginSum
        {
            get => !IsGet ? GetParametrs<decimal>("BeginSum", this.GetType()) : beginSum;
            set
            {
                if (beginSum != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("BeginSum", value);
                    }
                    beginSum = value;
                }
            }
        }
        public decimal RemnantSum
        {
            get => !IsGet ? GetParametrs<decimal>("RemnantSum", this.GetType()) : remnantSum;
            set
            {
                if (remnantSum != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LoanPayments>("RemnantSum", value);
                    }
                    remnantSum = value;
                }
            }
        }
        public Credit Credit { get; private set; }
        public Assets Assets { get; private set; }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "Id", "OverflowSum", "BeginSum", "RemnantSum" }.Contains(param))
                return;
            else if (new[] { "Description", "IdAssets" }.Contains(param))
                base.SetParametrs<T>(param, value, id);
            else
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_loan_payments('{id}','{idCredit}',@IdAssets,@Sum,@IsBasic,@Commit,@IsTerm,@DatePay);", new[]
                    {
                        new MySqlParameter("@IsTerm", isTerm is null ? DBNull.Value : isTerm),
                        new MySqlParameter("@IsBasic", isBasic),
                        new MySqlParameter("@IdAssets", idAssets is null ? DBNull.Value : idAssets),
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(description) ? DBNull.Value : description),
                        new MySqlParameter("@Sum", sum),
                        new MySqlParameter("@DatePay", datePay is null ? DBNull.Value : datePay)
                    });
            }
        }

        private new string Name { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}

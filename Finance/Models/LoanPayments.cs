using MySqlConnector;
using Finance.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class LoanPayments : DBModel
    {
        private int id;
        private int idCredit;
        private int? idAssets;
        private decimal sum;
        private bool isBasic;
        private string commit;
        private bool? isTerm;
        private DateTime? datePay;
        private decimal? overflowSum;
        private decimal beginSum;
        private decimal remnantSum;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("Id", value);
                }
                id = value;
            }
        }
        public int IdCredit
        {
            get => !IsGet ? GetParametrs<int>("IdCredit", this.GetType()) : idCredit;
            set
            {
                idCredit = value;
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("IdCredit", value);
                }

                Credit = GetModel<Credit>(value);
            }
        }
        public int? IdAssets
        {
            get => !IsGet ? GetParametrs<int?>("IdAssets", this.GetType()) : idAssets;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("IdAssets", value is null ? DBNull.Value : value);
                }

                Assets = value is null ? null : GetModel<Assets>(value);
                idAssets = value;
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
                    SetParametrs<LoanPayments>("Sum", value);
                }
            }
        }
        public bool IsBasic
        {
            get => !IsGet ? GetParametrs<bool>("IsBasic", this.GetType()) : isBasic;
            set
            {
                isBasic = value;
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("IsBasic", value);
                }
            }
        }
        public string Commit
        {
            get => !IsGet ? GetParametrs<string>("Commit", this.GetType()) : commit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("Commit", value is null ? DBNull.Value : value);
                }
                commit = value;
            }
        }
        public bool? IsTerm
        {
            get => !IsGet ? GetParametrs<bool?>("IsTerm", this.GetType()) : isTerm;
            set
            {
                isTerm = value;
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("IsTerm", value is null ? DBNull.Value : value);
                }
            }
        }
        public DateTime? DatePay
        {
            get => !IsGet ? GetParametrs<DateTime?>("DatePay", this.GetType()) : datePay;
            set
            {
                datePay = value;
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("DatePay", value is null ? DBNull.Value : value);
                }
            }
        }
        public decimal? OverflowSum
        {
            get => !IsGet ? GetParametrs<decimal?>("OverflowSum", this.GetType()) : overflowSum;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("OverflowSum", value is null ? DBNull.Value : value);
                }
                overflowSum = value;
            }
        }
        public decimal BeginSum
        {
            get => !IsGet ? GetParametrs<decimal>("BeginSum", this.GetType()) : beginSum;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("BeginSum", value);
                }
                beginSum = value;
            }
        }
        public decimal RemnantSum
        {
            get => !IsGet ? GetParametrs<decimal>("RemnantSum", this.GetType()) : remnantSum;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<LoanPayments>("RemnantSum", value);
                }
                remnantSum = value;
            }
        }

        [XmlIgnore]
        public Credit Credit { get; private set; }
        [XmlIgnore]
        public Assets Assets { get; private set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "Id", "OverflowSum", "BeginSum", "RemnantSum" }.Contains(param))
                return;
            else if (new[] { "Commit", "IdAssets" }.Contains(param))
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
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(commit) ? DBNull.Value : commit),
                        new MySqlParameter("@Sum", sum),
                        new MySqlParameter("@DatePay", datePay is null ? DBNull.Value : datePay)
                    });
            }
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.DeleteModel<LoanPayments>(this.Id);
            }
            else
            {
                base.DeleteModel<LoanPayments>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<LoanPayments>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<LoanPayments>(parametrs, Id, WhereCollection);
            }
        }
    }
}

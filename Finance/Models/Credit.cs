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
    public class Credit : DBModel
    {
        private int id;
        private DateTime startDate;
        private DateTime endDate;
        private int countYear;
        private string purpose;
        private decimal sum;
        private decimal percent;
        private string commit;
        private decimal monthSum;
        private decimal startSum;
        private int idStatusCredit;
        private decimal overpaymentSum;
        private DateTime realEndDate;
        private decimal overpaymentSumReal;
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
                    SetParametrs<Credit>("Id", value);
                }
                id = value;
            }
        }
        public DateTime StartDate
        {
            get => !IsGet ? GetParametrs<DateTime>("StartDate", this.GetType()) : startDate;
            set
            {
                startDate = value;
                if (!IsGet)
                {
                    SetParametrs<Credit>("StartDate", value);
                }
            }
        }
        public DateTime EndDate
        {
            get => !IsGet ? GetParametrs<DateTime>("EndDate", this.GetType()) : endDate;
            set
            {
                endDate = value;
                if (!IsGet)
                {
                    SetParametrs<Credit>("EndDate", value);
                }
            }
        }
        public int CountYear
        {
            get => Math.Abs(!IsGet ? GetParametrs<int>("CountYear", this.GetType()) : countYear);
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("CountYear", value);
                }
                countYear = value;
            }
        }
        public string Purpose
        {
            get => !IsGet ? GetParametrs<string>("Purpose", this.GetType()) : purpose;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("Purpose", value is null ? DBNull.Value : value);
                }
                purpose = value;
            }
        }
        public decimal Sum
        {
            get => !IsGet ? GetParametrs<decimal>("Sum", this.GetType()) : sum;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("Sum", value);
                }
                sum = value;
            }
        }
        public decimal Percent
        {
            get => !IsGet ? GetParametrs<decimal>("Percent", this.GetType()) : percent;
            set
            {
                percent = value;
                if (!IsGet)
                {
                    SetParametrs<Credit>("Percent", value);
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
                    SetParametrs<Credit>("Commit", value is null ? DBNull.Value : value);
                }
                commit = value;
            }
        }
        public decimal MonthSum
        {
            get => !IsGet ? GetParametrs<decimal>("MonthSum", this.GetType()) : monthSum;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("MonthSum", value);
                }
                monthSum = value;
            }
        }
        public decimal StartSum
        {
            get => !IsGet ? GetParametrs<decimal>("StartSum", this.GetType()) : startSum;
            set
            {
                startSum = value;
                if (!IsGet)
                {
                    SetParametrs<Credit>("StartSum", value);
                }
            }
        }
        public int IdStatusCredit
        {
            get => !IsGet ? GetParametrs<int>("IdStatusCredit", this.GetType()) : idStatusCredit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("IdStatusCredit", value);
                }

                CreditStatus = GetModel<CreditStatus>(value);
                idStatusCredit = value;
            }
        }
        public decimal OverpaymentSum
        {
            get => !IsGet ? GetParametrs<decimal>("OverpaymentSum", this.GetType()) : overpaymentSum;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("OverpaymentSum", value);
                }
                overpaymentSum = value;
            }
        }
        public DateTime RealEndDate
        {
            get => !IsGet ? GetParametrs<DateTime>("RealEndDate", this.GetType()) : realEndDate;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("RealEndDate", value);
                }
                realEndDate = value;
            }
        }
        public decimal OverpaymentSumReal
        {
            get => !IsGet ? GetParametrs<decimal>("OverpaymentSumReal", this.GetType()) : overpaymentSumReal;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("OverpaymentSumReal", value);
                }
                overpaymentSumReal = value;
            }
        }
        public int? IdUser
        {
            get => !IsGet ? GetParametrs<int?>("IdStatusCredit", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Credit>("IdStatusCredit", value is null ? DBNull.Value : value);
                }

                User = value is null ? null : GetModel<Users>(value);
                idUser = value;
            }
        }

        [XmlIgnore]
        public Users User { get; private set; }
        [XmlIgnore]
        public CreditStatus CreditStatus { get; private set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "StartDate", "EndDate", "Percent", "StartSum" }.Contains(param))
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_credit(@StartDate,@EndDate,@Purpoce,@Percent,@Commit,@StartSum,'{idStatusCredit}','{id}',@IdUser)", new[]
                    {
                        new MySqlParameter("@StartDate", startDate),
                        new MySqlParameter("@EndDate", endDate),
                        new MySqlParameter("@Purpoce", String.IsNullOrEmpty(purpose) ? DBNull.Value : purpose),
                        new MySqlParameter("@Percent", percent),
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(commit) ? DBNull.Value : commit),
                        new MySqlParameter("@StartSum", startSum),
                        new MySqlParameter("@IdUser", idUser)
                    },2);
            }
            else if (new[] { "Purpose", "Commit", "IdStatusCredit" }.Contains(param))
                base.SetParametrs<T>(param, value, id);
            else
                return;
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.DeleteModel<Credit>(this.Id);
            }
            else
            {
                base.DeleteModel<Credit>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Credit>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Credit>(parametrs, Id, WhereCollection);
            }
        }
    }
}

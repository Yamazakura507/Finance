using MySqlConnector;
using Finance.Classes;

namespace Finance.Models
{
    public class Credit : Abstract.AbstractModelStatus<Credit>
    {
        private DateTime startDate;
        private DateTime endDate;
        private int countYear;
        private decimal sum;
        private decimal percent;
        private decimal monthSum;
        private decimal startSum;
        private int idStatusCredit;
        private decimal overpaymentSum;
        private DateTime realEndDate;
        private decimal overpaymentSumReal;

        public DateTime StartDate
        {
            get => !IsGet ? GetParametrs<DateTime>("StartDate", this.GetType()) : startDate;
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("StartDate", value);
                    }
                }
            }
        }
        public DateTime EndDate
        {
            get => !IsGet ? GetParametrs<DateTime>("EndDate", this.GetType()) : endDate;
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("EndDate", value);
                    }
                }
            }
        }
        public int CountYear
        {
            get => Math.Abs(!IsGet ? GetParametrs<int>("CountYear", this.GetType()) : countYear);
            set
            {
                if (countYear != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("CountYear", value);
                    }
                    countYear = value;
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
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("Sum", value);
                    }
                    sum = value;
                }
            }
        }
        public decimal Percent
        {
            get => !IsGet ? GetParametrs<decimal>("Percent", this.GetType()) : percent;
            set
            {
                if (percent != value)
                {
                    percent = value;
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("Percent", value);
                    }
                }
            }
        }
        public decimal MonthSum
        {
            get => !IsGet ? GetParametrs<decimal>("MonthSum", this.GetType()) : monthSum;
            set
            {
                if (monthSum != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("MonthSum", value);
                    }
                    monthSum = value;
                }
            }
        }
        public decimal StartSum
        {
            get => !IsGet ? GetParametrs<decimal>("StartSum", this.GetType()) : startSum;
            set
            {
                if (startSum != value)
                {
                    startSum = value;
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("StartSum", value);
                    }
                }
            }
        }
        public int IdStatusCredit
        {
            get => !IsGet ? GetParametrs<int>("IdStatusCredit", this.GetType()) : idStatusCredit;
            set
            {
                if (idStatusCredit != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("IdStatusCredit", value);
                    }

                    CreditStatus = GetModel<CreditStatus>(value);
                    idStatusCredit = value;
                }
            }
        }
        public decimal OverpaymentSum
        {
            get => !IsGet ? GetParametrs<decimal>("OverpaymentSum", this.GetType()) : overpaymentSum;
            set
            {
                if (overpaymentSum != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("OverpaymentSum", value);
                    }
                    overpaymentSum = value;
                }
            }
        }
        public DateTime RealEndDate
        {
            get => !IsGet ? GetParametrs<DateTime>("RealEndDate", this.GetType()) : realEndDate;
            set
            {
                if (realEndDate != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("RealEndDate", value);
                    }
                    realEndDate = value;
                }
            }
        }
        public decimal OverpaymentSumReal
        {
            get => !IsGet ? GetParametrs<decimal>("OverpaymentSumReal", this.GetType()) : overpaymentSumReal;
            set
            {
                if (overpaymentSumReal != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Credit>("OverpaymentSumReal", value);
                    }
                    overpaymentSumReal = value;
                }
            }
        }

        public CreditStatus CreditStatus { get; private set; }

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
                        new MySqlParameter("@Purpoce", String.IsNullOrEmpty(name) ? DBNull.Value : name),
                        new MySqlParameter("@Percent", percent),
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(description) ? DBNull.Value : description),
                        new MySqlParameter("@StartSum", startSum),
                        new MySqlParameter("@IdUser", idUser)
                    },2);
            }
            else if (new[] { "Purpose", "Description", "IdStatusCredit" }.Contains(param))
                base.SetParametrs<T>(param, value, id);
            else
                return;
        }
    }
}

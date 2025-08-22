using MySqlConnector;
using Finance.Classes;

namespace Finance.Models
{
    public class Debtor : Abstract.AbstractModelStatus<Debtor>
    {
        private int idStatusDebtor;
        private decimal sum;

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
                        SetParametrs<Debtor>("Sum", value);
                    }
                }
            }
        }
        public int IdStatusDebtor
        {
            get => !IsGet ? GetParametrs<int>("IdStatusDebtor", this.GetType()) : idStatusDebtor;
            set
            {
                if (idStatusDebtor != value)
                {
                    idStatusDebtor = value;
                    if (!IsGet)
                    {
                        SetParametrs<Debtor>("IdStatusDebtor", value);
                    }

                    DebtorStatus = GetModel<DebtorStatus>(value);
                }
            }
        }

        public DebtorStatus DebtorStatus { get; private set; }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "Sum", "IdStatusDebtor" }.Contains(param))
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_debtor('{id}','{name}',@Sum,'{idStatusDebtor}',@Commit,'{idUser}')", new[]
                    {
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(description) ? DBNull.Value : description),
                        new MySqlParameter("@Sum", sum)
                    });
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
                    ms.ExecSql($"CALL delete_debtor('{id}')");
                }
                else
                {
                    ms.ExecSql($"CALL delete_debtor('{Id}')");
                }
            }
        }
    }
}

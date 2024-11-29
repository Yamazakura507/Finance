using MySqlConnector;
using Finance.Classes;

namespace Finance.Models
{
    public class Debtor : DBModel
    {
        private int id;
        private int idStatusDebtor;
        private int idUser;
        private decimal sum;
        private string name;
        private string commit;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Debtor>("Id", value);
                }
                id = value;
            }
        }
        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {       
                    if (!IsGet)
                    {
                        SetParametrs<Debtor>("Name", value);
                    }
                    name = value;
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
                    SetParametrs<Debtor>("Sum", value);
                }
            }
        }
        public int IdStatusDebtor
        {
            get => !IsGet ? GetParametrs<int>("IdStatusDebtor", this.GetType()) : idStatusDebtor;
            set
            {
                idStatusDebtor = value;
                if (!IsGet)
                {
                    SetParametrs<Debtor>("IdStatusDebtor", value);
                }

                DebtorStatus = GetModel<DebtorStatus>(value);
            }
        }
        public string Commit
        {
            get => !IsGet ? GetParametrs<string>("Commit", this.GetType()) : commit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Debtor>("Commit", String.IsNullOrEmpty(value) ? DBNull.Value : value);
                }
                commit = value;
            }
        }
        public int IdUser
        {
            get => !IsGet ? GetParametrs<int>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Debtor>("IdUser", value);
                }

                User = GetModel<Users>(value);
                idUser = value;
            }
        }

        public DebtorStatus DebtorStatus { get; private set; }
        public Users User { get; private set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "Sum", "IdStatusDebtor" }.Contains(param))
            {
                CheckPolice(false, typeof(T));

                using (var ms = new Mysql())
                    ms.ExecSql($"SELECT ins_upd_debtor('{id}','{name}',@Sum,'{idStatusDebtor}',@Commit,'{idUser}')", new[]
                    {
                        new MySqlParameter("@Commit", String.IsNullOrEmpty(commit) ? DBNull.Value : commit),
                        new MySqlParameter("@Sum", sum)
                    });
            }
            else if (new[] { "Name", "Commit" }.Contains(param))
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

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Debtor>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Debtor>(parametrs, Id, WhereCollection);
            }
        }
    }
}

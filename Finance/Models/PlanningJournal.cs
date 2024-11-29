using Finance.Classes;

namespace Finance.Models
{
    public class PlanningJournal : DBModel
    {
        private int id;
        private string name;
        private string commit;
        private decimal targetAmount;
        private int idUser;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<PlanningJournal>("Id", value);
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
                        SetParametrs<PlanningJournal>("Name", value);
                    }
                    name = value;
            }
        }

        public string Commit
        {
            get => !IsGet ? GetParametrs<string>("Commit", this.GetType()) : commit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<PlanningJournal>("Commit", value);
                }
                commit = value;
            }
        }

        public decimal TargetAmount
        {
            get => !IsGet ? GetParametrs<decimal>("TargetAmount", this.GetType()) : targetAmount;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<PlanningJournal>("TargetAmount", value);
                }
                targetAmount = value;
            }
        }

        public int IdUser
        {
            get => !IsGet ? GetParametrs<int>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<PlanningJournal>("IdUser", value);
                }

                Users = GetModel<Users>(value);
                idUser = value;
            }
        }

        public Users Users { get; private set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            base.SetParametrs<T>(param, value, id);
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.DeleteModel<PlanningJournal>(this.Id);
            }
            else
            {
                base.DeleteModel<PlanningJournal>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<PlanningJournal>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<PlanningJournal>(parametrs, Id, WhereCollection);
            }
        }
    }
}

using Finance.Classes;

namespace Finance.Models
{
    public class Jobs : DBModel
    {
        private int id;
        private string name;
        private string commit;
        private decimal sumResult;
        private bool isDone;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Jobs>("Id", value);
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
                        SetParametrs<Jobs>("Name", value);
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
                    SetParametrs<Jobs>("Commit", value);
                }
                commit = value;
            }
        }

        public decimal SumResult
        {
            get => !IsGet ? GetParametrs<decimal>("SumResult", this.GetType()) : sumResult;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Jobs>("SumResult", value);
                }
                sumResult = value;
            }
        }
        public bool IsDone
        {
            get => !IsGet ? GetParametrs<bool>("IsDone", this.GetType()) : isDone;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Jobs>("IsDone", value);
                }
                isDone = value;
            }
        }

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
                base.DeleteModel<Jobs>(this.Id);
            }
            else
            {
                base.DeleteModel<Jobs>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Jobs>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Jobs>(parametrs, Id, WhereCollection);
            }
        }
    }
}

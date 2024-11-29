using Finance.Classes;

namespace Finance.Models
{
    public class Quadrants : DBModel
    {
        private int id;
        private string initial;
        private string name;
        private string commit;
        private string downLimit;
        private byte[] icon;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Quadrants>("Id", value);
                }
                id = value;
            }
        }
        public string Initial
        {
            get => !IsGet ? GetParametrs<string>("Initial", this.GetType()) : initial;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Quadrants>("Initial", value);
                }
                initial = value;
            }
        }
        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {       
                    if (!IsGet)
                    {
                        SetParametrs<Quadrants>("Name", value);
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
                    SetParametrs<Quadrants>("Commit", value);
                }
                commit = value;
            }
        }
        public string DownLimit
        {
            get => !IsGet ? GetParametrs<string>("DownLimit", this.GetType()) : downLimit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Quadrants>("DownLimit", value);
                }
                downLimit = value;
            }
        }
        public byte[] Icon
        {
            get => !IsGet ? GetParametrs<byte[]>("Icon", this.GetType()) : icon;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Quadrants>("Icon", value);
                }
                icon = value;
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
                base.DeleteModel<Quadrants>(this.Id);
            }
            else
            {
                base.DeleteModel<Quadrants>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Quadrants>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Quadrants>(parametrs, Id, WhereCollection);
            }
        }
    }
}

using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class GroupingAssets : DBModel
    {
        private int id;
        private int idAssets;
        private int idGroupAssets ;
        private int idDate ;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingAssets>("Id", value);
                }
                id = value;
            }
        }

        public int IdAssets
        {
            get => !IsGet ? GetParametrs<int>("IdAssets", this.GetType()) : idAssets;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingAssets>("IdAssets", value);
                }

                Assets = GetModel<Assets>(value);
                idAssets = value;
            }
        }

        public int IdGroupAssets
        {
            get => !IsGet ? GetParametrs<int>("IdGroupAssets", this.GetType()) : idGroupAssets ;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingAssets>("IdGroupAssets", value);
                }

                AssetsGroup = GetModel<AssetsGroup>(value);
                idGroupAssets  = value;
            }
        }

        public int IdDate
        {
            get => !IsGet ? GetParametrs<int>("IdDate", this.GetType()) : idDate;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingAssets>("IdDate", value);
                }

                DateJournal = GetModel<DateJournal>(value);
                idDate = value;
            }
        }

        [XmlIgnore]
        public Assets Assets { get; private set; }
        [XmlIgnore]
        public AssetsGroup AssetsGroup { get; private set; }
        [XmlIgnore]
        public DateJournal DateJournal { get; private set; }

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
                base.DeleteModel<GroupingAssets>(this.Id);
            }
            else
            {
                base.DeleteModel<GroupingAssets>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<GroupingAssets>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<GroupingAssets>(parametrs, Id, WhereCollection);
            }
        }
    }
}

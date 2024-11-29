using Finance.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Models
{
    public class TypeObject : DBModel
    {
        private int id;
        private string name;
        private string objectName;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TypeObject>("Id", value);
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
                    SetParametrs<TypeObject>("Name", value);
                }
                name = value;
            }
        }

        public string ObjectName
        {
            get => !IsGet ? GetParametrs<string>("ObjectName", this.GetType()) : objectName;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TypeObject>("ObjectName", value);
                }
                objectName = value;
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
                base.DeleteModel<TypeObject>(this.Id);
            }
            else
            {
                base.DeleteModel<TypeObject>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<TypeObject>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<TypeObject>(parametrs, Id, WhereCollection);
            }
        }
    }
}

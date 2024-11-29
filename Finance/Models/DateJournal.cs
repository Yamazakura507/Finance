using Finance.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Models
{
    public class DateJournal : DBModel
    {
        private int id;
        private int name;
        private int month;
        private bool isCreateData;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<DateJournal>("Id", value);
                }
                id = value;
            }
        }

        public int Year
        {
            get => !IsGet ? GetParametrs<int>("Year", this.GetType()) : name;
            set
            {       
                    if (!IsGet)
                    {
                        SetParametrs<DateJournal>("Year", value);
                    }
                    name = value;
            }
        }

        public int Month
        {
            get => !IsGet ? GetParametrs<int>("Month", this.GetType()) : month;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<DateJournal>("Month", value);
                }
                month = value;
            }
        }

        public bool IsCreateData
        {
            get => !IsGet ? GetParametrs<bool>("IsCreateData", this.GetType()) : isCreateData;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<DateJournal>("IsCreateData", value);
                }
                isCreateData = value;
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
                base.DeleteModel<DateJournal>(this.Id);
            }
            else
            {
                base.DeleteModel<DateJournal>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<DateJournal>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<DateJournal>(parametrs, Id, WhereCollection);
            }
        }
    }
}

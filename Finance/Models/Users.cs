using Finance.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Models
{
    public class Users : DBModel
    {
        private int id;
        private string email;
        private string login;
        private string password;
        private bool isEducation;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set 
            {
                if (!IsGet)
                {
                    SetParametrs<Users>("Id", value);
                }
                id = value;
            } 
        }

        public string Login
        {
            get => !IsGet ? GetParametrs<string>("Login", this.GetType()) : login;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Users>("Login", value);
                }
                login = value;
            }
        }

        public string Password
        {
            get => !IsGet ? GetParametrs<string>("Password", this.GetType()) : password;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Users>("Password", value);
                }
                password = value;
            }
        }

        public string Email
        {
            get => !IsGet ? GetParametrs<string>("Email", this.GetType()) : email;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Users>("Email", String.IsNullOrEmpty(value) ? DBNull.Value : value);
                }
                email = value;
            }
        }

        public bool IsEducation
        {
            get => !IsGet ? GetParametrs<bool>("IsEducation", this.GetType()) : isEducation;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Users>("IsEducation",value);
                }
                isEducation = value;
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
                base.DeleteModel<Users>(this.Id);
            }
            else
            {
                base.DeleteModel<Users>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Users>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Users>(parametrs, Id, WhereCollection);
            }
        }
    }
}

using Finance.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Models
{
    public class AssetsGroup : DBModel
    {
        private int id;
        private int? idUser;
        private string name;
        private string commit;
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
                    SetParametrs<AssetsGroup>("Id", value);
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
                    SetParametrs<AssetsGroup>("Name", value);
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
                    SetParametrs<AssetsGroup>("Commit", value is null ? DBNull.Value : value);
                }
                commit = value;
            }
        }

        public byte[] Icon
        {
            get => !IsGet ? GetParametrs<byte[]>("Icon", this.GetType()) : icon;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<AssetsGroup>("Icon", value is null ? DBNull.Value : value);
                }
                icon = value;
            }
        }

        public int? IdUser
        {
            get => !IsGet ? GetParametrs<int?>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<AssetsGroup>("IdUser", value is null ? DBNull.Value : value);
                }
                idUser = value;
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
                base.DeleteModel<AssetsGroup>(this.Id);
            }
            else
            {
                base.DeleteModel<AssetsGroup>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<AssetsGroup>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<AssetsGroup>(parametrs, Id, WhereCollection);
            }
        }
    }
}

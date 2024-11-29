using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class MoneyTransfers : DBModel
    {
        private int id;
        private int idAssets;
        private int idStatusTransfer;
        private DateTime timeTransfer;
        private decimal sum;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<MoneyTransfers>("Id", value);
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
                        SetParametrs<MoneyTransfers>("IdAssets", value);
                    }

                    Assets = GetModel<Assets>(value);
                    idAssets = value;
            }
        }

        public int IdStatusTransfer
        {
            get => !IsGet ? GetParametrs<int>("IdStatusTransfer", this.GetType()) : idStatusTransfer ;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<MoneyTransfers>("IdStatusTransfer", value);
                }

                TransferStatus = GetModel<TransferStatus>(value);
                idStatusTransfer  = value;
            }
        }

        public DateTime TimeTransfer
        {
            get => !IsGet ? GetParametrs<DateTime>("TimeTransfer", this.GetType()) : timeTransfer;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<MoneyTransfers>("TimeTransfer", value);
                }

                timeTransfer = value;
            }
        }

        public decimal Sum
        {
            get => !IsGet ? GetParametrs<decimal>("Sum", this.GetType()) : sum;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<MoneyTransfers>("Sum", value);
                }

                sum = value;
            }
        }


        [XmlIgnore]
        public Assets Assets { get; private set; }
        [XmlIgnore]
        public TransferStatus TransferStatus { get; private set; }

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
                base.DeleteModel<MoneyTransfers>(this.Id);
            }
            else
            {
                base.DeleteModel<MoneyTransfers>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<MoneyTransfers>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<MoneyTransfers>(parametrs, Id, WhereCollection);
            }
        }
    }
}

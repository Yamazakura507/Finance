
namespace Finance.Models
{
    public class Scalping : Abstract.AbstractModelStatus<Scalping>
    {
        private decimal margin;
        private decimal marginBeforeTax;
        private decimal commissing;


        public decimal Margin
        {
            get => !IsGet ? GetParametrs<decimal>("Margin", this.GetType()) : margin;
            set
            {
                if (margin != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Scalping>("Margin", value);
                    }
                    margin = value;
                }
            }
        }

        public decimal MarginBeforeTax
        {
            get => !IsGet ? GetParametrs<decimal>("MarginBeforeTax", this.GetType()) : marginBeforeTax;
            set
            {
                if (marginBeforeTax != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Scalping>("MarginBeforeTax", value);
                    }
                    marginBeforeTax = value;
                }
            }
        }

        public decimal Commissing
        {
            get => !IsGet ? GetParametrs<decimal>("Commissing", this.GetType()) : commissing;
            set
            {
                if (commissing != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Scalping>("Commissing", value);
                    }
                    commissing = value;
                }
            }
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "Name", "Description" }.Contains(param))
                base.SetParametrs<T>(param, value, id);
            else
                return;
        }
    }
}

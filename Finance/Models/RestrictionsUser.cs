using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class RestrictionsUser : Abstract.AbstractModel<RestrictionsUser>
    {
        private int idRestrictions;

        public int IdRestrictions
        {
            get => !IsGet ? GetParametrs<int>("IdRestrictions", this.GetType()) : idRestrictions;
            set
            {
                if (idRestrictions != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<RestrictionsUser>("IdRestrictions", value);
                    }

                    Restriction = GetModel<Restrictions>(value);
                    idRestrictions = value;
                }
            }
        }

        public Restrictions Restriction { get; private set; }
    }
}

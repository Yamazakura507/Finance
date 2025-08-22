using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class QuadrantsUser : Abstract.AbstractModel<QuadrantsUser>
    {
        private int idQuadrants;

        public int IdQuadrants
        {
            get => !IsGet ? GetParametrs<int>("IdQuadrants", this.GetType()) : idQuadrants;
            set
            {
                if (idQuadrants != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<QuadrantsUser>("IdQuadrants", value);
                    }

                    Quadrants = GetModel<Quadrants>(value);
                    idQuadrants = value;
                }
            }
        }

        public Quadrants Quadrants { get; private set; }
    }
}

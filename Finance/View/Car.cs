
using Finance.Classes;

namespace Finance.View
{
    public class Car : Abstract.AbstractViewModel
    {
        private int idTypeCar;
        private int idColor;
        private string make;


        public string Make
        {
            get => make;
            set
            {
                if (make != value)
                {
                    ImgManufactureLink = CarInfo.LinkImageMakeCar(value);
                    make = value;
                }
            }
        }

        public string Model
        {
            get;
            set;
        }

        public string VIN
        {
            get;
            set;
        }

        public int IdTypeCar
        {
            get => idTypeCar;
            set
            {
                if (idTypeCar != value)
                {
                    TypeCar = GetModel<TypeCar>(value);
                    idTypeCar = value;
                }
            }
        }

        public int YearOfManufacture
        {
            get;
            set;
        }

        public int? Mileage
        {
            get;
            set;
        }

        public int IdColor
        {
            get => idColor;
            set
            {
                if (idColor != value)
                {
                    LibColors = GetModel<LibColors>(value);
                    idColor = value;
                }
            }
        }

        public string StateNumber
        {
            get;
            set;
        }

        public decimal? Price
        {
            get;
            set;
        }

        public TypeCar TypeCar { get; private set; }
        public LibColors LibColors { get; private set; }

        public Uri ImgManufactureLink { get; set; }
    }
}

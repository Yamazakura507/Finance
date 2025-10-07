
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.Models
{
    public class Car : Abstract.AbstractModel<Car>, INotifyPropertyChanged
    {
        private string make;
        private string model;
        private string vin;
        private string engineModel;
        private string engineNumber;
        private string chassisNumber;
        private string bodyNumber;
        private string ptsSeries;
        private string ptsNumber;
        private string ptsOrgIssuedDoc;
        private string stsSeries;
        private string stsNumber;
        private string stsOrgIssuedDoc;
        private string stateNumber;
        private decimal enginePower;
        private decimal workingVolume;
        private decimal? price;
        private int idTypeCar;
        private int idColor;
        private int yearOfManufacture;
        private int? mileage;
        private DateTime ptsDateReg;
        private DateTime stsDateReg;
        private View.LibColors color;


        public string Make
        {
            get => !IsGet ? GetParametrs<string>("Make", this.GetType()) : make;
            set
            {
                if (make != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("Make", value);
                    }
                    make = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Model
        {
            get => !IsGet ? GetParametrs<string>("Model", this.GetType()) : model;
            set
            {
                if (model != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("Model", value);
                    }
                    model = value;
                    OnPropertyChanged();
                }
            }
        }

        public string VIN
        {
            get => !IsGet ? GetParametrs<string>("VIN", this.GetType()) : vin;
            set
            {
                if (vin != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("VIN", value);
                    }
                    vin = value;
                }
            }
        }

        public int IdTypeCar
        {
            get => !IsGet ? GetParametrs<int>("IdTypeCar", this.GetType()) : idTypeCar;
            set
            {
                if (idTypeCar != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("IdTypeCar", value);
                    }

                    TypeCar = GetModel<View.TypeCar>(value);
                    idTypeCar = value;
                }
            }
        }

        public int YearOfManufacture
        {
            get => !IsGet ? GetParametrs<int>("YearOfManufacture", this.GetType()) : yearOfManufacture;
            set
            {
                if (yearOfManufacture != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("YearOfManufacture", value);
                    }
                    yearOfManufacture = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? Mileage
        {
            get => !IsGet ? GetParametrs<int?>("Mileage", this.GetType()) : mileage;
            set
            {
                if (mileage != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("Mileage", value);
                    }
                    mileage = value;
                }
            }
        }

        public decimal EnginePower
        {
            get => !IsGet ? GetParametrs<decimal>("EnginePower", this.GetType()) : enginePower;
            set
            {
                if (enginePower != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("EnginePower", value);
                    }
                    enginePower = value;
                }
            }
        }

        public decimal WorkingVolume
        {
            get => !IsGet ? GetParametrs<decimal>("WorkingVolume", this.GetType()) : workingVolume;
            set
            {
                if (workingVolume != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("WorkingVolume", value);
                    }
                    workingVolume = value;
                }
            }
        }

        public int IdColor
        {
            get => !IsGet ? GetParametrs<int>("IdColor", this.GetType()) : idColor;
            set
            {
                if (idColor != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("IdColor", value);
                    }

                    LibColors = GetModel<View.LibColors>(value);
                    idColor = value;
                }
            }
        }

        public string EngineModel
        {
            get => !IsGet ? GetParametrs<string>("EngineModel", this.GetType()) : engineModel;
            set
            {
                if (engineModel != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("EngineModel", value);
                    }
                    engineModel = value;
                }
            }
        }

        public string EngineNumber
        {
            get => !IsGet ? GetParametrs<string>("EngineNumber", this.GetType()) : engineNumber;
            set
            {
                if (engineNumber != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("EngineNumber", value);
                    }
                    engineNumber = value;
                }
            }
        }

        public string ChassisNumber
        {
            get => !IsGet ? GetParametrs<string>("ChassisNumber", this.GetType()) : chassisNumber;
            set
            {
                if (chassisNumber != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("ChassisNumber", value);
                    }
                    chassisNumber = value;
                }
            }
        }

        public string BodyNumber
        {
            get => !IsGet ? GetParametrs<string>("BodyNumber", this.GetType()) : bodyNumber;
            set
            {
                if (bodyNumber != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("BodyNumber", value);
                    }
                    bodyNumber = value;
                }
            }
        }

        public string PTSSeries
        {
            get => !IsGet ? GetParametrs<string>("PTSSeries", this.GetType()) : ptsSeries;
            set
            {
                if (ptsSeries != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("PTSSeries", value);
                    }
                    ptsSeries = value;
                }
            }
        }

        public string PTSNumber
        {
            get => !IsGet ? GetParametrs<string>("PTSNumber", this.GetType()) : ptsNumber;
            set
            {
                if (ptsNumber != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("PTSNumber", value);
                    }
                    ptsNumber = value;
                }
            }
        }

        public string PTSOrgIssuedDoc
        {
            get => !IsGet ? GetParametrs<string>("PTSOrgIssuedDoc", this.GetType()) : ptsOrgIssuedDoc;
            set
            {
                if (ptsOrgIssuedDoc != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("PTSOrgIssuedDoc", value);
                    }
                    ptsOrgIssuedDoc = value;
                }
            }
        }

        public DateTime PTSDateReg
        {
            get => !IsGet ? GetParametrs<DateTime>("PTSDateReg", this.GetType()) : ptsDateReg;
            set
            {
                if (ptsDateReg != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("PTSDateReg", value);
                    }
                    ptsDateReg = value;
                }
            }
        }

        public string STSSeries
        {
            get => !IsGet ? GetParametrs<string>("STSSeries", this.GetType()) : stsSeries;
            set
            {
                if (stsSeries != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("STSSeries", value);
                    }
                    stsSeries = value;
                }
            }
        }

        public string STSNumber
        {
            get => !IsGet ? GetParametrs<string>("STSNumber", this.GetType()) : stsNumber;
            set
            {
                if (stsNumber != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("STSNumber", value);
                    }
                    stsNumber = value;
                }
            }
        }

        public string STSOrgIssuedDoc
        {
            get => !IsGet ? GetParametrs<string>("STSOrgIssuedDoc", this.GetType()) : stsOrgIssuedDoc;
            set
            {
                if (stsOrgIssuedDoc != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("STSOrgIssuedDoc", value);
                    }
                    stsOrgIssuedDoc = value;
                }
            }
        }

        public DateTime STSDateReg
        {
            get => !IsGet ? GetParametrs<DateTime>("STSDateReg", this.GetType()) : stsDateReg;
            set
            {
                if (stsDateReg != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("STSDateReg", value);
                    }
                    stsDateReg = value;
                }
            }
        }

        public string StateNumber
        {
            get => !IsGet ? GetParametrs<string>("StateNumber", this.GetType()) : stateNumber;
            set
            {
                if (stateNumber != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("StateNumber", value);
                    }
                    stateNumber = value;
                }
            }
        }

        public decimal? Price
        {
            get => !IsGet ? GetParametrs<decimal?>("Price", this.GetType()) : price;
            set
            {
                if (price != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Car>("Price", value is null ? DBNull.Value : value);
                    }
                    price = value;
                }
            }
        }

        public View.TypeCar TypeCar { get; private set; }
        public View.LibColors LibColors 
        { 
            get => color;
            private set
            {
                if (idColor != value.Id)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

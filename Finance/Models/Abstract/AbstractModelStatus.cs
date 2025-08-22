
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.Models.Abstract
{
    public abstract class AbstractModelStatus<T> : AbstractModel<T>, INotifyPropertyChanged
    {
        protected string name;
        protected string description;

        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {
                if (name != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<T>("Name", value);
                    }
                    name = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => !IsGet ? GetParametrs<string>("Description", this.GetType()) : description;
            set
            {
                if (description != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<T>("Description", value is null ? DBNull.Value : value);
                    }
                    description = value;
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

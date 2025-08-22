using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.View.Abstract
{
    public abstract class AbstractViewStatus<T> : AbstractViewModel, INotifyPropertyChanged
    {
        protected string name;


        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

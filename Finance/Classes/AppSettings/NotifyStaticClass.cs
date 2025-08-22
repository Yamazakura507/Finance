using System.ComponentModel;

namespace Finance.Classes.AppSettings
{
    public class NotifyStaticClass<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private T objectNotify = default(T);
        public T ObjectNotify
        {
            get { return objectNotify; }
            set
            {
                if (!objectNotify.Equals(value))
                {
                    objectNotify = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ObjectNotify)));
                }
            }
        }
    }
}

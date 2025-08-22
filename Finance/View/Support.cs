
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.View
{
    public class Support : Abstract.AbstractViewModel, INotifyPropertyChanged
    {
        private bool isReadAnswer;

        public string AppealMessage
        {
            get;
            set;
        }
        public DateTime DateOfAccess
        {
            get;
            set;
        }
        public bool IsAnswer
        {
            get;
            set;
        }
        public string AnswerMessage
        {
            get;
            set;
        }
        public bool IsReadAnswer
        {
            get => isReadAnswer;
            set
            {
                if (isReadAnswer != value)
                {
                    isReadAnswer = value;

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

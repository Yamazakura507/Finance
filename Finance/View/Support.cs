using Finance.Classes;
using Finance.Models;

namespace Finance.View
{
    public class Support
    {
        public int Id
        {
            get;
            set;
        }
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
            get;
            set;
        }
    }
}

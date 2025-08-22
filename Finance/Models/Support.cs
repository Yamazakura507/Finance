using Finance.Classes;
using Finance.Classes.AppSettings;

namespace Finance.Models
{
    public class Support : Abstract.AbstractModel<Support>
    {
        private string appealMessage;
        private string answerMessage;
        private DateTime dateOfAccess;
        private bool isAnswer;
        private bool isReadAnswer;


        public string AppealMessage
        {
            get => !IsGet ? GetParametrs<string>("AppealMessage", this.GetType()) : appealMessage;
            set
            {
                if (appealMessage != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Support>("AppealMessage", value);
                    }
                    appealMessage = value;
                }
            }
        }
        public DateTime DateOfAccess
        {
            get => !IsGet ? GetParametrs<DateTime>("DateOfAccess", this.GetType()) : dateOfAccess;
            set
            {
                if (dateOfAccess != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Support>("DateOfAccess", value);
                    }
                    dateOfAccess = value;
                }
            }
        }
        public bool IsAnswer
        {
            get => !IsGet ? GetParametrs<bool>("IsAnswer", this.GetType()) : isAnswer;
            set
            {
                if (isAnswer != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Support>("IsAnswer", value);
                    }
                    isAnswer = value;
                }
            }
        }
        public string AnswerMessage
        {
            get => !IsGet ? GetParametrs<string>("AnswerMessage", this.GetType()) : answerMessage;
            set
            {
                if (answerMessage != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Support>("AnswerMessage", String.IsNullOrEmpty(value) ? DBNull.Value : value);

                        if (!String.IsNullOrEmpty(value))
                        {
                            if (InfoAccount.IsAdmin && !isAnswer) App.MyAppShell.SupportAnswerMail(InfoAccount.User.Email, new Dictionary<string, string>() { { "@Login", InfoAccount.User.Login }, { "@SupportMessage", appealMessage }, { "@Answer", value } });

                            SetParametrs<Support>("IsAnswer", true);
                            SetParametrs<Support>("IsReadAnswer", false);
                            isAnswer = true;
                            isReadAnswer = false;
                        }
                        else
                        {
                            SetParametrs<Support>("IsAnswer", false);
                            isAnswer = false;
                        }
                    }
                    answerMessage = value;
                }
            }
        }
        public bool IsReadAnswer
        {
            get => !IsGet ? GetParametrs<bool>("IsReadAnswer", this.GetType()) : isReadAnswer;
            set
            {
                if (isReadAnswer != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Support>("IsReadAnswer", value);
                    }
                    isReadAnswer = value;
                }
            }
        }
    }
}

using Finance.Classes;
using Finance.Classes.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Models
{
    public class Support : DBModel
    {
        private int id;
        private int idUser;
        private string appealMessage;
        private string answerMessage;
        private DateTime dateOfAccess;
        private bool isAnswer;
        private bool isReadAnswer;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Support>("Id", value);
                }
                id = value;
            }
        }
        public string AppealMessage
        {
            get => !IsGet ? GetParametrs<string>("AppealMessage", this.GetType()) : appealMessage;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Support>("AppealMessage", value);
                }
                appealMessage = value;
            }
        }
        public int IdUser
        {
            get => !IsGet ? GetParametrs<int>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Support>("IdUser", value);
                }

                User = GetModel<Users>(value);
                idUser = value;
            }
        }
        public DateTime DateOfAccess
        {
            get => !IsGet ? GetParametrs<DateTime>("DateOfAccess", this.GetType()) : dateOfAccess;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Support>("DateOfAccess", value);
                }
                dateOfAccess = value;
            }
        }
        public bool IsAnswer
        {
            get => !IsGet ? GetParametrs<bool>("IsAnswer", this.GetType()) : isAnswer;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Support>("IsAnswer", value);
                }
                isAnswer = value;
            }
        }
        public string AnswerMessage
        {
            get => !IsGet ? GetParametrs<string>("AnswerMessage", this.GetType()) : answerMessage;
            set
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
        public bool IsReadAnswer
        {
            get => !IsGet ? GetParametrs<bool>("IsReadAnswer", this.GetType()) : isReadAnswer;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Support>("IsReadAnswer", value);
                }
                isReadAnswer = value;
            }
        }

        public Users User { get; private set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            base.SetParametrs<T>(param, value, id);
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.DeleteModel<Support>(this.Id);
            }
            else
            {
                base.DeleteModel<Support>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Support>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Support>(parametrs, Id, WhereCollection);
            }
        }
    }
}

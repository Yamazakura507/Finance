
using CommunityToolkit.Maui.Alerts;
using Finance.Classes.Enums;

namespace Finance.Classes
{
    public static class InfoViwer
    {
        private static bool Answer { get; set; } = false;
        private static string AnswerStr { get; set; } = null;
        private static string AnswerBt { get; set; } = null;


        async private static void Messege(Page page,string messege, string title, string cancel = "ОК", string accept = null, bool isInput = false, bool isSheet = false, string[] sheets = null, string placeholder = null, int maxLenght = -1, Keyboard keyboard = null, string initilValue = null)
        {
            if (isSheet) 
            {
                AnswerBt = await page.DisplayActionSheet(title, cancel, accept, sheets);
                return;
            }

            if (accept is null)
            {
                await page.DisplayAlert(title, messege, cancel);
            }
            else 
            {
                if (isInput)
                {
                    AnswerStr = await page.DisplayPromptAsync(title, messege, accept, cancel, placeholder, maxLenght, keyboard, initilValue);
                }
                else 
                {
                    Answer = await page.DisplayAlert(title, messege, accept, cancel);
                }
            }
        }

        public static void Messege(this Page page,string messege, ProviderType providerType)
        {
            Messege(page, messege, providerType == ProviderType.Error ? "ОШИБКА" : providerType == ProviderType.Alert ? "ПРЕДУПРЕЖДЕНИЕ" : "СООБЩЕНИЕ");
        }

        public static string SheetMessege(this Page page, string title, string[] sheets, bool isDelete = false)
        {
            Messege(page, null, title, "ОТМЕНА", isDelete ? "УДАЛИТЬ" : null, default, true, sheets);
            return AnswerBt;
        }

        public static string InputMessege(this Page page, string messege, string placeholder = null, int maxLenght = -1, Keyboard keyboard = null, string initilValue = null)
        {
            Messege(page, messege, "ВВОД", "ОТМЕНА", "ОК", true, default, default, placeholder, maxLenght, keyboard, initilValue);
            return AnswerStr;
        }

        public static bool QuestionMessege(this Page page,string messege, string cancel = "ОТМЕНА", string accept = "ОК")
        {
            Messege(page, messege, "ВОПРОС", cancel, accept);
            return Answer;
        }
    }
}

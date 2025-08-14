
using CommunityToolkit.Maui.Alerts;
using Finance.Classes.Enums;

namespace Finance.Classes
{
    public static class InfoViwer
    {
        async private static Task<object> Messege(Page page,string messege, string title, string cancel = "ОК", string accept = null, bool isInput = false, bool isSheet = false, string[] sheets = null, string placeholder = null, int maxLenght = -1, Keyboard keyboard = null, string initilValue = null)
        {
            if (isSheet) 
            {
                return await page.DisplayActionSheet(title, cancel, accept, sheets);
            }

            if (accept is null)
            {
                await page.DisplayAlert(title, messege, cancel);
                return null;
            }
            else 
            {
                if (isInput)
                {
                    return await page.DisplayPromptAsync(title, messege, accept, cancel, placeholder, maxLenght, keyboard, initilValue);
                }
                else 
                {
                    return await page.DisplayAlert(title, messege, accept, cancel);
                }
            }
        }

        async public static Task Messege(this Page page,string messege, ProviderType providerType)
        {
            await Messege(page, messege, providerType == ProviderType.Error ? "ОШИБКА" : providerType == ProviderType.Alert ? "ПРЕДУПРЕЖДЕНИЕ" : "СООБЩЕНИЕ");
        }

        async public static Task<string> SheetMessege(this Page page, string title, string[] sheets, bool isDelete = false)
        {
            return (await Messege(page, null, title, "ОТМЕНА", isDelete ? "УДАЛИТЬ" : null, default, true, sheets)).ToString();
        }

        async public static Task<string> InputMessege(this Page page, string messege, string placeholder = null, int maxLenght = -1, Keyboard keyboard = null, string initilValue = null)
        {
            return (await Messege(page, messege, "ВВОД", "ОТМЕНА", "ОК", true, default, default, placeholder, maxLenght, keyboard, initilValue)).ToString();
        }

        async public static Task<bool> QuestionMessege(this Page page,string messege, string cancel = "ОТМЕНА", string accept = "ОК")
        {
            return Convert.ToBoolean(await Messege(page, messege, "ВОПРОС", cancel, accept));
        }

        /// <summary>
        /// Диалог на выбор набора данных, обрабатывает только модель у которой есть Id
        /// </summary>
        /// <typeparam name="T">тип модели</typeparam>
        /// <param name="page">страница</param>
        /// <param name="title">подпись диалога</param>
        /// <param name="provaider">провайдер ошибки</param>
        /// <returns>Id выбраной модели, 0 вслучае ошибки или отмены</returns>
        async public static Task<int> SheetPicker<T>(this Page page, string title, CustomControl.Provaider provaider, string sheetColumnName = "Name")
        {
            string[] sheets = DBModel.GetColumn<T, string>(sheetColumnName).ToArray();

            string sheetSelect = null;

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    sheetSelect = await page.SheetMessege(title, sheets);
                }
                catch (Exception ex)
                {
                    sheetSelect = null;
                    provaider.WorkProvider(ProviderType.Error, ex.Message);
                }
            });

            if (sheetSelect is null || sheetSelect == "ОТМЕНА") return 0;

            return DBModel.Serch<T, int>("Id", new KeyValuePair<string, object>(sheetColumnName, sheetSelect));
        }
    }
}

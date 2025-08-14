using System.Text;
using System.Text.RegularExpressions;

namespace Finance.Classes
{
    static class TextBoxRestrictions
    {

        private static Color AlertBursh = Color.FromRgba(112, 1, 1, 30);

        private static Color GoodBursh = Colors.Transparent;

        async private static void Sleep(Entry textBox)
        {
            await Task.Delay(3000);
            textBox.BackgroundColor = GoodBursh;
        }

        public static bool TextEmptyTextBox(Entry textBox)
        {
            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackgroundColor = AlertBursh;
                textBox.Text = null;
                Sleep(textBox);
            }
            else
            {
                textBox.BackgroundColor = GoodBursh;
                return true;
            }

            return false;
        }

        public static bool TextCheckTextBox(Entry passwordBox, Entry passwordRepetBox)
        {
            if (passwordBox.Text != passwordRepetBox.Text)
            {
                passwordRepetBox.BackgroundColor = passwordBox.BackgroundColor = AlertBursh;
                passwordBox.Text = passwordRepetBox.Text = null;

                Sleep(passwordRepetBox);
                Sleep(passwordBox);
                return false;
            }
            else
            {
                passwordRepetBox.BackgroundColor = passwordBox.BackgroundColor = GoodBursh;
                return true;
            }
        }

        public static bool EmailTextBoxCheck(this Page page, string email, string login = null)
        {
            try
            {
                 page.EmailTextBoxCheck(new Entry() { Text = email }, login);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public static string EmailTextBoxCheck(this Page page, Entry textBox, string login = null)
        {
            string rand = GeneratorChufleString(new Random().Next(4, 10));

            try
            {
                page.CheckCodeMail(textBox.Text, new Dictionary<string, string>(){
                                                                { "@Login", login },
                                                                { "@Code", rand }
                                                            });
            }
            catch (Exception ex)
            {
                throw new Exception("Указаный Email не существует.\n");
            }

            return rand;
        }

        public static bool ContinueEmailTextBoxCheck(this Page page, string rand, Entry answerEntry)
        {

            bool is_reterning = true;

            try
            {
                if (rand != answerEntry.Text || String.IsNullOrEmpty(answerEntry.Text))
                {
                    answerEntry.BackgroundColor = AlertBursh;
                    answerEntry.Text = null;
                    Sleep(answerEntry);
                    is_reterning = false;
                }
            }
            catch (Exception ex)
            {
                is_reterning = false;
                throw ex;
            }

            if (is_reterning) answerEntry.Background = GoodBursh;
            return is_reterning;
        }

        public static string GeneratorChufleString(int lenght)
        {
            string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();

            string rand = null;


            rand = Enumerable.Range(0, lenght)
                    .Aggregate(
                        new StringBuilder(),
                        (sb, n) => sb.Append(symbols[random.Next(0, symbols.Length)]),
                        sb => sb.ToString());


            return rand;
        }

        public static void IsDecimalNumberEntry(this Entry entry) => entry.Text = Regex.Replace(entry.Text, "[^\\d,.]", "");
        public static void IsIntNumberEntry(this Entry entry) => entry.Text = Regex.Replace(entry.Text, "[^\\d]", "");
    }
}

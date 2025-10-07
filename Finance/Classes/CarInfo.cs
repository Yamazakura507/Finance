
namespace Finance.Classes
{
    public static class CarInfo
    {
        public static readonly Uri PassangerCarIcon = new Uri("https://pngicon.ru/file/uploads/krasnyj-sportivnyj-avtomobil-256x154.png");
        public static readonly Uri TruckIcon = new Uri("https://pngicon.ru/file/uploads/gruzovik.png");
        public static readonly Uri BusIcon = new Uri("https://s1.iconbird.com/ico/0512/iconspackbyCem/w512h5121337871377bus512.png");

        public static Uri LinkImageMakeCar(string make)
        {
            return new Uri(@$"https://www.auto-dd.ru/img/logos/sm/{make.Trim().ToLower()}-logo.png");
        }

        private static void IsUpper(Entry entry)
        {
            if (entry.Text.Any(i => !Char.IsUpper(i)))
            { 
                entry.Text = entry.Text.ToUpper();
            }
        }

        public static void EntryCheckToBlockEGRNFormat(this Entry entry)
        {
            if (String.IsNullOrEmpty(entry.Text)) return;

            int len = entry.Text.Length;
            short lenNum = 3;
            short lenIntoNum = 2;
            short lenOneLaterAndNum = 5;
            short lenSeparator = 1;
            short lenTwoLater = 2;
            short lenRegion = 3;

            if (!Char.IsLetter(entry.Text[0]))
            {
                entry.Text = String.Empty;
                return;
            }
            else if (len <= 1)
            {
                IsUpper(entry);

                return;
            }

            string formating = entry.Text.Substring(lenIntoNum, len >= lenOneLaterAndNum ? lenNum : len-lenIntoNum);

            if (formating.Any(i => !Char.IsDigit(i)))
            {
                entry.Text = entry.Text.Substring(0, len-lenSeparator).ToUpper();
                return;
            }
            else if (len <= lenOneLaterAndNum)
            {
                IsUpper(entry);

                return;
            }

            int lenOneLaterAndNumAndTwoLater = lenOneLaterAndNum + lenSeparator + lenTwoLater;
            formating = entry.Text.Substring(lenOneLaterAndNum + lenSeparator, len >= lenOneLaterAndNumAndTwoLater ? lenTwoLater : len - lenOneLaterAndNum - lenSeparator);

            if (formating.Any(i => !Char.IsLetter(i)))
            {
                entry.Text = entry.Text.Substring(0, len - lenSeparator).ToUpper();
                return;
            }
            else if (len <= lenOneLaterAndNumAndTwoLater)
            {
                IsUpper(entry);

                return;
            }

            int allLen = lenOneLaterAndNumAndTwoLater + lenSeparator + lenRegion;
            formating = entry.Text.Substring(lenOneLaterAndNumAndTwoLater + lenSeparator, len == allLen ? lenRegion : len - lenOneLaterAndNumAndTwoLater - lenSeparator);

            if (formating.Any(i => !Char.IsDigit(i)))
            {
                entry.Text = entry.Text.Substring(0, len - lenSeparator).ToUpper();
                return;
            }
        }
    }
}

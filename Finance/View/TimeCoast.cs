using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.View
{
    public class TimeCoast : Abstract.AbstractViewStatus<TimeCoast>
    {
        private int minute;

        public int Minute
        {
            get => minute;
            set
            {
                Time = ConvertMinuteToStr(value);
                minute = value;
            }
        }

        public string Time { get; set; }

        private string ConvertMinuteToStr(int minute)
        { 
            string strTime = string.Empty;
            TimeSpan ts = TimeSpan.FromMinutes(minute);
            int dayNowYear = (new DateTime(DateTime.Now.Year+1,1,1) - new DateTime(DateTime.Now.Year, 1, 1)).Days;
            int tsYear = (int)Math.Floor((decimal)ts.Days / dayNowYear);

            if (tsYear > 0) 
                strTime += $"{tsYear} " + ((tsYear > 10 && tsYear < 20) ? "лет " : tsYear % 10 == 1 ? "год " : tsYear % 10 < 5 && tsYear % 10 != 0 ? "года " : "лет ");

            int days = ts.Days % dayNowYear;

            if (days > 0)
                strTime += $"{days} " + ((days > 10 && days < 20) ? "дней " : days % 10 == 1? "день " : days % 10 < 5 && days % 10 != 0 ? "дня " : "дней ");

            if (ts.Hours > 0)
                strTime += $"{ts.Hours} " + ((ts.Hours > 10 && ts.Hours < 20) ? "часов " : ts.Hours % 10 == 1 ? "час " : ts.Hours % 10 < 5 && ts.Hours % 10 != 0 ? "час " : "часов ");

            if (ts.Minutes > 0)
                strTime += $"{ts.Minutes} " + ((ts.Minutes > 10 && ts.Minutes < 20) ? "минут " : ts.Minutes % 10 == 1 ? "минута " : ts.Minutes % 10 < 5 && ts.Minutes % 10 != 0 ? "минуты " : "минут ");

            return strTime;
        }
    }
}

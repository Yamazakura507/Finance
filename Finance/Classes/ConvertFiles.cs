using SkiaSharp;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Finance.Classes
{
    public static class ConverFiles
    {
        public static ImageSource ToImageConvert(byte[] imageArr) => ImageSource.FromStream(() => new MemoryStream(imageArr));
       
        public static byte[] ToByteConvert(string path) => File.ReadAllBytes(path);

        public static async Task<byte[]> ConvertImageSourceToBytesAsync(ImageSource imageSource)
        {
            Stream stream = await ((StreamImageSource)imageSource).Stream(CancellationToken.None);
            byte[] bytesAvailable = new byte[stream.Length];
            stream.Read(bytesAvailable, 0, bytesAvailable.Length);

            return bytesAvailable;
        }

        public static T ToObject<T>(this DataRow dataRow, T obj)
        {
            T item = obj;

            Parallel.ForEach(dataRow.Table.Columns.Cast<DataColumn>(), column =>
            {
                PropertyInfo property = GetProperty(typeof(T), column.ColumnName);

                if (property != null && dataRow[column] != DBNull.Value && dataRow[column].ToString() != "NULL")
                {
                    property.SetValue(item, ChangeType(dataRow[column], property.PropertyType), null);
                }
            });

            return item;
        }

        private static PropertyInfo GetProperty(Type type, string attributeName)
        {
            PropertyInfo property = type.GetProperty(attributeName);

            if (property != null)
            {
                return property;
            }

            return type.GetProperties()
                 .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name == attributeName)
                 .FirstOrDefault();
        }

        public static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                return Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
            }

            return Convert.ChangeType(value, type);
        }

        public static SKColor RaschetColor(this byte[] dataImage)
        {
            bool flag = false;
            SKColor color = new SKColor();

            while (!flag)
            {
                try
                {
                    List<SKColor> R = new List<SKColor>();
                    List<SKColor> G = new List<SKColor>();
                    List<SKColor> B = new List<SKColor>();
                    List<SKColor> RB = new List<SKColor>();
                    List<SKColor> RG = new List<SKColor>();
                    List<SKColor> BG = new List<SKColor>();
                    List<SKColor> Y = new List<SKColor>();

                    SKBitmap bmp;

                    using (var ms = new MemoryStream(dataImage))
                    {
                        bmp = SKBitmap.Decode(ms);
                    }

                    for (int i = 0; i < bmp.Width; i++)
                    {
                        for (int j = 0; j < bmp.Height; j++)
                        {
                            SKColor col = bmp.GetPixel(i,j);

                            if (col.Red > col.Green && col.Red > col.Blue) R.Add(col);
                            if (col.Green > col.Red && col.Green > col.Blue) G.Add(col);
                            if (col.Blue > col.Red && col.Blue > col.Green) B.Add(col);

                            if (col.Red < col.Green && col.Red < col.Blue) BG.Add(col);
                            if (col.Green < col.Red && col.Green < col.Blue) RB.Add(col);
                            if (col.Blue < col.Red && col.Blue < col.Green) BG.Add(col);
                            else Y.Add(col);
                        }
                    }

                    List<SKColor> lc = (R.Count > G.Count) ? R : G;

                    lc = (lc.Count > B.Count) ? lc : B;
                    lc = (lc.Count > RB.Count) ? lc : RB;
                    lc = (lc.Count > BG.Count) ? lc : BG;
                    lc = (lc.Count > RG.Count) ? lc : RG;
                    lc = (lc.Count > Y.Count) ? lc : Y;

                    int r = 0;
                    int g = 0;
                    int b = 0;

                    for (int i = 0; i < lc.Count; i++)
                    {
                        r += lc[i].Red;
                        g += lc[i].Green;
                        b += lc[i].Blue;
                    }

                    GC.Collect();

                    flag = true;
                    color = SKColor.Parse($"#{r/lc.Count:X2}{g/lc.Count:X2}{b/lc.Count:X2}");
                }
                catch (Exception ex) { continue; }
            }

            return color;
        }
    }
}

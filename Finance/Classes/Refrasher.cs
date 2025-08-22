using CommunityToolkit.Maui.Views;
using Finance.CustomControl;
using System.Reflection;

namespace Finance.Classes
{
    public static class Refrasher
    {
        private static Loading loading { get; set; }

        public static void RefrasModelContext<T>(this Page page) where T : new()
        {
            if (page.BindingContext is null) return;

            loading = new Loading();

            page.ShowPopup(loading);

            loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(async () =>
            {
                int id = page.RefrashId<T>();

                await MainThread.InvokeOnMainThreadAsync(() => page.BindingContext = DBModel.GetModel<T>(id));
            }));
        }

        public static int RefrashId<T>(this Page page)
        {
            Type type = typeof(T);

            FieldInfo field = type.GetField("id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if (field is null) field = type.GetField("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if (field is null) throw new Exception("Не удалось обновить содержимое, отсутствует Id привязка");

            int id = Convert.ToInt32(field.GetValue(page.BindingContext));

            return id;
        }

        public static G RefrashValue<T,G>(this object obj, string fieldName)
        {
            Type type = typeof(T);
            FieldInfo field = null;

            while (type != null && field is null)
            {
                field = type.GetField(fieldName.ToLower(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (field is null) field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                string baking = $"<{fieldName}>k__BackingField";

                if (field is null) field = type.GetField(baking, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                type = type.BaseType;
            }

            if (field is null) throw new Exception($"Не удалось обновить содержимое, отсутствует {fieldName} привязка");

            return (G)field.GetValue(obj);
        }

        public static void SetRefrashValue<T>(this T objSet, object val, string fieldName)
        {
            PropertyInfo? propertyInfo = objSet.GetType().GetProperty(fieldName);

            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(objSet, val);
            }
            else
            {
                throw new Exception($"Свойство {fieldName} не найдено или доступно только для чтения.");
            }
        }
    }
}

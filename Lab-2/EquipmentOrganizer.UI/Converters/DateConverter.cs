using EquipmentOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EquipmentOrganizer.UI.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime datetime)
            {
                return datetime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string datetime)
            {
                return DateTime.Parse(datetime);
            }

            return null;
        }
    }
}

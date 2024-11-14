using EquipmentOrganizer.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentOrganizer.Model;
using System.Windows.Controls;
using System.Windows.Data;

namespace EquipmentOrganizer.UI.Converters
{
    public class EquipmentTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EquipmentType equipmentType)
            {
                return equipmentType.ToString();
            }
            else if (value is EquipmentType[] equipmentTypes)
            {
                return equipmentTypes.Select(t => t.ToString()).ToArray();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string equipmentType)
            {
                return (EquipmentType)Enum.Parse(typeof(EquipmentType), equipmentType);
            }
            else if (value is string[] equipmentTypes)
            {
                return equipmentTypes.Select(t => (EquipmentType)Enum.Parse(typeof(EquipmentType), t)).ToArray();
            }

            return null;
        }
    }
}

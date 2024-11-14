using EquipmentOrganizer.Model;
using EquipmentOrganizer.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EquipmentOrganizer.UI.Converters
{
    public class EquipmentStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EquipmentState equipmentState)
            {
                return equipmentState switch
                {
                    EquipmentState.InUse => "In use",
                    EquipmentState.InService => "In service",
                    EquipmentState.InWarehouse => "In warehouse",
                    EquipmentState.Disposed => "Disposed of"
                };
            }
            else if (value is EquipmentState[] equipmentStates)
            {
                return equipmentStates.Select(t =>
                {
                    return t switch
                    {
                        EquipmentState.InUse => "In use",
                        EquipmentState.InService => "In service",
                        EquipmentState.InWarehouse => "In warehouse",
                        EquipmentState.Disposed => "Disposed of"
                    };
                }).ToArray();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string equipmentState)
            {
                return equipmentState switch
                {
                    "In use" => EquipmentState.InUse,
                    "In service" => EquipmentState.InService,
                    "In warehouse" => EquipmentState.InWarehouse,
                    "Disposed of" => EquipmentState.Disposed
                };
            }
            else if (value is string[] equipmentStates)
            {
                return equipmentStates.Select(t =>
                {
                    return t switch
                    {
                        "In use" => EquipmentState.InUse,
                        "In service" => EquipmentState.InService,
                        "In warehouse" => EquipmentState.InWarehouse,
                        "Disposed of" => EquipmentState.Disposed
                    };
                }).ToArray();
            }

            return null;
        }
    }
}

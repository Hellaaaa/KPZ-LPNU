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
    public class DepartamentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Departament departament)
            {
                return departament switch
                {
                    Departament.Sales => "Sales",
                    Departament.Marketing => "Marketing",
                    Departament.HumanResources => "Human Resources",
                    Departament.Development => "Development",
                    Departament.Finances => "Finances"
                };
            }
            else if (value is Departament[] departaments)
            {
                return departaments.Select(d =>
                {
                    return d switch
                    {
                        Departament.Sales => "Sales",
                        Departament.Marketing => "Marketing",
                        Departament.HumanResources => "Human Resources",
                        Departament.Development => "Development",
                        Departament.Finances => "Finances"
                    };
                }).ToArray();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string departament)
            {
                return departament switch
                {
                    "Sales" => Departament.Sales,
                    "Marketing" => Departament.Marketing,
                    "Human Resources" => Departament.HumanResources,
                    "Development" => Departament.Development,
                    "Finances" => Departament.Finances
                };
            }
            else if (value is string[] departaments)
            {
                return departaments.Select(d =>
                {
                    return d switch
                    {
                        "Sales" => Departament.Sales,
                        "Marketing" => Departament.Marketing,
                        "Human Resources" => Departament.HumanResources,
                        "Development" => Departament.Development,
                        "Finances" => Departament.Finances
                    };
                }).ToArray();
            }

            return null;
        }
    }
}

using EquipmentOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentOrganizer.UI.ViewModels
{
    public class EmployeeViewModel : ViewModel
    {
        private string position;
        private Departament departament;
        private int equipmentOwned;

        public string Name
        {
            get { return $"{FirstName} {LastName}"; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    FirstName = string.Empty;
                    LastName = string.Empty;
                }
                else
                {
                    string[] s = value.Split(' ');
                    FirstName = s[0];
                    LastName = s[1];
                }
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public Departament Departament
        {
            get { return departament; }
            set
            {
                departament = value;
                OnPropertyChanged(nameof(Departament));
            }
        }

        public int EquipmentOwned
        {
            get { return equipmentOwned; }
            set
            {
                equipmentOwned = value;
                OnPropertyChanged(nameof(EquipmentOwned));
            }
        }


        private string firstName;
        private string lastName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public EmployeeViewModel Copy()
        {
            return (EmployeeViewModel)MemberwiseClone();
        }
    }
}

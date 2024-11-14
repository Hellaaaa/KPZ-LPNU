using EquipmentOrganizer.Model;
using KPZ_Lab2.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EquipmentOrganizer.UI.ViewModels
{
    public class DataViewModel : ViewModel
    {
        public ObservableCollection<EquipmentViewModel> Equipment { get; set; }

        private EquipmentViewModel selectedEquipment;

        public EquipmentViewModel SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                selectedEquipment = value;
                OnPropertyChanged(nameof(SelectedEquipment));
            }
        }


        public ObservableCollection<EmployeeViewModel> Employees { get; set; }

        private EmployeeViewModel selectedEmployee;

        public EmployeeViewModel SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }


        public DataViewModel()
        {
            SetEquipmentFormVisibility = new Command(EquipmentFormVisibility);
            SetEmployeeFormVisibility = new Command(EmployeeFormVisibility);
            RemoveEquipment = new Command(RemoveSelectedEquipment);
            RemoveEmployee = new Command(RemoveSelectedEmployee);
            AddEquipment = new Command(args =>
            {
                AddNewEquipment(args);
                EquipmentFormVisibility("formClose");
            });
            AddEmployee = new Command(args =>
            {
                AddNewEmployee(args);
                EmployeeFormVisibility("formClose");
            });            
        }


        private string addFormMode = "Add";

        public string AddFormMode
        {
            get { return addFormMode; }
            set
            {
                addFormMode = value;
                OnPropertyChanged(nameof(AddFormMode));
            }
        }

        private bool equipmentFormIsVisible;

        public bool EquipmentFormIsVisible
        {
            get { return equipmentFormIsVisible; }
            set
            {
                equipmentFormIsVisible = value;
                OnPropertyChanged(nameof(EquipmentFormIsVisible));
            }
        }

        public ICommand SetEquipmentFormVisibility { get; set; }

        public void EquipmentFormVisibility(object args)
        {
            if (args.ToString() == "formClose")
            {
                EquipmentFormIsVisible = false;

                AddedEquipment.Name = string.Empty;
                AddedEquipment.Cost = 0;
                AddedEquipment.EquipmentType = EquipmentType.Chair;
                AddedEquipment.Owner = "";
                AddedEquipment.EquipmentState = EquipmentState.InService;
                AddedEquipment.PurchaseDate = DateTime.Now;
                AddedEquipment.OwnerType = OwnerType.Employee;
            }
            else if (args.ToString() == "browseAdd")
            {
                AddFormMode = "Add";
                EquipmentFormIsVisible = true;
            }
            else if (args.ToString() == "browseEdit")
            {
                if (SelectedEquipment != null)
                {
                    AddFormMode = "Edit";

                    AddedEquipment.Name = SelectedEquipment.Name;
                    AddedEquipment.Cost = SelectedEquipment.Cost;
                    AddedEquipment.EquipmentType = SelectedEquipment.EquipmentType;
                    AddedEquipment.Owner = SelectedEquipment.Owner;
                    AddedEquipment.EquipmentState = SelectedEquipment.EquipmentState;
                    AddedEquipment.PurchaseDate = SelectedEquipment.PurchaseDate;
                    AddedEquipment.OwnerType = SelectedEquipment.OwnerType;

                    EquipmentFormIsVisible = true;
                }
            }
        }

        private bool employeeFormIsVisible;

        public bool EmployeeFormIsVisible
        {
            get { return employeeFormIsVisible; }
            set
            {
                employeeFormIsVisible = value;
                OnPropertyChanged(nameof(EmployeeFormIsVisible));
            }
        }

        public ICommand SetEmployeeFormVisibility { get; set; }

        public void EmployeeFormVisibility(object args)
        {
            if (args.ToString() == "formClose")
            {
                EmployeeFormIsVisible = false;

                AddedEmployee.Name = string.Empty;
                AddedEmployee.Position = string.Empty;
                AddedEmployee.EquipmentOwned = 0;
                AddedEmployee.Departament = Departament.Marketing;
            }
            else if (args.ToString() == "browseAdd")
            {
                AddFormMode = "Add";
                EmployeeFormIsVisible = true;
            }
            else if (args.ToString() == "browseEdit")
            {
                AddFormMode = "Edit";

                AddedEmployee.Name = SelectedEmployee.Name;
                AddedEmployee.Position = SelectedEmployee.Position;
                AddedEmployee.EquipmentOwned = SelectedEmployee.EquipmentOwned;
                AddedEmployee.Departament = SelectedEmployee.Departament;

                EmployeeFormIsVisible = true;
            }
        }

        public ICommand RemoveEquipment { get; set; }

        public void RemoveSelectedEquipment(object args)
        {
            if (args is EquipmentViewModel equipment)
            {
                Equipment.Remove(equipment);
            }
        }

        public ICommand RemoveEmployee { get; set; }

        public void RemoveSelectedEmployee(object args)
        {
            if (args is EmployeeViewModel employee)
            {
                Employees.Remove(employee);
            }
        }

        public EquipmentViewModel AddedEquipment { get; set; } = new EquipmentViewModel() 
        { 
            EquipmentType = EquipmentType.Chair, 
            EquipmentState = EquipmentState.InUse,
            PurchaseDate = DateTime.Now,
            OwnerType = OwnerType.Employee
        };

        public ICommand AddEquipment { get; set; }

        public void AddNewEquipment(object args)
        {
            if (args is EquipmentViewModel equipment)
            {
                if (AddFormMode == "Add")
                {
                    EquipmentViewModel copy = equipment.Copy();
                    copy.AccountingCode = string.Format("{0:D2}{1:D3}", (int)equipment.EquipmentType, 0);
                    if (copy.OwnerType == OwnerType.None || string.IsNullOrEmpty(copy.Owner))
                    {
                        copy.Owner = "None";
                    }
                    Equipment.Add(copy);
                }
                else if (AddFormMode == "Edit")
                {
                    SelectedEquipment.Name = AddedEquipment.Name;
                    SelectedEquipment.Cost = AddedEquipment.Cost;
                    SelectedEquipment.EquipmentType = AddedEquipment.EquipmentType;
                    SelectedEquipment.Owner = AddedEquipment.Owner;
                    SelectedEquipment.EquipmentState = AddedEquipment.EquipmentState;
                    SelectedEquipment.PurchaseDate = AddedEquipment.PurchaseDate;
                    SelectedEquipment.OwnerType = AddedEquipment.OwnerType;
                }                
            }
        }

        public EmployeeViewModel AddedEmployee { get; set; } = new EmployeeViewModel()
        {
            Departament = Departament.Sales
        };

        public ICommand AddEmployee { get; set; }

        public void AddNewEmployee(object args)
        {
            if (args is EmployeeViewModel employee)
            {
                if (AddFormMode == "Add")
                {
                    EmployeeViewModel copy = employee.Copy();
                    Employees.Add(copy);
                }
                else if (AddFormMode == "Edit")
                {
                    SelectedEmployee.Name = AddedEmployee.Name;
                    SelectedEmployee.Position = AddedEmployee.Position;
                    SelectedEmployee.EquipmentOwned = AddedEmployee.EquipmentOwned;
                    SelectedEmployee.Departament = AddedEmployee.Departament;
                }
            }
        }
    }
}

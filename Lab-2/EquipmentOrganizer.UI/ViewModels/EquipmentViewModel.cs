using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentOrganizer.Model;

namespace EquipmentOrganizer.UI.ViewModels
{
    public class EquipmentViewModel : ViewModel
    {
        private string accountingCode;
        private EquipmentType equipmentType;
        private string name;
        private DateTime purchaseDate;
        private int cost;
        private string owner;
        private EquipmentState equipmentState;
        private OwnerType ownerType = OwnerType.Employee;

        public string AccountingCode
        {
            get { return accountingCode; }
            set
            {
                accountingCode = value;
                OnPropertyChanged(nameof(AccountingCode));
            }
        }

        public EquipmentType EquipmentType
        {
            get { return equipmentType; }
            set
            {
                equipmentType = value;
                OnPropertyChanged(nameof(EquipmentType));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DateTime PurchaseDate
        {
            get { return purchaseDate; }
            set
            {
                purchaseDate = value;
                OnPropertyChanged(nameof(PurchaseDate));
            }
        }

        public int Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }
        
        public EquipmentState EquipmentState
        {
            get { return equipmentState; }
            set
            {
                equipmentState = value;
                OnPropertyChanged(nameof(EquipmentState));
            }
        }

        public OwnerType OwnerType
        {
            get { return ownerType; }
            set
            {
                ownerType = value;
                OnPropertyChanged(nameof(OwnerType));
            }
        }

        public EquipmentViewModel Copy()
        {
            return (EquipmentViewModel)MemberwiseClone();
        }
    }
}

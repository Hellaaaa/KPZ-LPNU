using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentOrganizer.Model
{
    [DataContract]
    public class Equipment
    {
        [DataMember]
        public string AccountingCode { get; set; }
        [DataMember]
        public EquipmentType EquipmentType { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime PurchaseDate { get; set; }
        [DataMember]
        public int Cost { get; set; }
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public EquipmentState EquipmentState { get; set; }
        [DataMember]
        public OwnerType OwnerType { get; set; }
    }

    [DataContract]
    public enum EquipmentType
    {
        [EnumMember]
        Chair,
        [EnumMember]
        Table,
        [EnumMember]
        Wardrobe, 
        [EnumMember]
        CoffeeMachine,
        [EnumMember]
        Fridge,
        [EnumMember]
        Microwave,
        [EnumMember]
        Teapot
    }

    [DataContract]
    public enum EquipmentState
    {
        [EnumMember]
        InUse,
        [EnumMember]
        InService,
        [EnumMember]
        InWarehouse,
        [EnumMember]
        Disposed
    }

    [DataContract]
    public enum OwnerType
    {
        [EnumMember]
        None,
        [EnumMember]
        Employee,
        [EnumMember]
        Departament
    }
}

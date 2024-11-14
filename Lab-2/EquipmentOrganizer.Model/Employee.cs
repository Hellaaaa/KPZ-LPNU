using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentOrganizer.Model
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public Departament Departament { get; set; }
        [DataMember]
        public int EquipmentOwned { get; set; }
    }

    [DataContract]
    public enum Departament
    {
        [EnumMember]
        Marketing,
        [EnumMember]
        Sales,
        [EnumMember]
        Finances,
        [EnumMember]
        HumanResources,
        [EnumMember]
        Development
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EquipmentOrganizer.Model.Serialization;

namespace EquipmentOrganizer.Model
{
    [DataContract]
    public class DataModel
    {
        [DataMember]
        public IEnumerable<Equipment> Equipment { get; set; }

        [DataMember]
        public IEnumerable<Employee> Employees { get; set; }

        public DataModel()
        {
            Equipment = new List<Equipment>();
            Employees = new List<Employee>();
        }

        private static readonly string file = @"D:\Univ\5sem\КПЗ\labs\lab2\EquipmentOrganizer\organizer.dat";

        public static DataModel Load()
        {
            if (System.IO.File.Exists(file))
            {
                return DataSerializer.DeserializeData(file);
            }
            return new DataModel();
        }

        public void Save()
        {
            DataSerializer.SerializeData(file, this);
        }
    }
}

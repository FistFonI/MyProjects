using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Domain
{
    public class Shop
    {
        [Key]
        public int ID { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value != null && value != "")
                    name = value;
                else
                    throw new ArgumentNullException("Название магазина не может быть пустым.");
            }
        }

        private string address;
        public string Address
        {
            get => address;
            set
            {
                if (value != null && value != "")
                    address = value;
                else
                    throw new ArgumentNullException("Адрес магазина не может быть пустым.");
            }
        }

        public virtual ICollection<StoreDepartment> StoreDepartments { get; set; }

        public virtual ICollection<Storage> Storages { get; set; }

        public virtual ICollection<ProductType> ProductsTypes { get; set; }

        public Shop(string name, string adress)
        {
            Name = name;
            Address = adress;
            StoreDepartments = new List<StoreDepartment>();
            Storages = new List<Storage>();
            ProductsTypes = new List<ProductType>();
        }

        public Shop()
        {
            StoreDepartments = new List<StoreDepartment>();
            Storages = new List<Storage>();
            ProductsTypes = new List<ProductType>();
        }

        public override string ToString()
        {
            return $"\"{Name}\" {Address}";
        }
    }
}

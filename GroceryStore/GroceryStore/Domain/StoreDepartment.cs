using GroceryStore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStore.Domain
{
    public class StoreDepartment
    {
        [Key]
        public int ID { get; set; }

        public Departments Name { get; set; }

        [ForeignKey("Shop")]
        public int ShopID { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual ICollection<ProductType> ProductTypes { get; set; }

        public StoreDepartment()
        {
            ProductTypes = new List<ProductType>();
        }

        public StoreDepartment(Departments name, Shop shop)
        {
            Name = name;
            Shop = shop;
            ProductTypes = new List<ProductType>();
        }
    }
}

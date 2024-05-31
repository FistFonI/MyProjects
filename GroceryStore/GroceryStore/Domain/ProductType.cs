using GroceryStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStore.Domain
{
    public class ProductType
    {
        [Key]
        public int ID { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set { name = value; }
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Значение цены товара должно быть неотрицательным числом.");
                price = value;
            }
        }

        private int shelfLife;
        public int ShelfLife
        {
            get => shelfLife;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Значение срока годности должно быть положительным числом.");
                shelfLife = value;
            }
        }

        public Departments Department { get; set; }

        public Storages StorageConditions { get; set; }

        public string Sort { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [ForeignKey("Shop")]
        public int ShopID { get; set; }

        public virtual Shop Shop { get; set; }

        public ProductType()
        {
            Products = new List<Product>();
        }

        public ProductType(string name, decimal price, int shelfLife, Departments department, Storages storage, string sort, Shop shop)
        {
            Name = name;
            Price = price;
            ShelfLife = shelfLife;
            Department = department;
            StorageConditions = storage;
            Sort = sort;
            Products = new List<Product>();
            Shop = shop;
        }
    }
}

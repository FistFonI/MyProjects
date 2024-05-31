using GroceryStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStore.Domain
{
    public class Storage
    {
        [Key]
        public int ID { get; set; }

        public Storages Type { get; set; }

        private int size;
        public int Size
        {
            get => size;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Значение объёма склада должно быть неотрицательным числом.");
                size = value;
            }
        }

        [ForeignKey("Shop")]
        public int ShopID { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual ICollection<ProductType> ProductsTypes { get; set; }

        public Storage()
        {
            ProductsTypes = new List<ProductType>();
        }

        public Storage(Storages type, int size, Shop shop)
        {
            Type = type;
            Size = size;
            Shop = shop;
            ProductsTypes = new List<ProductType>();
        }
    }
}

using GroceryStore.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryStore
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        public DateTime BuyDate { get; set; }

        private int count;
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Значение количенства оставшихся товаров должно быть неотрицательным числом.");
                count = value;
            }
        }

        private int sold;
        public int Sold
        {
            get => sold;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Значение количества проданных товаров должно быть неотрицательным числом.");
                sold = value;
            }
        }

        public int Balance
        {
            get => Count - Sold;
        }

        private DateTime dateOfManufacture;
        public DateTime DateOfManufacture
        {
            get => dateOfManufacture;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Указана не верная дата изготовления товара.");
                else
                    dateOfManufacture = value;
            }
        }

        [ForeignKey("ProductType")]
        public int ProdTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }

        public bool Overdue()
        {
            int DayPassed = DateTime.Now.DayOfYear + (((DateTime.Now.Year - dateOfManufacture.Year) * 365) - dateOfManufacture.DayOfYear);
            int DT = ProductType.ShelfLife - DayPassed;
            return DT < 0;
        }

        public Product()
        {

        }

        public Product(DateTime buyDate, int count, DateTime dateOfManufacture, ProductType productType)
        {
            BuyDate = buyDate;
            Count = count;
            DateOfManufacture = dateOfManufacture;
            ProductType = productType;
        }
    }
}

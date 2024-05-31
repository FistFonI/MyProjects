using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GroceryStore.Domain
{
    /// <summary>
    /// Представляет собой класс, формирующий новые заказы (заказывающий новые партии продуктов).
    /// </summary>
    class AutoOrder
    {
        /// <summary>
        /// Объект типа StoreContext, который является базой данных.
        /// </summary>
        private StoreContext context;
        /// <summary>
        /// Объект типа List<int>, представляет собой список из индексов продуктов, подлежащих удалению из базы данных.
        /// </summary>
        private List<int> deleteIndexes;

        /// <summary>
        /// Конструктор, принимающий базу данных, а также реализующий начало формирования новых заказов.
        /// </summary>
        /// <param name="context"></param>
        public AutoOrder(StoreContext context)
        {
            this.context = context;
            deleteIndexes = new List<int>();
            CheckContext();
        }

        /// <summary>
        /// Метод, который берёт все магазины из базы данных и перебирает их, в конце удаляет лишние элементы.
        /// </summary>
        private void CheckContext()
        {
            //Выбор всех магазинов из базы данных.
            var shops = context.Shops
                .Select(sh => sh)
                .ToList();

            //Проверка каждого магазина.
            foreach (var shop in shops)
            {
                CheckOneShop(shop);
            }
            //Удаление лишних элементов из базы данных.
            DeleteFromDB();
        }

        /// <summary>
        /// Метод, который выполняет проверку одного магазина и перебирает все виды продуктов.
        /// </summary>
        /// <param name="shop">Магазин. Объект типа Shop.</param>
        private void CheckOneShop(Shop shop)
        {
            //Выбор всех типов продуктов из базы данных.
            var productTypes = shop.ProductsTypes
                .Select(pt => pt)
                .ToList();
            //Проверка каждого типа продуктов.
            foreach (var pt in productTypes)
            {
                CheckOneProductType(pt);
            }
        }

        /// <summary>
        /// Метод, который выполняет проверку одного вида продуктов и перебирает все его партии. Подсчитывается количество товаров новой партии.
        /// </summary>
        /// <param name="productType">Вид продукта. Объект типа ProductType</param>
        private void CheckOneProductType(ProductType productType)
        {
            //Все партии продукта.
            var allProducts = productType.Products;
            //Количество у.е. всех партий продукта.
            var allCount = allProducts
                .Select(p => p.Count)
                .Sum();

            //Не испортившиеся партии продукта.
            var goodProducts = allProducts
                .Where(p => !p.Overdue())
                .ToList();
            //Количество у. е. не испортившихся партий продукта.
            var goodCount = goodProducts
                .Select(p => p.Count)
                .Sum();
            //Количество проданных у.е. не испортившихся партий продукта.
            var soldCount = goodProducts
                .Select(p => p.Sold)
                .Sum();

            //Количество закупки = Округление(Коэффициет испорченности * Коэффициет покупок * количество проданных).
            var buyCount = (int)Math.Round(FindOverdueCoefficient(allCount, goodCount) * FindBuyCoefficient(goodCount, soldCount) * soldCount);

            //Проверка каждой партии продуктов.
            foreach (var p in allProducts)
            {
                CheckOneProduct(p);
            }
            //Формирование новой партии продукта.
            CreateNewOrder(buyCount, productType);
        }

        /// <summary>
        /// Метод, который выполняет проверку партии продукта на испорченность и законченность.
        /// </summary>
        /// <param name="product">Партия продукта. Объект типа Product.</param>
        private void CheckOneProduct(Product product)
        {
            //Испорчена или закончена ли партия продукта. 
            if (product.Overdue() || (product.Balance == 0))
            {
                deleteIndexes.Add(product.ID);   
            }
            else
            {
                //Количество партии приравнивается остатку после продажи.
                product.Count = product.Balance;
                product.Sold = 0;
                context.Entry(product).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Метод нахождения коэффициента испорченности.
        /// </summary>
        /// <param name="allCount">Общее количество у.е. всех партий продукта.</param>
        /// <param name="goodCount">Количество у.е. не испорченных партий продукта.</param>
        /// <returns>Коэффициент испорченности.</returns>
        private double FindOverdueCoefficient(int allCount, int goodCount)
        {
            double part = (double)goodCount / allCount;
            if (1 >= part && part > 0.95)
                return 0.95;
            else if (0.95 >= part && part > 0.8)
                return 0.85;
            else if (0.8 >= part && part > 0.5)
                return 0.8;
            else if (0.5 >= part && part > 0.2)
                return 0.5;
            else 
                return 0.3;
        }

        /// <summary>
        /// Метод нахождения коэффициента покупок.
        /// </summary>
        /// <param name="goodCount">Количество у.е. не испорченных партий продукта.</param>
        /// <param name="soldCount">Количество проданных у.е. не испорченных партий продукта.</param>
        /// <returns>Коэффициент покупок.</returns>
        private double FindBuyCoefficient(int goodCount, int soldCount)
        {
            double part = (double)soldCount / goodCount;
            if (1 >= part && part > 0.8)
                return 1.3;
            else if (0.8 >= part && part > 0.6)
                return 1.1;
            else if (0.6 >= part && part > 0.4)
                return 1.0;
            else if (0.4 >= part && part > 0.2)
                return 0.9;
            else
                return 0.7;
        }

        /// <summary>
        /// Метод нахождения коэффициента склада.
        /// </summary>
        /// <param name="buyCount">Количество закупаемых у.е. партий продукта.</param>
        /// <param name="productType">Вид продукта.</param>
        /// <returns>Коэффициент склада.</returns>
        private double FindStorageCoefficient(int buyCount, ProductType productType)
        {
            var count = context.Products
                .Where(p => p.ProductType.ShopID == productType.ShopID)
                .Where(p => p.ProductType.StorageConditions == productType.StorageConditions)
                .ToList()
                .Where(p => !p.Overdue())
                .Count();
            int productsCount = 0;
            if (count > 0)
                productsCount = context.Products
                    .Where(p => p.ProductType.ShopID == productType.ShopID)
                    .Where(p => p.ProductType.StorageConditions == productType.StorageConditions)
                    .ToList()
                    .Where(p => !p.Overdue())
                    .Select(p => p.Count)
                    .Sum();
            var size = context.Storages
                .Where(st => st.ShopID == productType.ShopID)
                .Where(st => st.Type == productType.StorageConditions)
                .Select(st => st.Size)
                .FirstOrDefault();

            if (buyCount + productsCount > size)
                return 0;
            var part = ((double)buyCount + productsCount) / size;
            if (1 >= part && part > 0.8)
                return 0.97;
            else if (0.8 >= part && part > 0.6)
                return 1.0;
            else if (0.6 >= part && part > 0.4)
                return 1.03;
            else if (0.4 >= part && part > 0.2)
                return 1.05;
            else
                return 1.07;
        }

        /// <summary>
        /// Метод формирования новой партии продукта.
        /// </summary>
        /// <param name="buyCount">Количество закупочных у.е. партий продукта.</param>
        /// <param name="productType">Вид продукта.</param>
        private void CreateNewOrder(int buyCount, ProductType productType)
        {
            var pType = context.ProductTypes
                .Select(pt => pt)
                .Where(pt => pt.ID == productType.ID)
                .FirstOrDefault();
            var today = DateTime.Now;
            var finalCount = (int)Math.Round(buyCount * FindStorageCoefficient(buyCount, productType));
            var product = new Product(today, finalCount, today, pType);
            context.Products.Add(product);
            context.SaveChanges();
        }

        /// <summary>
        /// Метод удаления ненужных элементов из базы данных.
        /// </summary>
        private void DeleteFromDB()
        {
            foreach (var ind in deleteIndexes)
            {
                var product = context.Products
                    .Where(p => p.ID == ind)
                    .FirstOrDefault();
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}

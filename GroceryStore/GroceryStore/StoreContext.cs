using System.Data.Entity;
using GroceryStore.Domain;

namespace GroceryStore
{
    class StoreContext : DbContext
    {
        public StoreContext() : base("GroceryStoreDB")
        {
            // Установить новый инициализатор
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StoreContext>());
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<StoreDepartment> StoreDepartments { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}


using System.Data.Entity;

namespace Workshop
{
    class RepairOrderContext : DbContext
    {
        public RepairOrderContext() : base("WorkshopDB")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<RepairOrderContext>());
        }

        public DbSet<RepairOrder> Orders { get; set; }
    }
}

namespace QrMenu.DAL
{
    using QrMenu.MODEL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class QrMenuContext : DbContext
    {
        // Your context has been configured to use a 'QrMenuContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'QrMenu.DAL.QrMenuContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'QrMenuContext' 
        // connection string in the application configuration file.
        public QrMenuContext()
            : base("name=QrMenuContext")
        {
            Database.SetInitializer<QrMenuContext>(new MigrateDatabaseToLatestVersion<QrMenuContext, QrMenu.DAL.Migrations.Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Product>()
            //    .HasMany<ProductContent>(f => f.ProductContents)
            //    .WithMany(m => m.Products)
            //    .Map(x =>
            //    {
            //        x.MapLeftKey(new string[] { });
            //        x.MapRightKey(new string[] { });
            //        x.ToTable("ProductContentProduct");
            //    });


        }

    }
   


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
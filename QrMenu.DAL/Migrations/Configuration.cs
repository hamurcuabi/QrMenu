namespace QrMenu.DAL.Migrations
{
    using QrMenu.MODEL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QrMenu.DAL.QrMenuContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(QrMenu.DAL.QrMenuContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Kullanici.AddOrUpdate(new Kullanici { KullaniciAdi = "Admin", Sifre = "qrmenu/admin", Yetki = (int)EnumYetki.Admin });
            context.Kullanici.AddOrUpdate(new Kullanici { KullaniciAdi = "isletme", Sifre = "isletme", Yetki = (int)EnumYetki.Isletme });
        }
    }
}

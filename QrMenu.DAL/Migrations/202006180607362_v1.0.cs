namespace QrMenu.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        KategoriId = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        MediaPath = c.String(),
                        Aciklama = c.String(),
                        IsAktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KategoriId);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        KullaniciId = c.Int(nullable: false, identity: true),
                        KullanıcıAdi = c.String(),
                        Sifre = c.String(),
                    })
                .PrimaryKey(t => t.KullaniciId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        MenuSpecialId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        MediaPath = c.String(),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAktif = c.Boolean(nullable: false),
                        Miktar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ad = c.String(),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.Urun", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Urun",
                c => new
                    {
                        UrunId = c.Int(nullable: false, identity: true),
                        KategoriId = c.Int(),
                        Ad = c.String(),
                        Aciklama = c.String(),
                        MediaPath = c.String(),
                        Kodu = c.String(),
                        IsAktif = c.Boolean(nullable: false),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.UrunId)
                .ForeignKey("dbo.Kategori", t => t.KategoriId)
                .Index(t => t.KategoriId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menu", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.Urun", "KategoriId", "dbo.Kategori");
            DropIndex("dbo.Urun", new[] { "KategoriId" });
            DropIndex("dbo.Menu", new[] { "UrunId" });
            DropTable("dbo.Urun");
            DropTable("dbo.Menu");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Kategori");
        }
    }
}

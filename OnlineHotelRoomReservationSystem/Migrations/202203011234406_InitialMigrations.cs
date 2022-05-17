namespace OnlineHotelRoomReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailId = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AdminId)
                .Index(t => t.EmailId, unique: true);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        AadharNumber = c.String(nullable: false, maxLength: 12),
                        PhoneNumber = c.String(nullable: false, maxLength: 10),
                        EmailId = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .Index(t => t.AadharNumber, unique: true)
                .Index(t => t.PhoneNumber, unique: true)
                .Index(t => t.EmailId, unique: true);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        HotelName = c.String(nullable: false, maxLength: 100),
                        HotelAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        PinCode = c.String(nullable: false, maxLength: 6),
                        IsActive = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HotelId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        CheckIn = c.String(nullable: false),
                        CheckOut = c.String(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        FloorNumber = c.Int(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                        RoomType = c.String(nullable: false),
                        RoomCapacity = c.Int(nullable: false),
                        CostPerDay = c.Int(nullable: false),
                        RoomStatus = c.String(nullable: false),
                        HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "EmailId" });
            DropIndex("dbo.Customers", new[] { "PhoneNumber" });
            DropIndex("dbo.Customers", new[] { "AadharNumber" });
            DropIndex("dbo.Admins", new[] { "EmailId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.Hotels");
            DropTable("dbo.Customers");
            DropTable("dbo.Admins");
        }
    }
}

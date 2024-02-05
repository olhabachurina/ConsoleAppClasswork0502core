using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static ConsoleAppDop3HW3Core.Program;
using static System.Collections.Specialized.BitVector32;
namespace ConsoleAppDop3HW3Core;

class Program
{
   

    static void Main()
    {
        using (var db = new ApplicationContext())
        {
            //        var stations = new List<Station>
            //{
            //    new Station {StationName = "Central", Location = "City Center" },
            //    new Station {StationName = "North", Location = "North Side" },
            //    new Station {StationName = "East", Location = "East Side" },
            //    new Station {StationName = "West", Location = "West Side" }
            //};
            //        db.Stations.AddRange(stations);
            //        var trains = new List<Train>
            //{
            //    new Train { Model = "Pelican Express", Duration = 6, ManufactureDate = new DateTime(2010, 1, 1), StationId = 1 },
            //    new Train { Model = "Rapid Streamliner", Duration = 7, ManufactureDate = new DateTime(2005, 1, 1), StationId = 2 },
            //    new Train { Model = "Mountain Mover", Duration = 8, ManufactureDate = new DateTime(2000, 1, 1), StationId = 3 },
            //    new Train { Model = "Pelican Sprinter", Duration = 5, ManufactureDate = new DateTime(2015, 1, 1), StationId = 4 }
            //};
            //        db.Trains.AddRange(trains);
            //        db.SaveChanges();

            // 1. Добавить данные о станциях и поездах.
            //db.Database.ExecuteSqlRaw("INSERT INTO Stations (StationName, Location) VALUES ('Station1', 'Location1')");
            //db.Database.ExecuteSqlRaw("INSERT INTO Trains (Model, Duration, ManufactureDate, StationId) VALUES ('Model1', 6, '2005-01-01', (SELECT StationId FROM Stations WHERE StationName = 'Station1'))");

            //// 2. Поезда, длительность маршрута более 5 часов.
            //var longDurationTrains = db.Trains.FromSqlRaw("SELECT * FROM Trains WHERE Duration > 5").ToList();

            ////// 3. Общая информация о станции и ее поездах.
            //var stationInfo = db.Stations.FromSqlRaw("SELECT s.StationId, s.StationName, s.Location, COUNT(t.TrainId) AS NumberOfTrains " +
            //        "FROM Stations s " +
            //        "LEFT JOIN Trains t ON s.StationId = t.StationId " +
            //        "GROUP BY s.StationId, s.StationName, s.Location").ToList();
            //// 4.Название станций, где в наличии более 3 - х поездов.
            //var stationsWithMoreTrains = db.Stations.FromSqlRaw("SELECT s.StationId, s.StationName, s.Location FROM Stations s WHERE s.StationId IN (SELECT t.StationId FROM Trains t GROUP BY t.StationId HAVING COUNT(t.TrainId) > 3)").ToList();

            //// 5. Все поезда, модель которых начинается на 'Pel'.
            //var trainsWithModelPel = db.Trains.FromSqlRaw("SELECT * FROM Trains WHERE Model LIKE 'Pel%'").ToList();

            //// 6. Все поезда, возраст которых более 15 лет с текущей даты.
            //var oldTrains = db.Trains.FromSqlRaw("SELECT * FROM Trains WHERE DATEDIFF(year, ManufactureDate, GETDATE()) > 15").ToList();

            //// 7. Получить станции, у которых есть поезд с длительностью маршрута менее 5 часов.
            //var stationsWithShortDurationTrains = db.Stations.FromSqlRaw("SELECT * FROM Stations WHERE EXISTS (SELECT 1 FROM Trains WHERE StationId = Stations.StationId AND Duration < 5)").ToList();

            //// 8. Вывести станции без поездов.
            //var stationsWithoutTrains = db.Stations.FromSqlRaw("SELECT * FROM Stations WHERE NOT EXISTS (SELECT 1 FROM Trains WHERE StationId = Stations.StationId)").ToList();
        }

    }
}
    
    public class ApplicationContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }
        public DbSet<Train> Trains { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-4PCU5RA\\SQLEXPRESS;Database=testdb;Trusted_Connection=True;TrustServerCertificate=True;");
                optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>()
                .HasKey(s => s.StationId);

            modelBuilder.Entity<Train>()
                .HasKey(t => t.TrainId);

            modelBuilder.Entity<Train>()
                .HasOne<Station>()
                .WithMany()
                .HasForeignKey(t => t.StationId);
        }
    }

    public class Station
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public string Location { get; set; }
    }

    public class Train
    {
        public int TrainId { get; set; }
        public string Model { get; set; }
        public int Duration { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int StationId { get; set; }
    }

   


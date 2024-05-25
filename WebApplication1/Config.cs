using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplication1
{
    public class BaseDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-A5HLKLJ;" +
                "Initial Catalog=TicketSalesSystem;Integrated Security=True; " +
                "Trusted_Connection = True; TrustServerCertificate = True");
        
    }
    }
    public class ClientsDB : BaseDbContext
    {
        public DbSet<Clients> Clients { get; set; }
    }
    public class HallsDB : BaseDbContext
    {
        public DbSet<Halls> Halls { get; set; }
    }
    public class MoviesDB : BaseDbContext
    {
        public DbSet<Movies> Movies { get; set; }
    }
    public class OrdersDB : BaseDbContext
    {
        public DbSet<Orders> Orders { get; set; }

    }
    public class PaymentMethodsDB : BaseDbContext
    {
        public DbSet<PaymentMethods> PaymentMethods { get; set; }

    }
    public class PaymentsDB : BaseDbContext
    {
        public DbSet<Payments> Payments { get; set; }

    }
    public class PlaceCategoriesDB : BaseDbContext
    {
        public DbSet<PlaceCategories> PlaceCategories { get; set; }

    }
    public class PlacesDB : BaseDbContext
    {
        public DbSet<Places> Places { get; set; }

    }
    public class SessionsDB : BaseDbContext
    {
        public DbSet<Sessions> Sessions { get; set; }

    }
    public class StatusDB : BaseDbContext
    {
        public DbSet<Status> Status { get; set; }

    }
    public class TicketOrdersDB : BaseDbContext
    {
        public DbSet<TicketOrders> TicketOrders { get; set; }

    }
    public class TicketsDB : BaseDbContext
    {
        public DbSet<Tickets> Tickets { get; set; }

    }
}
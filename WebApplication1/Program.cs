using Microsoft.EntityFrameworkCore;
using WebApplication1;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        // добавление таблицы в контекст ASP.NET
        builder.Services.AddDbContext<ClientsDB>();
        builder.Services.AddDbContext<HallsDB>();
        builder.Services.AddDbContext<MoviesDB>();
        builder.Services.AddDbContext<OrdersDB>();
        builder.Services.AddDbContext<PaymentMethodsDB>();
        builder.Services.AddDbContext<PaymentsDB>();
        builder.Services.AddDbContext<PlaceCategoriesDB>();
        builder.Services.AddDbContext<PlacesDB>();
        builder.Services.AddDbContext<SessionsDB>();
        builder.Services.AddDbContext<StatusDB>();
        builder.Services.AddDbContext<TicketOrdersDB>();
        builder.Services.AddDbContext<TicketsDB>();
        
        builder.Services.AddSwaggerGen();
    
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // подключение свагера 
            app.UseSwaggerUI();
        }
        app.MapEntityEndpoints<Clients, ClientsDB>("/clients");
     
        app.MapEntityEndpoints<Halls, HallsDB>("/halls");
     
        app.MapEntityEndpoints<Movies, MoviesDB>("/movies");
     
        app.MapEntityEndpoints<Orders, OrdersDB>("/orders");
     
        app.MapEntityEndpoints<PaymentMethods, PaymentMethodsDB>("/paymentmethods");
     
        app.MapEntityEndpoints<Payments, PaymentsDB>("/payments");
     
        app.MapEntityEndpoints<PlaceCategories, PlaceCategoriesDB>("/Placecategories");
     
        app.MapEntityEndpoints<Places, PlacesDB>("/Places");
     
        app.MapEntityEndpoints<Sessions, SessionsDB>("/Sessions");
     
        app.MapEntityEndpoints<Status, StatusDB>("/Status");
     
        app.MapEntityEndpoints<TicketOrders, TicketOrdersDB>("/TicketOrders");
     
        app.MapEntityEndpoints<Tickets, TicketsDB>("/Tickets");
     
        app.Run();
    }
}
public static class EntityEndpoints
{
    public static void MapEntityEndpoints<TEntity, TDbContext>(this WebApplication app, string routePrefix)
      where TEntity : class, IEntity
      where TDbContext : DbContext
    {
        app.MapGet(routePrefix, async (TDbContext dbContext) =>
        {
            var entities = await dbContext.Set<TEntity>().ToListAsync();
            return entities;
        });

        app.MapGet($"{routePrefix}/{{ID}}", async (int id, TDbContext dbContext) =>
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity == null) return Results.NotFound();
            return Results.Ok(entity);
        });

        app.MapPost(routePrefix, async (TDbContext dbContext, TEntity entity) =>
        {
            dbContext.Set<TEntity>().Add(entity);
            await dbContext.SaveChangesAsync();
            return Results.Created($"{routePrefix}/{entity.Id}", entity);
        });

        app.MapPut($"{routePrefix}/{{ID}}", async (int id, TDbContext dbContext, TEntity entit) =>
        {
            var existingEntity = await dbContext.Set<TEntity>().FindAsync(id);
            if (existingEntity == null) return Results.NotFound();
            dbContext.Entry(existingEntity).CurrentValues.SetValues(entit);
            await dbContext.SaveChangesAsync();
            return Results.Ok(existingEntity);
        });

        app.MapDelete($"{routePrefix}/{{ID}}", async (int id, TDbContext dbContext) =>
        {
            if (await dbContext.Set<TEntity>().FindAsync(id) is TEntity entity)
            {
                dbContext.Set<TEntity>().Remove(entity);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}
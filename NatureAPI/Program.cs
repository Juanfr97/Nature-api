using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureAPI;
using NatureAPI.DTOS;
using NatureAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/nature", async (ApplicationDbContext context) =>
    {
        try
        {
            var natures = await context.Nature.ToListAsync();
            return Results.Ok(natures);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    })
    .WithName("GetNature")
    .WithOpenApi();

app.MapGet("/api/nature/{id}", async (ApplicationDbContext context, int id) =>
{
    try
    {
        var nature = await context.Nature.FindAsync(id);
        if (nature == null)
        {
            var message = new
            {
                Message = "Nature not found",
                Error = true
            };
            return Results.NotFound(message);
        }

        return Results.Ok(nature);
    }
    catch (Exception e)
    {
        var message = new
        {
            Message = e.Message,
            Error = true
        };
        return Results.BadRequest(message);
    }
});


app.MapPost("/api/nature", async (ApplicationDbContext context, [FromBody] NatureDTO nature) =>
{
    try
    {
        var newNature = new Nature
        {
            Title = nature.Title,
            Description = nature.Description,
            Image = nature.Image
        };
        await context.Nature.AddAsync(newNature);
        await context.SaveChangesAsync();
        return Results.Created($"/api/nature/{newNature.Id}", newNature);
    }
    catch (Exception e)
    {
        var message = new
        {
            Message = e.Message,
            Error = true
        };
        return Results.BadRequest(message);
    }
});

app.Run();

using BugTracker.Model;
using BugTracker.Model.Interfaces;
using BugTracker.Model.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BugTrackerDbContext>();

builder.Services.AddScoped<IBugRepo, BugRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IStaffRepo, StaffRepo>();
builder.Services.AddScoped<IInteractionRepo, InteractionRepo>();
builder.Services.AddScoped<INoteRepo, NoteRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Create the database tables if they don't exist
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BugTrackerDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapControllers();

app.Run();


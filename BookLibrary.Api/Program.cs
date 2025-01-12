using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<GoogleBooksService>();

builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BooksDbConnection")));
builder.Services.AddDbContext<LoginDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UserDataDbConnection")));

var connString = "Host=localhost;Port=5432;Database=books_db;Username=postgres;Password=your_password";
var connStringUserName = "Host=localhost;Port=5432;Database=userdata_db;Username=postgres;Password=your_password";

builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<LoginRepository>();
try
{
    using (var conn = new NpgsqlConnection(connString))
    {
        conn.Open();
        Console.WriteLine("Connected to PostgreSQL!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}try
{
    using (var conn = new NpgsqlConnection(connStringUserName))
    {
        conn.Open();
        Console.WriteLine("Connected to PostgreSQL!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development environment detected.");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
using CountryList.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DbcountryListContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CountryListContext"));
});
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

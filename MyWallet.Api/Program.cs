using Microsoft.EntityFrameworkCore;
using MyWallet.Api.Configurations;
using MyWallet.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<MyWalletContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyWalletDataBase"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<MyWalletContext>();
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

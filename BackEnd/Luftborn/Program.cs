using Luftborn.Data;
using Luftborn.Extensisons;
using Luftborn.Seed;

var builder = WebApplication.CreateBuilder(args);


builder.Services.addApplicationService(builder.Configuration);
builder.Services.AuthenticationServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var app = builder.Build();


// to seed the data for the first time  you can uncomment this block 
// and we create temp scope to handle the datacontext safely 

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         var context = services.GetRequiredService<DataContext>();
//         await Seed.SeedData(context);
//     }
//     catch (Exception ex)
//     {
//         var logger = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex, "error happened at the seed process.");
//     }
// }


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

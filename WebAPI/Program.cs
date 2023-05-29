using Todo.Application;
using Todo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen
(
    c =>
        c.ResolveConflictingActions(
            apiDescriptions => apiDescriptions.First())
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => 
        { o.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApplication Api v1"); });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
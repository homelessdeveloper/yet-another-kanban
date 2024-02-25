using Yak.Core.ErrorHandling;
using Yak.Core.Swashbuckle;
using Yak.Modules.Identity;
using Yak.Modules.Kanban;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwashbuckleModule();
builder.Services.AddIdentityModule();
builder.Services.AddKanbanModule();
builder.Services.AddExceptionHandler<ApplicationErrorHandler>();


var app = builder.Build();

// Must be empty.
app.UseExceptionHandler(_ => { });
await app.UseIdentityModule();
await app.UseKanbanModule();

app.UseHttpsRedirection();
app.UseCors("cors");
app.MapControllers();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();

// We need this to build WebApplicationFactory for API testing
public partial class Program
{
}

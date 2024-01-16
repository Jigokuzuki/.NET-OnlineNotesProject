using OnlineNotes.Api.Authorization;
using OnlineNotes.Api.Data;
using OnlineNotes.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepo(builder.Configuration);
builder.Services.AddRepo2(builder.Configuration);
builder.Services.AddRepo3(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(Policies.ReadAccess, builder => builder.RequireClaim("scope", "read"));
    option.AddPolicy(Policies.WriteAccess, builder => builder.RequireClaim("scope", "write").RequireRole("Admin"));

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://localhost:5238").AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

await app.Services.InitalizeDbAsync();

app.UseCors("AllowSpecificOrigin");



app.MapNotesEndpoints();
app.MapUsersEndpoints();
app.MapUserNotesEndpoints();
app.MapRazorPages();

app.Run();

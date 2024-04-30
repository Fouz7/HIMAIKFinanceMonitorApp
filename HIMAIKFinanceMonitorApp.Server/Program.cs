using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors((option) =>
//{
//    option.AddPolicy("DevCors", (corsBuilder) =>
//    {
//        corsBuilder.WithOrigins("http://localhost:3000", "http://localhost:4200", "http://localhost:8000", "https://localhost:7092")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });

//    option.AddPolicy("ProdCors", (corsBuilder) =>
//    {
//        corsBuilder.WithOrigins("https://example.com")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

string? tokenKeyString = builder.Configuration.GetSection("AppSettings:TokenKey").Value;
string? passwordKeyString = builder.Configuration.GetSection("AppSettings:PasswordKey").Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = tokenKeyString,
           ValidAudience = passwordKeyString,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKeyString ?? ""))
       };
    });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("Open");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("Open");
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

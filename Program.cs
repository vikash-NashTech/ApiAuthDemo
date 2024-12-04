var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer",options => 
    {
        options.Authority = builder.Configuration["OAuth2:Authority"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = builder.Configuration["OAuth2:Audience"],
            
        };
    });

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("ReadUsers", policy =>
        policy.RequireClaim("scope", "read:data")
    );
});
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();



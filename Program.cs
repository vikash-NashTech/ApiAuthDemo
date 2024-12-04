using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
)
.AddJwtBearer(options => 
{
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["OAuth2:Audience"],
        ValidIssuer = builder.Configuration["OAuth2:Authority"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]))  
    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            Console.WriteLine($"Token validated successfully: {context.SecurityToken}");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception}");
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            // Console.WriteLine($"Received Authorization Header: {context.Request.Headers["Authorization"]}");
            // return Task.CompletedTask;
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                context.Token = authHeader.Substring("Bearer ".Length).Trim();
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("ReadUsers", policy =>
        policy.RequireClaim("scope", "read")
    );
});
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   
app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"].ToString();
    Console.WriteLine($"Authorization Header: {authHeader}");
    await next();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();



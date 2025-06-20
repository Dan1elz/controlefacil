using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure.Repositories;
using ControleFacil.src.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ControleFacil.src.Application.Utils;
using ControleFacil.src.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ControleFacil.src.Application.Services;
namespace ControleFacil
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // *** CONFIGURAÇÃO DO DATABASE CONTEXT ***
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // *** CONFIGURAÇÃO DE CORS ***
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
            {
                policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));

            // *** CONFIGURAÇÃO DO SWAGGER ***
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
                });
            });

            // *** CONFIGURAÇÃO DE AUTENTICAÇÃO JWT ***
            var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.Configure<JwtUtils.JwtSettingsUseCase>(
                builder.Configuration.GetSection("JwtSettings"));

            // *** ADICIONANDO CONTROLLERS COM SUPORTE A NEWTONSOFT.JSON ***
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddEndpointsApiExplorer();

            // *** REGISTRO DE DEPENDÊNCIAS ***

            // *** CONTEXTO DE SERVICES ***
            builder.Services.AddScoped<AmbientesService>();
            builder.Services.AddScoped<ArmariosService>();
            builder.Services.AddScoped<FuncionariosService>();
            builder.Services.AddScoped<ItensService>();
            builder.Services.AddScoped<MovimentacoesService>();
            builder.Services.AddScoped<RefreshTokenService>();

            builder.Services.AddHttpContextAccessor();
            // *** CONTEXTO DE REPOSITORIES ***
            builder.Services.AddScoped<IAmbientesRepository, AmbientesRepository>();
            builder.Services.AddScoped<IArmariosRepository, ArmariosRepository>();
            builder.Services.AddScoped<IItensRepository, ItensRepository>();
            builder.Services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IMovimentacoesRepository, MovimentacoesRepository>();

            // *** UTILITARIOS ***
            builder.Services.AddScoped<JwtUtils>();

            // *** CONFIGURAÇÃO DO APP ***
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Executa Financeiro API V2");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

             app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors();
            app.UseRouting();

            // *** MIDDLEWARES ***
            app.UseMiddleware<GlobalExceptionMiddleware>();
            
            // *** AUTENTICAÇÃO E AUTORIZAÇÃO ***
            app.UseAuthentication();
            app.UseAuthorization();

            // *** MAPEAMENTO DE CONTROLLERS ***
            app.MapControllers();

            // *** EXECUÇÃO DA APLICAÇÃO ***
            app.Run();
        }
    }
}

// var builder = WebApplication.CreateBuilder(args);

// var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
// var url = $"http://0.0.0.0:{port}";
// var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

// var app = builder.Build();

// app.MapGet("/", () => $"Hello {target}!");

// app.Run(url);
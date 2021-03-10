using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSGCOlimpiada.Data.AcompanhamentoDAO;
using ApiSGCOlimpiada.Data.EscolaDAO;
using ApiSGCOlimpiada.Data.GrupoDAO;
using ApiSGCOlimpiada.Data.LogDAO;
using ApiSGCOlimpiada.Data.OcupacaoDAO;
using ApiSGCOlimpiada.Data.OcupacaoSolicitacaoCompraDAO;
using ApiSGCOlimpiada.Data.OrcamentoDAO;
using ApiSGCOlimpiada.Data.ProdutoDAO;
using ApiSGCOlimpiada.Data.ProdutoPedidoOrcamentoDAO;
using ApiSGCOlimpiada.Data.ResponsavelDAO;
using ApiSGCOlimpiada.Data.SolicitacaoCompraDAO;
using ApiSGCOlimpiada.Data.StatusDAO;
using ApiSGCOlimpiada.Data.TipoCompraDAO;
using ApiSGCOlimpiada.Data.UsuarioDAO;
using ApiSGCOlimpiada.Data.ProdutoSolicitacoesDAO;
using ApiSGCOlimpiada.Data.EmailDAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Coravel;
using System.Globalization;

namespace ApiSGCOlimpiada
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
            services.AddMailer(this.Configuration);
            services.AddTransient<IUsuarioDAO, UsuarioDAO>();
            services.AddTransient<IFuncaoDAO, FuncaoDAO>();
            services.AddTransient<IAcompanhamentoDAO, AcompanhamentoDAO>();
            services.AddTransient<IEscolaDAO, EscolaDAO>();
            services.AddTransient<IGrupoDAO, GrupoDAO>();
            services.AddTransient<ILogDAO, LogDAO>();
            services.AddTransient<IOcupacaoDAO, OcupacaoDAO>();
            services.AddTransient<IOcupacaoSolicitacaoCompraDAO, OcupacaoSolicitacaoCompraDAO>();
            services.AddTransient<IResponsavelDAO, ResponsavelDAO>();
            services.AddTransient<IProdutoDAO, ProdutoDAO>();
            services.AddTransient<IProdutoPedidoOrcamentoDAO, ProdutoPedidoOrcamentoDAO>();
            services.AddTransient<IOrcamentoDAO, OrcamentoDAO>();
            services.AddTransient<ISolicitacaoCompraDAO, SolicitacaoCompraDAO>();
            services.AddTransient<IStatusDAO, StatusDAO>();
            services.AddTransient<ITipoCompraDAO, TipoCompraDAO>();
            services.AddTransient<IProdutoSolicitacoesDAO, ProdutoSolicitacoesDAO>();
            services.AddTransient<IEmailDAO, EmailDAO>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSGCOlimpiada", Version = "v1" });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sistema-compras-olimpiadas-validacao-autenticacao")),
                    ValidIssuer = "ApiSGCOlimpiada",
                    ValidAudience = "ServerDino",
                };
                opt.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token Inválido " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token Válido " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiSGCOlimpiada v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSGCOlimpiada", Version = "v1" });
            });
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

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = "JwtBearer";
                opt.DefaultAuthenticateScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sistema-compras-olimpiadas-validacao-autenticacao")),
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidIssuer = "ApiSGCOlimpiada",
                    ValidAudience = "ServerDino",
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

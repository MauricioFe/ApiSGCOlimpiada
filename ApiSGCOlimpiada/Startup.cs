﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using ApiSGCOlimpiada.Data.ProdutoSolicitacoesDAO;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Swagger Implementation",
                    Description = "Api de gerênciamento de compras",
                    TermsOfService = "None"
                });
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
            services.AddTransient<IProdutoSolicitacoesDAO, ProdutoSolicitacoesDAO>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiSGCOlimpiada V1");
            });
            app.UseMvc();
        }
    }
}

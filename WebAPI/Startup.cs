using AutoMapper;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using BusinessLogicLayer;
using WebAPI.Authentication.JWE;
using WebAPI.Authentication.JWS;
using WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace WebAPI
{
    public class Startup
    {
        private readonly string _encodingSecurityKey;
        private readonly string _signingSecurityKey;
        private readonly string _connectionString;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();

            _encodingSecurityKey = Configuration["JWT:EncodingSecurityKey"];
            _signingSecurityKey = Configuration["JWT:SigningSecurityKey"];
            _connectionString = Configuration["ConnectionString"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var signingKey = new SigningSymmetricKey(_signingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

            var encryptionEncodingKey = new EncryptingSymmetricKey(_encodingSecurityKey);
            services.AddSingleton<IJwtEncryptingEncodingKey>(encryptionEncodingKey);

            //The connection string to database must be in the secrets.json file.
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_connectionString));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1,0);
                o.ReportApiVersions = true;
            });

            services.AddSwaggerDocument();

            const string jwtSchemeName = "JwtBearer";
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;
            var encryptingDecodingKey = (IJwtEncryptingDecodingKey)encryptionEncodingKey;
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),
                        TokenDecryptionKey = encryptingDecodingKey.GetKey(),

                        ValidateIssuer = true,
                        ValidIssuer = "SampleOfWebAPI",

                        ValidateAudience = true,
                        ValidAudience = "WebAPI",

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IPostService, PostService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseErrorHandlingMiddleware();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

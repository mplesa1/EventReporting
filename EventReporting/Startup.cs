using AutoMapper;
using EventReporting.Api.Infrastructure;
using EventReporting.BusinessLayer.AutoMapper;
using EventReporting.BusinessLayer.Services;
using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.DataAccessLayer.Repositories;
using EventReporting.Model.User;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventReporting
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
            services.AddMvcCore()
                    .AddApiExplorer();

            services.AddSwaggerDocumentation();

            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("databaseString"));
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<RabbitMqSettings>(Configuration.GetSection(nameof(RabbitMqSettings)));

            #region Services DI
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISettlementService, SettlementService>();
            services.AddScoped<IRabbitMQService, RabbitMQService>();
            services.AddScoped<IQueueSenderService, QueueSenderService>();
            services.AddHostedService<QueueInputSubscriber>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Repositories DI
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ISettlementRepository, SettlementRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            #endregion

            #region Autommaper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion


            var serviceProvider = services.BuildServiceProvider();
            var rabbitMQSettings = serviceProvider.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
            IOptions<RabbitMqSettings> rabbitMQSettingsOptions = Options.Create(rabbitMQSettings);
            IRabbitMQService rabbitMQService = new RabbitMQService(rabbitMQSettingsOptions);
            rabbitMQService.CreateQueues();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
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

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventReporting API");
            });
        }
    }
}

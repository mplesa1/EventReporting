using EventReporting.Api.Infrastructure;
using EventReporting.BusinessLayer.Services;
using EventReporting.DataAccessLayer.Persistence.Contexts;
using EventReporting.Model.User;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Infrastructure.Settings;
using EventReporting.Shared.Middlerwares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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

            services.ConfigureCors();

            services.AddSwaggerDocumentation();

            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("databaseString"));
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            services.ConfigureAuthetication(Configuration);
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<RabbitMqSettings>(Configuration.GetSection(nameof(RabbitMqSettings)));
            services.AddUrlHelper();
            services.RegisterServices();
            services.RegisterRepositories();
            services.AutoMapperConfig();

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

            app.UseExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
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

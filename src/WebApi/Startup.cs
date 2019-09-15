using Logic.Students;
using Logic.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Utils;

namespace WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Cqrs in Practice",
                        Version = "v1",
                        Description = "Example.",
                        Contact = new Contact
                        {
                            Name = "Ederson Lima",
                            Url = "https://github.com/sonederbr"
                        }
                    });
            });

            services.AddMvcCore()
                    .AddApiExplorer();


            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddSingleton<ICourseRepository, CourseRepository>();
            
            //services.AddTransient<ICommandHandler<EditPersonalInfoCommand>>(provider => 
            //    new AuditLogDecorator<EditPersonalInfoCommand>(
            //        new DatabaseRetryDecorator<EditPersonalInfoCommand>(
            //            new EditPersonalInfoCommandHandler(provider.GetService<IStudentRepository>())
            //        )
            //    )
            //);

            //services.AddTransient<ICommandHandler<RegisterCommand>, RegisterCommandHandler>();
            //services.AddTransient<ICommandHandler<UnregisterCommand>, UnregisterCommandHandler>();
            //services.AddTransient<ICommandHandler<TransferCommand>, TransferCommandHandler>();
            //services.AddTransient<ICommandHandler<EnrollCommand>, EnrollCommandHandler>();
            //services.AddTransient<ICommandHandler<DisenrollCommand>, DisenrollCommandHandler>();

            //services.AddTransient<IQueryHandler<GetListQuery, List<StudentDto>>, GetListQueryHandler>();

            services.AddSingleton<Messages>();
            services.AddHandlers();
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

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}

using EFCore.CodeFirst.WebApi.Contexts;
using EFCore.CodeFirst.WebApi.Helpers;
using EFCore.CodeFirst.WebApi.Mapping;
using EFCore.CodeFirst.WebApi.MiddleWares;
using EFCore.CodeFirst.WebApi.Repository;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using EFCore.CodeFirst.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace EFCore.CodeFirst.WebApi
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
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ) ;
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver = new DefaultContractResolver()
            );
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        "Server=tcp:efcorecodefirstwebapidbserver.database.windows.net,1433;Initial Catalog=EFCore.CodeFirst.WebApi_db;" +
            //        "Persist Security Info=False;User ID=anhpolu;Password=Invisible_0165790v2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
            //b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //        policy =>
            //        {
            //            policy.WithOrigins("https://*.ctequiz.com").AllowAnyHeader();
            //        });
            //});
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IQuestionTypeRepository, QuestionTypeRepository>();
            services.AddScoped<IQuestionTypeService, QuestionTypeService>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuestionCategoryRepository, QuestionCategoryRepository>();
            services.AddScoped<IQuestionCategoryService, QuestionCategoryService>();
            services.AddMvc(options => options.OutputFormatters.Add(new HtmlOutputFormatter())).AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddTokenAuthentication(Configuration);
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseOptions();
            //if (env.IsDevelopment())
            //{
               
            //}
            app.UseDeveloperExceptionPage();
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
            //app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

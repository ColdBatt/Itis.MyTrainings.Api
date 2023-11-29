using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Web;

public class Startup
{
    public IConfiguration Configuration;
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    // public void ConfigureServices(IServiceCollection services)
    // {
    //     services.AddDbContext<ApplicationContext>(options =>
    //         options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    //
    //     services.AddIdentity<User, IdentityRole>()
    //         .AddEntityFrameworkStores<ApplicationContext>();
    //              
    //     services.AddControllersWithViews();
    // }
 
    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();
 
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();  
        app.UseAuthorization();
 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
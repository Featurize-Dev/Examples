using Featurize.AspNetCore;

namespace WebApp.Features.App;

public class AppFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        services.AddCascadingAuthenticationState();
    }

    public void Use(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseAntiforgery();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }
}
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoalNewsV2.Startup))]
namespace GoalNewsV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

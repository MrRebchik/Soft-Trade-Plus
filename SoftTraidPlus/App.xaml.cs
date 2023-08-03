using SoftTradePlus.Model;
using System.Windows;

namespace SoftTradePlus
{
    public partial class App
    {
        private Bootstrap _bootstrap;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            _bootstrap = new Bootstrap();

            _bootstrap.Run();
        }
    }
}

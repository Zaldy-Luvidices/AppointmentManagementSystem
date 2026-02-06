using AppointmentManagementSystem.Client.Services;
using AppointmentManagementSystem.Client.ViewModels;
using AppointmentManagementSystem.Client.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Windows;

namespace AppointmentManagementSystem.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; private set; }

        public App()
        {
            var services = new ServiceCollection();

            // Logging
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            });

            // Register HttpClient with API base URL
            services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://localhost:5001/") });
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            Services = services.BuildServiceProvider();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = Services.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }
    }
}

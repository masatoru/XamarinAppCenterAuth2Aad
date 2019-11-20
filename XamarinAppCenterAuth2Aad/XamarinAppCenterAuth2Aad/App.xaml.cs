using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAppCenterAuth2Aad
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios={Your AppCenter Key};" +
                            "uwp={Your UWP App secret here};" +
                            "android={Your Android App secret here}",
                typeof(Analytics),
                typeof(Crashes),
                typeof(Auth));

            var isEnable = Auth.IsEnabledAsync().Result;
            Console.WriteLine($"Auth IsEnabled={isEnable}");

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

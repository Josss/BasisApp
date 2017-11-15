using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BasisApp
{
    using System.Threading;

    using Android.App;
    using Android.OS;

    [Activity(Label = "Mijn BasisApp", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, Icon = "@drawable/splash")]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _database db = new _database(this);

            Thread.Sleep(2000);
            StartActivity(typeof(Home));
        }
    }
}

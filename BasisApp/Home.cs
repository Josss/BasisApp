using Android.App;
using Android.Widget;
using Android.OS;

namespace BasisApp
{
    [Activity(Label = "BasisApp", MainLauncher = true, Icon = "@drawable/splash")]
    public class Home : Activity
    {
        int count = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Home);

            Button button = FindViewById<Button> (Resource.Id.myButton);

            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };
        }
    }
}


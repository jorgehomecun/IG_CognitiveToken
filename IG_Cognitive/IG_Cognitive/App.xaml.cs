using IG_Cognitive.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IG_Cognitive
{
	public partial class App : Application
	{
		public static MasterDetailPage MasterDetail { get; set; }
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new ViewCam_Gallery());

		}

		protected override void OnStart()
		{
			// Handle when your app starts
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

namespace IG_Cognitive.View
{
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewCam_Gallery : ContentPage
	{
		public ViewCam_Gallery ()
		{
			InitializeComponent ();

			BindingContext = new ViewModels.ViewModelsCam_Gallery();

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}
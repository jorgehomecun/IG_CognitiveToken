namespace IG_Cognitive.View
{
	using Acr.UserDialogs;
	using IG_Cognitive.ConectionServices;
	using IG_Cognitive.Models;
	using Newtonsoft.Json;
	using System;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Text;
	using System.Threading.Tasks;
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebView : ContentPage
	{
		string token = "";
		public WebView()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			if (token == "")
			{
				using (HttpClient httpclient = new HttpClient())
				{
					httpclient.BaseAddress = new Uri("https://66d077c9.ngrok.io");

					user sett = new user()
					{
						userName = "065399b83e51bac411059d12ec42a2d4",
						userPassword = "22d1c2e80fbcec3a7aff5d3436709e94"
					};

					var json = JsonConvert.SerializeObject(sett);
					var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
					var response = await httpclient.PostAsync("/Authentication/Login", stringContent);
					var resultContent = await response.Content.ReadAsStringAsync();
					if (response.IsSuccessStatusCode)
					{
						if (resultContent != null)
						{
							var posts = JsonConvert.DeserializeObject<AuthenticationApi>(resultContent);
							token = posts.authentication;
						}
					}
				}
			}

			using (HttpClient httpclient = new HttpClient())
			{
				httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				waitActivityIndicator.IsRunning = true;
				txt.IsVisible = true;
				var json = await httpclient.GetStringAsync(ConectionBack.BackUrl);
				var urlcont = Clasificar(json);
				waitActivityIndicator.IsRunning = false;
				txt.IsVisible = false;

				if (urlcont.Contains("https") || urlcont.Contains("www") || urlcont.Contains("http"))
				{
					webview.Source = urlcont;
					webview.IsVisible = true;
				}
				else
				{
					textview.Text = urlcont;
					textview.IsVisible = true;
				}
			}
		}

		public string Clasificar(string json)
		{
			string urlcont = "";

			var posts = JsonConvert.DeserializeObject<ContainerApi>(json);

			foreach (var container in posts.ResultData)
			{
				if (idcustomer.idcustom == container.Id)
				{
					urlcont = container.Contenido;
				}
			}

			token = posts.Authorization;
			return urlcont;
		}
	}
}
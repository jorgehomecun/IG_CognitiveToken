namespace IG_Cognitive.ViewModels
{
    using Acr.UserDialogs;
    using IG_Cognitive.ConectionServices;
    using IG_Cognitive.Models;
    using Newtonsoft.Json;
    using Plugin.Media.Abstractions;
    using System.Linq;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using WebView = View.WebView;

    public class ViewModelsCam_Gallery : BindableObject
    {
        #region Atributos
        private MediaFile Photo;
        public Command m_DisplayActionSheetComand;
        public Command m_SearchCustomVisionComand;
        #endregion

        public static readonly BindableProperty RoutePhotoProperty = BindableProperty.Create("RoutePhoto",
        typeof(string), typeof(ViewModelsCam_Gallery), default(string));

        public static readonly BindableProperty ResultPhotoProperty = BindableProperty.Create("Result",
        typeof(string), typeof(ViewModelsCam_Gallery), default(string));

        #region Properties
        public ImageSource RoutePhoto
        {
            get
            {
                return (string)GetValue(RoutePhotoProperty);
            }
            set
            {
                SetValue(RoutePhotoProperty, value);
            }
        }

        public string Result
        {
            get
            {
                return (string)GetValue(ResultPhotoProperty);
            }
            set
            {
                SetValue(ResultPhotoProperty, value);
            }
        }

        private async Task DisplayActionSheetAsync()
        {
            var source = await Application.Current.MainPage.DisplayActionSheet("Seleccionar Accion", "Cancelar",
                null, "Galeria", "Camara");

            if (source == "Camara")
            {
                Photo = await ModelsCam_Gallery.Instance.CapturePhotoAsync();
                if (Photo != null)
                {
                    RoutePhoto = Photo.Path;
                }

                else
                {
                    RoutePhoto = "no_product2.png";
                    return;
                }
            }

            else if (source == "Galeria")
            {
                Photo = await ModelsCam_Gallery.Instance.SelectPhotoAsync();
                if (Photo != null)
                {
                    RoutePhoto = Photo.Path;
                }
                else
                {
                    RoutePhoto = "no_product2.png";
                    return;
                }
            }
        }

        private async Task SearchCustomVisionComandAsync()
        {
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Add("Prediction-Key", ConectionCustomVisionUrlKey.PredictionKey);

                if (Photo == null)
                {
                    UserDialogs.Instance.Toast("Favor Ingresar Imagen.");
                    return;
                }
                else
                {
                    var contentStream = new StreamContent(Photo.GetStream());

                    using (UserDialogs.Instance.Loading("Cargando...", null, null, true, MaskType.Black))
                    {
                        var current = Xamarin.Essentials.Connectivity.NetworkAccess;

                        if (current == Xamarin.Essentials.NetworkAccess.Internet)
                        {
                            var url = ConectionCustomVisionUrlKey.PredictionUrl;
                            var response = await httpclient.PostAsync(url, contentStream);

                            if (!response.IsSuccessStatusCode)
                            {
                                UserDialogs.Instance.Toast("Ha ocurrido un error.!!!");
                                return;
                            }
                            var json = await response.Content.ReadAsStringAsync();
                            var prediction = JsonConvert.DeserializeObject<PredictionResponse>(json);
                            var tag = prediction.predictions.First();
                            idcustomer.idcustom = int.Parse(tag.tagName);

                            if (tag.probability >= 0.8)
                            {
                                var confirm = new ConfirmConfig
                                {
                                    Title = "IG_Cognitive",
                                    Message = "Está imagen tiene asociada un link desea ir?",
                                    OkText = "SI",
                                    CancelText = "NO"
                                };

                                if (await UserDialogs.Instance.ConfirmAsync(confirm) == true)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new WebView());
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                UserDialogs.Instance.Toast("Imagen con probabilidad muy baja de ser reconocida, " +
                                    "favor buscar nueva imagen.", TimeSpan.FromMilliseconds(5000));
                                return;
                            }
                        }
                        else
                        {
                            UserDialogs.Instance.Toast("Verificar conexion a internet e intentelo nuevamente.");
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region Commands
        public Command DisplayActionSheetComand
        {
            get
            {
                return (m_DisplayActionSheetComand ?? (m_DisplayActionSheetComand = new Command(async () =>
                await DisplayActionSheetAsync())));
            }
        }

        public Command SearchCustomVisionComand
        {
            get
            {
                return (m_SearchCustomVisionComand ?? (m_SearchCustomVisionComand = new Command(async () =>
                await SearchCustomVisionComandAsync())));
            }
        }
        #endregion

        #region Constructors
        public ViewModelsCam_Gallery()
        {
            RoutePhoto = "no_product2.png";
        }
        #endregion
    }
}

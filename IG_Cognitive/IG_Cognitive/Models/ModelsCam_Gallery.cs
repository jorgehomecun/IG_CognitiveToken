namespace IG_Cognitive.Models
{
	using System.Threading.Tasks;
	using Plugin.Media;
	using Plugin.Media.Abstractions;
	public class ModelsCam_Gallery
    {
		public ModelsCam_Gallery()
		{
			CrossMedia.Current.Initialize();
		}

		private static ModelsCam_Gallery m_Instance;

		public static ModelsCam_Gallery Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new ModelsCam_Gallery();
				}

				return m_Instance;
			}
		}

		public async Task<MediaFile> SelectPhotoAsync()
		{
			MediaFile photo = null;
			try
			{
				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					return null;
				}
				else
				{
					photo = await CrossMedia.Current.PickPhotoAsync();
				}

			}
			catch (TaskCanceledException)
			{
				photo = null;
			}
			return photo;
		}

		public async Task<MediaFile> CapturePhotoAsync()
		{
			MediaFile photo = null;
			try
			{
				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					return null;
				}
				else
                {
                    photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                    {
                        Directory = "clasificator",
                        Name = "source.jpg",
                        PhotoSize = PhotoSize.Small
                    });
                }

            }
			catch (TaskCanceledException)
			{
				photo = null;
			}
			return photo;
		}
	}
}

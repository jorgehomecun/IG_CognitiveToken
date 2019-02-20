namespace IG_Cognitive.Models
{
	public class user
	{
		public string userName { get; set; }
		public string userPassword { get; set; }
	}

	public partial class AuthenticationApi
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public string authentication { get; set; }
	}

}

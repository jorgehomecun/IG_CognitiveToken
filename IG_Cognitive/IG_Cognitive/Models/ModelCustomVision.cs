using System;
using System.Collections.Generic;
using System.Text;

namespace IG_Cognitive.Models
{
	public class ModelCustomVision
	{
		public float probability { get; set; }
		public string tagId { get; set; }
		public string tagName { get; set; }
	}

	public class PredictionResponse
	{
		public string id { get; set; }
		public string project { get; set; }
		public string iteration { get; set; }
		public DateTime created { get; set; }
		public ModelCustomVision[] predictions { get; set; }
	}
}

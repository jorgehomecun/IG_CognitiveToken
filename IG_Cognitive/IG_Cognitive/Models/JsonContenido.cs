namespace IG_Cognitive.ConectionServices
{
	using System;
	using System.Collections.Generic;

	public partial class ContainerApi
	{
		public int ResponseCode { get; set; }
		public string ResponseMessage { get; set; }
		public int RowsAffected { get; set; }
		public object IdTransactionCode { get; set; }
		public List<ResultDatum> ResultData { get; set; }
		public string Authorization { get; set; }
	}


	public partial class ResultDatum
	{
		public long Id { get; set; }
		public string Contenido { get; set; }
		public string Tipo { get; set; }
		public DateTimeOffset FechaCreacion { get; set; }
		public DateTimeOffset FechaModificacion { get; set; }
		public bool Activo { get; set; }
	}

	public static class idcustomer
	{
		public static int idcustom { get; set; }
	}

}

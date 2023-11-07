using System;
using THY.GatePlanner.Model.Enums;

namespace THY.GatePlanner.Model.Responses
{
	public class GetGatesResponse
	{ 
		public string code { get; set; }
        public string id { get; set; }
        public SizeEnum size { get; set; }
        public string location { get; set; } //"x:y"

    }
}


 
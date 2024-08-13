using System;
using THY.GatePlanner.Model.Enums;

namespace THY.GatePlanner.Model.Responses
{
	public class GetGatesResponse
	{ 
		public string Code { get; set; }
        public Guid Id { get; set; }
        public SizeEnum Size { get; set; }
        public string Location { get; set; } //"x:y"
        public int GateStatus { get; set; }

    }
}


 
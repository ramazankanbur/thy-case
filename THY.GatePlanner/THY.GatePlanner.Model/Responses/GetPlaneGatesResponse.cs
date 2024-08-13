using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THY.GatePlanner.Model.Enums;

namespace THY.GatePlanner.Model.Responses
{
    public class GetPlaneGatesResponse
    {
        public string GateCode { get; set; }
        public Guid GateId { get; set; }
        public int GateSize { get; set; }
        public string GateLocation { get; set; } //"x:y"
        public int GateStatus { get; set; }
        public int PassengerOffboardingDuration { get; set; }
    }
}

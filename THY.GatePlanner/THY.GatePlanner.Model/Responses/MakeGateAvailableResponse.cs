using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THY.GatePlanner.Model.Enums;

namespace THY.GatePlanner.Model.Responses
{
    public class MakeGateAvailableResponse
    {
        public Guid PlaneId { get; set; }
        public Guid GateId { get; set; }
        public PlaneStatus PlaneStatus { get; set; }
        public GateStatus GateStatus { get; set; }

    }
}

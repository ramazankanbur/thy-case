using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THY.GatePlanner.Model.Responses
{
    public class AssignGateToPlaneResponse
    {
        public Guid PlaneId { get; set; }
        public Guid GateId { get; set; }
    }
}

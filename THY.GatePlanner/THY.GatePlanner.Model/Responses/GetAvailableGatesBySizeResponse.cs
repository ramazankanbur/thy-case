using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THY.GatePlanner.Model.Responses
{
    public class GetAvailableGatesBySizeResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }

    }
}

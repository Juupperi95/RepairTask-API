using System;
using System.Collections.Generic;
using System.Text;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class RepairTaskPost
    {
        public int DeviceId { get; set; }
        public string Description { get; set; }
        public int Criticality { get; set; }
        public int Completed { get; set; }
    }
}


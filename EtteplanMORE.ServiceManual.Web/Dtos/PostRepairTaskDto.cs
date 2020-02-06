using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EtteplanMORE.ServiceManual.Web.Dtos
{
    public class PostRepairTaskDto
    {
        public int DeviceId { get; set; }
        public string Description { get; set; }
        public int Criticality { get; set; }
        public int Completed { get; set; }
    }
}

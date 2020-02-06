using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EtteplanMORE.ServiceManual.Web.Dtos
{
    public class RepairTaskDto
    {
        public int TaskId { get; set; }
        public int DeviceId { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public int Criticality { get; set; }
        public int Completed { get; set; }
    }
}

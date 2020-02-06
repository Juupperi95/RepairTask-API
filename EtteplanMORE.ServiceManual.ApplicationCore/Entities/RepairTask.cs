using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class RepairTask
    {
        public int TaskId { get; set; }
        public int DeviceId { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public int Criticality { get; set; }
        public  int Completed { get; set; }
    }
}

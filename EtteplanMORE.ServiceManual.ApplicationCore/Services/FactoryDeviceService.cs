using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.DBContexts;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class FactoryDeviceService : IFactoryDeviceService
    { 
        private readonly DatabaseContext _dbContext;

        public FactoryDeviceService(DatabaseContext DbContext)
        {
            _dbContext = DbContext;
        }
        /// <summary>
        /// Get all factorydevices that are stored in db in ascending Id order
        /// </summary>
        public async Task<IEnumerable<FactoryDevice>> GetAll()
        {
            var devices = await Task.FromResult(_dbContext.FactoryDevices.OrderBy(d => d.Id).ToList());
            return devices;
        }
        /// <summary>
        /// Find factory device from db by id and return it
        /// </summary>
        public async Task<FactoryDevice> Get(int id)
        {
            var device = await Task.FromResult(_dbContext.FactoryDevices.FirstOrDefault(c => c.Id == id));
            return device;
        }
    }
}
using ECommMVC.BL.Abstact;
using ECommMVC.DAL.Abstact;
using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Concrete
{
    public class ShipperManager : GenericManager<Shipper>, IShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperManager(IShipperRepository shipperRepository) : base(shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }
    }
}

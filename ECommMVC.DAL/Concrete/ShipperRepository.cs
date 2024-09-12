using ECommMVC.DAL.Abstact;
using ECommMVC.DAL.Context;
using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Concrete
{
    public class ShipperRepository: GenericRepository<Shipper>, IShipperRepository
    {
        public ShipperRepository(ECommContext context) : base(context)
        {
            
        }
    }
}

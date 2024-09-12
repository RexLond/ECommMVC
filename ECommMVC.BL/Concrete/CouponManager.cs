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
    public class CouponManager : GenericManager<Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponManager(ICouponRepository copunRepository) : base(copunRepository)
        {
            _couponRepository = copunRepository;
        }
    }
}

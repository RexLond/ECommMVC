using ECommMVC.BL.Abstact;
using ECommMVC.DAL.Abstact;
using ECommMVC.DAL.Concrete;
using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Concrete
{
    public class PaymentManager : GenericManager<Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentManager(IPaymentRepository paymentRepository) : base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> GetByIdWithRefAsync(int id)
        {
            return await _paymentRepository.GetByIdWithRefAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetAllWithRefAsync()
        {
            return await _paymentRepository.GetAllWithRefAsync();
        }
    }
}

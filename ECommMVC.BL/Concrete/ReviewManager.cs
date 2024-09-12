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
    public class ReviewManager : GenericManager<Review>, IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewManager(IReviewRepository reviewRepository) : base(reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> GetByIdWithRefAsync(int id)
        {
            return await _reviewRepository.GetByIdWithRefAsync(id);
        }

        public async Task<IEnumerable<Review>> GetAllWithRefAsync()
        {
            return await _reviewRepository.GetAllWithRefAsync();
        }
    }
}

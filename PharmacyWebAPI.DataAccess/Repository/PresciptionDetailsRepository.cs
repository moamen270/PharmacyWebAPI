using PharmacyWebAPI.Models.Dto;

namespace PharmacyWebAPI.DataAccess.Repository
{
    public class PresciptionDetailsRepository : Repository<PrescriptionDetails>, IPrescriptionDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public PresciptionDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<OrderDetailsDto> PrescriptionDetailsToOrderDetails(List<PrescriptionDetails> prescriptionDetails)
        {
            var details = new List<OrderDetailsDto>();
            foreach (var item in prescriptionDetails)
            {
                details.Add(new OrderDetailsDto
                {
                    Count = 1,
                    DrugId = item.DrugId,
                });
            }
            return details;
        }
    }
}
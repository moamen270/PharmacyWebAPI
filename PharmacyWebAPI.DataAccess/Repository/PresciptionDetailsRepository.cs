namespace PharmacyWebAPI.DataAccess.Repository
{
    public class PresciptionDetailsRepository : Repository<PrescriptionDetails>, IPrescriptionDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public PresciptionDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
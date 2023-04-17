using PharmacyWebAPI.Models.Dto;

namespace PharmacyWebAPI.DataAccess.Repository.IRepository
{
    public interface IPrescriptionDetailsRepository : IRepository<PrescriptionDetails>
    {
        List<OrderDetailsDto> PrescriptionDetailsToOrderDetails(List<PrescriptionDetails> prescriptionDetails);

        Task SetPresciptionId(int PresciptionId, List<PrescriptionDetails> details);
    }
}
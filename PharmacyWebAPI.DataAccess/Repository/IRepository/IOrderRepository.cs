﻿namespace PharmacyWebAPI.DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

        void UpdateStripePaymentID(int id, string paymentItentId);
    }
}
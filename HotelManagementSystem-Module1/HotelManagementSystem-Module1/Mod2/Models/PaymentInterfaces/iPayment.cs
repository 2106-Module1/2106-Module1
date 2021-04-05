using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.Models.PaymentInterfaces
{
    public interface iPayment<AbstractPayment>
    {
        IEnumerable<AbstractPayment> retrieveAll();
        AbstractPayment retrieveByID(int id);
        void addPayment(AbstractPayment payment);
        void updatePayment(AbstractPayment payment);
        void deletePayment(int id);
    }
}

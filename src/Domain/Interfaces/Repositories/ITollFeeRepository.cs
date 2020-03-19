using System;

namespace Domain.Interfaces.Repositories
{
    public interface ITollFeeRepository
    {
        int GetTollFee(DateTime date);
    }
}

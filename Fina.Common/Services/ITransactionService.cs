using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;

namespace Fina.Common.Services
{
    public interface ITransactionService
    {
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
        Task<Response<Transaction?>> GetByIdAsync(GetByIdTransactionRequest request);
        Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetByPeriodTransactionRequest request);
    }
}
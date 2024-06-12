using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Common.Requests.Transactions
{
    public class GetByPeriodTransactionRequest : PagedRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
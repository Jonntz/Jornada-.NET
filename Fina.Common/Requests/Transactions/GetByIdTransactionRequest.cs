using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Common.Requests.Transactions
{
    public class GetByIdTransactionRequest : Request
    {
        public long Id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fina.Common.Enums;

namespace Fina.Common.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required(ErrorMessage = "Titulo Inválido!")]
        [MaxLength(80, ErrorMessage = "O titulo deve ter no máximo 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo Inválido!")]
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

        [Required(ErrorMessage = "Valor Inválido!")]
        public decimal Amount { get; set; } 

        [Required(ErrorMessage = "Categoria Inválida!")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data Inválida!")]
        public DateTime? CreatedAt { get; set; }

        [Required(ErrorMessage = "Data Inválida!")]
        public DateTime? PaidOrreceivedAt { get; set; }
        
    }
}
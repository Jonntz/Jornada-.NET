using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Common.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Titulo Inválido!")]
        [MaxLength(80, ErrorMessage = "O titulo deve ter no máximo 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;
    }
}
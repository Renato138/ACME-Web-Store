using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Business.Models
{
    public enum UnidadeVenda
    {
        [Display(Name ="Peça")]
        Peca,

        [Display(Name = "Caixa")]
        Caixa,

        [Display(Name = "Quilo")]
        Quilo,

        [Display(Name = "Litro")]
        Litro,

        [Display(Name = "Metro")]
        Metro,

        [Display(Name = "Metro Quadrado")]
        MetroQuadrado,

        [Display(Name = "Metro Cúbico")]
        MetroCubico
    }
}

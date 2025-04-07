using System.ComponentModel.DataAnnotations;

namespace Acme.Store.UI.Mvc.Models
{
    public class SelectListViewModel
    {
        [Key]
        public Guid? Id { get; set; }
        public string Nome { get; set; }
    }
}

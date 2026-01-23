using System.ComponentModel.DataAnnotations;

namespace WorkshopManager.Web.ViewModels
{
    public class ClienteCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Telefono { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}

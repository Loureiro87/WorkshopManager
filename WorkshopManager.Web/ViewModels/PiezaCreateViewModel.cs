using System.ComponentModel.DataAnnotations;

namespace WorkshopManager.Web.ViewModels
{
    public class PiezaCreateViewModel
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="La referencia es obligatoria")]
        [MaxLength(50)]
        public string Referencia { get; set; }
        [Required(ErrorMessage ="El precio es obligatorio")]
        [Range(0,double.MaxValue, ErrorMessage ="El precio no puede ser negativo")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage ="El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage ="El stock no puede ser negativo")]
        public int Stock { get; set; }
    }
}
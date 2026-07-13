using System.ComponentModel.DataAnnotations;

namespace Veterinaria.ViewModels.Cliente
{
    public class ClienteEditVM : ClienteCreateVM
    {
        [Required]
        public int IdCliente { get; set; }
    }
}

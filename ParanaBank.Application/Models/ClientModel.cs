using System.ComponentModel.DataAnnotations;

namespace ParanaBank.Application.Models
{
    public class ClientModel
    {
        [Required(ErrorMessage = "{0} can't be null")]
        public string User { get; set; }

        [Required(ErrorMessage = "{0} can't be null")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em format invalid.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "E-mail em format invalid.")]
        public string Email { get; set; }
    }
}

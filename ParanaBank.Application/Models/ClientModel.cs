using System.ComponentModel.DataAnnotations;

namespace ParanaBank.Application.Models
{
    public class ClientModel
    {
        [Required(ErrorMessage = "{0} can't be null")]
        public string User { get; set; }

        [Required(ErrorMessage = "{0} can't be null")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em format invalid.")]
        [RegularExpression(@"b[A-Z0-9._%-]+@[A-Z0-9.-]+.[A-Z]{2,4}b", ErrorMessage = "E-mail em format invalid.")]
        public string Email { get; set; }
    }
}

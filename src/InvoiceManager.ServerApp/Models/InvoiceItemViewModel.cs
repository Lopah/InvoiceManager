using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.ServerApp.Models
{
    public class InvoiceItemViewModel
    {
        [Key, Display(AutoGenerateField = false)]
        public int Id { get; set; }
        [Display(Description = "Name of an item")]
        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "The {1} must be at least {0} characters long.")]
        public string Name { get; set; }

        [Display(Description = "Price of an item")]
        [Required]
        [DefaultValue((double)0)]
        public double Price { get; set; }

        /// <summary>
        /// The parent Invoice id
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int InvoiceId { get; set; }
    }
}

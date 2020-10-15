namespace TecTkt.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pais
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [StringLength(10)]

        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string IntrastatCode { get; set; }
        
        [Display(Name = "VAT Digits")]
        public int VatDigits { get; set; }

        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImagePath { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

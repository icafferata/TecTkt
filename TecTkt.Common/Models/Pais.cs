namespace TecTkt.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pais
    {
        [Key]
        public int PaisId { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Nombre {get; set; }
        public string Intrastat { get; set; }
        public int DigitosCif { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}

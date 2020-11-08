namespace TecTkt.Backend.Models
{
    using System.Web;
    using Common.Models;

    public class PaisView : Pais
    {
        public HttpPostedFileBase  ImageFile { get; set; }
    }
}
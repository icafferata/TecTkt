namespace TecTkt.Domain.Models
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<TecTkt.Common.Models.Pais> Pais { get; set; }
    }
}

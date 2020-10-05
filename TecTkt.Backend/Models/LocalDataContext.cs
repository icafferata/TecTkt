namespace TecTkt.Backend.Models
{
    using Domain.Models;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<TecTkt.Common.Models.Pais> Pais { get; set; }
    }
}
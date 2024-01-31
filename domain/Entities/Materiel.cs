namespace domain.Entities
{
    public class Materiel : IEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ServiceDat { get; set; }
        public DateTime EndGarantee { get; set; }
        public Category Cat { get; set; }
        public List<Materiel> Materiels { get; set; }
        public int? ProprietaireId { get; set; }
        public DateTime? lastUpdate { get; set; }
        public List<Category> categories { get; set; }

    }
}

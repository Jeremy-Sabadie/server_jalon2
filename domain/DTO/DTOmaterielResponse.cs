using domain.Entities;

namespace domain.DTO
{
    //Class for the data transfert of the materiel objects responses.
    public class DTOmaterielResponse : IEquatable<DTOmaterielResponse?>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ServiceDat { get; set; }
        public DateTime EndGarantee { get; set; }
        public int? proprietaireId { get; set; }
        public string proprietaireName { get; set; }
        public List<Category> categories { get; set; }
        public DateTime LastUpdate { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DTOmaterielResponse);
        }

        public bool Equals(DTOmaterielResponse? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   ServiceDat == other.ServiceDat &&
                   EndGarantee == other.EndGarantee &&
                   proprietaireId == other.proprietaireId &&
                   proprietaireName == other.proprietaireName &&
                   EqualityComparer<List<Category>>.Default.Equals(categories, other.categories) &&
                   LastUpdate == other.LastUpdate;
        }
    }
}

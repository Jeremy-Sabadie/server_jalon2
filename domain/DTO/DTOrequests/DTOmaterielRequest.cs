using FluentValidation;
namespace domain.DTO.Requests
{
    //Class for the data transfert of the materiel objects request.
    public class DTOmaterielRequest : IEquatable<DTOmaterielRequest?>
    {

        public string Name { get; set; }
        public DateTime serviceDat { get; set; }
        public DateTime endGarantee { get; set; }
        public int? proprietaireId { get; set; }
        public List<int> categories { get; set; }

        public DateTime? LastUpdate { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DTOmaterielRequest);
        }

        public bool Equals(DTOmaterielRequest? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   serviceDat == other.serviceDat &&
                   endGarantee == other.endGarantee &&
                   proprietaireId == other.proprietaireId &&
                   EqualityComparer<List<int>>.Default.Equals(categories, other.categories) &&
                   LastUpdate == other.LastUpdate;
        }

        public static bool operator ==(DTOmaterielRequest? left, DTOmaterielRequest? right)
        {
            return EqualityComparer<DTOmaterielRequest>.Default.Equals(left, right);
        }

        public static bool operator !=(DTOmaterielRequest? left, DTOmaterielRequest? right)
        {
            return !(left == right);
        }
    }
    //Materiel object request Validator.
    internal class DTOrequestMaterielValidator : AbstractValidator<DTOmaterielRequest>
    {
        public DTOrequestMaterielValidator()
        {
            //Object Name property must not be Empty to validate the Material request.
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.serviceDat).NotEmpty();
            RuleFor(x => x.endGarantee).NotEmpty();
        }
    }
}

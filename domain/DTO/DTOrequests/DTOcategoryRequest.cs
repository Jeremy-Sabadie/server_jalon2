using FluentValidation;
namespace domain.DTO.Requests
{
    internal class DTOcategoryRequest
    {
        public int Reference { get; set; }
        public string Name { get; set; }

    }
    internal class DTOrequestCategoryValidator : AbstractValidator<DTOcategoryRequest>
    {
        //Materiel object request Validator.
        public DTOrequestCategoryValidator()
        {
            //Name object property must not be empty .
            RuleFor(x => x.Name).NotEmpty();

        }
    }

}

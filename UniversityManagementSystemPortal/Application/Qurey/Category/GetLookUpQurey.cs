using MediatR;
using Org.BouncyCastle.Crypto.Parameters;
using UniversityManagementSystemPortal.Models.ModelDto.Category;

namespace UniversityManagementSystemPortal.Application.Qurey.Category
{
    public record GetLookUpQurey(LookupcategoryDto Lookupcategory) : IRequest<List<LookupcategoryDto>>;
 
}

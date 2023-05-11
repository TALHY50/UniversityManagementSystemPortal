using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Category;
using UniversityManagementSystemPortal.Application.Qurey.Department;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Category;
using UniversityManagementSystemPortal.Models.ModelDto.Department;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Application.Handler.Category
{
    public class GetLookUpHandler :  IRequestHandler<GetLookUpQurey, List<LookupcategoryDto>>
    {
         private readonly ICategoryRepository _repository;
        public GetLookUpHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<LookupcategoryDto>> Handle(GetLookUpQurey request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAllAsync();

            if (category == null)
            {
                return null;
            }

            var LookupList = category
               .Select(d => new LookupcategoryDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();

            return LookupList;
        }
    }
}

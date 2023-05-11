using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityManagementSystemPortal.Application.Handler.Position;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Application.Handler.Postion;
using UniversityManagementSystemPortal.Models.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Handler.Position
{
    public class GetLookupPositionListHandler : IRequestHandler<GetLookupPositionList, List<LookupPositiondto>>
    {
        private readonly IPositionRepository _positionRepository;

        public GetLookupPositionListHandler(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<List<LookupPositiondto>> Handle(GetLookupPositionList request, CancellationToken cancellationToken)
        {
            var positions = (await _positionRepository.GetAllAsync()).AsQueryable();

            var positionsList = await positions.ToListAsync();

            var result = new List<LookupPositiondto>();

            foreach (var position in positionsList)
            {
                result.Add(new LookupPositiondto
                {
                    Id = position.Id,
                    Name = position.Name
                });
            }

            return result;
        }
    }
}

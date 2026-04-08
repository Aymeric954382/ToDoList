using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByOverdueToDos
{
    public class GetToDoListByOverdueQueryHandler 
        : IRequestHandler<GetToDoListOverdueQuery, 
        ServiceResult<GetToDoListByOverdueResponseDto>>
    {
        public readonly IToDoRepository _repository;

        public readonly IMapper _mapper;
        public GetToDoListByOverdueQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<GetToDoListByOverdueResponseDto>> Handle(
            GetToDoListOverdueQuery request, 
            CancellationToken cancellationToken)
        {
            var filtered = new ToDoFilter()
            {
                IsOverdue = true
            };

            try
            {
                var resultFiltered = await _repository.GetByFilterAsync(filtered, cancellationToken);

                var itemsDto = _mapper.Map<List<ToDoItemDto>>(resultFiltered);

                var response = new GetToDoListByOverdueResponseDto()
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListByOverdueResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListByOverdueResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }

        }
    }
}

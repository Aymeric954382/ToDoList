using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQueryHandler 
        : IRequestHandler<GetToDoListQuery, 
            ServiceResult<GetToDoListResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        public GetToDoListQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ServiceResult<GetToDoListResponseDto>> Handle(
            GetToDoListQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetListByUserIdAsync(request.UserId, cancellationToken);

                var itemsDto = _mapper.Map<List<ToDoItemDto>>(result);

                var response = new GetToDoListResponseDto()
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListResponseDto>
                    .Success(response);
            }
            catch(Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }           
        }
    }
}


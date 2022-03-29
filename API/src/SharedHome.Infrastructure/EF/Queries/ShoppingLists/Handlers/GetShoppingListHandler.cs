using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    public class GetShoppingListHandler : IQueryHandler<GetShoppingList, Response<ShoppingListDto>>
    {
        private readonly SharedHomeDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetShoppingListHandler(SharedHomeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<ShoppingListDto>> Handle(GetShoppingList request, CancellationToken cancellationToken)
        {
            var shoppingList = await _dbContext.ShoppingLists
                .Include(shoppingList => shoppingList.Products)
                .SingleOrDefaultAsync(shoppingList => shoppingList.Id == request.Id && shoppingList.PersonId == request.PersonId);

            return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList));
        }
    }
}

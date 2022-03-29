using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    public class GetAllShoppingListByYearAndMonthHandler : IQueryHandler<GetAllShoppingListByYearAndMonth, Response<IEnumerable<ShoppingListDto>>>
    {
        private readonly SharedHomeDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllShoppingListByYearAndMonthHandler(SharedHomeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ShoppingListDto>>> Handle(GetAllShoppingListByYearAndMonth request, CancellationToken cancellationToken)
        {
            var shoppingLists = await _dbContext.ShoppingLists
                .Include(shoppingList => shoppingList.Products)
                .Where(shoppingList => shoppingList.CreatedAt.Month == request.Month &&
                shoppingList.CreatedAt.Year == request.Year &&
                shoppingList.PersonId == request.PersonId)
                .ToListAsync();

            return new Response<IEnumerable<ShoppingListDto>>(_mapper.Map<IEnumerable<ShoppingListDto>>(shoppingLists));
        }
    }
}

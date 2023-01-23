using SharedHome.Application.Common.DTO;
using System.Text.Json.Serialization;

namespace SharedHome.Application.ShoppingLists.DTO
{
    public class ShoppingListDto : AuditableDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Status { get; set; } = default!;

        public IEnumerable<ShoppingListProductDto> Products { get; set; } = default!;
    }
}

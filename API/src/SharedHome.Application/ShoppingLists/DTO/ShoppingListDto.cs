using System.Text.Json.Serialization;

namespace SharedHome.Application.ShoppingLists.DTO
{
    public class ShoppingListDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public bool IsDone { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CreatedByFirstName { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CreatedByLastName { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CreatedByFullName { get; set; } = default!;

        public IEnumerable<ShoppingListProductDto> Products { get; set; } = default!;
    }
}

﻿using SharedHome.Application.Common.DTO;
using System.Text.Json.Serialization;

namespace SharedHome.Application.ShoppingLists.DTO
{
    public class ShoppingListDto : AuditableDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public bool IsDone { get; set; }

        public IEnumerable<ShoppingListProductDto> Products { get; set; } = default!;
    }
}

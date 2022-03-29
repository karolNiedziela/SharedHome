using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.DTO
{
    public class ShoppingListProductDto
    {
        public string Name { get; set; } = default!;

        public uint Quantity { get; set; }

        public decimal? Price { get; set; }

        public bool IsBought { get; set; }
    }
}

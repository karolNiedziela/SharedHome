using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Options
{
    public class MySQLOptions
    {
        public const string SQLOptionsName = "MySQL";

        public string ConnectionString { get; set; } = default!;
    }
}

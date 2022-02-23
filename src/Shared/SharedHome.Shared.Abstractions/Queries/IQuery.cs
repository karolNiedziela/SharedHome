using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Queries
{
    public interface IQuery
    {
    }

    public interface IQuery<TResult> : IQuery
    {

    }
}

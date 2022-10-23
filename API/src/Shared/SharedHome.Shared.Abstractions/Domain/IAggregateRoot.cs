using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Domain
{
    public interface IAggregateRoot<T>
    {
        public T Id { get; }
    }
}

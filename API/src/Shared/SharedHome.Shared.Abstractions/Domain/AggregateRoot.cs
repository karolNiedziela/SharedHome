using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Domain
{
    public abstract class AggregateRoot<T> : Entity, IAggregateRoot<T>
    {
        public T Id { get; protected set; } = default!;
    }
}

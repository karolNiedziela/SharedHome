using SharedHome.Domain.Bills.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.ValueObjects
{
    public record ServiceProviderName
    {
        public string Name { get; }

        public ServiceProviderName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyServiceProviderNameException();
            }

            Name = name;
        }

        public static implicit operator string(ServiceProviderName name) => name.Name;

        public static implicit operator ServiceProviderName(string name) => new(name);
    }
}

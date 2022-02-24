using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Email
{
    public interface IIdentityEmailSender
    {
        Task SendAsync(string email, string code);
    }
}

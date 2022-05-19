using SharedHome.Domain.Bills.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.Services
{
    public interface IBillService
    {
        Task<Bill> GetForHouseGroupMemberAsync(int billId, string personId);
    }
}

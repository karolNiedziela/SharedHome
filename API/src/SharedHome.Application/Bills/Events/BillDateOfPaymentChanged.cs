﻿using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.Bills.Events
{
    public record BillDateOfPaymentChanged(int BillId, ServiceProviderName ServiceProviderName, DateTime DateOfPayment) : IDomainEvent;
}
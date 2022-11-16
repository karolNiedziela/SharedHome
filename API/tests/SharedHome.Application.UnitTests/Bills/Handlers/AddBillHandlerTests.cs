using Mapster;
using MapsterMapper;
using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands.AddBill;
using SharedHome.Application.Bills.DTO;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Common.Events;
using SharedHome.Infrastructure;
using SharedHome.Shared.Application.Responses;
using Shouldly;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class AddBillHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IRequestHandler<AddBillCommand, Response<BillDto>> _commandHandler;

        public AddBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _eventDispatcher = Substitute.For<IDomainEventDispatcher>();
            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetAssembly(typeof(InfrastructureAssemblyReference))!);
            _mapper = new Mapper(config);
            _commandHandler = new AddBillHandler(_billRepository, _mapper, _eventDispatcher);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddBillCommand
            {
                ServiceProviderName = "Bill",
                BillType = 2,
            };

            var response = await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).AddAsync(Arg.Any<Bill>());

            response.Data.ShouldNotBeNull();
        }
    }
}

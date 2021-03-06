using AutoMapper;
using NSubstitute;
using SharedHome.Application.Bills.Commands;
using SharedHome.Application.Bills.Commands.Handlers;
using SharedHome.Application.Bills.DTO;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Infrastructure;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
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
        private readonly ICommandHandler<AddBill, Response<BillDto>> _commandHandler;

        public AddBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            var mapperConfiguration = new MapperConfiguration(config => config.AddMaps(Assembly.GetAssembly(typeof(InfrastructureAssemblyReference))));
            _mapper = new Mapper(mapperConfiguration);
            _commandHandler = new AddBillHandler(_billRepository, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddBill
            {
                ServiceProviderName = "Bill",
                BillType = 2,
                Cost = 1500m,
                Currency = "zł",
            };

            var response = await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).AddAsync(Arg.Any<Bill>());

            response.Data.ShouldNotBeNull();
        }
    }
}

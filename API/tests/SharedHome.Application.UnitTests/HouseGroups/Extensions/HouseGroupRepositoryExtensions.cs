using NSubstitute;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Extensions
{
    public class HouseGroupRepositoryExtensions
    {
        private readonly IHouseGroupRepository _houseGroupRepository;

        public HouseGroupRepositoryExtensions()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Throw_HouseGroupNotFoundException_When_HouseGroup_Does_Not_Exist()
        {
            var exception = await Record.ExceptionAsync(() => 
                _houseGroupRepository.GetOrThrowAsync(new HouseGroupId(), Arg.Any<PersonId>()));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Should_Return_HouseGroup()
        {
            var houseGroup = HouseGroupProvider.GetWithMember();

            _houseGroupRepository.GetOrThrowAsync(Arg.Any<HouseGroupId>(), Arg.Any<PersonId>())
                .Returns(houseGroup);

            var returnedHouseGroup = await _houseGroupRepository.GetAsync(HouseGroupProvider.HouseGroupId, Guid.NewGuid());

            returnedHouseGroup!.Id.Value.ShouldBe(HouseGroupProvider.HouseGroupId);
            returnedHouseGroup.Members.First().PersonId.Value.ShouldBe(HouseGroupProvider.PersonId);
        }
    }
}

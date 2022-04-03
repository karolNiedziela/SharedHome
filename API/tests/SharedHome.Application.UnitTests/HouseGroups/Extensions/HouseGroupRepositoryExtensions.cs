using NSubstitute;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.HouseGroups.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.HouseGroups.Repositories;
using Shouldly;
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
                _houseGroupRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<HouseGroupNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Should_Return_HouseGroup()
        {
            var houseGroup = HouseGroupProvider.GetWithOwner();

            _houseGroupRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(houseGroup);

            var returnedHouseGroup = await _houseGroupRepository.GetOrThrowAsync(1, "personId");

            returnedHouseGroup.Id.ShouldBe(1);
            returnedHouseGroup.Members.First().FirstName.Value.ShouldBe(HouseGroupProvider.DefaultPersonFirstName);
            returnedHouseGroup.Members.First().LastName.Value.ShouldBe(HouseGroupProvider.DefaultPersonLastName);
            returnedHouseGroup.Members.First().Email.Value.ShouldBe(HouseGroupProvider.DefaultPersonEmail);
        }
    }
}

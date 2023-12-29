using Itis.MyTrainings.Api.Core.Entities;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;


public class Test : UnitTestBase
{
    private UserProfile _userProfile;
    
    public Test()
    {
        _userProfile = new UserProfile()
        {
            Gender = "машина"
        };
    }
    
    [Fact]
    public async Task Handle_QueryWithFilters_ShouldReturnFilteredEntitiesAsync()
    {
        var dbcontext = CreateInMemoryContext(
            x => x.Add(_userProfile));
    }
}
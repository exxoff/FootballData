using FootballDataSDK.Models.Results;

namespace FootballDataSDK.Services.Interface
{
    public interface IFixtureServices
    {
        FixturesResult GetFixtures();
        FixturesResult GetFixtures(string timeFrame, string league);
        FixtureDetailsResult GetFixture(int idFixture);
        FixtureDetailsResult GetFixture(int idFixture, int head2Head);
    }
}

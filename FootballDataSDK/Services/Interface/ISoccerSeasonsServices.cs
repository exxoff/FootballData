using FootballDataSDK.Models.Results;

namespace FootballDataSDK.Services.Interface
{
    public interface ISoccerSeasonsServices
    {
        SoccerSeasonResult GetSoccerSeasons();
        TeamsResult GetTeams(int idSeason);
        LeagueTableResult GetLeagueTable(int idSeason);
        LeagueTableResult GetLeagueTable(int idSeason, int matchday);
        FixturesResult GetFixtures(int idSeason, int matchday, string timeFrame);
        FixturesResult GetFixtures(int idSeason);
        FixturesResult GetFixtures(int idSeason, int matchday);
        FixturesResult GetFixtures(int idSeason, string timeFrame);
    }
}

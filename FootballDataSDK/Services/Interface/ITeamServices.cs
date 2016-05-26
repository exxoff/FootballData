using FootballDataSDK.Models;
using FootballDataSDK.Models.Enums;
using FootballDataSDK.Models.Results;

namespace FootballDataSDK.Services.Interface
{
    public interface ITeamServices
    {
        Team GetTeam(int idTeam);
        FixturesResult GetFixturesByTeam(int idTeam);
        FixturesResult GetFixturesByTeam(int idTeam, string timeFrame, string season, VenueEnum? venue = null);
        PlayersResult GetPlayers(int idTeam);

    }
}

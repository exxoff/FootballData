using System;
using System.Net;
using System.Threading.Tasks;
using FootballDataSDK.Client;
using FootballDataSDK.Models;
using FootballDataSDK.Models.Common;
using FootballDataSDK.Models.Enums;
using FootballDataSDK.Models.Results;
using FootballDataSDK.Services.Interface;
using Newtonsoft.Json;

namespace FootballDataSDK.Services
{
    public class FootDataServices : ISoccerSeasonsServices, IFixtureServices, ITeamServices
    {
        public string AuthToken { get; set; }

        public FootDataServices()
        {

        }
        public FootDataServices(string token)
        {
            AuthToken = token;
        }

        #region Soccer service 

        /// <summary>
        /// List all available soccer seasons.	
        /// </summary>
        /// <returns></returns>
        public SoccerSeasonResult GetSoccerSeasons()
        {
            string url = "http://api.football-data.org/v1/soccerseasons";
            
            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new SoccerSeasonResult { error = err.error };
                    }

                    var response = JsonConvert.DeserializeObject<SoccerSeason[]>(responseString);

                    return new SoccerSeasonResult
                    {
                        Seasons = response
                    };

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }
        public SoccerSeason GetCustomSeason(string url)
        {
            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new SoccerSeason();
                    }

                    var response = JsonConvert.DeserializeObject<SoccerSeason>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;

        }

        public async Task<SoccerSeason> GetCustomSeasonAsync(string url)
        {
            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new SoccerSeason();
                    }

                    var response = JsonConvert.DeserializeObject<SoccerSeason>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;

        }



        public async Task<SoccerSeasonResult> GetSoccerSeasonsAsync(string customUrl = null)
        {
            string url = customUrl != null ? customUrl : "http://api.football-data.org/v1/soccerseasons";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res =await  client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new SoccerSeasonResult { error = err.error };
                    }

                    var response = JsonConvert.DeserializeObject<SoccerSeason[]>(responseString);

                    return new SoccerSeasonResult
                    {
                        Seasons = response
                    };

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }


        public TeamsResult GetTeams(int idSeason)
        {
            string url = $"http://api.football-data.org/v1/soccerseasons/{idSeason}/teams";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new TeamsResult {error = err.error};
                    }


                    var response = JsonConvert.DeserializeObject<TeamsResult>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public async Task<TeamsResult> GetTeamsAsync(int idSeason)
        {
            string url = $"http://api.football-data.org/v1/soccerseasons/{idSeason}/teams";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new TeamsResult { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<TeamsResult>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public LeagueTableResult GetLeagueTable(int idSeason)
        {
            return GetLeagueTable(idSeason, -1);
        }

        public async Task<LeagueTableResult> GetLeagueTableAsync(int idSeason)
        {
            return await GetLeagueTableAsync(idSeason, -1);
        }

        public LeagueTableResult GetLeagueTable(int idSeason, int matchday)
        {
            string mDay = "";

            if (matchday > 0)
            {
                mDay = matchday + "";
            }

            string url = $"http://api.football-data.org/v1/soccerseasons/{idSeason}/leagueTable?matchday={mDay}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new LeagueTableResult { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<LeagueTableResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public async Task<LeagueTableResult> GetLeagueTableAsync(int idSeason, int matchday)
        {
            string mDay = "";

            if (matchday > 0)
            {
                mDay = matchday + "";
            }

            string url = $"http://api.football-data.org/v1/soccerseasons/{idSeason}/leagueTable?matchday={mDay}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new LeagueTableResult { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<LeagueTableResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }


        public FixturesResult GetFixtures(int idSeason,  int matchday, string timeFrame)
        {
            string p1 = "";
            if (matchday > 0)
                p1 = matchday + "";

            string url =
                $"http://api.football-data.org/v1/soccerseasons/{idSeason}/fixtures?matchday={p1}&timeFrame={timeFrame}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixturesResult { error = err.error };
                    }
                    
                    var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public async Task<FixturesResult> GetFixturesAsync(int idSeason, int matchday, string timeFrame)
        {
            string p1 = "";
            if (matchday > 0)
                p1 = matchday + "";

            string url =
                $"http://api.football-data.org/v1/soccerseasons/{idSeason}/fixtures?matchday={p1}&timeFrame={timeFrame}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixturesResult {error = err.error};
                    }

                    var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public FixturesResult GetFixtures(int idSeason)
        {
            return GetFixtures(idSeason, -1, null);
        }
        public async Task<FixturesResult> GetFixturesAsync(int idSeason)
        {
            return await GetFixturesAsync(idSeason, -1, null);
        }

        public FixturesResult GetFixtures(int idSeason, int matchday)
        {
            return GetFixtures(idSeason, matchday, null);
        }

        public async Task<FixturesResult> GetFixturesAsync(int idSeason, int matchday)
        {
            return await GetFixturesAsync(idSeason, matchday, null);
        }

        public FixturesResult GetFixtures(int idSeason, string timeFrame)
        {
            return GetFixtures(idSeason, -1, timeFrame);
        }

        public async Task<FixturesResult> GetFixturesAsync(int idSeason, string timeFrame)
        {
            return await GetFixturesAsync(idSeason, -1, timeFrame);
        }

        #endregion


        #region Fixtures

        /// <summary>
        /// List fixtures across a set of soccerseasons.
        /// </summary>
        /// <returns></returns>
        public FixturesResult GetFixtures()
        {
            return GetFixtures("", "");
        }
        public async Task<FixturesResult> GetFixturesAsync()
        {
            return await GetFixturesAsync("", "");
        }
        public FixturesResult GetFixtures(string timeFrame, string league)
        {
            string url = $"http://api.football-data.org/v1/fixtures?timeFrame={timeFrame}&league={league}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixturesResult { error = err.error };
                    }

                    var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }
        public async Task<FixturesResult> GetFixturesAsync(string timeFrame, string league)
        {
            string url = $"http://api.football-data.org/v1/fixtures?timeFrame={timeFrame}&league={league}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) 
                        || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixturesResult { error = err.error };
                    }

                    var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public FixtureDetailsResult GetFixture(int idFixture)
        {
            return GetFixture(idFixture, 10);
        }

        public async Task<FixtureDetailsResult> GetFixtureAsync(int idFixture)
        {
            return await GetFixtureAsync(idFixture, 10);
        }

        public FixtureDetailsResult GetFixture(int idFixture, int head2Head)
        {
            string url = $"http://api.football-data.org/v1/fixtures/{idFixture}?head2head={head2Head}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixtureDetailsResult {error = err.error};
                    }


                    var response = JsonConvert.DeserializeObject<FixtureDetailsResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public async Task<FixtureDetailsResult> GetFixtureAsync(int idFixture, int head2Head)
        {
            string url = $"http://api.football-data.org/v1/fixtures/{idFixture}?head2head={head2Head}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixtureDetailsResult { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<FixtureDetailsResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        #endregion


        #region Team Service

        public Team GetTeam(int idTeam)
        {
            string url = $"http://api.football-data.org/v1/teams/{idTeam}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new Team { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<Team>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public async Task<Team> TeamAsync(int idTeam)
        {
            string url = $"http://api.football-data.org/v1/teams/{idTeam}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new Team { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<Team>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public FixturesResult GetFixturesByTeam(int idTeam)
        {
            //string timeFrame, string season, VenueEnum? venue = null
            return GetFixturesByTeam(idTeam, null, null, null);
        }
        public async Task<FixturesResult> GetFixturesByTeamAsync(int idTeam)
        {
            //string timeFrame, string season, VenueEnum? venue = null
            return  await GetFixturesByTeamAsync(idTeam, null, null, null);
        }

        public FixturesResult GetFixturesByTeam(int idTeam, string timeFrame, string season, VenueEnum? venue = null)
        {
            string url = $"http://api.football-data.org/v1/teams/{idTeam}/fixtures?timeFrame={timeFrame}&season={season}&venue={venue}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixturesResult { error = err.error };
                    }

                    var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        public async  Task<FixturesResult> GetFixturesByTeamAsync(int idTeam, string timeFrame, string season, VenueEnum? venue = null)
        {
            string url = $"http://api.football-data.org/v1/teams/{idTeam}/fixtures?timeFrame={timeFrame}&season={season}&venue={venue}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new FixturesResult { error = err.error };
                    }

                    var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);

                    return response;
                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }


        public PlayersResult GetPlayers(int idTeam)
        {
            string url = $"http://api.football-data.org/v1/teams/{idTeam}/players";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = client.GetAsync(new Uri(url)).Result;

                    string responseString = res.Content.ReadAsStringAsync().Result;

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new PlayersResult { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<PlayersResult>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }
        public async Task<PlayersResult> GetPlayersAsync(int idTeam)
        {
            string url = $"http://api.football-data.org/v1/teams/{idTeam}/players";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                try
                {
                    var res = await client.GetAsync(new Uri(url));

                    string responseString = await res.Content.ReadAsStringAsync();

                    // Sanity Check
                    if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);

                        return new PlayersResult { error = err.error };
                    }


                    var response = JsonConvert.DeserializeObject<PlayersResult>(responseString);

                    return response;

                }
                catch (Exception)
                {
                    //Ignore..
                }
            }

            return null;
        }

        #endregion
    }
}

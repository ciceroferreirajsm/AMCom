using Newtonsoft.Json;
using Questao2.Domain;

namespace Questao1.Services
{
    public class GoalsDataService
    {
        public static async Task<(string Team, int Goals, int Year)[]> GetGoalsScoredByTeam(int year, string team)
        {
            using (var client = new HttpClient())
            {
                var goals = new System.Collections.Generic.List<(string Team, int Goals, int Year)>();

                int page = 1;
                bool hasMorePages = true;

                while (hasMorePages)
                {
                    string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";
                    var response = await client.GetStringAsync(url);

                    var data = JsonConvert.DeserializeObject<ApiResponse>(response);

                    if (data != null)
                    {
                        foreach (var match in data.Data)
                        {
                            int teamGoals = 0;
                            int opponentGoals = 0;

                            if (match.Team1 == team)
                            {
                                teamGoals += match.Team1Goals;
                                opponentGoals += match.Team2Goals;
                            }
                            if (match.Team2 == team)
                            {
                                teamGoals += match.Team2Goals;
                                opponentGoals += match.Team1Goals;
                            }

                            if (teamGoals > 0 || opponentGoals > 0)
                            {
                                goals.Add((team, teamGoals, year));
                            }
                        }

                        hasMorePages = page < data.TotalPages;
                        page++;
                    }
                    else
                    {
                        hasMorePages = false;
                    }
                }

                return goals.ToArray();
            }
        }

    }

}

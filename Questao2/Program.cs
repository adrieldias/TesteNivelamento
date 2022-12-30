using Newtonsoft.Json;
using Questao2;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class Program
{
    static HttpClient client = new();
    public static async Task Main()
    {
        client.BaseAddress = new Uri("https://jsonmock.hackerrank.com/api/football_matches");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public async static Task<int> getTotalScoredGoals(string team, int year)
    {
        HttpResponseMessage response;
        MatchesResponse? matchesResponse = null;
        var gols = 0;
        try
        {
            foreach (var order in new int[] { 1, 2 })
            {
                do
                {
                    response = await client.GetAsync($"?year={year}&team{order}={team}&page={(matchesResponse?.Page == null ? 1 : matchesResponse?.Page + 1)}");
                    matchesResponse = await response.Content.ReadFromJsonAsync<MatchesResponse>();

                    foreach (var match in matchesResponse.Data)
                        gols += order == 1 ? int.Parse(match.Team1goals) : int.Parse(match.Team2goals);
                }
                while (matchesResponse.Total_pages > matchesResponse.Page);
                matchesResponse = null;
            }
        }
        catch (Exception)
        {
            throw;
        }
        
        return gols;
    }

}
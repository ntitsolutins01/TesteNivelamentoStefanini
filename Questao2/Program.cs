using Newtonsoft.Json;
using Questao2;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

    }

    public static async Task<int> getTotalScoredGoals(string time, int ano)
    {
        PartidasAPI api = new PartidasAPI();

        List<Partida> matches = await api.GetPartidas(ano, time);

        int totalGoals = 0;
        
        foreach (var match in matches)
        {
            totalGoals += Convert.ToInt32(match.team1goals);

        }
        return totalGoals;
    }

}
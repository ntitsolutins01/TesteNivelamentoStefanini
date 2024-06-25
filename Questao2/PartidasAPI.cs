using System.Text.Json;

namespace Questao2
{
    public class PartidasAPI
    {
        private const string BaseUrl = "https://jsonmock.hackerrank.com/api/football_matches";

        public async Task<List<Partida>> GetPartidas(int ano, string? time1 = null, string? time2 = null)
        {
            List<Partida> partidas = new List<Partida>();
            string url = $"{BaseUrl}?year={ano}";
            if (time1 != null)
                url += $"&team1={time1}";
            if (time2 != null)
                url += $"&team2={time2}";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<PartidasApiResponse>(json, options);
                    if (result != null)
                    {
                        partidas.AddRange(result.Data);
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao chamar a API: " + response.StatusCode);
                }
            }

            return partidas;
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        string buttonColorTemplate = "button-color";
        string priceTemplate = "price";
        string deviceButtonEndPoint = "https://localhost:7080/experiment/button-color?device_token=";
        string priceEndPoint = "https://localhost:7080/experiment/price?device_token=";
        
        Dictionary<string, int> valuesCounterForButtons = new Dictionary<string, int>
        {
            { "#FF0000", 0 },
            { "#00FF00", 0 },
            { "#0000FF", 0 },
            { "Total", 0 }
        };
        Dictionary<string, int> valuesCounterForPrices = new Dictionary<string, int>
        {
            { "50,00", 0 },
            { "20,00", 0 },
            { "10,00", 0 },
            { "5,00", 0 },
            { "Total", 0 }
        };

        await CalculateStatistics(1000, priceTemplate, priceEndPoint, valuesCounterForPrices);
    }

    static async Task CalculateStatistics(int devices, string template, string url, Dictionary<string, int> valuesCounter)
    {
        string filePath = $"/Users/bondarenkooleksandr/C#Projects/ABPBackendTZ/ABPBackendGetStatistics/{template}-results-for-{devices}.json";
        HttpClient client = new HttpClient();
        List<string> results = new List<string>();

        for (int i = 0; i < devices; i++)
        {
            HttpResponseMessage response = await client.GetAsync(url + i);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                results.Add(result);
            }
        }

        foreach (string result in results)
        {
            var deserializedResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            string value = deserializedResult["value"];

            if (valuesCounter.ContainsKey(value))
            {
                valuesCounter[value]++;
                valuesCounter["Total"]++;
            }
        }

        JObject finalJson = new JObject();
        finalJson["results"] = JArray.FromObject(results);
        finalJson["values_counter"] = JObject.FromObject(valuesCounter);
        await File.WriteAllTextAsync(filePath, finalJson.ToString(Formatting.Indented));

        Console.WriteLine($"Results are saved in {filePath}");
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    class Argument
    {
        private int quantity;
        public int Quantity { get; init; }
        public string Template { get; init; }
        public string Url { get; init; }
        public Dictionary<string, int> valuesCounter;
    }
    static async Task Main(string[] args)
    {
        Argument buttonColorArgument = new()
        {
            Quantity = 2000,
            Template = "button-color",
            Url = "https://localhost:7080/experiment/button-color?device_token=",
            valuesCounter = new()
            {
                { "#FF0000", 0 },
                { "#00FF00", 0 },
                { "#0000FF", 0 },
                { "Total", 0 }
            }
        };

        Argument priceArgument = new()
        {
            Quantity = 2000,
            Template = "price",
            Url = "https://localhost:7080/experiment/price?device_token=",
            valuesCounter = new()
            {
                { "50,00", 0 },
                { "20,00", 0 },
                { "10,00", 0 },
                { "5,00", 0 },
                { "Total", 0 }
            }
        };

        await CalculateStatistics(priceArgument);
    }

    static async Task CalculateStatistics(Argument argument)
    {
        string filePath = $"/Users/bondarenkooleksandr/C#Projects/ABPBackendTZ/ABPBackendGetStatistics/{argument.Template}-results-for-{argument.Quantity}.json";
        HttpClient client = new HttpClient();
        List<string> results = new List<string>();

        for (int i = 0; i < argument.Quantity; i++)
        {
            HttpResponseMessage response = await client.GetAsync(argument.Url + i);
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

            if (argument.valuesCounter.ContainsKey(value))
            {
                argument.valuesCounter[value]++;
                argument.valuesCounter["Total"]++;
            }
        }

        JObject finalJson = new JObject();
        finalJson["results"] = JArray.FromObject(results);
        finalJson["values_counter"] = JObject.FromObject(argument.valuesCounter);
        await File.WriteAllTextAsync(filePath, finalJson.ToString(Formatting.Indented));

        Console.WriteLine($"Results are saved in {filePath}");
    }
}
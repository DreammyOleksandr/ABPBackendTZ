using ABPBackendTZ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ.Controllers
{
    [Route("experiment")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly APIResponse _response = new();
        private readonly ApplicationDbContext _dbContext;
        private const string GetButtonColorTemplate = "button-color";
        private const string GetPriceTemplate = "price";

        public ExperimentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(GetButtonColorTemplate)]
        public async Task<IActionResult> GetButtonColor(string? device_token)
        {
            _response.Key = GetButtonColorTemplate;
            try
            {
                if (String.IsNullOrEmpty(device_token)) return BadRequest();
                //Знаходимо девайс в БД для подальших дій (включаючи ButtonColor)
                Device device = await _dbContext.Devices.Include(nameof(ButtonColor))
                    .FirstOrDefaultAsync(_ => _.Token == device_token);
                if (device is null) // Якщо девайсу в БД не знайдено, то створюємо новий і видаємо йому кнопку з випадковим кольором.
                {
                    device = new Device();
                    device.Token = device_token;

                    var ButtonColorIds = _dbContext.ButtonColors.Select(_ => _.Id).ToList();
                    device.ButtonColorId = GetRandomValueFromList(ButtonColorIds);

                    await _dbContext.Devices.AddAsync(device); // Додаємо девайс до БД
                    await _dbContext.SaveChangesAsync(); // Зберігаємо зміни

                    _response.Value = _dbContext.ButtonColors
                        .FirstOrDefaultAsync(_ => _.Id == device.ButtonColorId).Result.HEX;
                    return Ok(_response); // Видаємо відповідь
                }
                else
                {
                    // Якщо девайс вже запитував до цього ButtonColor, то просто видаємо йому записаний до цього результат.
                    if (device.ButtonColorId is not null && device.ButtonColorId > 0)
                    {
                        _response.Value = device.ButtonColor.HEX;
                        return Ok(_response);
                    }
                    else // Якщо девайс записаний до БД, але не має ButtonColor, тоді надємо значення в це поле і оновлюємо Device
                    {
                        var ButtonColorIds = _dbContext.ButtonColors.Select(_ => _.Id).ToList();
                        device.ButtonColorId = GetRandomValueFromList(ButtonColorIds);

                        _dbContext.Devices.Update(device);
                        _response.Value = _dbContext.ButtonColors
                            .FirstOrDefaultAsync(_ => _.Id == device.ButtonColorId).Result.HEX;
                        return Ok(_response);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        [HttpGet(GetPriceTemplate)]
        public IActionResult GetPrice(string? device_token)
        {
            _response.Key = GetPriceTemplate;
            _response.Value = "TestValue";
            return Ok(_response);
        }

        private int GetRandomValueFromList(List<int> list)
        {
            /* Ця функція описує отримання значення з листа при однаковій вірогідності отримання значень із нього,
            тому тут використовується звичайний клас Random. Ця функція дуже проста, але вона виділена задля декомпозиції
            та запобіганню повторень коду
            */
            Random r = new();
            int value = r.Next(0, list.Count);
            return list[value];
        }

        private string GetRandomValueWithProbability(string[] items, double[] probabilities, double randomNumber)
        {
            double cumulativeProbability = 0;
            for (int i = 0; i < items.Length; i++)
            {
                cumulativeProbability += probabilities[i];
                if (randomNumber < cumulativeProbability) return items[i];
            }

            return null;
        }
    }
}
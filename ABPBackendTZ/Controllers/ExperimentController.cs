using ABPBackendTZ.Models;
using ABPBackendTZ.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ABPBackendTZ.Controllers
{
    [Route("experiment")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly APIResponse _response = new();
        private const string GetButtonColorTemplate = "button-color";
        private const string GetPriceTemplate = "price";
        private readonly IDeviceRepository _deviceRepository;
        private readonly IButtonColorRepository _buttonColorRepository;
        private readonly IPriceToShowRepository _priceToShowRepository;

        public ExperimentController(
            IDeviceRepository deviceRepository,
            IButtonColorRepository buttonColorRepository,
            IPriceToShowRepository priceToShowRepository)
        {
            _priceToShowRepository = priceToShowRepository;
            _buttonColorRepository = buttonColorRepository;
            _deviceRepository = deviceRepository;
        }

        [HttpGet(GetButtonColorTemplate)]
        public async Task<IActionResult> GetButtonColor(string? device_token)
        {
            _response.Key = GetButtonColorTemplate;
            try
            {
                if (String.IsNullOrEmpty(device_token)) return BadRequest();
                //Знаходимо девайс в БД для подальших дій (включаючи ButtonColor)
                Device device = await _deviceRepository.GetByToken(device_token, nameof(ButtonColor));
                if (device is null) // Якщо девайсу в БД не знайдено, то створюємо новий і видаємо йому кнопку з випадковим кольором.
                {
                    device = new Device();
                    device.Token = device_token;

                    // Тут ми отримуємо всі Id від ButtonColors для того, що б працювати з індексами, а не значеннями, бо це запобігає подібній помилці:
                    /*
                     якщо б ми просто присвоювали випадкове значкеея до ButtonColorId, то можлива була б подібна ситуація:
                     Ids: [1, 2, 3, 7, 10] і якщо б Random просто шукав значення, а не індекс, то ми б отримали наприклад число 6, якого немає в Листі і отримували б помилку.

                     Варіант роботи з Індексами запобігає подібним помилкам.
                     */
                    var ButtonColorIds = _buttonColorRepository.GetAll().Result.Select(_ => _.Id).ToList();
                    device.ButtonColorId = GetRandomValueFromList(ButtonColorIds);

                    await _deviceRepository.Add(device); // Додаємо девайс до БД і зберігаємо зміни.

                    _response.Value = _buttonColorRepository.GetById(device.ButtonColorId).Result.HEX;
                    return Ok(_response); // Видаємо відповідь
                }

                // Якщо девайс вже запитував до цього ButtonColor, то просто видаємо йому записаний до цього результат.
                if (device.ButtonColorId is not null && device.ButtonColorId > 0)
                {
                    _response.Value = device.ButtonColor.HEX;
                    return Ok(_response);
                }
                else // Якщо девайс записаний до БД, але не має ButtonColor, тоді надємо значення в це поле і оновлюємо Device
                {
                    var ButtonColorIds = _buttonColorRepository.GetAll().Result.Select(_ => _.Id).ToList();
                    device.ButtonColorId = GetRandomValueFromList(ButtonColorIds);
                    await _deviceRepository.Update(device);

                    _response.Value = _buttonColorRepository.GetById(device.ButtonColorId).Result.HEX;
                    return Ok(_response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        [HttpGet(GetPriceTemplate)]
        public async Task<IActionResult> GetPrice(string? device_token)
        {
            _response.Key = GetPriceTemplate;
            try
            {
                if (String.IsNullOrEmpty(device_token)) return BadRequest();
                //Знаходимо девайс в БД для подальших дій (включаючи PriceToShow)
                Device device = await _deviceRepository.GetByToken(device_token, nameof(PriceToShow));
                if (device is null) // Якщо девайсу в БД не знайдено, то створюємо новий і видаємо йому ціну з визначеною вірогідністю.
                {
                    device = new();
                    device.Token = device_token;

                    var PriceToShowIds = _priceToShowRepository.GetAll().Result.Select(_ => _.Id).ToList();
                    var Probabilities = _priceToShowRepository.GetAll().Result.Select(_ => _.Percentage).ToList();
                    device.PriceToShowId = GetRandomValueFromListWithProbability(PriceToShowIds, Probabilities);

                    await _deviceRepository.Add(device); // Додаємо девайс до БД і зберігаємо зміни.

                    _response.Value = _priceToShowRepository.GetById(device.PriceToShowId).Result.Value.ToString();
                    return Ok(_response);
                }

                // Якщо девайс вже запитував до цього PriceToShow, то просто видаємо йому записаний до цього результат.
                if (device.PriceToShowId is not null && device.PriceToShowId > 0)
                {
                    _response.Value = device.PriceToShow.Value.ToString();
                    return Ok(_response);
                }
                else // Якщо девайс записаний до БД, але не має PriceToShow, тоді надємо значення в це поле і оновлюємо Device
                {
                    var PriceToShowIds = _priceToShowRepository.GetAll().Result.Select(_ => _.Id).ToList();
                    var Probabilities = _priceToShowRepository.GetAll().Result.Select(_ => _.Percentage).ToList();
                    device.PriceToShowId = GetRandomValueFromListWithProbability(PriceToShowIds, Probabilities);

                    await _deviceRepository.Update(device);

                    _response.Value = _priceToShowRepository.GetById(device.PriceToShowId).Result.Value.ToString();
                    return Ok(_response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        private int GetRandomValueFromList(List<int>? list)
        {
            /* Ця функція описує отримання значення з листа при однаковій вірогідності отримання значень із нього,
            тому тут використовується звичайний клас Random. Ця функція дуже проста, але вона виділена задля декомпозиції
            та запобіганню повторень коду.
            */
            Random r = new();
            int value = r.Next(0, list.Count);
            return list[value];
        }

        private int? GetRandomValueFromListWithProbability(List<int>? list, List<float>? probabilities)
        {
            /* Ця функція описує отримання значення з листа при різній вірогідності отримання значень із нього.
             Ця функція дуже проста, але вона виділена задля декомпозиції та запобіганню повторень коду.
            */
            Random r = new();
            double randomNumber = r.NextDouble();

            double cumulativeProbability = 0;
            for (int i = 0; i < list.Count; i++)
            {
                cumulativeProbability += probabilities[i];
                if (randomNumber < cumulativeProbability) return list[i];
            }

            return null;
        }
    }
}
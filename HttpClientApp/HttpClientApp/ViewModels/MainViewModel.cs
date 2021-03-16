using HttpClientApp.Service;
using ProfilApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using HttpClientApp.Models;
using System.Linq;

namespace HttpClientApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        string querry;
        string message;
        string errormessage;
        string city;
        string windspeed;
        string humidity;
        string visibility;
        string temperature;
        
        public string Querry
        {
            get
            {
                return querry;
            }

            set
            {
                SetProperty(ref querry, value);
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                SetProperty(ref message, value);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return errormessage;
            }

            set
            {
                SetProperty(ref errormessage, value);
            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                SetProperty(ref city, value);
            }
        }

        public string WindSpeed
        {
            get
            {
                return windspeed;
            }

            set
            {
                SetProperty(ref windspeed, value);
            }
        }

        public string Humidity
        {
            get
            {
                return humidity;
            }
            set
            {
                SetProperty(ref humidity, value);
            }
        }

        public string Visibility
        {
            get
            {
                return visibility;
            }

            set
            {
                SetProperty(ref visibility, value);
            }
        }

        public string Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                SetProperty(ref temperature, value);
            }
        }


        public ICommand GetCommand => new Command(() =>
           Task.Run(LoadWeatherData)
        );

        async Task LoadWeatherData()
        {
            if(IsBusy)
            {
                return;
            }

            IsBusy = true;
            var client = HttpService.GetInstance();
            var result = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={Querry}&APPID=6fcb5a969e58b25ffb37b7426ac18d12&units=metric&lang=fr");
            var serializedResponse = await result.Content.ReadAsStringAsync();
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(serializedResponse);

            if(weatherResponse?.Weather != null && weatherResponse.Weather.Any())
            {
                ErrorMessage = "";
                City = weatherResponse.Name;
                WindSpeed = $"{weatherResponse.Wind.Speed} km/h";
                Humidity = $"{weatherResponse.Main.Humidity}%";
                Visibility = $"{weatherResponse.Visibility}m";
                Temperature = $"{weatherResponse.Main.Temp}°";
            }
            else
            {
                ErrorMessage = weatherResponse?.Message ?? "Unknown Error";
                City = "unknown";
                WindSpeed = "unknown";
                Humidity = "unknown";
                Visibility = "unknown";
                Temperature = "unknown";
            }

            IsBusy = false;
        }
    }
}

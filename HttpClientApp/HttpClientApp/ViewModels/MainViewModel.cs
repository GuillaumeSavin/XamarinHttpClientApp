using HttpClientApp.Service;
using ProfilApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HttpClientApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        string querry;
        string message;
        
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
            var result = await client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={Querry}&APPID=6fcb5a969e58b25ffb37b7426ac18d12&units = metric & lang = fr");

            Message = await result.Content.ReadAsStringAsync();

            IsBusy = false;
        }
    }
}

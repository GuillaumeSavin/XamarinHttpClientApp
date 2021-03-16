using HttpClientApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HttpClientApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        void Entry_Completed(System.Object sender, System.EventArgs e)
        {
            if (BindingContext is MainViewModel vm)
            {
                vm.GetCommand.Execute(sender);
            }
        }
    }
}

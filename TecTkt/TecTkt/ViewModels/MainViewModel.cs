
namespace TecTkt.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Views;


    public class MainViewModel
    {
        public PaisesViewModel Paises { get; set; }
        public AddPaisViewModel AddPais { get; set; }

        public MainViewModel()
        {
            this.Paises = new PaisesViewModel();
        }
        public ICommand AddCountryCommand
        {
            get
            {
                return new RelayCommand(GoToAddCountry);
            }
        }

        private async void GoToAddCountry()
        {
            this.AddPais = new AddPaisViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddPaisPage());
        }
    }
}

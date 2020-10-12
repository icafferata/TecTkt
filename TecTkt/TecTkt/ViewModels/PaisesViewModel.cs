namespace TecTkt.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Common.Models;
    using Helpers;
    using Services;
    using Xamarin.Forms;

    public class PaisesViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRefreshing;
        private ObservableCollection<Pais> paises;
        
        public ObservableCollection<Pais> Paises 
        {
            get { return this.paises; }
            set { this.SetValue(ref this.paises, value); } 
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public PaisesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPaises();
        }

        private async void LoadPaises()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSucces)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPaisesController"].ToString();
            var response = await this.apiService.GetList<Pais>(url, prefix, controller);
            if (!response.IsSucces)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            var list = (List<Pais>)response.Result;
            this.Paises = new ObservableCollection<Pais>(list);
            this.IsRefreshing = false;
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPaises);
            }
        }
    }
}

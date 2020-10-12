namespace TecTkt.ViewModels
{
    public class MainViewModel
    {
        public PaisesViewModel Paises { get; set; }

        public MainViewModel()
        {
            this.Paises = new PaisesViewModel();
        }
    }
}

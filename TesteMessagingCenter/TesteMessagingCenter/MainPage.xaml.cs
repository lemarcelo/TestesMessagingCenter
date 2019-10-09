using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TesteMessagingCenter
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void EnviarMensagem(object sender, EventArgs e)
        {
            ResultView destinoMsg = new ResultView();
            MessagingCenter.Send<ResultView>( destinoMsg, "jose");
        }

        private void GoPagina2(object sender, EventArgs e)
        {
            ResultView Pagina = new ResultView();
            App.Current.MainPage.Navigation.PushAsync(Pagina);
            Pagina.Registro();
        }
    }
}

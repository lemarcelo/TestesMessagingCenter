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

        private void GoPaginaResult(object sender, EventArgs e)
        {
            ResultView ResultPage = new ResultView();
            App.Current.MainPage.Navigation.PushAsync(ResultPage);
        }

        private void GoPaginaResultComRegistro(object sender, EventArgs e)
        {
            ResultView ResultPage = new ResultView(true);
            App.Current.MainPage.Navigation.PushAsync(ResultPage);
            MessagingCenter.Send<ResultView>(ResultPage, "jose");
        }

        private void RemoverRegistro(object sender, EventArgs e)
        {
            ResultView ResultPage = new ResultView();
            MessagingCenter.Unsubscribe<ResultView>(ResultPage, "jose");
        }

        private void SomenteRegistro(object sender, EventArgs e)
        {
            ResultView ResultPage = new ResultView(true);
        }
    }
}

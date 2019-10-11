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
        /*Correção dos erros de: Duplicidade no Registros de mensagem e Falha na Remoção da Mensagem(Unsubscribe).
         * Motivo dos erros: Multiplas instâncias de um mêsmo objeto que deveria ser estático/singleton por ter no 
         * construtor da classe de origem a criação de um registro de mensagem(que duplicava a cada instância).
        */    
        readonly ResultView ResultPage = new ResultView();

        public MainPage()
        {
            InitializeComponent();

        }

        private void GoPaginaResult(object sender, EventArgs e)
        {
            ResultView ResultPage = new ResultView();
            App.Current.MainPage.Navigation.PushAsync(ResultPage);
        }

        private void SomenteRegistro(object sender, EventArgs e)
        {
            ResultPage.Registro();
        }

        private void EnviarMensagem(object sender, EventArgs e)
        {

            MessagingCenter.Send<ResultView>( ResultPage, "jose");
        }

        private void GoPaginaResultComRegistro(object sender, EventArgs e)
        {
            ResultPage.Registro();
            App.Current.MainPage.Navigation.PushAsync(ResultPage);
            MessagingCenter.Send<ResultView>(ResultPage, "jose");
            MessagingCenter.Unsubscribe<ResultView>(ResultPage, "jose");
        }

        private void RemoverRegistro(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ResultView>(ResultPage, "jose");
        }

    }
}

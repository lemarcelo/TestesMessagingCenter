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

        NotifyBase notify = new NotifyBase();
        private string _Texto;
        public string Texto
        {
            get { return _Texto; }
            set
            {
                _Texto = value;
                notify.NotifyPropertyChanged("Texto");
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
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

            MessagingCenter.Send<ResultView>( ResultPage, "DisplayAlert");
        }

        private void GoPaginaResultComRegistro(object sender, EventArgs e)
        {
            ResultPage.Registro();
            MessagingCenter.Send<ResultView>(ResultPage, "DisplayAlert");
            ResultPage.CancelarRegistroSemParametro();
            this.Navigation.PushAsync(ResultPage);
        }

        private void RemoverRegistro(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ResultView>(ResultPage, "DisplayAlert");
        }

        private void GoPaginaResultTexto(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Texto))
            {
                ResultPage.RegistroComParametro();
                MessagingCenter.Send(ResultPage, "Texto", Texto);
                MessagingCenter.Unsubscribe<ResultView, string>(this, "Texto");
                this.Navigation.PushAsync(ResultPage);
            }
            else
            {
                this.DisplayAlert("Dados incorretos", "Digite algo na caixa de texto", "Ok");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteMessagingCenter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultView : ContentPage
    {
        NotifyBase Notify = new NotifyBase();
        private string _Texto;
        public string Texto
        {
            get { return _Texto; }
            set
            {
                _Texto = value;
                Notify.NotifyPropertyChanged("Texto");
            }
        }

        public ResultView()
        {
            InitializeComponent();
            BindingContext = this;
        }
        private void Voltar(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        private void RegistrarMensagem(object sender, EventArgs e)
        {
            Registro();
        }

        private void EnviarMensagem(object sender, EventArgs e)
        {
            MessagingCenter.Send<ResultView>(this, "DisplayAlert");
        }

        private void GoPaginaMainComRegistro(object sender, EventArgs e)
        {
            Registro();
            this.Navigation.PopAsync();
            MessagingCenter.Send<ResultView>(this, "DisplayAlert");
            CancelarRegistroSemParametro();
        }
        //Remoção do registro da mensagem
        /*Tem como objetivo remover(Unsubscribe) o registro da Mensagem(<ResultView>, "DisplayAlert"), 
         * importante ressaltar que o tipo do destino da mensagem(<ResultView>) também é chave 
         * para que o MessagingCenter a identifique, para neste caso remove-la*/
        private void ApagarRegistro(object sender, EventArgs e)
        {
            CancelarRegistroSemParametro();
        }
        public void CancelarRegistroSemParametro()
        {
            MessagingCenter.Unsubscribe<ResultView>(this, "DisplayAlert");
        }

        #region Registro
        //Registro da Mensagem
        /*Tem como objecivo declarar qual será a classe de 
         * destino(<ResultView>) e qual será a chave/mensagem ("DisplayAlert") e qual 
         * será a ação, caso exista(message => DisplayAlert)*/
        public void Registro()
        {
            MessagingCenter.Subscribe<ResultView>(this, "DisplayAlert", message =>
            {
                this.DisplayAlert("Alerta de Registro", "Mensage DisplayAlert com registro Enviada", "Ok");
            });
        }
        #endregion
        public void RegistroComParametro()
        {
            MessagingCenter.Subscribe<ResultView, string>(this, "Texto", (message, args) =>
            {
                this.DisplayAlert("Alerta de Registro", args, "Ok");
            });
        }
        public void CancelarRegistroComParametro()
        {
            MessagingCenter.Unsubscribe<ResultView, string>(this, "Texto");

        }

        private void VoltarTexto(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Texto))
            {
                RegistroComParametro();
                MessagingCenter.Send(this, "Texto", Texto);
                CancelarRegistroComParametro();
                App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                this.DisplayAlert("Dados incorretos", "Digite algo na caixa de texto", "Ok");
            }
        }
    }
}
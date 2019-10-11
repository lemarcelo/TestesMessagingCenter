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
        public ResultView()
        {
            InitializeComponent();
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
            MessagingCenter.Send<ResultView>(this, "jose");
        }

        private void GoPaginaMainComRegistro(object sender, EventArgs e)
        {
            Registro();
            this.Navigation.PopAsync();
            MessagingCenter.Send<ResultView>(this, "jose");
            MessagingCenter.Unsubscribe<ResultView>(this, "jose");
        }
        //Remoção do registro da mensagem
        /*Tem como objetivo remover(Unsubscribe) o registro da Mensagem(<ResultView>, "jose"), 
         * importante ressaltar que o tipo do destino da mensagem(<ResultView>) também é chave 
         * para que o MessagingCenter a identifique, para neste caso remove-la*/
        private void ApagarRegistro(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ResultView>(this, "jose");
        }

        #region Registro
        //Registro da Mensagem
        /*Tem como objecivo declarar qual será a classe de 
         * destino(<ResultView>) e qual será a chave/mensagem ("jose") e qual 
         * será a ação, caso exista(message => DisplayAlert)*/
        public void Registro()
        {
            MessagingCenter.Subscribe<ResultView>(this, "jose", message =>
            {
                this.DisplayAlert("Alerta de Registro", "Mensage jose com registro Enviada", "Ok");
            });
        }
        #endregion
    }
}
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
        public ResultView(bool registro)
        {
            InitializeComponent();
            Registro();
        }

        private void Voltar(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        private void ApagarRegistro(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ResultView>(this, "jose");
        }

        private void RegistrarMensagem(object sender, EventArgs e)
        {
            Registro();
        }
        //
        public void Registro()
        {
            MessagingCenter.Subscribe<ResultView>(this, "jose", message =>
            {
                this.DisplayAlert("asd", "Mensagem Registrada", "asd");
            });
        }
    }
}
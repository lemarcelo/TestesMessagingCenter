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
        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                notify.NotifyPropertyChanged("Text");
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void SecondPage(object sender, EventArgs e)
        {
            ResultView ResultPage = new ResultView();
            App.Current.MainPage.Navigation.PushAsync(ResultPage);
        }

        private void JustRegister(object sender, EventArgs e)
        {
            ResultPage.Subscribe();
        }

        private void SendMessage(object sender, EventArgs e)
        {

            MessagingCenter.Send<ResultView>( ResultPage, "DisplayAlert");
        }

        private void SecondPageWithMessaging(object sender, EventArgs e)
        {
            ResultPage.SubscribeUnsubscribe();
            this.Navigation.PushAsync(ResultPage);
            MessagingCenter.Send(ResultPage, "DisplayAlert");
        }
        private void Unsubscribe(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ResultView>(ResultPage, "DisplayAlert");
        }

        private void SecondPageWithMessagingParam(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                ResultPage.SubscribeWithParam();
                this.Navigation.PushAsync(ResultPage);
                MessagingCenter.Send(ResultPage, "Text", Text);
            }
            else
            {
                this.DisplayAlert("Ops", "Type something", "Ok");
            }
        }
    }
}

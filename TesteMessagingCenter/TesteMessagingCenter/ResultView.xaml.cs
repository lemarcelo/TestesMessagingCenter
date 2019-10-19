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
        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                Notify.NotifyPropertyChanged("Text");
            }
        }

        public ResultView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void BackPage(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        private void SubscribeMessage(object sender, EventArgs e)
        {
            Subscribe();
        }

        private void SendMessage(object sender, EventArgs e)
        {
            MessagingCenter.Send<ResultView>(this, "DisplayAlert");
        }
        private void Unsubscribe(object sender, EventArgs e)
        {
            UnsubscribeNoParam();
        }
        private void BackWithMessaging(object sender, EventArgs e)
        {
            SubscribeUnsubscribe();
            this.Navigation.PopAsync();
            MessagingCenter.Send<ResultView>(this, "DisplayAlert");
            UnsubscribeNoParam();
        }
        //Remoção do registro da mensagem
        /*Tem como objetivo remover(Unsubscribe) o registro da Mensagem(<ResultView>, "DisplayAlert"), 
         * importante ressaltar que o tipo do destino da mensagem(<ResultView>) também é chave 
         * para que o MessagingCenter a identifique, para neste caso remove-la*/

        #region Registro
        //Registro da Mensagem
        /*Tem como objecivo declarar qual será a classe de 
         * destino(<ResultView>) e qual será a chave/mensagem ("DisplayAlert") e qual 
         * será a ação, caso exiksta(message => DisplayAlert)
         */
        public void SubscribeUnsubscribe()
        {
            MessagingCenter.Subscribe<ResultView>(this, "DisplayAlert", (message) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Subscribe Alert", "Messaging Works", "Ok");
                    UnsubscribeNoParam();
                });
            });
        }
        #endregion
        public void Subscribe()
        {
            MessagingCenter.Subscribe<ResultView>(this, "DisplayAlert", (message) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Subscribe Alert", "Messaging Works", "Ok");
                });
            });
        }
        public void SubscribeWithParam()
        {
            MessagingCenter.Subscribe<ResultView, string>(this, "Text", (message, args) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Subscribe Alert", args, "Ok");
                    UnsubscribeParam();
                });
            }); 
        }
        public void UnsubscribeNoParam()
        {
            MessagingCenter.Unsubscribe<ResultView>(this, "DisplayAlert");
        }
        public void UnsubscribeParam()
        {
            MessagingCenter.Unsubscribe<ResultView, string> (this, "Text");
        }

        private void BackWithParam(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                SubscribeWithParam();
                MessagingCenter.Send(this, "Text", Text);
                UnsubscribeParam();
                App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                this.DisplayAlert("Ops", "Type something to use that's button", "Ok");
            }
        }
    }
}
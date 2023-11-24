using Plugin.NFC;
using System;
using Xamarin.Forms;

namespace Scan_NFC
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            // Atribui o Entry com o nome 'meuEntry' à variável no código-behind
            meuEntry = (Entry)FindByName("meuEntry");

            // Inicializa o NFC e registra o evento OnMessageReceived
            CrossNFC.Current.OnMessageReceived += NfcMessageReceived;
        }

        private void NfcMessageReceived(ITagInfo tagInfo)
        {
            var idBytes = tagInfo.Identifier;
            var message = BitConverter.ToString(idBytes);

            // Atualiza o Entry com o valor da tag NFC
            Device.BeginInvokeOnMainThread(() =>
            {
                meuEntry.Text = $"Tag NFC Lida - ID: {message}";
                // Exibe um alerta opcional
                DisplayAlert("Tag NFC Lida", $"ID: {message}", "OK");
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine("MainPage OnAppearing");

            CrossNFC.Current.StartListening();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            System.Diagnostics.Debug.WriteLine("MainPage OnDisappearing");

            CrossNFC.Current.StopListening();
        }
    }
}

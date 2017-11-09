using Plugin.Messaging;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        public Message()
        {
            InitializeComponent();
        }

        private void SendSMS_Clicked(object sender, EventArgs e)
        {
            // Send Sms
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms("+84965533254", "Hello Ms.Ngan");
            }
        }

        private void PhoneCall_Clicked(object sender, EventArgs e)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+84965533254", "Ngan");
            }
        }
    }
}
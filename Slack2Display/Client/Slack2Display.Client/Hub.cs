using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Windows.Devices.Gpio;
using Microsoft.Extensions.Logging;
using Slack2Display.Client.Models;
using Windows.Foundation;

namespace Slack2Display.Client
{
    internal sealed class Hub
    {

        private GpioPinValue _pinValue = GpioPinValue.High;
        private const int LED_PIN = 5;
        private GpioPin _pin;
        private string _url;

        private HubConnection _connection;

        public Hub(string url)
        {
            _url = url;

            InitGPIO();

            _connection = InitialiseConnection();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                _connection = InitialiseConnection();
                await _connection.StartAsync();
            };

            _connection.On<MessageModel>("ReceiveCommand", MessageHandler);

            // add to close method
            // connection.DisposeAsync();

        }

        public async Task StartAsync()
        {
            await _connection.StartAsync();
        }

        public async Task StopAsync()
        {
            await _connection.StopAsync();
        }

        private void MessageHandler(MessageModel message)
        {
            _pinValue = (_pinValue == GpioPinValue.High) ? GpioPinValue.Low : GpioPinValue.High;
            _pin.Write(_pinValue);
        }



        private HubConnection InitialiseConnection()
        {
            var handler = GetHandler();
            return new HubConnectionBuilder()
                 .WithUrl(_url, options =>
                 {
                     options.HttpMessageHandlerFactory = (option) =>
                     {
                         var result = handler;
                         return result;
                     };
                 })
                 .Build();
        }


        private HttpClientHandler GetHandler()
        {
            return new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            };
        }


        private void InitGPIO()
        {
            _pin = GpioController.GetDefault().OpenPin(LED_PIN);
            _pin.Write(GpioPinValue.High);
            _pin.SetDriveMode(GpioPinDriveMode.Output);
        }
    }
}

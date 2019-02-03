using System;
using Windows.ApplicationModel.Background;

namespace Slack2Display.Client
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        Hub _hub;
        private string _url = "command hub url here";


        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            taskInstance.Canceled += TaskInstance_Canceled;
            _hub = new Hub(_url);
            try
            {
                await _hub.StartAsync();
            }
            catch (Exception ex)
            {
                await _hub.StopAsync();
                _deferral.Complete();
            }
        }

        private async void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            await _hub.StopAsync();
            _deferral.Complete();
        }
    }
}
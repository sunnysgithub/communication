using communication.shared.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace communication.blazorwebapp.BackgroundServices
{
    public class BackgroundTask : IHostedService, IDisposable
    {

        private readonly ILogger<BackgroundTask> log;
        private Timer _timer;

        private ChannelWriter<Data> _channelWriter;

        public BackgroundTask(
            ILogger<BackgroundTask> logger,
            Channel<Data> channel)
        {
            log = logger;
            _channelWriter = channel.Writer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            log.LogInformation("Background Service started");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            log.LogDebug("Background Service doing work");
            await _channelWriter.WriteAsync(new Data());
            log.LogDebug("Background Service sent data");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log.LogInformation("Background Service is stopped");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

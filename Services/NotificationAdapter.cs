using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationAdapter : INotificationAdapter
    {
        private readonly INotificationService _notificationService;

        public NotificationAdapter(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task ProcessSendAsync<T>(T model)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(model, options);

            await _notificationService.SendAsync(json);
        }
    }
}

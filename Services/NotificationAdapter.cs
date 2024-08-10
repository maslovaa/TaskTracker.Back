using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void ProcessSend<T>(T model)
        {
            var json = JsonSerializer.Serialize(model);

            _notificationService.Send(json);
        }
    }
}

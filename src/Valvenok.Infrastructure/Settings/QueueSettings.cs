using System;
using System.Collections.Generic;
using System.Text;

namespace Valvenok.Infrastructure.Settings
{
    public class QueueSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
    }
}

using System;

namespace webapi.common
{

    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettings {
        public string RedisUrl { get; set; }
        public string LifeTime { get; set; }
        public string RedisOn { get; set; }
        public string RabbitMqQueueHost { get; set; }
    }


    //Connection Strings
    public class ConnectionStrings
    {
        public ReservationEntities ReservationEntities { get; set; }
    }

    public class ReservationEntities
    {
        public string ConnectionStrings   { get; set; }
        public string ProviderName { get; set; }
    }
}
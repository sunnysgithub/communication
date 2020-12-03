using System;

namespace communication.shared.Models
{
    public class Data
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public string Username { get; set; } = "";

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"Guid {Guid}, Username {Username}, Timestamp {Timestamp.ToUniversalTime()}";
        }
    }
}

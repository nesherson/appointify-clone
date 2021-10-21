using System;

namespace Appointify.Data.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public AppointmentStatus Status { get; set; }

        public int? Rate { get; set; }
        public DateTime? RatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}

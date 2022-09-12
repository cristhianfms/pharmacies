using System;

namespace AuthDomain
{
    public class Session
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
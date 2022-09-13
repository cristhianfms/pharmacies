using System;

namespace Domain.AuthDomain
{
    public class Session
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public int UserId { get; set; }
        public virtual AuthUser User { get; set; }
    }
}
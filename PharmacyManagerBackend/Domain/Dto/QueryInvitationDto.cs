using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class QueryInvitationDto
    {
        public string? PharmacyName { get; set; }
        public string? UserName {get; set; }
        public string? Role { get; set; }
    }
}

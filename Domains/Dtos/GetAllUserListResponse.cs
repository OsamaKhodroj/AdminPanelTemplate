using Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class GetAllUserListResponse
    {
        public int Id { get; set; } 
        public string FullName { get; set; } 
        public string EmailAddress { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Country { get; set; } 
    }
}

using Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class AddUserResponse
    {
        public string Message { get; set; }
        public OpStatus Status { get; set; }
    }
}

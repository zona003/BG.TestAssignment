using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.TestAssignment.Models
{
    public class AuthRequest
    {
        
         public required string UserName { get; set; }
         public required string Password { get; set; }
    }
}

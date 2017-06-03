using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace eTenderService.Tempmodel
{
   public class _login
    {
       [Required]
       public string UserName { get; set; }
       [Required]
       public string password { get; set; }
    }
}

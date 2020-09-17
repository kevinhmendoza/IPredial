using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.WebApi.ViewModels.General
{
    public class RegisterUserViewModel
    {
        public TerceroViewModel Tercero { get; set; }
        public UserViewModel Usuario { get; set; }
    }
}
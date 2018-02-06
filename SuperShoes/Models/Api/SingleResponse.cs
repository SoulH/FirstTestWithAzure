using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperShoes.Models.Api
{
    public class SingleResponse
    {
        public bool Success { get; set; }

        public string Error_code { get; set; }
    }
}
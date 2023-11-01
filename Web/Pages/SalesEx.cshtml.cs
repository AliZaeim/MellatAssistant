using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SalesExModel : PageModel
    {
        public string RefCode { get; set; }
        public void OnGet(string code)
        {
            RefCode = code ;
        }
    }
}

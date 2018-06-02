using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevExpress.Xpo.Demo.Pages
{
    public class FourOhFour : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "404 Not found. You broke the internet!";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public static class PDFTemplateGenerator
    {
        
        public static string GetHTMLString(Resume resume)
        {
            var sb = new StringBuilder();
            sb.Append($@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <header><p style='margin-top: 100px'>Recruterra 2023</p></header>
                                <div class='header font36 mrleft16'><h1>{resume.LastName}</h1></div>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTimesheet
{
    internal class MauiInterop
    {
        public static async Task AlertAsync(string message,string title)=> 
                         App.Current!.Windows[0].Page!.DisplayAlert(title, message, "OK");

    }
}

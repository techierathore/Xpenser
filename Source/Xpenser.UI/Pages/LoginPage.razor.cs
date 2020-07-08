using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Xpenser.Models;

namespace Xpenser.UI.Pages
{
    public class LoginBase : ComponentBase
    {
        public SvcData LoginDetails { get; set; } = new SvcData();
        public string LoginMesssage { get; set; }
    }
}

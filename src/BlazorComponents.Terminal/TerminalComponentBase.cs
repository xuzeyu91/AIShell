using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponents.Terminal
{
    public class TerminalComponentBase : ComponentBase
    {
        [Parameter]
        public string Style { get; set; } = "";
    }
}

using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponents.Terminal
{
    public class TerminalEventArgs
    {

        internal TerminalEventArgs(BlazorTerminal source, string value, TerminalInputEventArgs inputEventArgs, TerminalParagraph tp)
        {
            Source = source;
            InputEventArgs = inputEventArgs;
            InputValue = value;
            Paragraph = tp;
        }

        public BlazorTerminal Source { get; private set; }

        public string Key { get; set; }

        public TerminalInputEventArgs InputEventArgs { get; private set; }

        public string InputValue { get; private set; }

        public TerminalParagraph Paragraph { get; private set; }




    }



    public class TerminalInputEventArgs
    {
        internal TerminalInputEventArgs(TerminalInput source, KeyboardEventArgs eventArgs, string inputValue)
        {
            Source = source;
            InputValue = inputValue;
            KeyboardEventArgs = eventArgs;
        }

        public TerminalInput Source { get; private set; }

        public KeyboardEventArgs KeyboardEventArgs { get; private set; }

        public string InputValue { get; set; }

    }

}

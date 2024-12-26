using AIShell.Domain.Repositories;
using AntDesign;
using BlazorComponents.Terminal;
using Microsoft.Identity.Client;

namespace AIShell.Pages.Shell
{
    public partial class ShellPage
    {
        private BlazorTerminal blazorTerminal = new BlazorTerminal();
        private TerminalParagraph para;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            blazorTerminal.ConfigPrompt(new CommandPrompt()
            {
                Name = "visitor",
                Host = "blazor.terminal",
                Path = "~",
                Separator1 = '@',
                Separator2 = ':',
                Prompt = '#',
                AnswerPrompt = ">"
            });
        }
        async void commandEnter(TerminalEventArgs evenArgs)
        {
            //your code
        }

        //OnAnswerEnter event
        async void answerEnter(TerminalEventArgs evenArgs)
        {
            //your code
        }

    }
}
    


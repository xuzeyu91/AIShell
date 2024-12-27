using AIShell.Domain.Repositories;
using AntDesign;
using BlazorComponents.Terminal;
using Microsoft.Identity.Client;

namespace AIShell.Pages.Shell
{
    public partial class ShellPage
    {
        private BlazorTerminal blazorTerminal = default;
        private TerminalParagraph para;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
          
        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {

                //blazorTerminal.ResponseText("BlazorComponents.Terminal is a terminal style web ui component.");
                //blazorTerminal.ResponseText("input ask,html,text,help,? for demonstration");

                var para = blazorTerminal.RespondText("");
                para.AddTextLine("-=-=".Repeat(20));
                para.AddTextLine(@"     ___   _      _____   _    _          _         _");
                para.AddTextLine(@"    /   | | |    / ____| | |  | |        | |       | |");
                para.AddTextLine(@"   / /| | | |   | (___   | |__| |  ___   | |       | |");
                para.AddTextLine(@"  / /_| | | |    \___ \  | |__| | / _ \  | |       | |");
                para.AddTextLine(@" / ____ | | |    ____) | | |  | ||  __/  | |___    | |___");
                para.AddTextLine(@"/_/   |_| |_|    |_____/ |_|  |_| \___|  |______\  |______\");
                para.AddTextLine(@" ");
                para.AddTextLine("- ".Repeat(30));
                para.AddTextLine("AI Shell 是一款通过AI来只能提示操作的Shell工具");
                para.AddTextLine("-=-=".Repeat(20));
                //
                blazorTerminal.ConfigPrompt(new CommandPrompt()
                {
                    Name = "aishell",
                    Host = "root",
                    Path = "~",
                    Separator1 = '@',
                    Separator2 = ':',
                    Prompt = '#',
                    AnswerPrompt = ">"
                });

                blazorTerminal.Return();
            }

        }

        async void commandEnter(TerminalEventArgs evenArgs)
        {
            if (string.IsNullOrEmpty(evenArgs.InputValue))
            {
                blazorTerminal.Return();
                return;
            }


            switch (evenArgs.InputValue)
            {
                case "password":
                    blazorTerminal.RespondText("please input password", needInput: true, isPassword: true);
                    break;
                case "progress":
                    var par1 = blazorTerminal.RespondText("download 00% :", false, false);
                    await _progress(par1);
                    blazorTerminal.Return();
                    break;
                case "lines":
                    var par = blazorTerminal.RespondText("line1", false, false);
                    par.AddTextLine("line2");
                    par.AddTextLine("line3");
                    par.AddTextLine("line4");
                    par.AddTextLine("line5");
                    blazorTerminal.Return();
                    break;
                case "clear":
                    blazorTerminal.Clear();
                    break;
                case "ask":
                    blazorTerminal.RespondText(" continue process? y/n ", needInput: true);
                    break;
                case "html":
                    blazorTerminal.RespondHtml("<a href='/'>this is html response</a>", true);
                    break;
                case "text":
                    blazorTerminal.RespondText("text responese", true);
                    break;
                case "image":
                    blazorTerminal.RespondImage("icon-192.png", autoReturn: true);
                    break;
                case "help":
                case "?":
                    blazorTerminal.RespondText("try ask,html,text,help,? commands for demonstration", true);
                    break;
                default:
                    blazorTerminal.RespondText("unknown command", true);
                    break;
            }

        }

        async void answerEnter(TerminalEventArgs evenArgs)
        {
            blazorTerminal.RespondText($"answer input: {evenArgs.InputValue}");

            blazorTerminal.Return();
        }


        async Task _progress(TerminalParagraph tp)
        {
            int i = 1;
            while (i <= 100)
            {
                tp.UpdateTextLine(0, $"download {i}% :" + "".PadRight(i / 2, '/'));
                //this.StateHasChanged();
                //Thread.Sleep(50);
                await Task.Delay(50);
                //var resp = await httpc.GetAsync("sample-data/weather.json");
                i++;
            }
        }

    }
}
    


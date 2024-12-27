using AIShell.Domain.Repositories;
using AntDesign;
using BlazorComponents.Terminal;
using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;
using Renci.SshNet;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace AIShell.Pages.Shell
{
    public partial class ShellPage : IDisposable
    {
        [Parameter] public string Id { get; set; }

        [Inject] protected ISessions_Repositories _sessions_Repositories { get; set; }
        [Inject] protected MessageService? Message { get; set; }

        private Sessions _sessionModel = new Sessions();

        private BlazorTerminal blazorTerminal = default;
        private TerminalParagraph para;

        //ssh客户端
        private SshClient _sshClient;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _sessionModel = await _sessions_Repositories.GetFirstAsync(p => p.Id == Id);

            var connectionInfo = new ConnectionInfo(
               _sessionModel.Host,
               int.Parse(_sessionModel.Port),
               _sessionModel.User,
               new PasswordAuthenticationMethod(_sessionModel.User, _sessionModel.Password)
           );

            _sshClient = new SshClient(connectionInfo);

            try
            {
                _sshClient.Connect();
                if (_sshClient.IsConnected)
                {
                    // SSH 连接成功，您可以在此执行命令
                    var cmd = _sshClient.CreateCommand("ls -la");
                    var result = cmd.Execute();
  
                }
            }
            catch (Exception ex)
            {
                _ = Message.Error($"SSH 连接失败: {ex.Message}", 2);
            }
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
                para.AddTextLine(@"     ___   _      _____   _    _    ______    _         _");
                para.AddTextLine(@"    /   | | |    / ____| | |  | |  |  ____|  | |       | |");
                para.AddTextLine(@"   / /| | | |   | (___   | |__| |  | |____   | |       | |");
                para.AddTextLine(@"  / /_| | | |    \___ \  | |__| |  |  ____|  | |       | |");
                para.AddTextLine(@" / ____ | | |    ____) | | |  | |  | |____   | |___    | |___");
                para.AddTextLine(@"/_/   |_| |_|    |_____/ |_|  |_|  |______|  |______\  |______\");
                para.AddTextLine(@" ");
                para.AddTextLine("- ".Repeat(30));
                para.AddTextLine("AI Shell 是一款通过AI来智能提示操作的Shell工具");
                para.AddTextLine("-=-=".Repeat(20));
                //
                blazorTerminal.ConfigPrompt(new CommandPrompt()
                {
                    Name = _sessionModel.Host,
                    Host = _sessionModel.User,
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

            try
            {
                switch (evenArgs.InputValue)
                {
                    case "/ai":
                        
                        break;
                    default:
                        if (!_sshClient.IsConnected)
                        {
                            _sshClient.Connect();
                        }
                        // SSH 连接成功，您可以在此执行命令
                        var cmd = _sshClient.CreateCommand($"echo {_sessionModel.Password} | sudo -S {evenArgs.InputValue}");
                        var result = cmd.Execute();
                        var error = cmd.Error; // 捕获标准错误输出
                        var exitStatus = cmd.ExitStatus; // 获取命令的退出状态码

                        if (exitStatus == 0)
                        {
                            blazorTerminal.RespondText(result, true);
                        }
                        else
                        {
                            blazorTerminal.RespondText($"命令执行失败: {error}", true);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _ = Message.Error($"SSH 连接失败: {ex.Message}", 2);
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
        public void Dispose()
        {
            if (_sshClient != null)
            {
                if (_sshClient.IsConnected)
                {
                    _sshClient.Disconnect();
                }
                _sshClient.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
    


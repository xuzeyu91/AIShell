using AIShell.Domain.Repositories;
using AntDesign;
using Microsoft.AspNetCore.Components;
using NPOI.SS.Formula.Functions;

namespace AIShell.Pages.Session
{
    public partial class AddSession
    {
        [Parameter] public string Id { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected MessageService? Message { get; set; }
        [Inject] protected ISessions_Repositories _sessions_Repositories { get; set; }

        private Sessions _sessionModel = new Sessions();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!string.IsNullOrEmpty(Id))
            {
                _sessionModel = await _sessions_Repositories.GetFirstAsync(p => p.Id == Id);
            }
        }

        private async Task HandleSubmit()
        {
            if (string.IsNullOrEmpty(Id))
            {
                _sessionModel.Id = Guid.NewGuid().ToString();

                if (_sessions_Repositories.IsAny(p => p.Name == _sessionModel.Name))
                {
                    _ = Message.Error("名称已存在！", 2);
                    return;
                }
                _sessions_Repositories.Insert(_sessionModel);
            }
            else
            {
                _sessions_Repositories.Update(_sessionModel);
            }

            NavigationManager.NavigateTo("/session/list");
        }
    }
}

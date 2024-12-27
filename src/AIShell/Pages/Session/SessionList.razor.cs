using AIShell.Domain.Repositories;
using AntDesign;
using Microsoft.AspNetCore.Components;
using SqlSugar;
using System.Collections.Generic;

namespace AIShell.Pages.Session
{
    public partial class SessionList
    {
        private Sessions[] _data = { };

        [Inject] protected ISessions_Repositories _sessions_Repositories { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] IConfirmService _confirmService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await InitData("");
        }
        private async Task InitData(string searchKey)
        {
            var list = new List<Sessions> { new Sessions() { CreateTime = DateTime.MaxValue } };
            List<Sessions> data;
            var exp = Expressionable.Create<Sessions>();
            exp.AndIF(!string.IsNullOrEmpty(searchKey), p => p.Name.Contains(searchKey));

            data = await _sessions_Repositories.GetListAsync(exp.ToExpression());
            list.AddRange(data);
            _data = list.OrderByDescending(p => p.CreateTime).ToArray();
            await InvokeAsync(StateHasChanged);
        }
        private async Task Search(string searchKey)
        {
            await InitData(searchKey);
        }

        private void NavigateToAddSession()
        {
            NavigationManager.NavigateTo("/session/add");
        }

        private void Update(string id)
        {
            NavigationManager.NavigateTo($"/app/add/{id}");
        }

        private async Task Delete(string id)
        {
            var content = "是否确认删除会话";
            var title = "删除";
            var result = await _confirmService.Show(content, title, ConfirmButtons.YesNo);
            if (result == ConfirmResult.Yes)
            {
                await _sessions_Repositories.DeleteAsync(id);
                await InitData("");
            }
        }

    }
}

﻿using AIShell.Domain.Repositories;
using Microsoft.AspNetCore.Components;
using AIShell.Utils;
using AIShell.Domain.Utils;
using AntDesign;
using AIShell.Domain.Domain.Model.Enum;

namespace AIShell.Pages.Model
{
    public partial class ModelSetting
    {
        [Inject] IAIModels_Repositories _aIModels_Repositories { get; set; }

        [Inject] protected MessageService? Message { get; set; }
        private AIModels _aiModel { get; set; } = new AIModels();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var model=_aIModels_Repositories.GetFirst(p=>true);
            if (model.IsNotNull())
            {
                _aiModel = model;
            }
        }
        private void HandleSubmit()
        {
            var model=_aIModels_Repositories.GetFirst(p=>true);
            if (model.IsNull())
            {
                if (string.IsNullOrEmpty(_aiModel.EndPoint))
                {
                    _ = Message.Error("必须填写EndPoint", 2);
                }
                //新增
                _aiModel.Id = "001";
                _aIModels_Repositories.Insert(_aiModel);
                _ = Message.Info("保存成功", 2);
            }
            else 
            {
                model.AIType = _aiModel.AIType;
                model.ModelKey = _aiModel.ModelKey;
                model.ModelName = _aiModel.ModelName;
                model.EndPoint = _aiModel.EndPoint;
                _aIModels_Repositories.Update(_aiModel);
                _ = Message.Info("保存成功", 2);
            }
        }
    }
}

using AIShell.Domain.Domain.Model.Enum;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace AIShell.Domain.Repositories
{
    /// <summary>
    /// 连接会话
    /// </summary>
    [SugarTable("Sessions")]
    public partial class Sessions
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        [Required(ErrorMessage = "请输入连接名称")]
        public string Name { get; set; } = "";
        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "请输入连接地址")]
        public string Host { get; set; } = "";
        /// <summary>
        /// 端口
        /// </summary>
        [Required(ErrorMessage = "请输入端口")]
        public string Port { get; set; } = "";
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string User { get; set; } = "";

        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}

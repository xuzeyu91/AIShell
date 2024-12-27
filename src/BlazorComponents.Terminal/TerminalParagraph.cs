using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponents.Terminal
{

    public class TerminalParagraph
    {
        internal TerminalParagraph(BlazorTerminal bt)
        {
            ArgumentNullException.ThrowIfNull(bt, nameof(bt));
            _blazorTerminal = bt;


        }


        private BlazorTerminal _blazorTerminal;

        public Guid Id { get; internal set; }

        public eParagraphType Type { get; set; }

        public eResponseFormat? Format { get; set; }

        // type 为 command 时的内容
        public CommandPrompt Prompt { get; set; }

        // type 为 command 时的输入命令内容
        public string Command { get; set; }

        //绑定段落需要回复时的输入
        public string Answer { get; set; }

        //判断段落是否需要等待输入
        public bool IsReading { get; internal set; }

        public bool IsReadingPassword { get; internal set; }

        //标记，用于用户识别
        public object Attached { get; set; }


        #region response content properties

        //internal string Content { get; set; }

        internal List<string> TextLines { get; set; } = new List<string>();

        internal string Html { get; set; }

        //数据类型渲染对象
        //internal object Data { get; set; }

        #endregion


        public void AddTextLine(string line)
        {
            this.TextLines.Add(line);
            _blazorTerminal.UpdateUI();
        }

        public void AddTextLines(string[] lines)
        {
            this.TextLines.AddRange(lines);
            _blazorTerminal.UpdateUI();
        }

        public void UpdateTextLine(int idx, string line)
        {
            this.TextLines[idx] = line;
            _blazorTerminal.UpdateUI();
        }

    }

    public class CommandPrompt
    {
        public string Name { get; set; } = "name";

        public char Separator1 { get; set; } = '@';

        public string Host { get; set; } = "host";

        public char Separator2 { get; set; } = ':';

        public string Path { get; set; } = "~";

        public char Prompt { get; set; } = '#';

        public string AnswerPrompt { get; set; } = ">";

    }

    public enum eParagraphType
    {
        Command,
        Response,
    }

    public enum eResponseFormat
    {
        Text,
        Html,
        Json,
        Code,
        Image,
        Table,
    }



}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dotNetLab.Widgets
{
    public class LogView : System.Windows.Forms.ListView
    {
        Font fnt_Text;
        public LogView()
        {
            this.View = System.Windows.Forms.View.Details;
            this.LabelWrap = false;
            this.LabelEdit = true;
            this.fnt_Text = new Font("微软雅黑", 9, FontStyle.Bold);
        }
        //"yyyy-MM-dd hh:mm:ss"
        void AddItem(Color clr, string strText)
        {
            this.Items.Add(new System.Windows.Forms.ListViewItem());
            this.Items[this.Items.Count - 1].Font = fnt_Text;
            this.Items[this.Items.Count - 1].BeginEdit();
            this.Items[this.Items.Count - 1].ForeColor = clr;
            this.Items[this.Items.Count - 1].Text = string.Format("[{0}]  {1}", DateTime.Now.ToString("HH:mm:ss"), strText);
        }
        public enum LogType
        {
            /// <summary>  
            /// OpenVPN changed the internal state.l  
            /// </summary>  
            Past,

            /// <summary>  
            /// The management interface wants to say something.  
            /// </summary>  
            Error,

            /// <summary>  
            /// A "normal" message is logged by OpenVPN via Management Interface.  
            /// </summary>  
            Warn

            /// <summary>  
            /// A debug message is sent. This is primary for internal usage.  
            /// </summary>  

        }

        void AddLog(LogType prefix, string text)
        {


            Color rowColor;
            switch (prefix)
            {
                case LogType.Past:
                    rowColor = Color.DarkGreen;
                    break;
                case LogType.Error:
                    rowColor = Color.DarkRed;
                    break;
                case LogType.Warn:
                    rowColor = Color.Orange;
                    break;
                default: // e.g. State  
                    rowColor = Color.Black;
                    break;
            }
            AddItem(rowColor, text);

        }
        public string ErrorLog
        {
            set
            {
                AddLog(LogType.Error, value);
            }
        }
        public string WarnLog
        {
            set
            {
                AddLog(LogType.Warn, value);

            }

        }
        public string PastLog
        {
            set
            {
                AddLog(LogType.Past, value);

            }

        }

    }
}

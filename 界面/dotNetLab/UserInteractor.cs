using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;

namespace dotNetLab
{
   public class UserInteractor
    {
        public delegate void GenerateCtrlCallback(object sender, DragEventArgs e);
        public event GenerateCtrlCallback GenerateCtrl = null;

        Control ctrl_Host;
        Control ctrl_Parasite;
        public Control Host 
        {
            get
            {
                return ctrl_Host;
            }
            set
            {
                this.ctrl_Host = value;
                value.AllowDrop = true;
                value.DragEnter += Ctrl_DragEnter;
                value.DragDrop += Ctrl_DragDrop;
            }
        }
        public Control Parasite
        {
            get
            {
                return ctrl_Parasite;
            }
            set
            {
                this.ctrl_Parasite = value;
               
                value.MouseMove += Value_MouseMove;
            }
        }
        public UserInteractor()
        { }
        public UserInteractor(Control ctrlHost,Control ctrlParasite)
        {
            Host = ctrlHost;
            ctrl_Parasite = ctrlParasite;
        }
        private void Value_MouseMove(object sender, MouseEventArgs e)
        {
            //左键的话，标志位为true（表示拖拽开始）
            if ((e.Button == System.Windows.Forms.MouseButtons.Left))
            {
                if (!ctrl_Parasite.ClientRectangle.Contains(e.Location))
                    ctrl_Parasite.DoDragDrop(ctrl_Parasite, DragDropEffects.Copy | DragDropEffects.Move);
                //形成拖拽效果，移动+拷贝的组合效果
            }
        }
        private void Ctrl_DragDrop(object sender, DragEventArgs e)
        {
            if (GenerateCtrl == null)
            {
                Tipper.Tip_Info_Error("未绑定生成事件！");
                return;
            }
            else
            {
                GenerateCtrl(sender, e);
            }

            ////拖放完毕之后，自动生成新控件
            //Button btn = new Button();
            //btn.Size = button1.Size;
            //btn.Location = this.PointToClient(new Point(e.X, e.Y));
            ////用这个方法计算出客户端容器界面的X，Y坐标。否则直接使用X，Y是屏幕坐标
            //this.Controls.Add(btn);
            //btn.Text = "按钮" + count.ToString();
            //count = count + 1;
        }

        private void Ctrl_DragEnter(object sender, DragEventArgs e)
        {
            //当Button被拖拽到WinForm上时候，鼠标效果出现
            if ((e.Data.GetDataPresent(typeof(Button))))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        
    }
}

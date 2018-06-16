using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI_DB_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.mobileButton1 = new dotNetLab.Widgets.MobileButton();
            this.mobileProgressBar1 = new dotNetLab.Widgets.MobileProgressBar();
            this.SuspendLayout();
            // 
            // mobileButton1
            // 
            this.mobileButton1.BackColor = System.Drawing.Color.Transparent;
            this.mobileButton1.BorderColor = System.Drawing.Color.Empty;
            this.mobileButton1.BorderThickness = -1;
            this.mobileButton1.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.mobileButton1.DataBindingInfo = null;
            this.mobileButton1.EnableFlag = false;
            this.mobileButton1.EnableMobileRound = false;
            this.mobileButton1.EnableTextRenderHint = false;
            this.mobileButton1.FlagAlign = dotNetLab.Widgets.Alignments.Left;
            this.mobileButton1.FlagColor = System.Drawing.Color.DodgerBlue;
            this.mobileButton1.FlagThickness = 5;
            this.mobileButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mobileButton1.ForeColor = System.Drawing.Color.White;
            this.mobileButton1.GapBetweenTextFlag = 10;
            this.mobileButton1.GapBetweenTextImage = 8;
            this.mobileButton1.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.mobileButton1.ImageSize = new System.Drawing.Size(0, 0);
            this.mobileButton1.LEDStyle = false;
            this.mobileButton1.Location = new System.Drawing.Point(336, 280);
            this.mobileButton1.MainBindableProperty = "mobileButton1";
            this.mobileButton1.Name = "mobileButton1";
            this.mobileButton1.NeedAnimation = true;
            this.mobileButton1.NormalColor = System.Drawing.Color.DodgerBlue;
            this.mobileButton1.PressColor = System.Drawing.Color.Cyan;
            this.mobileButton1.Radius = 10;
            this.mobileButton1.Size = new System.Drawing.Size(150, 50);
            this.mobileButton1.Source = null;
            this.mobileButton1.TabIndex = 0;
            this.mobileButton1.Text = "mobileButton1";
            this.mobileButton1.UIElementBinders = null;
            this.mobileButton1.UnderLine = false;
            this.mobileButton1.UnderLineColor = System.Drawing.Color.DarkGray;
            this.mobileButton1.UnderLineThickness = 2F;
            this.mobileButton1.Vertical = false;
            this.mobileButton1.WhereReturn = ((byte)(0));
            // 
            // mobileProgressBar1
            // 
            this.mobileProgressBar1.Alignment = dotNetLab.Widgets.Alignments.Left;
            this.mobileProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.mobileProgressBar1.BallBorderColor = System.Drawing.Color.DarkGray;
            this.mobileProgressBar1.BallBorderThickness = 2F;
            this.mobileProgressBar1.BallProgressColor = System.Drawing.Color.DodgerBlue;
            this.mobileProgressBar1.BottomColor = System.Drawing.Color.Silver;
            this.mobileProgressBar1.CenterColor = System.Drawing.Color.White;
            this.mobileProgressBar1.CenterSize = 124;
            this.mobileProgressBar1.DataBindingInfo = null;
            this.mobileProgressBar1.EqualDivideParts = 5;
            this.mobileProgressBar1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.mobileProgressBar1.ForeColor = System.Drawing.Color.DimGray;
            this.mobileProgressBar1.Location = new System.Drawing.Point(78, 102);
            this.mobileProgressBar1.MainBindableProperty = "20";
            this.mobileProgressBar1.Name = "mobileProgressBar1";
            this.mobileProgressBar1.ProgressBarStyle = dotNetLab.Widgets.MobileProgressBar.ProgressBarStyles.Ball;
            this.mobileProgressBar1.ProgressColor = System.Drawing.Color.DodgerBlue;
            this.mobileProgressBar1.RingColor = System.Drawing.Color.DodgerBlue;
            this.mobileProgressBar1.RingThickness = 13;
            this.mobileProgressBar1.Size = new System.Drawing.Size(150, 150);
            this.mobileProgressBar1.SplitLineColor = System.Drawing.Color.White;
            this.mobileProgressBar1.SplitLineThickness = 1F;
            this.mobileProgressBar1.Step = 5F;
            this.mobileProgressBar1.TabIndex = 1;
            this.mobileProgressBar1.TextFormat = "{0}%";
            this.mobileProgressBar1.UIElementBinders = null;
            this.mobileProgressBar1.Value = 20F;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(547, 359);
            this.Controls.Add(this.mobileProgressBar1);
            this.Controls.Add(this.mobileButton1);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
    }
}

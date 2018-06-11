using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    [ToolboxItem(true),ToolboxBitmap(typeof(ColorDecorator), "ICons.ColorDecorator.bmp")]
   public class ColorDecorator :UIElement
    {
        private Circle roundX6;
        private Circle roundX1;
        private Circle roundX5;
        private Circle roundX3;
        private Circle roundX4;
        private Circle roundX2;

        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.BackColor = Color.Transparent;
            

        }
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.roundX6 = new dotNetLab.Widgets.Circle();
            this.roundX1 = new dotNetLab.Widgets.Circle();
            this.roundX5 = new dotNetLab.Widgets.Circle();
            this.roundX3 = new dotNetLab.Widgets.Circle();
            this.roundX4 = new dotNetLab.Widgets.Circle();
            this.roundX2 = new dotNetLab.Widgets.Circle();
            this.SuspendLayout();
            // 
            // roundX6
            // 
            this.roundX6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.roundX6.BackColor = System.Drawing.Color.Transparent;
            this.roundX6.ClipCircleRegion = true;
            this.roundX6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.roundX6.BorderThickness = 2F;
            this.roundX6.CenterImage = false;
            this.roundX6.Effect = null;
            this.roundX6.Fill = true;
            this.roundX6.FillColor = System.Drawing.Color.Fuchsia;
            this.roundX6.ImagePostion = new System.Drawing.Point(0, 0);
            this.roundX6.ImageSize = new System.Drawing.SizeF(0F, 0F);
            this.roundX6.Location = new System.Drawing.Point(82, 34);
            this.roundX6.MouseDownColor = System.Drawing.Color.Fuchsia;
            this.roundX6.Name = "roundX6";
            this.roundX6.NeedEffect = true;
            this.roundX6.Size = new System.Drawing.Size(20, 20);
            this.roundX6.Source = null;
            this.roundX6.TabIndex = 14;
            this.roundX6.WhichShap = 6;
            // 
            // roundX1
            // 
            this.roundX1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.roundX1.BackColor = System.Drawing.Color.Transparent;
            this.roundX1.ClipCircleRegion = true;
            this.roundX1.BorderColor = System.Drawing.Color.Empty;
            this.roundX1.BorderThickness = 2F;
            this.roundX1.CenterImage = false;
            this.roundX1.Effect = null;
            this.roundX1.Fill = true;
            this.roundX1.FillColor = System.Drawing.Color.DimGray;
            this.roundX1.ImagePostion = new System.Drawing.Point(0, 0);
            this.roundX1.ImageSize = new System.Drawing.SizeF(0F, 0F);
            this.roundX1.Location = new System.Drawing.Point(6, 7);
            this.roundX1.MouseDownColor = System.Drawing.Color.DimGray;
            this.roundX1.Name = "roundX1";
            this.roundX1.NeedEffect = true;
            this.roundX1.Size = new System.Drawing.Size(45, 45);
            this.roundX1.Source = null;
            this.roundX1.TabIndex = 19;
            this.roundX1.WhichShap = 6;
            // 
            // roundX5
            // 
            this.roundX5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.roundX5.BackColor = System.Drawing.Color.Transparent;
            this.roundX5.ClipCircleRegion = true;
            this.roundX5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.roundX5.BorderThickness = 2F;
            this.roundX5.CenterImage = false;
            this.roundX5.Effect = null;
            this.roundX5.Fill = true;
            this.roundX5.FillColor = System.Drawing.Color.DodgerBlue;
            this.roundX5.ImagePostion = new System.Drawing.Point(0, 0);
            this.roundX5.ImageSize = new System.Drawing.SizeF(0F, 0F);
            this.roundX5.Location = new System.Drawing.Point(121, 14);
            this.roundX5.MouseDownColor = System.Drawing.Color.DodgerBlue;
            this.roundX5.Name = "roundX5";
            this.roundX5.NeedEffect = true;
            this.roundX5.Size = new System.Drawing.Size(20, 20);
            this.roundX5.Source = null;
            this.roundX5.TabIndex = 15;
            this.roundX5.WhichShap = 6;
            // 
            // roundX3
            // 
            this.roundX3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.roundX3.BackColor = System.Drawing.Color.Transparent;
            this.roundX3.ClipCircleRegion = true;
            this.roundX3.BorderColor = System.Drawing.Color.Red;
            this.roundX3.BorderThickness = 2F;
            this.roundX3.CenterImage = false;
            this.roundX3.Effect = null;
            this.roundX3.Fill = true;
            this.roundX3.FillColor = System.Drawing.Color.Red;
            this.roundX3.ImagePostion = new System.Drawing.Point(0, 0);
            this.roundX3.ImageSize = new System.Drawing.SizeF(0F, 0F);
            this.roundX3.Location = new System.Drawing.Point(46, 22);
            this.roundX3.MouseDownColor = System.Drawing.Color.Red;
            this.roundX3.Name = "roundX3";
            this.roundX3.NeedEffect = true;
            this.roundX3.Size = new System.Drawing.Size(30, 30);
            this.roundX3.Source = null;
            this.roundX3.TabIndex = 17;
            this.roundX3.WhichShap = 6;
            // 
            // roundX4
            // 
            this.roundX4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.roundX4.BackColor = System.Drawing.Color.Transparent;
            this.roundX4.ClipCircleRegion = true;
            this.roundX4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.roundX4.BorderThickness = 2F;
            this.roundX4.CenterImage = false;
            this.roundX4.Effect = null;
            this.roundX4.Fill = true;
            this.roundX4.FillColor = System.Drawing.Color.Green;
            this.roundX4.ImagePostion = new System.Drawing.Point(0, 0);
            this.roundX4.ImageSize = new System.Drawing.SizeF(0F, 0F);
            this.roundX4.Location = new System.Drawing.Point(100, 21);
            this.roundX4.MouseDownColor = System.Drawing.Color.Green;
            this.roundX4.Name = "roundX4";
            this.roundX4.NeedEffect = true;
            this.roundX4.Size = new System.Drawing.Size(30, 30);
            this.roundX4.Source = null;
            this.roundX4.TabIndex = 16;
            this.roundX4.WhichShap = 6;
            // 
            // roundX2
            // 
            this.roundX2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.roundX2.BackColor = System.Drawing.Color.Transparent;
            this.roundX2.ClipCircleRegion = true;
            this.roundX2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.roundX2.BorderThickness = 2F;
            this.roundX2.CenterImage = false;
            this.roundX2.Effect = null;
            this.roundX2.Fill = false;
            this.roundX2.FillColor = System.Drawing.Color.DimGray;
            this.roundX2.ImagePostion = new System.Drawing.Point(0, 0);
            this.roundX2.ImageSize = new System.Drawing.SizeF(0F, 0F);
            this.roundX2.Location = new System.Drawing.Point(68, 1);
            this.roundX2.MouseDownColor = System.Drawing.Color.DimGray;
            this.roundX2.Name = "roundX2";
            this.roundX2.NeedEffect = true;
            this.roundX2.Size = new System.Drawing.Size(40, 40);
            this.roundX2.Source = null;
            this.roundX2.TabIndex = 18;
            this.roundX2.WhichShap = 6;
            // 
            // ColorDecorator
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.roundX6);
            this.Controls.Add(this.roundX1);
            this.Controls.Add(this.roundX5);
            this.Controls.Add(this.roundX3);
            this.Controls.Add(this.roundX4);
            this.Controls.Add(this.roundX2);
            this.Name = "ColorDecorator";
            this.Size = new System.Drawing.Size(150, 53);
            this.ResumeLayout(false);

        }
    }
}

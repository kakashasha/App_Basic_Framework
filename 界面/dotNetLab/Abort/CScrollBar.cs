//        using System;
//  2 using System.Collections.Generic;
//  3 using System.ComponentModel;
//  4 using System.Drawing;
//  5 using System.Data;
//  6 using System.Text;
//  7 using System.Windows.Forms;
//  8 using System.Windows.Forms.Design;
//  9 using System.Diagnostics;
 

//namespace dotNetLab
//{
//    class CScrollBar
//    {
//  namespace Winamp
//  {
//      [Designer(typeof(ScrollbarControlDesigner))]
//      public partial class CustomScrollbar : UserControl
//      {
  
//          protected Color moChannelColor = Color.Empty;
//          protected Image moUpArrowImage = null;
//                 protected Image moDownArrowImage = null;
//                  protected Image moThumbArrowImage = null;
  
//          protected Image moThumbTopImage = null;
//          protected Image moThumbTopSpanImage = null;
//          protected Image moThumbBottomImage = null;
//          protected Image moThumbBottomSpanImage = null;
//          protected Image moThumbMiddleImage = null;
  
//          protected int moLargeChange = 10;
//          protected int moSmallChange = 1;
//          protected int moMinimum = 0;
//          protected int moMaximum = 100;
//          protected int moValue = 0;
//          private int nClickPoint;
  
//          protected int moThumbTop = 0;
  
//          protected bool moAutoSize = false;
  
//          private bool moThumbDown = false;
//          private bool moThumbDragging = false;
  
//          public new event EventHandler Scroll = null;
//          public event EventHandler ValueChanged = null;
  
//          private int GetThumbHeight()
//          {
//              int nTrackHeight = (this.Height - (UpArrowImage.Height + DownArrowImage.Height));
//              float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
//              int nThumbHeight = (int)fThumbHeight;
  
//              if (nThumbHeight > nTrackHeight)
//              {
//                  nThumbHeight = nTrackHeight;
//                  fThumbHeight = nTrackHeight;
//              }
//              if (nThumbHeight < 56)
//              {
//                  nThumbHeight = 56;
//                  fThumbHeight = 56;
//              }
  
//              return nThumbHeight;
//          }
  
//          public CustomScrollbar()
//          {
  
//              InitializeComponent();
//              SetStyle(ControlStyles.ResizeRedraw, true);
//              SetStyle(ControlStyles.AllPaintingInWmPaint, true);
//              SetStyle(ControlStyles.DoubleBuffer, true);
  
//              moChannelColor = Color.FromArgb(51, 166, 3);
//              UpArrowImage = BASSSkin.uparrow;
//              DownArrowImage = BASSSkin.downarrow;
  
  
//              ThumbBottomImage = BASSSkin.ThumbBottom;
  
//              ThumbMiddleImage = BASSSkin.ThumbMiddle;
  
//              this.Width = UpArrowImage.Width;
//              base.MinimumSize = new Size(UpArrowImage.Width, UpArrowImage.Height + DownArrowImage.Height + GetThumbHeight());
//          }
  
//          [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description
  
//  ("LargeChange")]
//          public int LargeChange
//          {
//              get { return moLargeChange; }
//              set
//              {
//                  moLargeChange = value;
//                  Invalidate();
//              }
//          }
  
//          [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description

// ("SmallChange")]
//         public int SmallChange
//         {
//             get { return moSmallChange; }
//             set
//             {
//                 moSmallChange = value;
//                 Invalidate();
//             }
//         }
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Minimum")]
//         public int Minimum
//         {
//             get { return moMinimum; }
//             set
//             {
//                 moMinimum = value;
//                 Invalidate();
//             }
//         }
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Maximum")]
//         public int Maximum
//         {
//             get { return moMaximum; }
//             set
//             {
//                 moMaximum = value;
//                 Invalidate();
//             }
//         }
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Value")]
//         public int Value
//         {
//             get { return moValue; }
//             set
//             {
//                 moValue = value;
 
//                 int nTrackHeight = (this.Height - (UpArrowImage.Height + DownArrowImage.Height));
//                 float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
//                 int nThumbHeight = (int)fThumbHeight;
 
//                 if (nThumbHeight > nTrackHeight)
//                 {
//                     nThumbHeight = nTrackHeight;
//                     fThumbHeight = nTrackHeight;
//                 }
//                 if (nThumbHeight < 56)
//                 {
//                     nThumbHeight = 56;
//                     fThumbHeight = 56;
//                 }
 
                 
//                 int nPixelRange = nTrackHeight - nThumbHeight;
//                 int nRealRange = (Maximum - Minimum) - LargeChange;
//                 float fPerc = 0.0f;
//                 if (nRealRange != 0)
//                 {
//                     fPerc = (float)moValue / (float)nRealRange;
 
//                 }
 
//                 float fTop = fPerc * nPixelRange;
//                 moThumbTop = (int)fTop;
 
 
//                 Invalidate();
//             }
//         }
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Channel Color")]
//         public Color ChannelColor
//         {
//             get { return moChannelColor; }
//             set { moChannelColor = value; }
//         }
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
//         public Image UpArrowImage
//         {
//             get { return moUpArrowImage; }
//             set { moUpArrowImage = value; }
//         }
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
//         public Image DownArrowImage
//         {
//             get { return moDownArrowImage; }
//             set { moDownArrowImage = value; }
//         }
 
 
 
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
//         public Image ThumbBottomImage
//         {
//             get { return moThumbBottomImage; }
//             set { moThumbBottomImage = value; }
//         }
 
 
 
//         [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
//         public Image ThumbMiddleImage
//         {
//             get { return moThumbMiddleImage; }
//             set { moThumbMiddleImage = value; }
//         }
 
//         protected override void OnPaint(PaintEventArgs e)
//         {
 
//             e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
 
//             if (UpArrowImage != null)
//             {
//                 e.Graphics.DrawImage(UpArrowImage, new Rectangle(new Point(0, 0), new Size(this.Width, UpArrowImage.Height)));
//             }
 
//             Brush oBrush = new SolidBrush(moChannelColor);
//             Brush oWhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
             
//             e.Graphics.FillRectangle(oWhiteBrush, new Rectangle(0, UpArrowImage.Height, 1, (this.Height - DownArrowImage.Height)));
//             e.Graphics.FillRectangle(oWhiteBrush, new Rectangle(this.Width - 1, UpArrowImage.Height, 1, (this.Height - 
 
// DownArrowImage.Height)));
 
            
//             e.Graphics.DrawImage(ThumbBottomImage, new Rectangle(0, UpArrowImage.Height, this.Width, (this.Height - DownArrowImage.Height)));
          
//             int nTrackHeight = (this.Height - (UpArrowImage.Height + DownArrowImage.Height));
//             float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
//             int nThumbHeight = (int)fThumbHeight;
 
//             if (nThumbHeight > nTrackHeight)
//             {
//                 nThumbHeight = nTrackHeight;
//                 fThumbHeight = nTrackHeight;
//             }
          
//             if (nThumbHeight < 56)
//             {
//                 nThumbHeight = 56;
//                 fThumbHeight = 56;
//             }
 
             
//             int nTop = moThumbTop;//0
//             nTop += UpArrowImage.Height;//9px
 
             
//             e.Graphics.DrawImage(ThumbMiddleImage, new Rectangle(0, nTop, this.Width, ThumbMiddleImage.Height));
 
 
             
//             if (DownArrowImage != null)
//             {
//                 e.Graphics.DrawImage(DownArrowImage, new Rectangle(new Point(0, (this.Height - DownArrowImage.Height)), new Size(this.Width, 
 
// DownArrowImage.Height)));
//             }
 
//         }
 
 
//         public override bool AutoSize
//         {
//             get
//             {
//                 return base.AutoSize;
//             }
//             set
//             {
//                 base.AutoSize = value;
//                 if (base.AutoSize)
//                 {
//                     this.Width = moUpArrowImage.Width;
//                 }
//             }
//         }
 
//         private void InitializeComponent()
//         {
//             this.SuspendLayout();
           
//             this.Name = "CustomScrollbar";
//             this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseDown);
//             this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseMove);
//             this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseUp);
//             this.ResumeLayout(false);
 
//         }
 
//         private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e)
//         {
//             Point ptPoint = this.PointToClient(Cursor.Position);
//             int nTrackHeight = (this.Height - (UpArrowImage.Height + DownArrowImage.Height));
//             float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
//             int nThumbHeight = (int)fThumbHeight;
 
//             if (nThumbHeight > nTrackHeight)
//             {
//                 nThumbHeight = nTrackHeight;
//                 fThumbHeight = nTrackHeight;
//             }
//             if (nThumbHeight < 56)
//             {
//                 nThumbHeight = 56;
//                 fThumbHeight = 56;
//             }
 
//             int nTop = moThumbTop;
//             nTop += UpArrowImage.Height;
 
 
//             Rectangle thumbrect = new Rectangle(new Point(1, nTop), new Size(ThumbMiddleImage.Width, nThumbHeight));
//             if (thumbrect.Contains(ptPoint))
//             {
 
              
//                 nClickPoint = (ptPoint.Y - nTop);
                
//                 this.moThumbDown = true;
//             }
 
//             Rectangle uparrowrect = new Rectangle(new Point(1, 0), new Size(UpArrowImage.Width, UpArrowImage.Height));
//             if (uparrowrect.Contains(ptPoint))
//             {
 
//                 int nRealRange = (Maximum - Minimum) - LargeChange;
//                 int nPixelRange = (nTrackHeight - nThumbHeight);
//                 if (nRealRange > 0)
//                 {
//                     if (nPixelRange > 0)
//                     {
//                         if ((moThumbTop - SmallChange) < 0)
//                             moThumbTop = 0;
//                         else
//                             moThumbTop -= SmallChange;
 
                       
//                         float fPerc = (float)moThumbTop / (float)nPixelRange;
//                         float fValue = fPerc * (Maximum - LargeChange);
 
//                         moValue = (int)fValue;
//                         Debug.WriteLine(moValue.ToString());
 
//                         if (ValueChanged != null)
//                             ValueChanged(this, new EventArgs());
 
//                         if (Scroll != null)
//                             Scroll(this, new EventArgs());
 
//                         Invalidate();
//                     }
//                 }
//             }
 
//             Rectangle downarrowrect = new Rectangle(new Point(1, UpArrowImage.Height + nTrackHeight), new Size(UpArrowImage.Width, 
 
// UpArrowImage.Height));
//             if (downarrowrect.Contains(ptPoint))
//             {
//                 int nRealRange = (Maximum - Minimum) - LargeChange;
//                 int nPixelRange = (nTrackHeight - nThumbHeight);
//                 if (nRealRange > 0)
//                 {
//                     if (nPixelRange > 0)
//                     {
//                         if ((moThumbTop + SmallChange) > nPixelRange)
//                             moThumbTop = nPixelRange;
//                         else
//                             moThumbTop += SmallChange;
 
                     
//                         float fPerc = (float)moThumbTop / (float)nPixelRange;
//                         float fValue = fPerc * (Maximum - LargeChange);
 
//                         moValue = (int)fValue;
//                         Debug.WriteLine(moValue.ToString());
 
//                         if (ValueChanged != null)
//                             ValueChanged(this, new EventArgs());
 
//                         if (Scroll != null)
//                             Scroll(this, new EventArgs());
 
//                         Invalidate();
//                     }
//                 }
//             }
//         }
 
//         private void CustomScrollbar_MouseUp(object sender, MouseEventArgs e)
//         {
//             this.moThumbDown = false;
//             this.moThumbDragging = false;
//         }
 
//         private void MoveThumb(int y)
//         {
//             int nRealRange = Maximum - Minimum;
//             int nTrackHeight = (this.Height - (UpArrowImage.Height + DownArrowImage.Height));
//             float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
//             int nThumbHeight = (int)fThumbHeight;
 
//             if (nThumbHeight > nTrackHeight)
//             {
//                 nThumbHeight = nTrackHeight;
//                 fThumbHeight = nTrackHeight;
//             }
//             if (nThumbHeight < 56)
//             {
//                 nThumbHeight = 56;
//                 fThumbHeight = 56;
//             }
 
//             int nSpot = nClickPoint;
 
//             int nPixelRange = (nTrackHeight - nThumbHeight);
//             if (moThumbDown && nRealRange > 0)
//             {
//                 if (nPixelRange > 0)
//                 {
//                     int nNewThumbTop = y - (UpArrowImage.Height + nSpot);
 
//                     if (nNewThumbTop < 0)
//                     {
//                         moThumbTop = nNewThumbTop = 0;
//                     }
//                     else if (nNewThumbTop > nPixelRange)
//                     {
//                         moThumbTop = nNewThumbTop = nPixelRange;
//                     }
//                     else
//                     {
//                         moThumbTop = y - (UpArrowImage.Height + nSpot);
//                     }
 
                   
//                     float fPerc = (float)moThumbTop / (float)nPixelRange;
//                     float fValue = fPerc * (Maximum - LargeChange);
//                     moValue = (int)fValue;
//                     Debug.WriteLine(moValue.ToString());
 
//                     Application.DoEvents();
 
//                     Invalidate();
//                 }
//             }
//         }
 
//         private void CustomScrollbar_MouseMove(object sender, MouseEventArgs e)
//         {
//             if (moThumbDown == true)
//             {
//                 this.moThumbDragging = true;
//             }
 
//             if (this.moThumbDragging)
//             {
 
//                 MoveThumb(e.Y);
//             }
 
//             if (ValueChanged != null)
//                 ValueChanged(this, new EventArgs());
 
//             if (Scroll != null)
//                 Scroll(this, new EventArgs());
//         }
 
//     }
 
//     internal class ScrollbarControlDesigner : System.Windows.Forms.Design.ControlDesigner
//     {
 
 
 
//         public override SelectionRules SelectionRules
//         {
//             get
//             {
//                 SelectionRules selectionRules = base.SelectionRules;
//                 PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(this.Component)["AutoSize"];
//                 if (propDescriptor != null)
//                 {
//                     bool autoSize = (bool)propDescriptor.GetValue(this.Component);
//                     if (autoSize)
//                     {
//                         selectionRules = SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.BottomSizeable | 
 
// SelectionRules.TopSizeable;
//                     }
//                     else
//                     {
//                         selectionRules = SelectionRules.Visible | SelectionRules.AllSizeable | SelectionRules.Moveable;
//                     }
//                 }
//                 return selectionRules;
//             }
//         }
//     }
// }
//    }
//}


using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace dotNetLab.Widgets.Framework
{
    public struct DrageMoveArgs
    {
        public Control ctrlParent;
        public int X, Y;
        public int nSelfWidth;
        public int nSelfHight;
        public int nParentWidth;
        public int nParentHeight;
          public enum DragableDirections
        {
            Left, Right, Top, Down
        }
        public DragableDirections DragableDirection;
       public int nDeltaX, nDeltaY;

    }

   
    public class DragableElement : RoundElement
    {
        Point pnt_MouseDownPos;
       
            public delegate void DragCallback(ref DrageMoveArgs e);

            public delegate void DoTaskCallback();

            protected DrageMoveArgs dma;

            private Orientation enu_Orientation;

            public event DragCallback DragMove;
             
            public event DoTaskCallback DoTask;
            [Category("外观")]
            public Orientation Orientation
            {
                get
                {
                    return this.enu_Orientation;
                }
                set
                {
                    this.enu_Orientation = value;
                    this.Refresh();
                }
            }

            public virtual void PrepareDragable()
            {
            pnt_MouseDownPos = new Point();
            this.MouseMove += new MouseEventHandler(this.Me_MouseMove);
            this.MouseDown += DragableElement_MouseDown;
           
            }
             

        private void DragableElement_MouseDown(object sender, MouseEventArgs e)
        {
            
            pnt_MouseDownPos.X = e.X;
            pnt_MouseDownPos.Y = e.Y;
        }

        protected virtual void HandleDragMove(DrageMoveArgs e)
            {
                this.DragMove(ref e);
                this.pnt_Modify.X = e.X;
                this.pnt_Modify.Y = e.Y;
                base.Location = this.pnt_Modify;
            }
            private void CollectDragMoveInfo(Point pnt)
            {
                this.dma.X = pnt.X;
            this.dma.nDeltaX = Math.Abs(pnt.X);
                this.dma.Y = pnt.Y;
            dma.nDeltaY = Math.Abs(pnt.Y);
                 if(this.Orientation== Orientation.Horizontal)
            {
                if (pnt.X < 0)
                    dma.DragableDirection = DrageMoveArgs.DragableDirections.Left;
                else
                    dma.DragableDirection = DrageMoveArgs.DragableDirections.Right;
            }
           else
            {
                if (pnt.Y > 0)
                    dma.DragableDirection = DrageMoveArgs.DragableDirections.Down;
                else
                    dma.DragableDirection = DrageMoveArgs.DragableDirections.Top;
            }
                this.dma.nParentWidth = base.Parent.Width;
                this.dma.nParentHeight = base.Parent.Height;
                this.dma.nSelfWidth = base.Width;
            this.dma.nSelfHight = base.Height;
            }
            protected void Me_MouseMove(object sender, MouseEventArgs e)
            {
                 
                if (e.Button == MouseButtons.Left)
                {
                    switch (this.enu_Orientation)
                    {
                        case Orientation.Horizontal:
                            {
                            // this.pnt_Modify.X = this.Location.X + e.X;
                            //this.pnt_Modify.X - this.Width / 2;
                            this.pnt_Modify.X = e.X - pnt_MouseDownPos.X   ;
                                this.pnt_Modify.Y = this.Location.Y;
                                this.CollectDragMoveInfo(this.pnt_Modify);
                                this.HandleDragMove(this.dma);
                                
                                if (this.DoTask != null)
                                {
                                    this.DoTask();
                                }
                                break;
                            }
                        case Orientation.Vertical:
                            {
                            //this.pnt_Modify.X = base.Location.X;
                            //this.pnt_Modify.Y = base.Location.Y + e.Y;
                            //this.pnt_Modify.Y - base.Height / 2;
                            this.pnt_Modify.Y = e.Y - pnt_MouseDownPos.Y;
                                this.CollectDragMoveInfo(this.pnt_Modify);
                                this.HandleDragMove(this.dma);
                                
                                if (this.DoTask != null)
                                {
                                    this.DoTask();
                                }
                                break;
                            }
                      
                    }
                }
            }
        }
    }

/*
                            default:
                            this.pnt_Modify.X = base.Location.X + e.X;
                            this.pnt_Modify.Y = base.Location.Y + e.Y;
                            this.CollectDragMoveInfo(this.pnt_Modify);
                            this.HandleDragMove(this.dma);
                            break;
     */


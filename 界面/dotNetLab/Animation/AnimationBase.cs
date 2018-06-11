using System;
using System.Threading;
using System.Windows.Forms;
using dotNetLab.Widgets;

namespace dotNetLab.Animation
{
    public  class AnimationBase
    {
        public Timer tmr;
        private UIElement view;
        //private int nTickTime;
        protected int LifeTime;
        public Object[] objects = null;
        public int nTimes = -1;
        public AnimationBase()
        {
          tmr = new Timer();  
          tmr.Tick += TmrOnTick;
        }

       public void Connect(UIElement ctrl,int nTickTime,int nLifeTime)
        {
            view = ctrl;
            this.LifeTime = nLifeTime;
            tmr.Interval = nTickTime;
            nTimes = nLifeTime / nTickTime;
            if (nLifeTime % nTickTime != 0)
                nTimes += 1;
            

        }

        public void Play()
        {
            tmr.Start();
        }

        public void Stop()
        {
            tmr.Stop();
        }
        protected virtual void TmrOnTick()
        {
            view.Invoke(view.DoAnimation);
        }
        ~AnimationBase()
        {
            tmr.Terminal();
            
        }
        
    }
    public class Timer
    {
        private Thread thd_Main;
        public delegate void TickCallback();
         

        public event TickCallback Tick = null;
        public int Interval = 100;
        bool Enable = false;
         

        public Timer()
        {
              
            

        }

        void InternalRun()
        {
            Enable = true;
            while (true)
            {
                Thread.Sleep(Interval);
               

                if (!Enable)
                    break;
                
                    if (Tick != null)
                    {
                        Tick();
                    }
                    else
                    {
                        Console.WriteLine("定时器Tick事件未设置");
                    }
                 
            }
        }
        public void Start()
        {
            
                thd_Main = new Thread(InternalRun);
                thd_Main.Start();
            
        }
     

        public void Stop()
        { 
           this.Enable = false ;

        }
        public void Terminal()
        {
            this.Enable = false;
            
           // thd_Main.Join();
        }
    }
}
using System;
using System.Security.Policy;
using System.Threading;

namespace dotNetLab.Common
{
    public class Timer
    {
        private Thread thd_Main;
        public delegate void TickCallback();

        public event TickCallback Tick = null;
        public int Interval = 100 ;
        bool Enable = false ;

        public Timer()
        {
          
        }

        void InternalRun()
        {
            Enable = true ;
            while(true)
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
        public void Start() {
            thd_Main = new Thread(InternalRun);
            thd_Main.Start();
        }
        public void Stop() {
            this.Enable = false ;
        }
    }
}
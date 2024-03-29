using System;
using System.Timers;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Threading;
using System.Text;


namespace dotNetLab
{
    public struct reg_str
    {
        public static string chn_str = " [\u4e00-\u9fa5]+";
    } ;
    class SoundAndVideo
    {
        public static int SND_ASYNC = 0x0001; // play asynchronously 
        public static int SND_FILENAME = 0x00020000; // name is file name
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);
        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }
        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            ShowCommands nShowCmd);
        [DllImport("winmm.dll")]
        static extern bool PlaySound(string pszSound, int hmod, int fdwSound);
        public static bool PlayVideo(string FileName)
        {
            IntPtr jet = ShellExecute(IntPtr.Zero, "open", FileName, null, null, ShowCommands.SW_NORMAL);
            if ((int)jet > 32)
                return true;
            else
                return false;
        }
        public static void PlayVoice(string soundPath)//例：soundPath="sound\\2.wav" 路径只满足环境目录下就OK
        {
            PlaySound(soundPath, 0, SND_ASYNC | SND_FILENAME);//播放WAV格式声音
            //  using System.Threading.Thread.Sleep(2000);//播放2000毫秒（2秒）
        }//每次调用此方法，如果播放效果对时间有严格要求，那么在添加一个int 时间参数，这样就
        public static void PlayMP3(string FileName)
        {
            StringBuilder shortpath = new StringBuilder(80);
            int result = GetShortPathName(FileName, shortpath, shortpath.Capacity);
            FileName = shortpath.ToString();
            mciSendString(@"close all", null, 0, 0);
            mciSendString(@"open " + FileName + " alias song", null, 0, 0); //打开
            mciSendString("play song", null, 0, 0); //播放
        }
        public static void RecordWav()
        {
            mciSendString(@"close movie", null, 0, 0);
            mciSendString(@"open new type WAVEAudio alias Mymovie", null, 0, 0); //打开
            mciSendString("record Mymovie", null, 0, 0);
        }
        public static void StopRecordWav(string FileName)
        {
            mciSendString(@"stop Mymovie", null, 0, 0);
            mciSendString(@"save  Mymovie " + FileName, null, 0, 0);
            mciSendString("close Mymovie", null, 0, 0);
        }
        public static void Pause()
        {
            mciSendString("pause song", null, 0, 0);
        }
        public static void Resume()
        {
            mciSendString("resume song", null, 0, 0);
        }
        public static void Stop()
        {
            mciSendString("stop song", null, 0, 0);
        }
    }
   public class SystemX
    {
        static String strVBS_CreateDeskTop =
            "Set WshShell = WScript.CreateObject(\"WScript.Shell\")\r\nstrDesktop = WshShell.SpecialFolders(\"{0}\")\r\nset oShellLink = WshShell.CreateShortcut(strDesktop & \"\\{1}.lnk\")\r\noShellLink.TargetPath = \"{2}\"\r\noShellLink.WindowStyle = 3\r\noShellLink.Hotkey = \"Ctrl+Alt+e\"\r\noShellLink.IconLocation = \"{3}, 0\"\r\noShellLink.Description = \"快捷方式\"\r\noShellLink.WorkingDirectory = \"{4}\"\r\noShellLink.Save";
        public static  String STARTMENU = "StartMenu";
        public static  String DESKTOP = "Desktop";
        public static ArrayList Folders = new ArrayList();
        public static ArrayList Files = new ArrayList();
        public enum BatteryChargeStatus : byte
        {
            High = 1,
            Low = 2,
            Critical = 4,
            Charging = 8,
            NoSystemBattery = 128,
            Unknown = 255
        }
        public enum PowerLineStatus : byte
        {
            Offline = 0,

            Online = 1,

            Unknown = 255
        }
        public struct SystemPowerStatus
        {

            public PowerLineStatus PowerLineStatus;

            public BatteryChargeStatus BatteryChargeStatus;

            public Byte BatteryLifePercent;

            public Byte Reserved;

            public int BatteryLifeRemaining;

            public int BatteryFullLifeTime;

        }
        [Flags]
        public enum ExitWindows : uint
        {
            LogOff = 0x00,      //注销 
            ShutDown = 0x01,    //关机             
            Reboot = 0x02,      //重启             
            Force = 0x04,
            PowerOff = 0x08,
            ForceIfHung = 0x10
        }
        [Flags]
        public enum ShutdownReason : uint
        {
            MajorApplication = 0x00040000, MajorHardware = 0x00010000, MajorLegacyApi = 0x00070000, MajorOperatingSystem = 0x00020000, MajorOther = 0x00000000, MajorPower = 0x00060000, MajorSoftware = 0x00030000, MajorSystem = 0x00050000, MinorBlueScreen = 0x0000000F, MinorCordUnplugged = 0x0000000b, MinorDisk = 0x00000007, MinorEnvironment = 0x0000000c, MinorHardwareDriver = 0x0000000d, MinorHotfix = 0x00000011, MinorHung = 0x00000005, MinorInstallation = 0x00000002, MinorMaintenance = 0x00000001, MinorMMC = 0x00000019,
            MinorNetworkConnectivity = 0x00000014, MinorNetworkCard = 0x00000009, MinorOther = 0x00000000, MinorOtherDriver = 0x0000000e, MinorPowerSupply = 0x0000000a, MinorProcessor = 0x00000008, MinorReconfig = 0x00000004, MinorSecurity = 0x00000013, MinorSecurityFix = 0x00000012,
            MinorSecurityFixUninstall = 0x00000018, MinorServicePack = 0x00000010,
            MinorServicePackUninstall = 0x00000016, MinorTermSrv = 0x00000020, MinorUnstable = 0x00000006, MinorUpgrade = 0x00000003, MinorWMI = 0x00000015, FlagUserDefined = 0x40000000, FlagPlanned = 0x80000000
        }
        [DllImport("kernel32", EntryPoint = "GetSystemPowerStatus")]
        private static extern void GetSystemPowerStatus(ref SystemPowerStatus powerStatus);
        [DllImport("user32.dll")]
        public static extern bool ExitWindowsEx(ExitWindows uFlags, ShutdownReason dwReason);
        public static void KillTargetProcess(string strProcessNameWithoutExt, int ElapseTime)
        {
            Process[] it = null;
            while (true)
            {
                it = Process.GetProcessesByName(strProcessNameWithoutExt);
                if (it.Length > 0)
                {
                    for (int i = 0; i < it.Length; i++)
                        it[i].Kill();
                }
                Thread.Sleep(ElapseTime);
            }
        }
        public static void LogOff()
        {
            ExitWindowsEx(ExitWindows.LogOff, ShutdownReason.FlagUserDefined);
        }
        public static void LogOff_Force()
        {
            ExitWindowsEx(ExitWindows.LogOff | ExitWindows.Force, ShutdownReason.FlagUserDefined);
        }
        public static void Reboot_Force()
        {
            ExitWindowsEx(ExitWindows.Reboot  , ShutdownReason.FlagUserDefined);
        }
        public static void Shutdown()
        {
            ExitWindowsEx(ExitWindows.ShutDown | ExitWindows.ForceIfHung, ShutdownReason.FlagUserDefined);
        }
        public static void DeepFind(string path)
        {
            string[] MyFolders = Directory.GetDirectories(path);
            if (MyFolders.Length != 0)
            {
                foreach (string i in MyFolders)
                {

                    Files.AddRange(Directory.GetFiles(i));
                    DeepFind(i);
                }
            }
            else
                Files.AddRange(Directory.GetFiles(path));
        }
        public static void GenerateAppLink(String strAppLocation)
        {
            //Environment.SpecialFolder.StartMenu
            string strFileNameNonExt = Path.GetFileNameWithoutExtension(strAppLocation);
            string AppDirectoryPath = Path.GetDirectoryName(strAppLocation);
            string strCmd = String.Format(strVBS_CreateDeskTop, DESKTOP,
                strFileNameNonExt, strAppLocation, strAppLocation, AppDirectoryPath);
            File.WriteAllText("Execute.vbs", strCmd, Encoding.Default);
            Process.Start("Execute.vbs");
        }
        public static void GenerateAppLink(String strAppLocation, String strSpecialFolder)
        {
            //Environment.SpecialFolder.StartMenu
            string strFileNameNonExt = Path.GetFileNameWithoutExtension(strAppLocation);
            string AppDirectoryPath = Path.GetDirectoryName(strAppLocation);
            string strCmd = String.Format(strVBS_CreateDeskTop, strSpecialFolder,
                strFileNameNonExt, strAppLocation, strAppLocation, AppDirectoryPath);
            File.WriteAllText("Execute.vbs", strCmd, Encoding.Default);
            Process.Start("Execute.vbs");
        }
        //被Math.Round取代
        public static double GetPreciece(double TargetNum, int Preciece)
        {
            double Result = 0;
            int Double_Int;
            double Double_Float;
            Double_Int = (int)TargetNum;
            Double_Float = TargetNum - Double_Int;
            Double_Float *= Math.Pow(10, Preciece);
            if ((Double_Float - (int)Double_Float) >= 0.5)
            {
                Double_Float += 1;
            }
            Result = Double_Int + (int)Double_Float * Math.Pow(10, -Preciece);
            return Result;
        }
        //被Math.Round取代
        public static float GetPreciece(float TargetNum, int Preciece)
        {
            float Result = 0;
            int float_Int;
            float float_Float;
            float_Int = (int)TargetNum;
            float_Float = TargetNum - float_Int;
            float_Float  = (float)(float_Float * Math.Pow(10, Preciece));
            if ((float_Float - (int)float_Float) >= 0.5)
            {
                float_Float += 1;
            }
            Result = (float)(float_Int + (int)float_Float *
                Math.Pow(10, -Preciece));
            return Result;
        }
    }
    namespace TimeX
    {
        class BackGroundTimer
        {
            public System.Timers.Timer MyTimer;
            DateTime TargetDateTime;
            TimeSpan it_TimeSpan;
            public void Init(int ElapseTime, int nElapseHour, int nElapseMin, int nElapseSec)
            {
                MyTimer = new System.Timers.Timer(ElapseTime);
                MyTimer.Elapsed += new ElapsedEventHandler(MyTimer_Elapsed);
                TargetDateTime = DateTime.Now;
                TimeSpan it_TimeSpan = new TimeSpan(nElapseHour, nElapseMin, nElapseSec);
                TargetDateTime += it_TimeSpan;

            }
            void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
            {
                if (DateTime.Now >= TargetDateTime)
                {

                }
            }
            void Boot()
            {
                MyTimer.Enabled = true;
            }
            void Disable()
            {
                MyTimer.Enabled = false;
            }
        }
        class PassedTime
        {
            int OrigionHour;
            int OrigionMin;
            int OrigionSec;
            string Report = null;
            public int _TotalSecs = 0;
            public PassedTime()
            {
                OrigionHour = DateTime.Now.Hour;
                OrigionMin = DateTime.Now.Minute;
                OrigionSec = DateTime.Now.Second;
            }
            public string GetPassedTime()
            {

                double WastTime = 0;
                int Infer = DateTime.Now.Hour - OrigionHour;
                if (Infer >= 0)
                {
                    int TotalSecs = DateTime.Now.Hour * 3600 +
                          DateTime.Now.Minute * 60 + DateTime.Now.Second
                          - OrigionHour * 3600 - OrigionMin * 60 - OrigionSec;
                    if (TotalSecs <= 60)
                        Report = string.Format("Takes {0} Secs", TotalSecs);
                    else if (TotalSecs > 60 && TotalSecs < 3600)
                    {
                        WastTime = TotalSecs;
                        WastTime = (WastTime / 60.0);
                        Report = string.Format("Takes {0} Mins", WastTime);
                    }
                    else if (TotalSecs > 3600)
                    {
                        WastTime = TotalSecs;
                        WastTime = (WastTime / 3600.0);
                        Report = string.Format("Takes {0} hours", WastTime);
                    }
                    _TotalSecs = TotalSecs;
                }
                else
                {
                    int TotalSecs = 24 * 3600 + DateTime.Now.Hour * 3600 +
                             DateTime.Now.Minute * 60 + DateTime.Now.Second
                             - OrigionHour * 3600 - OrigionMin * 60 - OrigionSec;
                    if (TotalSecs <= 60)
                        Report = string.Format("Takes {0} Secs", TotalSecs);
                    else if (TotalSecs > 60 && TotalSecs < 3600)
                    {
                        WastTime = TotalSecs;
                        WastTime = (WastTime / 60.0);
                        Report = string.Format("Takes {0} Mins", WastTime);
                    }
                    else if (TotalSecs > 3600)
                    {
                        WastTime = TotalSecs;
                        WastTime = (WastTime / 3600.0);
                        Report = string.Format("Takes {0} hours", WastTime);
                    }
                    _TotalSecs = TotalSecs;
                }
                return Report;
            }
        }
        class TimeUp
        {
            int OrigionHour;
            int OrigionMin;
            int OrigionSec;
            int TargetH;
            int TargetM;
            //  int DelayTime_s ;
            TimeUp()
            {
                OrigionHour = DateTime.Now.Hour;
                OrigionMin = DateTime.Now.Minute;
                OrigionSec = DateTime.Now.Second;

            }
            public void Start(int DelayTime_h,
            int DelayTime_m
            )
            {

                //     this.DelayTime_s = DelayTime_s ;
                int _hour = DateTime.Now.Hour + DelayTime_h;
                int IsOverMin = DateTime.Now.Minute + DelayTime_m;
                if (IsOverMin > 60)
                {
                    IsOverMin -= 60;
                    _hour += 1;
                    TargetH = _hour;
                    TargetM = IsOverMin;
                }
                else
                {
                    TargetH = _hour;
                    TargetM = IsOverMin;
                }

            }
            public bool IsTimeUp()
            {
                if (DateTime.Now.Hour >= TargetH && DateTime.Now.Minute >= TargetM)
                {
                    return true;
                }
                else
                    return false;
            }
        }
    }
    public class ExitWindows
    {
        #region win32 api
        [StructLayout(LayoutKind.Sequential, Pack = 1)]

        private struct TokPriv1Luid
        {

            public int Count;

            public long Luid;

            public int Attr;

        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
            ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool ExitWindowsEx(int flg, int rea);

        #endregion

        private const int SE_PRIVILEGE_ENABLED = 0x00000002;

        private const int TOKEN_QUERY = 0x00000008;

        private const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;

        private const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        #region Exit Windows Flags
        private const int EWX_LOGOFF = 0x00000000;

        private const int EWX_SHUTDOWN = 0x00000001;

        private const int EWX_REBOOT = 0x00000002;

        private const int EWX_FORCE = 0x00000004;

        private const int EWX_POWEROFF = 0x00000008;

        private const int EWX_FORCEIFHUNG = 0x00000010;

        #endregion

        public static void DoExitWin(int flg)
        {

            //give current process SeShutdownPrivilege
            TokPriv1Luid tp;

            IntPtr hproc = GetCurrentProcess();

            IntPtr htok = IntPtr.Zero;

            if (!OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok))
            {
                throw new Exception("Open Process Token fail");
            }

            tp.Count = 1;

            tp.Luid = 0;

            tp.Attr = SE_PRIVILEGE_ENABLED;

            if (!LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid))
            {
                throw new Exception("Lookup Privilege Value fail");
            }

            if (!AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero))
            {
                throw new Exception("Adjust Token Privileges fail");
            }

            //Exit windows
            if (!ExitWindowsEx(flg, 0))
            {
                throw new Exception("Exit Windows fail");
            }
        }

        /// <summary>
        /// Reboot computer
        /// </summary>
        /// <param name="force">force reboot</param>
        public static void Reboot(bool force)
        {
            if (force)
            {
                DoExitWin(EWX_REBOOT | EWX_FORCE);
            }
            else
            {
                DoExitWin(EWX_REBOOT | EWX_FORCEIFHUNG);
            }
        }

        /// <summary>
        /// Reboot computer force if hung
        /// </summary>
        public static void Reboot()
        {
            Reboot(false);
        }

        /// <summary>
        /// Shut down computer
        /// </summary>
        /// <param name="force">force shut down</param>
        public static void Shutdown(bool force)
        {
            if (force)
            {
                DoExitWin(EWX_SHUTDOWN | EWX_FORCE);
            }
            else
            {
                DoExitWin(EWX_SHUTDOWN | EWX_FORCEIFHUNG);
            }
        }

        /// <summary>
        /// Shut down computer force if hung
        /// </summary>
        public static void Shutdown()
        {
            Shutdown(false);
        }

        /// <summary>
        /// Log off
        /// </summary>
        /// <param name="force">force logoff</param>
        public static void Logoff(bool force)
        {
            if (force)
            {
                DoExitWin(EWX_LOGOFF | EWX_FORCE);
            }
            else
            {
                DoExitWin(EWX_LOGOFF | EWX_FORCEIFHUNG);
            }
        }

        /// <summary>
        /// logoff computer force if hung
        /// </summary>
        public static void Logoff()
        {
            Logoff(false);
        }
    }
}
/*
         class SystemInformationX
        {
            static int _14inchTabletWidth = 309;
            [DllImport("User32.dll")]
            extern static IntPtr GetDC(IntPtr e);
            [DllImport("GDI32.dll")]
            extern static int GetDeviceCaps(IntPtr e, int Mode);
            [DllImport("User32.dll")]
            extern static int ReleaseDC(IntPtr e, IntPtr f);
            public static bool isTablet()
            {
                int nScreenWidth = 0, nScreenHeight = 0;
                IntPtr hdcScreen = GetDC(IntPtr.Zero);   //获取屏幕的HDC
                nScreenWidth = GetDeviceCaps(hdcScreen, 4);
                nScreenHeight = GetDeviceCaps(hdcScreen, 6);
                ReleaseDC(IntPtr.Zero, hdcScreen);
                if (nScreenWidth < _14inchTabletWidth)
                    return false;
                else
                    return true;
            }
        }
     
     */
/*
 
       //class RealCollect
        //{
        //    public int[] IrregularNum;
        //    int[] StaffNum;
        //    int WordsNums;
        //    Random RNum = new Random();
        //    public RealCollect(int Num)
        //    {
        //        WordsNums = Num;
        //    }
        //    void ClearSame()
        //    {
        //        StaffNum = new int[WordsNums];
        //        int j = 0;
        //        for (int i = 0; i < WordsNums; i++)
        //        {
        //            if (!IrregularNum.Contains(i))
        //            {
        //                StaffNum[j] = i;
        //                j++;
        //            }

        //        }
        //        j = 0;
        //        for (int k = 0; k < WordsNums; k++)
        //        {
        //            for (int i = k + 1; i < WordsNums; i++)
        //            {
        //                if (IrregularNum[k] == IrregularNum[i])
        //                {
        //                    IrregularNum[i] = StaffNum[j];
        //                    j++;
        //                }
        //            }
        //        }
        //    }
        //    public void MakeRandom()
        //    {
        //        IrregularNum = new int[WordsNums];
        //        for (int i = 0; i < WordsNums; i++)
        //        {
        //            IrregularNum[i] = RNum.Next(0, WordsNums);
        //        }
        //        ClearSame();
        //    }
        //}
        //class BatchGet
        //{
        //    public static string GetSelected(System.Windows.Forms.Control.ControlCollection _Container)
        //    {
        //        string SelectString = "0";
        //        foreach (Control get in _Container)
        //        {
        //            RadioButton target = get as RadioButton;
        //            if (target != null && target.Checked == true)
        //            {
        //                SelectString = target.Text;
        //                break;
        //            }
        //        }
        //        return SelectString;
        //    }
        //    public static void GetTextBox(System.Windows.Forms.Control.ControlCollection _Container, TextBox[] TargetTextBox, ref int num)
        //    {
        //        int i = 0;
        //        foreach (Control get in _Container)
        //        {
        //            TextBox target = get as TextBox;
        //            if (target != null)
        //            {
        //                TargetTextBox[i] = target;
        //                i++;
        //            }
        //        }
        //        num = i;
        //    }
        //}
 */
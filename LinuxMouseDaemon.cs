using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Security.Principal;

namespace WinPrimarySelect
{
    public class LinuxMouseDaemon : ApplicationContext
    {
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_MBUTTONDOWN = 0x0207;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private LowLevelMouseProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private NotifyIcon _trayIcon;

        [STAThread]
        public static void Main()
        {
            if (!IsAdministrator())
            {
                MessageBox.Show("Tips: Kör som administratör för att programmet ska fungera i alla fönster (terminaler, etc).", "WinPrimarySelect");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LinuxMouseDaemon());
        }

        public LinuxMouseDaemon()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);

            _trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Application,
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true,
                Text = "WinPrimarySelect (Linux Mouse)"
            };

            _trayIcon.ContextMenuStrip.Items.Add("Exit", null, (s, e) => {
                _trayIcon.Visible = false;
                UnhookWindowsHookEx(_hookID);
                Application.Exit();
            });
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule!)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName!), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_LBUTTONUP)
                {
                    // Markering klar -> Kopiera
                    Thread.Sleep(100); 
                    SendKeys.SendWait("^c");
                }
                else if (wParam == (IntPtr)WM_MBUTTONDOWN)
                {
                    // 1. Simulera ett vänsterklick för att flytta fokus till fönstret under musen
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    
                    // 2. Ge Windows en kort stund att fatta att fokus har flyttats
                    Thread.Sleep(50);
                    
                    // 3. Klistra in
                    SendKeys.SendWait("^v");
                    
                    // 4. Svälj mittenklicket så vi inte råkar stänga flikar
                    return (IntPtr)1; 
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        // --- Windows API Imports ---
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelMouseProc lpfn, IntPtr hMod, uint idT);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wp, IntPtr lp);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
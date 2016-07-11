using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace DrawingClient
{
    static class Program
    {
        static internal CultureInfo ci;
        static internal CultureInfo ciSiteNumeric;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ci = new CultureInfo(DrawingClient.Properties.Settings.Default.CustCulture);    // Used for all strings
            ciSiteNumeric = new CultureInfo(DrawingClient.Properties.Settings.Default.SiteNumericCulture);  // Using different culture for all amount value

            // Sets current thred's currency symbol to whatever set in the config file
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = DrawingClient.Properties.Settings.Default.CurrencySymbol;

            frmLogin loginForm = new frmLogin();
            loginForm.ShowDialog();

            if (Common.Instance.UserIDText != null)
                Application.Run(new frmMain());
        }
    }
}
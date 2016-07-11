using System;
using System.Windows.Forms;
using System.IO;

namespace DrawingClient
{
    public class Error
    {
        public static void Log(Exception error, bool showError)
        {
            try
            {
                string logFile = Application.StartupPath + @"\Error.log";

                using (StreamWriter sw = new StreamWriter(logFile, true))
                {
                    sw.WriteLine("<--------------------------------------->");
                    sw.WriteLine(String.Format("Date/Time:{0}", DateTime.Now));
                    sw.WriteLine("Message:");
                    sw.WriteLine(error.Message);
                    sw.WriteLine("Help Link:");
                    sw.WriteLine(error.HelpLink);
                    sw.WriteLine("Source:");
                    sw.WriteLine(error.Source);
                    sw.WriteLine("Stack Trace:");
                    sw.WriteLine(error.StackTrace);
                    sw.WriteLine("Target Site:");
                    sw.WriteLine(error.TargetSite);
                    sw.WriteLine("<--------------------------------------->");
                    sw.WriteLine();
                    sw.WriteLine();
                }

                if (showError)
                    MessageBox.Show(error.Message, TextRes.Get("Error", Program.ci), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
    }
}

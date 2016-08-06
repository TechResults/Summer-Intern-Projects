using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

namespace BFMCMPrintService2
{
	public class BFMCMPrintService2 : System.ServiceProcess.ServiceBase
	{
		private System.Timers.Timer m_PrintTimer;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BFMCMPrintService2()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new BFMCMPrintService2() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_PrintTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.m_PrintTimer)).BeginInit();
            // 
            // m_PrintTimer
            // 
            this.m_PrintTimer.Interval = 10000D;
            this.m_PrintTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.m_PrintTimer_Elapsed);
            // 
            // BFMCMPrintService
            // 
            this.CanShutdown = true;
            this.ServiceName = "BFMCMPrintService2";
            ((System.ComponentModel.ISupportInitialize)(this.m_PrintTimer)).EndInit();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			string timeint = TRUtilities.TRUtilities.Instance.getGlobalProfileString("COMMON", "TimerInterval");

			TRUtilities.TRUtilities.Instance.appLogHR();
			TRUtilities.TRUtilities.Instance.appLog("Service Startup...");

			if(timeint != null)
			{
				int millis = Int32.Parse(timeint.Trim());
				millis *= 1000;
				this.m_PrintTimer.Interval = millis;

				TRUtilities.TRUtilities.Instance.appLog("New Timer Interval is " + this.m_PrintTimer.Interval / 1000 + " Seconds");
			}

			this.m_PrintTimer.Enabled = true;
			this.m_PrintTimer.Start();

		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			//Write shutdown info to the log file.
			this.m_PrintTimer.Enabled = false;
			this.m_PrintTimer.Stop();

			base.OnStop();

			TRUtilities.TRUtilities.Instance.appLog("Service Stopped...");
			TRUtilities.TRUtilities.Instance.appLogHR();
		}

		protected override void OnShutdown()
		{
			this.m_PrintTimer.Enabled = false;
			this.m_PrintTimer.Stop();

			base.OnShutdown ();

			TRUtilities.TRUtilities.Instance.appLog("Service Shutdown...");
			TRUtilities.TRUtilities.Instance.appLogHR();
		}
		
		private void m_PrintTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			PrintServiceProcess.PrintVouchers();
		}
	}
}

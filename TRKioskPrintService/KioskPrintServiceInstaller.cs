using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace BFMCMPrintService2
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class KioskPrintServiceInstaller : System.Configuration.Install.Installer
	{
		private System.ServiceProcess.ServiceInstaller BFMCMPrintService2Installer;
		private System.ServiceProcess.ServiceProcessInstaller BFMCMPrintProcessInstaller;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public KioskPrintServiceInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.BFMCMPrintProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BFMCMPrintService2Installer = new System.ServiceProcess.ServiceInstaller();
            // 
            // BFMCMPrintProcessInstaller
            // 
            this.BFMCMPrintProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BFMCMPrintProcessInstaller.Password = null;
            this.BFMCMPrintProcessInstaller.Username = null;
            this.BFMCMPrintProcessInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.TRKioskPrintProcessInstaller_AfterInstall);
            // 
            // BFMCMPrintService2Installer
            // 
            this.BFMCMPrintService2Installer.DisplayName = "BlueFin MCM Print Service2";
            this.BFMCMPrintService2Installer.ServiceName = "BFMCMPrintService2";
            this.BFMCMPrintService2Installer.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // KioskPrintServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BFMCMPrintProcessInstaller,
            this.BFMCMPrintService2Installer});

		}
		#endregion

        private void TRKioskPrintProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }
	}
}

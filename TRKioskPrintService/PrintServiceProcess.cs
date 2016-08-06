using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using TRUtilities;
using TRUtilities.DBConnector;
using CommonObjects;

namespace BFMCMPrintService2
{
	/// <summary>
	/// Summary description for PrintServiceProcess.
	/// </summary>
	public class PrintServiceProcess
	{
		private static Object m_ThreadLock;


		static PrintServiceProcess()
		{
			m_ThreadLock = new Object();
		}
		private PrintServiceProcess()
		{
			
		}

		public static void PrintVouchers()
		{
			lock(m_ThreadLock)
			{

				try
				{
					ArrayList alist = TRPrintJob.LoadJobs();

					foreach(TRPrintJob printjob in alist)
					{
						//printjob.show();
						printjob.Print();
						printjob.Save();
					}
				}
				catch(Exception e)
				{
					TRUtilities.TRUtilities.Instance.appLogError(e);
				}
			}
		}

		[STAThread]
		static void Main(string[] args)
		{
			PrintVouchers();
		}
	}
}

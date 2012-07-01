using System;
using System.IO;
using System.Windows.Forms;

namespace MapCopy
{
	internal class Log
	{
		private FileInfo logFile;

		public Log(string directory, string filename)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directory);
			bool exists = directoryInfo.Exists;
			if (!exists)
			{
				directoryInfo.Create();
			}
			this.logFile = new FileInfo(string.Concat(directory, filename));
			exists = this.logFile.Exists;
			if (exists)
			{
				this.clearLog();
			}
			else
			{
				this.logFile.Create();
			}
		}

		public void addLogRecord(string writeLine)
		{
			try
			{
				StreamWriter streamWriter = this.logFile.AppendText();
				DateTime now = DateTime.Now;
				streamWriter.Write(now.ToShortTimeString());
				streamWriter.Write(string.Concat(": ", writeLine, "\n"));
				streamWriter.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Error trying to write to the log file: ", exception.Message), "Log file error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
			}
		}

		public void clearLog()
		{
			this.logFile.Delete();
			this.logFile.Create();
		}
	}
}
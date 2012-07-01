using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace CleanMapCopy
{
	public class Install
	{
		public ArrayList al_data;

		public string installDirectRtDirectory;

		public string installMediaSource;

		public string mapXData;

		public string mmUSAData;

		public string rjsData;

		public string zipNinePath;

		public Install(string installMediaSource, string installDir, string mapXData, string mmUSAData, string zipNinePath, string rjsData)
		{
			bool flag = false;
			bool flag1 = flag;
			bool flag2 = flag;
			bool flag3 = !(mapXData != "");
			if (!flag3)
			{
				flag2 = true;
			}
			flag3 = !(rjsData != "");
			if (!flag3)
			{
				flag1 = true;
			}
			this.al_data = new ArrayList();
			this.installDirectRtDirectory = installDir;
			this.installMediaSource = installMediaSource;
			this.mapXData = mapXData;
			this.mmUSAData = mmUSAData;
			this.zipNinePath = zipNinePath;
			this.rjsData = rjsData;
			this.addStateData(this.installMediaSource, flag2, flag1);
		}

		private void addStateData(string dataFolder, bool mapx, bool rjs)
		{
			string str;
			try
			{
				bool flag = dataFolder[dataFolder.Length - 1] == '\\';
				if (flag)
				{
					str = string.Concat(dataFolder, "DATA.TXT");
				}
				else
				{
					str = string.Concat(dataFolder, "\\DATA.TXT");
				}
				FileInfo fileInfo = new FileInfo(str);
				StreamReader streamReader = new StreamReader(fileInfo.FullName);
				try
				{
					while (true)
					{
						string str1 = streamReader.ReadLine();
						string str2 = str1;
						flag = str1 != null;
						if (!flag)
						{
							break;
						}
						flag = this.al_data.Contains(str2);
						if (!flag)
						{
							this.al_data.Add(str2);
						}
					}
				}
				finally
				{
					flag = streamReader == null;
					if (!flag)
					{
						streamReader.Dispose();
					}
				}
				flag = !mapx;
				if (!flag)
				{
					this.al_data.Add("Bonus");
				}
				flag = !rjs;
				if (!flag)
				{
					this.al_data.Add("Region");
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Exception, error filling data arrayList: ", exception.Message), "DATA.TXT file exception.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public void copyData(string sourceZipPath, string ExtractPath)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(ExtractPath);
				bool exists = directoryInfo.Exists;
				if (!exists)
				{
					directoryInfo.Create();
				}
				FastZip fastZip = new FastZip();
				fastZip.ExtractZip(sourceZipPath, ExtractPath, "");
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Exception while copying data: ", exception.Message), "Data copy exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}
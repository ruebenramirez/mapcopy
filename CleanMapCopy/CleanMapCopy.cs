using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CleanMapCopy
{
	public class CleanMapCopy : Form
	{
		protected ArrayList al_canada;

		protected ArrayList al_central;

		protected ArrayList al_midwest;

		protected ArrayList al_northeast;

		protected ArrayList al_pacific;

		protected ArrayList al_regionsZips;

		protected ArrayList al_south;

		private ArrayList al_statesList;

		protected int bonusIndex;

		private IContainer components;

		protected const bool DEBUG = false;

		protected string directRtInstallPath;

		private FolderBrowserDialog fb_selectPath;

		private FontDialog fontDialog1;

		private Label lbl_curState;

		private Label lbl_percentage;

		private bool mapxCanadaData;

		protected string mapXPath;

		private bool mapxUSAData;

		protected string mediaSource;

		protected string mmusaPath;

		private Install myInstall;

		private ProgressBar pb_copyData;

		protected int regionIndex;

		protected string rjsDataPath;

		protected string rjsInstallPath;

		protected string seamlessBuilderInstallPath;

		protected string zipNinePath;

		public CleanMapCopy()
		{
			this.components = null;
			this.InitializeComponent();
			this.pullDataPaths();
			this.setRJSData();
			bool flag = !this.sourceValidate();
			if (!flag)
			{
				this.setupGo();
			}
		}

		public void copyDataMgr()
		{
			DirectoryInfo directoryInfo;
			FileInfo fileInfo;
			FileInfo fileInfo1;
			bool count;
			object[] objArray;
			bool flag;
			bool flag1;
			bool flag2;
			bool flag3;
			bool flag4;
			try
			{
				DateTime now = DateTime.Now;
				ArrayList arrayLists = new ArrayList();
				int num = 0;
				while (true)
				{
					count = num < this.myInstall.al_data.Count;
					if (!count)
					{
						break;
					}
					arrayLists.Add((bool)0);
					num++;
				}
				this.bonusIndex = this.myInstall.al_data.IndexOf("Bonus");
				this.regionIndex = this.myInstall.al_data.IndexOf("Region");
				num = 0;
				while (true)
				{
					count = num < this.myInstall.al_data.Count;
					if (!count)
					{
						break;
					}
					if (this.mapXPath == "")
					{
						flag = true;
					}
					else
					{
						flag = !arrayLists[this.bonusIndex].Equals((bool)0);
					}
					count = flag;
					if (!count)
					{
						directoryInfo = new DirectoryInfo(string.Concat(this.mediaSource, "\\", this.myInstall.al_data[this.bonusIndex]));
						count = !directoryInfo.Exists;
						if (!count)
						{
							fileInfo = new FileInfo(string.Concat(directoryInfo.FullName.ToString(), "\\", this.myInstall.al_data[this.bonusIndex].ToString(), "SP.ZIP"));
							count = !fileInfo.Exists;
							if (!count)
							{
								objArray = new object[1];
								objArray[0] = string.Concat(this.myInstall.al_data[this.bonusIndex].ToString(), " Map X");
								base.Invoke(updateStateExtracting, objArray);
								this.myInstall.copyData(fileInfo.FullName.ToString(), string.Concat(this.mapXPath, "\\", this.myInstall.al_data[this.bonusIndex]));
							}
							arrayLists[this.bonusIndex] = (bool)1;
						}
					}
					if (this.rjsDataPath == "")
					{
						flag1 = true;
					}
					else
					{
						flag1 = !arrayLists[this.regionIndex].Equals((bool)0);
					}
					count = flag1;
					if (!count)
					{
						directoryInfo = new DirectoryInfo(string.Concat(this.mediaSource, "\\", this.myInstall.al_data[this.regionIndex]));
						count = !directoryInfo.Exists;
						if (!count)
						{
							int num1 = 0;
							while (true)
							{
								count = num1 < this.al_regionsZips.Count;
								if (!count)
								{
									break;
								}
								FileInfo fileInfo2 = new FileInfo(string.Concat(directoryInfo.FullName.ToString(), "\\", this.al_regionsZips[num1].ToString(), ".ZIP"));
								count = !fileInfo2.Exists;
								if (!count)
								{
									this.myInstall.copyData(fileInfo2.FullName, string.Concat(this.rjsDataPath, "\\", this.al_regionsZips[num1].ToString()));
								}
								num1++;
							}
							arrayLists[this.regionIndex] = (bool)1;
						}
					}
					while (true)
					{
						count = arrayLists[num].Equals((bool)0);
						if (!count)
						{
							break;
						}
						directoryInfo = new DirectoryInfo(string.Concat(this.mediaSource, "\\", this.myInstall.al_data[num]));
						count = !directoryInfo.Exists;
						if (count)
						{
							arrayLists[num] = (bool)0;
							string[] str = new string[5];
							str[0] = "Dir not found: ";
							str[1] = directoryInfo.FullName.ToString();
							str[2] = "Please put the next data disc in the same disc drive. (disc with ";
							str[3] = this.myInstall.al_data[num].ToString().ToUpper();
							str[4] = " data.";
							DialogResult dialogResult = MessageBox.Show(string.Concat(str), "Next data disc needed.", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
							count = dialogResult != DialogResult.Cancel;
							if (!count)
							{
								DialogResult dialogResult1 = MessageBox.Show("Exit the installer? click 'No' to browse for another directory.", "Confirm cancel install", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
								DialogResult dialogResult2 = dialogResult1;
								switch (dialogResult2)
								{
									case DialogResult.Yes:
									{
										Application.Exit();
									}
									case DialogResult.No:
									{
										base.Invoke(sourceDirUpdate);
									}
								}
							}
						}
						else
						{
							count = !(this.mapXPath != "");
							if (!count)
							{
								fileInfo = new FileInfo(string.Concat(directoryInfo.FullName.ToString(), "\\", this.myInstall.al_data[num].ToString(), "SP.ZIP"));
								count = !fileInfo.Exists;
								if (!count)
								{
									objArray = new object[1];
									objArray[0] = string.Concat(this.myInstall.al_data[num].ToString(), " Map X");
									base.Invoke(updateStateExtracting, objArray);
									this.myInstall.copyData(fileInfo.FullName.ToString(), string.Concat(this.mapXPath, "\\", this.myInstall.al_data[num]));
									count = !this.al_canada.Contains(this.myInstall.al_data[num].ToString().ToUpper());
									if (count)
									{
										if (this.al_central.Contains(this.myInstall.al_data[num].ToString().ToUpper()) || this.al_midwest.Contains(this.myInstall.al_data[num].ToString().ToUpper()) || this.al_northeast.Contains(this.myInstall.al_data[num].ToString().ToUpper()) || this.al_pacific.Contains(this.myInstall.al_data[num].ToString().ToUpper()))
										{
											flag2 = false;
										}
										else
										{
											flag2 = !this.al_south.Contains(this.myInstall.al_data[num].ToString().ToUpper());
										}
										count = flag2;
										if (!count)
										{
											count = this.mapxUSAData;
											if (!count)
											{
												this.mapxUSAData = true;
											}
										}
									}
									else
									{
										count = this.mapxCanadaData;
										if (!count)
										{
											this.mapxCanadaData = true;
										}
									}
								}
							}
							count = !(this.mmusaPath != "");
							if (!count)
							{
								FileInfo fileInfo3 = new FileInfo(string.Concat(directoryInfo.FullName.ToString(), "\\", this.myInstall.al_data[num].ToString(), "MM.ZIP"));
								count = !fileInfo3.Exists;
								if (!count)
								{
									objArray = new object[1];
									objArray[0] = string.Concat(this.myInstall.al_data[num].ToString(), " Map Marker USA");
									base.Invoke(updateStateExtracting, objArray);
									this.myInstall.copyData(fileInfo3.FullName.ToString(), this.mmusaPath);
								}
							}
							count = !(this.rjsDataPath != "");
							if (!count)
							{
								FileInfo fileInfo4 = new FileInfo(string.Concat(directoryInfo.FullName.ToString(), "\\", this.myInstall.al_data[num].ToString(), "RJS.ZIP"));
								count = !fileInfo4.Exists;
								if (!count)
								{
									objArray = new object[1];
									objArray[0] = string.Concat(this.myInstall.al_data[num].ToString(), " RJS");
									base.Invoke(updateStateExtracting, objArray);
									count = !this.al_central.Contains(directoryInfo.Name);
									if (!count)
									{
										this.myInstall.copyData(fileInfo4.FullName.ToString(), string.Concat(this.rjsDataPath, "\\CENTRAL\\"));
									}
									count = !this.al_midwest.Contains(directoryInfo.Name);
									if (!count)
									{
										this.myInstall.copyData(fileInfo4.FullName.ToString(), string.Concat(this.rjsDataPath, "\\MIDWEST\\"));
									}
									count = !this.al_northeast.Contains(directoryInfo.Name);
									if (!count)
									{
										this.myInstall.copyData(fileInfo4.FullName.ToString(), string.Concat(this.rjsDataPath, "\\NORTHEAST\\"));
									}
									count = !this.al_pacific.Contains(directoryInfo.Name);
									if (!count)
									{
										this.myInstall.copyData(fileInfo4.FullName.ToString(), string.Concat(this.rjsDataPath, "\\PACIFIC\\"));
									}
									count = !this.al_south.Contains(directoryInfo.Name);
									if (!count)
									{
										this.myInstall.copyData(fileInfo4.FullName.ToString(), string.Concat(this.rjsDataPath, "\\SOUTH\\"));
									}
									count = !this.al_canada.Contains(directoryInfo.Name);
									if (!count)
									{
										this.myInstall.copyData(fileInfo4.FullName.ToString(), string.Concat(this.rjsDataPath, "\\CANADA\\"));
									}
								}
							}
							count = !(this.zipNinePath != "");
							if (!count)
							{
								FileInfo fileInfo5 = new FileInfo(string.Concat(directoryInfo.FullName.ToString(), "\\", this.myInstall.al_data[num].ToString(), "Z9.ZIP"));
								count = !fileInfo5.Exists;
								if (!count)
								{
									objArray = new object[1];
									objArray[0] = string.Concat(this.myInstall.al_data[num].ToString(), " Zip 9");
									base.Invoke(updateStateExtracting, objArray);
									this.myInstall.copyData(fileInfo5.FullName.ToString(), this.zipNinePath);
								}
							}
							arrayLists[num] = (bool)1;
						}
					}
					base.Invoke(paintProgress);
					num++;
				}
				count = !(this.rjsDataPath != "");
				if (!count)
				{
					this.writeDSDs();
					RJSConfigFile.configureRJS(this.rjsInstallPath, this.rjsDataPath);
				}
				count = !(this.mapXPath != "");
				if (!count)
				{
					if (!this.mapxCanadaData)
					{
						flag3 = true;
					}
					else
					{
						flag3 = !this.mapxUSAData;
					}
					count = flag3;
					if (count)
					{
						if (!this.mapxCanadaData)
						{
							flag4 = true;
						}
						else
						{
							flag4 = this.mapxUSAData;
						}
						count = flag4;
						if (!count)
						{
							fileInfo1 = new FileInfo(string.Concat(this.mapXPath, "\\USA\\usa.gst"));
							MapXSeamlessGst.deleteUSAgst(fileInfo1);
							MapXSeamlessGst.writeCanGST(fileInfo1);
						}
					}
					else
					{
						fileInfo1 = new FileInfo(string.Concat(this.mapXPath, "\\USA\\usa.gst"));
						MapXSeamlessGst.deleteUSAgst(fileInfo1);
						MapXSeamlessGst.writeCanadianUSAgst(fileInfo1);
					}
					this.setupSeamlessBuilder();
				}
				Environment.Exit(0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Exception: ", exception.Message.ToString(), "\nYou may need to run MapCopy again from the start menu"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Environment.Exit(0);
			}
		}

		protected override void Dispose(bool disposing)
		{
			bool flag;
			if (!disposing)
			{
				flag = true;
			}
			else
			{
				flag = this.components == null;
			}
			bool flag1 = flag;
			if (!flag1)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
		private static extern int GetShortPathName(string LongPath, StringBuilder ShortPath, int BufferSize);

		private void InitializeComponent()
		{
			this.lbl_curState = new Label();
			this.lbl_percentage = new Label();
			this.pb_copyData = new ProgressBar();
			this.fb_selectPath = new FolderBrowserDialog();
			this.fontDialog1 = new FontDialog();
			base.SuspendLayout();
			this.lbl_curState.AutoSize = true;
			this.lbl_curState.Location = new Point(18, 71);
			this.lbl_curState.Name = "lbl_curState";
			this.lbl_curState.Size = new Size(0, 13);
			this.lbl_curState.TabIndex = 45;
			this.lbl_percentage.AutoSize = true;
			this.lbl_percentage.Location = new Point(15, 21);
			this.lbl_percentage.Name = "lbl_percentage";
			this.lbl_percentage.Size = new Size(0, 13);
			this.lbl_percentage.TabIndex = 44;
			this.pb_copyData.Location = new Point(12, 41);
			this.pb_copyData.Name = "pb_copyData";
			this.pb_copyData.Size = new Size(0x17f, 23);
			this.pb_copyData.Step = 1;
			this.pb_copyData.TabIndex = 43;
			this.pb_copyData.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(0x197, 98);
			base.ControlBox = false;
			base.Controls.Add(this.lbl_curState);
			base.Controls.Add(this.lbl_percentage);
			base.Controls.Add(this.pb_copyData);
			this.MaximumSize = new Size(0x19f, 127);
			this.MinimumSize = new Size(0x19f, 127);
			base.Name = "CleanMapCopy";
			this.Text = "Copying Mapping Data";
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void paintProgress()
		{
			ProgressBar pbCopyData = this.pb_copyData;
			pbCopyData.Value = pbCopyData.Value + 1;
			this.lbl_percentage.Text = string.Concat(Convert.ToString(Convert.ToInt16(this.pb_copyData.Value) * 100 / this.al_statesList.Count), "%");
		}

		protected void pullDataPaths()
		{
			this.mediaSource = string.Concat(Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "MediaSource", "")), "\\Data");
			this.mapXPath = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "mapXPath", ""));
			this.mmusaPath = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "mmusaPath", ""));
			this.zipNinePath = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "zipNinePath", ""));
			this.rjsInstallPath = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "rjsInstPath", ""));
			this.rjsDataPath = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "rjsDataPath", ""));
			this.directRtInstallPath = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "directRtInstallPath", ""));
			this.seamlessBuilderInstallPath = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "smbPath", ""));
		}

		protected void setRJSData()
		{
			object[] objArray = new object[13];
			objArray[0] = "AZ";
			objArray[1] = "CO";
			objArray[2] = "ID";
			objArray[3] = "KS";
			objArray[4] = "MT";
			objArray[5] = "ND";
			objArray[6] = "NE";
			objArray[7] = "NM";
			objArray[8] = "OK";
			objArray[9] = "SD";
			objArray[10] = "TX";
			objArray[11] = "UT";
			objArray[12] = "WY";
			this.al_central = new ArrayList(objArray);
			objArray = new object[9];
			objArray[0] = "IA";
			objArray[1] = "IL";
			objArray[2] = "IN";
			objArray[3] = "KY";
			objArray[4] = "MI";
			objArray[5] = "MN";
			objArray[6] = "MO";
			objArray[7] = "OH";
			objArray[8] = "WI";
			this.al_midwest = new ArrayList(objArray);
			objArray = new object[14];
			objArray[0] = "CT";
			objArray[1] = "DC";
			objArray[2] = "DE";
			objArray[3] = "MA";
			objArray[4] = "MD";
			objArray[5] = "ME";
			objArray[6] = "NH";
			objArray[7] = "NJ";
			objArray[8] = "NY";
			objArray[9] = "PA";
			objArray[10] = "RI";
			objArray[11] = "VA";
			objArray[12] = "VT";
			objArray[13] = "WV";
			this.al_northeast = new ArrayList(objArray);
			objArray = new object[6];
			objArray[0] = "AK";
			objArray[1] = "CA";
			objArray[2] = "HI";
			objArray[3] = "NV";
			objArray[4] = "OR";
			objArray[5] = "WA";
			this.al_pacific = new ArrayList(objArray);
			objArray = new object[10];
			objArray[0] = "AL";
			objArray[1] = "AR";
			objArray[2] = "FL";
			objArray[3] = "GA";
			objArray[4] = "LA";
			objArray[5] = "MS";
			objArray[6] = "NC";
			objArray[7] = "PR";
			objArray[8] = "SC";
			objArray[9] = "TN";
			this.al_south = new ArrayList(objArray);
			objArray = new object[13];
			objArray[0] = "AB";
			objArray[1] = "BC";
			objArray[2] = "MB";
			objArray[3] = "NB";
			objArray[4] = "NL";
			objArray[5] = "NS";
			objArray[6] = "NT";
			objArray[7] = "NU";
			objArray[8] = "ON";
			objArray[9] = "PE";
			objArray[10] = "QC";
			objArray[11] = "SK";
			objArray[12] = "YT";
			this.al_canada = new ArrayList(objArray);
			objArray = new object[6];
			objArray[0] = "CENTRAL";
			objArray[1] = "MIDWEST";
			objArray[2] = "NORTHEAST";
			objArray[3] = "PACIFIC";
			objArray[4] = "SOUTH";
			objArray[5] = "CANADA";
			this.al_regionsZips = new ArrayList(objArray);
		}

		protected void setupGo()
		{
			this.lbl_percentage.Text = "0%";
			this.pb_copyData.Visible = true;
			this.myInstall = new Install(this.mediaSource, this.directRtInstallPath, this.mapXPath, this.mmusaPath, this.zipNinePath, this.rjsDataPath);
			this.al_statesList = this.myInstall.al_data;
			this.pb_copyData.Maximum = this.al_statesList.Count;
			Thread thread = new Thread(copyDataMgr);
			thread.IsBackground = true;
			thread.Start();
		}

		protected void setupSeamlessBuilder()
		{
			StringBuilder stringBuilder = new StringBuilder(0xff);
			CleanMapCopy.GetShortPathName(this.mapXPath, stringBuilder, stringBuilder.Capacity);
			Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Appian\\InstallTemp", "smbMapsPath", stringBuilder.ToString());
		}

		protected void sourceDirUpdate()
		{
			this.fb_selectPath.RootFolder = Environment.SpecialFolder.MyComputer;
			this.fb_selectPath.ShowNewFolderButton = false;
			this.fb_selectPath.ShowDialog();
			this.mediaSource = this.fb_selectPath.SelectedPath;
		}

		protected bool sourceValidate()
		{
			string str;
			bool flag;
			bool exists = this.mediaSource[this.mediaSource.Length - 1] == '\\';
			if (exists)
			{
				str = "DATA.TXT";
			}
			else
			{
				str = "\\DATA.TXT";
			}
			FileInfo fileInfo = new FileInfo(string.Concat(this.mediaSource, str));
			exists = fileInfo.Exists;
			if (!exists)
			{
				DialogResult dialogResult = MessageBox.Show(string.Concat("Path: ", fileInfo.FullName.ToString(), "\n\r\n\rdoes not have a DATA.TXT file. Would you like to browse to another data directory? \n\r\n\rClick 'No' to to retry or 'Cancel' to quit installation."), "Data folder selection", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Hand);
				exists = dialogResult != DialogResult.Yes;
				if (!exists)
				{
					FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
					folderBrowserDialog.ShowNewFolderButton = false;
					folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
					DialogResult dialogResult1 = folderBrowserDialog.ShowDialog();
					exists = dialogResult1 != DialogResult.OK;
					if (exists)
					{
						exists = dialogResult1 != DialogResult.Cancel;
						if (!exists)
						{
							DialogResult dialogResult2 = MessageBox.Show("Are you sure you want to quit the Map Copy Process?", "Exit Map Copy?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							exists = dialogResult2 != DialogResult.Yes;
							if (!exists)
							{
								Environment.Exit(0);
							}
							exists = dialogResult2 != DialogResult.No;
							if (exists)
							{
								goto Label1;
							}
							flag = this.sourceValidate();
							return flag;
						}
					}
					else
					{
						this.mediaSource = folderBrowserDialog.SelectedPath;
						flag = true;
						return flag;
					}
				}
			Label1:
				exists = dialogResult != DialogResult.No;
				if (!exists)
				{
					flag = this.sourceValidate();
					return flag;
				}
				exists = dialogResult != DialogResult.Cancel;
				if (!exists)
				{
					Environment.Exit(0);
				}
			}
			flag = true;
			return flag;
		}

		protected void updateStateExtracting(string state)
		{
			this.lbl_curState.Text = string.Concat("Extracting ", state, " data");
		}

		protected void writeDSDs()
		{
			this.writeRegionDSD("CENTRAL", this.al_central);
			this.writeRegionDSD("MIDWEST", this.al_midwest);
			this.writeRegionDSD("NORTHEAST", this.al_northeast);
			this.writeRegionDSD("PACIFIC", this.al_pacific);
			this.writeRegionDSD("SOUTH", this.al_south);
			this.writeRegionDSD("CANADA", this.al_canada);
		}

		protected void writeRegionDSD(string region, ArrayList al_region)
		{
			bool exists;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(this.rjsDataPath, "\\", region));
				exists = !directoryInfo.Exists;
				if (!exists)
				{
					FileInfo fileInfo = new FileInfo(string.Concat(directoryInfo.FullName, "\\DatasetDescriptor.xml"));
					StreamWriter streamWriter = new StreamWriter(fileInfo.FullName);
					try
					{
						streamWriter.WriteLine("<DatasetDefinition>");
						streamWriter.WriteLine(string.Concat("<Name>", region, "</Name>"));
						streamWriter.WriteLine("<Version>GDT January 2003</Version>");
						streamWriter.WriteLine("<Description>");
						streamWriter.Write("<ShortDescription>");
						int num = 0;
						while (true)
						{
							exists = num < al_region.Count;
							if (!exists)
							{
								break;
							}
							exists = !this.al_statesList.Contains(al_region[num].ToString().ToUpper());
							if (!exists)
							{
								streamWriter.Write(al_region[num].ToString());
								exists = num + 1 == al_region.Count;
								if (!exists)
								{
									streamWriter.Write(",");
								}
							}
							num++;
						}
						streamWriter.WriteLine("</ShortDescription>");
						streamWriter.WriteLine("</Description>");
						streamWriter.WriteLine("</DatasetDefinition>");
					}
					finally
					{
						exists = streamWriter == null;
						if (!exists)
						{
							streamWriter.Dispose();
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				DialogResult dialogResult = MessageBox.Show(string.Concat("Error writing ", region, " DSD: ", exception.Message), "Error writing RJS File", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
				exists = dialogResult != DialogResult.Retry;
				if (!exists)
				{
					this.writeRegionDSD(region, al_region);
				}
			}
		}
	}
}
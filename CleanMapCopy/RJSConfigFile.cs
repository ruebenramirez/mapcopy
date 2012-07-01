using System;
using System.IO;
using System.Windows.Forms;

namespace CleanMapCopy
{
	public static class RJSConfigFile
	{
		public static void configureRJS(string rjsInstallPath, string rjsDataPath)
		{
			bool exists;
			DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(rjsInstallPath, "\\RoutingJServer-3.3\\Tomcat-Routing\\webapps\\routing33\\WEB-INF\\"));
			if (!(rjsInstallPath != "") || !(rjsDataPath != ""))
			{
				exists = true;
			}
			else
			{
				exists = !directoryInfo.Exists;
			}
			bool flag = exists;
			if (flag)
			{
				flag = !(rjsInstallPath == "");
				if (!flag)
				{
					MessageBox.Show("Routing J Server install Path not set", "RJS Path not found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				flag = !(rjsDataPath == "");
				if (!flag)
				{
					MessageBox.Show("Routing J server Data path not set", "RJS Path not found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				flag = directoryInfo.Exists;
				if (!flag)
				{
					MessageBox.Show("Routing J Server configuration directory not found.", "RJS Path not found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				MessageBox.Show("Routing J server web.xml file might require manual configuration", "RJS not configured", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				FileInfo fileInfo = new FileInfo(string.Concat(rjsInstallPath, "\\RoutingJServer-3.3\\Tomcat-Routing\\webapps\\routing33\\WEB-INF\\web.xml"));
				DirectoryInfo directoryInfo1 = new DirectoryInfo(rjsDataPath);
				RJSConfigFile.writeWebConfig(fileInfo, directoryInfo1, rjsInstallPath);
			}
		}

		public static bool pathExists(string dir)
		{
			bool flag;
			DirectoryInfo directoryInfo = new DirectoryInfo(dir);
			bool exists = !directoryInfo.Exists;
			if (exists)
			{
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		public static void writePath(DirectoryInfo dataDir, StreamWriter sw, string region)
		{
			string str = string.Concat(dataDir.FullName, "\\", region);
			bool flag = !RJSConfigFile.pathExists(str);
			if (!flag)
			{
				sw.Write(string.Concat(str, ";"));
			}
		}

		public static void writeWebConfig(FileInfo configFile, DirectoryInfo dataDir, string instdir)
		{
			StreamWriter streamWriter = new StreamWriter(configFile.FullName);
			try
			{
				streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
				streamWriter.WriteLine("<!DOCTYPE web-app PUBLIC \"-//Sun Microsystems, Inc.//DTD Web Application 2.3//EN\" \"http://java.sun.com/dtd/web-app_2_3.dtd\">");
				streamWriter.WriteLine("<web-app>");
				streamWriter.WriteLine("<servlet>");
				streamWriter.WriteLine("<servlet-name>routing</servlet-name>");
				streamWriter.WriteLine("<servlet-class>com.mapinfo.routing.RoutingServlet</servlet-class>");
				streamWriter.WriteLine("\t\t");
				streamWriter.WriteLine("<!-- data configuration -->");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>datasetList</param-name>");
				streamWriter.Write("<param-value>");
				RJSConfigFile.writePath(dataDir, streamWriter, "CENTRAL");
				RJSConfigFile.writePath(dataDir, streamWriter, "MIDWEST");
				RJSConfigFile.writePath(dataDir, streamWriter, "NORTHEAST");
				RJSConfigFile.writePath(dataDir, streamWriter, "PACIFIC");
				RJSConfigFile.writePath(dataDir, streamWriter, "SOUTH");
				RJSConfigFile.writePath(dataDir, streamWriter, "CANADA");
				streamWriter.WriteLine("</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>routingUpdateFilePath</param-name>");
				streamWriter.WriteLine(string.Concat("<param-value>", instdir, "\\RoutingJServer-3.3\\updates</param-value>"));
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("");
				streamWriter.WriteLine("<!-- thread configuration.  if these are not specified they default to the ");
				streamWriter.WriteLine("number of processors. -->");
				streamWriter.WriteLine("<!--\t\t<init-param>");
				streamWriter.WriteLine("<param-name>shortProcessThreads</param-name>");
				streamWriter.WriteLine("<param-value>@shortProcessThreads@</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>longProcessThreads</param-name>");
				streamWriter.WriteLine("<param-value>@longProcessThreads@</param-value>");
				streamWriter.WriteLine("</init-param> -->");
				streamWriter.WriteLine("");
				streamWriter.WriteLine("<!-- capability configuration -->");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>handlesRouteRequests</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>handlesIsoRequests</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>handlesMultiPointRequests</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>handlesRoutingDataRequests</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>handlesPersistentUpdatesRequests</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>handlesMatrixRouteRequests</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>acceptOldRequests</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>validate</param-name>");
				streamWriter.WriteLine("<param-value>false</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("");
				streamWriter.WriteLine("<!-- unit preferences -->");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>distUnit</param-name>");
				streamWriter.WriteLine("<param-value>mile</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>timeUnit</param-name>");
				streamWriter.WriteLine("<param-value>minute</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>speedUnit</param-name>");
				streamWriter.WriteLine("<param-value>mph</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("");
				streamWriter.WriteLine("<!-- timeout preferences -->");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>routeTimeout</param-name>");
				streamWriter.WriteLine("<param-value>500000</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>multiPointTimeout</param-name>");
				streamWriter.WriteLine("<param-value>600000</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>isoTimeout</param-name>");
				streamWriter.WriteLine("<param-value>600000000</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>matrixRouteTimeout</param-name>");
				streamWriter.WriteLine("<param-value>1200000</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("");
				streamWriter.WriteLine("<!-- response preferences -->");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>allowFallback</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>returnPoints</param-name>");
				streamWriter.WriteLine("<param-value>all</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>returnDirections</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>returnSegmentData</param-name>");
				streamWriter.WriteLine("<param-value>true</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("");
				streamWriter.WriteLine("<!-- general algorithm preferences -->");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>optimizeBy</param-name>");
				streamWriter.WriteLine("<param-value>shortestTime</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine();
				streamWriter.WriteLine("<!-- iso algorithm preferences -->");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>isoMajorRoadsOnly</param-name>");
				streamWriter.WriteLine("<param-value>false</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>isoSimplificationFactor</param-name>");
				streamWriter.WriteLine("<param-value>0.05</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>isoDefaultPropagationFactor</param-name>");
				streamWriter.WriteLine("<param-value>0.16</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>isoDefaultAmbientSpeedUnit</param-name>");
				streamWriter.WriteLine("<param-value>mph</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<init-param>");
				streamWriter.WriteLine("<param-name>isoDefaultAmbientSpeed</param-name>");
				streamWriter.WriteLine("<param-value>15</param-value>");
				streamWriter.WriteLine("</init-param>");
				streamWriter.WriteLine("<load-on-startup>1</load-on-startup>");
				streamWriter.WriteLine("</servlet>");
				streamWriter.WriteLine("<servlet-mapping>");
				streamWriter.WriteLine("<servlet-name>routing</servlet-name>");
				streamWriter.WriteLine("<url-pattern>/servlet/routing</url-pattern>");
				streamWriter.WriteLine("</servlet-mapping>");
				streamWriter.WriteLine("</web-app>");
			}
			finally
			{
				bool flag = streamWriter == null;
				if (!flag)
				{
					streamWriter.Dispose();
				}
			}
		}
	}
}
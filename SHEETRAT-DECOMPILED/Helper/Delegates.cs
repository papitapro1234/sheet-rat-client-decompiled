using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Stub.Helper
{
	// Token: 0x02000011 RID: 17
	public class Delegates
	{
		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x06000040 RID: 64
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate uint NtProtectVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref IntPtr RegionSize, uint NewProtect, ref uint OldProtect);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000044 RID: 68
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate IntPtr GetModuleHandleA(string lpModuleName);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x06000048 RID: 72
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate IntPtr GetForegroundWindow();

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x0600004C RID: 76
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x06000050 RID: 80
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000054 RID: 84
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate bool CloseHandle(IntPtr handle);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x06000058 RID: 88
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate bool TerminateProcess(IntPtr dwProcessHandle, int exitCode);

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x0600005C RID: 92
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x06000060 RID: 96
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		// Token: 0x0200001B RID: 27
		// (Invoke) Token: 0x06000064 RID: 100
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);
	}
}

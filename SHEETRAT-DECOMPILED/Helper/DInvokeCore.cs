using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Stub.Helper
{
	// Token: 0x02000010 RID: 16
	public class DInvokeCore
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000227F File Offset: 0x0000047F
		public static IntPtr GetLibraryAddress(string DLLName, string FunctionName)
		{
			IntPtr loadedModuleAddress = DInvokeCore.GetLoadedModuleAddress(DLLName);
			if (loadedModuleAddress == IntPtr.Zero)
			{
				throw new DllNotFoundException(DLLName);
			}
			return DInvokeCore.GetExportAddress(loadedModuleAddress, FunctionName);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003984 File Offset: 0x00001B84
		public static IntPtr GetLoadedModuleAddress(string DLLName)
		{
			foreach (object obj in Process.GetCurrentProcess().Modules)
			{
				ProcessModule processModule = (ProcessModule)obj;
				if (string.Compare(processModule.ModuleName, DLLName, true) == 0)
				{
					return processModule.BaseAddress;
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000039FC File Offset: 0x00001BFC
		public static IntPtr GetExportAddress(IntPtr ModuleBase, string ExportName)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				int num = Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + 60L));
				Marshal.ReadInt16((IntPtr)(ModuleBase.ToInt64() + (long)num + 20L));
				long num2 = ModuleBase.ToInt64() + (long)num + 24L;
				long value;
				if (Marshal.ReadInt16((IntPtr)num2) == 267)
				{
					value = num2 + 96L;
				}
				else
				{
					value = num2 + 112L;
				}
				int num3 = Marshal.ReadInt32((IntPtr)value);
				int num4 = Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num3 + 16L));
				Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num3 + 20L));
				int num5 = Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num3 + 24L));
				int num6 = Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num3 + 28L));
				int num7 = Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num3 + 32L));
				int num8 = Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num3 + 36L));
				for (int i = 0; i < num5; i++)
				{
					if (Marshal.PtrToStringAnsi((IntPtr)(ModuleBase.ToInt64() + (long)Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num7 + (long)(i * 4))))).Equals(ExportName, StringComparison.OrdinalIgnoreCase))
					{
						int num9 = (int)Marshal.ReadInt16((IntPtr)(ModuleBase.ToInt64() + (long)num8 + (long)(i * 2))) + num4;
						int num10 = Marshal.ReadInt32((IntPtr)(ModuleBase.ToInt64() + (long)num6 + (long)(4 * (num9 - num4))));
						intPtr = (IntPtr)((long)ModuleBase + (long)num10);
						break;
					}
				}
			}
			catch
			{
				throw new InvalidOperationException();
			}
			if (intPtr == IntPtr.Zero)
			{
				throw new MissingMethodException(ExportName);
			}
			return intPtr;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000022A1 File Offset: 0x000004A1
		public static object DynamicAPIInvoke(string DLLName, string FunctionName, Type FunctionDelegateType, ref object[] Parameters)
		{
			IntPtr libraryAddress = DInvokeCore.GetLibraryAddress(DLLName, FunctionName);
			if (libraryAddress == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			return DInvokeCore.DynamicFunctionInvoke(libraryAddress, FunctionDelegateType, ref Parameters);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000022C4 File Offset: 0x000004C4
		public static object DynamicFunctionInvoke(IntPtr FunctionPointer, Type FunctionDelegateType, ref object[] Parameters)
		{
			return Marshal.GetDelegateForFunctionPointer(FunctionPointer, FunctionDelegateType).DynamicInvoke(Parameters);
		}
	}
}

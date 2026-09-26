using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x0200000A RID: 10
	internal class Camera
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002A98 File Offset: 0x00000C98
		public static string havecamera()
		{
			string[] array = Camera.FindDevices();
			if (array.Length == 0)
			{
				return false; //false -> 3?%8\\
			}
			return array[0];
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000211F File Offset: 0x0000031F
		public static string[] FindDevices()
		{
			return Camera.GetFiltes(Camera.CLSID_VideoInputDeviceCategory).ToArray();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public static List<string> GetFiltes(Guid category)
		{
			List<string> result = new List<string>();
			Camera.EnumMonikers(category, delegate(IMoniker moniker, Camera.IPropertyBag prop)
			{
				object obj = null;
				prop.Read("FriendlyName", ref obj, 0);
				string item = (string)obj;
				result.Add(item);
				return false;
			});
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private static void EnumMonikers(Guid category, Func<IMoniker, Camera.IPropertyBag, bool> func)
		{
			IEnumMoniker enumMoniker = null;
			Camera.ICreateDevEnum createDevEnum = null;
			try
			{
				createDevEnum = (Camera.ICreateDevEnum)Activator.CreateInstance(Type.GetTypeFromCLSID(Camera.CLSID_SystemDeviceEnum));
				createDevEnum.CreateClassEnumerator(ref category, ref enumMoniker, 0);
				if (enumMoniker != null)
				{
					IMoniker[] array = new IMoniker[1];
					IntPtr zero = IntPtr.Zero;
					while (enumMoniker.Next(array.Length, array, zero) == 0)
					{
						IMoniker moniker = array[0];
						object obj = null;
						Guid iid_IPropertyBag = Camera.IID_IPropertyBag;
						moniker.BindToStorage(null, null, ref iid_IPropertyBag, out obj);
						Camera.IPropertyBag propertyBag = (Camera.IPropertyBag)obj;
						try
						{
							if (func(moniker, propertyBag))
							{
								break;
							}
						}
						finally
						{
							Marshal.ReleaseComObject(propertyBag);
							if (moniker != null)
							{
								Marshal.ReleaseComObject(moniker);
							}
						}
					}
				}
			}
			finally
			{
				if (enumMoniker != null)
				{
					Marshal.ReleaseComObject(enumMoniker);
				}
				if (createDevEnum != null)
				{
					Marshal.ReleaseComObject(createDevEnum);
				}
			}
		}

		// Token: 0x04000010 RID: 16
		public static readonly Guid CLSID_VideoInputDeviceCategory = new Guid("{860BB310-5D01-11d0-BD3B-00A0C911CE86}");

		// Token: 0x04000011 RID: 17
		public static readonly Guid CLSID_SystemDeviceEnum = new Guid("{62BE5D10-60EB-11d0-BD3B-00A0C911CE86}");

		// Token: 0x04000012 RID: 18
		public static readonly Guid IID_IPropertyBag = new Guid("{55272A00-42CB-11CE-8135-00AA004BB851}");

		// Token: 0x0200000B RID: 11
		[ComVisible(true)]
		[Guid("29840822-5B84-11D0-BD3B-00A0C911CE86")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ICreateDevEnum
		{
			// Token: 0x0600001E RID: 30
			int CreateClassEnumerator([In] ref Guid pType, [In] [Out] ref IEnumMoniker ppEnumMoniker, [In] int dwFlags);
		}

		// Token: 0x0200000C RID: 12
		[ComVisible(true)]
		[Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPropertyBag
		{
			// Token: 0x0600001F RID: 31
			int Read([MarshalAs(UnmanagedType.LPWStr)] string PropName, ref object Var, int ErrorLog);

			// Token: 0x06000020 RID: 32
			int Write(string PropName, ref object Var);
		}
	}
}

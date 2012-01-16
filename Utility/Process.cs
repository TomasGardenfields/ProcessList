using System;
using System.Runtime.InteropServices;

namespace Utility
{
	/* ****************************************************** */
	/// <summary>
	/// �v���Z�X����Ɋւ���N���X
	/// </summary>
	public class Process
	{
		/* ====================================================== */
		// �v���Z�X�񋓊֘A
		[Flags]
			private enum SnapshotFlags : uint
		{
			HeapList = 0x00000001,
			Process  = 0x00000002,
			Thread   = 0x00000004,
			Module   = 0x00000008,
			Module32 = 0x00000010,
			All      = (HeapList | Process | Thread | Module),
			Inherit  = 0x80000000
		}

		[StructLayout(LayoutKind.Sequential)]
			private struct PROCESSENTRY32
		{
			public uint		dwSize;
			public uint		cntUsage;
			public uint		th32ProcessID;
			public IntPtr	th32DefaultHeapID;
			public uint		th32ModuleID;
			public uint		cntThreads;
			public uint		th32ParentProcessID;
			public int		pcPriClassBase;
			public uint		dwFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)] public string szExeFile;
		};

		[DllImport("kernel32.dll", SetLastError=true)]
		private static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);

		[DllImport("kernel32.dll", SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll")]
		private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[DllImport("kernel32.dll")]
		private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		/* ====================================================== */
		// �v���Z�X�ڍ׏��֘A

		// 
		private enum PROCESSINFOCLASS: int
		{
			ProcessBasicInformation = 0,
			ProcessQuotaLimits,
			ProcessIoCounters,
			ProcessVmCounters,
			ProcessTimes,
			ProcessBasePriority,
			ProcessRaisePriority,
			ProcessDebugPort,
			ProcessExceptionPort,
			ProcessAccessToken,
			ProcessLdtInformation,
			ProcessLdtSize,
			ProcessDefaultHardErrorMode,
			ProcessIoPortHandlers, // Note: this is kernel mode only
			ProcessPooledUsageAndLimits,
			ProcessWorkingSetWatch,
			ProcessUserModeIOPL,
			ProcessEnableAlignmentFaultFixup,
			ProcessPriorityClass,
			ProcessWx86Information,
			ProcessHandleCount,
			ProcessAffinityMask,
			ProcessPriorityBoost,
			MaxProcessInfoClass,
			ProcessWow64Information = 26
		};

		// 
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
			private struct PROCESS_BASIC_INFORMATION
		{
			public System.IntPtr		ExitStatus;
			public System.IntPtr		PebBaseAddress;
			public System.UIntPtr		AffinityMask;
			public System.IntPtr		BasePriority;
			public System.UIntPtr		UniqueProcessId;
			public System.UIntPtr		InheritedFromUniqueProcessId;
		}

		// 
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
			private struct PROCESS_ENVIRONMENT_BLOCK
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)] public System.Byte[] Reserved1;
			public System.Byte		BeingDebugged;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=1)] public System.Byte[] Reserved2;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)] public System.IntPtr[] Reserved3;
			public System.IntPtr	LDR;
			public System.IntPtr	ProcessParameters;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=104)] public System.Byte[] Reserved4;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=52)] public System.IntPtr[] Reserved5;
			public System.IntPtr	PostProcessInitRoutine;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=128)] public System.Byte[] Reserved6;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=1)] public System.IntPtr[] Reserved7;
			public System.UInt32	SessionId;
		}

		// 
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
			private struct UNICODE_STRING : IDisposable
		{
			public ushort				Length;
			public ushort				MaximumLength;
			public System.IntPtr		buffer;

			public UNICODE_STRING(string s)
			{
				Length = (ushort)(s.Length * System.Text.UnicodeEncoding.CharSize);
				MaximumLength = (ushort)(Length + System.Text.UnicodeEncoding.CharSize);
				buffer = Marshal.StringToHGlobalUni(s);
			}

			public void Dispose()
			{
				Marshal.FreeHGlobal(buffer);
				buffer = IntPtr.Zero;
			}

			public override string ToString()
			{
				return Marshal.PtrToStringUni(buffer);
			}
		}

		// 
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
			private struct USER_PROCESS_PARAMETERS
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)] public System.Byte[] Reserved1;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=10)] public System.IntPtr[] Reserved2;
			public UNICODE_STRING ImagePathName;
			public UNICODE_STRING CommandLine;
		}

		/* ------------------------------------------------------ */
		[DllImport("ntdll.dll", SetLastError=true)]
		private static extern int NtQueryInformationProcess(
			IntPtr hProcess,
			PROCESSINFOCLASS pic,
			IntPtr processInformation,
			uint processInformationLength,
			IntPtr returnLength);

		[DllImport("ntdll.dll", SetLastError=true)]
		private static extern int NtQueryInformationProcess(
			IntPtr hProcess,
			PROCESSINFOCLASS pic,
			ref PROCESS_BASIC_INFORMATION pbi,
			int cb,
			out int pSize);

		/* ------------------------------------------------------ */
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			[Out, MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
			int dwSize,
			out int lpNumberOfBytesRead );

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			IntPtr lpBuffer,
			int dwSize,
			out int lpNumberOfBytesRead );

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			[Out] byte[] lpBuffer,
			int dwSize,
			out int lpNumberOfBytesRead );

		/* ////////////////////////////////////////////////////// */
		private Process()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		/// <summary>
		/// �e�v���Z�X���擾����
		/// </summary>
		/// <param name="i_Process">�v���Z�X</param>
		/// <returns>Process �e�v���Z�X�I�u�W�F�N�g</returns>
		public static System.Diagnostics.Process GetParentProcess( System.Diagnostics.Process i_Process )
		{
			System.Diagnostics.Process a_ResultProcess = null;

			// �ŏ�ʃv���Z�X���w�肳�ꂽ�ꍇ��null��Ԃ�
			if ( i_Process.Id == 0 )
			{
				return a_ResultProcess;
			}

			// ���s���_�̃v���Z�X�X�i�b�v�V���b�g���쐬
			System.IntPtr a_hSnapShot = System.IntPtr.Zero;
			PROCESSENTRY32 a_Entry;
			try
			{
				a_hSnapShot = CreateToolhelp32Snapshot( SnapshotFlags.Process, 0 );
				
				a_Entry = new PROCESSENTRY32();
				a_Entry.dwSize = (uint)Memory.SizeOf( a_Entry );

				if ( Process32First( a_hSnapShot, ref a_Entry ) )
				{
					/* ////////////////////////////////////////////////////// */
					// �v���Z�XID����v�����ꍇ�A���̐e�v���Z�X��Ԃ����[�v�I��
					do
					{
						if ( i_Process.Id == a_Entry.th32ProcessID )
						{
							try
							{
								a_ResultProcess = System.Diagnostics.Process.GetProcessById( (int)a_Entry.th32ParentProcessID );
							}
							catch( System.Exception )
							{
								a_ResultProcess = null;
							}
							finally
							{
							}
							// �q�b�g����v���Z�X��1�����Ȃ̂ŁA�q�b�g�������_�Ń��[�v�I��
							break;
						}
					}
					while ( Process32Next( a_hSnapShot, ref a_Entry ) );
					/* ////////////////////////////////////////////////////// */
				}
			}
			catch ( System.Exception )
			{
			}
			finally
			{
				CloseHandle( a_hSnapShot );
			}
			
			// ��1���q�b�g���Ȃ������ꍇ��null��Ԃ�
			return a_ResultProcess;
		}

		/// <summary>
		/// �q�v���Z�X(����)���擾����
		/// </summary>
		/// <param name="i_Process"></param>
		/// <returns>Process[] �q�v���Z�X(�z��)</returns>
		public static System.Diagnostics.Process[] GetChileProcess( System.Diagnostics.Process i_Process )
		{
			System.IntPtr a_hSnapShot = System.IntPtr.Zero;
			PROCESSENTRY32 a_Entry;
			System.Collections.ArrayList a_ProcessArray = null;

			try
			{
				a_hSnapShot = CreateToolhelp32Snapshot( SnapshotFlags.Process, 0 );
				a_Entry = new PROCESSENTRY32();
				a_Entry.dwSize = (uint)Memory.SizeOf( a_Entry );
				a_ProcessArray = new System.Collections.ArrayList( 0 );

				if ( Process32First( a_hSnapShot, ref a_Entry ) )
				{
					/* ////////////////////////////////////////////////////// */
					// �e�v���Z�XID���w�肵���v���Z�XID�ƈ�v����v���Z�X��z��֒ǉ�����
					do
					{
						if ( i_Process.Id == a_Entry.th32ParentProcessID )
						{
							try
							{
								a_ProcessArray.Add( System.Diagnostics.Process.GetProcessById( (int)a_Entry.th32ProcessID ) );
							}
							catch ( System.Exception )
							{
							}
							finally
							{
							}
						}
					}
					while( Process32Next( a_hSnapShot, ref a_Entry ) );
					/* ////////////////////////////////////////////////////// */
				}
			}
			catch ( System.Exception )
			{
				a_ProcessArray = null;
			}
			finally
			{
				CloseHandle( a_hSnapShot );
			}

			return (System.Diagnostics.Process[])a_ProcessArray.ToArray( typeof( System.Diagnostics.Process ) );
		}

		/// <summary>
		/// �v���Z�X�̃R�}���h���C�����擾����
		/// </summary>
		/// <param name="i_Process">�v���Z�X�I�u�W�F�N�g</param>
		/// <returns>String �R�}���h���C��������</returns>
		public static System.String GetRemoteCommandLine( System.Diagnostics.Process i_Process )
		{
			System.String a_CommandLine = "";
			int ReadSize = 0;
			System.IntPtr a_hProcess = System.IntPtr.Zero;
			System.IntPtr a_Buffer = System.IntPtr.Zero;

			// �n���h���擾�Ɏ��s������I��
			try
			{
				a_hProcess = i_Process.Handle;
			}
			catch( System.Exception )
			{
				return "";
			}

			try
			{
				// Get Process Basic Information
				PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
				NtQueryInformationProcess(
					a_hProcess,
					PROCESSINFOCLASS.ProcessBasicInformation,
					ref pbi,
					Memory.SizeOf( pbi ),
					out ReadSize );

				// Read PEB Memory Block
				PROCESS_ENVIRONMENT_BLOCK peb = new PROCESS_ENVIRONMENT_BLOCK();
				a_Buffer = Marshal.AllocHGlobal( Memory.SizeOf( peb ) );
				ReadProcessMemory(
					a_hProcess,
					pbi.PebBaseAddress,
					a_Buffer,
					Memory.SizeOf( peb ),
					out ReadSize );
				peb = (PROCESS_ENVIRONMENT_BLOCK)Marshal.PtrToStructure( a_Buffer,peb.GetType() );
				Marshal.FreeHGlobal( a_Buffer );
				a_Buffer = System.IntPtr.Zero;

				// Read User Process Parameters
				USER_PROCESS_PARAMETERS upp = new USER_PROCESS_PARAMETERS();
				a_Buffer = Marshal.AllocHGlobal( Memory.SizeOf( upp ) );
				ReadProcessMemory(
					a_hProcess,
					peb.ProcessParameters,
					a_Buffer,
					Memory.SizeOf( upp ),
					out ReadSize );
				upp = (USER_PROCESS_PARAMETERS)Marshal.PtrToStructure( a_Buffer, upp.GetType() );
				Marshal.FreeHGlobal( a_Buffer );
				a_Buffer = System.IntPtr.Zero;

				// CommandLine Option������ �擾
				if ( 0 < upp.CommandLine.Length )
				{
					a_Buffer = Marshal.AllocHGlobal( upp.CommandLine.Length );
					Memory.ZeroMemory( ref a_Buffer, (System.IntPtr)upp.CommandLine.Length );
					ReadProcessMemory(
						a_hProcess,
						upp.CommandLine.buffer,
						a_Buffer,
						upp.CommandLine.Length,
						out ReadSize );
					a_CommandLine = Marshal.PtrToStringUni( a_Buffer, upp.CommandLine.Length / System.Text.UnicodeEncoding.CharSize );
					Marshal.FreeHGlobal( a_Buffer );
					a_Buffer = System.IntPtr.Zero;
				}
			}
			catch ( System.Exception )
			{
				if ( a_Buffer != System.IntPtr.Zero )
				{
					Marshal.FreeHGlobal( a_Buffer );
				}
				a_Buffer = System.IntPtr.Zero;
				a_CommandLine = "";
			}
			finally
			{
			}

			return a_CommandLine;
		}
	}
}

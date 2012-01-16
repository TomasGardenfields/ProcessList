using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

namespace Utility
{
	/// <summary>
	/// ����������Ɋւ���N���X
	/// </summary>
	public class Memory
	{
		[DllImport("Kernel32.dll", EntryPoint="RtlZeroMemory", SetLastError=false)]
		private static extern void ZeroMemory(IntPtr dest, IntPtr size);

		private Memory()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		/// <summary>
		/// �̈�̃[���N���A(ZeroMemory API�֐��̈Ϗ�)
		/// </summary>
		/// <param name="io_Object">�N���A�ΏۃI�u�W�F�N�g</param>
		/// <returns>boolean true:�N���A����/false:�N���A���s</returns>
		public static void ZeroMemory( ref System.IntPtr io_Target, System.IntPtr i_Size )
		{
			ZeroMemory( io_Target, i_Size );
		}

		/// <summary>
		/// �I�u�W�F�N�g�̗̈�T�C�Y(System.Runtime.InteropServices.Marshal.SizeOf()�̈Ϗ�)
		/// </summary>
		/// <param name="i_Object">�ΏۃI�u�W�F�N�g</param>
		/// <returns>int �̈�T�C�Y</returns>
		public static int SizeOf( object i_Object )
		{
			return Marshal.SizeOf( i_Object );
		}

	}
}

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

namespace Utility
{
	/// <summary>
	/// メモリ操作に関するクラス
	/// </summary>
	public class Memory
	{
		[DllImport("Kernel32.dll", EntryPoint="RtlZeroMemory", SetLastError=false)]
		private static extern void ZeroMemory(IntPtr dest, IntPtr size);

		private Memory()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		/// <summary>
		/// 領域のゼロクリア(ZeroMemory API関数の委譲)
		/// </summary>
		/// <param name="io_Object">クリア対象オブジェクト</param>
		/// <returns>boolean true:クリア成功/false:クリア失敗</returns>
		public static void ZeroMemory( ref System.IntPtr io_Target, System.IntPtr i_Size )
		{
			ZeroMemory( io_Target, i_Size );
		}

		/// <summary>
		/// オブジェクトの領域サイズ(System.Runtime.InteropServices.Marshal.SizeOf()の委譲)
		/// </summary>
		/// <param name="i_Object">対象オブジェクト</param>
		/// <returns>int 領域サイズ</returns>
		public static int SizeOf( object i_Object )
		{
			return Marshal.SizeOf( i_Object );
		}

	}
}

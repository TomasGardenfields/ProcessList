using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Utility
{
	/// <summary>
	/// Windows の概要の説明です。
	/// </summary>
	public class Windows
	{

		/* ////////////////////////////////////////////////////// */
		// ウィンドウメッセージ
		public enum WM : int
		{
			HSCROLL = 0x0114,
			VSCROLL = 0x0115,
			SETFOCUS = 0x0007,
			USER = 0x0400,
		}

		public enum EM : uint
		{
			CANUNDO = 0xC6,
			EMPTYUNDOBUFFER = 0xCD,
			FMTLINES = 0xC8,
			GETFIRSTVISIBLELINE = 0xCE,
			GETHANDLE = 0xBD,
			GETLANGOPTIONS = (WM.USER + 121),
			GETLINE = 0xC4,
			GETLINECOUNT = 0xBA,
			GETMARGINS = 0xD4,
			GETMODIFY = 0xB8,
			GETPASSWORDCHAR = 0xD2,
			GETRECT = 0xB2,
			GETSCROLLPOS = 0x04DD,
			GETSEL = 0xB0,
			GETTHUMB = 0xBE,
			GETWORDBREAKPROC = 0xD1,
			LIMITTEXT = 0xC5,
			LINEFROMCHAR = 0xC9,
			LINEINDEX = 0xBB,
			LINELENGTH = 0xC1,
			LINESCROLL = 0xB6,
			REPLACESEL = 0xC2,
			SCROLL = 0xB5,
			SCROLLCARET = 0xB7,
			SETHANDLE = 0xBC,
			SETLANGOPTIONS = (WM.USER + 120),
			SETMARGINS = 0xD3,
			SETMODIFY = 0xB9,
			SETPASSWORDCHAR = 0xCC,
			SETREADONLY = 0xCF,
			SETRECT = 0xB3,
			SETRECTNP = 0xB4,
			SETSCROLLPOS = 0x04DE,
			SETSEL = 0xB1,
			SETTABSTOPS = 0xCB,
			SETWORDBREAKPROC = 0xD0,
			UNDO = 0xC7,
		}

		public enum IMF : uint
		{
			DUALFONT = 0x0080,
		}

		public enum LVM : uint
		{
			FIRST = 0x1000,
			SETCOLUMNWIDTH = (LVM.FIRST + 30),
		}

		public enum LVSCW : int
		{
			AUTOSIZE = -1,
			AUTOSIZE_USEHEADER = -2,
		}

		public enum DeviceCap : int
		{
			/// <summary>
			/// Device driver version
			/// </summary>
			DRIVERVERSION = 0,
			/// <summary>
			/// Device classification
			/// </summary>
			TECHNOLOGY = 2,
			/// <summary>
			/// Horizontal size in millimeters
			/// </summary>
			HORZSIZE = 4,
			/// <summary>
			/// Vertical size in millimeters
			/// </summary>
			VERTSIZE = 6,
			/// <summary>
			/// Horizontal width in pixels
			/// </summary>
			HORZRES = 8,
			/// <summary>
			/// Vertical height in pixels
			/// </summary>
			VERTRES = 10,
			/// <summary>
			/// Number of bits per pixel
			/// </summary>
			BITSPIXEL = 12,
			/// <summary>
			/// Number of planes
			/// </summary>
			PLANES = 14,
			/// <summary>
			/// Number of brushes the device has
			/// </summary>
			NUMBRUSHES = 16,
			/// <summary>
			/// Number of pens the device has
			/// </summary>
			NUMPENS = 18,
			/// <summary>
			/// Number of markers the device has
			/// </summary>
			NUMMARKERS = 20,
			/// <summary>
			/// Number of fonts the device has
			/// </summary>
			NUMFONTS = 22,
			/// <summary>
			/// Number of colors the device supports
			/// </summary>
			NUMCOLORS = 24,
			/// <summary>
			/// Size required for device descriptor
			/// </summary>
			PDEVICESIZE = 26,
			/// <summary>
			/// Curve capabilities
			/// </summary>
			CURVECAPS = 28,
			/// <summary>
			/// Line capabilities
			/// </summary>
			LINECAPS = 30,
			/// <summary>
			/// Polygonal capabilities
			/// </summary>
			POLYGONALCAPS = 32,
			/// <summary>
			/// Text capabilities
			/// </summary>
			TEXTCAPS = 34,
			/// <summary>
			/// Clipping capabilities
			/// </summary>
			CLIPCAPS = 36,
			/// <summary>
			/// Bitblt capabilities
			/// </summary>
			RASTERCAPS = 38,
			/// <summary>
			/// Length of the X leg
			/// </summary>
			ASPECTX = 40,
			/// <summary>
			/// Length of the Y leg
			/// </summary>
			ASPECTY = 42,
			/// <summary>
			/// Length of the hypotenuse
			/// </summary>
			ASPECTXY = 44,
			/// <summary>
			/// Shading and Blending caps
			/// </summary>
			SHADEBLENDCAPS = 45,

			/// <summary>
			/// Logical pixels inch in X
			/// </summary>
			LOGPIXELSX = 88,
			/// <summary>
			/// Logical pixels inch in Y
			/// </summary>
			LOGPIXELSY = 90,

			/// <summary>
			/// Number of entries in physical palette
			/// </summary>
			SIZEPALETTE = 104,
			/// <summary>
			/// Number of reserved entries in palette
			/// </summary>
			NUMRESERVED = 106,
			/// <summary>
			/// Actual color resolution
			/// </summary>
			COLORRES = 108,

			// Printing related DeviceCaps. These replace the appropriate Escapes
			/// <summary>
			/// Physical Width in device units
			/// </summary>
			PHYSICALWIDTH = 110,
			/// <summary>
			/// Physical Height in device units
			/// </summary>
			PHYSICALHEIGHT = 111,
			/// <summary>
			/// Physical Printable Area x margin
			/// </summary>
			PHYSICALOFFSETX = 112,
			/// <summary>
			/// Physical Printable Area y margin
			/// </summary>
			PHYSICALOFFSETY = 113,
			/// <summary>
			/// Scaling factor x
			/// </summary>
			SCALINGFACTORX = 114,
			/// <summary>
			/// Scaling factor y
			/// </summary>
			SCALINGFACTORY = 115,

			/// <summary>
			/// Current vertical refresh rate of the display device (for displays only) in Hz
			/// </summary>
			VREFRESH = 116,
			/// <summary>
			/// Horizontal width of entire desktop in pixels
			/// </summary>
			DESKTOPVERTRES = 117,
			/// <summary>
			/// Vertical height of entire desktop in pixels
			/// </summary>
			DESKTOPHORZRES = 118,
			/// <summary>
			/// Preferred blt alignment
			/// </summary>
			BLTALIGNMENT = 119
		}

		/* ////////////////////////////////////////////////////// */
		// 構造体
		[StructLayout(LayoutKind.Sequential)]
			public struct RECT
		{
			private int _Left;
			private int _Top;
			private int _Right;
			private int _Bottom;

			public RECT(RECT Rectangle) : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
			{
			}
			public RECT(int Left, int Top, int Right, int Bottom)
			{
				_Left = Left;
				_Top = Top;
				_Right = Right;
				_Bottom = Bottom;
			}

			public int X 
			{
				get { return _Left; }
				set { _Left = value; }
			}
			public int Y 
			{
				get { return _Top; }
				set { _Top = value; }
			}

			public int Left 
			{
				get { return _Left; }
				set { _Left = value; }
			}
			public int Top 
			{
				get { return _Top; }
				set { _Top = value; }
			}
			public int Right 
			{
				get { return _Right; }
				set { _Right = value; }
			}
			public int Bottom 
			{
				get { return _Bottom; }
				set { _Bottom = value; }
			}

			public int Height 
			{
				get { return _Bottom - _Top; }
				set { _Bottom = value + _Top; }
			}
			public int Width 
			{
				get { return _Right - _Left; }
				set { _Right = value + _Left; }
			}

			public System.Drawing.Point Location 
			{
				get { return new System.Drawing.Point(Left, Top); }
				set 
				{
					_Left = value.X;
					_Top = value.Y;
				}
			}
			public System.Drawing.Size Size 
			{
				get { return new System.Drawing.Size(Width, Height); }
				set 
				{
					_Right = value.Width + _Left;
					_Bottom = value.Height + _Top;
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct POINT
		{
			public int X;
			public int Y;

			public POINT(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}

			public static implicit operator System.Drawing.Point(POINT p)
			{
				return new System.Drawing.Point(p.X, p.Y);
			}

			public static implicit operator POINT(System.Drawing.Point p)
			{
				return new POINT(p.X, p.Y);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct BITMAPINFO 
		{
			public Int32 biSize;
			public Int32 biWidth;
			public Int32 biHeight;
			public Int16 biPlanes;
			public Int16 biBitCount;
			public Int32 biCompression;
			public Int32 biSizeImage;
			public Int32 biXPelsPerMeter;
			public Int32 biYPelsPerMeter;
			public Int32 biClrUsed;
			public Int32 biClrImportant;
		}

		/* ////////////////////////////////////////////////////// */
		// API定義
		// User32
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern System.IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, ref RECT lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, ref POINT lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		public static extern uint SendMessage(IntPtr hWnd, uint wMsg, uint wParam, uint lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Kernel32
		[DllImport("kernel32.dll")]
		public static extern int MulDiv(int nNumber, int nNumerator, int nDenominator);

		// Gdi32
		[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi,
			uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		private Windows()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		/* ////////////////////////////////////////////////////// */
		// DWORD/WORD/BYTE演算関数
		public static System.UInt32 MAKEDWORD( System.UInt16 wParam, System.UInt16 lParam )
		{
			return (System.UInt32)( wParam << 16 + lParam );
		}

		public static System.UInt16 LOWORD( System.UInt32 dwParam )
		{
			return (System.UInt16)( dwParam & 0xFFFF );
		}

		public static System.UInt16 HIWORD( System.UInt32 dwParam )
		{
			return (System.UInt16)( ( dwParam >> 16 ) & 0xFFFF );
		}

		public static System.UInt16 MAKEWORD( System.Byte wParam, System.Byte lParam )
		{
			return (System.UInt16)( wParam << 8 + lParam );
		}

		public static System.Byte LOBYTE( System.UInt16 wParam )
		{
			return (System.Byte)( wParam & 0xFF );
		}

		public static System.Byte HIBYTE( System.UInt16 wParam )
		{
			return (System.Byte)( ( wParam >> 8 ) & 0xFF );
		}

	}
}

using System;
using System.ComponentModel;
//using System.Collections;
//using System.Diagnostics;

namespace Utility.CustomControl
{
	/// <summary>
	/// CustomRichEditBox の概要の説明です。
	/// </summary>
	public class CustomRichTextBox : System.Windows.Forms.RichTextBox
	{
		/* ////////////////////////////////////////////////////// */
		/// <summary>
		/// リッチテキストボックスのIMEアジア言語サポート設定を取得または設定します。
		/// </summary>
		private bool m_boDualFont = true;
		[Category("表示")]
		[DefaultValue(true)]
		[Description("リッチテキストボックスのIMEアジア言語サポート設定を取得または設定します。")]
		public bool DualFont
		{
			get
			{
				return m_boDualFont;
			}
			set
			{
				// DualFontプロパティ値を取得
				m_boDualFont = value;

				// 現在のリッチテキストボックスのIMEアジア言語サポート設定を取得
				uint dwLangOptions = (uint)Utility.Windows.SendMessage( this.Handle, (uint)Utility.Windows.EM.GETLANGOPTIONS, 0, 0 );
				uint dwOptions = 0;

				// 「true」指定された場合、DualFontを有効に
				if (value)
				{
					dwOptions = dwLangOptions | (uint)Utility.Windows.IMF.DUALFONT;
				}
					// 「false」指定された場合、DualFontを無効に
				else
				{
					dwOptions = dwLangOptions & ~((uint)Utility.Windows.IMF.DUALFONT);
				}
				// 再度、リッチテキストボックスに設定
				Utility.Windows.SendMessage(this.Handle, (uint)Utility.Windows.EM.SETLANGOPTIONS, 0, dwOptions);
			}
		}

		// WordWrap時にフォント設定がクリアされるのを防ぐ
		public new bool WordWrap
		{
			get
			{
				return base.WordWrap;
			}
			set
			{
				base.WordWrap = value;

				// DualFontプロパティ値を再度設定する
				this.m_boDualFont = DualFont;
			}
		}

		/* ////////////////////////////////////////////////////// */
		private bool m_boSelectable = true;
		[Category("動作")]
		[DefaultValue(true)]
		[Description("コントロールにフォーカスが設定されるかどうかを示す値を取得または設定します。")]
		public bool Selectable
		{
			get
			{
				return this.m_boSelectable;
			}
			set
			{
				m_boSelectable = value;
				//this.SetStyle( System.Windows.Forms.ControlStyles.Selectable, this.m_boSelectable );
			}

		}

		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CustomRichTextBox(System.ComponentModel.IContainer container)
		{
			///
			/// Windows.Forms クラス作成デザイナ サポートに必要です。
			///
			container.Add(this);
			InitializeComponent();
			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			this.DualFont = this.m_boDualFont;
			this.Selectable = this.m_boSelectable;
		}

		public CustomRichTextBox()
		{
			///
			/// Windows.Forms クラス作成デザイナ サポートに必要です。
			///
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			this.DualFont = this.m_boDualFont;
			this.Selectable = this.m_boSelectable;
		}

		/// <summary>
		/// ウィンドウメッセージ処理
		/// </summary>
		/// <param name="m">メッセージ情報</param>
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			// HSCROLL/VSCROLLメッセージに対してもHScroll()/VScroll()イベント実行
			switch (m.Msg)
			{
				case (int)Utility.Windows.WM.HSCROLL:
					base.OnHScroll( System.EventArgs.Empty );
					break;
				case (int)Utility.Windows.WM.VSCROLL:
					base.OnVScroll( System.EventArgs.Empty );
					break;
				case (int)Utility.Windows.WM.SETFOCUS:
					// 選択不可能の場合はSETFOCUSメッセージを無視させる
					if( !this.m_boSelectable )
					{
						return;
					}
					break;
				default:
					break;
			}
			base.WndProc( ref m );			
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

	}
}

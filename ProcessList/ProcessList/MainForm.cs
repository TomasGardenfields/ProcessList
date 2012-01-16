using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Utility;

namespace ProcessList
{


	/// <summary>
	/// MainForm の概要の説明です。
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private enum OperationMode : uint
		{
			And = 1,
			Or = 2,
			Xor = 3,
		}

		private Utility.CustomControl.CustomListView listViewProcess;
		private System.Windows.Forms.ColumnHeader columnHeaderID;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderNo;
		private System.Windows.Forms.Timer timerRefresh;
		private System.Windows.Forms.ColumnHeader columnHeaderStartTime;
		private System.Windows.Forms.ColumnHeader columnHeaderCommandLine;
		private System.Windows.Forms.TextBox textBoxProcessName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.Button buttonKillProcess;
		private System.Windows.Forms.TextBox textBoxPrcessID;
		private System.Windows.Forms.ToolTip toolTipListView;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.TextBox textBoxCommandLine;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonOperation;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.StatusBar statusBarProcessList;

		/* ------------------------------------------------------ */

		private OperationMode enOperationMode;

		public MainForm()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//

		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listViewProcess = new Utility.CustomControl.CustomListView();
			this.columnHeaderNo = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderID = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStartTime = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderCommandLine = new System.Windows.Forms.ColumnHeader();
			this.timerRefresh = new System.Windows.Forms.Timer(this.components);
			this.textBoxProcessName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.textBoxPrcessID = new System.Windows.Forms.TextBox();
			this.buttonKillProcess = new System.Windows.Forms.Button();
			this.toolTipListView = new System.Windows.Forms.ToolTip(this.components);
			this.buttonExit = new System.Windows.Forms.Button();
			this.textBoxCommandLine = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonOperation = new System.Windows.Forms.Button();
			this.statusBarProcessList = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// listViewProcess
			// 
			this.listViewProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.columnHeaderNo,
																							  this.columnHeaderID,
																							  this.columnHeaderStartTime,
																							  this.columnHeaderName,
																							  this.columnHeaderCommandLine});
			this.listViewProcess.FullRowSelect = true;
			this.listViewProcess.GridLines = true;
			this.listViewProcess.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.listViewProcess.LabelEdit = true;
			this.listViewProcess.Location = new System.Drawing.Point(8, 120);
			this.listViewProcess.Name = "listViewProcess";
			this.listViewProcess.Size = new System.Drawing.Size(476, 200);
			this.listViewProcess.TabIndex = 0;
			this.listViewProcess.View = System.Windows.Forms.View.Details;
			this.listViewProcess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewProcess_KeyDown);
			this.listViewProcess.DoubleClick += new System.EventHandler(this.listViewProcess_DoubleClick);
			this.listViewProcess.SelectedIndexChanged += new System.EventHandler(this.listViewProcess_SelectedIndexChanged);
			this.listViewProcess.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listViewProcess_MouseMove);
			// 
			// columnHeaderNo
			// 
			this.columnHeaderNo.Text = "No";
			this.columnHeaderNo.Width = 35;
			// 
			// columnHeaderID
			// 
			this.columnHeaderID.Text = "ID";
			this.columnHeaderID.Width = 50;
			// 
			// columnHeaderStartTime
			// 
			this.columnHeaderStartTime.Text = "StartTime";
			this.columnHeaderStartTime.Width = 75;
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 100;
			// 
			// columnHeaderCommandLine
			// 
			this.columnHeaderCommandLine.Text = "CommandLine";
			this.columnHeaderCommandLine.Width = 155;
			// 
			// timerRefresh
			// 
			this.timerRefresh.Interval = 1000;
			this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
			// 
			// textBoxProcessName
			// 
			this.textBoxProcessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxProcessName.Location = new System.Drawing.Point(104, 8);
			this.textBoxProcessName.Name = "textBoxProcessName";
			this.textBoxProcessName.Size = new System.Drawing.Size(292, 19);
			this.textBoxProcessName.TabIndex = 1;
			this.textBoxProcessName.Text = "";
			this.textBoxProcessName.TextChanged += new System.EventHandler(this.textBoxProcess_TextChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Process Name";
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRefresh.Location = new System.Drawing.Point(408, 88);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(72, 24);
			this.buttonRefresh.TabIndex = 3;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// textBoxPrcessID
			// 
			this.textBoxPrcessID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPrcessID.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textBoxPrcessID.Location = new System.Drawing.Point(104, 56);
			this.textBoxPrcessID.Name = "textBoxPrcessID";
			this.textBoxPrcessID.ReadOnly = true;
			this.textBoxPrcessID.Size = new System.Drawing.Size(292, 19);
			this.textBoxPrcessID.TabIndex = 4;
			this.textBoxPrcessID.Text = "";
			// 
			// buttonKillProcess
			// 
			this.buttonKillProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonKillProcess.Location = new System.Drawing.Point(408, 56);
			this.buttonKillProcess.Name = "buttonKillProcess";
			this.buttonKillProcess.Size = new System.Drawing.Size(72, 24);
			this.buttonKillProcess.TabIndex = 5;
			this.buttonKillProcess.Text = "Kill";
			this.buttonKillProcess.Click += new System.EventHandler(this.buttonKillProcess_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExit.Location = new System.Drawing.Point(408, 328);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(80, 24);
			this.buttonExit.TabIndex = 6;
			this.buttonExit.Text = "Exit";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// textBoxCommandLine
			// 
			this.textBoxCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCommandLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.textBoxCommandLine.Location = new System.Drawing.Point(104, 32);
			this.textBoxCommandLine.Name = "textBoxCommandLine";
			this.textBoxCommandLine.Size = new System.Drawing.Size(292, 19);
			this.textBoxCommandLine.TabIndex = 7;
			this.textBoxCommandLine.Text = "";
			this.textBoxCommandLine.TextChanged += new System.EventHandler(this.textBoxProcess_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 15);
			this.label2.TabIndex = 8;
			this.label2.Text = "Command Line";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "Process ID";
			// 
			// buttonOperation
			// 
			this.buttonOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOperation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonOperation.Location = new System.Drawing.Point(408, 16);
			this.buttonOperation.Name = "buttonOperation";
			this.buttonOperation.Size = new System.Drawing.Size(72, 24);
			this.buttonOperation.TabIndex = 10;
			this.buttonOperation.Click += new System.EventHandler(this.buttonOperation_Click);
			// 
			// statusBarProcessList
			// 
			this.statusBarProcessList.Location = new System.Drawing.Point(0, 358);
			this.statusBarProcessList.Name = "statusBarProcessList";
			this.statusBarProcessList.Size = new System.Drawing.Size(492, 16);
			this.statusBarProcessList.TabIndex = 11;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(492, 374);
			this.Controls.Add(this.statusBarProcessList);
			this.Controls.Add(this.buttonOperation);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxCommandLine);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonKillProcess);
			this.Controls.Add(this.textBoxPrcessID);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxProcessName);
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.listViewProcess);
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "MainForm";
			this.Text = "ProcessList";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThreadAttribute]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			this.toolTipListView.SetToolTip( this.textBoxProcessName, "表示するプロセス名\n※正規表現対応" );
			this.toolTipListView.SetToolTip( this.textBoxCommandLine, "表示するプロセスのコマンドライン引数\n※正規表現対応" );
			this.toolTipListView.SetToolTip( this.buttonOperation, "検索条件" );
			this.toolTipListView.SetToolTip( this.textBoxPrcessID, "プロセスID\n※読み取り専用" );

			this.toolTipListView.SetToolTip( this.buttonRefresh, "リスト更新" );
			this.toolTipListView.SetToolTip( this.buttonKillProcess, "プロセス強制終了" );
			this.toolTipListView.SetToolTip( this.buttonExit, "プログラム終了" );

			enOperationMode = OperationMode.Or;

			this.buttonOperation_Click( sender, e );

			this.buttonRefresh_Click( sender, e );
		}

		private void timerRefresh_Tick(object sender, System.EventArgs e)
		{
		}

		private void buttonRefresh_Click(object sender, System.EventArgs e)
		{
			this.textBoxPrcessID.Text = "";

			/* ////////////////////////////////////////////////////// */
			this.listViewProcess.BeginUpdate();
			{
				this.listViewProcess.Items.Clear();
				
				int i = 0;
				foreach( System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses() )
				{
					// プロセス名とコマンドライン文字列を取得
					System.String a_strProcessName = p.ProcessName;
					System.String a_strCommandLine = Utility.Process.GetRemoteCommandLine( p );

					// コマンドライン文字列中に存在する終端文字をスペースに置換
					// ※StringやRegxのReplaceでは(文字コードの関係か?)正常に置換できなかった為、手動で検索・置換している
					byte[] byteArray = System.Text.Encoding.Unicode.GetBytes( a_strCommandLine );
					byte[] byteSpaceArray = System.Text.Encoding.Unicode.GetBytes( " " );			// 置換用スペース文字

					int charSize = System.Text.UnicodeEncoding.CharSize;
					int index = a_strCommandLine.IndexOf( "\0" );
					while ( index != -1 )
					{
						byteArray[index*charSize] = byteSpaceArray[0];
						byteArray[index*charSize+1] = byteSpaceArray[1];
						index = a_strCommandLine.IndexOf( "\0", index + 1 );
					}
					a_strCommandLine = System.Text.Encoding.Unicode.GetString( byteArray ); 

					// プロセス名/コマンドラインが正規表現でマッチするプロセスのみリストへ追加する
					bool boProcessNameMatched = false;
					bool boCommandLineMatched = false;
					bool boMatched = false;
					try
					{
						boProcessNameMatched = System.Text.RegularExpressions.Regex.IsMatch(
							a_strProcessName,
							this.textBoxProcessName.Text,
							System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline );
						boCommandLineMatched = System.Text.RegularExpressions.Regex.IsMatch(
							a_strCommandLine,
							this.textBoxCommandLine.Text,
							System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline );

						switch ( this.enOperationMode )
						{
							case OperationMode.Or:
								boMatched = boProcessNameMatched | boCommandLineMatched;
								break;
							case OperationMode.And:
								boMatched = boProcessNameMatched & boCommandLineMatched;
								break;
							case OperationMode.Xor:
								boMatched = boProcessNameMatched ^ boCommandLineMatched;
								break;
							default:
								boMatched = false;
								break;
						}
					}
					catch( Exception ex )
					{
						// マッチング中に例外が発生した場合は無条件でマッチ無し扱いとする
						System.Diagnostics.Debug.WriteLine( ex.ToString() );
						boMatched = false;
					}

					// マッチしたら1行追加
					// マッチしなかったら追加せず次へ進む
					if ( !boMatched )
					{
						continue;
					}

					try
					{
						this.listViewProcess.Items.Insert( i, "" );
						this.listViewProcess.Items[i].SubItems.Add( "" );
						this.listViewProcess.Items[i].SubItems.Add( "" );
						this.listViewProcess.Items[i].SubItems.Add( "" );
						this.listViewProcess.Items[i].SubItems.Add( "" );

						this.listViewProcess.Items[i].SubItems[0].Text = (i+1).ToString();					// 見出し(項目番号)
						this.listViewProcess.Items[i].SubItems[1].Text = p.Id.ToString();					// プロセスID
						this.listViewProcess.Items[i].SubItems[2].Text = p.StartTime.ToShortTimeString();	// プロセス開始時刻
						this.listViewProcess.Items[i].SubItems[3].Text = a_strProcessName;					// プロセス名
						this.listViewProcess.Items[i].SubItems[4].Text = a_strCommandLine;					// コマンドライン引数
						i++;
					}
					catch( Exception ex )
					{
						System.Diagnostics.Debug.WriteLine( ex.ToString() );
					}
					finally
					{
					}
				}
			}
			// コマンドライン長にカラム幅を合わせる
			Utility.Windows.SendMessage(
				this.listViewProcess.Handle,
				(Int32)Utility.Windows.LVM.SETCOLUMNWIDTH,
				4,
				(Int32)Utility.Windows.LVSCW.AUTOSIZE );
			this.listViewProcess.EndUpdate();
			/* ////////////////////////////////////////////////////// */

			this.statusBarProcessList.Text = "" + this.listViewProcess.Items.Count.ToString() + " processes matched.";
		}

		/// <summary>
		/// 選択しているプロセスを終了する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonKillProcess_Click(object sender, System.EventArgs e)
		{
			// クエリを出してプロセスキル
			System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(
				"Kill process ?",
				"Query",
				System.Windows.Forms.MessageBoxButtons.OKCancel );

			if ( result == System.Windows.Forms.DialogResult.Cancel )
			{
				return;
			}

			foreach ( System.Windows.Forms.ListViewItem item in this.listViewProcess.SelectedItems )
			{
				int id = int.Parse( item.SubItems[1].Text );

				try
				{
					System.Diagnostics.Process.GetProcessById( id ).Kill();
				}
				catch ( Exception ex )
				{
					System.Diagnostics.Debug.WriteLine( ex.ToString() );
					System.Windows.Forms.MessageBox.Show( ex.ToString() );
				}
			}
			this.buttonRefresh_Click( sender, e );
		}

		private void listViewProcess_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// 選択した項目のプロセス情報を表示
			try
			{
				// 最初の選択項目の情報取得
				this.textBoxPrcessID.Text = this.listViewProcess.SelectedItems[0].SubItems[1].Text;
			}
			catch ( Exception ex )
			{
				System.Diagnostics.Debug.WriteLine( ex.ToString() );
			}
			finally
			{
			}
		}

		private void textBoxProcess_TextChanged(object sender, System.EventArgs e)
		{
			this.buttonRefresh_Click( sender, e );
		}

		// リストビューダブルクリック時
		// テキストボックスを表示し項目を編集できるようにする
		private void listViewProcess_DoubleClick(object sender, System.EventArgs e)
		{
			System.Drawing.Point p = System.Drawing.Point.Empty;
			p.X = System.Windows.Forms.Cursor.Position.X;
			p.Y = System.Windows.Forms.Cursor.Position.Y;

			System.Drawing.Point cp = this.listViewProcess.PointToClient( p );

			System.Windows.Forms.ListViewItem item = this.listViewProcess.GetItemAt( cp.X, cp.Y );
			System.Drawing.Point lvsp = this.listViewProcess.HitTest( cp );
			
			// 範囲外(行/列インデックス値取得失敗)の場合は即時終わり
			if ( item.Index == -1 || cp.X == -1 )
			{
				return;
			}

			// マウスの位置に編集用テキストボックスを表示
			Utility.CustomControl.ListViewTextBox InputBox = new Utility.CustomControl.ListViewTextBox( this.listViewProcess, item, lvsp.X );
			InputBox.FinishInput += new Utility.CustomControl.ListViewTextBox.InputEventHandler( inputBox_FinishInput );
			InputBox.Show();
		}

		/// <summary>
		/// テキストボックス編集終了時にコールされるイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void inputBox_FinishInput(object sender, Utility.CustomControl.ListViewTextBox.InputEventArgs e)
		{
			this.listViewProcess.Items[e.Row].SubItems[e.Column].Text = e.Text;
		}

		// リストビューマウス移動時
		private void listViewProcess_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Drawing.Point p = System.Drawing.Point.Empty;
			p.X = System.Windows.Forms.Cursor.Position.X;
			p.Y = System.Windows.Forms.Cursor.Position.Y;

			System.Drawing.Point lvsp = this.listViewProcess.HitTest( this.listViewProcess.PointToClient( p ) );

			if ( lvsp.Y == -1 || lvsp.X == -1 )
			{
				// this.toolTipListView.Active = false;
			}
			else
			{
				String a_strToolTip = this.listViewProcess.Items[lvsp.Y].SubItems[lvsp.X].Text;
				this.toolTipListView.SetToolTip( this.listViewProcess, a_strToolTip );
				this.toolTipListView.Active = true;
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void listViewProcess_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			int i;
			if ( e.Control && e.KeyCode == System.Windows.Forms.Keys.C )
			{
				// Ctrl+C押下時
				// 選択している行の情報をタブ文字で連結してクリップボードへ出力する

				System.String text = "";

				foreach( System.Windows.Forms.ListViewItem item in this.listViewProcess.SelectedItems )
				{
					text += item.SubItems[0].Text;
					for ( i = 1 ; i < this.listViewProcess.Columns.Count ; i++ )
					{
						text += ( "\t" + item.SubItems[i].Text );
					}
					text += "\r\n";
				}

				if ( text != "" )
				{
					System.Windows.Forms.Clipboard.SetDataObject( text, true );
				}
			}
			else if ( e.Control && e.KeyCode == System.Windows.Forms.Keys.A )
			{
				// Ctrl+A押下時
				// 全ての行を選択する

				this.listViewProcess.BeginUpdate();
				foreach( System.Windows.Forms.ListViewItem item in this.listViewProcess.Items )
				{
					item.Selected = true;
				}
				this.listViewProcess.EndUpdate();
			}
		}

		private void buttonOperation_Click(object sender, System.EventArgs e)
		{
			switch ( this.enOperationMode )
			{
				case OperationMode.Or:
					this.enOperationMode = OperationMode.And;
					break;
				case OperationMode.And:
					this.enOperationMode = OperationMode.Xor;
					break;
				case OperationMode.Xor:
					this.enOperationMode = OperationMode.Or;
					break;
				default:
					break;
			}

			this.buttonOperation.Text = this.enOperationMode.ToString();
			this.buttonRefresh_Click( sender, e );
		}

	}
}

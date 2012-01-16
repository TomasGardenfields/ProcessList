using System;
using System.ComponentModel;
//using System.Collections;
//using System.Diagnostics;
using System.Windows.Forms;

namespace Utility.CustomControl
{
	/// <summary>
	/// ListViewEditBox の概要の説明です。
	/// </summary>
	public class ListViewTextBox : System.Windows.Forms.TextBox
	{
		public class InputEventArgs : System.EventArgs
		{
			public string Text = "";
			public int Row = -1;
			public int Column = -1;
		}

		// 
		public delegate void InputEventHandler(object sender, InputEventArgs e);

		//イベントデリゲートの宣言
		public event InputEventHandler FinishInput;

		private InputEventArgs args = new InputEventArgs();
		private bool finished = false;

		public ListViewTextBox( System.Windows.Forms.ListView lvParent, System.Windows.Forms.ListViewItem lvItem, int ColumnIndex ) : base()
		{
			args.Text = lvItem.SubItems[ColumnIndex].Text;
			args.Row = lvItem.Index;
			args.Column = ColumnIndex;

			int left = 0;
			for ( int i = 0 ; i < ColumnIndex ; i++ )
			{
				left += lvParent.Columns[i].Width;
			}
			int width = lvParent.Columns[ColumnIndex].Width;
			int height = lvItem.Bounds.Height - 4;

			this.Parent = lvParent;
			this.Size = new System.Drawing.Size(width, height);
			this.Left = left;
			this.Top = lvItem.Bounds.Y - 1;
			this.Text = lvItem.SubItems[ColumnIndex].Text;
			this.LostFocus += new EventHandler(textbox_LostFocus);
			this.ImeMode = ImeMode.NoControl;
			this.Multiline = false;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.KeyDown += new KeyEventHandler(textbox_KeyDown);
			this.Focus();
		}

		void Finish(string new_name)
		{
			// Enterで入力を完了した場合はKeyDownが呼ばれた後に
			// さらにLostFocusが呼ばれるため，二回Finishが呼ばれる
			if (!finished)
			{
				// textbox.Hide()すると同時にLostFocusするため，
				// finished=trueを先に呼び出しておかないと，
				// このブロックが二回呼ばれてしまう．
				finished = true;
				this.Hide();
				args.Text = new_name;
				FinishInput(this, args);
			}
		}

		void textbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Finish(this.Text);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				Finish(args.Text);
			}
		}

		void textbox_LostFocus(object sender, EventArgs e)
		{
			Finish(this.Text);
		}	
	}
}

using System;
//using System.Drawing;
//using System.Windows.Forms;

namespace Utility.CustomControl
{
	/// <summary>
	/// 
	/// </summary>
	public class CustomListView : System.Windows.Forms.ListView 
	{
		public CustomListView()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		/// <summary>
		/// リストビューのクリック位置から行/列を取得
		/// </summary>
		/// <param name="i_Point">クリック位置(クライアント座標)</param>
		/// <returns>クリック位置に対応する行/列番号(0から始まる)</returns>
		public System.Drawing.Point HitTest( System.Drawing.Point i_Point )
		{
			int i;
			int left = 0;
			int RowIndex = -1, ColumnIndex = -1;
			System.Drawing.Point ResultPoint = new System.Drawing.Point( -1, -1 );

			/* ------------------------------------------------------ */
			// 行番号の取得
			System.Windows.Forms.ListViewItem item = this.GetItemAt( i_Point.X, i_Point.Y );

			// 範囲外(行インデックス値取得失敗)の場合は即時終わり
			if ( item == null )
			{
				return ResultPoint;
			}

			RowIndex = item.Index;

			/* ------------------------------------------------------ */
			// 列番号の取得
			for ( i = 0 ; i < this.Columns.Count ; i++ )
			{
				left += this.Columns[i].Width;
				if ( i_Point.X < left )
				{
					ColumnIndex = i;
					break;
				}
			}

			// 範囲外(列インデックス値取得失敗)の場合は即時終わり
			if ( ColumnIndex == -1 )
			{
				return ResultPoint;
			}

			ResultPoint.X = ColumnIndex;
			ResultPoint.Y = RowIndex;

			return ResultPoint;
		}
	}
}

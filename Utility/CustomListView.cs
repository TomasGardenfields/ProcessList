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
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		/// <summary>
		/// ���X�g�r���[�̃N���b�N�ʒu����s/����擾
		/// </summary>
		/// <param name="i_Point">�N���b�N�ʒu(�N���C�A���g���W)</param>
		/// <returns>�N���b�N�ʒu�ɑΉ�����s/��ԍ�(0����n�܂�)</returns>
		public System.Drawing.Point HitTest( System.Drawing.Point i_Point )
		{
			int i;
			int left = 0;
			int RowIndex = -1, ColumnIndex = -1;
			System.Drawing.Point ResultPoint = new System.Drawing.Point( -1, -1 );

			/* ------------------------------------------------------ */
			// �s�ԍ��̎擾
			System.Windows.Forms.ListViewItem item = this.GetItemAt( i_Point.X, i_Point.Y );

			// �͈͊O(�s�C���f�b�N�X�l�擾���s)�̏ꍇ�͑����I���
			if ( item == null )
			{
				return ResultPoint;
			}

			RowIndex = item.Index;

			/* ------------------------------------------------------ */
			// ��ԍ��̎擾
			for ( i = 0 ; i < this.Columns.Count ; i++ )
			{
				left += this.Columns[i].Width;
				if ( i_Point.X < left )
				{
					ColumnIndex = i;
					break;
				}
			}

			// �͈͊O(��C���f�b�N�X�l�擾���s)�̏ꍇ�͑����I���
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

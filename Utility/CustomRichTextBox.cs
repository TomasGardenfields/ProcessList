using System;
using System.ComponentModel;
//using System.Collections;
//using System.Diagnostics;

namespace Utility.CustomControl
{
	/// <summary>
	/// CustomRichEditBox �̊T�v�̐����ł��B
	/// </summary>
	public class CustomRichTextBox : System.Windows.Forms.RichTextBox
	{
		/* ////////////////////////////////////////////////////// */
		/// <summary>
		/// ���b�`�e�L�X�g�{�b�N�X��IME�A�W�A����T�|�[�g�ݒ���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		private bool m_boDualFont = true;
		[Category("�\��")]
		[DefaultValue(true)]
		[Description("���b�`�e�L�X�g�{�b�N�X��IME�A�W�A����T�|�[�g�ݒ���擾�܂��͐ݒ肵�܂��B")]
		public bool DualFont
		{
			get
			{
				return m_boDualFont;
			}
			set
			{
				// DualFont�v���p�e�B�l���擾
				m_boDualFont = value;

				// ���݂̃��b�`�e�L�X�g�{�b�N�X��IME�A�W�A����T�|�[�g�ݒ���擾
				uint dwLangOptions = (uint)Utility.Windows.SendMessage( this.Handle, (uint)Utility.Windows.EM.GETLANGOPTIONS, 0, 0 );
				uint dwOptions = 0;

				// �utrue�v�w�肳�ꂽ�ꍇ�ADualFont��L����
				if (value)
				{
					dwOptions = dwLangOptions | (uint)Utility.Windows.IMF.DUALFONT;
				}
					// �ufalse�v�w�肳�ꂽ�ꍇ�ADualFont�𖳌���
				else
				{
					dwOptions = dwLangOptions & ~((uint)Utility.Windows.IMF.DUALFONT);
				}
				// �ēx�A���b�`�e�L�X�g�{�b�N�X�ɐݒ�
				Utility.Windows.SendMessage(this.Handle, (uint)Utility.Windows.EM.SETLANGOPTIONS, 0, dwOptions);
			}
		}

		// WordWrap���Ƀt�H���g�ݒ肪�N���A�����̂�h��
		public new bool WordWrap
		{
			get
			{
				return base.WordWrap;
			}
			set
			{
				base.WordWrap = value;

				// DualFont�v���p�e�B�l���ēx�ݒ肷��
				this.m_boDualFont = DualFont;
			}
		}

		/* ////////////////////////////////////////////////////// */
		private bool m_boSelectable = true;
		[Category("����")]
		[DefaultValue(true)]
		[Description("�R���g���[���Ƀt�H�[�J�X���ݒ肳��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B")]
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
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CustomRichTextBox(System.ComponentModel.IContainer container)
		{
			///
			/// Windows.Forms �N���X�쐬�f�U�C�i �T�|�[�g�ɕK�v�ł��B
			///
			container.Add(this);
			InitializeComponent();
			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
			this.DualFont = this.m_boDualFont;
			this.Selectable = this.m_boSelectable;
		}

		public CustomRichTextBox()
		{
			///
			/// Windows.Forms �N���X�쐬�f�U�C�i �T�|�[�g�ɕK�v�ł��B
			///
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
			this.DualFont = this.m_boDualFont;
			this.Selectable = this.m_boSelectable;
		}

		/// <summary>
		/// �E�B���h�E���b�Z�[�W����
		/// </summary>
		/// <param name="m">���b�Z�[�W���</param>
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			// HSCROLL/VSCROLL���b�Z�[�W�ɑ΂��Ă�HScroll()/VScroll()�C�x���g���s
			switch (m.Msg)
			{
				case (int)Utility.Windows.WM.HSCROLL:
					base.OnHScroll( System.EventArgs.Empty );
					break;
				case (int)Utility.Windows.WM.VSCROLL:
					base.OnVScroll( System.EventArgs.Empty );
					break;
				case (int)Utility.Windows.WM.SETFOCUS:
					// �I��s�\�̏ꍇ��SETFOCUS���b�Z�[�W�𖳎�������
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
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

		#region �R���|�[�l���g �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

	}
}

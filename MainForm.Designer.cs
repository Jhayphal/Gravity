
namespace StarConnections
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pictureBoxSpace = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpace)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBoxSpace
			// 
			this.pictureBoxSpace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxSpace.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxSpace.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.pictureBoxSpace.Name = "pictureBoxSpace";
			this.pictureBoxSpace.Size = new System.Drawing.Size(600, 384);
			this.pictureBoxSpace.TabIndex = 0;
			this.pictureBoxSpace.TabStop = false;
			this.pictureBoxSpace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSpace_MouseMove);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.BackColor = System.Drawing.Color.Black;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(600, 384);
			this.ControlBox = false;
			this.Controls.Add(this.pictureBoxSpace);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(75, 81);
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Star connections";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpace)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxSpace;
	}
}


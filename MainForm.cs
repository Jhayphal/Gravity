using StarConnections.Providers;
using System;

#if DEBUG
using System.Diagnostics;
#endif

using System.Timers;
using System.Windows.Forms;

using Timer = System.Timers.Timer;

namespace StarConnections
{
	public partial class MainForm : Form
	{
		private const double FrameRate = 30D; // количество кадров в секунду
#if DEBUG
		private readonly Stopwatch clock = new Stopwatch();
#endif
		
		private readonly Timer timer = new Timer(1000D / FrameRate);
		private readonly Painter painter = new Painter(new DefaultStarProvider());
		
		private Action redrawScreen;

		public MainForm()
		{
			InitializeComponent();
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x0112 /* WM_SYSCOMMAND */)
			{
				switch (m.WParam.ToInt32())
				{
					case 0xF020: /* SC_MINIMIZE */
						m.Result = IntPtr.Zero;

						break;
				}
			}

			base.WndProc(ref m);
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var parameters = base.CreateParams;
				
				parameters.ExStyle |= 0x80 | 0x080;
				
				parameters.X = 0;
				parameters.Y = 0;

				var area = Screen.PrimaryScreen.WorkingArea;
				
				parameters.Width = area.Width;
				parameters.Height = area.Height;
				
				return parameters;
			} 
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			var area = Screen.PrimaryScreen.WorkingArea;

			redrawScreen = new Action(() => pictureBoxSpace.Refresh());

			pictureBoxSpace.Image = painter.View;
			
			timer.Elapsed += DrawFrame;
			timer.Start();
		}

		private void DrawFrame(object sender, ElapsedEventArgs e)
		{
			if (timer != null)
				timer.Enabled = false;

#if DEBUG
			clock.Start();
#endif

			painter.Draw();

#if DEBUG
			clock.Stop();
			Debug.WriteLine(clock.ElapsedMilliseconds.ToString());

			clock.Reset();
#endif

			pictureBoxSpace.Invoke(redrawScreen);

			if (timer != null)
				timer.Enabled = true;
		}

		private void pictureBoxSpace_MouseMove(object sender, MouseEventArgs e)
		{
			painter.SetMousePosition(e.Location);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			timer.Dispose();
			painter.Dispose();
		}
	}
}

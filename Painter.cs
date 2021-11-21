using StarConnections.Providers;
using System;
using System.Drawing;
using System.Linq;

namespace StarConnections
{
	/// <summary>
	/// Художник.
	/// </summary>
	internal sealed class Painter : IDisposable
	{
		/// <summary>
		/// Холст.
		/// </summary>
		public Image View = new Bitmap(Space.Width, Space.Height);

		private readonly Pen[] linePens;
		private readonly IStarProvider provider;

		private readonly Star mouse;
		private readonly Star[] stars;
		
		private const int StarsCount = 110;
		private const float LineWidth = 1F;
		
		private const int MinColor = 0;
		private const int CursorIndex = 0;

		private readonly Color background = Color.FromArgb(MinColor, MinColor, MinColor);

		public Painter(IStarProvider provider)
		{
			this.provider = provider;

			stars = this.provider
				.Get(StarsCount)
				.ToArray();


			mouse = stars[CursorIndex];

			SetupMouse();


			linePens = new Pen[byte.MaxValue + 1];

			SetupPens();
		}

		/// <summary>
		/// Задать параметры звезды, которая отвечает за указатель мыши.
		/// </summary>
		private void SetupMouse()
		{
			mouse.Size = 6F; // пусть указатель мыши имеет постоянный размер (и, соответственно, массу)
		}

		/// <summary>
		/// Подготовить карандаши для рисования линий.
		/// </summary>
		private void SetupPens()
		{
			for (int i = 0; i <= byte.MaxValue; ++i)
				linePens[i] = new Pen(Color.FromArgb(i, i, i), LineWidth);
		}

		/// <summary>
		/// Нарисовать кадр.
		/// </summary>
		public void Draw()
		{
			for (int i = 1; i < StarsCount; ++i)
			{
				var current = stars[i];

				if (!current.TryMove())
					current.Burn(provider.GenerateSize);
			}

			try
			{
				using (var canvas = Graphics.FromImage(View))
				{
					canvas.Clear(background);

					for (int i = 0; i < StarsCount; ++i)
					{
						var a = stars[i];

						if (!a.OnTheScreen)
							continue;

						for (int j = i + 1; j < StarsCount; ++j)
						{
							var b = stars[j];

							if (!b.OnTheScreen)
								continue;

							try
							{
								float distance = a.GetConnectionLength(b);

								if (distance > Space.Darkness)
									continue;

								int penIndex = СalculatePenIndexByDistance(distance);

								a.Gravity(b, distance);
								b.Gravity(a, distance);

								if (!b.NoDrawLines)
								{
									float shiftToCenterA = a.Size / 2F;
									float shiftToCenterB = b.Size / 2F;

									var pen = linePens[penIndex];

									canvas.DrawLine(
										pen,
										a.Position.X + shiftToCenterA,
										a.Position.Y + shiftToCenterA,
										b.Position.X + shiftToCenterB,
										b.Position.Y + shiftToCenterB);
								}
							}
							finally
							{
								if (!b.NoDraw)
									canvas.FillEllipse(
										b.Ligth,
										b.Position.X, 
										b.Position.Y,
										b.Size,
										b.Size);
							}
						}
					}
				}
			}
			catch
			{
				// игнорируем
			}
		}

		/// <summary>
		/// Вычислить индекс карандаша для рисования соединений в соответствии с расстоянием между звёздами.
		/// </summary>
		/// <param name="distance">Расстояние между звёздами.</param>
		/// <returns>Индекс карандаша.</returns>
		private int СalculatePenIndexByDistance(float distance)
		{
			int penIndex = (int)(distance / Space.Darkness * byte.MaxValue);

			if (penIndex == 0)
				return byte.MaxValue;

			// т.к. зрение воспринимает яркость логарифмически
			penIndex = byte.MaxValue - (int)(Math.Log(penIndex) / Math.Log(byte.MaxValue) * byte.MaxValue);

			return penIndex;
		}

		/// <summary>
		/// Установить координаты звезды в соответствие с позицией указателя мыши.
		/// </summary>
		/// <param name="position">Позиция указателя мыши.</param>
		public void SetMousePosition(Point position)
		{
			mouse.Position.X = position.X;
			mouse.Position.Y = position.Y;
		}

		public void Dispose()
		{
			var pens = linePens;

			foreach (var pen in pens)
				pen.Dispose();

			View.Dispose();

			foreach (var star in stars)
				star?.Dispose();
		}
	}
}

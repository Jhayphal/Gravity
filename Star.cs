using System;
using System.Drawing;
using System.Numerics;

namespace StarConnections
{
	/// <summary>
	/// Звезда.
	/// </summary>
	internal sealed class Star : IDisposable
	{
		private static readonly Random generator = new Random((int)DateTime.Now.TimeOfDay.TotalSeconds);

		private bool locked;

		/// <summary>
		/// Размер звезды.
		/// </summary>
		/// <remarks>Диаметр в пикселях.</remarks>
		public float Size { get; set; } = 4F;

		/// <summary>
		/// Цвет звезды.
		/// </summary>
		public Brush Ligth { get; set; }

		/// <summary>
		/// Не рисовать звезду.
		/// </summary>
		public bool NoDraw { get; set; }

		/// <summary>
		/// Не рисовать связи.
		/// </summary>
		public bool NoDrawLines { get; set; }

		/// <summary>
		/// Положение в пространстве.
		/// </summary>
		public Vector2 Position;

		/// <summary>
		/// Скорость.
		/// </summary>
		public float Speed;

		/// <summary>
		/// Ускорение.
		/// </summary>
		public Vector2 Acceleration;

		/// <summary>
		/// Запрет на перемещение.
		/// </summary>
		public bool Locked => locked;

		public Star(Func<Random, float> generateSize)
		{
			Burn(generateSize);

			Position.X = generator.Next(Space.Width + Space.Darkness);
			Position.Y = generator.Next(Space.Height + Space.Darkness);

			Ligth = new SolidBrush(Color.FromArgb(
				generator.Next(byte.MaxValue),
				generator.Next(byte.MaxValue),
				generator.Next(byte.MaxValue)));
		}

		/// <summary>
		/// Запретить перемещение звезды.
		/// </summary>
		public void Lock()
		{
			if (!locked)
				locked = true;
		}

		/// <summary>
		/// Инициализирует случайные положение в пространстве и скорость.
		/// </summary>
		/// <param name="generateSize">Метод генерации размера звезды.</param>
		public void Burn(Func<Random, float> generateSize)
		{
			if (Locked)
				return;

			Size = generateSize(generator);

			if (Generate()) // фиксированный X
			{
				if (Generate()) // X - начало координат
				{
					Position.X = 0F;

					Acceleration = new Vector2(1F, 1F);
				}
				else // X - конец координат
				{
					Position.X = Space.Width + Space.Darkness;
					Acceleration = new Vector2(-1F, 1F);
				}

				if (Generate())
					Acceleration.Y = -1F;

				Position.Y = generator.Next(Space.Height + Space.Darkness);
			}
			else // фиксированный Y
			{
				if (generator.Next(10) < 3) // Y - начало координат
				{
					Position.Y = 0F;
					Acceleration = new Vector2(1F, 1F);
				}
				else // Y - конец координат
				{
					Position.Y = Space.Height + Space.Darkness;
					Acceleration = new Vector2(1F, -1F);
				}

				if (Generate())
					Acceleration.X = -1F;

				Position.X = generator.Next(Space.Width + Space.Darkness);
			}

			const float MinSpeed = 0.4F;

			Speed = (float)generator.NextDouble() * Space.Viscosity + MinSpeed;
		}

		/// <summary>
		/// Сгенерировать логику.
		/// </summary>
		/// <returns>Логика.</returns>
		private static bool Generate()
		{
			return generator.Next(100) < 50;
		}

		/// <summary>
		/// Вычислить расстояние между двумя звёздами.
		/// </summary>
		/// <param name="other">Звезда-компаньон.</param>
		/// <returns>Расстояние между двумя звёздами</returns>
		public float GetConnectionLength(Star other)
		{
			return (other.Position - Position).Length();
		}

		/// <summary>
		/// Притяжение.
		/// </summary>
		/// <param name="other">Звезда-компаньон.</param>
		/// <param name="distance">Расстояние.</param>
		public void Gravity(Star other, float distance)
		{
			if (Locked)
				return;

			var force = other.Position - Position;
			force /= distance;
			force *= other.Size;

			force *= Space.GravityForce;
			
			Acceleration += force;
		}

		/// <summary>
		/// Находится ли в зоне рисования.
		/// </summary>
		public bool OnTheScreen
		{
			get
			{
				return !(Position.X < -Space.Darkness || Position.X > Space.Width + Space.Darkness) 
					&& !(Position.Y < -Space.Darkness || Position.Y > Space.Height + Space.Darkness);
			}
		}

		/// <summary>
		/// Выполнить движение в один шаг.
		/// </summary>
		/// <returns>Истина, если в результате движения звезда осталась в области рисования.</returns>
		public bool TryMove()
		{
			if (Locked)
				return true;

			Position += Speed * Acceleration;

			return OnTheScreen;
		}

		public void Dispose()
		{
			Ligth?.Dispose();
		}
	}
}

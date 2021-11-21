namespace StarConnections
{
	/// <summary>
	/// Холст-пространство.
	/// </summary>
	internal static class Space
	{
		/// <summary>
		/// Шрина в пикселях.
		/// </summary>
		public const int Width = 1920;

		/// <summary>
		/// Высота в пикселях.
		/// </summary>
		public const int Height = 1080;

		/// <summary>
		/// Темнота.
		/// </summary>
		/// <remarks>Максимальное расстояние видимости между точками в пикселях.</remarks>
		public const int Darkness = 150;

		/// <summary>
		/// Вязкость.
		/// </summary>
		/// <remarks>Максимальная скорость звёзд в пикселях.</remarks>
		public const float Viscosity = 3;

		/// <summary>
		/// Сила гравитации.
		/// </summary>
		public const float GravityForce = 0.03F;
	}
}

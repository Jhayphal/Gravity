using System;
using System.Collections.Generic;

namespace StarConnections.Providers
{
	class DefaultStarProvider : IStarProvider
	{
		public Func<Random, float> GenerateSize => _ => 4F; 

		public IEnumerable<Star> Get(int count)
		{
			var stars = new Star[count];

			for (int i = 0; i < count; ++i)
				stars[i] = new Star(GenerateSize);

			return stars;
		}
	}
}

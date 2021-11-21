using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarConnections.Providers
{
	interface IStarProvider
	{
		Func<Random, float> GenerateSize { get; }

		IEnumerable<Star> Get(int count);
	}
}

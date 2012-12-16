using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lydian.Unity.CallHandlers.Core
{
	internal abstract class Composite<T>
	{
		private IEnumerable<T> Publishers { get; set; }

		[InjectionMethod]
		public void LoadPublishers(IUnityContainer container)
		{
			Publishers = container.ResolveAll<T>()
								  .ToArray();
		}

		protected void Broadcast(Action<T> broadcastAction)
		{
			foreach (var publisher in Publishers)
				broadcastAction(publisher);
		}
	}
}

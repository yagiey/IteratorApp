using System;

namespace IteratorApp
{
	internal interface IIterableIn<T>
	{
		void ForEach(Action<T> proc);
	}
}

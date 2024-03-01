using System;

namespace IteratorApp
{
	internal class Procedure<T> : IProcedure<T>
	{
		readonly Action<T> _action;

		public Procedure(Action<T> action)
		{
			_action = action;
		}

		public void Process(T obj)
		{
			_action(obj);
		}
	}
}

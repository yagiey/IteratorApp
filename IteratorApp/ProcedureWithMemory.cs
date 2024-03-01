namespace IteratorApp
{
	internal class ProcedureWithMemory<T> : IProcedureWithMemory<T>
	{
		T _lastProcessedValue;

		public void Process(T obj)
		{
			_lastProcessedValue = obj;
		}

		public T LastProcessedValue()
		{
			return _lastProcessedValue;
		}
	}
}

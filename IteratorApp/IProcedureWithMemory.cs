namespace IteratorApp
{
	internal interface IProcedureWithMemory<T> : IProcedure<T>
	{
		T LastProcessedValue();
	}
}

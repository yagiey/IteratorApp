using System;
using System.Collections.Generic;

namespace IteratorApp
{
	internal class OutIterator<T> : IEnumerator<T>
	{
		JobHolder _holder;

		IProcedureWithMemory<T> _proc;

		Action _initCont;

		public OutIterator(JobHolder holder,
		  IProcedureWithMemory<T> proc,
		  Action initCont)
		{
			_holder = holder;
			_proc = proc;
			_initCont = initCont;
			Reset();
		}

		public T Current
		{
			get
			{
				CheckDisposed();
				return _proc.LastProcessedValue();
			}
		}

		public void Dispose()
		{
			CheckDisposed();
			_holder = null!;
			_initCont = null!;
			_proc = null!;
		}

		object? System.Collections.IEnumerator.Current => Current;

		public bool MoveNext()
		{
			CheckDisposed();

			if (_holder.Job is null)
			{
				return false;
			}
			_holder.Job();
			return true;
		}

		public void Reset()
		{
			CheckDisposed();
			_holder.Job = _initCont;
		}

		void CheckDisposed()
		{
			if (_holder is null || _proc is null)
			{
				throw new InvalidOperationException();
			}
		}
	}
}

using System;
using System.Collections.Generic;

namespace IteratorApp
{
	internal class TreeNode<T> : IIterableIn<T>, IEnumerable<T>
	{
		public TreeNode(T value)
		{
			Value = value;
			Parent = null;
			Left = null;
			Right = null;
		}

		public T Value { get; set; }

		public TreeNode<T>? Parent { get; set; }

		public TreeNode<T>? Left { get; set; }

		public TreeNode<T>? Right { get; set; }

		public void ForEach(Action<T> action)
		{
			JobHolder holder = new();
			IProcedure<T> proc = new Procedure<T>(action);
			ForEachDetail(this, proc, holder, null);
			while (holder.Job is not null)
			{
				holder.Job();
			}
		}

		static void ForEachDetail(
		  TreeNode<T> root,
		  IProcedure<T> proc,
		  JobHolder holder,
		  Action? cont)
		{
			// find left most
			TreeNode<T> leftmost = root;
			while (leftmost.Left is not null)
			{
				leftmost = leftmost.Left;
			}

			AtLeftMost(leftmost, proc, holder, cont);
		}

		static void AtLeftMost(
		  TreeNode<T> leftmost,
		  IProcedure<T> proc,
		  JobHolder holder,
		  Action? cont)
		{
			proc.Process(leftmost.Value);

			if (leftmost.Parent is not null)
			{
				if (leftmost == leftmost.Parent.Left)
				{
					holder.Job =
					  () => OnTheWay(leftmost.Parent, proc, holder);
				}
				else
				{
					holder.Job = cont;
				}
			}
			else
			{
				holder.Job = null;
			}
		}

		static void OnTheWay(TreeNode<T> node, IProcedure<T> proc, JobHolder holder)
		{
			proc.Process(node.Value);

			if (node.Right is not null)
			{
				TreeNode<T>? nextRoot = GetNextRoot(node);
				if (nextRoot is null)
				{
					// exit
					holder.Job =
					  () => ForEachDetail(node.Right, proc, holder, null);
				}
				else
				{
					holder.Job =
					  () => ForEachDetail(
						node.Right,
						proc, holder,
						() => OnTheWay(nextRoot, proc, holder));
				}
			}
			else
			{
				if (node.Parent is not null)
				{
					holder.Job =
					  () => OnTheWay(node.Parent, proc, holder);
				}
				else
				{
					holder.Job = null;
				}
			}
		}

		static TreeNode<T>? GetNextRoot(TreeNode<T> node)
		{
			while (node.Parent is not null)
			{
				if (node.Parent.Left == node)
				{
					return node.Parent;
				}
				node = node.Parent;
			}
			return null;
		}

		public IEnumerator<T> GetEnumerator()
		{
			JobHolder holder = new();
			IProcedureWithMemory<T> proc =
			  new ProcedureWithMemory<T>();

			void init() => ForEachDetail(this, proc, holder, null);
			holder.Job = init;

			return new OutIterator<T>(holder, proc, init);
		}

		System.Collections.IEnumerator
		System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

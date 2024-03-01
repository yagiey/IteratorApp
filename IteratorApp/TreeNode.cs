using System;

namespace IteratorApp
{
	internal class TreeNode<T> : IIterableIn<T>
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
			ForEachDetail(this, action, holder, null);
			while (holder.Job is not null)
			{
				holder.Job();
			}
		}

		static void ForEachDetail(
		  TreeNode<T> root,
		  Action<T> action,
		  JobHolder holder,
		  Action? cont)
		{
			// find left most
			TreeNode<T> leftmost = root;
			while (leftmost.Left is not null)
			{
				leftmost = leftmost.Left;
			}

			AtLeftMost(leftmost, action, holder, cont);
		}

		static void AtLeftMost(
		  TreeNode<T> leftmost,
		  Action<T> action,
		  JobHolder holder,
		  Action? cont)
		{
			action(leftmost.Value);

			if (leftmost.Parent is not null)
			{
				if (leftmost == leftmost.Parent.Left)
				{
					holder.Job =
					  () => OnTheWay(leftmost.Parent, action, holder);
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

		static void OnTheWay(TreeNode<T> node, Action<T> action, JobHolder holder)
		{
			action(node.Value);

			if (node.Right is not null)
			{
				TreeNode<T>? nextRoot = GetNextRoot(node);
				if (nextRoot is null)
				{
					// exit
					holder.Job =
					  () => ForEachDetail(node.Right, action, holder, null);
				}
				else
				{
					holder.Job =
					  () => ForEachDetail(
						node.Right,
						action, holder,
						() => OnTheWay(nextRoot, action, holder));
				}
			}
			else
			{
				if (node.Parent is not null)
				{
					holder.Job =
					  () => OnTheWay(node.Parent, action, holder);
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
	}
}

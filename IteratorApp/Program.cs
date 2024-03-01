using System;

namespace IteratorApp
{
	internal class Program
	{
		static void Main()
		{
			TreeNode<int> tree = CreateTree();
			tree.ForEach( n => Console.Write(n + " "));
		}

		static TreeNode<int> CreateTree()
		{
			TreeNode<int> node01 = new(1);
			TreeNode<int> node02 = new(2);
			TreeNode<int> node03 = new(3);
			TreeNode<int> node04 = new(4);
			TreeNode<int> node05 = new(5);
			TreeNode<int> node06 = new(6);
			TreeNode<int> node07 = new(7);
			TreeNode<int> node08 = new(8);
			TreeNode<int> node09 = new(9);
			TreeNode<int> node10 = new(10);
			TreeNode<int> node11 = new(11);
			TreeNode<int> node12 = new(12);
			TreeNode<int> node13 = new(13);
			TreeNode<int> node14 = new(14);
			TreeNode<int> node15 = new(15);

			node08.Left = node04;
			node08.Right = node12;

			node04.Parent = node08;
			node04.Left = node02;
			node04.Right = node06;

			node02.Parent = node04;
			node02.Left = node01;
			node02.Right = node03;

			node01.Parent = node02;
			node03.Parent = node02;

			node06.Parent = node04;
			node06.Left = node05;
			node06.Right = node07;

			node05.Parent = node06;
			node07.Parent = node06;

			node12.Parent = node08;
			node12.Left = node10;
			node12.Right = node14;

			node10.Parent = node12;
			node10.Left = node09;
			node10.Right = node11;

			node09.Parent = node10;
			node11.Parent = node10;

			node14.Parent = node12;
			node14.Left = node13;
			node14.Right = node15;

			node13.Parent = node14;
			node15.Parent = node14;

			return node08;
		}
	}
}

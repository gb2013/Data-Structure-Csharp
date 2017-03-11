using DataStructure.Library.LinearList;
using System;

namespace DataStructure.ConsoleTest.LinearListTest
{
    class ArrayListTest
    {
        static void Main(string[] args)
        {
            ArrayList<int> list = new ArrayList<int>();
            Console.WriteLine("list现在的容量为：" + list.Capacity + " 长度为：" + list.Count);
            list.Add(1);//添加一个元素
            Console.WriteLine("list现在的容量为：" + list.Capacity + " 长度为：" + list.Count);
            for (int i = 2; i <= 5; i++)
            {
                list.Add(i);
            }
            Console.WriteLine("list现在的容量为：" + list.Capacity + " 长度为：" + list.Count);
            for (int i = 6; i <= 9; i++)
            {
                list.Add(i);
            }
            Console.WriteLine("list现在的容量为：" + list.Capacity + " 长度为：" + list.Count);
            //打印所有元素
            foreach (var i in list)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();//换行
            list.RemoveAt(3);
            list.RemoveAt(3);
            //打印所有元素
            foreach (var i in list)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();//换行
            Console.WriteLine("list现在的容量为：" + list.Capacity + " 长度为：" + list.Count);
            list.TrimToSize();
            Console.WriteLine("list现在的容量为：" + list.Capacity + " 长度为：" + list.Count);
        }
    }
}

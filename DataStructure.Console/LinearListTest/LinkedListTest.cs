using System;
using System.Linq;
using System.Text;
using DataStructure.Library.LinearList;

namespace DataStructure.ConsoleTest.LinearListTest
{
    class LinkedListTest
    {
        static void Main(string[] args)
        {
            LinkedList<int> list=new LinkedList<int>();
            list.Add(0);
            list.Add(1);
            list.Add(3);
            list.Insert(0,4);
            foreach (var i in list)
            {
                Console.Write(i+" ");
            }
            Console.WriteLine();
            list.RemoveAt(3);
            list[2] = 13;
            foreach (var i in list)
            {
                Console.Write(i + " ");
            }
        }
    }
}

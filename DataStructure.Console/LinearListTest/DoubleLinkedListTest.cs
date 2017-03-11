using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure.Library.LinearList;

namespace DataStructure.ConsoleTest.LinearListTest
{
    class DoubleLinkedListTest
    {
        static void Main()
        {
            //创建一个实例来进行方法
            DoubleLinkedList list=new DoubleLinkedList();
            Random rand=new Random();
            Console.Write("添加元素：");
            for(int i=0;i<10;i++)
            {
                //用循环给链表添加1~10之间的随机数
                int nextInt = rand.Next(10);
                Console.Write("{0}",nextInt);
                list.Add(nextInt);
            }
            Console.WriteLine();
            Console.WriteLine("打印元素："+list.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure.Library.LinearList;

namespace DataStructure.ConsoleTest.LinearListTest
{
    public class CircularLinkedListTest
    {
        static void Main(string[] args)
        {
            CircularLinkedList<int> cLst=new CircularLinkedList<int>();
            string s = string.Empty;//用于记录出队顺序
            Console.WriteLine("请输入总人数：");
            int count = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("请输入数字M的值：");
            int m = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("游戏开始！");
            for(int i=1;i<=count;i++)
            {
                cLst.Add(i);
            }
            Console.WriteLine("所有人："+cLst.ToString());
            while (cLst.Count>1)
            {
                cLst.Move(m);//数数
                s += cLst.Current.ToString() + " ";
                cLst.RemoveCurrentNode();//出队
                Console.Write("\r\n剩余的人："+cLst.ToString());
                Console.Write(" 开始数数的人："+cLst.Current);
            }
            Console.WriteLine("\r\n出队顺序："+s+cLst.Current);

        }
    }
}

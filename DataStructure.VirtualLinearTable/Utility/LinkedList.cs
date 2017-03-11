using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure.VirtualLinearTable.Utility
{
    public class LinkedList
    {
        //成员
        private int count;//记录元素个数
        private MemoryUnit _head;//头指针
        private Heap _heap; //托管堆
        private Color _background;//背景色
        //构造方法
        public LinkedList(Heap heap, Color color)
        {
            _heap = heap;
            _background = color;
        }

        //在链表的结尾添加元素
        public void Add(string value)//在链表的结尾添加元素
        {
            //在托管堆中分配新空间
            MemoryUnit newNode = _heap.NewUnit(_background);
            newNode.Value = value;
            if (_head == null)
            {
                //如果链表为空则直接作为头指针
                _head = newNode;
            }
            else
            {
                GetByIndex(count - 1).Next = newNode;
            }
            count++;
        }

        //查找指定索引的元素
        private MemoryUnit GetByIndex(int index)
        {
            if (index < 0 || index >= this.count)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            MemoryUnit tempNode = this._head;
            for (int i = 0; i < index; i++) //循环查找
            {
                tempNode = tempNode.Next;
            }
            return tempNode;
        }

        //在指定索引处插入元素
        public void Insert(int index, string value)
        {
            MemoryUnit newNode = _heap.NewUnit(_background);
            newNode.Value = value;
            if (index == 0)//如果在起始结点处插入
            {
                if (_heap == null)//如果链表为空
                {
                    _head = newNode;
                }
                else//链表不为空
                {
                    //新元素作为第一个元素
                    newNode.Next = _head;
                    _head = newNode;
                }
            }
            else
            {
                //查找插入点的前驱结点
                MemoryUnit prevNode = GetByIndex(index - 1);
                MemoryUnit nextNode = prevNode.Next;//插入点的后续结点
                prevNode.Next = newNode;//前驱结点的后继结点为新结点
                newNode.Next = nextNode;//
            }
        }

        //删除指定索引元素
        public void RemoveAt(int index)
        {
            if (index == 0)//如果删除起始结点
            {
                _head.SetUnuse();//删除
                _head = _head.Next;
            }
            else
            {
                //查找删除结点的前驱结点
                MemoryUnit prevNode = GetByIndex(index - 1);
                if(prevNode.Next==null)
                {
                    throw new ArgumentOutOfRangeException("索引超出范围");
                }
                prevNode.Next.SetUnuse();
                prevNode.Next = prevNode.Next.Next;//删除
            }
            count--;
        }
    }
}

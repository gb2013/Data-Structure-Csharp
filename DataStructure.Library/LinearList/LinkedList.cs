using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure.Library.LinearList
{
    /// <summary>
    /// 单向链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T>:IEnumerable<T>
    {
        #region 成员

        private int _count;  //记录元素个数
        private Node _head;  //头指针

        public LinkedList()
        {
            _count = 0;
            _head = null;
        }

        /// <summary>
        /// 指示链表中的元素个数
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return GetByIndex(index).Item; }
            set { GetByIndex(index).Item = value; }
        }

        #endregion

        /// <summary>
        /// 查找指定索引处的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Node GetByIndex(int index)
        {
            if ((index < 0) || (index >= this._count))
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            Node tempNode = this._head;
            for (int i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }
            return tempNode;
        }

        /// <summary>
        /// 在链表结尾添加元素
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            Node newNode=new Node(value);
            if(_head==null)
            {
                _head = newNode;//链表为空时直接作为头指针
            }
            else  //否则插入到链表结尾
            {
                GetByIndex(_count - 1).Next = newNode;
            }
            _count++;
        }
        public void Insert(int index, T value)
        {
            Node tempNode;
            if (index == 0) //如果在开始结点处插入
            {
                if(_head==null)
                {
                    _head=new Node(value);
                }
                else
                {
                    tempNode = new Node(value);
                    tempNode.Next = _head;
                    _head = tempNode;
                }
            }
            else
            {
                Node preNode = GetByIndex(index - 1);   //查找插入点的前驱结点
                Node nextNode = preNode.Next;           //插入点的后继结点
                tempNode=new Node(value);               //新结点
                preNode.Next = tempNode;                //前驱结点的后继结点为新结点
                tempNode.Next = nextNode;               //指定新结点的后继结点
            }
            _count++;
        }

        /// <summary>
        /// 删除指定索引处的元素
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if(index==0)    //如果要删除开始结点
            {
                _head = _head.Next;
            }
            else
            {
                Node preNode = GetByIndex(index - 1);   //查找删除结点的前驱结点
                if(preNode.Next==null)
                {
                    throw new ArgumentOutOfRangeException("index","索引超出范围");
                }
                preNode.Next = preNode.Next.Next;       //删除当前索引处的元素
            }
            _count--;
        }

        /// <summary>
        /// 打印整个链表
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "";
            for (Node temp = _head; temp != null; temp = temp.Next)
            {
                s += temp.ToString() + " ";
            }
            return s;
        }

        /// <summary>
        /// 嵌套类，表示单个结点
        /// </summary>
        private class Node
        {
            public Node(T value)
            {
                _item = value;
            }
            private T _item;
            /// <summary>
            /// 数据域
            /// </summary>
            public T Item
            {
                get { return _item; }
                set { _item = value; }
            }
            /// <summary>
            /// 后继结点
            /// </summary>
            public LinkedList<T>.Node Next { get; set; }
            public override string ToString()
            {
                return Item.ToString();
            }
        }

        //迭代器的实现部分
        #region IEnumerator<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
        /// <summary>
        /// 嵌套类，用于实现单向链表的迭代器
        /// </summary>
        public class LinkedListEnumerator:IEnumerator<T>
        {
            private int _index;
            private LinkedList<T> _array;
            public LinkedListEnumerator(LinkedList<T> array)
            {
                _index = -1;
                _array = array;
            }

            #region IEnumerator<T> Members

            /// <summary>
            /// 获取集合中的当前元素
            /// </summary>
            public T Current
            {
                get { return _array[_index]; }
            }
            /// <summary>
            /// 将枚举数推进到集合的下一个元素。如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                return ++_index >= _array.Count ? false : true;
            }
            /// <summary>
            /// 将枚举数设置为其初始位置，该位置位于集合中第一个元素之前
            /// </summary>
            public void Reset()
            {
                _index = -1;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
            /// <summary>
            /// 析构函数
            /// </summary>
            public void Dispose()
            {
                this._array = null;
            }

            #endregion
        }
    }
}

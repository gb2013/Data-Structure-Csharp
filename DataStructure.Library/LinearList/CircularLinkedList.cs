using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure.Library.LinearList
{
    /// <summary>
    /// 单向循环链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularLinkedList<T>:IEnumerable<T>
    {
        private int _count; //记录元素个数
        private Node _tail; //尾指针
        private Node _currentPrev;//使用前驱结点来标识当前结点
        /// <summary>
        /// 向链表结尾添加元素
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            Node newNode=new Node(value);
            if (_tail == null)
            {
                //如果链表为空则新元素既是头指针也是尾指针
                _tail = newNode;
                _tail.Next = newNode;
                _currentPrev = newNode;
            }
            else//否则插入到链表结尾
            {
                newNode.Next = _tail.Next;  //新结点的指针域指向头结点
                _tail.Next = newNode;       //原终端结点指针指向新结点
                if(_currentPrev==_tail)
                {
                    _currentPrev = newNode;
                }
                _tail = newNode;//把尾指针指向新结点
            }
            _count++;
        }
        /// <summary>
        /// 移除当前结点
        /// </summary>
        public void RemoveCurrentNode()
        {
            if(_tail==null)
            {
                throw new NullReferenceException("集合中没有任何元素");
            }
            else if (_count == 1)
            {
                //当前只有一个元素时
                _tail = null;
                _currentPrev = null;
            }
            else
            {
                if (_currentPrev.Next == _tail)
                {
                    //当删除的是尾指针所指向的结点时
                    _tail = _currentPrev;
                }
                _currentPrev.Next = _currentPrev.Next.Next;
            }
            _count--;
        }
        /// <summary>
        /// 让当前结点向前移动指定步数
        /// </summary>
        /// <param name="step"></param>
        public void Move(int step)
        {
            if(step<0)
            {
                throw new ArgumentOutOfRangeException("step", "移动频数不能小于零");
            }
            if(_tail==null)
            {
                //如果为空表
                throw new NullReferenceException("集合中没有任何元素");
            }
            for(int i=0;i<step;i++) //移动指针
            {
                _currentPrev = _currentPrev.Next;
            }
        }

        /// <summary>
        /// 打印整个链表，仅用于测试
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if(_tail==null)
            {
                return string.Empty;
            }
            string s = "";
            Node temp = _tail.Next;
            for(int i=0;i<_count;i++)
            {
                s += temp.ToString() + " ";
                temp = temp.Next;
            }
            return s;
        }

        /// <summary>
        /// 指示链表中的元素个数
        /// </summary>
        public int Count
        {
            get { return _count; }
        }
        /// <summary>
        /// 指示当前结点中的值
        /// </summary>
        public T Current
        {
            get { return _currentPrev.Next.Item; }
        }
        /// <summary>
        /// 嵌套类，表示单个结点
        /// </summary>
        private class Node
        {
            public Node(T value)
            {
                Item = value;
            }

            public T Item { get; set; } //数据域

            public CircularLinkedList<T>.Node Next { get; set; }    //指针，指向后继结点
            public override string ToString()
            {
                return Item.ToString();
            }
        }


        //迭代器的实现部分
        #region IEnumerator<T> Members
        public IEnumerator<T> GetEnumerator()
        {
            return new CircularLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        /// <summary>
        /// 嵌套套，用于实现单向循环链表的迭代器
        /// </summary>
        public class CircularLinkedListEnumerator:IEnumerator<T>
        {
            private int _index;
            private CircularLinkedList<T> _array;
            public CircularLinkedListEnumerator(CircularLinkedList<T> array)
            {
                _index = -1;
                _array = array;
            }

            #region IEnumerator<T> Members
            /// <summary>
            /// 将枚举数推进到集合的下一个元素。如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                return ++_index > _array.Count ? false : true;
            }
            /// <summary>
            /// 将枚举数设置为其初始位置，该位置位于集合中第一个元素之前
            /// </summary>
            public void Reset()
            {
                _index = -1;
            }
            /// <summary>
            /// 获取集合中的当前元素
            /// </summary>
            public T Current
            {
                get { return _array.Current; }
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

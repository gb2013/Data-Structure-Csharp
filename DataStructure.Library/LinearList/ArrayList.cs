/*
 * 出处：数据结构(C#语言描述) 主编：陈广
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure.Library.LinearList
{
    public class ArrayList<T>:IEnumerable<T>
    {
        #region 属性

        ///成员变量
        private const int _defaultCapacity = 4;//默认初始容量
        private T[] _items; //用于存放元素的数组
        private int _size;//指示当前元素个数
        private static readonly T[] emptyArray = new T[0];//元素个数为0时的数组状态

        #endregion

        #region 方法

        public ArrayList()//默认的构造函数
        {
            //避免元素个数为0时访问出错
            this._items = emptyArray;
        }

        /// <summary>
        /// 指定ArrayList初始容量的构造方法
        /// </summary>
        /// <param name="capacity">ArrayList的初始容量</param>
        public ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                //当容量为负数时引发异常
                throw new ArgumentOutOfRangeException("capacity","为ArrayList指定的初始容量不能为负数");
            }
            //按照capacity参数指定的长度的值初始化数组
            this._items=new T[capacity];
        }

        /// <summary>
        /// 添加元素的方法
        /// </summary>
        /// <param name="value">要添加的对象</param>
        /// <returns></returns>
        public virtual int Add(T value)
        {
            //当空间已满时
            if(this._size==this._items.Length)
            {
                this.EnsureCapacity(this._size + 1);
            }
            this._items[this._size] = value;//添加元素
            return this._size++;//总长度加1
        }
        /// <summary>
        /// 动态分配数组空间
        /// </summary>
        /// <param name="min">对象数量</param>
        private void EnsureCapacity(int min)
        {
            if(this._items.Length<min)
            {
                int num = (this._items.Length == 0) ? _defaultCapacity : (this._items.Length*2);
                if(num<min)
                {
                    num = min;
                }
                //调用Capacity的set访问器按照num的值调整数组空间
                this.Capacity = num;
            }
        }
        /// <summary>
        /// 在指定索引处插入指定元素
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public virtual void Insert(int index,T value)
        {
            if(index<0||(index>this._size))     //验证插入位置的有效性(大于0且小于数组长度)
            {
                throw new ArgumentOutOfRangeException("index","索引超出范围！");
            }
            if(this._size==this._items.Length)  //当容量已满时扩容
            {
                this.EnsureCapacity(this._size + 1);
            }
            if(index<this._size)
            {
                //插入点后面的所有元素向后移动一位
                Array.Copy(this._items, index, this._items, index + 1, this._size - index);
            }
            this._items[index] = value; //插入元素
            this._size++;               //使长度加1
        }
        /// <summary>
        /// 移除指定索引处的元素
        /// </summary>
        /// <param name="index">索引值</param>
        public virtual void RemoveAt(int index)
        {
            if((index<0)||(index>=this._size))
            {
                throw new ArgumentOutOfRangeException("index","索引超出范围");
            }
            this._size--;               //长度减1
            if (index < this._size)
            {
                //使被删除元素后的所有元素前移一位
                Array.Copy(this._items, index + 1, this._items, index, this._size - index);
            }
            this._items[this._size] = default(T);   //最后一位空出的元素置空
        }
        /// <summary>
        /// 裁减空间，使存储空间正好等于元素个数
        /// </summary>
        public virtual void TrimToSize()
        {
            this.Capacity = this._size;
        }
        /// <summary>
        /// 数组容量(调整数组容量)
        /// </summary>
        public virtual int Capacity
        {
            get { return this._items.Length; }
            set
            {
                if (value != this._items.Length)
                {
                    if (value < this._size)
                    {
                        throw new ArgumentOutOfRangeException("value", "容量太小");
                    }
                    if (value > 0)
                    {
                        //开辟一片新的内存存储元素
                        T[] destinationArray = new T[value];
                        if (this._size > 0)
                        {
                            //把元素搬迁到新空间内
                            Array.Copy(this._items, 0, destinationArray, 0, this._size);
                        }
                        this._items = destinationArray;
                    }
                    else  //最小空间为_defaultCapacity所指定的数目，这里是4  Note:据以上逻辑看来，此分支的else部分应该永远不会被执行，怀疑冗余(2010-11-07 gb2013注释)
                    {
                        this._items = new T[_defaultCapacity];
                    }
                }
            }
        }
        /// <summary>
        /// 指示当前元素个数
        /// </summary>
        public virtual int Count
        {
            get { return this._size; }
        }
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public virtual T this[int index]
        {
            get
            {
                if((index<0)||(index>=this._size))
                {
                    throw new ArgumentOutOfRangeException("index","索引超出范围");
                }
                return this._items[index];
            }
            set
            {
                if ((index < 0) || (index >= this._size))
                {
                    throw new ArgumentOutOfRangeException("index", "索引超出范围");
                }
                this._items[index] = value;
            }
        }
        /// <summary>
        /// 获取线性表的所有数组元素
        /// </summary>
        public T[] Items
        {
            get { return _items; }
        }
        #endregion

        //迭代器的实现部分
        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        /// <summary>
        /// 嵌套类，用于实现线性表的迭代器
        /// </summary>
        public class ArrayListEnumerator :IEnumerator<T>
        {
            private int _index;
            private ArrayList<T> _array;
            public ArrayListEnumerator(ArrayList<T> array)
            {
                _index = -1;
                _array = array;
            }

            #region IEnumerator<T> Members

            /// <summary>
            /// 获取集合中的当前元素。
            /// </summary>
            public T Current
            {
                get { return _array.Items[_index]; }
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

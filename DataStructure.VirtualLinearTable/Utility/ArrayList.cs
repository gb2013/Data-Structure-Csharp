using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure.VirtualLinearTable.Utility
{
    public class ArrayList
    {
        //成员变量
        private const int _defaultCapacity = 4; //默认初始容量
        private MemoryUnit[] _items;            //用于存放元素的数组
        private int _size;                      //指示当前元素个数
        private Heap _heap;                     //托管堆
        private Color _background;              //背景色
        public ArrayList(Heap heap, Color color)
        {
            this._heap = heap;
            this._background = color;
            //从托管堆中获取指定容量的空间
            this._items = heap.NewUnit(_defaultCapacity, color);
        }

        //添加元素
        public int Add(string value)
        {
            if (this._size == this._items.Length)
            {
                //调整空间为原来的2倍
                this.IncreaseCapacity(_items.Length * 2);
            }
            _items[_size].Value = value;//添加元素
            return _size++;             //使长度加1
        }

        //动态调整内存空间
        private void IncreaseCapacity(int num)
        {
            //获取新空间
            MemoryUnit[] destination = _heap.NewUnit(num, _background);
            for (int i = 0; i < _size; i++)
            {
                //在新空间存放原空间的元素值
                destination[i].Value = _items[i].Value;
                _items[i].SetUnuse(); //原空间变为垃圾
            }
            this._items = destination;//搬家
        }

        //在指定索引处插入元素
        public virtual void Insert(int index, string value)
        {
            if (index < 0 || index > this._size)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            if(_size==_items.Length)
            {
                //当空间满时调整空间
                this.IncreaseCapacity(_items.Length*2);
            }
            //插入点后面的所有元素向后移动一位
            for(int i=_size;i>index;i--)
            {
                _items[i].Value = _items[i - 1].Value;
            }
            _items[index].Value = value;//插入元素
            _size++;//长度加1
        }

        //移除指定索引的元素
        public virtual void RemoveAt(int index)
        {
            if((index<0)||(index>=this._size))
            {
                throw new ArgumentOutOfRangeException("index","索引超出范围");
            }
            this._size--;//长度减1
            for(int i=index;i<_size;i++)
            {
                _items[i].Value = _items[i + 1].Value;
            }
            this._items[_size].Value = "";//最后一位空出的元素置空
        }
    }
}

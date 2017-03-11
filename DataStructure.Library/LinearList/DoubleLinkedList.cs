using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure.Library.LinearList
{
    /// <summary>
    /// 双向链表，此双向链表仅实现int类型操作，未实现泛型操作
    /// </summary>
    public class DoubleLinkedList
    {
        private Node _head;
        #region 方法

        public void Add(int data)
        {
            if(_head==null)
            {
                _head=new Node(data);
            }
            else
            {
                _head = _head.Add(new Node(data));
            }
        }
        public override string ToString()
        {
            if(this._head!=null)
            {
                return this._head.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        private class Node
        {
            #region 属性和构造函数

            private int _data;//结点数据域
            private Node _next;//前驱结点
            private Node _prev;//后继结点
            public int Data
            {
                get { return this._data; }
            }
            public Node(int data)
            {
                this._data = data;
            }

            #endregion

            #region 方法

            public Node Add(Node newNode)
            {
                //下面的“我”表示当前实例
                if(_data>newNode.Data)
                {
                    newNode._next = this;//新结点的后继结点指向我
                    if(this._prev!=null) //如果我的前驱结点不为空
                    {
                        this._prev._next = newNode; //我的前驱结点的后续结点指向新结点
                        newNode._prev = this._prev; //新结点的前驱结点指向我的前驱结点
                    }
                    this._prev = newNode;   //设置我的前驱结点为新结点
                    return newNode;         //如果新结点为头时返回新结点
                }
                else //大于我时则放在我后面
                {
                    //如果我有下一个，则跟下一个进行对比
                    if(this._next!=null)
                    {
                        //这里使用了递归，直到新结点找到比它大的结点为止
                        this._next.Add(newNode);
                    }
                    else//如果我没有下一个结点
                    {
                        this._next = newNode;//设置新结点为我的前驱结点
                        newNode._prev = this;//新结点的后继结点指向我
                    }
                    return this;
                }
            }

            public override string ToString()
            {
                string output = _data.ToString();
                if(_next!=null)
                {
                    //这里使用递归打印链表中的所有元素
                    output += " " + _next.ToString();
                }
                return output;
            }

            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure.VirtualLinearTable.Utility
{
    /// <summary>
    /// 内存单元类
    /// </summary>
    public class MemoryUnit
    {
        private string _value;      //值
        private MemoryUnit next;    //指针域，指向后继结点，仅用于链表
        private Color _background = Color.White;    //背景
        private Color unUseColor = Color.Gray;  //为可回收对象时的背景色
        private SolidBrush bgBrush;     //画背景时用的刷子
        private SolidBrush textBrush;   //画文字时用的刷子
        private Font font = new Font("宋体", 25);   //字体
        private StringFormat format;    //字体格式
        private bool _used = false;     //表示是否为可回收对象
        private Graphics graphic;       //画板
        private Rectangle _drawRect;    //绘制区域

        public MemoryUnit(Graphics gp, Rectangle rect)
        {
            graphic = gp;
            _drawRect = rect;
            bgBrush = new SolidBrush(_background);
            textBrush = new SolidBrush(Color.Black);
            format = new StringFormat();//字体格式
            format.Alignment = StringAlignment.Center;//水平居中
            format.LineAlignment = StringAlignment.Center;//垂直居中
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                ReDraw();//重画
            }
        }

        public Color Background
        {
            get { return _background; }
            set
            {
                _background = value;
                bgBrush.Color = value;
                ReDraw();
            }
        }

        public Rectangle DrawRect
        {
            get { return _drawRect; }
            set { _drawRect = value; }
        }

        public bool Used    //是否可回收
        {
            get { return _used; }
            set { _used = value; }
        }

        public MemoryUnit Next //下一个元素，只用于链表
        {
            get { return next; }
            set { next = value; }
        }

        public void SetUnuse()//变为可回收对象
        {
            _used = false;
            bgBrush.Color = unUseColor;
            ReDraw();
        }

        public void ReDraw()
        {
            //绘制背景和数据
            graphic.FillRectangle(bgBrush,_drawRect);
            graphic.DrawString(_value,font,textBrush,_drawRect,format);
        }
    }
}

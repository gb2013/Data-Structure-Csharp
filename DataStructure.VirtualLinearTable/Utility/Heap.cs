using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure.VirtualLinearTable.Utility
{
    public class Heap
    {
        private const int width = 50;   //单元格宽度
        private const int height = 40;  //单元格高度
        private const int space = 5;    //单元格间距
        private int cols;               //托管堆矩阵列数
        private int rows;               //托管堆矩阵行数
        private Bitmap _bmp;            //托管堆图像
        private Graphics graphic;       //画板
        private MemoryUnit[,] units;    //内存单元集合
        private int count = 0;          //单元个数
        public Heap(int row, int col)
        {
            rows = row;
            cols = col;
            _bmp = new Bitmap(cols * (width + space) + space, rows * (height + space) + space);
            graphic = Graphics.FromImage(_bmp);
            units = new MemoryUnit[rows, cols];
            for (int i = 0; i < units.GetLength(0); i++)
            {
                //初始化内存单元
                for (int j = 0; j < units.GetLength(1); j++)
                {
                    units[i, j] = new MemoryUnit(graphic, new Rectangle(space + j * (width + space), space + i * (height + space), width, height));
                    units[i, j].ReDraw();
                }
            }
        }

        public Bitmap Bmp
        {
            get { return _bmp; }
        }

        //分配指定个数的内存空间并返回
        public MemoryUnit[] NewUnit(int size, Color color)
        {
            if (count + size > cols * rows)
            {
                throw new ArgumentException("空间已满，请运行垃圾回收再执行此操作！");
            }
            MemoryUnit[] arrUnit = new MemoryUnit[size];
            for (int i = 0; i < size; i++)
            {
                //从空白处开始分配
                arrUnit[i] = units[count / cols, count % cols];
                arrUnit[i].Used = true;
                arrUnit[i].Background = color;
                count++;
            }
            return arrUnit;
        }

        public MemoryUnit NewUnit(Color color)//分配单个内存单元
        {
            if (count + 1 > cols * rows)
            {
                throw new ArgumentException("空间已满，请运行垃圾回收再执行此操作！");
            }
            MemoryUnit mu = units[count / cols, count % cols];
            mu.Used = true;
            mu.Background = color;
            count++;
            return mu;
        }

        public void Collect()//垃圾回收
        {
            int pointer = -1;//指示已整理的单元
            for (int i = 0; i < units.Length; i++)
            {
                //首先找到第一个可回收点
                if (pointer == -1 && (!units[i / cols, i % cols].Used))
                {
                    pointer = i;
                }
                if (pointer != -1 && units[i / cols, i % cols].Used)
                {
                    //压缩托管堆，去除可回收对象
                    MemoryUnit left = units[pointer / cols, pointer % cols];
                    MemoryUnit right = units[i / cols, i % cols];
                    Rectangle temp = right.DrawRect;
                    units[i / cols, i % cols] = left;
                    right.DrawRect = left.DrawRect;
                    units[pointer / cols, pointer % cols] = right;
                    left.DrawRect = temp;
                    right.ReDraw();
                    pointer++;
                }
            }
            count = pointer;
            for (int i = pointer; i < units.Length; i++)
            {
                //重新初始化清理出来的剩余空间
                int row = i / cols;
                int col = i % cols;
                units[row, col] = new MemoryUnit(graphic, new Rectangle(space + col * (width + space), space + row * (height + space), width, height));
                units[row, col].ReDraw();
            }
        }
    }
}

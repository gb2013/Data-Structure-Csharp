using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructure.VirtualLinearTable.Utility;

namespace DataStructure.VirtualLinearTable
{
    public partial class MainForm : Form
    {
        private Heap heap;
        private ArrayList arrayLst;
        private LinkedList linkedLst;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            heap=new Heap(6,10);
            pbLst.Image = heap.Bmp;
        }

        private void lblArrayColor_Click(object sender, EventArgs e)
        {
            //选取对象所显示的角色
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                ((Label) sender).BackColor = colorDialog1.Color;
            }
        }

        private void btnNewArr_Click(object sender, EventArgs e)
        {
            //顺序表的初始化
            arrayLst=new ArrayList(heap,lblArrayColor.BackColor);
            pbLst.Refresh();
        }

        private void btnAddArr_Click(object sender, EventArgs e)
        {
            arrayLst.Add(txtArrAddValue.Text);
            pbLst.Refresh();
        }

        private void btnInsertArr_Click(object sender, EventArgs e)
        {
            //顺序表在指定索引处插入指定元素操作
            arrayLst.Insert(int.Parse(txtArrInsertIndex.Text),txtArrInsertValue.Text);
            pbLst.Refresh();
        }

        private void btnDelArr_Click(object sender, EventArgs e)
        {
            //删除顺序表中指定元素的操作
            arrayLst.RemoveAt(int.Parse(txtDelArrIndex.Text));
            pbLst.Refresh();
        }

        private void lblLnkColor_Click(object sender, EventArgs e)
        {
            //选取对象所显示的角色
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((Label)sender).BackColor = colorDialog1.Color;
            }
        }

        private void btnNewLnk_Click(object sender, EventArgs e)
        {
            //链表的初始化
            linkedLst=new LinkedList(heap,lblLnkColor.BackColor);
        }

        private void btnAddLnk_Click(object sender, EventArgs e)
        {
            //链表的添加操作
            linkedLst.Add(txtAddLnkValue.Text);
            pbLst.Refresh();
        }

        private void btnInsertLnk_Click(object sender, EventArgs e)
        {
            //链表插入操作
            linkedLst.Insert(int.Parse(txtLnkInsertIndex.Text),txtLnkInsertValue.Text);
            pbLst.Refresh();
        }

        private void btnDelLnk_Click(object sender, EventArgs e)
        {
            //删除链表指定位置的元素
            linkedLst.RemoveAt(int.Parse(txtLnkDelIndex.Text));
            pbLst.Refresh();
        }
    }
}

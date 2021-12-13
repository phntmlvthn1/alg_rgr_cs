using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;


namespace alg_rgr_cs
{
    public partial class Form1 : Form
    {

        Bitmap bmp = new Bitmap(1800, 800);
        public Bitmap Image { get; internal set; }

        AVL tree = new AVL();
        int rad = 15;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        }
        private void DrawNodes(Node cur)
        {
            Rectangle rect = new Rectangle(cur.x - rad, cur.y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Black, 3);
            Font font = new Font("Arial", 15, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.White);
            g.DrawEllipse(pen, rect);
            g.FillEllipse(brush, cur.x - rad, cur.y - rad, rad * 2, rad * 2);
            g.DrawString((cur.data).ToString(), font, Brushes.Black, cur.x - 15, cur.y - 10);
            if (cur.left != null)
            {
                DrawNodes(cur.left);
            }
            if (cur.right != null)
            {
                DrawNodes(cur.right);
            }

        }
        private void DrawRibs(Node cur)
        {
            Pen p = new Pen(Color.Black, 3);
            Point p1 = new Point(cur.x, cur.y);


            if (cur.left != null)
            {
                Point p2 = new Point(cur.left.x, cur.left.y);
                g.DrawLine(p, p1, p2);
                DrawRibs(cur.left);
            }
            if (cur.right != null)
            {
                Point p2 = new Point(cur.right.x, cur.right.y);
                g.DrawLine(p, p1, p2);
                DrawRibs(cur.right);
            }
        }
        private void Clear()
        {
            Rectangle rect = new Rectangle(0, 0, 2000, 1200);
            Pen pen = new Pen(Color.White, 3);
            g.DrawRectangle(pen, rect);
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillRectangle(brush, rect);

        }
        private void Search(Node n)
        {
            int x = n.x;
            int y = n.y;
            int d = n.data;
            Rectangle rect = new Rectangle(x - rad, y - rad, rad * 2, rad * 2);
            Pen pen = new Pen(Color.Green, 3);
            Font font = new Font("Arial", 15, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Green);
            g.DrawEllipse(pen, rect);
            g.FillEllipse(brush, x - rad, y - rad, rad * 2, rad * 2);
            g.DrawString((d).ToString(), font, Brushes.Black, x - 15, y - 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "Работу выполнил: Чураев Ильдар Разифович \nСтудент группы ПРО-228Б\nЗадание: Напишите программу поиска, включения\nи исключения из сбалансированного поискового\nдерева (вариант 2)\n";
        }

        private void Add_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox1.Text);
            tree.Add(num);
            tree.GetDeep2(tree.root);
            tree.GetDeep(tree.root);
            tree.root.x = 550;
            tree.root.y = 50;
            Clear();
            tree.ForDraw(tree.root);
            DrawRibs(tree.root);
            DrawNodes(tree.root);
        }

        private void Srch_Click(object sender, EventArgs e)
        {
            if (tree.root != null)
            { 
                Clear();
                DrawRibs(tree.root);
                DrawNodes(tree.root);
                int num = Convert.ToInt32(textBox1.Text);
                Node nd = null;
                nd = tree.Find(num, tree.root);
                if (nd != null)
                {
                    Search(nd);
                    label3.Text = "";
                }
                else
                    label3.Text = "элемент отсутствует";
            }
            else label3.Text = "дерево пустое";
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (tree.root != null)
            {
                int num = Convert.ToInt32(textBox1.Text);
                tree.Delete(num);
                tree.GetDeep2(tree.root);
                tree.GetDeep(tree.root);
                if ((tree.root != null))
                {
                    tree.root.x = 550;
                    tree.root.y = 30;
                    Clear();
                    tree.ForDraw(tree.root);
                    DrawRibs(tree.root);
                    DrawNodes(tree.root);
                }
                else
                {
                    Clear();

                }
            }
        }

    }

    public class Node
    {
        public int data, deep;
        public int x, y;
        public Node left;
        public Node right;
        public Node(int data)
        {
            this.data = data;
        }
    }

    public class AVL
    {

        public Node root;
        public AVL()
        {
        }

        public void ForDraw(Node c)
        {

            if (c.left != null)
            {
                if (c.deep > 2)
                    c.left.x = c.x - 275 / (c.deep + 1);
                else if (c.deep == 1)
                    c.left.x = c.x - 275 / (c.deep);
                else if (c.deep == 2)
                    c.left.x = c.x - 275 / (c.deep);
                c.left.y = c.y + 125;
                ForDraw(c.left);
            }
            if (c.right != null)
            {
                if (c.deep > 2)
                    c.right.x = c.x + 275 / (c.deep + 1);
                else if (c.deep == 1)
                    c.right.x = c.x + 275 / (c.deep);
                else if (c.deep == 2)
                    c.right.x = c.x + 275 / (c.deep);
                c.right.y = c.y + 125;
                ForDraw(c.right);
            }
        }
        public void GetDeep(Node n)
        {
            if (root != null)
            { 
            if (n.left != null)
            {
                n.left.deep = n.deep + 1;
                GetDeep(n.left);
            }
            if (n.right != null)
            {
                n.right.deep = n.deep + 1;
                GetDeep(n.right);
            }
            }
        }
        public void GetDeep2(Node n)
        {
            if (root != null)
            {
                n.deep = 1;
                if (n.left != null)
                {
                    GetDeep2(n.left);
                }
                if (n.right != null)
                {
                    GetDeep2(n.right);
                }
            }
        }
        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        private Node balance_tree(Node current)
        {
            int b_factor = BalanceVal(current);
            if (b_factor > 1)
            {
                if (BalanceVal(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (BalanceVal(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Delete(int target)
        {
            root = Delete(root, target);
        }
        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (BalanceVal(current) == -2)
                    {
                        if (BalanceVal(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (BalanceVal(current) == 2)
                    {
                        if (BalanceVal(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (BalanceVal(current) == 2)
                        {
                            if (BalanceVal(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   
                        return current.left;
                    }
                }
            }
            return current;
        }
    
        public Node Find(int target, Node current)
        {
            if (current != null)
            { 
            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }
            }
            else return null;

        }
      
        private int IsMax(int l, int r)
        {
            return l > r ? l : r;
        }
        private int GetHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = GetHeight(current.left);
                int r = GetHeight(current.right);
                int m = IsMax(l, r);
                height = m + 1;
            }
            return height;
        }
        private int BalanceVal(Node current)
        {
            int l = GetHeight(current.left);
            int r = GetHeight(current.right);
            int b_val = l - r;
            return b_val;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }

}

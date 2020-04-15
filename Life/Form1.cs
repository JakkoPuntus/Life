using System;
using System.Drawing;
using System.Windows.Forms;

namespace Life
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] pixels = new int[439, 513];
        int[,] pixelsNext = new int[439, 513];
        Pen myPen = new Pen(Color.White, 0.5F);
        Pen blackPen = new Pen(Color.Black, 0.5F);
        Graphics graphics;
        Graphics g;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                g = Graphics.FromHwnd(pictureBox2.Handle);
                graphics = Graphics.FromHwnd(pictureBox1.Handle);
                graphics.DrawRectangle(myPen, e.X, e.Y, 0.5F, 0.5F);
                pixels[e.X, e.Y] = 1;
            }
            label1.BackColor = panel1.BackColor;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                graphics = Graphics.FromHwnd(pictureBox1.Handle);
                graphics.DrawRectangle(myPen, e.X, e.Y, 0.5F, 0.5F);
                pixels[e.X, e.Y] = 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < 438; x++)
            {
                for (int y = 0; y < 512; y++)
                {
                    pixels[x, y] = 0;
                    pixelsNext[x, y] = 0;
                }
            }
        }


        int countOfNeightbours = 0;
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < 439; x++)
            {
                for (int y = 0; y < 513; y++)
                {
                    pixels[x, y] = 0;
                    pixelsNext[x, y] = 0;
                }
            }
            timer1.Stop();
            timer1.Enabled = false;
            // Create solid brush.
           

            // Fill rectangle to screen.
            graphics.FillRectangle(blackBrush, 0, 0, 329, 513);

        }

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int x = 64; --x >= 1;)
            {
                for (int y = 64; --y >= 1;)
                {
                    countOfNeightbours = 0;
                    if (pixels[x, y] == 1)
                    {
                        if (pixels[x - 1, y + 1] == 1)
                            countOfNeightbours++;

                        if (pixels[x, y + 1] == 1)
                            countOfNeightbours++;
                        if (pixels[x + 1, y + 1] == 1)
                            countOfNeightbours++;

                        if (pixels[x - 1, y] == 1)
                            countOfNeightbours++;
                        if (pixels[x + 1, y] == 1)
                            countOfNeightbours++;

                        if (pixels[x - 1, y - 1] == 1)
                            countOfNeightbours++;
                        if (pixels[x, y - 1] == 1)
                            countOfNeightbours++;
                        if (pixels[x + 1, y - 1] == 1)
                            countOfNeightbours++;


                        if (countOfNeightbours < 2 || countOfNeightbours > 3)
                            pixelsNext[x, y] = 0;
                        else
                            pixelsNext[x, y] = 1;
                    }
                    else if (pixels[x, y] == 0)
                    {
                        
                        if (pixels[x - 1, y + 1] == 1)
                            countOfNeightbours++;

                        if (pixels[x, y + 1] == 1)
                            countOfNeightbours++;
                        if (pixels[x + 1, y + 1] == 1)
                            countOfNeightbours++;

                        if (pixels[x - 1, y] == 1)
                            countOfNeightbours++;
                        if (pixels[x + 1, y] == 1)
                            countOfNeightbours++;

                        if (pixels[x - 1, y - 1] == 1)
                            countOfNeightbours++;
                        if (pixels[x, y - 1] == 1)
                            countOfNeightbours++;
                        if (pixels[x + 1, y - 1] == 1)
                            countOfNeightbours++;

                        if (countOfNeightbours == 3)
                            pixelsNext[x, y] = 1;
                        else
                            pixelsNext[x, y] = 0;
                    }
                }
            }
            RedrawField();
        }

        private void RedrawField()
        {
            for (int x = 64; --x >= 1;)
            {
                for (int y = 64; --y >= 1;)
                {
                    if (pixelsNext[x, y] == 0)
                    {
                        graphics.DrawRectangle(blackPen, x, y, 0.5f, 0.5f);
                        g.FillRectangle(blackBrush, x, y, 4, 4);
                        pixels[x, y] = 0;
                        
                    }
                    else
                    {
                        
                        graphics.DrawRectangle(myPen, x, y, 0.5f, 0.5f);
                        g.FillRectangle(whiteBrush, x, y, 4, 4);
                        pixels[x, y] = 1;
                    }
                }
            }
        }

        Point lastPoint;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Security.Cryptography;

namespace traffic_road
{
    public partial class Form1 : Form
    {
        private string path; //应用程序路径
        private int width = 32;//图片最小单位
        private readonly ArrayList eCars = new ArrayList();
        private readonly int[,] Map = new int[20, 20]; //砖块地图
        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox2.Visible = !pictureBox2.Visible;
            pictureBox3.Visible = !pictureBox3.Visible;
            pictureBox4.Visible = !pictureBox4.Visible;
            pictureBox5.Visible = !pictureBox5.Visible;
            pictureBox6.Visible = pictureBox7.Visible = pictureBox4.Visible;
            pictureBox11.Visible = pictureBox10.Visible = pictureBox5.Visible;
            pictureBox12.Visible = pictureBox13.Visible = pictureBox2.Visible;
            pictureBox12.Visible = pictureBox13.Visible = pictureBox2.Visible;
            pictureBox8.Visible = pictureBox9.Visible = pictureBox3.Visible;

        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Width = 20 * width;
            pictureBox1.Height = 20 * width;
            path = Application.StartupPath;
            //初始化所有的点为可使用的
           for(int i = 0; i !=20; i++)
            {for(int j = 0; j != 20; j++)
                {
                    Map[i,j] = 1;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)//第一个timer_Tick 用来添加汽车
        {
            Random r = new Random();

            //表示一次性产生的车辆个数
            int number = r.Next(0, 1);
            //每次创建一辆车
            for(int i = 0; i <= number; i++)
            {
                //产生0—3的数 方向
                //0表示向右，1向左，2向上，3向下
                int direct = r.Next(0, 4);
                int turn = 0;
                byte[] randomBytes = new byte[4];
                RNGCryptoServiceProvider rngCrypto =
                new RNGCryptoServiceProvider();
                rngCrypto.GetBytes(randomBytes);
                Int32 rngNum = BitConverter.ToInt32(randomBytes, 0);
                int temp = Math.Abs(rngNum % 7);
                int type=1;
                int c=0;
                //五分之一的概率产生特殊车辆
                if (temp == 0)
                {
                    c = 0;
                    type = 2;
                }
                //五分之一的概率左转弯
                if (temp == 1|| temp == 3)
                {
                    turn = 1;c = 1;
                }
                //五分之一的概率右转弯
                if (temp == 2 || temp == 4)
                {
                    turn = 2;c = 1;
                }

                //2种类型，1普通，2特殊
                
                Car car = new Car(type,direct,turn,c);
                eCars.Add(car);//添加进去
                pictureBox1.Invalidate(); //重画游戏面板区域  
            }
        }

        private void timer2_Tick(object sender, EventArgs e)//用来移动汽车
        {
            for(int i = 0; i < eCars.Count; i++)
            {
                Car t =(Car)eCars[i];
                //t.ang
                if (t != null)
                {
                    switch (t.Direct)
                    {
                        case 0: //向右

                            if (t.Left >= 19)
                            {
                                Map[t.Left, t.Top] = 1;
                                //可以消失了
                                eCars.Remove(t);
                                t = null; GC.Collect();
                                continue;
                            }
                            //到达路口,并且为红灯
                            if (t.Left == 6 && pictureBox2.Visible&&t.Type==1&&t.ang!=1)
                            {
                                   Map[t.Left, t.Top] -= 1;
                                
                            }
                            //如果向右方向可以走的话,且没有到达路口
                            else if (Map[t.Left + 1, t.Top] == 1)
                            {
                                if (t.ang != 0&&t.Type!=2)
                                {
                                    if (t.Left == 7 && t.ang == 1)
                                    {
                                        //右转
                                        t.Direct = 2;
                                        t.ang = 0;
                                        continue;
                                    }
                                    if (t.Left == 12 && t.ang == -1)
                                    {
                                        //左转
                                        t.Direct = 3;
                                        t.ang = 0;
                                        continue;
                                    }
                                }
                                t.Left++;//移动
                                Map[t.Left - 1, t.Top] = 1;//V操作
                                Map[t.Left, t.Top] -= 1;//P操作
                            }
                            break;
                        case 1://向左
                            if (t.Left <= 0)
                            {
                                Map[t.Left, t.Top] = 1;
                                //可以消失了
                                eCars.Remove(t);
                                t = null; GC.Collect();
                                continue;
                            }
                            //到达路口,并且为红灯
                            if (t.Left == 13 && pictureBox2.Visible&&t.Type==1 && t.ang != 1)
                            {
                                 Map[t.Left, t.Top] -= 1;
                            }

                            //如果向右方向可以走的话,且没有到达路口
                            else if (Map[t.Left - 1, t.Top] == 1)
                            {
                                if (t.ang != 0 && t.Type != 2)
                                {
                                    if (t.Left == 12 && t.ang == 1)
                                    {
                                        //右转
                                        t.Direct = 3;
                                        t.ang = 0;
                                        continue;
                                    }
                                    if (t.Left == 7 && t.ang == -1)
                                    {
                                        //左转
                                        t.Direct = 2;
                                        t.ang = 0;
                                        continue;
                                    }
                                }
                                t.Left--;//移动
                                Map[t.Left + 1, t.Top] = 1;//V操作
                                Map[t.Left, t.Top] -= 1;//P操作
                            }
                            break;
                        case 2: //向下——(OpenGL型坐标
                            if (t.Top >= 19)
                            {
                                Map[t.Left, t.Top] = 1;
                                //可以消失了
                                eCars.Remove(t);
                                t = null; GC.Collect();
                                continue;
                            }
                            //到达路口,并且为红灯
                            if (t.Top == 6 && !pictureBox2.Visible && t.Type == 1 && t.ang != 1)
                            {
                                 Map[t.Left, t.Top] -= 1;
                            }
                            //如果向下方向可以走的话,且没有到达路口
                            else if (Map[t.Left, t.Top+1] == 1)
                            {
                                if (t.ang != 0 && t.Type != 2)
                                {
                                    if (t.Top == 7 && t.ang == 1)
                                    {
                                        //右转
                                        t.Direct = 1;
                                        t.ang = 0;
                                        continue;
                                    }
                                    if (t.Top == 12 && t.ang == -1)
                                    {
                                        //左转
                                        t.Direct = 0;
                                        t.ang = 0;
                                        continue;
                                    }
                                }

                                t.Top++;//移动
                                Map[t.Left, t.Top-1] = 1;//V操作
                                Map[t.Left, t.Top] -= 1;//P操作
                            }

                            break;

                        case 3:  //向上
                            if (t.Top <= 0)
                            {
                                Map[t.Left, t.Top] = 1;
                                //可以消失了
                                eCars.Remove(t);
                                t = null; GC.Collect();
                                continue;
                            }
                            //到达路口,并且为红灯
                            if (t.Top == 13 && !pictureBox2.Visible && t.Type == 1 && t.ang != 1)
                            {
                                Map[t.Left, t.Top] -= 1;
                            }
                            //如果向下方向可以走的话,且没有到达路口
                            else if (Map[t.Left, t.Top - 1] == 1)
                            {
                                if (t.ang != 0 && t.Type != 2)
                                {
                                    if (t.Top == 12 && t.ang == 1)
                                    {
                                        //右转
                                        t.Direct = 0;
                                        t.ang = 0;
                                        continue;
                                    }
                                    if (t.Top == 7 && t.ang == -1)
                                    {
                                        //左转
                                        t.Direct = 1;
                                        t.ang = 0;
                                        continue;
                                    }
                                }
                                t.Top--;//移动
                                Map[t.Left, t.Top + 1] = 1;//V操作
                                Map[t.Left, t.Top] -= 1;//P操作
                            }

                            break;
                    }

                }
            }
            pictureBox1.Invalidate(); //重画游戏面板区域  

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //每次重新画图在这里实现
            for (int i = 0; i < eCars.Count; i++) 
                if (eCars[i] != null)
                {
                    Car t = (Car)eCars[i];
                    t.Draw(e.Graphics, t.Type);
                }
        }
    }
}

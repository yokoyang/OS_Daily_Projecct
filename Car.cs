using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
namespace traffic_road
{
    class Car
    {
        private readonly int height; //车的高度
        private readonly int width; //车的宽度
        private int direct; //0--右,1--左,2--上,3--下
        private int left; //汽车位置的横坐标
        private int top; //汽车位置的纵坐标
        private int type; //车辆类型，1表示普通车辆，2表示特殊车辆
        public float ang=0;//改变方向时，改变的角度
        private int clor;
        public Car(int car_type,int car_direct,int turn,int color)
        {
            //turn --0，不发生转弯，turn --1 左转  turn ---2 右转
            //初始化的时候，觉得汽车类型，和汽车的方向
            direct = car_direct;
            type = car_type;
            width = 32;
            height = 32;
            clor = color;
            if (turn == 1)
            {
                ang = -1;
            }
            else if (turn == 2)
            {
                ang = 1;
            }

            if (direct == 0)
            {
                left = 0;
                top = 12;
                if (type != 1)
                {
                    top = 10;
                }
            }
            if (direct == 1)
            {
                left = 18;
                top = 7;
                if (type != 1)
                {
                    top = 9;

                }
            }
            if (direct == 2)
            {
                left = 7;
                if (type != 1)
                {
                    left = 9;
                }
                
                top = 0;
            }
            if (direct == 3)
            {
                left = 12;
                if (type != 1)
                {
                    left = 10;
                }
                top = 19;
            }

        }
        public int Top //Top属性
        {
            get { return top; }
            set
            {
                if (top >= 0 && top <= 19)
                {
                    top = value;
                }
            }
        }
        public int Left //Left属性
        {
            get { return left; }
            set
            {
                if (left >= 0 && left <= 19)
                {
                    left = value;
                }
            }
        }
        public int Type //车辆的类型属性
        {
            get { return type; }
            set
            {
                //先暂定为只有2种类型的车
                if (top >= 1 && top <= 2)
                {
                    type = value;
                }
            }
        }
        public int Direct //Direct属性(汽车的方向）
        {
            get { return direct; }
            set { direct = value; }
        }

        public void Draw(Graphics g, int type) //根据车辆类型选择不同图片
        {
            Image carImage = Image.FromFile("BMP/car1.png");
            if (type == 1)
            {
                if (clor == 1)
                {
                    if (direct == 1)
                    {
                        carImage = Image.FromFile("BMP/car1_1.png");
                    }
                    if (direct == 2)
                    {
                        carImage = Image.FromFile("BMP/car1_2.png");
                    }
                    if (direct == 3)
                    {
                        carImage = Image.FromFile("BMP/car1_3.png");
                    }

                }
                else if(clor == 0)
                {
                    if (direct == 0)
                    {
                        carImage = Image.FromFile("BMP/car3-1.png");
                    }
                    if (direct == 1)
                    {
                        carImage = Image.FromFile("BMP/car3-2.png");
                    }
                    if (direct == 2)
                    {
                        carImage = Image.FromFile("BMP/car3-3.png");
                    }
                    if (direct == 3)
                    {
                        carImage = Image.FromFile("BMP/car3-4.png");
                    }
                }
                

                
            }

            if (type == 2)
            {
                if (direct == 0)
                {
                    carImage = Image.FromFile("BMP/car2.png");
                }
                if (direct == 1)
                {
                    carImage = Image.FromFile("BMP/car2_1.png");
                }
                if (direct == 2)
                {
                    carImage = Image.FromFile("BMP/car2_2.png");
                }
                if (direct == 3)
                {
                    carImage = Image.FromFile("BMP/car2_3.png");
                }
            }

            //得到绘制这个汽车图形的在游戏面板中的矩形区域
            Rectangle destRect = new Rectangle(left * width, top * height, width, height);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            g.DrawImage(carImage, destRect, srcRect, GraphicsUnit.Pixel);
        }
    }
}

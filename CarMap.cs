using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_road
{
    class CarMap
    {
        public int[,] CMap; //地图
        public CarMap()
        {
            //初始化地图大小为20 20 
            CMap = new int[20, 20];
        }
    }
}

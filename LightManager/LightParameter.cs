using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightManager
{
    class LightParameter
    {
        public int LightCount;

        public int[] ComportNum;
        public int[] ControllerType;
        public int[] LightChannel;
        public int[] LightValues;

        public LightParameter()
        {
            LightCount = 1;

            ComportNum = new int[1];
            ControllerType = new int[1];
            LightChannel = new int[1];
            LightValues = new int[1];

            ComportNum[0] = 2;
            ControllerType[0] = 0;
            LightChannel[0] = 0;
            LightValues[0] = 128;
        }
    }
}

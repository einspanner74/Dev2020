using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParameterManager
{
    public class AlignResult
    {
        public double AlignMarkDistance = 0;
        public double AlignRotateCenterX = 0;
        public double AlignRotateCenterY = 0;

//        public AlignAxis MarginError = new AlignAxis();
//        public AlignAxis OffsetValue = new AlignAxis();
        public PointD FirstStageOrigin = new PointD();      //First Stage Camera Origin
        public PointD SecondStageOrigin = new PointD();     //First Stage Camera Origin
        public PointD FirstStageOriginTemp = new PointD();  //Second Stage Camera Origin Temp
        public PointD SecondStageOriginTemp = new PointD(); //Second Stage Camera Origin Temp

        public AlignResult()
        {

        }
    }

    public class ResultConditionParmeter
    {
        public AlignResult Align;

        public ResultConditionParmeter()
        {
            Align = new AlignResult();
        }
    }
}

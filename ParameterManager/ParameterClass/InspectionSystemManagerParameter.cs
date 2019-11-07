using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParameterManager
{
    public class InspectionSystemManagerParameter
    {
        public InspectionWindowParameter    InspWndParam;
        
        public object   ProjectItemParam;
        public int      ProjectItem;

        public int      CameraCount;
        public string   CameraType;
        public string   CameraName;
        public string   CameraConfigInfo;

        public int      CameraRotate;
        public bool     IsCameraVerFlip;
        public bool     IsCameraHorFlip;
        
        public double   ImageSizeWidth;
        public double   ImageSizeHeight;

        public double   ResolutionX;
        public double   ResolutionY;
        
        public InspectionSystemManagerParameter()
        {
            InspWndParam = new InspectionWindowParameter();

            CameraCount = 1;
            CameraType = "Dalsa";
            CameraName = "Nano-M2420";
            CameraConfigInfo = null;
            CameraRotate = 0;
            IsCameraVerFlip = false;
            IsCameraHorFlip = false;
            
            ImageSizeWidth = 2464;
            ImageSizeHeight = 2056;

            ResolutionX = 0.005;
            ResolutionY = 0.005;

            ProjectItem = 0;
        }
    }

    public class WindowParameter
    {
        public int LocationX;
        public int LocationY;
        public int Width;
        public int Height;
    }

    public class InspectionWindowParameter : WindowParameter
    {
        public double DisplayZoomValue;
        public double DisplayPanXValue;
        public double DisplayPanYValue;

        public InspectionWindowParameter()
        {
            LocationX = 854;
            LocationY = 153;
            Width = 415;
            Height = 489;

            DisplayZoomValue = 1;
            DisplayPanXValue = 0;
            DisplayPanYValue = 0;
        }
    }
}

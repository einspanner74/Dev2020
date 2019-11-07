using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParameterManager
{
    public class SystemParameter
    {
        public int      MachineNumber;
        public bool     IsProgramUsable;
        public bool     IsSimulationMode;
        public bool     IsTotalRecipe;
        public string[] LastRecipeName = new string[1];
        public int      InspSystemManagerCount;
        public double[] InspSystemManagerResolutionX;
        public double[] InspSystemManagerResolutionY;
        public int      ProjectType;
        public string   IPAddress;
        public int      PortNumber;
        public int      Language;

        public int ResultWindowLocationX;
        public int ResultWindowLocationY;
        public int ResultWindowWidth;
        public int ResultWindowHeight;

        public string[] DataFolderPath = new string[2];

        public SystemParameter()
        {
            MachineNumber = 1;
            IsProgramUsable = false;
            IsSimulationMode = true;

            //LDH, 2019.01.09, LastRecipe 형 변경
            IsTotalRecipe = false;
            LastRecipeName[0] = "Default";
            InspSystemManagerCount = 1;
            ProjectType = 0;

            ResultWindowLocationX = 1482;
            ResultWindowLocationY = 148;
            ResultWindowWidth = 421;
            ResultWindowHeight = 571;

            IPAddress = "192.168.0.100";
            PortNumber = 5050;

            DataFolderPath[0] = "";
            DataFolderPath[1] = "";

            Language = 0;
        }
    }
}

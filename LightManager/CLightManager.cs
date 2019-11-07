using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Collections;
using Microsoft.VisualBasic.FileIO;

using LogMessageManager;

enum eLightControllerType { Normal, JV501, JV504 }
namespace LightManager
{
    public class CLightManager
    {
        private LightParameter LightParam;
        private LightWindow     LightWnd;
        private bool IsSimulationMode;
        private bool IsTotalRecipe;
        public bool IsShowWindow = false;
        public string[] RecipeName;

        // LDH, 각 COM 번호에 해당하는 TotalLight가 생성된 번호를 담는다
        private int[] ComportNum = new int[15];

        private List<object> LightControlList = new List<object>();

        private string LightInformationFolderPath;
        private string LightInformationFilePath;

        private string LightParameterFolderPath;
        private string LightParameterFullPath;

        public CLightManager()
        {
            LightParam = new LightParameter();
            LightWnd = new LightWindow();          
        }

        public void Initialize(string[] _RecipeName, bool _IsTotalRecipe, bool _IsSimulationMode)
        {
            IsSimulationMode = _IsSimulationMode;
            IsTotalRecipe = _IsTotalRecipe;
            RecipeName = _RecipeName;

            LightInformationFolderPath = @"D:\VisionInspectionData\Common\";
            LightInformationFilePath = LightInformationFolderPath + "LightInformation.xml";

            LightParameterFolderPath = @"D:\VisionInspectionData\Common\" + "LightParameter";
            LightParameterFullPath = LightParameterFolderPath + "\\" + _RecipeName[0] + ".sys";

            //LJH 2019.01.25 ADD
            ReadLightInformation();

            ReadLightParameters();

            if (false == IsSimulationMode)
            {
                CheckComportNum();
                SetLightParam();
                LightWnd.SetLightCommandEvent += new LightWindow.SetLightCommandHandler(SetLightCommand);
            }

            SetLightWindow();
        }

        public void DeInitialize()
        {
            LightWnd.SetLightCommandEvent -= new LightWindow.SetLightCommandHandler(SetLightCommand);

            for (int iLoopCount = 0; iLoopCount < LightControlList.Count; iLoopCount++)
            {
                switch ((eLightControllerType)LightParam.ControllerType[iLoopCount])
                {
                    case eLightControllerType.Normal:
                        {
                            var ControlTemp = LightControlList[iLoopCount] as LightController;

                            ControlTemp.SetCommand(LightCommand.LightAllOff);
                            ControlTemp.DeInitialize();
                        }
                        break;

                    case eLightControllerType.JV501:
                        {
                            var ControlTemp = LightControlList[iLoopCount] as JV501Controller;

                            ControlTemp.SetCommand(LightCommand.LightAllOff);
                            ControlTemp.DeInitialize();
                        }
                        break;
                }
            }

            LightWnd.DeInitialize();
        }

        private void SetLightParam()
        {
            for (int iLoopCount = 0; iLoopCount < LightParam.LightCount; iLoopCount++)
            {
                int SelectLight = ComportNum[LightParam.ComportNum[iLoopCount]];

                switch ((eLightControllerType)LightParam.ControllerType[iLoopCount])
                {
                    case eLightControllerType.Normal:
                        {
                            var ControlTemp = LightControlList[SelectLight] as LightController;

                            ControlTemp.SetLightChannel(LightParam.LightChannel[iLoopCount]);
                            ControlTemp.SetLightValue(LightParam.LightValues[iLoopCount]);
                        }
                        break;

                    case eLightControllerType.JV501:
                        {
                            var ControlTemp = LightControlList[SelectLight] as JV501Controller;

                            ControlTemp.SetLightChannel(LightParam.LightChannel[iLoopCount]);
                            ControlTemp.SetLightValue(LightParam.LightValues[iLoopCount]);
                            System.Threading.Thread.Sleep(50);
                            ControlTemp.SetCommand(LightCommand.LightAllOff);
                        }
                        break;
                }
            }
        }

        private void SetLightWindow()
        {
            if((eLightControllerType)LightParam.ControllerType[0] == eLightControllerType.Normal) LightWnd.Initialize(LightParam.LightValues, 2500);
            else LightWnd.Initialize(LightParam.LightValues);
        }

        public void ShowLightWindow()
        {
            LightWnd.TopMost = true;
            IsShowWindow = true;
            LightWnd.Show();
        }

        public void RecipeChange(int _ID, string _RecipeName)
        {
            LightParameterFullPath = LightParameterFolderPath + "\\" + _RecipeName + ".sys";

            //LJH 2019.02.11 추가 함수(ReadLightParameter) 사용
            //ReadLightParameters();
            ReadLightParameter(_ID, _RecipeName);

            //LJH 2019.02.11 Main에서 Setting 하도록 수정
            if (false == IsSimulationMode) SetLightParam();
            SetLightWindow();
        }

        public void RecipeCopy(string _RecipeName, string _SrcRecipeName)
        {
            LightParameterFullPath = LightParameterFolderPath + "\\" + _RecipeName + ".sys";
            if (_SrcRecipeName != "") CopyLightParameter(_RecipeName, _SrcRecipeName);
        }

        private void CheckComportNum()
        {
            List<int> ComportNumList = new List<int>();

            for (int iLoopCount = 0; iLoopCount < LightParam.LightCount; iLoopCount++)
            {
                if (ComportNumList.Count == 0 || !ComportNumList.Contains(LightParam.ComportNum[iLoopCount]))
                {
                    ComportNumList.Add(LightParam.ComportNum[iLoopCount]);

                    switch ((eLightControllerType)LightParam.ControllerType[iLoopCount])
                    {
                        case eLightControllerType.Normal:
                            {
                                LightController NormalControl = new LightController();
                                NormalControl.Initialize("COM" + LightParam.ComportNum[iLoopCount].ToString());

                                LightControlList.Add(NormalControl);
                                ComportNum[LightParam.ComportNum[iLoopCount]] = LightControlList.Count - 1;
                            }
                            break;

                        case eLightControllerType.JV501:
                            {
                                JV501Controller JVControl = new JV501Controller();
                                JVControl.Initialize("COM" + LightParam.ComportNum[iLoopCount].ToString());

                                LightControlList.Add(JVControl);
                                ComportNum[LightParam.ComportNum[iLoopCount]] = LightControlList.Count - 1;
                            }
                            break;
                    }
                }
            }
        }

        private void SetLightCommand(int _LightNum, LightCommand _Command, int _LightValue = 0)
        {
            int SelectLightNum = ComportNum[LightParam.ComportNum[_LightNum]];

            if(_Command == LightCommand.SaveValue)
            {
                LightParam.LightValues[_LightNum] = _LightValue;
                WriteLightParameter();
            }

            try
            {
                switch ((eLightControllerType)LightParam.ControllerType[_LightNum])
                {
                    case eLightControllerType.Normal:
                        {
                            var ControlTemp = LightControlList[SelectLightNum] as LightController;

                        ControlTemp.SetLightChannel(LightParam.LightChannel[_LightNum]);
                        if (_LightValue != 0) ControlTemp.SetLightValue(_LightValue);
                        if (_LightValue != 0) ControlTemp.SetLightValue(LightParam.LightValues);
                        ControlTemp.SetCommand(_Command);
                    }
                    break;
                case eLightControllerType.JV501:
                    {
                        var ControlTemp = LightControlList[SelectLightNum] as JV501Controller;

                            ControlTemp.SetLightChannel(LightParam.LightChannel[_LightNum]);
                            if (_LightValue != 0) ControlTemp.SetLightValue(_LightValue);
                            ControlTemp.SetCommand(_Command);
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("CLightManager : SetLightCommand {0}", _LightNum));
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, ex.ToString());
            }
        }

        #region Read & Write Light Parameter
        private XmlNodeList GetNodeList(string _XmlFilePath)
        {
            XmlNodeList _XmlNodeList = null;

            try
            {
                XmlDocument _XmlDocument = new XmlDocument();
                _XmlDocument.Load(_XmlFilePath);
                XmlElement _XmlRoot = _XmlDocument.DocumentElement;
                _XmlNodeList = _XmlRoot.ChildNodes;
            }

            catch
            {
                _XmlNodeList = null;
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "CLightManager - GetNodeList Exception!!", CLogManager.LOG_LEVEL.LOW);
            }

            return _XmlNodeList;
        }

        #region Read & Write Light Information Parameter
        public bool ReadLightInformation()
        {
            bool _Result = true;

            LightParam = new LightParameter();
            
            try
            {
                DirectoryInfo _DirInfo = new DirectoryInfo(LightInformationFolderPath);
                if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }
                if (false == File.Exists(LightInformationFilePath))
                {
                    File.Create(LightInformationFilePath).Close();
                    WriteLightInformation();
                    System.Threading.Thread.Sleep(100);
                }

                XmlNodeList _XmlNodeList = GetNodeList(LightInformationFilePath);
                if (null == _XmlNodeList) return false;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return false;

                    if ("LightCount" == _Node.Name)         LightParam.LightCount = Convert.ToInt32(_Node.InnerText);
                    else if ("LightOption" == _Node.Name)   GetLightOptionParameter(_Node);
                }
            }

            catch
            {

            }

            return _Result;
        }

        public void GetLightOptionParameter(XmlNode _Nodes)
        {
            if (null == _Nodes) return;

            LightParam.ComportNum = new int[LightParam.LightCount];
            LightParam.ControllerType = new int[LightParam.LightCount];
            LightParam.LightChannel = new int[LightParam.LightCount];
            LightParam.LightValues = new int[LightParam.LightCount];

            for (int iLoopCount = 0; iLoopCount < LightParam.LightCount; ++iLoopCount)
            {
                string _LightName = String.Format("Light{0}", iLoopCount + 1);
                XmlNode _Node = _Nodes[_LightName];
                if (null == _Node) break;

                foreach (XmlNode _NodeChild in _Node.ChildNodes)
                {
                    switch (_NodeChild.Name)
                    {
                        case "ComportNum": LightParam.ComportNum[iLoopCount] = Convert.ToInt32(_NodeChild.InnerText); break;
                        case "Controller": LightParam.ControllerType[iLoopCount] = Convert.ToInt32(_NodeChild.InnerText); break;
                        case "LightChannel": LightParam.LightChannel[iLoopCount] = Convert.ToInt32(_NodeChild.InnerText); break;
                    }
                }
            }
        }

        public void WriteLightInformation()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(LightInformationFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            XmlTextWriter _XmlWriter = new XmlTextWriter(LightInformationFilePath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("LightParameter");
            {
                _XmlWriter.WriteElementString("LightCount", LightParam.LightCount.ToString());
                _XmlWriter.WriteStartElement("LightOption");
                {
                    for (int iLoopCount = 0; iLoopCount < LightParam.LightCount; ++iLoopCount)
                    {
                        _XmlWriter.WriteStartElement(String.Format("Light{0}", iLoopCount + 1));
                        _XmlWriter.WriteElementString("ComportNum", LightParam.ComportNum[iLoopCount].ToString());
                        _XmlWriter.WriteElementString("Controller", LightParam.ControllerType[iLoopCount].ToString());
                        _XmlWriter.WriteElementString("LightChannel", LightParam.LightChannel[iLoopCount].ToString());
                        _XmlWriter.WriteEndElement();
                    }
                }
                _XmlWriter.WriteEndElement();
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion

        #region Read & Write Light Parmeter
        public bool ReadLightParameters()
        {
            bool _Result = true;

            DirectoryInfo _DirInfo = new DirectoryInfo(LightParameterFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            try
            {
                if (true == IsTotalRecipe)
                {
                    if (false == File.Exists(LightParameterFullPath))
                    {
                        File.Create(LightParameterFullPath).Close();
                        WriteLightParameter();
                        System.Threading.Thread.Sleep(100);
                    }

                    XmlNodeList _XmlNodeList = GetNodeList(LightParameterFullPath);
                    if (null == _XmlNodeList) return false;
                    foreach (XmlNode _Node in _XmlNodeList)
                    {
                        if (null == _Node) return false;
                        GetLightValuesParameter(_Node, ref LightParam);
                    }
                }

                else
                {
                    for (int iLoopCount = 0; iLoopCount < LightParam.LightCount; ++iLoopCount)
                    {
                        LightParameterFullPath = LightParameterFolderPath + "\\" + RecipeName[iLoopCount] + ".sys";
                        if (false == File.Exists(LightParameterFullPath))
                        {
                            File.Create(LightParameterFullPath).Close();
                            WriteLightParameter(LightParameterFullPath, iLoopCount);
                            System.Threading.Thread.Sleep(100);
                        }

                        XmlNodeList _XmlNodeList = GetNodeList(LightParameterFullPath);
                        if (null == _XmlNodeList) return false;
                        foreach (XmlNode _Node in _XmlNodeList)
                        {
                            if (null == _Node) return false;
                            GetLightValuesParameter(_Node, ref LightParam, iLoopCount);
                        }
                    }
                }
            }

            catch
            {
                _Result = false;
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ReadLightParameters Exception!!", CLogManager.LOG_LEVEL.LOW);
            }

            return _Result;
        }

        private bool ReadLightParameter(int _ID, string _RecipeName)
        {
            bool _Result = true;
            RecipeName[_ID] = _RecipeName;

            DirectoryInfo _DirInfo = new DirectoryInfo(LightParameterFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            try
            {
                if (true == IsTotalRecipe)
                {
                    if (false == File.Exists(LightParameterFullPath))
                    {
                        File.Create(LightParameterFullPath).Close();
                        WriteLightParameter();
                        System.Threading.Thread.Sleep(100);
                    }

                    XmlNodeList _XmlNodeList = GetNodeList(LightParameterFullPath);
                    if (null == _XmlNodeList) return false;
                    foreach (XmlNode _Node in _XmlNodeList)
                    {
                        if (null == _Node) return false;
                        GetLightValuesParameter(_Node, ref LightParam);
                    }
                }

                else
                {
                    LightParameterFullPath = LightParameterFolderPath + "\\" + RecipeName[_ID] + ".sys";
                    if (false == File.Exists(LightParameterFullPath))
                    {
                        File.Create(LightParameterFullPath).Close();
                        WriteLightParameter(LightParameterFullPath, _ID);
                        System.Threading.Thread.Sleep(100);
                    }

                    XmlNodeList _XmlNodeList = GetNodeList(LightParameterFullPath);
                    if (null == _XmlNodeList) return false;
                    foreach (XmlNode _Node in _XmlNodeList)
                    {
                        if (null == _Node) return false;
                        GetLightValuesParameter(_Node, ref LightParam, _ID);
                    }
                }
            }

            catch
            {

            }

            return _Result;
        }

        private void GetLightValuesParameter(XmlNode _Nodes, ref LightParameter _LightParam, int _Index)
        {
            if (null == _Nodes) return;

            string _NodeName = String.Format("Light{0}", 1);
            XmlNode _Node = _Nodes[_NodeName];

            foreach (XmlNode _NodeChild in _Node.ChildNodes)
            {
                switch (_NodeChild.Name)
                {
                    case "LightValues": _LightParam.LightValues[_Index] = Convert.ToInt32(_NodeChild.InnerText); break;
                }
            }
        }

        private void GetLightValuesParameter(XmlNode _Nodes, ref LightParameter _LightParam)
        {
            if (null == _Nodes) return;

            _LightParam.LightValues = new int[_LightParam.LightCount];

            for (int iLoopCount = 0; iLoopCount < _LightParam.LightCount; iLoopCount++)
            {
                string _NodeName = String.Format("Light{0}", iLoopCount + 1);
                XmlNode _Node = _Nodes[_NodeName];
                if (null == _Node) break;

                foreach (XmlNode _NodeChild in _Node.ChildNodes)
                {
                    switch (_NodeChild.Name)
                    {
                        case "LightValues":  _LightParam.LightValues[iLoopCount] = Convert.ToInt32(_NodeChild.InnerText); break;
                    }
                }
            }
        }

        public void WriteLightParameter()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(LightParameterFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            XmlTextWriter _XmlWriter = new XmlTextWriter(LightParameterFullPath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("LightParameter");
            {
                //_XmlWriter.WriteElementString("LightCount", LightParam.LightCount.ToString());
                _XmlWriter.WriteStartElement("LightOption");
                {
                    for (int iLoopCount = 0; iLoopCount < LightParam.LightCount; ++iLoopCount)
                    {
                        _XmlWriter.WriteStartElement(String.Format("Light{0}", iLoopCount + 1));
                        //_XmlWriter.WriteElementString("ComportNum", LightParam.ComportNum[iLoopCount].ToString());
                        //_XmlWriter.WriteElementString("Controller", LightParam.ControllerType[iLoopCount].ToString());
                        //_XmlWriter.WriteElementString("LightChannel", LightParam.LightChannel[iLoopCount].ToString());
                        _XmlWriter.WriteElementString("LightValues", LightParam.LightValues[iLoopCount].ToString());
                        _XmlWriter.WriteEndElement();
                    }
                }
                _XmlWriter.WriteEndElement();
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }

        public void WriteLightParameter(string _LightParameterFilePath, int _Index)
        {
            XmlTextWriter _XmlWriter = new XmlTextWriter(_LightParameterFilePath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("LightParameter");
            {
                _XmlWriter.WriteStartElement("LightOption");
                {
                    _XmlWriter.WriteStartElement(String.Format("Light{0}", 1));
                    _XmlWriter.WriteElementString("LightValues", LightParam.LightValues[_Index].ToString());
                    _XmlWriter.WriteEndElement();
                }
                _XmlWriter.WriteEndElement();
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion

        public void CopyLightParameter(string _NewRecipeName, string _SrcRecipeName)
        {
            string _NewRecipePath = LightParameterFolderPath + "\\" + _NewRecipeName + ".sys";
            string _SrcRecipePath = LightParameterFolderPath + "\\" + _SrcRecipeName + ".sys";

            FileInfo CopyFile = new FileInfo(_SrcRecipePath);

            if (!CopyFile.Exists) return;

            try
            {
                CopyFile.CopyTo(_NewRecipePath);
            }
            catch
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "CopyLightParameter Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }
        #endregion Read & Write Light Parameter

        public void LightControl(int _LightNum, bool _IsLightOn)
        {
            if (_IsLightOn == true) SetLightCommand(_LightNum, LightCommand.LightOn);
            else                    SetLightCommand(_LightNum, LightCommand.LightOff);
        }

        public void AllLightOff()
        {

        }
    }
}

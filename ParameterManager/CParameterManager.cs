using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;

using LogMessageManager;

namespace ParameterManager
{
    public class CParameterManager
    {
        public SystemParameter                      SystemParam;
        public InspectionParameter[]                InspParam;
        public InspectionSystemManagerParameter[]   InspSysManagerParam;
        public MapDataParameter[]                   InspMapDataParam;
        public ResultConditionParmeter              ResultConditionParam;

        public static eSysMode  SystemMode;
        public static eSysMode  SystemModeBackup;
        public static eLanguage Language;

        private string ProjectName;
        private string InspectionDefaultPath;
        private string ISMParameterFullPath;
        private string ProjectItemParameterFullPath;
        private string RecipeParameterPath;
        private string SystemParameterFullPath;

        private int AlgoNumber = 1;
        private int AreaNumber = 1;

        #region Initialize & DeInitialize
        public CParameterManager()
        {
            SystemMode = eSysMode.MANUAL_MODE;
            SystemParam = new SystemParameter();
            ResultConditionParam = new ResultConditionParmeter();

            ProjectName             = "KPINTVisionInspection";
            InspectionDefaultPath   = @"D:\VisionInspectionData\" + ProjectName + @"\";
            ISMParameterFullPath    = @"D:\VisionInspectionData\" + ProjectName + @"\RecipeParameter\Default\ISMParameter1.Sys";

            RecipeParameterPath     = InspectionDefaultPath + @"RecipeParameter\";
            SystemParameterFullPath = InspectionDefaultPath + @"SystemParameter.Sys";
        }

        public bool Initialize(string _ProjectName)
        {
            bool _Result = false;

            ProjectName             = _ProjectName;
            InspectionDefaultPath   = @"D:\VisionInspectionData\" + ProjectName + @"\";
            ISMParameterFullPath    = @"D:\VisionInspectionData\" + ProjectName + @"\RecipeParameter\Default\ISMParameter1.Sys";

            RecipeParameterPath     = InspectionDefaultPath + @"RecipeParameter\";
            SystemParameterFullPath = InspectionDefaultPath + @"SystemParameter.Sys";

            do
            {
                if (false == ReadSystemParameter()) break;
                if (false == ReadISMParameters())   break;
                if (false == ReadProjectItemParameters()) break;
                if (false == ReadInspectionParameters()) break;
                SystemParam.IsProgramUsable = true;

                _Result = true;
            } while (false);

            return _Result;
        }

        public bool RecipeReload(int _ID, string _RecipeName)
        {
            bool _Result = true;
            if (_RecipeName == "") return false;

            //LDH, 통합(통신)으로 recipe 변경시 -1
            if (_ID == -1)
            {
                for (int iLoopCount = 0; iLoopCount < SystemParam.LastRecipeName.Count(); iLoopCount++)
                {
                    SystemParam.LastRecipeName[iLoopCount] = _RecipeName;
                }
                ReadInspectionParameters();
            }
            else
            {
                SystemParam.LastRecipeName[_ID] = _RecipeName;

                InspParam[_ID] = new InspectionParameter();
                if (false == ReadInspectionParameter(_ID, _RecipeName)) { _Result = false; return _Result; }
                if (false == ReadInspectionMapDataParameter(_ID, _RecipeName)) { _Result = false; return _Result; }
            }

            return _Result;
        }

        public void DeInitialize()
        {
            WriteSystemParameter();

            for (int iLoopCount = 0; iLoopCount < SystemParam.InspSystemManagerCount; ++iLoopCount)
                WriteISMParameter(iLoopCount);

            for (int iLoopCount = 0; iLoopCount < SystemParam.InspSystemManagerCount; ++iLoopCount)
                WriteProjectItemParameter(iLoopCount);
        }
        #endregion Initialize & DeInitialize

        #region Read & Write SystemParameter
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

            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "GetNodeList Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _XmlNodeList = null;
            }

            return _XmlNodeList;
        }

        public bool ReadSystemParameter()
        {
            bool _Result = true;

            try
            {
                DirectoryInfo _DirInfo = new DirectoryInfo(InspectionDefaultPath);
                if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }
                if (false == File.Exists(SystemParameterFullPath))
                {
                    File.Create(SystemParameterFullPath).Close();
                    WriteSystemParameter();
                    System.Threading.Thread.Sleep(100);
                }

                XmlNodeList _XmlNodeList = GetNodeList(SystemParameterFullPath);
                if (null == _XmlNodeList) return false;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return false;
                    switch (_Node.Name)
                    {
                        case "MachineID":        SystemParam.MachineNumber = Convert.ToInt32(_Node.InnerText); break;
                        case "SimulationMode":   SystemParam.IsSimulationMode = Convert.ToBoolean(_Node.InnerText); break;
                        case "TotalRecipe":      SystemParam.IsTotalRecipe = Convert.ToBoolean(_Node.InnerText); break;
                        case "LastRecipeName":   GetLastRecipeName(_Node); break;
                        case "ISMModuleCount":   SystemParam.InspSystemManagerCount = Convert.ToInt32(_Node.InnerText); break;
                        case "ISMModuleRes":     GetISMModuleResolution(_Node); break;
                        case "ResultWndLocateX": SystemParam.ResultWindowLocationX = Convert.ToInt32(_Node.InnerText); break;
                        case "ResultWndLocateY": SystemParam.ResultWindowLocationY = Convert.ToInt32(_Node.InnerText); break;
                        case "ResultWndWidth":   SystemParam.ResultWindowWidth = Convert.ToInt32(_Node.InnerText); break;
                        case "ResultWndHeight":  SystemParam.ResultWindowHeight = Convert.ToInt32(_Node.InnerText); break;
                        case "ProjectType":      SystemParam.ProjectType = Convert.ToInt32(_Node.InnerText); break;
                        case "IPAddress":        SystemParam.IPAddress = _Node.InnerText; break;
                        case "PortNumber":       SystemParam.PortNumber = Convert.ToInt32(_Node.InnerText); break;
                        case "Language":         SystemParam.Language = Convert.ToInt32(_Node.InnerText); break;
                        case "DataFolderPath0":  SystemParam.DataFolderPath[0] = _Node.InnerText; break;
                        case "DataFolderPath1":  SystemParam.DataFolderPath[1] = _Node.InnerText; break;
                    }

                    if (_Node.Name == "ISMModuleCount")
                    {
                        SystemParam.LastRecipeName = new string[SystemParam.InspSystemManagerCount];
                        SystemParam.InspSystemManagerResolutionX = new double[SystemParam.InspSystemManagerCount];
                        SystemParam.InspSystemManagerResolutionY = new double[SystemParam.InspSystemManagerCount];
                    }
                }
            }

            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ReadSystemParameter Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        private void GetLastRecipeName(XmlNode _Nodes)
        {
            if (null == _Nodes) return;

            int _RecipeCount = 0;
            foreach (XmlNode _NodeChild in _Nodes.ChildNodes)
            {
                SystemParam.LastRecipeName[_RecipeCount] = _NodeChild.InnerText;
                _RecipeCount++;
            }
        }

        private void GetISMModuleResolution(XmlNode _Nodes)
        {
            int _Cnt = 1;
            if (null == _Nodes) return;
            if (0 == SystemParam.InspSystemManagerCount) return;

            SystemParam.InspSystemManagerResolutionX = new double[SystemParam.InspSystemManagerCount];
            SystemParam.InspSystemManagerResolutionY = new double[SystemParam.InspSystemManagerCount];
            foreach (XmlNode _NodeChild in _Nodes.ChildNodes)
            {
                if (null == _NodeChild) return;

                string _ModuleName = string.Format("ISMModule{0}", _Cnt);
                if (_NodeChild.Name == _ModuleName)
                {
                    foreach (XmlNode _Node in _NodeChild)
                    {
                        if (null == _Node) return;
                        switch (_Node.Name)
                        {
                            case "ResolutionX": SystemParam.InspSystemManagerResolutionX[_Cnt - 1] = Convert.ToDouble(_Node.InnerText); break;
                            case "ResolutionY": SystemParam.InspSystemManagerResolutionY[_Cnt - 1] = Convert.ToDouble(_Node.InnerText); break;
                        }
                        
                    }
                    _Cnt++;
                }
            }
        }

        public void WriteSystemParameter()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(InspectionDefaultPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            #region XML Element Define
            XElement _SystemParameter        = new XElement("SystemParameter");
            XElement _MachineID              = new XElement("MachineID", SystemParam.MachineNumber.ToString());
            XElement _SimulationMode         = new XElement("SimulationMode", SystemParam.IsSimulationMode.ToString());
            XElement _TotalRecipe            = new XElement("TotalRecipe", SystemParam.IsTotalRecipe.ToString());
            XElement _InspSystemManagerCount = new XElement("ISMModuleCount", SystemParam.InspSystemManagerCount.ToString());
            XElement _InspSystemManagerRes   = new XElement("ISMModuleRes");
            XElement _ResultWndLocateX       = new XElement("ResultWndLocateX", SystemParam.ResultWindowLocationX.ToString());
            XElement _ResultWndLocateY       = new XElement("ResultWndLocateY", SystemParam.ResultWindowLocationY.ToString());
            XElement _ResultWndWidth         = new XElement("ResultWndWidth", SystemParam.ResultWindowWidth.ToString());
            XElement _ResultWndHeight        = new XElement("ResultWndHeight", SystemParam.ResultWindowHeight.ToString());
            XElement _ProjectType            = new XElement("ProjectType", SystemParam.ProjectType.ToString());
            XElement _IPAddress              = new XElement("IPAddress", SystemParam.IPAddress);
            XElement _PortNumber             = new XElement("PortNumber", SystemParam.PortNumber.ToString());
            XElement _Language               = new XElement("Language", SystemParam.Language.ToString());
            XElement _DataFolderPath0        = new XElement("DataFolderPath0", SystemParam.DataFolderPath[0]);
            XElement _DataFolderPath1        = new XElement("DataFolderPath1", SystemParam.DataFolderPath[1]);

            XElement _LastRecipeName         = new XElement("LastRecipeName");
            XElement[] _RecipeName           = new XElement[SystemParam.InspSystemManagerCount];
            //XElement[] _ISMModuleRes         = new XElement[SystemParam.InspSystemManagerCount];
            #endregion XML Element Define

            #region XML Tree ADD
            for (int iLoopCount = 0; iLoopCount< SystemParam.InspSystemManagerCount; ++iLoopCount)
            {
                XElement _ISMModuleRes = new XElement(string.Format("ISMModule{0}", iLoopCount + 1));
                XElement _ResolutionX = new XElement("ResolutionX", SystemParam.InspSystemManagerResolutionX[iLoopCount].ToString());
                XElement _ResolutionY = new XElement("ResolutionY", SystemParam.InspSystemManagerResolutionY[iLoopCount].ToString());
                _ISMModuleRes.Add(_ResolutionX);
                _ISMModuleRes.Add(_ResolutionY);
                _InspSystemManagerRes.Add(_ISMModuleRes);

                _RecipeName[iLoopCount] = new XElement("LastRecipeName" + iLoopCount, SystemParam.LastRecipeName[iLoopCount]);
                _LastRecipeName.Add(_RecipeName[iLoopCount]);
            }

            _SystemParameter.Add(_MachineID);
            _SystemParameter.Add(_SimulationMode);
            _SystemParameter.Add(_TotalRecipe);
            _SystemParameter.Add(_InspSystemManagerCount);
            _SystemParameter.Add(_InspSystemManagerRes);
            _SystemParameter.Add(_ResultWndLocateX);
            _SystemParameter.Add(_ResultWndLocateY);
            _SystemParameter.Add(_ResultWndWidth);
            _SystemParameter.Add(_ResultWndHeight);
            _SystemParameter.Add(_ProjectType);
            _SystemParameter.Add(_IPAddress);
            _SystemParameter.Add(_PortNumber);
            _SystemParameter.Add(_Language);
            _SystemParameter.Add(_DataFolderPath0);
            _SystemParameter.Add(_DataFolderPath1);
            _SystemParameter.Add(_LastRecipeName);
            _SystemParameter.Save(SystemParameterFullPath);
            #endregion XML Tree ADD
        }
        #endregion Read & Write SystemParameter

        #region Read & Write Inspection System Manager
        public bool ReadISMParameters()
        {
            bool _Result = true;
            int ISMCount = 0;

            InspSysManagerParam = new InspectionSystemManagerParameter[SystemParam.InspSystemManagerCount];
            for (int iLoopCount = 0; iLoopCount < SystemParam.InspSystemManagerCount; ++iLoopCount)
            {
                InspSysManagerParam[iLoopCount] = new InspectionSystemManagerParameter();

                ISMCount = iLoopCount;
                if (SystemParam.IsTotalRecipe == false) ISMCount = 0;
                if (false == ReadISMParameter(iLoopCount, ISMCount)) { _Result = false; break; }
            }

            return _Result;
        }

        public bool ReadISMParameter(int _ID, int ISMCount)
        {   
            bool _Result = true;

            try
            {
                ISMParameterFullPath = String.Format(@"{0}{1}\ISMParameter{2}.Sys", RecipeParameterPath, SystemParam.LastRecipeName[_ID], (ISMCount + 1));

                DirectoryInfo _DirInfo = new DirectoryInfo(RecipeParameterPath + SystemParam.LastRecipeName[_ID]);
                if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }
                if (false == File.Exists(ISMParameterFullPath))
                {
                    File.Create(ISMParameterFullPath).Close();
                    WriteISMParameter(_ID);
                    System.Threading.Thread.Sleep(100);
                }

                XmlNodeList _XmlNodeList = GetNodeList(ISMParameterFullPath);
                if (null == _XmlNodeList) return false;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return false;
                    switch (_Node.Name)
                    {
                        case "ProjectItem":      InspSysManagerParam[_ID].ProjectItem = Convert.ToInt32(_Node.InnerText); break;
                        case "CameraCount":      InspSysManagerParam[_ID].CameraCount = Convert.ToInt32(_Node.InnerText); break;
                        case "CameraType":       InspSysManagerParam[_ID].CameraType = _Node.InnerText; break;
                        case "CameraName":       InspSysManagerParam[_ID].CameraName = _Node.InnerText; break;
                        case "CameraConfigInfo": InspSysManagerParam[_ID].CameraConfigInfo = _Node.InnerText; break;
                        case "CameraRotate":     InspSysManagerParam[_ID].CameraRotate = Convert.ToInt32(_Node.InnerText); break;
                        case "CameraVerFlip":    InspSysManagerParam[_ID].IsCameraVerFlip = Convert.ToBoolean(_Node.InnerText); break;
                        case "CameraHorFlip":    InspSysManagerParam[_ID].IsCameraHorFlip = Convert.ToBoolean(_Node.InnerText); break;
                        case "ImageSizeWidth":   InspSysManagerParam[_ID].ImageSizeWidth = Convert.ToInt32(_Node.InnerText); break;
                        case "ImageSizeHeight":  InspSysManagerParam[_ID].ImageSizeHeight = Convert.ToInt32(_Node.InnerText); break;
                        case "ResolutionX":      InspSysManagerParam[_ID].ResolutionX = Convert.ToDouble(_Node.InnerText); break;
                        case "ResolutionY":      InspSysManagerParam[_ID].ResolutionY = Convert.ToDouble(_Node.InnerText); break;
                        case "InspWindowZoom":      InspSysManagerParam[_ID].InspWndParam.DisplayZoomValue = Convert.ToDouble(_Node.InnerText); break;
                        case "InspWindowPanX":      InspSysManagerParam[_ID].InspWndParam.DisplayPanXValue = Convert.ToDouble(_Node.InnerText); break;
                        case "InspWindowPanY":      InspSysManagerParam[_ID].InspWndParam.DisplayPanYValue = Convert.ToDouble(_Node.InnerText); break;
                        case "InspWindowLocationX": InspSysManagerParam[_ID].InspWndParam.LocationX = Convert.ToInt32(_Node.InnerText); break;
                        case "InspWindowLocationY": InspSysManagerParam[_ID].InspWndParam.LocationY = Convert.ToInt32(_Node.InnerText); break;
                        case "InspWindowWidth":     InspSysManagerParam[_ID].InspWndParam.Width = Convert.ToInt32(_Node.InnerText); break;
                        case "InspWindowHeight":    InspSysManagerParam[_ID].InspWndParam.Height = Convert.ToInt32(_Node.InnerText); break;
                    }
                }
            }

            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ReadISMParameter Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        public void WriteISMParameter(int _ID, string _RecipeName = null)
        {
            //LDH, 2019.01.15, 별도 Recipe 사용을 위해 ISMCount 따로 설정
            int ISMCount = 0;
            if (SystemParam.IsTotalRecipe) ISMCount = _ID;

            if (null == _RecipeName) _RecipeName = SystemParam.LastRecipeName[_ID];
            DirectoryInfo _DirInfo = new DirectoryInfo(RecipeParameterPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            ISMParameterFullPath = String.Format(@"{0}{1}\ISMParameter{2}.Sys", RecipeParameterPath, SystemParam.LastRecipeName[_ID], (ISMCount + 1));
            XmlTextWriter _XmlWriter = new XmlTextWriter(ISMParameterFullPath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("ISMParameter");
            {
                _XmlWriter.WriteElementString("ProjectItem", InspSysManagerParam[_ID].ProjectItem.ToString());
                _XmlWriter.WriteElementString("CameraCount", InspSysManagerParam[_ID].CameraCount.ToString());
                _XmlWriter.WriteElementString("CameraType", InspSysManagerParam[_ID].CameraType.ToString());
                _XmlWriter.WriteElementString("CameraName", InspSysManagerParam[_ID].CameraName);
                _XmlWriter.WriteElementString("CameraConfigInfo", InspSysManagerParam[_ID].CameraConfigInfo);
                _XmlWriter.WriteElementString("CameraRotate", InspSysManagerParam[_ID].CameraRotate.ToString());
                _XmlWriter.WriteElementString("CameraVerFlip", InspSysManagerParam[_ID].IsCameraVerFlip.ToString());
                _XmlWriter.WriteElementString("CameraHorFlip", InspSysManagerParam[_ID].IsCameraHorFlip.ToString());
                _XmlWriter.WriteElementString("ImageSizeWidth", InspSysManagerParam[_ID].ImageSizeWidth.ToString());
                _XmlWriter.WriteElementString("ImageSizeHeight", InspSysManagerParam[_ID].ImageSizeHeight.ToString());
                _XmlWriter.WriteElementString("ResolutionX", InspSysManagerParam[_ID].ResolutionX.ToString());
                _XmlWriter.WriteElementString("ResolutionY", InspSysManagerParam[_ID].ResolutionY.ToString());
                _XmlWriter.WriteElementString("InspWindowZoom", InspSysManagerParam[_ID].InspWndParam.DisplayZoomValue.ToString());
                _XmlWriter.WriteElementString("InspWindowPanX", InspSysManagerParam[_ID].InspWndParam.DisplayPanXValue.ToString());
                _XmlWriter.WriteElementString("InspWindowPanY", InspSysManagerParam[_ID].InspWndParam.DisplayPanYValue.ToString());
                _XmlWriter.WriteElementString("InspWindowLocationX", InspSysManagerParam[_ID].InspWndParam.LocationX.ToString());
                _XmlWriter.WriteElementString("InspWindowLocationY", InspSysManagerParam[_ID].InspWndParam.LocationY.ToString());
                _XmlWriter.WriteElementString("InspWindowWidth", InspSysManagerParam[_ID].InspWndParam.Width.ToString());
                _XmlWriter.WriteElementString("InspWindowHeight", InspSysManagerParam[_ID].InspWndParam.Height.ToString());
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion Read & Write Inspection System Manager

        #region Read & Write Project Item Parameter
        public bool ReadProjectItemParameters()
        {
            bool _Result = true;

            for (int iLoopCount = 0; iLoopCount < InspSysManagerParam.Length; ++iLoopCount)
            {
                if (false == ReadProjectItemParameter(iLoopCount)) { _Result = false; break; }
            }

            return _Result;
        }

        /// <summary>
        /// 프로젝트 아이템 별로 별도의 조건을 가져가는 경우 (Align 정밀도 등)
        /// </summary>
        /// <param name="_ID"></param>
        /// <returns></returns>
        public bool ReadProjectItemParameter(int _ID)
        {
            bool _Result = true;

            //LDH, 2019.01.15, 별도 Recipe 사용을 위해 ISMCount 따로 설정
            int ISMCount = 0;
            if (SystemParam.IsTotalRecipe) ISMCount = _ID;

            try
            {
                ProjectItemParameterFullPath = String.Format(@"{0}{1}\ProjectItemParameter.Sys", RecipeParameterPath, SystemParam.LastRecipeName[_ID], (ISMCount + 1));

                DirectoryInfo _DirInfo = new DirectoryInfo(RecipeParameterPath + SystemParam.LastRecipeName[_ID]);
                if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }
                if (false == File.Exists(ProjectItemParameterFullPath))
                {
                    File.Create(ProjectItemParameterFullPath).Close();
                    WriteProjectItemParameter(_ID);
                    System.Threading.Thread.Sleep(100);
                }

                XmlNodeList _XmlNodeList = GetNodeList(ISMParameterFullPath);
                if (null == _XmlNodeList) return false;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return false;
                    switch (_Node.Name)
                    {
                        //LDH, 2019.10.29, LAT Align 아이템은 ResultConditionAlign 창으로 이동
                    }
                }
            }

            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ReadProjectItemParameter Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        /// <summary>
        /// 프로젝트 아이템 별로 별도의 조건을 가져가는 경우 (Align 정밀도 등)
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_RecipeName"></param>
        public void WriteProjectItemParameter(int _ID, string _RecipeName = null)
        {
            //LDH, 2019.01.15, 별도 Recipe 사용을 위해 ISMCount 따로 설정
            int ISMCount = 0;
            if (SystemParam.IsTotalRecipe) ISMCount = _ID;

            if (null == _RecipeName) _RecipeName = SystemParam.LastRecipeName[_ID];
            DirectoryInfo _DirInfo = new DirectoryInfo(RecipeParameterPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            ProjectItemParameterFullPath = String.Format(@"{0}{1}\ProjectItemParameter.Sys", RecipeParameterPath, SystemParam.LastRecipeName[_ID], (ISMCount + 1));
            XmlTextWriter _XmlWriter = new XmlTextWriter(ProjectItemParameterFullPath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("ProjectItemParameter");
            {
                //#region AlignResult Parameter
                //_XmlWriter.WriteElementString("AlignMarkDistance", ResultConditionParam.Align.AlignMarkDistance.ToString());
                //_XmlWriter.WriteElementString("AlignRotateCenterX", ResultConditionParam.Align.AlignRotateCenterX.ToString());
                //_XmlWriter.WriteElementString("AlignRotateCenterY", ResultConditionParam.Align.AlignRotateCenterY.ToString());
                //_XmlWriter.WriteElementString("MarginErrorX", ResultConditionParam.Align.MarginError.X.ToString());
                //_XmlWriter.WriteElementString("MarginErrorY", ResultConditionParam.Align.MarginError.Y.ToString());
                //_XmlWriter.WriteElementString("MarginErrorT", ResultConditionParam.Align.MarginError.T.ToString());
                //_XmlWriter.WriteElementString("MarginErrorU", ResultConditionParam.Align.MarginError.U.ToString());
                //_XmlWriter.WriteElementString("MarginErrorV", ResultConditionParam.Align.MarginError.V.ToString());
                //_XmlWriter.WriteElementString("MarginErrorW", ResultConditionParam.Align.MarginError.W.ToString());
                //_XmlWriter.WriteElementString("OffsetValueX", ResultConditionParam.Align.OffsetValue.X.ToString());
                //_XmlWriter.WriteElementString("OffsetValueY", ResultConditionParam.Align.OffsetValue.Y.ToString());
                //_XmlWriter.WriteElementString("OffsetValueT", ResultConditionParam.Align.OffsetValue.T.ToString());
                //_XmlWriter.WriteElementString("OffsetValueU", ResultConditionParam.Align.OffsetValue.U.ToString());
                //_XmlWriter.WriteElementString("OffsetValueV", ResultConditionParam.Align.OffsetValue.V.ToString());
                //_XmlWriter.WriteElementString("OffsetValueW", ResultConditionParam.Align.OffsetValue.W.ToString());
                //_XmlWriter.WriteElementString("FirstStageOriginX", ResultConditionParam.Align.FirstStageOrigin.X.ToString());
                //_XmlWriter.WriteElementString("FirstStageOriginY", ResultConditionParam.Align.FirstStageOrigin.Y.ToString());
                //_XmlWriter.WriteElementString("SecondStageOriginX", ResultConditionParam.Align.SecondStageOrigin.X.ToString());
                //_XmlWriter.WriteElementString("SecondStageOriginY", ResultConditionParam.Align.SecondStageOrigin.Y.ToString());
                //_XmlWriter.WriteElementString("FirstStageOriginTempX", ResultConditionParam.Align.FirstStageOriginTemp.X.ToString());
                //_XmlWriter.WriteElementString("FirstStageOriginTempY", ResultConditionParam.Align.FirstStageOriginTemp.Y.ToString());
                //_XmlWriter.WriteElementString("SecondStageOriginTempX", ResultConditionParam.Align.SecondStageOriginTemp.X.ToString());
                //_XmlWriter.WriteElementString("SecondStageOriginTempY", ResultConditionParam.Align.SecondStageOriginTemp.Y.ToString());
                //#endregion
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion Read & Write Project Item Parameter

        #region Read & Write InspectionParameter / Map Data Parameter
        public bool ReadInspectionParameters(bool _InitializeFlag = false)
        {
            bool _Result = true;

            InspParam = new InspectionParameter[SystemParam.InspSystemManagerCount];
            InspMapDataParam = new MapDataParameter[SystemParam.InspSystemManagerCount];

            for (int iLoopCount = 0; iLoopCount < SystemParam.InspSystemManagerCount; ++iLoopCount)
            {
                InspParam[iLoopCount] = new InspectionParameter();
                InspMapDataParam[iLoopCount] = new MapDataParameter();
                if (false == ReadInspectionParameter(iLoopCount, SystemParam.LastRecipeName[iLoopCount]))           { _Result = false; break; }
                if (false == ReadInspectionMapDataParameter(iLoopCount, SystemParam.LastRecipeName[iLoopCount]))    { _Result = false; break; }
            }

            return _Result;
        }

        #region Read & Write InspectionParameter
        private bool ReadInspectionParameter(int _ID, string _RecipeName = null)
        {
            bool _Result = true;

            //LDH, 2019.01.15, 별도 Recipe 사용을 위해 ISMCount 따로 설정
            int ISMCount = 0;
            if (SystemParam.IsTotalRecipe) ISMCount = _ID;

            if (null == _RecipeName) return false;
            string _RecipeParameterPath = InspectionDefaultPath + @"RecipeParameter\" + _RecipeName + @"\Module" + (ISMCount + 1);
            DirectoryInfo _DirInfo = new DirectoryInfo(_RecipeParameterPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _InspParamFullPath = _RecipeParameterPath + @"\InspectionCondition.Rcp";
            if (false == File.Exists(_InspParamFullPath))
            {
                File.Create(_InspParamFullPath).Close();
                WriteInspectionParameter(_ID, _InspParamFullPath);
                System.Threading.Thread.Sleep(100);
            }

            AreaNumber = AlgoNumber = 1;
            XmlNodeList _XmlNodeList = GetNodeList(_InspParamFullPath);
            if (null == _XmlNodeList) return false;

            foreach (XmlNode _Node in _XmlNodeList)
            {
                InspectionAreaParameter _InspAreaParamTemp = new InspectionAreaParameter();
                MapDataParameter _MapDataParamTemp = new MapDataParameter();

                if (null == _Node) return false;
                if (true == GetInspectionParameterConfig(_Node, ref InspParam[_ID]))    {   continue;   }
                //GetInspectionMapDataParameter(_Node, ref _MapDataParamTemp);
                GetInspectionParameterRegion(_Node, ref _InspAreaParamTemp);
                GetInspectionParameterAlgorithm(_Node, ref _InspAreaParamTemp);

                if (-1 == _InspAreaParamTemp.BaseIndexNumber)
                    _InspAreaParamTemp.BaseIndexNumber = InspParam[_ID].InspAreaParam.Count;
                InspParam[_ID].InspAreaParam.Add(_InspAreaParamTemp);
            }

            return _Result;
        }

        private bool GetInspectionParameterConfig(XmlNode _Nodes, ref InspectionParameter _InspParam)
        {
            bool _Result = true;
            if (null == _Nodes) return false;
            switch (_Nodes.Name)
            {
                case "ResolutionX":     _InspParam.ResolutionX = Convert.ToDouble(_Nodes.InnerText); break;
                case "ResolutionY":     _InspParam.ResolutionY = Convert.ToDouble(_Nodes.InnerText); break;
                case "ResultUseFlag":   _InspParam.ResultUseFlag = _Nodes.InnerText; break;
                default:            _Result = false; break;
            }
            return _Result;
        }

        private bool GetInspectionParameterRegion(XmlNode _Nodes, ref InspectionAreaParameter _InspAreaParam)
        {
            bool _Result = true;
            if (null == _Nodes) return false;
            foreach (XmlNode _NodeChild in _Nodes.ChildNodes)
            {
                switch (_NodeChild.Name)
                {
                    case "Enable":                  _InspAreaParam.Enable = Convert.ToBoolean(_NodeChild.InnerText); break;
                    case "BenchMark":               _InspAreaParam.AreaBenchMark = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "NgNumber":                _InspAreaParam.NgAreaNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "AreaRegionCenterX":       _InspAreaParam.AreaRegionCenterX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "AreaRegionCenterY":       _InspAreaParam.AreaRegionCenterY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "AreaRegionWidth":         _InspAreaParam.AreaRegionWidth = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "AreaRegionHeight":        _InspAreaParam.AreaRegionHeight = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BaseIndexNumber":         _InspAreaParam.BaseIndexNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "IsUseMapData":            _InspAreaParam.IsUseMapData = Convert.ToBoolean(_NodeChild.InnerText); break;
                    case "MapDataUnitTotalCount":   _InspAreaParam.MapDataUnitTotalCount = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "MapDataStartNumber":      _InspAreaParam.MapDataStartNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "MapDataEndNumber":        _InspAreaParam.MapDataEndNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    default:                        _Result = false; break;
                }
            }
            return _Result;
        }

        private bool GetInspectionParameterAlgorithm(XmlNode _Nodes, ref InspectionAreaParameter _InspAreaParam)
        {
            bool _Result = true;
            if (null == _Nodes) return false;
            for (int iLoopCount = 0; ; ++iLoopCount)
            {
                InspectionAlgorithmParameter _InspAlgoParamTemp = new InspectionAlgorithmParameter();

                int _AlgoNumber = iLoopCount + 1;
                string _AlgoNumberName = String.Format("Algo{0}", _AlgoNumber);

                XmlNode _Node = _Nodes[_AlgoNumberName];
                if (null == _Node) break;

                foreach (XmlNode _NodeChild in _Node.ChildNodes)
                {
                    switch (_NodeChild.Name)
                    {
                        case "AlgoEnable":          _InspAlgoParamTemp.AlgoEnable = Convert.ToBoolean(_NodeChild.InnerText); break;
                        case "AlgoType":            _InspAlgoParamTemp.AlgoType = Convert.ToInt32(_NodeChild.InnerText); break;
                        case "AlgoBenchMark":       _InspAlgoParamTemp.AlgoBenchMark = Convert.ToInt32(_NodeChild.InnerText); break;
                        case "AlgoRegionCenterX":   _InspAlgoParamTemp.AlgoRegionCenterX = Convert.ToDouble(_NodeChild.InnerText); break;
                        case "AlgoRegionCenterY":   _InspAlgoParamTemp.AlgoRegionCenterY = Convert.ToDouble(_NodeChild.InnerText); break;
                        case "AlgoRegionWidth":     _InspAlgoParamTemp.AlgoRegionWidth = Convert.ToDouble(_NodeChild.InnerText); break;
                        case "AlgoRegionHeight":    _InspAlgoParamTemp.AlgoRegionHeight = Convert.ToDouble(_NodeChild.InnerText); break;
                        default:                    _Result = false; break;
                    }
                }

                if ((int)eAlgoType.C_PATTERN == _InspAlgoParamTemp.AlgoType)            GetPatternInspectionparameter(_Node, ref _InspAlgoParamTemp);
                else if ((int)eAlgoType.C_BLOB == _InspAlgoParamTemp.AlgoType)          GetBlobInspectionParameter(_Node, ref _InspAlgoParamTemp);
                else if ((int)eAlgoType.C_BLOB_REFER == _InspAlgoParamTemp.AlgoType)    GetBlobReferInspectionParameter(_Node, ref _InspAlgoParamTemp);
                else if ((int)eAlgoType.C_ID == _InspAlgoParamTemp.AlgoType)            GetBarCodeIDInspectionParameter(_Node, ref _InspAlgoParamTemp);
                else if ((int)eAlgoType.C_LINE_FIND == _InspAlgoParamTemp.AlgoType)     GetLineFindInspectionParameter(_Node, ref _InspAlgoParamTemp);
                else if ((int)eAlgoType.C_MULTI_PATTERN == _InspAlgoParamTemp.AlgoType) GetMultiPatternInspectionparameter(_Node, ref _InspAlgoParamTemp);
                else if ((int)eAlgoType.C_ELLIPSE == _InspAlgoParamTemp.AlgoType)       GetEllipseFindInspectionParameter(_Node, ref _InspAlgoParamTemp);
                

                _InspAreaParam.InspAlgoParam.Add(_InspAlgoParamTemp);
            }
            return _Result;
        }

        private void GetPatternInspectionparameter(XmlNode _Nodes, ref InspectionAlgorithmParameter _InspParam)
        {
            if (null == _Nodes) return;

            int _Cnt = 1;
            CogPatternAlgo _CogPattern = new CogPatternAlgo();
            _CogPattern.ReferenceInfoList = new References();
            _CogPattern.ReferenceInfoList.Clear();
            
            foreach (XmlNode _NodeChild in _Nodes)
            {
                if (null == _NodeChild) return;

                string ReferenceName = "Reference" + _Cnt;

                if (_NodeChild.Name == "PatternCount")       _CogPattern.PatternCount = Convert.ToInt32(_NodeChild.InnerText);
                else if (_NodeChild.Name == "MatchingScore") _CogPattern.MatchingScore = Convert.ToDouble(_NodeChild.InnerText);
                else if (_NodeChild.Name == "MatchingAngle") _CogPattern.MatchingAngle = Convert.ToDouble(_NodeChild.InnerText);
                else if (_NodeChild.Name == "MatchingCount") _CogPattern.MatchingCount = Convert.ToInt32(_NodeChild.InnerText);
                else if (_NodeChild.Name == "EnableShift")   _CogPattern.IsShift = Convert.ToBoolean(_NodeChild.InnerText);
                else if (_NodeChild.Name == "ShiftX")        _CogPattern.AllowedShiftX = Convert.ToInt32(_NodeChild.InnerText);
                else if (_NodeChild.Name == "ShiftY")        _CogPattern.AllowedShiftY = Convert.ToInt32(_NodeChild.InnerText);

                //Reference
                ReferenceInformation _ReferInfo = new ReferenceInformation();
                if (_NodeChild.Name == ReferenceName)
                {
                    foreach (XmlNode _Node in _NodeChild)
                    {
                        if (null == _Node) return;
                        switch (_Node.Name)
                        {
                            case "ReferencePath":       _ReferInfo.ReferencePath = _Node.InnerText; break;
                            case "InterActiveStartX":   _ReferInfo.InterActiveStartX = Convert.ToDouble(_Node.InnerText); break;
                            case "InterActiveStartY":   _ReferInfo.InterActiveStartY = Convert.ToDouble(_Node.InnerText); break;
                            case "StaticStartX":        _ReferInfo.StaticStartX = Convert.ToDouble(_Node.InnerText); break;
                            case "StaticStartY":        _ReferInfo.StaticStartY = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginX":             _ReferInfo.CenterX = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginY":             _ReferInfo.CenterY = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginPointOffsetX":  _ReferInfo.OriginPointOffsetX = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginPointOffsetY":  _ReferInfo.OriginPointOffsetY = Convert.ToDouble(_Node.InnerText); break;
                            case "Width":               _ReferInfo.Width = Convert.ToDouble(_Node.InnerText); break;
                            case "Height":              _ReferInfo.Height = Convert.ToDouble(_Node.InnerText); break;
                        }
                    }

                    try
                    {
                        _ReferInfo.Reference = (CogPMAlignPattern)CogSerializer.LoadObjectFromFile(_ReferInfo.ReferencePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.All);
                        _CogPattern.ReferenceInfoList.Add(_ReferInfo);
                    }

                    catch
                    {
                        _ReferInfo.Reference = new CogPMAlignPattern();
                        CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "GetPatternInspectionparameter Err", CLogManager.LOG_LEVEL.LOW);
                    }
                    
                    _Cnt++;
                }
            }
            _InspParam.Algorithm = _CogPattern;
        }

        private void GetMultiPatternInspectionparameter(XmlNode _Nodes, ref InspectionAlgorithmParameter _InspParam)
        {
            if (null == _Nodes) return;

            int _Cnt = 1;
            CogMultiPatternAlgo _CogMultiPattern = new CogMultiPatternAlgo();
            _CogMultiPattern.ReferenceInfoList = new References();
            _CogMultiPattern.ReferenceInfoList.Clear();

            foreach (XmlNode _NodeChild in _Nodes)
            {
                if (null == _NodeChild) return;

                string ReferenceName = "Reference" + _Cnt;

                if (_NodeChild.Name == "PatternCount") _CogMultiPattern.PatternCount = Convert.ToInt32(_NodeChild.InnerText);
                else if (_NodeChild.Name == "MatchingScore") _CogMultiPattern.MatchingScore = Convert.ToDouble(_NodeChild.InnerText);
                else if (_NodeChild.Name == "MatchingAngle") _CogMultiPattern.MatchingAngle = Convert.ToDouble(_NodeChild.InnerText);
                else if (_NodeChild.Name == "MatchingCount") _CogMultiPattern.MatchingCount = Convert.ToInt32(_NodeChild.InnerText);
                else if (_NodeChild.Name == "TwoPointAngle") _CogMultiPattern.TwoPointAngle = Convert.ToDouble(_NodeChild.InnerText);

                //Reference
                ReferenceInformation _ReferInfo = new ReferenceInformation();
                if (_NodeChild.Name == ReferenceName)
                {
                    foreach (XmlNode _Node in _NodeChild)
                    {
                        if (null == _Node) return;
                        switch (_Node.Name)
                        {
                            case "ReferencePath": _ReferInfo.ReferencePath = _Node.InnerText; break;
                            case "InterActiveStartX": _ReferInfo.InterActiveStartX = Convert.ToDouble(_Node.InnerText); break;
                            case "InterActiveStartY": _ReferInfo.InterActiveStartY = Convert.ToDouble(_Node.InnerText); break;
                            case "StaticStartX": _ReferInfo.StaticStartX = Convert.ToDouble(_Node.InnerText); break;
                            case "StaticStartY": _ReferInfo.StaticStartY = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginX": _ReferInfo.CenterX = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginY": _ReferInfo.CenterY = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginPointOffsetX": _ReferInfo.OriginPointOffsetX = Convert.ToDouble(_Node.InnerText); break;
                            case "OriginPointOffsetY": _ReferInfo.OriginPointOffsetY = Convert.ToDouble(_Node.InnerText); break;
                            case "Width": _ReferInfo.Width = Convert.ToDouble(_Node.InnerText); break;
                            case "Height": _ReferInfo.Height = Convert.ToDouble(_Node.InnerText); break;
                        }
                    }

                    try
                    {
                        _ReferInfo.Reference = (CogPMAlignPattern)CogSerializer.LoadObjectFromFile(_ReferInfo.ReferencePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.All);
                        _CogMultiPattern.ReferenceInfoList.Add(_ReferInfo);
                    }

                    catch
                    {
                        _ReferInfo.Reference = new CogPMAlignPattern();
                        CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "GetPatternInspectionparameter Err", CLogManager.LOG_LEVEL.LOW);
                    }

                    _Cnt++;
                }
            }
            _InspParam.Algorithm = _CogMultiPattern;
        }

        private void GetBlobReferInspectionParameter(XmlNode _Nodes, ref InspectionAlgorithmParameter _InspParam)
        {
            if (null == _Nodes) return;
            CogBlobReferenceAlgo _CogBlobRefer = new CogBlobReferenceAlgo();
            foreach (XmlNode _NodeChild in _Nodes)
            {
                if (null == _NodeChild) return;
                switch (_NodeChild.Name)
                {
                    case "Foreground":          _CogBlobRefer.ForeGround = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "ThresholdMin":        _CogBlobRefer.ThresholdMin = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "ThresholdMax":        _CogBlobRefer.ThresholdMax = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "BlobAreaMin":         _CogBlobRefer.BlobAreaMin = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BlobAreaMax":         _CogBlobRefer.BlobAreaMax = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "Width":               _CogBlobRefer.Width = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "WidthPos":            _CogBlobRefer.WidthPos = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "WidthNeg":            _CogBlobRefer.WidthNeg = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "Height":              _CogBlobRefer.Height = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "HeightPos":           _CogBlobRefer.HeightPos = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "HeightNeg":           _CogBlobRefer.HeightNeg = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginX":             _CogBlobRefer.OriginX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginY":             _CogBlobRefer.OriginY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BenchMarkPosition":   _CogBlobRefer.BenchMarkPosition = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "BodyArea":            _CogBlobRefer.BodyArea = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BodyWidth":           _CogBlobRefer.BodyWidth = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BodyHeight":          _CogBlobRefer.BodyHeight = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BodyAreaPermitPercent":   _CogBlobRefer.BodyAreaPermitPercent = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BodyWidthPermitPercent":  _CogBlobRefer.BodyWidthPermitPercent = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BodyHeightPermitPercent": _CogBlobRefer.BodyHeightPermitPercent = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "DummyHistoMeanValue":     _CogBlobRefer.DummyHistoMeanValue = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "UseBodyArea":         _CogBlobRefer.UseBodyArea = Convert.ToBoolean(_NodeChild.InnerText); break;
                    case "UseBodyWidth":        _CogBlobRefer.UseBodyWidth = Convert.ToBoolean(_NodeChild.InnerText); break;
                    case "UseBodyHeight":       _CogBlobRefer.UseBodyHeight = Convert.ToBoolean(_NodeChild.InnerText); break;
                    case "UseDummyValue":       _CogBlobRefer.UseDummyValue = Convert.ToBoolean(_NodeChild.InnerText); break;
                    case "ResolutionX":         _CogBlobRefer.ResolutionX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "ResolutionY":         _CogBlobRefer.ResolutionY = Convert.ToDouble(_NodeChild.InnerText); break;
                }
            }
            _InspParam.Algorithm = _CogBlobRefer;
        }

        private void GetBlobInspectionParameter(XmlNode _Nodes, ref InspectionAlgorithmParameter _InspParam)
        {
            if (null == _Nodes) return;
            CogBlobAlgo _CogBlob = new CogBlobAlgo();
            foreach (XmlNode _NodeChild in _Nodes)
            {
                if (null == _NodeChild) return;
                switch (_NodeChild.Name)
                {
                    case "Foreground":      _CogBlob.ForeGround = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "ThresholdMin":    _CogBlob.ThresholdMin = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "ThresholdMax":    _CogBlob.ThresholdMax = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "BlobAreaMin":     _CogBlob.BlobAreaMin = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BlobAreaMax":     _CogBlob.BlobAreaMax = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "WidthMin":        _CogBlob.WidthMin = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "WidthMax":        _CogBlob.WidthMax = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "HeightMin":       _CogBlob.HeightMin = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "HeightMax":       _CogBlob.HeightMax = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginX":         _CogBlob.OriginX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginY":         _CogBlob.OriginY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "BenchMarkPosition": _CogBlob.BenchMarkPosition = Convert.ToInt32(_NodeChild.InnerText); break;
                }
            }
            _InspParam.Algorithm = _CogBlob;
        }

        private void GetEllipseFindInspectionParameter(XmlNode _Nodes, ref InspectionAlgorithmParameter _InspParam)
        {
            if (null == _Nodes) return;
            CogEllipseAlgo _CogEllipseFind = new CogEllipseAlgo();
            foreach (XmlNode _NodeChild in _Nodes)
            {
                if (null == _NodeChild) return;
                switch (_NodeChild.Name)
                {
                    case "CaliperNumber": _CogEllipseFind.CaliperNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "CaliperSearchLength": _CogEllipseFind.CaliperSearchLength = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "CaliperProjectionLength": _CogEllipseFind.CaliperProjectionLength = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "CaliperSearchDirection": _CogEllipseFind.CaliperSearchDirection = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "CaliperPolarity": _CogEllipseFind.CaliperPolarity = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "CaliperIgnoreNumber": _CogEllipseFind.CaliperIgnoreNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "ArcCenterX": _CogEllipseFind.ArcCenterX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "ArcCenterY": _CogEllipseFind.ArcCenterY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "ArcRadiusX": _CogEllipseFind.ArcRadiusX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "ArcRadiusY": _CogEllipseFind.ArcRadiusY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "ArcAngleSpan": _CogEllipseFind.ArcAngleSpan = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginX": _CogEllipseFind.OriginX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginY": _CogEllipseFind.OriginY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginRadiusX": _CogEllipseFind.OriginRadiusX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginRadiusY": _CogEllipseFind.OriginRadiusY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "DiameterSize": _CogEllipseFind.DiameterSize = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "DiameterMinus": _CogEllipseFind.DiameterMinus = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "DiameterPlus": _CogEllipseFind.DiameterPlus = Convert.ToDouble(_NodeChild.InnerText); break;
                }
            }
            _InspParam.Algorithm = _CogEllipseFind;
        }

        private void GetBarCodeIDInspectionParameter(XmlNode _Nodes, ref InspectionAlgorithmParameter _InspParam)
        {
            if (null == _Nodes) return;
            CogBarCodeIDAlgo _CogBarCodeID = new CogBarCodeIDAlgo();
            foreach (XmlNode _NodeChild in _Nodes)
            {
                if (null == _NodeChild) return;
                switch (_NodeChild.Name)
                {
                    case "Symbology": _CogBarCodeID.Symbology = _NodeChild.InnerText; break;
                    case "TimeLimit": _CogBarCodeID.TimeLimit = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "FindCount": _CogBarCodeID.FindCount = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "OriginX": _CogBarCodeID.OriginX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "OriginY": _CogBarCodeID.OriginY = Convert.ToDouble(_NodeChild.InnerText); break;
                }
            }
            _InspParam.Algorithm = _CogBarCodeID;
        }

        private void GetLineFindInspectionParameter(XmlNode _Nodes, ref InspectionAlgorithmParameter _InspParam)
        {
            if (null == _Nodes) return;
            CogLineFindAlgo _CogLineFind = new CogLineFindAlgo();
            foreach (XmlNode _NodeChild in _Nodes)
            {
                if (null == _NodeChild) return;
                switch (_NodeChild.Name)
                {
                    case "CaliperNumber":           _CogLineFind.CaliperNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "CaliperSearchLength":     _CogLineFind.CaliperSearchLength = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "CaliperProjectionLength": _CogLineFind.CaliperProjectionLength = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "CaliperSearchDirection":  _CogLineFind.CaliperSearchDirection = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "IgnoreNumber":            _CogLineFind.IgnoreNumber = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "CaliperLineStartX":       _CogLineFind.CaliperLineStartX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "CaliperLineStartY":       _CogLineFind.CaliperLineStartY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "CaliperLineEndX":         _CogLineFind.CaliperLineEndX = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "CaliperLineEndY":         _CogLineFind.CaliperLineEndY = Convert.ToDouble(_NodeChild.InnerText); break;
                    case "ContrastThreshold":       _CogLineFind.ContrastThreshold = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "FilterHalfSizePixels":    _CogLineFind.FilterHalfSizePixels = Convert.ToInt32(_NodeChild.InnerText); break;
                    case "UseAlignment":            _CogLineFind.UseAlignment = Convert.ToBoolean(_NodeChild.InnerText); break;
                }
            }
            _InspParam.Algorithm = _CogLineFind;
        }

        public void WriteInspectionParameter(int _ID, string _InspParamFullPath = null)
        {
            //LDH, 2019.01.15, 별도 Recipe 사용을 위해 ISMCount 따로 설정
            int ISMCount = 0;
            if (SystemParam.IsTotalRecipe) ISMCount = _ID;

            if (null == _InspParamFullPath) _InspParamFullPath = InspectionDefaultPath + @"RecipeParameter\" + SystemParam.LastRecipeName[_ID] + @"\Module" + (ISMCount + 1) + @"\InspectionCondition.Rcp";

            string _InspParamPath = String.Format(@"{0}{1}\Module{2}", RecipeParameterPath, SystemParam.LastRecipeName[_ID], (ISMCount + 1));
            DirectoryInfo _DirInfo = new DirectoryInfo(_InspParamPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            //Reference folder를 지우고 다시 저장
            string _ReferencePath = String.Format(@"{0}{1}\Module{2}\Reference", RecipeParameterPath, SystemParam.LastRecipeName[_ID], (ISMCount + 1));
            _DirInfo = new DirectoryInfo(_ReferencePath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }
            Directory.Delete(_ReferencePath, true);

            XmlTextWriter _XmlWriter = new XmlTextWriter(_InspParamFullPath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("AlgoParameter");
            {
                _XmlWriter.WriteElementString("ResolutionX", InspParam[_ID].ResolutionX.ToString());
                _XmlWriter.WriteElementString("ResolutionY", InspParam[_ID].ResolutionY.ToString());
                _XmlWriter.WriteElementString("ResultUseFlag", InspParam[_ID].ResultUseFlag);

                for (int iLoopCount = 0; iLoopCount < InspParam[_ID].InspAreaParam.Count; ++iLoopCount)
                {
                    _XmlWriter.WriteStartElement("InspAlgoArea" + (iLoopCount + 1));
                    {
                        //검사 영역 저장
                        _XmlWriter.WriteElementString("Enable", InspParam[_ID].InspAreaParam[iLoopCount].Enable.ToString());
                        _XmlWriter.WriteElementString("NgNumber", InspParam[_ID].InspAreaParam[iLoopCount].NgAreaNumber.ToString());
                        _XmlWriter.WriteElementString("BenchMark", InspParam[_ID].InspAreaParam[iLoopCount].AreaBenchMark.ToString());
                        _XmlWriter.WriteElementString("AreaRegionCenterX", InspParam[_ID].InspAreaParam[iLoopCount].AreaRegionCenterX.ToString());
                        _XmlWriter.WriteElementString("AreaRegionCenterY", InspParam[_ID].InspAreaParam[iLoopCount].AreaRegionCenterY.ToString());
                        _XmlWriter.WriteElementString("AreaRegionWidth", InspParam[_ID].InspAreaParam[iLoopCount].AreaRegionWidth.ToString());
                        _XmlWriter.WriteElementString("AreaRegionHeight", InspParam[_ID].InspAreaParam[iLoopCount].AreaRegionHeight.ToString());
                        _XmlWriter.WriteElementString("BaseIndexNumber", InspParam[_ID].InspAreaParam[iLoopCount].BaseIndexNumber.ToString());
                        _XmlWriter.WriteElementString("IsUseMapData", InspParam[_ID].InspAreaParam[iLoopCount].IsUseMapData.ToString());
                        _XmlWriter.WriteElementString("MapDataUnitTotalCount", InspParam[_ID].InspAreaParam[iLoopCount].MapDataUnitTotalCount.ToString());
                        _XmlWriter.WriteElementString("MapDataStartNumber", InspParam[_ID].InspAreaParam[iLoopCount].MapDataStartNumber.ToString());
                        _XmlWriter.WriteElementString("MapDataEndNumber", InspParam[_ID].InspAreaParam[iLoopCount].MapDataEndNumber.ToString());

                        for (int jLoopCount = 0; jLoopCount < InspParam[_ID].InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                        {
                            InspectionAlgorithmParameter _InspAlgoParamTemp = InspParam[_ID].InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount];
                            _XmlWriter.WriteStartElement("Algo" + (jLoopCount + 1));
                            {
                                _XmlWriter.WriteElementString("AlgoEnable", _InspAlgoParamTemp.AlgoEnable.ToString());
                                _XmlWriter.WriteElementString("AlgoType", _InspAlgoParamTemp.AlgoType.ToString());
                                _XmlWriter.WriteElementString("AlgoBenchMark", _InspAlgoParamTemp.AlgoBenchMark.ToString());
                                _XmlWriter.WriteElementString("AlgoRegionCenterX", _InspAlgoParamTemp.AlgoRegionCenterX.ToString());
                                _XmlWriter.WriteElementString("AlgoRegionCenterY", _InspAlgoParamTemp.AlgoRegionCenterY.ToString());
                                _XmlWriter.WriteElementString("AlgoRegionWidth", _InspAlgoParamTemp.AlgoRegionWidth.ToString());
                                _XmlWriter.WriteElementString("AlgoRegionHeight", _InspAlgoParamTemp.AlgoRegionHeight.ToString());

                                AreaNumber = iLoopCount + 1;
                                AlgoNumber = jLoopCount + 1;
                                eAlgoType _AlgoType = (eAlgoType)_InspAlgoParamTemp.AlgoType;
                                if (eAlgoType.C_PATTERN == _AlgoType)            WritePatternInspectionParameter(_ID, ISMCount, _XmlWriter, _InspAlgoParamTemp.Algorithm);
                                else if (eAlgoType.C_BLOB == _AlgoType)          WriteBlobInspectionParameter(_ID, ISMCount, _XmlWriter, _InspAlgoParamTemp.Algorithm);
                                else if (eAlgoType.C_BLOB_REFER == _AlgoType)    WriteBlobReferInspectionParameter(_ID, ISMCount, _XmlWriter, _InspAlgoParamTemp.Algorithm);
                                else if (eAlgoType.C_ELLIPSE == _AlgoType)       WriteEllipseInspectionParameter(_ID, ISMCount, _XmlWriter, _InspAlgoParamTemp.Algorithm);
                                else if (eAlgoType.C_ID == _AlgoType)            WriteBarCodeIDInspectionParameter(_ID, ISMCount, _XmlWriter, _InspAlgoParamTemp.Algorithm);
                                else if (eAlgoType.C_LINE_FIND == _AlgoType)     WriteLineFindInspectionParameter(_ID, ISMCount, _XmlWriter, _InspAlgoParamTemp.Algorithm);
                                else if (eAlgoType.C_MULTI_PATTERN == _AlgoType) WriteMultiPatternInspectionParameter(_ID, ISMCount, _XmlWriter, _InspAlgoParamTemp.Algorithm);
                            }
                            _XmlWriter.WriteEndElement();
                        }
                    }
                    _XmlWriter.WriteEndElement();
                }
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }

        private void WritePatternInspectionParameter(int _ID, int _ISMCount, XmlTextWriter _XmlWriter, Object _InspAlgoParam)
        {
            CogPatternAlgo _CogPatternAlgo = (CogPatternAlgo)_InspAlgoParam;
            _XmlWriter.WriteElementString("PatternCount", _CogPatternAlgo.PatternCount.ToString());
            _XmlWriter.WriteElementString("MatchingScore", _CogPatternAlgo.MatchingScore.ToString());
            _XmlWriter.WriteElementString("MatchingAngle", _CogPatternAlgo.MatchingAngle.ToString());
            _XmlWriter.WriteElementString("MatchingCount", _CogPatternAlgo.MatchingCount.ToString());
            _XmlWriter.WriteElementString("EnableShift", _CogPatternAlgo.IsShift.ToString());
            _XmlWriter.WriteElementString("ShiftX", _CogPatternAlgo.AllowedShiftX.ToString());
            _XmlWriter.WriteElementString("ShiftY", _CogPatternAlgo.AllowedShiftY.ToString());

            //Reference Model Save
            for (int iLoopCount = 0; iLoopCount < _CogPatternAlgo.ReferenceInfoList.Count; ++iLoopCount)
            {
                string _Extention = ".pat";
                string _FileName = String.Format("AR{0:D2}_AL{1:D2}_RF{2:D2}{3}", AreaNumber, AlgoNumber, iLoopCount + 1, _Extention);
                string _RecipeName = SystemParam.LastRecipeName[_ID];
                string _RecipeParameterPath = String.Format(@"{0}RecipeParameter\{1}\Module{2}\Reference\", InspectionDefaultPath, _RecipeName, _ISMCount + 1);

                if (false == Directory.Exists(_RecipeParameterPath)) Directory.CreateDirectory(_RecipeParameterPath);
                _CogPatternAlgo.ReferenceInfoList[iLoopCount].ReferencePath = _RecipeParameterPath + _FileName;
                CogSerializer.SaveObjectToFile(_CogPatternAlgo.ReferenceInfoList[iLoopCount].Reference, _CogPatternAlgo.ReferenceInfoList[iLoopCount].ReferencePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.InputImages);

                _XmlWriter.WriteStartElement("Reference" + (iLoopCount + 1));
                {
                    _XmlWriter.WriteElementString("ReferencePath", _CogPatternAlgo.ReferenceInfoList[iLoopCount].ReferencePath);
                    _XmlWriter.WriteElementString("InterActiveStartX", _CogPatternAlgo.ReferenceInfoList[iLoopCount].InterActiveStartX.ToString());
                    _XmlWriter.WriteElementString("InterActiveStartY", _CogPatternAlgo.ReferenceInfoList[iLoopCount].InterActiveStartY.ToString());
                    _XmlWriter.WriteElementString("StaticStartX", _CogPatternAlgo.ReferenceInfoList[iLoopCount].StaticStartX.ToString());
                    _XmlWriter.WriteElementString("StaticStartY", _CogPatternAlgo.ReferenceInfoList[iLoopCount].StaticStartY.ToString());
                    _XmlWriter.WriteElementString("OriginX", _CogPatternAlgo.ReferenceInfoList[iLoopCount].CenterX.ToString());
                    _XmlWriter.WriteElementString("OriginY", _CogPatternAlgo.ReferenceInfoList[iLoopCount].CenterY.ToString());
                    _XmlWriter.WriteElementString("OriginPointOffsetX", _CogPatternAlgo.ReferenceInfoList[iLoopCount].OriginPointOffsetX.ToString());
                    _XmlWriter.WriteElementString("OriginPointOffsetY", _CogPatternAlgo.ReferenceInfoList[iLoopCount].OriginPointOffsetY.ToString());
                    _XmlWriter.WriteElementString("Width", _CogPatternAlgo.ReferenceInfoList[iLoopCount].Width.ToString());
                    _XmlWriter.WriteElementString("Height", _CogPatternAlgo.ReferenceInfoList[iLoopCount].Height.ToString());
                }
                _XmlWriter.WriteEndElement();
            }
        }

        private void WriteMultiPatternInspectionParameter(int _ID, int _ISMCount, XmlTextWriter _XmlWriter, Object _InspAlgoParam)
        {
            CogMultiPatternAlgo _CogMultiPatternAlgo = (CogMultiPatternAlgo)_InspAlgoParam;
            _XmlWriter.WriteElementString("PatternCount", _CogMultiPatternAlgo.PatternCount.ToString());
            _XmlWriter.WriteElementString("MatchingScore", _CogMultiPatternAlgo.MatchingScore.ToString());
            _XmlWriter.WriteElementString("MatchingAngle", _CogMultiPatternAlgo.MatchingAngle.ToString());
            _XmlWriter.WriteElementString("MatchingCount", _CogMultiPatternAlgo.MatchingCount.ToString());
            _XmlWriter.WriteElementString("TwoPointAngle", _CogMultiPatternAlgo.TwoPointAngle.ToString());
            
            //Reference Model Save
            for (int iLoopCount = 0; iLoopCount < _CogMultiPatternAlgo.ReferenceInfoList.Count; ++iLoopCount)
            {
                string _Extention = ".pat";
                string _FileName = String.Format("AR{0:D2}_AL{1:D2}_RF{2:D2}{3}", AreaNumber, AlgoNumber, iLoopCount + 1, _Extention);
                string _RecipeName = SystemParam.LastRecipeName[_ID];
                string _RecipeParameterPath = String.Format(@"{0}RecipeParameter\{1}\Module{2}\Reference\", InspectionDefaultPath, _RecipeName, _ISMCount + 1);

                if (false == Directory.Exists(_RecipeParameterPath)) Directory.CreateDirectory(_RecipeParameterPath);
                _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].ReferencePath = _RecipeParameterPath + _FileName;
                CogSerializer.SaveObjectToFile(_CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].Reference, _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].ReferencePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.InputImages);

                _XmlWriter.WriteStartElement("Reference" + (iLoopCount + 1));
                {
                    _XmlWriter.WriteElementString("ReferencePath", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].ReferencePath);
                    _XmlWriter.WriteElementString("InterActiveStartX", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].InterActiveStartX.ToString());
                    _XmlWriter.WriteElementString("InterActiveStartY", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].InterActiveStartY.ToString());
                    _XmlWriter.WriteElementString("StaticStartX", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].StaticStartX.ToString());
                    _XmlWriter.WriteElementString("StaticStartY", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].StaticStartY.ToString());
                    _XmlWriter.WriteElementString("OriginX", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].CenterX.ToString());
                    _XmlWriter.WriteElementString("OriginY", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].CenterY.ToString());
                    _XmlWriter.WriteElementString("OriginPointOffsetX", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].OriginPointOffsetX.ToString());
                    _XmlWriter.WriteElementString("OriginPointOffsetY", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].OriginPointOffsetY.ToString());
                    _XmlWriter.WriteElementString("Width", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].Width.ToString());
                    _XmlWriter.WriteElementString("Height", _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].Height.ToString());
                }
                _XmlWriter.WriteEndElement();
            }
        }

        private void WriteBlobReferInspectionParameter(int _ID, int _ISMCount, XmlTextWriter _XmlWriter, Object _InspAlgoParam)
        {
            CogBlobReferenceAlgo _CogBlobReferAlgo = (CogBlobReferenceAlgo)_InspAlgoParam;
            _XmlWriter.WriteElementString("Foreground", _CogBlobReferAlgo.ForeGround.ToString());
            _XmlWriter.WriteElementString("ThresholdMin", _CogBlobReferAlgo.ThresholdMin.ToString());
            _XmlWriter.WriteElementString("ThresholdMax", _CogBlobReferAlgo.ThresholdMax.ToString());
            _XmlWriter.WriteElementString("BlobAreaMin", _CogBlobReferAlgo.BlobAreaMin.ToString());
            _XmlWriter.WriteElementString("BlobAreaMax", _CogBlobReferAlgo.BlobAreaMax.ToString());
            _XmlWriter.WriteElementString("Width", _CogBlobReferAlgo.Width.ToString());
            _XmlWriter.WriteElementString("WidthPos", _CogBlobReferAlgo.WidthPos.ToString());
            _XmlWriter.WriteElementString("WidthNeg", _CogBlobReferAlgo.WidthNeg.ToString());
            _XmlWriter.WriteElementString("Height", _CogBlobReferAlgo.Height.ToString());
            _XmlWriter.WriteElementString("HeightPos", _CogBlobReferAlgo.HeightPos.ToString());
            _XmlWriter.WriteElementString("HeightNeg", _CogBlobReferAlgo.HeightNeg.ToString());
            _XmlWriter.WriteElementString("OriginX", _CogBlobReferAlgo.OriginX.ToString());
            _XmlWriter.WriteElementString("OriginY", _CogBlobReferAlgo.OriginY.ToString());
            _XmlWriter.WriteElementString("BenchMarkPosition", _CogBlobReferAlgo.BenchMarkPosition.ToString());
            _XmlWriter.WriteElementString("BodyArea", _CogBlobReferAlgo.BodyArea.ToString());
            _XmlWriter.WriteElementString("BodyWidth", _CogBlobReferAlgo.BodyWidth.ToString());
            _XmlWriter.WriteElementString("BodyHeight", _CogBlobReferAlgo.BodyHeight.ToString());
            _XmlWriter.WriteElementString("BodyAreaPermitPercent", _CogBlobReferAlgo.BodyAreaPermitPercent.ToString());
            _XmlWriter.WriteElementString("BodyWidthPermitPercent", _CogBlobReferAlgo.BodyWidthPermitPercent.ToString());
            _XmlWriter.WriteElementString("BodyHeightPermitPercent", _CogBlobReferAlgo.BodyHeightPermitPercent.ToString());
            _XmlWriter.WriteElementString("DummyHistoMeanValue", _CogBlobReferAlgo.DummyHistoMeanValue.ToString());
            _XmlWriter.WriteElementString("UseBodyArea", _CogBlobReferAlgo.UseBodyArea.ToString());
            _XmlWriter.WriteElementString("UseBodyWidth", _CogBlobReferAlgo.UseBodyWidth.ToString());
            _XmlWriter.WriteElementString("UseBodyHeight", _CogBlobReferAlgo.UseBodyHeight.ToString());
            _XmlWriter.WriteElementString("UseDummyValue", _CogBlobReferAlgo.UseDummyValue.ToString());
            _XmlWriter.WriteElementString("ResolutionX", _CogBlobReferAlgo.ResolutionX.ToString());
            _XmlWriter.WriteElementString("ResolutionY", _CogBlobReferAlgo.ResolutionY.ToString());
        }

        private void WriteBlobInspectionParameter(int _ID, int _ISMCount, XmlTextWriter _XmlWriter, Object _InspAlgoParam)
        {
            CogBlobAlgo _CogBlobAlgo = (CogBlobAlgo)_InspAlgoParam;
            _XmlWriter.WriteElementString("Foreground", _CogBlobAlgo.ForeGround.ToString());
            _XmlWriter.WriteElementString("ThresholdMin", _CogBlobAlgo.ThresholdMin.ToString());
            _XmlWriter.WriteElementString("ThresholdMax", _CogBlobAlgo.ThresholdMax.ToString());
            _XmlWriter.WriteElementString("BlobAreaMin", _CogBlobAlgo.BlobAreaMin.ToString());
            _XmlWriter.WriteElementString("BlobAreaMax", _CogBlobAlgo.BlobAreaMax.ToString());
            _XmlWriter.WriteElementString("WidthMin", _CogBlobAlgo.WidthMin.ToString());
            _XmlWriter.WriteElementString("WidthMax", _CogBlobAlgo.WidthMax.ToString());
            _XmlWriter.WriteElementString("HeightMin", _CogBlobAlgo.HeightMin.ToString());
            _XmlWriter.WriteElementString("HeightMax", _CogBlobAlgo.HeightMax.ToString());
            _XmlWriter.WriteElementString("OriginX", _CogBlobAlgo.OriginX.ToString());
            _XmlWriter.WriteElementString("OriginY", _CogBlobAlgo.OriginY.ToString());
            _XmlWriter.WriteElementString("BenchMarkPosition", _CogBlobAlgo.BenchMarkPosition.ToString());
        }
               
        private void WriteEllipseInspectionParameter(int _ID, int _ISMCount, XmlTextWriter _XmlWriter, Object _InspAlgoParam)
        {
            var _CogEllipseAlgo = _InspAlgoParam as CogEllipseAlgo;
            _XmlWriter.WriteElementString("CaliperNumber", _CogEllipseAlgo.CaliperNumber.ToString());
            _XmlWriter.WriteElementString("CaliperSearchLength", _CogEllipseAlgo.CaliperSearchLength.ToString());
            _XmlWriter.WriteElementString("CaliperProjectionLength", _CogEllipseAlgo.CaliperProjectionLength.ToString());
            _XmlWriter.WriteElementString("CaliperSearchDirection", _CogEllipseAlgo.CaliperSearchDirection.ToString());
            _XmlWriter.WriteElementString("CaliperPolarity", _CogEllipseAlgo.CaliperPolarity.ToString());
            _XmlWriter.WriteElementString("CaliperIgnoreNumber", _CogEllipseAlgo.CaliperIgnoreNumber.ToString());
            _XmlWriter.WriteElementString("ArcCenterX", _CogEllipseAlgo.ArcCenterX.ToString());
            _XmlWriter.WriteElementString("ArcCenterY", _CogEllipseAlgo.ArcCenterY.ToString());
            _XmlWriter.WriteElementString("ArcRadiusX", _CogEllipseAlgo.ArcRadiusX.ToString());
            _XmlWriter.WriteElementString("ArcRadiusY", _CogEllipseAlgo.ArcRadiusY.ToString());
            _XmlWriter.WriteElementString("ArcAngleSpan", _CogEllipseAlgo.ArcAngleSpan.ToString());
            _XmlWriter.WriteElementString("OriginX", _CogEllipseAlgo.OriginX.ToString());
            _XmlWriter.WriteElementString("OriginY", _CogEllipseAlgo.OriginY.ToString());
            _XmlWriter.WriteElementString("OriginRadiusX", _CogEllipseAlgo.OriginRadiusX.ToString());
            _XmlWriter.WriteElementString("OriginRadiusY", _CogEllipseAlgo.OriginRadiusY.ToString());
            _XmlWriter.WriteElementString("DiameterSize", _CogEllipseAlgo.DiameterSize.ToString());
            _XmlWriter.WriteElementString("DiameterMinus", _CogEllipseAlgo.DiameterMinus.ToString());
            _XmlWriter.WriteElementString("DiameterPlus", _CogEllipseAlgo.DiameterPlus.ToString());
        }

        private void WriteBarCodeIDInspectionParameter(int _ID, int _ISMCount, XmlTextWriter _XmlWriter, Object _InspAlgoParam)
        {
            var _CogBarCodeIDAlgo = _InspAlgoParam as CogBarCodeIDAlgo;
            _XmlWriter.WriteElementString("Symbology", _CogBarCodeIDAlgo.Symbology);
            _XmlWriter.WriteElementString("OriginX", _CogBarCodeIDAlgo.OriginX.ToString());
            _XmlWriter.WriteElementString("OriginY", _CogBarCodeIDAlgo.OriginY.ToString());
            _XmlWriter.WriteElementString("TimeLimit", _CogBarCodeIDAlgo.TimeLimit.ToString());
            _XmlWriter.WriteElementString("FindCount", _CogBarCodeIDAlgo.FindCount.ToString());
        }

        private void WriteLineFindInspectionParameter(int _ID, int _ISMCount, XmlTextWriter _XmlWriter, Object _InspAlgoParam)
        {
            var _CogLineFindAlgo = _InspAlgoParam as CogLineFindAlgo;
            _XmlWriter.WriteElementString("CaliperNumber", _CogLineFindAlgo.CaliperNumber.ToString());
            _XmlWriter.WriteElementString("CaliperSearchLength", _CogLineFindAlgo.CaliperSearchLength.ToString());
            _XmlWriter.WriteElementString("CaliperProjectionLength", _CogLineFindAlgo.CaliperProjectionLength.ToString());
            _XmlWriter.WriteElementString("CaliperSearchDirection", _CogLineFindAlgo.CaliperSearchDirection.ToString());
            _XmlWriter.WriteElementString("IgnoreNumber", _CogLineFindAlgo.IgnoreNumber.ToString());
            _XmlWriter.WriteElementString("CaliperLineStartX", _CogLineFindAlgo.CaliperLineStartX.ToString());
            _XmlWriter.WriteElementString("CaliperLineStartY", _CogLineFindAlgo.CaliperLineStartY.ToString());
            _XmlWriter.WriteElementString("CaliperLineEndX", _CogLineFindAlgo.CaliperLineEndX.ToString());
            _XmlWriter.WriteElementString("CaliperLineEndY", _CogLineFindAlgo.CaliperLineEndY.ToString());
            _XmlWriter.WriteElementString("ContrastThreshold", _CogLineFindAlgo.ContrastThreshold.ToString());
            _XmlWriter.WriteElementString("FilterHalfSizePixels", _CogLineFindAlgo.FilterHalfSizePixels.ToString());
            _XmlWriter.WriteElementString("UseAlignment", _CogLineFindAlgo.UseAlignment.ToString());
        }
        
        #endregion Read & Write InspectionParameter

        #region Read & Write Map Data Parameter
        private bool ReadInspectionMapDataParameter(int _ID, string _RecipeName = null)
        {
            bool _Result = true;

            //LDH, 2019.01.15, 별도 Recipe 사용을 위해 ISMCount 따로 설정
            int ISMCount = 0;
            if (SystemParam.IsTotalRecipe) ISMCount = _ID;

            if (null == _RecipeName) _RecipeName = SystemParam.LastRecipeName[_ID];
            string _RecipeParameterPath = InspectionDefaultPath + @"RecipeParameter\" + _RecipeName + @"\Module" + (ISMCount + 1);
            DirectoryInfo _DirInfo = new DirectoryInfo(_RecipeParameterPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            //파일이 없으면 Map Data가 없는 걸로
            string _InspMapDataParamFullPath = _RecipeParameterPath + @"\InspectionMapData.Rcp";
            if (false == File.Exists(_InspMapDataParamFullPath)) return true;

            XmlNodeList _XmlNodeList = GetNodeList(_InspMapDataParamFullPath);
            if (null == _XmlNodeList) return true;

            int _Cnt = 0;
            InspMapDataParam[_ID] = new MapDataParameter();
            InspMapDataParam[_ID].Info.UnitListCenterX.Clear();
            InspMapDataParam[_ID].Info.UnitListCenterY.Clear();
            InspMapDataParam[_ID].Info.UnitListWidth.Clear();
            InspMapDataParam[_ID].Info.UnitListHeight.Clear();
            foreach (XmlNode _Nodes in _XmlNodeList)
            {
                if (null == _Nodes) return true;
                    
                if (_Nodes.Name == "UnitPatternPath")                InspMapDataParam[_ID].Info.UnitPatternPath = _Nodes.InnerText;
                else if (_Nodes.Name == "UnitTotalCount")            InspMapDataParam[_ID].Info.UnitTotalCount = Convert.ToUInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitRowCount")              InspMapDataParam[_ID].Info.UnitRowCount = Convert.ToUInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitColumnCount")           InspMapDataParam[_ID].Info.UnitColumnCount = Convert.ToUInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "SectionRowCount")           InspMapDataParam[_ID].Info.SectionRowCount = Convert.ToUInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "SectionColumnCount")        InspMapDataParam[_ID].Info.SectionColumnCount = Convert.ToUInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchType")                InspMapDataParam[_ID].Info.SearchType = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "MapDataTeachingMode")       InspMapDataParam[_ID].Info.MapDataTeachingMode = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitSearchAreaCenterX")     InspMapDataParam[_ID].Unit.SearchAreaCenterX = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitSearchAreaCenterY")     InspMapDataParam[_ID].Unit.SearchAreaCenterY = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitSearchAreaWidth")       InspMapDataParam[_ID].Unit.SearchAreaWidth = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitSearchAreaHeight")      InspMapDataParam[_ID].Unit.SearchAreaHeight = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitPatternAreaOriginX")    InspMapDataParam[_ID].Unit.PatternAreaOriginX = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitPatternAreaOriginY")    InspMapDataParam[_ID].Unit.PatternAreaOriginY = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitPatternAreaCenterX")    InspMapDataParam[_ID].Unit.PatternAreaCenterX = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitPatternAreaCenterY")    InspMapDataParam[_ID].Unit.PatternAreaCenterY = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitPatternAreaWidth")      InspMapDataParam[_ID].Unit.PatternAreaWidth = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitPatternAreaHeight")     InspMapDataParam[_ID].Unit.PatternAreaHeight = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "WholeSearchAreaCenterX")    InspMapDataParam[_ID].Whole.SearchAreaCenterX = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "WholeSearchAreaCenterY")    InspMapDataParam[_ID].Whole.SearchAreaCenterY = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "WholeSearchAreaWidth")      InspMapDataParam[_ID].Whole.SearchAreaWidth = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "WholeSearchAreaHeight")     InspMapDataParam[_ID].Whole.SearchAreaHeight = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "FindCount")                 InspMapDataParam[_ID].Info.FindCount = Convert.ToUInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "FindScore")                 InspMapDataParam[_ID].Info.FindScore = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "AngleLimit")                InspMapDataParam[_ID].Info.AngleLimit = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "UnitCenterList")
                {
                    foreach (XmlNode _Node in _Nodes)
                    {
                        if (null == _Node) break;

                        string _UnitCenterString = String.Format("UnitCenter{0}", _Cnt);
                        if (_UnitCenterString == _Node.Name)
                        {
                            foreach (XmlNode _NodeChild in _Node)
                            {
                                if (_NodeChild.Name == "X")      InspMapDataParam[_ID].Info.UnitListCenterX.Add(Convert.ToDouble(_NodeChild.InnerText));
                                else if (_NodeChild.Name == "Y") InspMapDataParam[_ID].Info.UnitListCenterY.Add(Convert.ToDouble(_NodeChild.InnerText));
                                else if (_NodeChild.Name == "W") InspMapDataParam[_ID].Info.UnitListWidth.Add(Convert.ToDouble(_NodeChild.InnerText));
                                else if (_NodeChild.Name == "H") InspMapDataParam[_ID].Info.UnitListHeight.Add(Convert.ToDouble(_NodeChild.InnerText));
                            }
                        }
                        _Cnt++;
                    }
                }

                else if (_Nodes.Name == "IsUsableMapID")        InspMapDataParam[_ID].MapID.IsUsableMapID = Convert.ToBoolean(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchAreaCenterX")    InspMapDataParam[_ID].MapID.SearchAreaCenterX = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchAreaCenterY")    InspMapDataParam[_ID].MapID.SearchAreaCenterY = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchAreaWidth")      InspMapDataParam[_ID].MapID.SearchAreaWidth = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchAreaHeight")     InspMapDataParam[_ID].MapID.SearchAreaHeight = Convert.ToDouble(_Nodes.InnerText);
                else if (_Nodes.Name == "SearhDirection")       InspMapDataParam[_ID].MapID.SearhDirection = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchThreshold")      InspMapDataParam[_ID].MapID.SearchThreshold = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchSizeMin")        InspMapDataParam[_ID].MapID.SearchSizeMin = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "SearchSizeMax")        InspMapDataParam[_ID].MapID.SearchSizeMax = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "BlobAreaSizeMin")      InspMapDataParam[_ID].MapID.BlobAreaSizeMin = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "BlobAreaSizeMax")      InspMapDataParam[_ID].MapID.BlobAreaSizeMax = Convert.ToInt32(_Nodes.InnerText);
                else if (_Nodes.Name == "MapIDInfoList")
                {
                    _Cnt = 0;
                    foreach (XmlNode _Node in _Nodes)
                    {
                        if (null == _Node) break;

                        string _MapIDString = String.Format("MapIDInfo{0}", _Cnt);
                        if (_MapIDString == _Node.Name)
                        {
                            MapIDRectInfo _MapIDRectInfoTemp = new MapIDRectInfo();
                            foreach (XmlNode _NodeChild in _Node)
                            {
                                if (_NodeChild.Name == "X") _MapIDRectInfoTemp.CenterPt.X = (Convert.ToDouble(_NodeChild.InnerText));
                                else if (_NodeChild.Name == "Y") _MapIDRectInfoTemp.CenterPt.Y = (Convert.ToDouble(_NodeChild.InnerText));
                                else if (_NodeChild.Name == "W") _MapIDRectInfoTemp.Width = (Convert.ToDouble(_NodeChild.InnerText));
                                else if (_NodeChild.Name == "H") _MapIDRectInfoTemp.Height = (Convert.ToDouble(_NodeChild.InnerText));
                            }
                            InspMapDataParam[_ID].MapID.MapIDInfoList.Add(_MapIDRectInfoTemp);
                        }
                        _Cnt++;
                    }
                }
            }

            try
            {
                InspMapDataParam[_ID].Info.UnitPattern = (CogPMAlignPattern)CogSerializer.LoadObjectFromFile(InspMapDataParam[_ID].Info.UnitPatternPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.All);
            }
            
            catch
            {
                InspMapDataParam[_ID].Info.UnitPattern = new CogPMAlignPattern();
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ReadInspectionMapDataParameter Err", CLogManager.LOG_LEVEL.LOW);
            }

            return _Result;
        }
        
        public void WriteInspectionMapDataparameter(int _ID, string _RecipeName = null)
        {
            //LDH, 2019.01.15, 별도 Recipe 사용을 위해 ISMCount 따로 설정
            int ISMCount = 0;
            if (SystemParam.IsTotalRecipe) ISMCount = _ID;

            //if (InspMapDataParam[_ID].UnitListCenterX.Count == 0 || InspMapDataParam[_ID].UnitListCenterY.Count == 0) return;
            if (null == _RecipeName) _RecipeName = SystemParam.LastRecipeName[_ID];
            string _MapDataParameterFilePath = InspectionDefaultPath + @"RecipeParameter\" + _RecipeName + @"\Module" + (ISMCount + 1) + @"\InspectionMapData.Rcp";
            string _RecipeParameterPath = InspectionDefaultPath + @"RecipeParameter\" + _RecipeName + @"\Module" + (ISMCount + 1);
            DirectoryInfo _DirInfo = new DirectoryInfo(_RecipeParameterPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _PatternFileName = String.Format("MapDataReference.pat");
            string _PatternFilePath = String.Format(@"{0}RecipeParameter\{1}\Module{2}\MapDataReference\", InspectionDefaultPath, _RecipeName, ISMCount + 1);
            if (false == Directory.Exists(_PatternFilePath)) Directory.CreateDirectory(_PatternFilePath);
            InspMapDataParam[_ID].Info.UnitPatternPath = _PatternFilePath + _PatternFileName;
            if (InspMapDataParam[_ID].Info.UnitPattern != null) 
                CogSerializer.SaveObjectToFile(InspMapDataParam[_ID].Info.UnitPattern, InspMapDataParam[_ID].Info.UnitPatternPath);

            XmlTextWriter _XmlWriter = new XmlTextWriter(_MapDataParameterFilePath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("MapDataList");
            {
                _XmlWriter.WriteElementString("UnitPatternPath", InspMapDataParam[_ID].Info.UnitPatternPath);
                _XmlWriter.WriteElementString("UnitTotalCount", InspMapDataParam[_ID].Info.UnitTotalCount.ToString());
                _XmlWriter.WriteElementString("UnitRowCount", InspMapDataParam[_ID].Info.UnitRowCount.ToString());
                _XmlWriter.WriteElementString("UnitColumnCount", InspMapDataParam[_ID].Info.UnitColumnCount.ToString());
                _XmlWriter.WriteElementString("SectionRowCount", InspMapDataParam[_ID].Info.SectionRowCount.ToString());
                _XmlWriter.WriteElementString("SearchType", InspMapDataParam[_ID].Info.SearchType.ToString());
                _XmlWriter.WriteElementString("SectionColumnCount", InspMapDataParam[_ID].Info.SectionColumnCount.ToString());
                _XmlWriter.WriteElementString("MapDataTeachingMode", InspMapDataParam[_ID].Info.MapDataTeachingMode.ToString());
                _XmlWriter.WriteElementString("UnitSearchAreaCenterX", InspMapDataParam[_ID].Unit.SearchAreaCenterX.ToString());
                _XmlWriter.WriteElementString("UnitSearchAreaCenterY", InspMapDataParam[_ID].Unit.SearchAreaCenterY.ToString());
                _XmlWriter.WriteElementString("UnitSearchAreaWidth", InspMapDataParam[_ID].Unit.SearchAreaWidth.ToString());
                _XmlWriter.WriteElementString("UnitSearchAreaHeight", InspMapDataParam[_ID].Unit.SearchAreaHeight.ToString());
                _XmlWriter.WriteElementString("UnitPatternAreaOriginX", InspMapDataParam[_ID].Unit.PatternAreaOriginX.ToString());
                _XmlWriter.WriteElementString("UnitPatternAreaOriginY", InspMapDataParam[_ID].Unit.PatternAreaOriginY.ToString());
                _XmlWriter.WriteElementString("UnitPatternAreaCenterX", InspMapDataParam[_ID].Unit.PatternAreaCenterX.ToString());
                _XmlWriter.WriteElementString("UnitPatternAreaCenterY", InspMapDataParam[_ID].Unit.PatternAreaCenterY.ToString());
                _XmlWriter.WriteElementString("UnitPatternAreaWidth", InspMapDataParam[_ID].Unit.PatternAreaWidth.ToString());
                _XmlWriter.WriteElementString("UnitPatternAreaHeight", InspMapDataParam[_ID].Unit.PatternAreaHeight.ToString());
                _XmlWriter.WriteStartElement("UnitCenterList");
                {
                    for (int iLoopCount = 0; iLoopCount < InspMapDataParam[_ID].Info.UnitListCenterX.Count; ++iLoopCount)
                    {
                        _XmlWriter.WriteStartElement("UnitCenter" + (iLoopCount));
                        {
                            _XmlWriter.WriteElementString("X", InspMapDataParam[_ID].Info.UnitListCenterX[iLoopCount].ToString());
                            _XmlWriter.WriteElementString("Y", InspMapDataParam[_ID].Info.UnitListCenterY[iLoopCount].ToString());
                            _XmlWriter.WriteElementString("W", InspMapDataParam[_ID].Info.UnitListWidth[iLoopCount].ToString());
                            _XmlWriter.WriteElementString("H", InspMapDataParam[_ID].Info.UnitListHeight[iLoopCount].ToString());
                        }
                        _XmlWriter.WriteEndElement();
                    }
                }
                _XmlWriter.WriteEndElement();

                _XmlWriter.WriteElementString("WholeSearchAreaCenterX", InspMapDataParam[_ID].Whole.SearchAreaCenterX.ToString());
                _XmlWriter.WriteElementString("WholeSearchAreaCenterY", InspMapDataParam[_ID].Whole.SearchAreaCenterY.ToString());
                _XmlWriter.WriteElementString("WholeSearchAreaWidth", InspMapDataParam[_ID].Whole.SearchAreaWidth.ToString());
                _XmlWriter.WriteElementString("WholeSearchAreaHeight", InspMapDataParam[_ID].Whole.SearchAreaHeight.ToString());
                _XmlWriter.WriteElementString("FindCount", InspMapDataParam[_ID].Info.FindCount.ToString());
                _XmlWriter.WriteElementString("FindScore", InspMapDataParam[_ID].Info.FindScore.ToString());
                _XmlWriter.WriteElementString("AngleLimit", InspMapDataParam[_ID].Info.AngleLimit.ToString());

                _XmlWriter.WriteElementString("IsUsableMapID", InspMapDataParam[_ID].MapID.IsUsableMapID.ToString());
                _XmlWriter.WriteElementString("SearchAreaCenterX", InspMapDataParam[_ID].MapID.SearchAreaCenterX.ToString());
                _XmlWriter.WriteElementString("SearchAreaCenterY", InspMapDataParam[_ID].MapID.SearchAreaCenterY.ToString());
                _XmlWriter.WriteElementString("SearchAreaWidth", InspMapDataParam[_ID].MapID.SearchAreaWidth.ToString());
                _XmlWriter.WriteElementString("SearchAreaHeight", InspMapDataParam[_ID].MapID.SearchAreaHeight.ToString());
                _XmlWriter.WriteElementString("SearhDirection", InspMapDataParam[_ID].MapID.SearhDirection.ToString());
                _XmlWriter.WriteElementString("SearchThreshold", InspMapDataParam[_ID].MapID.SearchThreshold.ToString());
                _XmlWriter.WriteElementString("SearchSizeMin", InspMapDataParam[_ID].MapID.SearchSizeMin.ToString());
                _XmlWriter.WriteElementString("SearchSizeMax", InspMapDataParam[_ID].MapID.SearchSizeMax.ToString());
                _XmlWriter.WriteElementString("BlobAreaSizeMin", InspMapDataParam[_ID].MapID.BlobAreaSizeMin.ToString());
                _XmlWriter.WriteElementString("BlobAreaSizeMax", InspMapDataParam[_ID].MapID.BlobAreaSizeMax.ToString());
                _XmlWriter.WriteStartElement("MapIDInfoList");
                {
                    for (int iLoopCount = 0; iLoopCount < InspMapDataParam[_ID].MapID.MapIDInfoList.Count; ++iLoopCount)
                    {
                        _XmlWriter.WriteStartElement("MapIDInfo" + iLoopCount);
                        {
                            _XmlWriter.WriteElementString("X", InspMapDataParam[_ID].MapID.MapIDInfoList[iLoopCount].CenterPt.X.ToString());
                            _XmlWriter.WriteElementString("Y", InspMapDataParam[_ID].MapID.MapIDInfoList[iLoopCount].CenterPt.Y.ToString());
                            _XmlWriter.WriteElementString("W", InspMapDataParam[_ID].MapID.MapIDInfoList[iLoopCount].Width.ToString());
                            _XmlWriter.WriteElementString("H", InspMapDataParam[_ID].MapID.MapIDInfoList[iLoopCount].Height.ToString());
                        }
                        _XmlWriter.WriteEndElement();
                    }
                }
                _XmlWriter.WriteEndElement();
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion Read & Write Map Data Parameter

        #endregion Read & Write InspectionParameter

        #region RecipeCopy
        /// <summary>
        /// Inspection Parameter (Recipe) 복사
        /// </summary>
        /// <param name="_SrcParam">원본 Recipe</param>
        /// <param name="_DestParam">저장 할 Recipe</param>
        public static void RecipeCopy(InspectionParameter _SrcParam, ref InspectionParameter _DestParam)
        {
            _DestParam.InspAreaParam.Clear();

            _DestParam.ResolutionX = _SrcParam.ResolutionX;
            _DestParam.ResolutionY = _SrcParam.ResolutionY;

            for (int iLoopCount = 0; iLoopCount < _SrcParam.InspAreaParam.Count; ++iLoopCount)
            {
                InspectionAreaParameter _InspAreaParam = new InspectionAreaParameter();
                _InspAreaParam.Enable                = _SrcParam.InspAreaParam[iLoopCount].Enable;
                _InspAreaParam.NgAreaNumber          = _SrcParam.InspAreaParam[iLoopCount].NgAreaNumber;
                _InspAreaParam.AreaBenchMark         = _SrcParam.InspAreaParam[iLoopCount] .AreaBenchMark;
                _InspAreaParam.AreaRegionCenterX     = _SrcParam.InspAreaParam[iLoopCount].AreaRegionCenterX;
                _InspAreaParam.AreaRegionCenterY     = _SrcParam.InspAreaParam[iLoopCount].AreaRegionCenterY;
                _InspAreaParam.AreaRegionWidth       = _SrcParam.InspAreaParam[iLoopCount].AreaRegionWidth;
                _InspAreaParam.AreaRegionHeight      = _SrcParam.InspAreaParam[iLoopCount].AreaRegionHeight;
                _InspAreaParam.BaseIndexNumber       = _SrcParam.InspAreaParam[iLoopCount].BaseIndexNumber;
                _InspAreaParam.IsUseMapData          = _SrcParam.InspAreaParam[iLoopCount].IsUseMapData;
                _InspAreaParam.MapDataUnitTotalCount = _SrcParam.InspAreaParam[iLoopCount].MapDataUnitTotalCount;
                _InspAreaParam.MapDataStartNumber    = _SrcParam.InspAreaParam[iLoopCount].MapDataStartNumber;
                _InspAreaParam.MapDataEndNumber      = _SrcParam.InspAreaParam[iLoopCount].MapDataEndNumber;

                for (int jLoopCount = 0; jLoopCount < _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                {
                    InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter();
                    _InspAlgoParam.AlgoEnable           = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoEnable;
                    _InspAlgoParam.AlgoBenchMark        = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoBenchMark;
                    _InspAlgoParam.AlgoType             = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoType;
                    _InspAlgoParam.AlgoRegionCenterX    = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionCenterX;
                    _InspAlgoParam.AlgoRegionCenterY    = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionCenterY;
                    _InspAlgoParam.AlgoRegionWidth      = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionWidth;
                    _InspAlgoParam.AlgoRegionHeight     = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionHeight;

                    eAlgoType _AlgoType = (eAlgoType)_InspAlgoParam.AlgoType;
                    if (eAlgoType.C_PATTERN == _AlgoType)
                    {
                        #region Pattern Algorithm Copy
                        _InspAlgoParam.Algorithm = new CogPatternAlgo();
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).PatternCount     = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).PatternCount;
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).PatternCount     = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).PatternCount;
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).MatchingScore    = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).MatchingScore;
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).MatchingAngle    = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).MatchingAngle;
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).MatchingCount    = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).MatchingCount;
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).IsShift          = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).IsShift;
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).AllowedShiftX    = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).AllowedShiftX;
                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).AllowedShiftY    = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).AllowedShiftY;

                        int _ReferCount = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList.Count;
                        for (int zLoopCount = 0; zLoopCount < _ReferCount; ++zLoopCount)
                        {
                            ReferenceInformation _ReferInfo = new ReferenceInformation();
                            _ReferInfo.ReferencePath     = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].ReferencePath;
                            _ReferInfo.Reference         = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].Reference;
                            _ReferInfo.InterActiveStartX = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].InterActiveStartX;
                            _ReferInfo.InterActiveStartY = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].InterActiveStartY;
                            _ReferInfo.StaticStartX      = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].StaticStartX;
                            _ReferInfo.StaticStartY      = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].StaticStartY;
                            _ReferInfo.CenterX           = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].CenterX;
                            _ReferInfo.CenterY           = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].CenterY;
                            _ReferInfo.OriginPointOffsetX= ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].OriginPointOffsetX;
                            _ReferInfo.OriginPointOffsetY= ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].OriginPointOffsetY;
                            _ReferInfo.Width             = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].Width;
                            _ReferInfo.Height            = ((CogPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].Height;

                            ((CogPatternAlgo)_InspAlgoParam.Algorithm).ReferenceInfoList.Add(_ReferInfo);
                        }
                        #endregion Pattern Algorithm Copy
                    }

                    if (eAlgoType.C_MULTI_PATTERN == _AlgoType)
                    {
                        #region Pattern Algorithm Copy
                        _InspAlgoParam.Algorithm = new CogMultiPatternAlgo();
                        ((CogMultiPatternAlgo)_InspAlgoParam.Algorithm).PatternCount = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).PatternCount;
                        ((CogMultiPatternAlgo)_InspAlgoParam.Algorithm).MatchingScore = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).MatchingScore;
                        ((CogMultiPatternAlgo)_InspAlgoParam.Algorithm).MatchingAngle = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).MatchingAngle;
                        ((CogMultiPatternAlgo)_InspAlgoParam.Algorithm).MatchingCount = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).MatchingCount;
                        ((CogMultiPatternAlgo)_InspAlgoParam.Algorithm).TwoPointAngle = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).TwoPointAngle;

                        int _ReferCount = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList.Count;
                        for (int zLoopCount = 0; zLoopCount < _ReferCount; ++zLoopCount)
                        {
                            ReferenceInformation _ReferInfo = new ReferenceInformation();
                            _ReferInfo.ReferencePath = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].ReferencePath;
                            _ReferInfo.Reference = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].Reference;
                            _ReferInfo.ReferencePath = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].ReferencePath;
                            _ReferInfo.InterActiveStartX = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].InterActiveStartX;
                            _ReferInfo.InterActiveStartY = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].InterActiveStartY;
                            _ReferInfo.StaticStartX = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].StaticStartX;
                            _ReferInfo.StaticStartY = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].StaticStartY;
                            _ReferInfo.CenterX = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].CenterX;
                            _ReferInfo.CenterY = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].CenterY;
                            _ReferInfo.OriginPointOffsetX = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].OriginPointOffsetX;
                            _ReferInfo.OriginPointOffsetY = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].OriginPointOffsetY;
                            _ReferInfo.Width = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].Width;
                            _ReferInfo.Height = ((CogMultiPatternAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ReferenceInfoList[zLoopCount].Height;

                            ((CogMultiPatternAlgo)_InspAlgoParam.Algorithm).ReferenceInfoList.Add(_ReferInfo);
                        }
                        #endregion Pattern Algorithm Copy
                    }

                    else if (eAlgoType.C_BLOB == _AlgoType)
                    {
                        #region Blob Algorithm Copy
                        _InspAlgoParam.Algorithm = new CogBlobAlgo();
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).ForeGround    = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ForeGround;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).ThresholdMin  = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ThresholdMin;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).ThresholdMax  = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ThresholdMax;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).BlobAreaMin   = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BlobAreaMin;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).BlobAreaMax   = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BlobAreaMax;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).WidthMin      = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).WidthMin;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).WidthMax      = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).WidthMax;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).HeightMin     = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).HeightMin;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).HeightMax     = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).HeightMax;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).OriginX       = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).OriginX;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).OriginY       = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).OriginY;
                        ((CogBlobAlgo)_InspAlgoParam.Algorithm).BenchMarkPosition = ((CogBlobAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BenchMarkPosition;
                        #endregion Blob Algorithm Copy
                    }

                    else if (eAlgoType.C_BLOB_REFER == _AlgoType)
                    {
                        #region Blob Reference Algorithm Copy
                        _InspAlgoParam.Algorithm = new CogBlobReferenceAlgo();
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ForeGround    = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ForeGround;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ThresholdMin  = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ThresholdMin;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ThresholdMax  = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ThresholdMax;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BlobAreaMin   = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BlobAreaMin;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BlobAreaMax   = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BlobAreaMax;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).Width = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).Width;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).WidthPos = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).WidthPos;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).WidthNeg = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).WidthNeg;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).Height = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).Height;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).HeightPos = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).HeightPos;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).HeightNeg = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).HeightNeg;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).OriginX       = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).OriginX;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).OriginY       = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).OriginY;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BenchMarkPosition = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BenchMarkPosition;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BodyArea       = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BodyArea;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BodyWidth      = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BodyWidth;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BodyHeight     = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BodyHeight;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BodyAreaPermitPercent   = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BodyAreaPermitPercent;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BodyWidthPermitPercent  = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BodyWidthPermitPercent;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BodyHeightPermitPercent = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).BodyHeightPermitPercent;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).DummyHistoMeanValue     = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).DummyHistoMeanValue;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).UseBodyArea             = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).UseBodyArea;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).UseBodyWidth            = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).UseBodyWidth;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).UseBodyHeight           = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).UseBodyHeight;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).UseDummyValue           = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).UseDummyValue;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ResolutionX             = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ResolutionX;
                        ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ResolutionY             = ((CogBlobReferenceAlgo)_SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm).ResolutionY;
                        #endregion Blob Reference Algorithm Copy
                    }
                    
                    else if (eAlgoType.C_ELLIPSE == _AlgoType)
                    {
                        #region Ellipse Algorithm Copy
                        var _Algorithm = _InspAlgoParam.Algorithm as CogEllipseAlgo;
                        var _SrcAlgorithm = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm as CogEllipseAlgo;

                        _Algorithm = new CogEllipseAlgo();
                        _Algorithm.CaliperNumber = _SrcAlgorithm.CaliperNumber;
                        _Algorithm.CaliperSearchLength = _SrcAlgorithm.CaliperSearchLength;
                        _Algorithm.CaliperProjectionLength = _SrcAlgorithm.CaliperProjectionLength;
                        _Algorithm.CaliperSearchDirection = _SrcAlgorithm.CaliperSearchDirection;
                        _Algorithm.CaliperPolarity = _SrcAlgorithm.CaliperPolarity;
                        _Algorithm.CaliperIgnoreNumber = _SrcAlgorithm.CaliperIgnoreNumber;
                        _Algorithm.ArcCenterX = _SrcAlgorithm.ArcCenterX;
                        _Algorithm.ArcCenterY = _SrcAlgorithm.ArcCenterY;
                        _Algorithm.ArcRadiusX = _SrcAlgorithm.ArcRadiusX;
                        _Algorithm.ArcRadiusY = _SrcAlgorithm.ArcRadiusY;
                        _Algorithm.ArcAngleSpan = _SrcAlgorithm.ArcAngleSpan;
                        _Algorithm.OriginX = _SrcAlgorithm.OriginX;
                        _Algorithm.OriginY = _SrcAlgorithm.OriginY;
                        _Algorithm.OriginRadiusX = _SrcAlgorithm.OriginRadiusX;
                        _Algorithm.OriginRadiusY = _SrcAlgorithm.OriginRadiusY;
                        _Algorithm.DiameterSize = _SrcAlgorithm.DiameterSize;
                        _Algorithm.DiameterMinus = _SrcAlgorithm.DiameterMinus;
                        _Algorithm.DiameterPlus = _SrcAlgorithm.DiameterPlus;

                        _InspAlgoParam.Algorithm = _Algorithm;
                        #endregion
                    }
                    
                    else if (eAlgoType.C_ID == _AlgoType)
                    {
                        var _Algorithm = _InspAlgoParam.Algorithm as CogBarCodeIDAlgo;
                        var _SrcAlgorithm = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm as CogBarCodeIDAlgo;

                        _Algorithm = new CogBarCodeIDAlgo();
                        _Algorithm.Symbology = _SrcAlgorithm.Symbology;
                        _Algorithm.OriginX = _SrcAlgorithm.OriginX;
                        _Algorithm.OriginY = _SrcAlgorithm.OriginY;
                        _Algorithm.TimeLimit = _SrcAlgorithm.TimeLimit;
                        _Algorithm.FindCount = _SrcAlgorithm.FindCount;

                        _InspAlgoParam.Algorithm = _Algorithm;
                    }

                    else if (eAlgoType.C_LINE_FIND == _AlgoType)
                    {
                        var _Algorithm = _InspAlgoParam.Algorithm as CogLineFindAlgo;
                        var _SrcAlgorithm = _SrcParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].Algorithm as CogLineFindAlgo;

                        _Algorithm = new CogLineFindAlgo();
                        _Algorithm.CaliperNumber = _SrcAlgorithm.CaliperNumber;
                        _Algorithm.CaliperSearchLength = _SrcAlgorithm.CaliperSearchLength;
                        _Algorithm.CaliperProjectionLength = _SrcAlgorithm.CaliperProjectionLength;
                        _Algorithm.CaliperSearchDirection = _SrcAlgorithm.CaliperSearchDirection;
                        _Algorithm.IgnoreNumber = _SrcAlgorithm.IgnoreNumber;
                        _Algorithm.CaliperLineStartX = _SrcAlgorithm.CaliperLineStartX;
                        _Algorithm.CaliperLineStartY = _SrcAlgorithm.CaliperLineStartY;
                        _Algorithm.CaliperLineEndX = _SrcAlgorithm.CaliperLineEndX;
                        _Algorithm.CaliperLineEndY = _SrcAlgorithm.CaliperLineEndY;
                        _Algorithm.ContrastThreshold = _SrcAlgorithm.ContrastThreshold;
                        _Algorithm.FilterHalfSizePixels = _SrcAlgorithm.FilterHalfSizePixels;
                        _Algorithm.UseAlignment = _SrcAlgorithm.UseAlignment;

                        _InspAlgoParam.Algorithm = _Algorithm;
                    }

                    _InspAreaParam.InspAlgoParam.Add(_InspAlgoParam);
                }
                _DestParam.InspAreaParam.Add(_InspAreaParam);
            }
        }


        public static void RecipeCopy(InspectionAlgorithmParameter _SrcParam, ref InspectionAlgorithmParameter _DestParam, double _OffsetX = 0, double _OffsetY = 0)
        {
            _DestParam.AlgoEnable        = _SrcParam.AlgoEnable;
            _DestParam.AlgoBenchMark     = _SrcParam.AlgoBenchMark;
            _DestParam.AlgoType          = _SrcParam.AlgoType;
            _DestParam.AlgoRegionCenterX = _SrcParam.AlgoRegionCenterX;
            _DestParam.AlgoRegionCenterY = _SrcParam.AlgoRegionCenterY;
            _DestParam.AlgoRegionWidth   = _SrcParam.AlgoRegionWidth;
            _DestParam.AlgoRegionHeight  = _SrcParam.AlgoRegionHeight;

            eAlgoType _AlgoType = (eAlgoType)_DestParam.AlgoType;
            if (eAlgoType.C_PATTERN == _AlgoType)
            {
                #region Pattern Algorithm Copy
                _DestParam.Algorithm = new CogPatternAlgo();
                ((CogPatternAlgo)_DestParam.Algorithm).PatternCount  = ((CogPatternAlgo)_SrcParam.Algorithm).PatternCount;
                ((CogPatternAlgo)_DestParam.Algorithm).MatchingScore = ((CogPatternAlgo)_SrcParam.Algorithm).MatchingScore;
                ((CogPatternAlgo)_DestParam.Algorithm).MatchingAngle = ((CogPatternAlgo)_SrcParam.Algorithm).MatchingAngle;
                ((CogPatternAlgo)_DestParam.Algorithm).MatchingCount = ((CogPatternAlgo)_SrcParam.Algorithm).MatchingCount;
                ((CogPatternAlgo)_DestParam.Algorithm).IsShift       = ((CogPatternAlgo)_SrcParam.Algorithm).IsShift;
                ((CogPatternAlgo)_DestParam.Algorithm).AllowedShiftX = ((CogPatternAlgo)_SrcParam.Algorithm).AllowedShiftX;
                ((CogPatternAlgo)_DestParam.Algorithm).AllowedShiftY = ((CogPatternAlgo)_SrcParam.Algorithm).AllowedShiftY;

                int _ReferCount = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList.Count;
                for (int iLoopCount = 0; iLoopCount < _ReferCount; ++iLoopCount)
                {
                    ReferenceInformation _ReferInfo = new ReferenceInformation();
                    _ReferInfo.ReferencePath = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].ReferencePath;
                    _ReferInfo.Reference = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].Reference;

                    _ReferInfo.InterActiveStartX    = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].InterActiveStartX;
                    _ReferInfo.InterActiveStartY    = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].InterActiveStartY;
                    _ReferInfo.StaticStartX         = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].StaticStartX + _OffsetX;
                    _ReferInfo.StaticStartY         = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].StaticStartY + _OffsetY;
                    _ReferInfo.CenterX              = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].CenterX + _OffsetX;
                    _ReferInfo.CenterY              = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].CenterY + _OffsetY;
                    _ReferInfo.OriginPointOffsetX   = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].OriginPointOffsetX;
                    _ReferInfo.OriginPointOffsetY   = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].OriginPointOffsetY;
                    _ReferInfo.Width                = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].Width;
                    _ReferInfo.Height               = ((CogPatternAlgo)_SrcParam.Algorithm).ReferenceInfoList[iLoopCount].Height;

                    ((CogPatternAlgo)_DestParam.Algorithm).ReferenceInfoList.Add(_ReferInfo);
                }
                #endregion Pattern Algorithm Copy
            }
        }

        /// <summary>
        /// Map Data 적용 용 InspectionAlgorithmParameter (Recipe) 복사
        /// </summary>
        /// <param name="_SrcParam">원본 Recipe</param>
        /// <param name="_DestParam">저장 할 Recipe</param>
        public static void RecipeCopy(List<InspectionAlgorithmParameter> _SrcParam, ref List<InspectionAlgorithmParameter> _DestParam, double _AreaCenterX, double _AreaCenterY)
        {
            _DestParam = new List<InspectionAlgorithmParameter>();

            for (int iLoopCount = 0; iLoopCount < _SrcParam.Count; ++iLoopCount)
            {
                InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter();
                double _OffsetX = _SrcParam[iLoopCount].AlgoRegionCenterX - _AreaCenterX;
                double _OffsetY = _SrcParam[iLoopCount].AlgoRegionCenterY - _AreaCenterY;

                _InspAlgoParam.AlgoEnable        = _SrcParam[iLoopCount].AlgoEnable;
                _InspAlgoParam.AlgoBenchMark     = _SrcParam[iLoopCount].AlgoBenchMark;
                _InspAlgoParam.AlgoType          = _SrcParam[iLoopCount].AlgoType;
                _InspAlgoParam.AlgoRegionCenterX = _SrcParam[iLoopCount].AlgoRegionCenterX + _OffsetX;
                _InspAlgoParam.AlgoRegionCenterY = _SrcParam[iLoopCount].AlgoRegionCenterY + _OffsetY;
                _InspAlgoParam.AlgoRegionWidth   = _SrcParam[iLoopCount].AlgoRegionWidth;
                _InspAlgoParam.AlgoRegionHeight  = _SrcParam[iLoopCount].AlgoRegionHeight;

                eAlgoType _AlgoType = (eAlgoType)_InspAlgoParam.AlgoType;
                if (eAlgoType.C_PATTERN == _AlgoType)
                {
                    #region Pattern Algorithm Copy
                    _InspAlgoParam.Algorithm = new CogPatternAlgo();
                    ((CogPatternAlgo)_InspAlgoParam.Algorithm).PatternCount  = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).PatternCount;
                    ((CogPatternAlgo)_InspAlgoParam.Algorithm).MatchingScore = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).MatchingScore;
                    ((CogPatternAlgo)_InspAlgoParam.Algorithm).MatchingAngle = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).MatchingAngle;
                    ((CogPatternAlgo)_InspAlgoParam.Algorithm).MatchingCount = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).MatchingCount;
                    ((CogPatternAlgo)_InspAlgoParam.Algorithm).IsShift = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).IsShift;
                    ((CogPatternAlgo)_InspAlgoParam.Algorithm).AllowedShiftX = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).AllowedShiftX;
                    ((CogPatternAlgo)_InspAlgoParam.Algorithm).AllowedShiftY = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).AllowedShiftY;

                    int _ReferCount = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList.Count;
                    for (int jLoopCount = 0; jLoopCount < _ReferCount; ++jLoopCount)
                    {
                        ReferenceInformation _ReferInfo = new ReferenceInformation();
                        _ReferInfo.ReferencePath = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].ReferencePath;
                        _ReferInfo.Reference = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].Reference;
                        _ReferInfo.InterActiveStartX = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].InterActiveStartX;
                        _ReferInfo.InterActiveStartY = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].InterActiveStartY;
                        _ReferInfo.StaticStartX = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].StaticStartX + _OffsetX;
                        _ReferInfo.StaticStartY = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].StaticStartY + _OffsetY;
                        _ReferInfo.CenterX = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].CenterX + _OffsetX;
                        _ReferInfo.CenterY = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].CenterY + _OffsetY;
                        _ReferInfo.OriginPointOffsetX = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].OriginPointOffsetX;
                        _ReferInfo.OriginPointOffsetY = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].OriginPointOffsetY;
                        _ReferInfo.Width = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].Width;
                        _ReferInfo.Height = ((CogPatternAlgo)_SrcParam[iLoopCount].Algorithm).ReferenceInfoList[jLoopCount].Height;

                        ((CogPatternAlgo)_InspAlgoParam.Algorithm).ReferenceInfoList.Add(_ReferInfo);
                    }
                    #endregion Pattern Algorithm Copy
                }

                _DestParam.Add(_InspAlgoParam);
            }
        }

        /// <summary>
        /// Map Data Parameter (Recipe) 복사
        /// </summary>
        /// <param name="_SrcParam">원본 Recipe</param>
        /// <param name="_DestParam">저장 할 Recipe</param>
        public static void RecipeCopy(MapDataParameter _SrcParam, ref MapDataParameter _DestParam)
        {
            if (null == _SrcParam) return;
            if (null == _DestParam) _DestParam = new MapDataParameter();
            _DestParam.Info.UnitListCenterX = new List<double>();
            _DestParam.Info.UnitListCenterY = new List<double>();
            _DestParam.Info.UnitListWidth = new List<double>();
            _DestParam.Info.UnitListHeight = new List<double>();
            _DestParam.MapID.MapIDInfoList = new List<MapIDRectInfo>();

            _DestParam.Info.UnitPattern         = _SrcParam.Info.UnitPattern;
            _DestParam.Info.UnitPatternPath     = _SrcParam.Info.UnitPatternPath;
            _DestParam.Info.UnitTotalCount      = _SrcParam.Info.UnitTotalCount;
            _DestParam.Info.UnitRowCount        = _SrcParam.Info.UnitRowCount;
            _DestParam.Info.UnitColumnCount     = _SrcParam.Info.UnitColumnCount;
            _DestParam.Info.SectionRowCount     = _SrcParam.Info.SectionRowCount;
            _DestParam.Info.SectionColumnCount  = _SrcParam.Info.SectionColumnCount;
            _DestParam.Info.SearchType          = _SrcParam.Info.SearchType;
            _DestParam.Info.MapDataTeachingMode = _SrcParam.Info.MapDataTeachingMode;
            _DestParam.Unit.SearchAreaCenterX   = _SrcParam.Unit.SearchAreaCenterX;
            _DestParam.Unit.SearchAreaCenterY   = _SrcParam.Unit.SearchAreaCenterY;
            _DestParam.Unit.SearchAreaWidth     = _SrcParam.Unit.SearchAreaWidth;
            _DestParam.Unit.SearchAreaHeight    = _SrcParam.Unit.SearchAreaHeight;
            _DestParam.Unit.PatternAreaOriginX  = _SrcParam.Unit.PatternAreaOriginX;
            _DestParam.Unit.PatternAreaOriginY  = _SrcParam.Unit.PatternAreaOriginY;
            _DestParam.Unit.PatternAreaCenterX  = _SrcParam.Unit.PatternAreaCenterX;
            _DestParam.Unit.PatternAreaCenterY  = _SrcParam.Unit.PatternAreaCenterY;
            _DestParam.Unit.PatternAreaWidth    = _SrcParam.Unit.PatternAreaWidth;
            _DestParam.Unit.PatternAreaHeight   = _SrcParam.Unit.PatternAreaHeight;
            _DestParam.Whole.SearchAreaCenterX  = _SrcParam.Whole.SearchAreaCenterX;
            _DestParam.Whole.SearchAreaCenterY  = _SrcParam.Whole.SearchAreaCenterY;
            _DestParam.Whole.SearchAreaWidth    = _SrcParam.Whole.SearchAreaWidth;
            _DestParam.Whole.SearchAreaHeight   = _SrcParam.Whole.SearchAreaHeight;
            _DestParam.Info.FindCount           = _SrcParam.Info.FindCount;
            _DestParam.Info.FindScore           = _SrcParam.Info.FindScore;
            _DestParam.Info.AngleLimit          = _SrcParam.Info.AngleLimit;
            _DestParam.MapID.SearchAreaCenterX  = _SrcParam.MapID.SearchAreaCenterX;
            _DestParam.MapID.SearchAreaCenterY  = _SrcParam.MapID.SearchAreaCenterY;
            _DestParam.MapID.SearchAreaWidth    = _SrcParam.MapID.SearchAreaWidth;
            _DestParam.MapID.SearchAreaHeight   = _SrcParam.MapID.SearchAreaHeight;
            _DestParam.MapID.SearhDirection     = _SrcParam.MapID.SearhDirection;
            _DestParam.MapID.SearchThreshold    = _SrcParam.MapID.SearchThreshold;
            _DestParam.MapID.SearchSizeMin      = _SrcParam.MapID.SearchSizeMin;
            _DestParam.MapID.SearchSizeMax      = _SrcParam.MapID.SearchSizeMax;
            _DestParam.MapID.BlobAreaSizeMin    = _SrcParam.MapID.BlobAreaSizeMin;
            _DestParam.MapID.BlobAreaSizeMax    = _SrcParam.MapID.BlobAreaSizeMax;
            _DestParam.MapID.IsUsableMapID      = _SrcParam.MapID.IsUsableMapID;

            if (_SrcParam.Info.UnitListCenterX.Count == _SrcParam.Info.UnitListWidth.Count)
            {
                for (int iLoopCount = 0; iLoopCount < _SrcParam.Info.UnitListCenterX.Count; ++iLoopCount)
                {
                    _DestParam.Info.UnitListCenterX.Add(_SrcParam.Info.UnitListCenterX[iLoopCount]);
                    _DestParam.Info.UnitListCenterY.Add(_SrcParam.Info.UnitListCenterY[iLoopCount]);
                    _DestParam.Info.UnitListWidth.Add(_SrcParam.Info.UnitListWidth[iLoopCount]);
                    _DestParam.Info.UnitListHeight.Add(_SrcParam.Info.UnitListHeight[iLoopCount]);
                }
                for (int iLoopCount = 0; iLoopCount < _SrcParam.MapID.MapIDInfoList.Count; ++iLoopCount)
                {
                    MapIDRectInfo _MapIDInfo = new MapIDRectInfo();
                    _MapIDInfo.CenterPt.X = _SrcParam.MapID.MapIDInfoList[iLoopCount].CenterPt.X;
                    _MapIDInfo.CenterPt.Y = _SrcParam.MapID.MapIDInfoList[iLoopCount].CenterPt.Y;
                    _MapIDInfo.Width = _SrcParam.MapID.MapIDInfoList[iLoopCount].Width;
                    _MapIDInfo.Height = _SrcParam.MapID.MapIDInfoList[iLoopCount].Height;
                    _DestParam.MapID.MapIDInfoList.Add(_MapIDInfo);
                }
            }
        }
        #endregion RecipeCopy
    }
}

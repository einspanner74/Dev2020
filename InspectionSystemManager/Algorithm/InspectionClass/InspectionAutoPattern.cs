using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ImageProcessing;

using ParameterManager;
using LogMessageManager;

namespace InspectionSystemManager
{
    class InspectionAutoPattern
    {
        CogHistogram HistogramProc;
        CogHistogramResult HistogramResult;
        CogIPOneImageTool OneImageProc;
        CogBlob BlobProc;
        CogBlobResults BlobResults;
        CogBlobResult BlobResult;
        CogPMAlignTool PMAlignProc;
        CogPMAlignResult PMAlignResult;

        public InspectionAutoPattern()
        {
            HistogramProc = new CogHistogram();
            HistogramResult = new CogHistogramResult();

            OneImageProc = new CogIPOneImageTool();

            BlobProc = new CogBlob();
            BlobResults = new CogBlobResults();
            BlobResult = new CogBlobResult();

            PMAlignProc = new CogPMAlignTool();
            PMAlignProc.Pattern.TrainAlgorithm = CogPMAlignTrainAlgorithmConstants.PatMax;
            PMAlignResult = new CogPMAlignResult();
        }

        public void Initialize()
        {
            
        }

        public void DeInitialize()
        {
            OneImageProc.Dispose();
            BlobProc.Dispose();
            BlobResults.Dispose();
            BlobResult.Dispose();
            PMAlignProc.Dispose();
        }

        //LDH, 2108.11.07, Auto Pattern Algo
        public bool AutoPatternFind(CogImage8Grey _SrcImage, CogRectangle _InspRegion, CogAutoPatternAlgo _CogAutoPatternAlgo, ref CogAutoPatternResult _CogAutoPatternResult)
        {
            bool _Result = false;

            //ㅋ태챠퍼ㅐ젇라ㅓㅣㅏ ㅁ~~~
            if (Inspection(_SrcImage, _InspRegion, _CogAutoPatternAlgo.ReferenceInfoList[0].Reference) == false) return _Result;
            
            Run(_SrcImage, _InspRegion, _CogAutoPatternAlgo, ref _CogAutoPatternResult);

            return _Result;
        }

        //LDH, 2018.11.07, Pattern Find. 검사할 때 단순 Pattern 검색용으로 사용
        public bool Run(CogImage8Grey _SrcImage, CogRectangle _InspRegion, CogAutoPatternAlgo _CogAutoPatternAlgo, ref CogAutoPatternResult _CogAutoPatternResult)
        {
            bool _Result = false;

            return _Result;
        }

        private bool Inspection(CogImage8Grey _SrcImage, CogRectangle _Region, CogPMAlignPattern _Pattern)
        {
            bool _Result = false;

            HistogramResult = HistogramProc.Execute(_SrcImage, _Region);
            //HistogramResult.Mean;

            OneImageProc.InputImage = _SrcImage;
            //OneImageProc.Operators.Add()

            return _Result;
        }
    }
}

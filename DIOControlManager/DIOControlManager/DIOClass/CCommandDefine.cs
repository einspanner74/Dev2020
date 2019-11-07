using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParameterManager;

namespace DIOControlManager
{
    public class SignalToggleData
    {
        public short Signal;
        public int ToggleTime;
        public bool  CurrentSignal;
    }

    public class DIOBaseCommand
    {
        protected int IOCount = 16;
        protected int[] InCmdArray;
        protected int[] OutCmdArray;

        public DIOBaseCommand(int _IOCount = 8)
        {
            IOCount = _IOCount;
            InCmdArray = new int[IOCount];
            OutCmdArray = new int[IOCount];
        }

        public int InputBitCheck(int _BitNumber)
        {
            int _ReturnBit = 0;
            _ReturnBit = InCmdArray[_BitNumber];
            return _ReturnBit;
        }

        public int OutputBitCheck(int _BitNumber)
        {
            int _ReturnBit = 0;
            _ReturnBit = OutCmdArray[_BitNumber];
            return _ReturnBit;
        }

        public int OutputBitIndexCheck(int _CheckBit)
        {
            int _ReternIndex = 0;
            
            for (int iLoopCount = 0; iLoopCount < OutCmdArray.Length; ++iLoopCount)
            {
                if (OutCmdArray[iLoopCount] == _CheckBit)
                {
                    _ReternIndex = iLoopCount;
                    break;
                }
            }
            return _ReternIndex;
        }
    }

    public class DefaultCmd: DIOBaseCommand
    {
        public static readonly int IN_LIVE      = 0;
        public static readonly int IN_RESET     = 1;
        public static readonly int IN_TRIGGER   = 2;
        public static readonly int IN_REQUEST   = 3;

        public static readonly int OUT_LIVE     = 0;
        public static readonly int OUT_READY    = 1;
        public static readonly int OUT_COMPLETE = 2;
        public static readonly int OUT_RESULT   = 3;

        public DefaultCmd(int _IOCount)
        {
            IOCount = _IOCount;

            InCmdArray = new int[IOCount];
            for (int iLoopCount = 0; iLoopCount < _IOCount; ++iLoopCount) InCmdArray[iLoopCount] = DIO_DEF.NONE;
            InCmdArray[IN_LIVE]     = DIO_DEF.IN_LIVE;
            InCmdArray[IN_RESET]    = DIO_DEF.IN_RESET;
            InCmdArray[IN_TRIGGER]  = DIO_DEF.IN_TRG;


            OutCmdArray = new int[IOCount];
            for (int iLoopCount = 0; iLoopCount < _IOCount; ++iLoopCount) OutCmdArray[iLoopCount] = DIO_DEF.NONE;
            OutCmdArray[OUT_LIVE]       = DIO_DEF.OUT_LIVE;
            OutCmdArray[OUT_READY]      = DIO_DEF.OUT_READY;
            OutCmdArray[OUT_COMPLETE]   = DIO_DEF.OUT_COMPLETE;
            OutCmdArray[OUT_RESULT]     = DIO_DEF.OUT_RESULT;
        }
    }

    public class NavienCmd : DIOBaseCommand
    {
        public static readonly int IN_TRIGGER = 0;

        public static readonly int OUT_ERROR = 0;
        public static readonly int OUT_READY = 1;
        public static readonly int OUT_GOOD = 2;

        public NavienCmd(int _IOCount)
        {
            IOCount = _IOCount;

            InCmdArray = new int[IOCount];
            for (int iLoopCount = 0; iLoopCount < _IOCount; ++iLoopCount) InCmdArray[iLoopCount] = DIO_DEF.NONE;

            InCmdArray[IN_TRIGGER] = DIO_DEF.IN_TRG;

            OutCmdArray = new int[IOCount];
            for (int iLoopCount = 0; iLoopCount < _IOCount; ++iLoopCount) OutCmdArray[iLoopCount] = DIO_DEF.NONE;

            OutCmdArray[OUT_ERROR] = DIO_DEF.OUT_ERROR;
            OutCmdArray[OUT_READY] = DIO_DEF.OUT_READY;
            OutCmdArray[OUT_GOOD]  = DIO_DEF.OUT_GOOD;
        }
    }
}

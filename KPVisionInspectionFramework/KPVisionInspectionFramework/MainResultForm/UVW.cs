using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPVisionInspectionFramework
{
    public class UVW
    {
        //Stage 거리
        private double StageDistanceWidth;
        private double StageDistanceHeight;

        //UVW 방향
        private int UAxisDirectionX;
        private int UAxisDirectionY;
        private int VAxisDirectionX;
        private int VAxisDirectionY;
        private int WAxisDirectionX;
        private int WAxisDirectionY;

        //계산값
        private double CenterRadiius;
        private double CenterToUAxisRealDistance;
        private double CenterToVAxisRealDistance;
        private double CenterToWAxisRealDistance;
        private double CenterToUAxisRealAngle;
        private double CenterToVAxisRealAngle;
        private double CenterToWAxisRealAngle;

        private double CurrentStageAngle;
        private double LastDegree;

        public UVW()
        {

        }

        public void Initialize(int _UAxisDirX, int _UAxisDirY, int _VAxisDirX)
        {
            UAxisDirectionX = _UAxisDirX;
            UAxisDirectionY = _UAxisDirY;
            VAxisDirectionX = _VAxisDirX;

            //CenterToUAxisRealAngle = 225;
            //CenterToVAxisRealAngle = 315;
            //CenterToWAxisRealAngle = 135;

            CenterToUAxisRealAngle = 45;
            CenterToVAxisRealAngle = 135;
            CenterToWAxisRealAngle = 315;

            CenterRadiius = 55;

            CenterToUAxisRealDistance = CenterRadiius;
            CenterToVAxisRealDistance = CenterRadiius;
            CenterToWAxisRealDistance = CenterRadiius;
        }

        public void Reset()
        {
            CurrentStageAngle = 0;
        }

        public void StageMove()
        {
            CurrentStageAngle += LastDegree;
        }

        public void GetUVW(double _MoveX, double _MoveY, double _MoveDeg, ref double _U, ref double _V, ref double _W)
        {
            //_U = CenterToUAxisRealDistance * Math.Sin(_MoveDeg + CenterToUAxisRealAngle + CurrentStageAngle) - CenterToUAxisRealDistance * Math.Sin(CenterToUAxisRealAngle + CurrentStageAngle);
            //_V = CenterToVAxisRealDistance * Math.Cos(_MoveDeg + CenterToVAxisRealAngle + CurrentStageAngle) - CenterToVAxisRealDistance * Math.Cos(CenterToVAxisRealAngle + CurrentStageAngle);
            //_W = CenterToWAxisRealDistance * Math.Cos(_MoveDeg + CenterToWAxisRealAngle + CurrentStageAngle) - CenterToWAxisRealDistance * Math.Cos(CenterToWAxisRealAngle + CurrentStageAngle);

            //if... X 축 방향
            //_V = (1 * _MoveY) + (1 * ((CenterToVAxisRealDistance * Math.Sin(DegToRad(_MoveDeg + CenterToVAxisRealAngle + CurrentStageAngle))) - (CenterToVAxisRealDistance * Math.Sin(DegToRad(CenterToVAxisRealAngle + CurrentStageAngle)))));

            ////if... Y1축 방향
            //_U = (1 * _MoveX) + (1 * ((CenterToUAxisRealDistance * Math.Cos(DegToRad(_MoveDeg + CenterToUAxisRealAngle + CurrentStageAngle))) - (CenterToUAxisRealDistance * Math.Cos(DegToRad(CenterToUAxisRealAngle + CurrentStageAngle)))));

            ////if... Y2축 방향
            //_W = (-1 * _MoveX) + (-1 * ((CenterToWAxisRealDistance * Math.Cos(DegToRad(_MoveDeg + CenterToWAxisRealAngle + CurrentStageAngle))) - (CenterToWAxisRealDistance * Math.Cos(DegToRad(CenterToWAxisRealAngle + CurrentStageAngle)))));



            _V = (1 * _MoveY) + (-1 * ((CenterToVAxisRealDistance * Math.Sin(DegToRad(_MoveDeg + CenterToVAxisRealAngle + CurrentStageAngle))) - (CenterToVAxisRealDistance * Math.Sin(DegToRad(CenterToVAxisRealAngle + CurrentStageAngle)))));

            //if... Y1축 방향
            _U = (1 * _MoveX) + (-1 * ((CenterToUAxisRealDistance * Math.Cos(DegToRad(_MoveDeg + CenterToUAxisRealAngle + CurrentStageAngle))) - (CenterToUAxisRealDistance * Math.Cos(DegToRad(CenterToUAxisRealAngle + CurrentStageAngle)))));

            //if... Y2축 방향
            _W = (-1 * _MoveX) + (1 * ((CenterToWAxisRealDistance * Math.Cos(DegToRad(_MoveDeg + CenterToWAxisRealAngle + CurrentStageAngle))) - (CenterToWAxisRealDistance * Math.Cos(DegToRad(CenterToWAxisRealAngle + CurrentStageAngle)))));

            LastDegree = _MoveDeg;
        }

        private double DegToRad(double _Degree)
        {
            return Math.PI * _Degree / 180.0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busBycycle
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] bycycleStation = { "E1", "E4", "E7", "E10","E12" };
            string start = "E11";
            string dst = "E2";

            int travelTime = getTravelTimeWitoutBus(bycycleStation, start, dst);
            int minTime = getTravelTime(bycycleStation, start, dst, "13:25");
            System.Console.WriteLine("도보 자전거: {0}", travelTime);
            System.Console.WriteLine("최소시간: {0}", minTime);
            System.Console.ReadLine();
        }

        static int getTravelTimeWitoutBus(string[] bycycleStation, string start, string destination)
        {

            int[] bycycleStationNum = new int[bycycleStation.Length];
            start = start.Replace("E","");
            destination = destination.Replace("E", "");

            int startNum = int.Parse(start);
            int dstNum = int.Parse(destination);

            start.ToCharArray();

            int i = 0;

            foreach (string s in bycycleStation)
            {
                string station = s.Remove(0, 1);
                bycycleStationNum[i] = int.Parse(station);
                i++;
            }

            //1.자전거를 탈경우
            int bycycleTravelTime = 0;

            //가까운 정거장 찾기

            int minStartIdx = 0;
            int minDistanceStart = Math.Abs(bycycleStationNum[0]-startNum);

            int minDstIdx = 0;
            int minDistanceDst = Math.Abs(bycycleStationNum[0] - dstNum);

            for (int j=0; j < bycycleStationNum.Length; j++)
            {
                int distanceStart = Math.Abs(bycycleStationNum[j] - startNum);
                int distanceDst = Math.Abs(bycycleStationNum[j] - dstNum);

                if (minDistanceStart > distanceStart)
                {
                    minDistanceStart = distanceStart;
                    minStartIdx = j;
                }
                
                if(minDistanceDst > distanceDst)
                {
                    minDistanceDst = distanceDst;
                    minDstIdx = j;
                }

            }

            int bycycleTime = Math.Abs(bycycleStationNum[minStartIdx] - bycycleStationNum[minDstIdx]) * 70;
            int walkTimeWithinBycycle = (minDistanceStart + minDistanceDst) * 240;

            bycycleTravelTime = bycycleTime + walkTimeWithinBycycle;


            //2.걸어갈 경우
            int walkingTime = 0;

            walkingTime = Math.Abs(dstNum - startNum) * 240;


            int travelTime = 0;

            if(walkingTime> bycycleTravelTime)
            {
                travelTime = bycycleTravelTime;
            }
            else
            {
                travelTime = walkingTime;
            }

            return travelTime;
        }

        static int getTravelTime(string[] bycycleStation, string start, string destination,string depature)
        {

            int[] busStation = {1,12}; 
            int busTravelTime = 11 * 20;

            start = start.Remove(0, 1);
            destination = destination.Remove(0, 1);
            //1. Bus를 탈때
            int e1 = Math.Abs(int.Parse(start) - 1);
            int e12 = Math.Abs(int.Parse(start) - 12);
            int walkingTimeToBusStation = 0;
            int walkingTimeToDestinationFromBusStaion = 0;

            if (e1 > e12)
            {
                walkingTimeToBusStation = getTravelTimeWitoutBus(bycycleStation,start,"E12");
                walkingTimeToDestinationFromBusStaion = getTravelTimeWitoutBus(bycycleStation, destination, "E1");
            }
            else
            {
                walkingTimeToBusStation = getTravelTimeWitoutBus(bycycleStation, start, "E1");
                walkingTimeToDestinationFromBusStaion = getTravelTimeWitoutBus(bycycleStation, destination, "E12");
            }

            int arriveTime = 0;
            int watingTime = 0;

            string[] depatureArray = depature.Split(':');
            int depatureMinute = int.Parse(depatureArray[1]);

            if (depatureMinute * 60 + walkingTimeToBusStation > 3600)
            {
                arriveTime = depatureMinute * 60 + walkingTimeToBusStation - 3600;
            }
            else
            {
                arriveTime = depatureMinute * 60 + walkingTimeToBusStation;
            }

            watingTime = (600 - arriveTime % 600);

            int travelTimeByBus = walkingTimeToBusStation + walkingTimeToDestinationFromBusStaion + watingTime + busTravelTime;

            int travelTimeWithoutBus = getTravelTimeWitoutBus(bycycleStation, start, destination);

            if(travelTimeByBus> travelTimeWithoutBus)
            {
                return travelTimeWithoutBus;
            }
            else
            {
                return travelTimeByBus;
            }


        }
    }
}

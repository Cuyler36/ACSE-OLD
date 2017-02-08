using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSE
{
    public static class DateUtil
    {
        //Might use for something
    }

    public class ACDate
    {
        public uint Second = 0;
        public uint Minute = 0;
        public uint Hour = 0;
        public uint Day = 0;
        public uint Day_of_Week = 0;
        public uint Month = 0;
        public uint Year = 0;
        public string Date_Time_String = "";
        public bool Is_PM = false;

        public ACDate(byte[] dateData)
        {
            Second = dateData[0];
            Minute = dateData[1];
            Hour = dateData[2];
            Day = dateData[3];
            Day_of_Week = dateData[4];
            Month = dateData[5];
            Year = BitConverter.ToUInt16(new byte[] { dateData[7], dateData[6] }, 0);

            Is_PM = Hour >= 12;
            Date_Time_String = string.Format("{0}:{1}:{2} {3}, {4}/{5}/{6}", (Hour % 12) == 0 ? 12 : Hour % 12,
                Minute.ToString("D2"), Second.ToString("D2"), Is_PM ? "PM" : "AM", Month, Day, Year);
        }
    }

}

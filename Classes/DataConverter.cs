using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACSE
{
    class DataConverter
    {
        public static void WriteData(int offset, byte[] data)
        {
            Array.Reverse(data);
            data.CopyTo(MainForm.SaveBuffer, offset);
        }

        public static byte[] ReadData(int offset, int size)
        {
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
                data[i] = MainForm.SaveBuffer[offset + i];
            Array.Reverse(data);
            return data;
        }

        public static ushort[] ReadUShort(int offset, int size)
        {
            ushort[] data = new ushort[size];
            byte[] byteData = ReadData(offset, size);
            for (int i = 0; i < byteData.Length; i += 2)
            {
                ushort item = BitConverter.ToUInt16(byteData, i);
                data[i / 2] = item;
            }
            return data;
        }

        public static ushort[] ReadRawUShort(int offset, int size)
        {
            ushort[] data = new ushort[size / 2];
            byte[] rawData = ReadDataRaw(offset, size);
            for (int i = 0; i < rawData.Length; i += 2)
            {
                byte[] udata = new byte[2] { rawData[i], rawData[i + 1] };
                Array.Reverse(udata);
                data[i / 2] = BitConverter.ToUInt16(udata, 0);
            }
            return data;
        }

        public static void WriteUShort(ushort[] buffer, int offset)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                byte[] ushortBytes = BitConverter.GetBytes(buffer[i]);
                Array.Reverse(ushortBytes);
                ushortBytes.CopyTo(MainForm.SaveBuffer, offset + i * 2);
            }
        }

        public static byte[] ReadDataRaw(int offset, int size)
        {
            byte[] data = new byte[size];
            Buffer.BlockCopy(MainForm.SaveBuffer, offset, data, 0, size);
            return data;
        }

        public static void WriteDataRaw(int offset, byte[] buffer)
        {
            buffer.CopyTo(MainForm.SaveBuffer, offset);
        }

        public static uint ReadUInt(int offset)
        {
            return BitConverter.ToUInt32(ReadData(offset, 4), 0);
        }

        public static uint[] ReadUIntArray(int offset, int numInts)
        {
            uint[] uintArray = new uint[numInts];
            for (int i = 0; i < numInts; i++)
                uintArray[i] = ReadUInt(offset + i * 4);
            return uintArray;
        }

        public static ACString ReadString(int offset, int maxSize)
        {
            byte[] data = new byte[maxSize];
            Buffer.BlockCopy(MainForm.SaveBuffer, offset, data, 0, maxSize);
            return new ACString(data);
        }

        public static void WriteString(int offset, string str, int maxSize)
        {
            byte[] strBytes = new byte[maxSize];
            byte[] ACStringBytes = ACString.GetBytes(str, maxSize);
            if (ACStringBytes.Length <= maxSize)
            {
                ACStringBytes.CopyTo(strBytes, 0);
                if (str.Length < maxSize)
                    for (int i = (str.Length); i <= maxSize - 1; i++)
                        strBytes[i] = 0x20;
                strBytes.CopyTo(MainForm.SaveBuffer, offset);
            }
        }
    }
}

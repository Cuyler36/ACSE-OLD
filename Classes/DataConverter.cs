using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

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
            Buffer.BlockCopy(MainForm.SaveBuffer, offset, data, 0, size);
            Array.Reverse(data);
            return data;
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

        public static ushort ReadUShort(int offset)
        {
            byte[] ushortData = ReadData(offset, 2);
            return BitConverter.ToUInt16(ushortData, 0);
        }

        public static ushort[] ReadUShortArray(int offset, int numUshorts)
        {
            ushort[] ushortArray = new ushort[numUshorts];
            for (int i = 0; i < numUshorts; i++)
                ushortArray[i] = ReadUShort(offset + i * 2);
            return ushortArray;
        }

        public static void WriteUShort(ushort value, int offset)
        {
            byte[] ushortBytes = BitConverter.GetBytes(value);
            Array.Reverse(ushortBytes);
            ushortBytes.CopyTo(MainForm.SaveBuffer, offset);
        }

        public static void WriteUShortArray(ushort[] buffer, int offset)
        {
            for (int i = 0; i < buffer.Length; i++)
                WriteUShort(buffer[i], offset + i * 2);
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

        public static void WriteUInt(int offset, uint data)
        {
            byte[] UInt_Bytes = BitConverter.GetBytes(data);
            Array.Reverse(UInt_Bytes);
            WriteDataRaw(offset, UInt_Bytes);
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

        public static byte[] ToBits(byte[] Byte_Buffer, bool Reverse = false)
        {
            byte[] Bits = new byte[8 * Byte_Buffer.Length];
            for (int i = 0; i < Byte_Buffer.Length; i++)
            {
                BitArray Bit_Array = new BitArray(new byte[] { Byte_Buffer[i] });
                for (int x = 0; x < Bit_Array.Length; x++)
                    Bits[i * 8 + (Reverse ? 7 - x : x)] = Convert.ToByte(Bit_Array[x]);
            }
            return Bits;
        }

        public static byte ToBit(byte Bit_Byte, int Bit_Index, bool Reverse = false)
        {
            return (byte)((Reverse ? Bit_Byte >> (7 - Bit_Index) : Bit_Byte >> Bit_Index) & 1);
        }

        public static void SetBit(ref byte Bit_Byte, int Bit_Index, bool Set, bool Reverse = false)
        {
            int Mask = 1 << (Reverse ? 7 - Bit_Index : Bit_Index);
            if (Set)
                Bit_Byte = Bit_Byte |= (byte)Mask;
            else
                Bit_Byte = Bit_Byte &= (byte)~Mask;
        }
    }
}

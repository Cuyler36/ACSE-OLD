using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Animal_Crossing_GCN_Save_Editor
{
    class DataConverter
    {
        static BinaryReader Reader = null;
        static BinaryWriter Writer = null;
        static FileStream FileStream = null;
        
        public DataConverter(FileStream fileStream)
        {
            if (FileStream != null)
            {
                Reader.Close();
                Writer.Close();
                FileStream.Close();
            }
            FileStream = fileStream;
            Reader = new BinaryReader(FileStream);
            Writer = new BinaryWriter(FileStream);
        }

        public void Close()
        {
            if (FileStream != null)
            {
                Reader.Close();
                Writer.Close();
                FileStream.Close();
            }
        }

        /*private static void ConvertArray(ref dynamic array)
        {
            if (array.GetType().IsArray)
            {
                Array.Reverse(array);
            }
        }*/

        private static byte[] ConvertUShortArray(ushort[] array)
        {
            byte[] newArray = new byte[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                byte[] currentShort = BitConverter.GetBytes(array[i]);
                newArray[i * 2] = currentShort[1];
                newArray[(i * 2) + 1] = currentShort[0];
            }
            return newArray;
        }

        private static ushort[] ConvertByteArray(byte[] array)
        {
            Array.Reverse(array);
            ushort[] newArray = new ushort[array.Length / 2];
            for (int i = 0; i < array.Length; i += 2)
            {
                newArray[i / 2] = BitConverter.ToUInt16(array, i);
            }
            return newArray;
        }

        public byte[] ReadBytes(int offset, int size)
        {
            FileStream.Seek(offset, SeekOrigin.Begin);
            byte[] data = Reader.ReadBytes(size);
            Array.Reverse(data);
            return data;
        }

       /* public void WriteData(dynamic data, int offset)
        {
            Writer.Seek(offset, SeekOrigin.Begin);
            Type type = data.GetType();
            if (type == typeof(int) || type == typeof(ushort))
            {
                byte[] dataBytes = BitConverter.GetBytes(data);
                Array.Reverse(dataBytes);
                Writer.Write(dataBytes);
            }
            else if (type == typeof(byte[]))
            {
                Array.Reverse(data);
                Writer.Write(data);
            }
            else if (type == typeof(ushort[]))
            {
                Writer.Write(ConvertUShortArray(data));
            }
            else if (type == typeof(string))
            {
                byte[] strBytes = new byte[8];
                Encoding.ASCII.GetBytes(data).CopyTo(strBytes, 0);
                for (int i = (data.Length); i <= 8 - 1; i++)
                {
                    strBytes[i] = 0x20;
                }
                Writer.Write(strBytes);
            }
        }*/

    }
}

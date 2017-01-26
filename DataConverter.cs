using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACSE
{
    //TODO: Either use this class or delete it
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
    }
}

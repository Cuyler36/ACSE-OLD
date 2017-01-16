using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSE
{
    public class Checksum
    {

        //Checksum Calculation Method:
        //The checksum is located at 0x26052, and is an Unsigned Short
        //The checksum is an offset value, so that the final additive of the save + the checksum is equal to 0
        //The checksum offset is calculated by iterating through 0x26040 - 0x26051 and 0x26053 - 0x4C03F inclusively and adding every two bytes as a 16 bit ushort
        //The checksum offset can then be verfied by adding 0x26040 - 0x4C03F in two byte intervals. If your sum is 0, then the checksum offset value is correct!
        //Important to note that the gamecube used Big Endian notation, so you will likely have to convert between Little & Big Endian values to get a correct chksum

        public static ushort Calculate(byte[] buffer)
        {
            uint checksum = 0;
            for (int i = 0; i < 0x12; i += 2)
                checksum += (uint)(buffer[i] << 8) + buffer[i + 1];
            for (int i = 0x14; i < buffer.Length - 1; i += 2)
                checksum += (uint)(buffer[i] << 8) + buffer[i + 1];
            return (ushort)-checksum;
        }

        public static bool Verify(byte[] buffer)
        {
            return BitConverter.ToUInt16(new byte[2] { buffer[0x13], buffer[0x12] }, 0) == Calculate(buffer);
        }

        public static void Update(byte[] buffer)
        {
            byte[] chksumBytes = BitConverter.GetBytes(Calculate(buffer));
            Array.Reverse(chksumBytes);
            chksumBytes.CopyTo(buffer, 0x12);
        }
    }
}

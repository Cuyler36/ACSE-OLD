using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACSE
{
    /// <summary>
    /// Pattern Data Write-up
    /// Patterns consist of a 15-color palette.
    /// There are 16 palettes to select from, but you can only use one palette at a time.
    /// To save space, the AC Devs used each nibble for a pixel on the pattern. FF = White, White | 1F = Red, White
    /// Each Pattern is 32x32 pixels. So the total space in memory was halved by doing this (only 0x220 bytes)
    /// Data is stored like this:
    ///     Name: 0x10 bytes
    ///     Palette: 0x1 bytes
    ///     Alignment Bytes?: 0xF bytes
    ///     Pattern Data: 0x200 bytes
    /// </summary>
    class PatternData
    {
        public static List<uint[]> Palette_Data = new List<uint[]>()
        {
            new uint[15]
            {
                0xFFCD4A4A, 0xFFDE8341, 0xFFE6AC18, 0xFFE6C520, 0xFFD5DE18, 0xFFB4E618, 0xFF83D552, 0xFF39C56A, 0xFF29ACC5, 0xFF417BEE, 0xFF6A4AE6, 0xFF945ACD, 0xFFBD41B4, 0xFF000000, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFFFF8B8B, 0xFFFFCD83, 0xFFFFE65A, 0xFFFFF662, 0xFFFFFF83, 0xFFDEFF52, 0xFFB4FF83, 0xFF7BF6AC, 0xFF62E6F6, 0xFF83C5FF, 0xFFA49CFF, 0xFFD59CFF, 0xFFFF9CF6, 0xFF8B8B8B, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFF9C1818, 0xFFAC5208, 0xFFB47B00, 0xFFB49400, 0xFFA4AC00, 0xFF83B400, 0xFF52A431, 0xFF089439, 0xFF007B94, 0xFF104ABD, 0xFF3918AC, 0xFF5A2994, 0xFF8B087B, 0xFF080808, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFF41945A, 0xFF73C58B, 0xFF94E6AC, 0xFF008B7B, 0xFF5AB4AC, 0xFF83C5C5, 0xFF2073A4, 0xFF4A9CCD, 0xFF6AACDE, 0xFF7383BD, 0xFF6A73AC, 0xFF525294, 0xFF39397B, 0xFF181862, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFF9C8352, 0xFFBD945A, 0xFFD5BD83, 0xFF9C5252, 0xFFCD7362, 0xFFEE9C8B, 0xFF8B6283, 0xFFA483B4, 0xFFDEB4DE, 0xFFBD8383, 0xFFAC736A, 0xFF945252, 0xFF7B3939, 0xFF621810, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFFEE5A00, 0xFFFF9C41, 0xFFFFCD83, 0xFFFFEEA4, 0xFF8B4A29, 0xFFB47B5A, 0xFFE6AC8B, 0xFFFFDEBD, 0xFF318BFF, 0xFF62B4FF, 0xFF9CDEFF, 0xFFC5E6FF, 0xFF6A6A6A, 0xFF000000, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFF39B441, 0xFF62DE5A, 0xFF8BEE83, 0xFFB4FFAC, 0xFF2020C5, 0xFF5252F6, 0xFF8383FF, 0xFFB4B4FF, 0xFFCD3939, 0xFFDE6A6A, 0xFFE68B9C, 0xFFEEBDBD, 0xFF6A6A6A, 0xFF000000, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFF082000, 0xFF415A39, 0xFF6A8362, 0xFF9CB494, 0xFF5A2900, 0xFF7B4A20, 0xFFA4734A, 0xFFD5A47B, 0xFF947B00, 0xFFB49439, 0xFFCDB46A, 0xFFDED59C, 0xFF6A6A6A, 0xFF000000, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFF2020FF, 0xFFFF2020, 0xFFD5D500, 0xFF6262FF, 0xFFFF6262, 0xFFD5D562, 0xFF9494FF, 0xFFFF9494, 0xFFD5D594, 0xFFACACFF, 0xFFFFACAC, 0xFFE6E6AC, 0xFF6A6A6A, 0xFF000000, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFF20A420, 0xFF39ACFF, 0xFF9C52EE, 0xFF52BD52, 0xFF5AC5FF, 0xFFB49CFF, 0xFF6AD573, 0xFF8BE6FF, 0xFFCDB4FF, 0xFF94DEAC, 0xFFBDF6FF, 0xFFD5CDFF, 0xFF6A6A6A, 0xFF000000, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFFD50000, 0xFFFFBD00, 0xFFEEF631, 0xFF4ACD41, 0xFF299C29, 0xFF528BBD, 0xFF414AAC, 0xFF9452D5, 0xFFF67BDE, 0xFFA49439, 0xFF9C4141, 0xFF5A3139, 0xFF6A6A6A, 0xFF000000, 0xFFFFFFFF
            },
            new uint[15]
            {
                0xFFE6CD18, 0xFF20C518, 0xFFFF6A00, 0xFF0000FF, 0xFF9400BD, 0xFFE6CD18, 0xFF00A400, 0xFFCD4100, 0xFF0000D5, 0xFF5A008B, 0xFF9C8B18, 0xFF008300, 0xFFA42000, 0xFF0000A4, 0xFF4A005A
            },
            new uint[15]
            {
                0xFFFF2020, 0xFFE6D500, 0xFFF639BD, 0xFF00D59C, 0xFF107310, 0xFFC52020, 0xFFBDA400, 0xFFCD3994, 0xFF009C6A, 0xFF204A20, 0xFF8B2020, 0xFF836A00, 0xFF941862, 0xFF00734A, 0xFF183918
            },
            new uint[15]
            {
                0xFFEED5D5, 0xFFDEC5C5, 0xFFCDB4B4, 0xFFBDA4A4, 0xFFAC9494, 0xFF9C8383, 0xFF8B7373, 0xFF7B6262, 0xFF6A5252, 0xFF5A4141, 0xFF4A3131, 0xFF392020, 0xFF291010, 0xFF180000, 0xFF100000
            },
            new uint[15]
            {
                0xFFEEEEEE, 0xFFDEDEDE, 0xFFCDCDCD, 0xFFBDBDBD, 0xFFACACAC, 0xFF9C9C9C, 0xFF8B8B8B, 0xFF7B7B7B, 0xFF6A6A6A, 0xFF5A5A5A, 0xFF4A4A4A, 0xFF393939, 0xFF292929, 0xFF181818, 0xFF101010
            },
            new uint[15]
            {
                0xFFEE7B7B, 0xFFD51818, 0xFFF69418, 0xFFE6E652, 0xFF006A00, 0xFF39B439, 0xFF0039B4, 0xFF399CFF, 0xFF940094, 0xFFFF6AFF, 0xFF944108, 0xFFEE9C5A, 0xFFFFC594, 0xFF000000, 0xFFFFFFFF
            },
        };

        private Color ColorFromUInt32(uint color)
        {
            return Color.FromArgb(0xFF, (byte)(color >> 16), (byte)(color >> 8), (byte)(color));
        }

        public uint ClosestColor(uint color, int palette)
        {
            uint closestColor = Palette_Data[palette][0];
            double diff = double.MaxValue;
            Color c = ColorFromUInt32(color);
            float targetHue = c.GetHue();
            float targetSat = c.GetSaturation();
            float targetBri = c.GetBrightness();

            foreach (uint validColor in Palette_Data[palette])
            {
                Color checkColor = ColorFromUInt32(validColor);
                float currentHue = checkColor.GetHue();
                float currentSat = checkColor.GetSaturation();
                float currentBri = checkColor.GetBrightness();
                int t = (int)color;

                double currentDiff = Math.Pow(targetHue - currentHue, 2) + Math.Pow(targetSat - currentSat, 2) + Math.Pow(targetBri - currentBri, 2);

                if (currentDiff < diff)
                {
                    diff = currentDiff;
                    closestColor = validColor;
                }
            }

            return closestColor;
        }
    }

    public class Pattern
    {
        private Form1 form;
        private int Offset = 0;
        private byte[] patternBitmapBuffer = new byte[4 * 32 * 32];
        public string Name;
        public byte Palette;
        public Bitmap Pattern_Bitmap;

        public Pattern(int patternOffset, Form1 form1)
        {
            Offset = patternOffset;
            form = form1;
            Read();
        }

        public void GeneratePatternBitmap()
        {
            byte[] patternRawData = form.ReadDataRaw(Offset + 0x20, 0x200);
            if (PatternData.Palette_Data.Count >= Palette + 1)
            {
                List<byte[]> Block_Data = new List<byte[]>();
                for (int block = 0; block < 32; block++)
                {
                    byte[] Block = new byte[16];
                    Array.ConstrainedCopy(patternRawData, block * 16, Block, 0, 16);
                    Block_Data.Add(Block);
                }
                List<byte[]> Sorted_Block_Data = new List<byte[]>()
                {
                    Block_Data[0], Block_Data[2], Block_Data[4], Block_Data[6],
                    Block_Data[1], Block_Data[3], Block_Data[5], Block_Data[7],
                    Block_Data[8], Block_Data[10], Block_Data[12], Block_Data[14],
                    Block_Data[9], Block_Data[11], Block_Data[13], Block_Data[15],
                    Block_Data[16], Block_Data[18], Block_Data[20], Block_Data[22],
                    Block_Data[17], Block_Data[19], Block_Data[21], Block_Data[23],
                    Block_Data[24], Block_Data[26], Block_Data[28], Block_Data[30],
                    Block_Data[25], Block_Data[27], Block_Data[29], Block_Data[31],
                };
                int pos = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            byte RightPixel = (byte)(Sorted_Block_Data[i * 4 + x / 4][x % 4 + y * 4] & 0x0F);
                            byte LeftPixel = (byte)((Sorted_Block_Data[i * 4 + x / 4][x % 4 + y * 4] & 0xF0) >> 4);
                            Buffer.BlockCopy(BitConverter.GetBytes(PatternData.Palette_Data[Palette][LeftPixel - 1]), 0, patternBitmapBuffer, pos * 4, 4);
                            Buffer.BlockCopy(BitConverter.GetBytes(PatternData.Palette_Data[Palette][RightPixel - 1]), 0, patternBitmapBuffer, (pos + 1) * 4, 4);
                            pos += 2;
                        }
                    }
                }
                Pattern_Bitmap = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
                BitmapData bitmapData = Pattern_Bitmap.LockBits(new Rectangle(0, 0, 32, 32), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                System.Runtime.InteropServices.Marshal.Copy(patternBitmapBuffer, 0, bitmapData.Scan0, patternBitmapBuffer.Length);
                Pattern_Bitmap.UnlockBits(bitmapData);
            }
        }

        public void Read()
        {
            Name = form.ReadString(Offset, 0x10).Trim();
            Palette = form.ReadData(Offset + 0x10, 0x1)[0];
            GeneratePatternBitmap();
        }

        public void Write()
        {
            form.WriteString(Offset, Name, 0x10);
            form.WriteData(Offset + 0x10, new byte[] { Palette });
            /*if (Pattern_Bitmap != null && PatternData.Palette_Data.Count >= Palette + 1)
            {
                List<byte[]> Block_Data = new List<byte[]>();
                List<byte[]> Corrected_Data = new List<byte[]>();
                for (int i = 0; i < 32; i++)
                    Corrected_Data.Add(new byte[16]);
                byte[] Bitmap_Data = (byte[])(new ImageConverter().ConvertTo(Pattern_Bitmap, typeof(byte[])));
                for (int block = 0; block < 32; block++)
                {
                    byte[] Block = new byte[16];
                    Array.ConstrainedCopy(Bitmap_Data, block * 16, Block, 0, 16);
                    Block_Data.Add(Block);
                }
                int pos = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            byte data = Block_Data[i * 4 + x / 4][x % 4 + y * 4];
                            Buffer.BlockCopy(BitConverter.GetBytes(PatternData.Palette_Data[Palette][data - 1]), 0, patternBitmapBuffer, pos * 4, 4);
                            pos += 2;
                        }
                    }
                }
                List<byte[]> Sorted_Block_Data = new List<byte[]>()
                {
                    Block_Data[0], Block_Data[4], Block_Data[1], Block_Data[5], //0, 1, 2, 3|
                    Block_Data[2], Block_Data[6], Block_Data[3], Block_Data[7], //4, 5, 6, 7|
                    Block_Data[8], Block_Data[12], Block_Data[9], Block_Data[13], //8, 9, 10, 11|
                    Block_Data[10], Block_Data[14], Block_Data[11], Block_Data[15], //12, 13, 14, 15|
                    Block_Data[16], Block_Data[20], Block_Data[17], Block_Data[21], //16, 17, 18, 19
                    Block_Data[18], Block_Data[22], Block_Data[19], Block_Data[23], //20, 21, 22, 23
                    Block_Data[24], Block_Data[28], Block_Data[25], Block_Data[29], //24, 25, 26, 27
                    Block_Data[26], Block_Data[30], Block_Data[27], Block_Data[31], //28, 29, 30, 31
                };
                //Finish this
                //Do not forget, Blocks may be ordered, but still have to reverse the order corrections made in the for loop
            }*/
        }
    }
}

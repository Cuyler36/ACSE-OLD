using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ACSE
{
    public enum SaveType
    {
        Unknown,
        Doubutsu_no_Mori,
        Doubutsu_no_Mori_Plus,  //Possible to remove this? I can't even find a valid file on the internet... Perhaps I'll just buy a copy
        Animal_Crossing,
        Doubutsu_no_Mori_e_Plus,
        Wild_World,
        City_Folk,
        New_Leaf,
        Welcome_Amiibo
    }

    public struct Offsets
    {
        public int Town_Name;
        public int Player_Start;
        public int Player_Size;
        public int Town_Data;
        public int Town_Data_Size;
        public int Acre_Data;
        public int Acre_Data_Size;
        public int Buried_Data;
        public int Buried_Data_Size;
        public int Island_World_Data;
        public int Island_World_Size;
        public int Island_Buried_Data;
        public int Island_Buried_Size;
        public int House_Data;
        public int House_Data_Size;
        public int Villager_Data;
        public int Villager_Size;
        
    }

    public enum SaveFileDataOffset
    {
        nafjfla = 0,
        gafegci = 0x26040,
        gafegcs = 0x26150,
    }

    public static class SaveDataManager
    {
        /// <summary>
        /// Used for Doubutsu no Mori. Every four bytes is reversed in the save, so we change it back.
        /// </summary>
        /// <param name="saveBuff"></param>
        /// <returns></returns>
        public static byte[] ByteSwap(byte[] saveBuff)
        {
            byte[] Corrected_Save = new byte[saveBuff.Length];
            for (int i = 0; i < saveBuff.Length; i += 4)
            {
                byte[] Temp = new byte[4];
                Buffer.BlockCopy(saveBuff, i, Temp, 0, 4);
                Array.Reverse(Temp);
                Temp.CopyTo(Corrected_Save, i);
            }
            return Corrected_Save;
        }

        public static SaveType GetSaveType(byte[] Save_Data)
        {
            if (Save_Data.Length == 0x20000)
                return SaveType.Doubutsu_no_Mori;
            else if (Save_Data.Length == 0x72040 || Save_Data.Length == 0x72150)
            {
                string Game_ID = Encoding.ASCII.GetString(Save_Data, Save_Data.Length == 0x72150 ? 0x110 : 0, 4);
                if (Game_ID == "GAFE")
                    return SaveType.Animal_Crossing;
                else if (Game_ID == "GAFJ")
                    return SaveType.Doubutsu_no_Mori_Plus;
                else if (Game_ID == "GAEJ")
                    return SaveType.Doubutsu_no_Mori_e_Plus;
            }
            else if (Save_Data.Length == 0x401F4)
                return SaveType.Wild_World;
            else if (Save_Data.Length == 0x40F340 || Save_Data.Length == 0x47A0DA)
                return SaveType.City_Folk;
            else if (Save_Data.Length == 0x7FA00) //Don't forget RAM dumps
                return SaveType.New_Leaf;
            else if (Save_Data.Length == 0x89B00)
                return SaveType.Welcome_Amiibo;
            return SaveType.Unknown;
        }

        public static int GetSaveDataOffset(string Game_ID, string Extension)
        {
            SaveFileDataOffset Extension_Enum;
            if (Enum.TryParse(Game_ID + Extension, out Extension_Enum))
                return (int)Extension_Enum;
            return 0;
        }

        public static string GetGameID(SaveType Save_Type)
        {
            switch (Save_Type)
            {
                case SaveType.Doubutsu_no_Mori:
                    return "NAFJ";
                case SaveType.Animal_Crossing:
                    return "GAFE";
                case SaveType.Doubutsu_no_Mori_Plus:
                    return "GAFJ";
                case SaveType.Doubutsu_no_Mori_e_Plus:
                    return "GAEJ";
                case SaveType.Wild_World: //Currently only supporting the English versions of WW+
                    return "ADME";
                case SaveType.City_Folk:
                    return "RUUE";
                case SaveType.New_Leaf:
                    return "EDGE";
                case SaveType.Welcome_Amiibo:
                    return "EAAE";
                default:
                    return "Unknown";
            }
        }
    }

    //This class will be used when support for other games in the series is added.
    //Many classes will have to be rewritten, and I'll probably have to redo all the forms...

    public class Save
    {
        public SaveType Save_Type;
        public byte[] Original_Save_Data;
        public byte[] Working_Save_Data;
        public int[] Save_Offsets;
        public int Save_Data_Start_Offset;
        public string Save_Path;
        public string Save_Name;
        public string Save_Extension;
        public string Save_ID;
        public bool Is_Big_Endian = true;
        private FileStream Save_File;
        private BinaryReader Save_Reader;
        private BinaryWriter Save_Writer;

        public Save(string File_Path)
        {
            if (File.Exists(File_Path))
            {
                if (Save_File != null)
                {
                    Save_Reader.Close();
                    Save_File.Close();
                }
                Save_File = new FileStream(File_Path, FileMode.Open);
                if (!Save_File.CanWrite)
                {
                    MessageBox.Show(string.Format("Error: File {0} is being used by another process. Please close any process using it before editing!",
                        Path.GetFileName(File_Path)), "File Opening Error");
                    try { Save_File.Close(); } catch { };
                    return;
                }

                Save_Reader = new BinaryReader(Save_File);

                Original_Save_Data = Save_Type == SaveType.Doubutsu_no_Mori ? SaveDataManager.ByteSwap(Save_Reader.ReadBytes((int)Save_File.Length)) : SaveDataManager.ByteSwap(Save_Reader.ReadBytes((int)Save_File.Length));
                Working_Save_Data = new byte[Original_Save_Data.Length];
                Buffer.BlockCopy(Original_Save_Data, 0, Working_Save_Data, 0, Original_Save_Data.Length);

                Save_Type = SaveDataManager.GetSaveType(Original_Save_Data);
                Save_Name = Path.GetFileNameWithoutExtension(File_Path);
                Save_Path = Path.GetDirectoryName(File_Path) + Path.DirectorySeparatorChar;
                Save_Extension = Path.GetExtension(File_Path);
                Save_ID = SaveDataManager.GetGameID(Save_Type);
                Save_Data_Start_Offset = SaveDataManager.GetSaveDataOffset(Save_ID.ToLower(), Save_Extension.Replace(".", "").ToLower());

                if (Save_Type == SaveType.Wild_World || Save_Type == SaveType.New_Leaf || Save_Type == SaveType.Welcome_Amiibo)
                    Is_Big_Endian = false;

                Save_Reader.Close();
                Save_File.Close();

                //Test stuff
                /*
                MessageBox.Show("SaveType: " + Save_Type);
                MessageBox.Show(string.Format("Directory: {0}\nName: {1}\nExtension: {2}", Save_Path, Save_Name, Save_Extension));
                MessageBox.Show(Save_Data_Start_Offset.ToString("X"));
                MessageBox.Show("GameID: " + Encoding.ASCII.GetString(Working_Save_Data, 4, 4));
                MessageBox.Show(Working_Save_Data[0].ToString("X"));

                Save_Name = "AC_TEST";
                Flush();*/
            }
        }

        public void Flush()
        {
            string Full_Save_Name = Save_Path + Path.DirectorySeparatorChar + Save_Name + Save_Extension;
            Save_File = new FileStream(Full_Save_Name, FileMode.OpenOrCreate);
            Save_Writer = new BinaryWriter(Save_File);

            Save_Writer.Write(Working_Save_Data);
            Save_Writer.Flush();
            Save_File.Flush();

            Save_Writer.Close();
            Save_File.Close();
        }

        public void Write(int offset, object data, bool reversed = false)
        {
            Type Data_Type = data.GetType();
            if (!Data_Type.IsArray)
            {
                if (Data_Type == typeof(byte))
                    Working_Save_Data[offset] = (byte)data;
                else
                {
                    byte[] Byte_Array = BitConverter.GetBytes((dynamic)data);
                    if (reversed)
                        Array.Reverse(Byte_Array);
                    Buffer.BlockCopy(Byte_Array, 0, Working_Save_Data, offset, Byte_Array.Length);
                }
            }
            else
            {
                dynamic Data_Array = (dynamic)data;
                if (Data_Type == typeof(byte[]))
                    for (int i = 0; i < Data_Array.Length; i++)
                        Working_Save_Data[offset + i] = Data_Array[i];
                else
                {
                    int Data_Size = Marshal.SizeOf(Data_Array[0]);
                    for (int i = 0; i < Data_Array.Length; i++)
                    {
                        byte[] Byte_Array = BitConverter.GetBytes(Data_Array[i]);
                        if (reversed)
                            Array.Reverse(Byte_Array);
                        Byte_Array.CopyTo(Working_Save_Data, offset + i * Data_Size);
                    }
                }
            }
        }

        public byte ReadByte(int offset)
        {
            return Working_Save_Data[offset];
        }

        public byte[] ReadByteArray(int offset, int count, bool reversed = false)
        {
            byte[] Data = new byte[count];
            Buffer.BlockCopy(Working_Save_Data, offset, Data, 0, count);
            if (reversed)
                Array.Reverse(Data);
            return Data;
        }

        public ushort ReadUInt16(int offset, bool reversed = false)
        {
            return BitConverter.ToUInt16(ReadByteArray(offset, 2, reversed), 0);
        }

        public ushort[] ReadUInt16Array(int offset, int count, bool reversed = false)
        {
            ushort[] Returned_Values = new ushort[count];
            for (int i = 0; i < count; i++)
                Returned_Values[i] = ReadUInt16(offset + i * 2, reversed);
            return Returned_Values;
        }

        public uint ReadUInt32(int offset, bool reversed = false)
        {
            return BitConverter.ToUInt32(ReadByteArray(offset, 4, reversed), 0);
        }

        public uint[] ReadUInt32Array(int offset, int count, bool reversed = false)
        {
            uint[] Returned_Values = new uint[count];
            for (int i = 0; i < count; i++)
                Returned_Values[i] = ReadUInt32(offset + i * 4, reversed);
            return Returned_Values;
        }
    }
}

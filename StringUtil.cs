using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animal_Crossing_GCN_Save_Editor
{
    class StringUtil
    {
        public static Dictionary<byte, string> CharacterDictionary = new Dictionary<byte, string>()
            {
                {0x90, "–" },
                {0xCD, "\n" },
                {0x2A, "~" },
                {0xD0, ";" },
                {0xD4, "⚷" },
                {0xD1, "#" },
                {0x85, "•" },
                {0xA2, "Æ" },
                {0xA0, "¯" },
                {0xAE, "/" },
                {0x97, "¦" },
                {0xC0, "×" },
                {0xC1, "÷" },
                {0xA5, "»" },
                {0xA6, "«" },
                {0xAC, "∈" },
                {0xAD, "∋" },
                {0xB4, "+" },
                {0x1D, "ß" },
                {0x1E, "Þ" },
                {0x86, "ð" },
                {0x98, "§" },
                {0x9B, "‖" },
                {0x9C, "µ" },
                {0xA1, "¬" },
                {0x2B, "♥" },
                {0xB9, "★" },
                {0x2F, "♪" },
                {0x3B, "🌢" },
                {0x5C, "💢" },
                {0xB8, "🍀"},
                {0xC6, "🐾" },
                {0xB6, "♂" },
                {0xB7, "♀" },
                {0xAF, "∞" },
                {0xB0, "○" },
                {0xB1, "🗙" },
                {0xB2, "□" },
                {0xB3, "△" },
                {0xBA, "💀" },
                {0xBB, "😮" },
                {0xBC, "😄" },
                {0xBD, "😣" },
                {0xBE, "😠" },
                {0xBF, "😃" },
                {0xA7, "☀" },
                {0xA8, "☁" },
                {0xA9, "☂" },
                {0xAB, "☃" },
                {0xAA, "🌬" }, //Wind...
                {0xB5, "⚡" },
                {0xC2, "🔨" }, //Hammer??
                {0xC3, "🎀" }, //Not sure wtf this is (put it as ribbon)
                {0xC4, "✉" },
                {0xC7, "🐶" },
                {0xC8, "🐱" },
                {0xC9, "🐰" },
                {0xCA, "🐦" },
                {0xCB, "🐮" },
                {0xCC, "🐷" },
                {0xC5, "💰" },
                {0xCE, "🐟" },
                {0xCF, "🐞" },
                {0x7B, "è" },
                {0x7C, "é" },
                {0x7D, "ê" },
                {0x7E, "ë" },
                {0x93, "ý" },
                {0x94, "ÿ" },
                {0x8E, "ù" },
                {0x8F, "ú" },
                {0x91, "û" },
                {0x92, "ü" },
                {0x81, "ì" },
                {0x82, "í" },
                {0x83, "î" },
                {0x84, "ï" },
                {0x88, "ò" },
                {0x89, "ó" },
                {0x8A, "ô" },
                {0x8B, "õ" },
                {0x8C, "ö" },
                {0x1F, "à" },
                {0x23, "á" },
                {0x24, "â" },
                {0x5B, "ã" },
                {0x5D, "ä" },
                {0x5E, "å" },
                {0x09, "È" },
                {0x0A, "É" },
                {0x0B, "Ê" },
                {0x0C, "Ë" },
                {0x96, "Ý" },
                {0x19, "Ù" },
                {0x1A, "Ú" },
                {0x1B, "Û" },
                {0x1C, "Ü" },
                {0x0D, "Ì" },
                {0x0E, "Í" },
                {0x0F, "Î" },
                {0x10, "Ï" },
                {0x13, "Ò" },
                {0x14, "Ó" },
                {0x15, "Ô" },
                {0x16, "Õ" },
                {0x17, "Ö" },
                {0x02, "Ä" },
                {0x03, "À" },
                {0x04, "Á" },
                {0x05, "Â" },
                {0x06, "Ã" },
                {0x07, "Å" },
                {0x11, "Ð" },
                {0x08, "Ç" },
                {0x12, "Ñ" },
                {0x87, "ñ" },
                {0x60, "ç" },
                {0x95, "þ" },
                {0x01, "¿" },
                {0xA4, "„" },
                {0xA3, "æ" }
            };
    }

    public class ACString
    {
        byte[] String_Bytes;
        public string String = "";

        public ACString(byte[] stringBuffer)
        {
            String_Bytes = stringBuffer;
            foreach (byte b in stringBuffer)
            {
                if (StringUtil.CharacterDictionary.ContainsKey(b))
                    String += StringUtil.CharacterDictionary.FirstOrDefault(o => o.Key == b).Value;
                else
                    String += Encoding.UTF8.GetString(new byte[1] { b });
            }
        }

        public static byte[] GetBytes(string String)
        {
            byte[] stringBytes = Encoding.Unicode.GetBytes(String);
            for (int i = 0; i < String.Length; i++)
                if (StringUtil.CharacterDictionary.ContainsValue(String[i].ToString()))
                    stringBytes[i] = StringUtil.CharacterDictionary.FirstOrDefault(o => o.Value == String[i].ToString()).Key;
            return stringBytes;
        }

        public string Trim()
        {
            return String.Trim(' ');
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSE
{
    class VillagerData
    {
        public static Dictionary<ushort, string> Villagers = new Dictionary<ushort, string>()
        {
            {0x0000, "No Villager" },
            {0xE000, "Bob" },
            {0xE001, "Olivia" },
            {0xE002, "Mitzi" },
            {0xE003, "Kiki" },
            {0xE004, "Tangy" },
            {0xE005, "Kabuki" },
            {0xE006, "Tabby" },
            {0xE007, "Monique" },
            {0xE008, "Stinky" },
            {0xE009, "Purrl" },
            {0xE00A, "Kitty" },
            {0xE00B, "Tom" },
            {0xE00C, "Rosie" },
            {0xE00D, "Nosegay" },
            {0xE00E, "Zoe" },
            {0xE00F, "Pango" },
            {0xE010, "Cyrano" },
            {0xE011, "Snooty" },
            {0xE012, "Teddy" },
            {0xE013, "Chow" },
            {0xE014, "Dozer" },
            {0xE015, "Nate" },
            {0xE016, "Groucho" },
            {0xE017, "Tutu" },
            {0xE018, "Ursala" },
            {0xE019, "Grizzly" },
            {0xE01A, "Pinky" },
            {0xE01B, "Jay" },
            {0xE01C, "Twiggy" },
            {0xE01D, "Anchovy" },
            {0xE01E, "Piper" },
            {0xE01F, "Admiral" },
            {0xE020, "Otis" },
            {0xE021, "Robin" },
            {0xE022, "Midge" },
            {0xE023, "Ace" },
            {0xE024, "Twirp" },
            {0xE025, "Chuck" },
            {0xE026, "Stu" },
            {0xE027, "Goose" },
            {0xE028, "Betty" },
            {0xE029, "Hector" },
            {0xE02A, "Egbert" },
            {0xE02B, "Ava" },
            {0xE02C, "Hank" },
            {0xE02D, "Leigh" },
            {0xE02E, "Rhoda" },
            {0xE02F, "Vladimir" },
            {0xE030, "Murphy" },
            {0xE031, "Cupcake" },
            {0xE032, "Kody" },
            {0xE033, "Maple" },
            {0xE034, "Pudge" },
            {0xE035, "Olive" },
            {0xE036, "Poncho" },
            {0xE037, "Bluebear" },
            {0xE038, "Patty" },
            {0xE039, "Petunia" },
            {0xE03A, "Bessie" },
            {0xE03B, "Belle" },
            {0xE03C, "Alfonso" },
            {0xE03D, "Boots" },
            {0xE03E, "Liz" },
            {0xE03F, "Biskit" },
            {0xE040, "Goldie" },
            {0xE041, "Daisy" },
            {0xE042, "Lucky" },
            {0xE043, "Portia" },
            {0xE044, "Maddie" },
            {0xE045, "Butch" },
            {0xE046, "Bill" },
            {0xE047, "Pompom" },
            {0xE048, "Joey" },
            {0xE049, "Scoot" },
            {0xE04A, "Derwin" },
            {0xE04B, "Freckles" },
            {0xE04C, "Paolo" },
            {0xE04D, "Dizzy" },
            {0xE04E, "Axel" },
            {0xE04F, "Emerald" },
            {0xE050, "Tad" },
            {0xE051, "Wart Jr." },
            {0xE052, "Cousteau" },
            {0xE053, "Puddles" },
            {0xE054, "Lily" },
            {0xE055, "Jeremiah" },
            {0xE056, "Huck" },
            {0xE057, "Camofrog" },
            {0xE058, "Ribbot" },
            {0xE059, "Prince" },
            {0xE05A, "Jambette" },
            {0xE05B, "Billy" },
            {0xE05C, "Chevere" },
            {0xE05D, "Iggy" },
            {0xE05E, "Gruff" },
            {0xE05F, "Sven" },
            {0xE060, "Velma" },
            {0xE061, "Jane" },
            {0xE062, "Cesar" },
            {0xE063, "Louie" },
            {0xE064, "Peewee" },
            {0xE065, "Rollo" },
            {0xE066, "Bubbles" },
            {0xE067, "Bertha" },
            {0xE068, "Elmer" },
            {0xE069, "Winnie" },
            {0xE06A, "Savannah" },
            {0xE06B, "Ed" },
            {0xE06C, "Cleo" },
            {0xE06D, "Peaches" },
            {0xE06E, "Buck" },
            {0xE06F, "Carrie" },
            {0xE070, "Mathilda" },
            {0xE071, "Marcy" },
            {0xE072, "Kitt" },
            {0xE073, "Valise" },
            {0xE074, "Astrid" },
            {0xE075, "Sydney" },
            {0xE076, "Gonzo" },
            {0xE077, "Ozzie" },
            {0xE078, "Yuka" },
            {0xE079, "Huggy" },
            {0xE07A, "Rex" },
            {0xE07B, "Aziz" },
            {0xE07C, "Leopold" },
            {0xE07D, "Samson" },
            {0xE07E, "Penny" },
            {0xE07F, "Dora" },
            {0xE080, "Chico" },
            {0xE081, "Candi" },
            {0xE082, "Rizzo" },
            {0xE083, "Anicotti" },
            {0xE084, "Limberg" },
            {0xE085, "Carmen" },
            {0xE086, "Octavian" },
            {0xE087, "Sandy" },
            {0xE088, "Sprocket" },
            {0xE089, "Rio" },
            {0xE08A, "Queenie" },
            {0xE08B, "Apollo" },
            {0xE08C, "Buzz" },
            {0xE08D, "Quetzel" },
            {0xE08E, "Amelia" },
            {0xE08F, "Pierce" },
            {0xE090, "Roald" },
            {0xE091, "Aurora" },
            {0xE092, "Hopper" },
            {0xE093, "Cube" },
            {0xE094, "Puck" },
            {0xE095, "Gwen" },
            {0xE096, "Friga" },
            {0xE097, "Curly" },
            {0xE098, "Truffles" },
            {0xE099, "Spork" },
            {0xE09A, "Hugh" },
            {0xE09B, "Rasher" },
            {0xE09C, "Sue E" },
            {0xE09D, "Hambo" },
            {0xE09E, "Lucy" },
            {0xE09F, "Cobb" },
            {0xE0A0, "Boris" },
            {0xE0A1, "Bunnie" },
            {0xE0A2, "Doc" },
            {0xE0A3, "Gaston" },
            {0xE0A4, "Coco" },
            {0xE0A5, "Gabi" },
            {0xE0A6, "Dotty" },
            {0xE0A7, "Genji" },
            {0xE0A8, "Snake" },
            {0xE0A9, "Claude" },
            {0xE0AA, "Tank" },
            {0xE0AB, "Spike" },
            {0xE0AC, "Tiara" },
            {0xE0AD, "Vesta" },
            {0xE0AE, "Filbert" },
            {0xE0AF, "Hazel" },
            {0xE0B0, "Peanut" },
            {0xE0B1, "Pecan" },
            {0xE0B2, "Ricky" },
            {0xE0B3, "Static" },
            {0xE0B4, "Mint" },
            {0xE0B5, "Nibbles" },
            {0xE0B6, "Tybalt" },
            {0xE0B7, "Rolf" },
            {0xE0B8, "Bangle" },
            {0xE0B9, "Lobo" },
            {0xE0BA, "Freya" },
            {0xE0BB, "Chief" },
            {0xE0BC, "Weber" },
            {0xE0BD, "Mallary" },
            {0xE0BE, "Wolfgang" },
            {0xE0BF, "Hornsby" },
            {0xE0C0, "Oxford" },
            {0xE0C1, "T-Bone" },
            {0xE0C2, "Biff" },
            {0xE0C3, "Opal" },
            {0xE0C4, "Bones" },
            {0xE0C5, "Bea" },
            {0xE0C6, "Bitty" },
            {0xE0C7, "Rocco" },
            {0xE0C8, "Lulu" },
            {0xE0C9, "Blaire" },
            {0xE0CA, "Sally" },
            {0xE0CB, "Ellie" },
            {0xE0CC, "Eloise" },
            {0xE0CD, "Alli" },
            {0xE0CE, "Pippy" },
            {0xE0CF, "Eunice" },
            {0xE0D0, "Baabara" },
            {0xE0D1, "Fang" },
            {0xE0D2, "Deena" },
            {0xE0D3, "Pate" },
            {0xE0D4, "Stella" },
            {0xE0D5, "Cashmere" },
            {0xE0D6, "Woolio" },
            {0xE0D7, "Cookie" },
            //Beginning of Islanders
            {0xE0D8, "Maelle" },
            {0xE0D9, "O'Hare" },
            {0xE0DA, "Bliss" }, //Aka Caroline
            {0xE0DB, "Drift" },
            {0xE0DC, "Bud" },
            {0xE0DD, "Boomer" },
            {0xE0DE, "Elina" },
            {0xE0DF, "Flash" },
            {0xE0E0, "Dobie" },
            {0xE0E1, "Flossie" },
            {0xE0E2, "Annalise" },
            {0xE0E3, "Plucky" },
            {0xE0E4, "Faith" },
            {0xE0E5, "Yodel" },
            {0xE0E6, "Rowan" },
            {0xE0E7, "June" },
            {0xE0E8, "Cheri" }, //Cheri is a regular villager, not an islander
            {0xE0E9, "Pigleg" },
            {0xE0EA, "Ankha" },
            //End of Islanders
            {0xE0EB, "Punchy" }, //Regular Villager
            //Beginning of Corrupt Villagers
            {0xE0EC, "White Rooster (Corrupted)" },
            {0xE0ED, "White Rooster 2 (Corrupted)" },
            {0xE0EE, "Wendel (Corrupted)" },
            {0xE0EF, "Redd (Corrupted)" },
            {0xE0F0, "Gracie (Corrupted)" },
            {0xE0F1, "Pelly (Corrupted)" },
            {0xE0F2, "Rover (Corrupted)" },
            {0xE0F3, "Rover 2 (Corrupted)" },
            {0xE0F4, "Sahara (Corrupted)" },
            {0xE0F5, "Joan (Corrupted)" },
            {0xE0F6, "Tom Nook (Blue Apron) (Corrupted)" },
            {0xE0F7, "Tom Nook (Striped Shirt) (Corrupted)" },
            {0xE0F8, "Tom Nook (White Apron) (Corrupted)" },
            {0xE0F9, "Tom Nook (Blue Suit) (Corrupted)" },
            {0xE0FA, "Katrina (Corrupted)" },
            {0xE0FB, "Copper (Corrutped)" },
            {0xE0FC, "Porter (Corrupted)" },
            {0xE0FD, "Jingle (Corrupted)" },
            {0xE0FE, "Booker (Corrputed)" },
            {0xE0FF, "Postman Pete (Corrupted)" },
            {0xE100, "Phylis" },
            {0xE101, "Redd" },
            {0xE102, "Nook (Nook's Cranny)" },
            {0xE103, "Nook (Nook 'n Go)" },
            {0xE104, "Nook (Nookway)" },
            {0xE105, "Nook (Nookingtons)" },
            {0xE106, "K.K. Slider" },
            {0xE107, "Nook (Cranny)" },
            {0xE108, "Nook (Nook 'n Go)" },
            {0xE109, "Nook (Nookway)" },
            {0xE10A, "Nook (Nookingtons)" },
            {0xE10B, "Chip" },
            {0xE10C, "Nook (Raffle)" },
            {0xE10D, "K.K. Slider" },
            {0xE10E, "Jack" },
            {0xE10F, "Jack" },
            {0xE110, "Jack" },
            {0xE111, "Jack" },
            {0xE112, "Jack" },
            {0xE113, "Jack" },
            {0xE114, "Timmy/Tommy" },
            {0xE115, "K.K. Slider" },
            {0xE116, "K.K. Slider" },
            {0xE117, "K.K. Slider" },
            {0xE118, "K.K. Slider" },
            {0xE119, "K.K. Slider" },
            {0xE11A, "Redd (Shop Stall)" },
            {0xE11B, "K.K. Slider" },
            {0xE11C, "K.K. Slider" },
            {0xE11D, "K.K. Slider" },
            {0xE11E, "K.K. Slider" },
            {0xE11F, "K.K. Slider" },
            {0xE120, "K.K. Slider" },
            {0xE121, "K.K. Slider" },
            {0xE122, "K.K. Slider" },
            {0xE123, "K.K. Slider" },
            {0xE124, "K.K. Slider" },
            {0xE125, "Timmy/Tommy" },
            {0xE126, "Joan" },
            {0xE127, "Redd (shop stall)" },
            {0xE128, "nook (raffle)" },
            {0xE129, "nook (raffle)" },
            {0xE12A, "nook (raffle)" },
            {0xE12B, "Katrina" },
            {0xE12C, "Resetti" },
            {0xE12D, "K.K. Slider" },
            {0xE12E, "K.K. Slider" },
            {0xE12F, "K.K. Slider" },
            {0xE130, "K.K. Slider" },
            {0xE131, "K.K. Slider" },
            {0xE132, "K.K. Slider" },
            {0xE133, "K.K. Slider" },
            {0xE134, "K.K. Slider" },
            {0xE135, "K.K. Slider" },
            {0xE136, "K.K. Slider" },
            {0xE137, "K.K. Slider" },
            {0xE138, "K.K. Slider" },
            {0xE139, "K.K. Slider" },
            {0xE13A, "K.K. Slider" },
            {0xE13B, "K.K. Slider" },
            {0xE13C, "Copper (Workout Uniform)" },
            {0xE13D, "K.K. Slider" },
            {0xE13E, "K.K. Slider" },
            {0xE13F, "K.K. Slider" },
            {0xE140, "K.K. Slider" },
            {0xE141, "K.K. Slider" },
            {0xE142, "K.K. Slider" },
            {0xE143, "K.K. Slider" },
            {0xE144, "K.K. Slider" },
            {0xE145, "K.K. Slider" },
            {0xE146, "K.K. Slider" },
            {0xE147, "K.K. Slider" },
            {0xE148, "K.K. Slider" },
            {0xE149, "K.K. Slider" },
            {0xE14A, "K.K. Slider" },
            {0xE14B, "K.K. Slider" },
            {0xE14C, "K.K. Slider" },
            {0xE14D, "K.K. Slider" },
            {0xE14E, "K.K. Slider" },
            {0xE14F, "K.K. Slider" },
            {0xE150, "K.K. Slider" },
            {0xE151, "K.K. Slider" },
            {0xE152, "Gulliver" },
            {0xE153, "Resetti" },
            {0xE154, "K.K. Slider" },
            {0xE155, "Porter" },
            {0xE156, "Resetti" },
            {0xE157, "Resetti" },
            {0xE158, "K.K. Slider" },
            {0xE159, "Resetti" },
            {0xE15A, "Blazel" },
            {0xE15B, "Blathers" },
            {0xE15C, "Tortimer" },
            {0xE15D, "Wisp" },
            {0xE15E, "Mable" },
            {0xE15F, "Sable" },
            {0xE160, "Kapp'n" },
            {0xE161, "K.K. Slider" },
            {0xE162, "Tortimer" },
            {0xE163, "Blanca (Strange Empty Face)" },
            {0xE164, "Blanca (Strange Empty Face)" },
            {0xE165, "K.K. Slider" },
            {0xE166, "Tortimer" },
            {0xE167, "Jack" },
            {0xE168, "Porter" },
            {0xE169, "Gulliver" },
            {0xE16A, "Resetti" },
            {0xE16B, "Resetti" },
            {0xE16C, "Don Resetti" },
            {0xE16D, "Tortimer" },
            {0xE16E, "Resetti" },
            {0xE16F, "Resetti (Groundhog Day Suit)" },
            {0xE170, "K.K. Slider" },
            {0xE171, "K.K. Slider" },
            {0xE172, "K.K. Slider" },
            {0xE173, "K.K. Slider" },
            {0xE174, "K.K. Slider" },
            {0xE175, "Tortimer" },
            {0xE176, "K.K. Slider" },
            {0xE177, "K.K. Slider" },
            {0xE178, "K.K. Slider" },
            {0xE179, "K.K. Slider" },
            {0xE17A, "K.K. Slider" },
            {0xE17B, "Franklin" },
            {0xE17C, "Invisible (Wisp?)" },
            {0xE17D, "K.K. Slider" },
            {0xD06C, "Blazel (Kapp'n AI)" },
        };

        public static List<KeyValuePair<ushort, string>> VillagerDatabase = new List<KeyValuePair<ushort, string>>();

        public static void CreateVillagerDatabase()
        {
            foreach (KeyValuePair<ushort, string> k in Villagers)
                VillagerDatabase.Add(k);
        }

        public static string[] Personalities = new string[6]
        {
            "Lazy", "Normal", "Peppy", "Jock", "Cranky", "Snooty"
        };

        public static string GetVillagerName(ushort villagerId)
        {
            if (Villagers.ContainsKey(villagerId))
                return Villagers[villagerId];
            return villagerId == 0x0 ? "No Villager" : "Unknown";
        }

        public static string GetVillagerPersonality(int type)
        {
            return type < 6 ? Personalities[type] : "Lazy";
        }

        public static int GetVillagerPersonalityID(string personality)
        {
            return Array.IndexOf(Personalities, personality) > -1 ? Array.IndexOf(Personalities, personality) : 0;
        }

        public static ushort GetVillagerID(string villagerName)
        {
            if (Villagers.ContainsValue(villagerName))
                return Villagers.FirstOrDefault(x => x.Value == villagerName).Key;
            return 0xE000;
        }

        public static ushort GetVillagerIdByIndex(int i)
        {
            if (Villagers.Count > i)
                return Villagers.Keys.ElementAt(i);
            return 0xE000;
        }
    }

    public class Villager
    {
        public ushort ID = 0;
        public ushort TownIdentifier = 0;
        public string Name = "";
        public string Personality = "";
        public byte PersonalityID = 0;
        public int Index = 0;
        public string Catchphrase = "";
        public bool Exists = false;
        public bool Modified = false;
        public Item Shirt;
        public byte[] House_Coords = new byte[4]; //X-Acre, Y-Acre, Y-Position, X-Position - 1 (This is actually the location of their sign, also dictates map location)
        public Villager_Player_Entry[] Villager_Player_Entries = new Villager_Player_Entry[7];
        public int Offset = 0;

        public Villager(int idx)
        {
            Index = idx;
            Offset = Index == 16 ? MainForm.Islander_Offset : MainForm.VillagerData_Offset + (Index - 1) * 0x988;
            ID = DataConverter.ReadRawUShort(Offset, 2)[0];
            TownIdentifier = DataConverter.ReadRawUShort(Offset + 2, 2)[0];
            Name = VillagerData.GetVillagerName(ID);
            PersonalityID = DataConverter.ReadDataRaw(Offset + 0xD, 1)[0];
            Personality = VillagerData.GetVillagerPersonality(PersonalityID);
            Catchphrase = DataConverter.ReadString(Offset + 0x89D, 10).Trim();
            Shirt = new Item(DataConverter.ReadRawUShort(Offset + 0x8E4, 2)[0]);
            House_Coords = DataConverter.ReadDataRaw(Offset + 0x899, 4); //Could make this WorldCoords class if used for other things
            House_Coords[2] = (byte)(House_Coords[2] + 1);
            //House_Coords[3] = (byte)(House_Coords[3] + 1); //X-Position is the position of the Villager Name Sign, which is to the left of the house object, so we add one.
            Exists = ID != 0x0000 && ID != 0xFFFF;
            for (int i = 0; i < 7; i++)
            {
                int Entry_Offset = Offset + 0x10 + (i * 0x138); //Offet + 16 data bytes + entrynum * entrysize
                uint Player_ID = DataConverter.ReadUInt(Entry_Offset + 0x10);
                if (Player_ID < 0xFFFFFFFF && Player_ID >= 0xF0000000)
                    Villager_Player_Entries[i] = new Villager_Player_Entry(DataConverter.ReadDataRaw(Entry_Offset, 0x138));
            }
        }

        public void Write()
        {
            House_Coords[2] = (byte)(House_Coords[2] - 1);
            //House_Coords[3] = (byte)(House_Coords[3] - 1);
            DataConverter.WriteUShort(new ushort[] { ID }, Offset);
            DataConverter.WriteUShort(new ushort[] { TownIdentifier }, Offset + 2);
            DataConverter.WriteDataRaw(Offset + 0xC, new byte[] { Index == 16 ? (byte)0xFF : (byte)(ID & 0x00FF) }); //Normally same as villager identifier, but is 0xFF for islanders. This is likely the byte for what AI the villager will use.
            DataConverter.WriteDataRaw(Offset + 0xD, new byte[] { PersonalityID });
            DataConverter.WriteString(Offset + 0x89D, Catchphrase, 10);
            DataConverter.WriteDataRaw(Offset + 0x899, House_Coords);
            if (Shirt != null)
                DataConverter.WriteUShort(new ushort[] { Shirt.ItemID }, Offset + 0x8E4);
            if (!Exists && Modified)
            {
                DataConverter.WriteString(Offset + 4, DataConverter.ReadString(MainForm.Town_Name_Offset, 8).Trim(), 8); //Set town name
                DataConverter.WriteDataRaw(Offset + 0x8EB, new byte[] { 0xFF, 0x01 }); //This byte might be the met flag. Setting it just in case
                Exists = true;
                if (Index < 16)
                    Add_House();
            }
            Modified = false;
            //Second byte here is always a random number. This could be responsible for the Villager's AI, but I'm not sure. Just writing it for good measure.
            //If the Villager's house location is out of bounds, (or just left 0xFFFF) the game will pick a random signboard as the new house location and write it on load.
        }

        public void Delete()
        {
            if (Index < 16) //Don't delete islander
            {
                if (Properties.Settings.Default.ModifyVillagerHouse)
                    Remove_House();
                ID = 0;
                TownIdentifier = 0xFFFF;
                PersonalityID = 6;
                Catchphrase = "";
                House_Coords = new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF };
                Shirt = new Item(0);
                Exists = false;
                Modified = false;
            }
        }

        public void Remove_House()
        {
            ushort House_ID = BitConverter.ToUInt16(new byte[] { (byte)(ID & 0x00FF), 0x50 }, 0);
            ushort[] World_Buffer = DataConverter.ReadRawUShort(MainForm.AcreData_Offset, MainForm.AcreData_Size);
            for (int i = 0; i < World_Buffer.Length; i++)
            {
                if (World_Buffer[i] == House_ID)
                {
                    for (int x = i - 17; x < i - 14; x++) //First Row
                        World_Buffer[x] = 0;
                    for (int x = i - 1; x < i + 2; x++) //Middle Row
                        World_Buffer[x] = 0;
                    for (int x = i + 15; x < i + 18; x++) //Final Row
                        World_Buffer[x] = 0;
                    World_Buffer[i] = BitConverter.ToUInt16(new byte[] { (byte)(new Random().Next(0x10, 0x25)), 0x58 }, 0); //New Signboard to replace house
                    //This is akin to actual game behavior
                }
            }
            DataConverter.WriteUShort(World_Buffer, MainForm.AcreData_Offset);
        }

        public void Add_House()
        {
            ushort House_ID = BitConverter.ToUInt16(new byte[] { (byte)(ID & 0x00FF), 0x50 }, 0);
            ushort[] World_Buffer = DataConverter.ReadRawUShort(MainForm.AcreData_Offset, MainForm.AcreData_Size);
            if (House_Coords[0] > 5 || House_Coords[1] > 6 || House_Coords[2] > 15 || House_Coords[3] > 15) //Houses can't be on edge of acres
                return;
            int Position = (House_Coords[0] - 1) * 256 + (House_Coords[1] - 1) * 1280 + (House_Coords[2]) + (House_Coords[3] - 1) * 16; //X Acre + Y Acre + X Pos + Y Pos
            if (Position > 0x1E00) //7,680 item spots per town (minus island acres) (5 * 6 * 16^2)
                return;
            for (int x = Position - 17; x < Position - 14; x++) //First Row
                World_Buffer[x] = 0xFFFF;
            for (int x = Position - 1; x < Position + 2; x++) //Middle Row
                World_Buffer[x] = 0xFFFF;
            for (int x = Position + 15; x < Position + 18; x++) //Final Row
                World_Buffer[x] = 0xFFFF;
            World_Buffer[Position] = House_ID;
            World_Buffer[Position + 15] = 0xA012; //Add Nameplate

            DataConverter.WriteUShort(World_Buffer, MainForm.AcreData_Offset);
        }
    }

    /*Villager Player Entry Structure (Size: 0x138):
        Player Name: 0x000 - 0x007
        Town Name: 0x008 - 0x00F
        Player ID: 0x010 - 0x013
        Met Date: 0x014 - 0x01B
        Met Town Name: 0x01C - 0x023
        Met Town ID: 0x024 - 0x025
        Padding??: 0x026 - 0x027
        Unknown Data: 0x028 - 0x02F
        Unknown Byte: 0x030 - 0x032
        Padding??: 0x033 - 0x037
        Saved Message: 0x038 - 0x138
    */
    public class Villager_Player_Entry
    {
        public bool Exists = false;
        //Struct Start
        public string Player_Name;
        public string Player_Town_Name;
        public uint Player_ID;
        public ACDate Met_Date;
        public string Met_Town_Name;
        public uint Met_Town_ID;
        public byte[] Garbage = new byte[8]; //I have no idea wtf these are for. Might investigate some day.
        //

        public Villager_Player_Entry(byte[] entryData)
        {
            Exists = true;
            byte[] playerNameBytes = new byte[8], playerTownName = new byte[8], metTownName = new byte[8], metDate = new byte[8], playerId = new byte[4];
            Buffer.BlockCopy(entryData, 0, playerNameBytes, 0, 8);
            Buffer.BlockCopy(entryData, 8, playerTownName, 0, 8);
            Buffer.BlockCopy(entryData, 0x1C, metTownName, 0, 8);
            Buffer.BlockCopy(entryData, 0x10, playerId, 0, 4);
            Buffer.BlockCopy(entryData, 0x14, metDate, 0, 8);
            Array.Reverse(playerId);

            Player_Name = new ACString(playerNameBytes).Trim();
            Player_Town_Name = new ACString(playerTownName).Trim();
            Met_Town_Name = new ACString(metTownName).Trim();

            Met_Date = new ACDate(metDate);
            Player_ID = BitConverter.ToUInt32(playerId, 0);
            Met_Town_ID = BitConverter.ToUInt16(entryData, 0x24);
        }
    }
}

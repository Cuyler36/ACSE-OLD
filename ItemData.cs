using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Animal_Crossing_GCN_Save_Editor
{
    public class ItemData
    {
        //REDO THIS UGLY CODE

        public static ushort[] Furniture_IDs = new ushort[838]
        {
            0x1C68, 0x1C6C, 0x1C70, 0x1C74, 0x1C78, 0x1C7C, 0x1C80, 0x1C84, 0x1C88, 0x1C8C, 0x1C90, 0x1C94, 0x1C98, 0x1C9C, 0x1CA0, 0x1CA4, 0x1CA8, 0x1CAC, 0x1CB0, 0x1CB4, 0x1CB8, 0x1CBC, 0x1CC0, 0x1CC4, 0x1CC8, 0x1CCC, 0x1CD0, 0x1CD4, 0x1CD8, 0x1CDC, 0x1CE0, 0x1CE4, 0x1CE8, 0x1CEC, 0x1CF0, 0x1CF4, 0x1CF8, 0x1CFC, 0x1D00, 0x1D04, 0x1EEC, 0x1EF0, 0x1EF4, 0x1EF8, 0x1EFC, 0x1F00, 0x1F04, 0x1F08, 0x1F0C, 0x1F10, 0x1F14, 0x1F18, 0x1F1C, 0x1F20, 0x1F24, 0x1F28, 0x1F2C, 0x1F30, 0x1F34, 0x1F38, 0x1F3C, 0x1F40, 0x1F44, 0x1F48, 0x1F4C, 0x1350, 0x1354, 0x1358, 0x135C, 0x1360, 0x1364, 0x1368, 0x136C, 0x1370, 0x1374, 0x1378, 0x137C, 0x1380, 0x1384, 0x1388, 0x138C, 0x1390, 0x1394, 0x1398, 0x139C, 0x13A0, 0x13A4, 0x13A8, 0x13AC, 0x13B0, 0x13B4, 0x13B8, 0x13BC, 0x13C0, 0x13C4, 0x13C8, 0x13CC, 0x13D0, 0x13D4, 0x13D8, 0x13DC, 0x13E0, 0x13E4, 0x13E8, 0x13EC, 0x13F0, 0x13F4, 0x13F8, 0x13FC, 0x1400, 0x1404, 0x1408, 0x140C, 0x1410, 0x1414, 0x1418, 0x141C, 0x1420, 0x1424, 0x1428, 0x142C, 0x1430, 0x1434, 0x1438, 0x143C, 0x1440, 0x1444, 0x1448, 0x144C, 0x1450, 0x1454, 0x1458, 0x145C, 0x1460, 0x1464, 0x1468, 0x146C, 0x1470, 0x1474, 0x1478, 0x147C, 0x1480, 0x1484, 0x1488, 0x148C, 0x1490, 0x1494, 0x1498, 0x149C, 0x14A0, 0x14A4, 0x14A8, 0x14AC, 0x14B0, 0x14B4, 0x14B8, 0x14BC, 0x14C0, 0x14C4, 0x14C8, 0x14CC, 0x14D0, 0x14D4, 0x14D8, 0x14DC, 0x14E0, 0x14E4, 0x14E8, 0x14EC, 0x14F0, 0x14F4, 0x14F8, 0x14FC, 0x1500, 0x1504, 0x1508, 0x150C, 0x1510, 0x1514, 0x1518, 0x151C, 0x1520, 0x1524, 0x1528, 0x152C, 0x1530, 0x1534, 0x1538, 0x153C, 0x1540, 0x1544, 0x1548, 0x154C, 0x1550, 0x1554, 0x1558, 0x155C, 0x1560, 0x1564, 0x1568, 0x156C, 0x1570, 0x1574, 0x1578, 0x157C, 0x1580, 0x1584, 0x1588, 0x158C, 0x1590, 0x1594, 0x1598, 0x159C, 0x15A0, 0x15A4, 0x15A8, 0x15AC, 0x1000, 0x1004, 0x1008, 0x100C, 0x1010, 0x1014, 0x1018, 0x101C, 0x1020, 0x1024, 0x1028, 0x102C, 0x1030, 0x1034, 0x1038, 0x103C, 0x1040, 0x1044, 0x1048, 0x104C, 0x1050, 0x1054, 0x1058, 0x105C, 0x1060, 0x1064, 0x1068, 0x106C, 0x1070, 0x1074, 0x1078, 0x107C, 0x1080, 0x1084, 0x1088, 0x108C, 0x1090, 0x1094, 0x1098, 0x109C, 0x10A0, 0x10A4, 0x10A8, 0x10AC, 0x10B0, 0x10B4, 0x10B8, 0x10BC, 0x10C0, 0x10C4, 0x10C8, 0x10CC, 0x10D0, 0x10D4, 0x10D8, 0x10DC, 0x10E0, 0x10E4, 0x10E8, 0x10EC, 0x10F0, 0x10F4, 0x10F8, 0x10FC, 0x1100, 0x1104, 0x1108, 0x110C, 0x1110, 0x1114, 0x1118, 0x111C, 0x1120, 0x1124, 0x1128, 0x112C, 0x1130, 0x1134, 0x1138, 0x113C, 0x1140, 0x1144, 0x1148, 0x114C, 0x1150, 0x1154, 0x1158, 0x115C, 0x1160, 0x1164, 0x1168, 0x116C, 0x1170, 0x1174, 0x1178, 0x117C, 0x1180, 0x1184, 0x1188, 0x118C, 0x1190, 0x1194, 0x1198, 0x119C, 0x11A0, 0x11A4, 0x11A8, 0x11AC, 0x11B0, 0x11B4, 0x11B8, 0x11BC, 0x11C0, 0x11C4, 0x11C8, 0x11CC, 0x11D0, 0x11D4, 0x11D8, 0x11DC, 0x11E0, 0x11E4, 0x11E8, 0x11EC, 0x11F0, 0x11F4, 0x11F8, 0x11FC, 0x1200, 0x1204, 0x1208, 0x120C, 0x1210, 0x1214, 0x1218, 0x121C, 0x1220, 0x1224, 0x1228, 0x122C, 0x1230, 0x1234, 0x1238, 0x123C, 0x1240, 0x1244, 0x1248, 0x124C, 0x1250, 0x1254, 0x1258, 0x125C, 0x1260, 0x1264, 0x1268, 0x126C, 0x1270, 0x1274, 0x1278, 0x127C, 0x1280, 0x1284, 0x1288, 0x128C, 0x1290, 0x1294, 0x1298, 0x129C, 0x12A0, 0x12A4, 0x12A8, 0x12AC, 0x12B0, 0x12B4, 0x12B8, 0x12BC, 0x12C0, 0x12C4, 0x12C8, 0x12CC, 0x12D0, 0x12D4, 0x12D8, 0x12DC, 0x12E0, 0x12E4, 0x12E8, 0x12EC, 0x12F0, 0x12F4, 0x12F8, 0x12FC, 0x1300, 0x1304, 0x1308, 0x130C, 0x1310, 0x1314, 0x1318, 0x131C, 0x1320, 0x1324, 0x1328, 0x132C, 0x1330, 0x1334, 0x1338, 0x133C, 0x1340, 0x1344, 0x1348, 0x134C, 0x1DA8, 0x1DAC, 0x1DB0, 0x1DB4, 0x1DB8, 0x1DBC, 0x1DC0, 0x1DC4, 0x1DC8, 0x1DCC, 0x1DD0, 0x1DD4, 0x1DD8, 0x1DDC, 0x1DE0, 0x1DE4, 0x1DE8, 0x1DEC, 0x1DF0, 0x1DF4, 0x1DF8, 0x1DFC, 0x1E00, 0x1E04, 0x1E08, 0x1E0C, 0x1E10, 0x1E14, 0x1E18, 0x1E1C, 0x1E20, 0x1E24, 0x1E28, 0x1E2C, 0x1E30, 0x1E34, 0x1E38, 0x1E3C, 0x1E40, 0x1E44, 0x1E48, 0x1E4C, 0x1E50, 0x1E54, 0x1E58, 0x1E5C, 0x1E60, 0x1E64, 0x1E68, 0x1E6C, 0x1E70, 0x1E74, 0x1E78, 0x1E7C, 0x1E80, 0x1E84, 0x1E88, 0x1E8C, 0x1E90, 0x1E94, 0x1E98, 0x1E9C, 0x1EA0, 0x1EA4, 0x1EA8, 0x1EAC, 0x1EB0, 0x1EB4, 0x1EB8, 0x1EBC, 0x1EC0, 0x1EC4, 0x1EC8, 0x1ECC, 0x1ED0, 0x1ED4, 0x1ED8, 0x1EDC, 0x1EE0, 0x1EE4, 0x1EE8, 0x1F50, 0x1F54, 0x1F58, 0x1F5C, 0x1F60, 0x1F64, 0x1F68, 0x1F6C, 0x1F70, 0x1F74, 0x1F78, 0x1F7C, 0x1F80, 0x1F84, 0x1F88, 0x1F8C, 0x1F90, 0x1F94, 0x1F98, 0x1F9C, 0x1FA0, 0x1FA4, 0x1FA8, 0x1FAC, 0x1FB0, 0x1FB4, 0x1FB8, 0x1FBC, 0x1FC0, 0x1FC4, 0x1FC8, 0x1FCC, 0x1FD0, 0x1FD4, 0x1FD8, 0x1FDC, 0x1FE0, 0x1FE4, 0x1FE8, 0x1FEC, 0x1FF0, 0x1FF4, 0x1FF8, 0x1FFC, 0x3000, 0x3004, 0x3008, 0x300C, 0x3010, 0x3014, 0x3018, 0x301C, 0x3020, 0x3024, 0x3028, 0x302C, 0x3030, 0x3034, 0x3038, 0x303C, 0x3040, 0x3044, 0x3048, 0x304C, 0x3050, 0x3054, 0x3058, 0x305C, 0x3060, 0x3064, 0x3068, 0x306C, 0x3070, 0x3074, 0x3078, 0x307C, 0x3080, 0x3084, 0x3088, 0x308C, 0x3090, 0x3094, 0x3098, 0x309C, 0x30A0, 0x30A4, 0x30A8, 0x30AC, 0x30B0, 0x30B4, 0x30B8, 0x30BC, 0x30C0, 0x30C4, 0x30C8, 0x30CC, 0x30D0, 0x30D4, 0x30D8, 0x30DC, 0x30E0, 0x30E4, 0x30E8, 0x30EC, 0x30F0, 0x30F4, 0x319C, 0x31A0, 0x31A4, 0x31A8, 0x31AC, 0x31B0, 0x31B4, 0x31B8, 0x31BC, 0x31C0, 0x31C4, 0x31C8, 0x31CC, 0x31D0, 0x31D4, 0x31D8, 0x31DC, 0x31E0, 0x31E4, 0x31E8, 0x31EC, 0x31F0, 0x31F4, 0x31F8, 0x31FC, 0x3200, 0x3204, 0x3208, 0x320C, 0x3210, 0x3214, 0x3218, 0x321C, 0x3220, 0x3224, 0x3228, 0x322C, 0x3230, 0x3234, 0x3238, 0x323C, 0x3240, 0x3244, 0x3248, 0x324C, 0x3250, 0x3254, 0x3258, 0x325C, 0x3260, 0x3264, 0x3268, 0x326C, 0x3270, 0x3274, 0x3278, 0x327C, 0x3280, 0x3284, 0x3288, 0x328C, 0x3290, 0x3294, 0x3298, 0x329C, 0x32A0, 0x32A4, 0x32A8, 0x32AC, 0x32B0, 0x32B4, 0x32B8, 0x32BC, 0x32C0, 0x32C4, 0x32C8, 0x32CC, 0x32D0, 0x32D4, 0x32D8, 0x32DC, 0x32E0, 0x32E4, 0x32E8, 0x32EC, 0x32F0, 0x32F4, 0x32F8, 0x32FC, 0x3300, 0x3304, 0x3308, 0x330C, 0x3310, 0x3314, 0x3318, 0x331C, 0x3320, 0x3324, 0x3328, 0x332C, 0x3330, 0x3334, 0x3338, 0x333C, 0x3340, 0x3344, 0x3348, 0x334C, 0x3350, 0x3354, 0x3358, 0x335C, 0x3360, 0x3364, 0x3368, 0x336C, 0x3370, 0x3374, 0x3378, 0x337C, 0x3380, 0x3384, 0x3388, 0x338C, 0x3390, 0x3394, 0x3398, 0x339C, 0x33A0, 0x33A4, 0x33A8, 0x33AC, 0x33B0, 0x33B4, 0x33B8, 0x33BC, 0x33C0, 0x33C4, 0x1BC8, 0x1BCC, 0x1BD0, 0x1BD4, 0x1BD8, 0x1BDC, 0x1BE0, 0x1BE4, 0x1BE8, 0x1BEC, 0x1BF0, 0x1BF4, 0x1BF8, 0x1BFC, 0x1C00, 0x1C04, 0x1C08, 0x1C0C, 0x1C10, 0x1C14, 0x1C18, 0x1C1C, 0x1C20, 0x1C24, 0x1C28, 0x1C2C, 0x1C30, 0x1C34, 0x1C38, 0x1C3C, 0x1C40, 0x1C44, 0x1C48, 0x1C4C, 0x1C50, 0x1C54, 0x1C58, 0x1C5C, 0x1C60, 0x1C64, 0x313C, 0x3140, 0x3144, 0x3148, 0x314C, 0x3150, 0x3154, 0x3158, 0x315C, 0x3160, 0x3164, 0x3168, 0x316C, 0x3170, 0x3174, 0x3178, 0x317C, 0x3180, 0x3184, 0x3188, 0x318C, 0x3190, 0x3194, 0x3198, 0x30F8, 0x30FC, 0x3100, 0x3104, 0x3108, 0x310C, 0x3110, 0x3114, 0x3118, 0x311C, 0x3120, 0x3124, 0x3128, 0x312C, 0x3130, 0x3134, 0x3138, 0xFE0F, 0xFE16
        };
        public static string[] Furniture_Names = new string[838]
        {
            "Crucian Carp (Furniture)", "Brook Trout (Furniture)", "Carp (Furniture)", "Koi (Furniture)", "Catfish (Furniture)", "Small Bass (Furniture)", "Bass (Furniture)", "Large Bass (Furniture)", "Bluegill (Furniture)", "Giant Catfish (Furniture)", "Giant Snakehead (Furniture)", "Barbel Steed (Furniture)", "Dace (Furniture)", "Pale Chub (Furniture)", "Bitterling (Furniture)", "Loach (Furniture)", "Pond Smelt (Furniture)", "Sweetfish (Furniture)", "Cherry Salmon (Furniture)", "Large Char (Furniture)", "Rainbow Trout (Furniture)", "Stringfish (Furniture)", "Salmon (Furniture)", "Goldfish (Furniture)", "Piranha (Furniture)", "Arowana (Furniture)", "Eel (Furniture)", "Freshwater Goby (Furniture)", "Angelfish (Furniture)", "Guppy (Furniture)", "Popeyed Goldfish (Furniture)", "Coelacanth (Furniture)", "Crawfish (Furniture)", "Frog (Furniture)", "Killifish (Furniture)", "Jellyfish (Furniture)", "Sea Bass (Furniture)", "Red Snapper (Furniture)", "Barred Knifejaw (Furniture)", "Arapaima (Furniture)", "Tricera Skull", "Tricera Tail", "Tricera Torso", "T-rex Skull", "T-rex Tail", "T-rex Torso", "Apato Skull", "Apato Tail", "Apato Torso", "Stego Skull", "Stego Tail", "Stego Torso", "Ptera Skull", "Ptera Right Wing", "Ptera Left Wing", "Plesio Skull", "Plesio Neck", "Plesio Torso", "Mammoth Skull", "Mammoth Torso", "Amber", "Dinosaur Track", "Ammonite", "Dinosaur Egg", "Trilobite", "Modern Sofa", "Modern Table", "Blue Bed", "Blue Bench", "Blue Chair", "Blue Dresser", "Blue Bookcase", "Blue Table", "Green Bed", "Green Bench", "Green Chair", "Green Pantry", "Green Counter", "Green Lamp", "Green Table", "Cabin Bed", "Cabin Couch", "Cabin Armchair", "Cabin Bookcase", "Cabin Low Table", "Aloe", "Bromeliaceae", "Coconut Palm", "Snake Plant", "Dracaena", "Rubber Tree", "Pothos", "Fan Palm", "Grapefruit Table", "Lime Chair", "Weeping Fig", "Corn Plant", "Croton", "Pachira", "Cactus", "Metronome", "Deer Scare", "Pine Bonsai", "Mugho Bonsai", "Barber's Pole", "Ponderosa Bonsai", "Plum Bonsai", "Giant Dharma", "Dharma", "Mini-Dharma", "Quince Bonsai", "Azalea Bonsai", "Jasmine Bonsai", "Executive Toy", "Traffic Cone", "Striped Cone", "Orange Cone", "Cola Machine", "Maple Bonsai", "Hawthorne Bonsai", "Holly Bonsai", "Barricade", "Fence", "Plastic Fence", "Fence and Sign", "Soda Machine", "Manhole Cover", "Pop Machine", "Brown Drum", "Green Drum", "Red Drum", "Juice Machine", "Iron Frame", "Trash Can", "Grabage Pail", "Watermelon Chair", "Melon Chair", "Watermelon Table", "Robotic Flagman", "Garbage Can", "Trash Bin", "Violin", "Bass", "Cello", "Ebony Piano", "Zen Basin", "Handcart", "Wash Basin", "Jack-o'-Lantern", "Warning Sign", "Detour Arrow", "Garden Stone", "Standing Stone", "Route Sign", "Men Working Sign", "Caution Sign", "Temple Basin", "Spooky Bed", "Jack-in-the-Box", "Spooky Chair", "Unused Chair", "Spooky Bookcase", "Spooky Sofa", "Spooky Table", "Lunar Lander", "Satellite", "Mossy Stone", "Leaning Stone", "Dark Stone", "Flying Saucer", "Stone Couple", "Garden Pond", "Rocket", "Spaceman Sam", "Spooky Clock", "Spooky Lamp", "Exotic Bed", "Exotic End Table", "Asteroid", "Cabana Lamp", "Cabana Table", "Bucket", "Scale", "Faucet", "Spa Chair", "Cabana Screen", "Cabana Vanity", "Cabana Chair", "Cabana Bookcase", "Arwing", "Lunar Rover", "Massage Chair", "Bath Mat", "Spa Tub", "Blue Clock", "Mochi Pestle", "Spooky Vanity", "Green Desk", "Clerk's Booth", "Modern Chair", "Modern End Table", "Space Station", "Spa Screen", "Cabin Chair", "Regal Bed", "Space Shuttle", "Regal Vanity", "Regal Sofa", "Regal Lamp", "Cabin Table", "Bath Locker", "Milk Fridge", "Tea Set", "Nook's Portrait", "Gerbera", "Sunflower", "Daffodil", "Spooky Wardrobe", "Classic Wardrobe", "Blue Wardrobe", "Office Locker", "Jingle Wardrobe", "Regal Armoire", "Cabana Wardrobe", "Cabin Wardrobe", "Lovely Armoire", "Green Wardrobe", "Pear Wardrobe", "Ranch Wardrobe", "Blue Cabinet", "Modern Wardrobe", "Exotic Wardrobe", "Jingle Dresser", "Regal Dresser", "Cabana Dresser", "Cabin Dresser", "Lovely Dresser", "Spooky Dresser", "Green Dresser", "Pear Dresser", "Ranch Dresser", "Classic Vanity", "Blue Bureau", "Modern Dresser", "Exotic Bureau", "Kiddie Dresser", "Kiddie Bureau", "Kiddie Wardrobe", "Dresser", "Tansu", "Sewing Box", "Fan", "Paper Lantern", "Tea Table", "Shogi Board", "Screen", "Zabuton", "Bus Stop", "Froggy Chair", "Lilly-pad Table", "Refridgerator", "Chest", "Rack", "Red Sofa", "Red Armchair", "Hibachi", "Stove", "Cream Sofa", "Tea Tansu", "Pink Kotatsu", "Blue Kotatsu", "Folk Guitar", "Country Guitar", "Rock Guitar", "Hinaningyo", "Papa Bear", "Mama Bear", "Baby Bear", "Classic Hutch", "Classic Chair", "Classic Desk", "Classic Table", "Classic Cabinet", "Rocking Chair", "Regal Cupboard", "Writing Desk", "Keiko Figurine", "Yuki Figurine", "Yoko Figurine", "Emi Figurine", "Maki Figurine", "Naomi Figurine", "Globe", "Regal Chair", "Regal Table", "Retro TV", "Eagle Pole", "Raven Pole", "Bear Pole", "Frog Woman Pole", "Taiko Drum", "Space Heater", "Retro Stereo", "Cabana Armchair", "Classic Sofa", "Lovely End Table", "Lovely Armchair", "Ivory Piano", "Lovely Lamp", "Lovely Kitchen", "Lovely Chair", "Lovely Bed", "Classic Clock", "Cabana Bed", "Green Golf Bag", "White Golf Bag", "Blue Golf Bag", "Regal Bookcase", "Writing Chair", "Ranch Couch", "Ranch Armchair", "Ranch Tea Table", "Ranch Hutch", "Ranch Bookcase", "Ranch Chair", "Ranch Bed", "Ranch Table", "Computer", "Office Desk", "Master Sword", "N Logo", "Vibraphone", "Biwa Lute", "Conga Drum", "Extinguisher", "Ruby Econo-chair", "Gold Econo-chair", "Jade Econo-chair", "Gold Stereo", "Folding Chair", "Lovely Vanity", "Birdcage", "Timpano Drum", "Nice Speaker", "Birthday Cake", "School Desk", "Graffiti Desk", "Towel Desk", "Tall Cactus", "Round Cactus", "Classic Bed", "Wide-screen TV", "Lovely Table", "Kadomatsu", "Kagamimochi", "Low Lantern", "Tall Lantern", "Pond Lantern", "Office Chair", "Cubby Hole", "Letter Cubby", "Heavy Chair", "School Chair", "Towel Chair", "Science Table", "Stepstool", "Shrine Lantern", "Barrel", "Keg", "Vaulting Horse", "Glass-top Table", "Alarm Clock", "Tulip Table", "Daffodil Table", "Iris Table", "Blue Vase", "Tulip Chair", "Daffodil Chair", "Iris Chair", "Elephant Slide", "Toilet", "Super Toilet", "Pine Table", "Pine Chair", "Tea Vase", "Red Vase", "Sewing Machine", "Billiard Table", "Famous Painting", "Basic Painting", "Scarying Painting", "Moving Painting", "Flowery Painting", "Common Painting", "Quaint Painting", "Dainty Painting", "Amazing Painting", "Strange Painting", "Rare Painting", "Classic Painting", "Perfect Painting", "Fine Painting", "Worthy Painting", "Pineapple Bed", "Orange Chair", "Unused Dresser", "Lemon Table", "Apple TV", "Table Tennis", "Harp", "Cabin Clock", "Train Set", "Water Bird", "Wobbelina", "Unused Monkey", "Slot Machine", "Exotic Bench", "Exotic Chair", "Exotic Chest", "Exotic Lamp", "Caladium", "Lady Palm", "Exotic Screen", "Exotic Table", "Djimbe Drum", "Modern Bed", "Modern Den Chair", "Modern Cabinet", "Modern Desk", "Clu Clu Land", "Balloon Fight", "Donkey Kong", "DK Jr MATH", "Pinball", "Tennis", "Golf", "Punchout", "Baseball", "Clu Clu Land D", "Donkey Kong 3", "Donky Kong Jr", "Soccer", "Excitebike", "Wario's Woods", "Ice Climber", "Mario Bros", "Super Mario Bros", "Legend of Zelda", "NES", "Phonograph", "Turntable", "Jukebox", "Red Boom Box", "White Boom Box", "High-end Stereo", "Hi-fi Stereo", "Lovely Stereo", "Jingle Lamp", "Jingle Chair", "Jingle Shelves", "Jingle Sofa", "Jingle Bed", "Jingle Clock", "Jingle Table", "Jingle Piano", "Aiko Figurine", "Robo-Stereo", "Dice Stereo", "Apple Clock", "Robo-clock", "Kitschy Clock", "Antique Clock", "Reel-to-reel", "Tape Deck", "CD Player", "Glow Clock", "Odd Clock", "Red Clock", "Cube Clock", "Owl Clock", "Lucky Cat", "Lucky Black Cat", "Samurai Suit", "Racoon Obje", "Lucky Frog", "Big Festive Tree", "White Rook", "Black Rook", "White Queen", "Black Queen", "White Bishop", "Black Bishop", "White King", "Black King", "White Knight", "Black Knight", "White Pawn", "Black Pawn", "Festive Tree", "Kiddie Clock", "Kiddie Bed", "Kiddie Table", "Kiddie Couch", "Kiddie Stereo", "Kiddie Chair", "Kiddie Bookcase", "Alcove", "Hearth", "Chalk Board", "Mop", "Modern Lamp", "Snowman Fridge", "Snowman Table", "Snowman Bed", "Snowman Chair", "Snowman Lamp", "Snowman Sofa", "Snowman TV", "Snowman Dresser", "Snowman Wardrobe", "Snowman Clock", "Tricera D", "T-rex D", "Bronto D", "Ptera D", "HUTABAD", "Mammoth D", "Stego D", "Stego D2", "Fossil (Furniture)", "Shogi Piece", "Chocolates", "Post Box", "Piggy Bank", "Tissue", "Tribal Mask", "Matryoshka", "Legend of Zelda", "Bottled Shop", "Tiger Bobblehead", "Moai Statue", "Aerobics Radio", "Pagoda", "Fishing Bear", "Mouth of Truth", "Chinese Lioness", "Tower of Pisa", "Merlion", "Manekin Pis", "Tokyo Tower", "Red Balloon", "Yellow Balloon", "Blue Balloon", "Green Balloon", "Purple Balloon", "Bunny P. Balloon", "Bunny B. Balloon", "Bunny O. Balloon", "Lady Liberty", "Arc De Triomphe", "Stone Coin", "Mermaid Statue", "Post Model", "House Model", "Manor Model", "Police Model", "Museum Model", "Plate Armor", "Moon Dumpling", "Bean Set", "Osechi", "Lovely Phone", "Market Model", "Katrina's Tent", "Chinese Lion", "Tanabata Palm", "Spring Medal", "Fall Medal", "Shop Model", "Compass", "Long-life Noodle", "Bass Boat", "Lighthouse Model", "Life Ring", "Tree Model", "Pink Tree Model", "Weed Model", "Tailor Model", "Dump Model", "Mortar Ball", "Snowman", "Miniature Car", "Big Catch Flag", "Moon", "Locomotive Model", "Dolly", "Station Model 1", "Station Model 2", "Station Model 3", "Station Model 4", "Station Model 5", "Station Model 6", "Station Model 7", "Station Model 8", "Station Model 9", "Station Model 10", "Station Model 11", "Station Model 12", "Station Model 13", "Station Model 14", "Station Model 15", "Well Model", "Grass Model", "Track Model", "Dirt Model", "Train Car Model", "Crab Stew", "Fireplace", "Igloo Model", "Snowy Tree Model", "Snowcone Machine", "Treasure Chest", "Beach Chair", "Beach Table", "Hibachi Grill", "Surfboard", "Snowboard", "Wave Breaker", "Ukulele", "Diver Dan", "Snow Bunny", "Scary Painting", "Novel Painting", "Sleigh", "Nintendo Bench", "G Logo", "Merge Sign", "Bottle Rocket", "Wet Roadway Sign", "Detour Sign", "Men at Work Sign", "Lefty Desk", "Righty Desk", "School Desk", "Flagman Sign", "Fishing Trophy", "Jersey Barrier", "Speed Sign", "Golf Trophy", "Teacher's Desk", "Haz-mat Barrel", "Tennis Trophy", "Saw Horse", "Kart Trophy", "Bug Zapper", "Telescope", "Coffee Machine", "Bird Bath", "Barbecue", "Radiator", "Lawn Chair", "Chess Table", "Candy Machine", "Backyard Pool", "Cement Mixer", "Jackhammer", "Tiki Torch", "Birdhouse", "Potbelly Stove", "Bus Stop", "Hamster Cage", "Flip-top Desk", "Festive Flag", "Super Tortimer", "Bird Feeder", "Teacher's Chair", "Steam roller", "Mr. Flamingo", "Mailbox", "Festive Candle", "Hammock", "Garden Gnome", "Mrs. Flamingo", "Spring Medal (again?)", "Autumn Medal", "Tumbleweed", "Cow Skull", "Oil Drum", "Saddle Fence", "Western Fence", "Watering Trough", "Luigi Trophy", "Mario Trophy", "Harvest Lamp", "Covered Wagon", "Storefront", "Picnic Table", "Harvest Table", "Harvest TV", "Harvest Bed", "Harvest Chair", "Harvest Clock", "Harvest Sofa", "Green Pipe", "Brick Block", "Harvest Bureau", "Flagpole", "Harvest Dresser", "Super Mushroom", "Harvest Mirror", "Coin", "? Block", "Starman", "Koopa Shell", "Cannon", "Desert Cactus", "Fire Flower", "Wagon Wheel", "Well", "Boxing Barricade", "Neutral Corner", "Red Corner", "Blue Corner", "Boxing Mat", "Ringside Table", "Speed Bag", "Sandbag", "Weight Bench", "Campfire", "Bonfire", "Kayak", "Sprinkler", "Tent Model", "Backpack", "Angler Trophy", "Pansy Model 1", "Pansy Model 2", "Pansy Model 3", "Cosmos Model 1", "Cosmos Model 2", "Cosmos Model 3", "Tulip Model 1", "Tulip Model 2", "Tulip Model 3", "Lantern", "Lawn Mower", "Cooler", "Mountain Bike", "Sleeping Bag", "Propane Stove", "Cornucopia", "Judge's Bell", "Noisemaker", "Chowder", "DUMMY", "Common Butterfly (Furniture)", "Yellow Butterfly (Furniture)", "Tiger Butterfly (Furniture)", "Purple Butterfly (Furniture)", "Robust Cicada (Furniture)", "Walker Cicada (Furniture)", "Evening Cicada (Furniture)", "Brown Cicada (Furniture)", "Bee (Furniture)", "Common Dragonfly (Furniture)", "Red Dragonfly (Furniture)", "Darner Dragonfly (Furniture)", "Banded Dragonfly (Furniture)", "Long Locust (Furniture)", "Migratory Locust (Furniture)", "Cricket (Furniture)", "Grasshopper (Furniture)", "Bell Cricket (Furniture)", "Pine Cricket (Furniture)", "Drone Beetle (Furniture)", "Dynastid Beetle (Furniture)", "Flat Stag Beetle (Furniture)", "Jewel Beetle (Furniture)", "Longhorn Beetle (Furniture)", "Ladybug (Furniture)", "Spotted Ladybug (Furniture)", "Mantis (Furniture)", "Firefly (Furniture)", "Cockroach (Furniture)", "Saw Stag Beetle (Furniture)", "Mountain Beetle (Furniture)", "Giant Beetle (Furniture)", "Snail (Furniture)", "Mole Cricket (Furniture)", "Pond Skater (Furniture)", "Bagworm (Furniture)", "Pill Bug (Furniture)", "Spider (Furniture)", "Ant (Furniture)", "Mosquito (Furniture)", "Golden Net", "Golden Axe (Furniture)", "Golden Shovel (Furniture)", "Golden Rod (Furniture)", "Bluebell Fan (Furniture)", "Plum Fan (Furniture)", "Bamboo Fan (Furniture)", "Cloud Fan (Furniture)", "Maple Fan (Furniture)", "Fan Fan (Furniture)", "Flower Fan (Furniture)", "Leaf Fan (Furniture)", "Yellow Pinwheel (Furniture)", "Red Pinwheel (Furniture)", "Tiger Pinwheel (Furniture)", "Green Pinwheel (Furniture)", "Pink Pinwheel (Furniture)", "Striped Pinwheel (Furniture)", "Flower Pinwheel (Furniture)", "Fancy Pinwheel (Furniture)", "Net (Furniture)", "Axe (Furniture)", "Shovel (Furniture)", "Fishing Rod (Furniture)", "Orange Box", "College Rule (Furniture)", "School Pad (Furniture)", "Organizer (Furniture)", "Diary (Furniture)", "Journal (Furniture)", "Pink Diary (Furniture)", "Captain's Log (Furniture)", "Blue Diary (Furniture)", "French Notebook (Furniture)", "Scroll (Furniture)", "Pink Plaid Pad (Furniture)", "Blue Polka Pad (Furniture)", "Green Plaid Pad (Furniture)", "Red Polka Pad (Furniture)", "Yellow Plaid Pad (Furniture)", "Calligraphy Pad (Furniture)", "Empty Manniquin", "Sold Out Sign"
        };

        //Villager Houses are 50XX (Where XX is the Villager Identification Byte) Ex: Mitzi > 5002
        //0x580A = Train (Front) < Becomes invisible/is removed
        //0x580B = Train (Caboose) < Causes game to crash

        public static List<ushort> acreItemIDs = new List<ushort> {
            0x0005, 0x0006,
            0x0007, 0x000B, 0x000C, 0x000D, 0x000E, 0x000F, 0x0010,
            0x0011,
            0x0800,
            0x0804, 0x080C, 0x0814, 0x081C, 0x0824, 0x082C, 0x085B,
            0x0861, 0x0867, 0x0868, 0x0079, 0x0802, 0x0078, 0x0069,
            0x005E, 0x007A, 0x0060, 0x0082, 0x005F,
            0x0845, 0x0846, 0x0847, 0x0848, 0x0849, 0x084A, 0x084B,
            0x084C, 0x084D,
            0x0008, 0x0009, 0x000A,
            0x5808, 0x583B, 0x5843, 0x5809, 0x5804, 0x5805, 0x5806, 0x5807,
            0x584A, 0x584D, 0x5825, 0x580C,
            0x0063, 0x0064, 0x0065, 0x0066, 0x0067,
            0x580D, 0x580E, 0x580F,
            0x5810, 0x5811, 0x5812, 0x5813, 0x5814, 0x5815, 0x5816,
            0x5817, 0x5818, 0x5819, 0x581A, 0x581B, 0x581C, 0x581D,
            0x581E, 0x581F, 0x5820, 0x5821, 0x5822, 0x5823, 0x5824, 
            0x5800, 0x5801, 0x5802, 0x5803,
            0x5841, 0x5842, 0x5844, 0x5852,
            0xA000, 0xA001, 0xA002, 0xA003,
            0xA004, 0xA005, 0xA006, 0xA007,
            0xA012,
            0xFE1D, 0xFE1E,
            0x584E, 0x584F, 0x5851, 0x5850,
            0x5826, 0x5827, 0x5828, 0x5829, 0x582A, 0x582B, 0x582C,
            0x582D, 0x582E, 0x582F, 0x5830, 0x5831, 0x5832, 0x5833,
            0x5834, 0x5835, 0x5836, 0x5837, 0x5838, 0x5839, 0x583A, //583C - 5840
            0x5845, 0x5846, 0x5847, 0x5848, 0x5849,
            0x584B, 0x584C,
            0xFFFF
        }; //FE 1D || FE 1E = river? (to the right of the middle of the river) (Possibly shows new bridge locations?)
        public static List<string> acreItemNames = new List<string>
        {
            "Fence (Type 1)", "Fence (Type 2)",
            "Message Board (B)", "Message Board (A)", "Map Board (B)", "Map Board (A)", "Music Board (B)", "Music Board (A)", "Wooden Fence",
            "Hole",
            "Sapling (Stage 1)",
            "Tree", "Apple Tree (Fruit)", "Orange Tree (Fruit)", "Peach Tree (Fruit)", "Pear Tree (Fruit)", "Cherry Tree(Fruit)", "Palm Tree (Fruit)",
            "Cedar Tree", "Golden Tree (Golden Shovel)", "Golden Tree", "Cedar Tree (Furniture)", "Tree (Growing)", "Cedar Tree (Bells)", "Tree (Bells)",
            "Tree (Bees)", "Cedar Tree (Bees)", "Tree (Festive Lights)", "Cedar Tree (Festive Lights)", "Tree (Furniture)",
            "White Pansies", "Purple Pansies", "Yellow Pansies", "Yellow Cosmos", "Purple Cosmos", "Blue Cosmos", "Red Tulips",
            "White Tulips", "Yellow Tulips",
            "Weed", "Weed", "Weed",
            "Post Office", "Dump", "Train Station (Left)", "Train Station (Right)", "Nook's Cranny", "Nook 'n' Go", "Nookway", "Nookington's",
            "Museum", "Tailor's Shop", "Wishing Well", "Police Station",
            "Rock (Type 1)", "Rock (Type 2)", "Rock (Type 3)", "Rock (Type 4)", "Rock (Type 5)",
            "Waterfall", "Waterfall (Left)", "Waterfall (Right)",
            "Signboard (1)", "Signboard (2)", "Signboard (3)", "Signboard (4)", "Signboard (5)", "Signboard (6)", "Signboard (7)",
            "Signboard (8)", "Signboard (9)", "Signboard (10)", "Signboard (11)", "Signboard (12)", "Signboard (13)", "Signboard (14)",
            "Signboard (15)", "Signboard (16)", "Signboard (17)", "Signboard (18)", "Signboard (19)", "Signboard (20)", "Signboard (21)",
            "Player 1's House", "Player 2's House", "Player 3's House", "Player 4's House",
            "Lily Pads", "K.K. Slider's Box", "Lighthouse", "Dock Signboard",
            "Upper Left House's Mailbox", "Upper Right House's Mailbox", "Lower Left House's Mailbox", "Lower Right House's Mailbox",
            "Upper Left House's Gyroid", "Upper Right House's Gyroid", "Lower Left House's Gyroid", "Lower Right House's Gyroid",
            "Villager Signboard",
            "Possible Bridge Location? (Upper)", "Possible Bridge Location? (Lower)",
            "Island Flag", "Kapp'n w/ Boat", "Islander's House", "Player's Island House",
            "Crazy Redd's Tent", "Katrina's Tent", "Gracie's Car", "Igloo", "Cherry Festival Table #1", "Cherry Festival Table #2", "Aerobics Radio",
            "Redd's Stall (Right)", "Redd's Stall (Left)", "Katrina's Shrine (Right)", "Katrina's Shrine (Left)", "Katrina's New Years Table", "New Years Clock (Part 1)", "New Years Clock (Part 2)",
            "Red Sports Fair Balls", "White Sports Fair Balls", "Red Sports Fair Basket", "White Sports Fair Basket", "Fish Check Stand (Right)", "Fish Check Stand (Left)", "Fish Windsock",
            "Tortimer's Stand (Groundhog Day)", "Cherry Blossom Festival Table #1", "Cherry Blossom Festival Table #2", "Harvest Festival Table", "Camping Tent",
            "Suspension Bridge (/)", "Suspension Bridge (\\)",
            "Occupied/Unavailable"
        };

        public static ushort[] Shirt_IDs = new ushort[255] { 0x2400, 0x2401, 0x2402, 0x2403, 0x2404, 0x2405, 0x2406, 0x2407, 0x2408, 0x2409, 0x240A, 0x240B, 0x240C, 0x240D, 0x240E, 0x240F, 0x2410, 0x2411, 0x2412, 0x2413, 0x2414, 0x2415, 0x2416, 0x2417, 0x2418, 0x2419, 0x241A, 0x241B, 0x241C, 0x241D, 0x241E, 0x241F, 0x2420, 0x2421, 0x2422, 0x2423, 0x2424, 0x2425, 0x2426, 0x2427, 0x2428, 0x2429, 0x242A, 0x242B, 0x242C, 0x242D, 0x242E, 0x242F, 0x2430, 0x2431, 0x2432, 0x2433, 0x2434, 0x2435, 0x2436, 0x2437, 0x2438, 0x2439, 0x243A, 0x243B, 0x243C, 0x243D, 0x243E, 0x243F, 0x2440, 0x2441, 0x2442, 0x2443, 0x2444, 0x2445, 0x2446, 0x2447, 0x2448, 0x2449, 0x244A, 0x244B, 0x244C, 0x244D, 0x244E, 0x244F, 0x2450, 0x2451, 0x2452, 0x2453, 0x2454, 0x2455, 0x2456, 0x2457, 0x2458, 0x2459, 0x245A, 0x245B, 0x245C, 0x245D, 0x245E, 0x245F, 0x2460, 0x2461, 0x2462, 0x2463, 0x2464, 0x2465, 0x2466, 0x2467, 0x2468, 0x2469, 0x246A, 0x246B, 0x246C, 0x246D, 0x246E, 0x246F, 0x2470, 0x2471, 0x2472, 0x2473, 0x2474, 0x2475, 0x2476, 0x2477, 0x2478, 0x2479, 0x247A, 0x247B, 0x247C, 0x247D, 0x247E, 0x247F, 0x2480, 0x2481, 0x2482, 0x2483, 0x2484, 0x2485, 0x2486, 0x2487, 0x2488, 0x2489, 0x248A, 0x248B, 0x248C, 0x248D, 0x248E, 0x248F, 0x2490, 0x2491, 0x2492, 0x2493, 0x2494, 0x2495, 0x2496, 0x2497, 0x2498, 0x2499, 0x249A, 0x249B, 0x249C, 0x249D, 0x249E, 0x249F, 0x24A0, 0x24A1, 0x24A2, 0x24A3, 0x24A4, 0x24A5, 0x24A6, 0x24A7, 0x24A8, 0x24A9, 0x24AA, 0x24AB, 0x24AC, 0x24AD, 0x24AE, 0x24AF, 0x24B0, 0x24B1, 0x24B2, 0x24B3, 0x24B4, 0x24B5, 0x24B6, 0x24B7, 0x24B8, 0x24B9, 0x24BA, 0x24BB, 0x24BC, 0x24BD, 0x24BE, 0x24BF, 0x24C0, 0x24C1, 0x24C2, 0x24C3, 0x24C4, 0x24C5, 0x24C6, 0x24C7, 0x24C8, 0x24C9, 0x24CA, 0x24CB, 0x24CC, 0x24CD, 0x24CE, 0x24CF, 0x24D0, 0x24D1, 0x24D2, 0x24D3, 0x24D4, 0x24D5, 0x24D6, 0x24D7, 0x24D8, 0x24D9, 0x24DA, 0x24DB, 0x24DC, 0x24DD, 0x24DE, 0x24DF, 0x24E0, 0x24E1, 0x24E2, 0x24E3, 0x24E4, 0x24E5, 0x24E6, 0x24E7, 0x24E8, 0x24E9, 0x24EA, 0x24EB, 0x24EC, 0x24ED, 0x24EE, 0x24EF, 0x24F0, 0x24F1, 0x24F2, 0x24F3, 0x24F4, 0x24F5, 0x24F6, 0x24F7, 0x24F8, 0x24F9, 0x24FA, 0x24FB, 0x24FC, 0x24FD, 0x24FE};
        public static string[] Shirt_Names = new string[255] { "Flame Shirt", "Paw Shirt", "Wavy Pink Shirt", "Furture Shirt", "Bold Check Shirt", "Mint Gingham", "Bad Plaid Shirt", "Speedway Shirt", "Folk Shirt", "Daisy Shirt", "Wavy Tan Shirt", "Optical Shirt", "Rugby Shirt", "Sherbet Gingham", "Yellow Tartan", "Gelato Shirt", "Work Uniform", "Patched Shirt", "Plum Kimono", "Somber Robe", "Red Sweatsuit", "Blue Sweatsuit", "Red Puffy Vest", "Blue Puffy Vest", "Summer Robe", "Bamboo Robe", "Red Aloha Shirt", "Blue Aloha Shirt", "Dark Polka Shirt", "Lite Polka Shirt", "Lovely Shirt", "Citrus Shirt", "Kiwi Shirt", "Watermelon Shirt", "Strawberry Shirt", "Grape Shirt", "Melon Shirt", "Jingle Shirt", "Blossom Shirt", "Icy Shirt", "Crewel Shirt", "Tropical Shirt", "Ribbon Shirt", "Fall Plaid Shirt", "Fiendish Shirt", "Chevron Shirt", "Ladybug Shirt", "Botanical Shirt", "Anju's Shirt", "Kaffe's Shirt", "Lavender Robe", "Blue Grid Shirt", "Butterfly Shirt", "Blue Tartan", "Gracie's Top", "Orange Tie-Die", "Purple Tie-Die", "Green Tie-Die", "Blue Tie-Die", "Red Tie-Die", "One-Ball Shirt", "Two-Ball Shirt", "Three-Ball Shirt", "Four-Ball Shirt", "Five-Ball Shirt", "Six-Ball Shirt", "Seven-Ball Shirt", "Eight-Ball Shirt", "Nine-Ball Shirt", "Arctic Camo", "Jungle Camo", "Desert Camo", "Rally Shirt", "Racer Shirt", "Racer 6 Shirt", "Fish Bone Shirt", "Spiderweb Shirt", "Zipper Shirt", "Bubble Shirt", "Yellow Bolero", "Nebula Shirt", "Neo-Classic Knit", "Noble Shirt", "Turnip Top", "Oft-Seen Print", "Ski Sweater", "Circus Shirt", "Patchwork Top", "Mod Top", "Hippie Shirt", "Rickrack Shirt", "Diner Uniform", "Shirt Circuit", "U R Here Shirt", "Yodel Shirt", "Pulse Shirt", "Prism Shirt", "Star Shirt", "Straw Shirt", "Noodle Shirt", "Dice Shirt", "Kiddie Shirt", "Frog Shirt", "Moody Blue Shirt", "Cloudy Shirt", "Fortune Shirt", "Skull Shirt", "Desert Shirt", "Aurora Knit", "Winter Sweater", "Go-Go Shirt", "Jade Check Print", "Blue Check Print", "Red Grid Shirt", "Flicker Shirt", "Floral Knit", "Rose Shirt", "Sunset Top", "Chain-Gang Shirt", "Spring Shirt", "Bear Shirt", "MVP Shirt", "Silk Bloom Shirt", "Pop Bloom Shirt", "Loud Bloom Shirt", "Hot Spring Shirt", "New Spring Shirt", "Deep Blue Tee", "Snowcone Shirt", "Ugly Shirt", "Sharp Outfit", "Painter's Smock", "Spade Shirt", "Blossoming Shirt", "Peachy Shirt", "Static Shirt", "Rainbow Shirt", "Groovy Shirt", "Loud Line Shirt", "Dazed Shirt", "Red Bar Shirt", "Blue Stripe Knit", "Earthy Knit", "Spunky Knit", "Deer Shirt", "Blue Check Shirt", "Light Line Shirt", "Blue Pinestripe", "Diamond Shirt", "Lime Line Shirt", "Big Bro's Shirt", "Green Bar Shirt", "Yellow Bar Shirt", "Monkey Shirt", "Polar Fleece", "Ancient Knit", "Fish Knit", "Vertigo Shirt", "Misty Shirt", "Stormy Shirt", "Red Scale Shirt", "Blue Scale Shirt", "Heart Shirt", "Yellow Pinstripe", "Club Shirt", "Li'l Bro's Shirt", "Argyle Knit", "Caveman Tunic", "Café Shirt", "Tiki Shirt", "A Shirt", "Checkered Shirt", "No. 1 Shirt", "No. 2 Shirt", "No. 3 Shirt", "No. 4 Shirt", "No. 5 Shirt", "No. 23 Shirt", "No. 67 Shirt", "BB Shirt", "Beatnik Shirt", "Moldy Shirt", "Houndstooth Tee", "Big Star Shirt", "Orange Pinstripe", "Twinkle Shirt", "Funky Dot Shirt", "Crossing Shirt", "Splendid Shirt", "Jagged Shirt", "Denim Shirt", "Cherry Shirt", "Gumdrop Shirt", "Barber Shirt", "Concierge Shirt", "Fresh Shirt", "Far-Out Shirt", "Dawn Shirt", "Striking Outfit", "Red Check Shirt", "Berry Gingham", "Lemon Gingham", "Dragon Suit", "G Logo Shirt", "Tin Shirt", "Jester Shirt", "Pink Tartan", "Waffle Shirt", "Gray Tartan", "Windsock Shirt", "Trendy Top", "Green Ring Shirt", "White Ring Shirt", "Snappy Print", "Chichi Print", "Wave Print", "Checkerboard Tee", "Subdued Print", "Airy Shirt", "Coral Shirt", "Leather Jerkin", "Zebra Print", "Tiger Print", "Cow Print", "Leopard Print", "Danger Shirt", "Big Dot Shirt", "Puzzling Shirt", "Exotic Shirt", "Houndstooth Knit", "Uncommon Shirt", "Dapper Shirt", "Gaudy Sweater", "Cozy Sweater", "Comfy Sweater", "Classic Top", "Vogue Top", "Laced Shirt", "Natty Shirt", "Citrus Gingham", "Cool Shirt", "Dreamy Shirt", "Flowery Shirt", "Caterpillar Tee", "Shortcake Shirt", "Whirly Shirt", "Thunder Shirt", "Giraffe Print", "Swell Shirt", "Toad Print", "Grass Shirt", "Mosaic Shirt", "Fetching Outfit", "Snow Shirt", "Melon Gingham" };

        static ushort[] Wallpaper_IDs = new ushort[67] { 0x2700, 0x2701, 0x2702, 0x2703, 0x2704, 0x2705, 0x2706, 0x2707, 0x2708, 0x2709, 0x270A, 0x270B, 0x270C, 0x270D, 0x270E, 0x270F, 0x2710, 0x2711, 0x2712, 0x2713, 0x2714, 0x2715, 0x2716, 0x2717, 0x2718, 0x2719, 0x271A, 0x271B, 0x271C, 0x271D, 0x271E, 0x271F, 0x2720, 0x2721, 0x2722, 0x2723, 0x2724, 0x2725, 0x2726, 0x2727, 0x2728, 0x2729, 0x272A, 0x272B, 0x272C, 0x272D, 0x272E, 0x272F, 0x2730, 0x2731, 0x2732, 0x2733, 0x2734, 0x2735, 0x2736, 0x2737, 0x2738, 0x2739, 0x273A, 0x273B, 0x273C, 0x273D, 0x273E, 0x273F, 0x2740, 0x2741, 0x2742 };
        static string[] Wallpaper_Names = new string[67] { "Chic Wall", "Classic Wall", "Parlor Wall", "Stone Wall", "Blue-Trim Wall", "Plaster Wall", "Classroom Wall", "Lovely Wall", "Exotic Wall", "Mortar Wall", "Gold Screen Wall", "Tea Room Wall", "Citrus Wall", "Cabin Wall", "Blue Tarp", "Lunar Horizon", "Garden Wall", "Spooky Wall", "Western Vista", "Green Wall", "Blue Wall", "Regal Wall", "Ranch Wall", "Modern Wall", "Cabana Wall", "Snowman Wall", "Backyard Fence", "Music Room Wall", "Plaza Wall", "Lattice Wall", "Ornate Wall", "Modern Screen", "Bamboo Wall", "Kitchen Wall", "Old Brick Wall", "Stately Wall", "Imperial Wall", "Manor Wall", "Ivy Wall", "Mod Wall", "Rose Wall", "Wood Paneling", "Concrete Wall", "Office Wall", "Ancient Wall", "Exquisite Wall", "Sandlot Wall", "Jingle Wall", "Meadow Vista", "Tree-Lined Wall", "Mosaic Wall", "Arched Window", "Basement Wall", "Backgammon Wall", "Kiddie Wall", "Shanty Wall", "Industrial Wall", "Desert Vista", "Library Wall", "Floral Wall", "Tropical Vista", "Playroom Wall", "Kitschy Wall", "Groovy Wall", "Mushroom Mural", "Ringside Seating", "Harvest Wall"  };

        static ushort[] Carpet_IDs = new ushort[67] { 0x2600, 0x2601, 0x2602, 0x2603, 0x2604, 0x2605, 0x2606, 0x2607, 0x2608, 0x2609, 0x260A, 0x260B, 0x260C, 0x260D, 0x260E, 0x260F, 0x2610, 0x2611, 0x2612, 0x2613, 0x2614, 0x2615, 0x2616, 0x2617, 0x2618, 0x2619, 0x261A, 0x261B, 0x261C, 0x261D, 0x261E, 0x261F, 0x2620, 0x2621, 0x2622, 0x2623, 0x2624, 0x2625, 0x2626, 0x2627, 0x2628, 0x2629, 0x262A, 0x262B, 0x262C, 0x262D, 0x262E, 0x262F, 0x2630, 0x2631, 0x2632, 0x2633, 0x2634, 0x2635, 0x2636, 0x2637, 0x2638, 0x2639, 0x263A, 0x263B, 0x263C, 0x263D, 0x263E, 0x263F, 0x2640, 0x2641, 0x2642 };
        static string[] Carpet_Names = new string[67] { "Plush Carpet", "Classic Carpet", "Checkered Tile", "Old Flooring", "Red Tile", "Birch Flooring", "Classroom Floor", "Lovely Carpet", "Exotic Rug", "Mossy Carpet", "18 Mat Tatami", "8 Mat Tatami", "Citrus Carpet", "Cabin Rug", "Closed Road", "Lunar Surface", "Sand Garden", "Spooky Carpet", "Western Desert", "Green Rug", "Blue Flooring", "Regal Carpet", "Ranch Flooring", "Modern Tile", "Cabana Flooring", "Snowman Carpet", "Backyard Lawn", "Music Room Floor", "Plaza Tile", "Kitchen Tile", "Ornate Rug", "Tatami Floor", "Bamboo Flooring", "Kitchen Flooring", "Charcoal Tile", "Stone Tile", "Imperial Tile", "Opulent Rug", "Slate Flooring", "Ceramic Tile", "Fancy Carpet", "Cowhide Rug", "Steel Flooring", "Office Flooring", "Ancient Tile", "Exquisite Rug", "Sandlot", "Jingle Carpet", "Daisy Meadow", "Sidewalk", "Mosaic Tile", "Parquet Floor", "Basement Floor", "Chessboard Rug", "Kiddie Carpet", "Shanty Mat", "Concrete Floor", "Saharah's Desert", "Tartan Rug", "Palace Tile", "Tropical Floor", "Playroom Rug", "Kitschy Tile", "Diner Tile", "Block Flooring", "Boxing Ring Mat", "Harvest Rug" };

        static ushort[] Item_IDs = new ushort[336]
        {
            0x2503, 0x2504, 0x2505, 0x2506, 0x2507, 0x2508, 0x2509, 0x250A, 0x250B, 0x250C, 0x2103, 0x2100, 0x2101, 0x2102, 0x250E, 0x250F, 0x2510, 0x2511, 0x2512, 0x2514, 0x2515, 0x2516, 0x2517, 0x2518, 0x2519, 0x251A, 0x251B, 0x251C, 0x251E, 0x251F, 0x2520, 0x2521, 0x2522, 0x2523, 0x2524, 0x2525, 0x2526, 0x2527, 0x2528, 0x2529, 0x252A, 0x252B, 0x252C, 0x252D, 0x252E, 0x252F, 0x2530, 0x2B00, 0x2B01, 0x2B02, 0x2B03, 0x2B04, 0x2B05, 0x2B06, 0x2B07, 0x2B08, 0x2B09, 0x2B0A, 0x2B0B, 0x2B0C, 0x2B0D, 0x2B0E, 0x2B0F, 0x2800, 0x2801, 0x2802, 0x2803, 0x2804, 0x2805, 0x2806, 0x2807, 0x2900, 0x2901, 0x2902, 0x2903, 0x2904, 0x2905, 0x2906, 0x2907, 0x2908, 0x2909, 0x290A, 0x2C00, 0x2C01, 0x2C02, 0x2C03, 0x2C04, 0x2C05, 0x2C06, 0x2C07, 0x2C08, 0x2C09, 0x2C0A, 0x2C0B, 0x2C0C, 0x2C0D, 0x2C0E, 0x2C0F, 0x2C10, 0x2C11, 0x2C12, 0x2C13, 0x2C14, 0x2C15, 0x2C16, 0x2C17, 0x2C18, 0x2C19, 0x2C1A, 0x2C1B, 0x2C1C, 0x2C1D, 0x2C1E, 0x2C1F, 0x2C20, 0x2C21, 0x2C22, 0x2C23, 0x2C24, 0x2C25, 0x2C26, 0x2C27, 0x2C28, 0x2C29, 0x2C2A, 0x2C2B, 0x2C2C, 0x2C2D, 0x2C2E, 0x2C2F, 0x2C30, 0x2C31, 0x2C32, 0x2C33, 0x2C34, 0x2C35, 0x2C36, 0x2C37, 0x2C38, 0x2C39, 0x2C3A, 0x2C3B, 0x2C3C, 0x2C3D, 0x2C3E, 0x2C3F, 0x2C40, 0x2C41, 0x2C42, 0x2C43, 0x2C44, 0x2C45, 0x2C46, 0x2C47, 0x2C48, 0x2C49, 0x2C4A, 0x2C4B, 0x2C4C, 0x2C4D, 0x2C4E, 0x2C4F, 0x2C50, 0x2C51, 0x2C52, 0x2C53, 0x2C54, 0x2C55, 0x2C56, 0x2C57, 0x2C58, 0x2C59, 0x2C5A, 0x2C5B, 0x2C5C, 0x2C5D, 0x2C5E, 0x2C5F, 0x2A00, 0x2A01, 0x2A02, 0x2A03, 0x2A04, 0x2A05, 0x2A06, 0x2A07, 0x2A08, 0x2A09, 0x2A0A, 0x2A0B, 0x2A0C, 0x2A0D, 0x2A0E, 0x2A0F, 0x2A10, 0x2A11, 0x2A12, 0x2A13, 0x2A14, 0x2A15, 0x2A16, 0x2A17, 0x2A18, 0x2A19, 0x2A1A, 0x2A1B, 0x2A1C, 0x2A1D, 0x2A1E, 0x2A1F, 0x2A20, 0x2A21, 0x2A22, 0x2A23, 0x2A24, 0x2A25, 0x2A26, 0x2A27, 0x2A28, 0x2A29, 0x2A2A, 0x2A2B, 0x2A2C, 0x2A2D, 0x2A2E, 0x2A2F, 0x2A30, 0x2A31, 0x2A32, 0x2A33, 0x2A34, 0x2A35, 0x2A36, 0x2200, 0x2201, 0x2202, 0x2203, 0x2204, 0x2205, 0x2206, 0x2207, 0x2208, 0x2209, 0x220A, 0x220B, 0x220C, 0x220D, 0x220E, 0x220F, 0x2210, 0x2211, 0x2212, 0x2213, 0x2214, 0x2215, 0x2216, 0x2217, 0x2218, 0x2219, 0x221A, 0x221B, 0x221C, 0x221D, 0x221E, 0x221F, 0x2220, 0x2221, 0x2222, 0x2223, 0x2224, 0x2225, 0x2226, 0x2227, 0x2228, 0x2229, 0x222A, 0x222B, 0x222C, 0x222D, 0x222E, 0x222F, 0x2230, 0x2231, 0x2232, 0x2233, 0x2234, 0x2235, 0x2236, 0x2237, 0x2238, 0x2239, 0x223A, 0x223B, 0x223C, 0x223D, 0x223E, 0x223F, 0x2240, 0x2241, 0x2242, 0x2243, 0x2244, 0x2245, 0x2246, 0x2247, 0x2248, 0x2249, 0x224A, 0x224B, 0x224C, 0x224D, 0x224E, 0x224F, 0x2250, 0x2251, 0x2252, 0x2253, 0x2254, 0x2255, 0x2256, 0x2257, 0x2258, 0x2259, 0x225A, 0x225B, 0x2D28, 0x2D29, 0x2D2A, 0x2D2B, 0x2D2C, 0x2E00, 0x2E01, 0x2F00, 0x2F01, 0x2F02, 0x2F03
        };
        static string[] Item_Names = new string[336]
        {
            "Videotape", "Organizer", "Pokémon Pikachu", "Comic Book", "Picture Book", "Game Boy", "Camera", "Watch", "Handkerchief", "Glasses Case", "100 Bells", "1000 Bells", "10000 Bells", "30000 Bells", "Empty Can", "Boot", "Old Tire", "Fossil", "Pitfall", "Lion's Paw", "Wentletrap", "Venus Comb", "Porceletta", "Sand Dollar", "White Scallop", "Conch", "Coral", "Present (Drop to Open)(Chess Table)", "Signboard (Not Placeable)", "Present (Drop to Open)(Golden Net)", "Present (Drop to Open)(Golden Axe)", "Present (Drop to Open)(Golden Shovel)", "Present (Drop to Open)(Golden Rod)", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Exercise Card", "Knife and Fork", "College Rule", "School Pad", "Organizer", "Diary", "Journal", "Pink Diary", "Captain's Log", "Blue Diary", "French Notebook", "Scroll", "Pink Plaid Pad", "Blue Polka Pad", "Green Plaid Pad", "Red Polka Pad", "Yellow Plaid Pad", "Calligraphy Pad", "Apple", "Cherry Shirt", "Pear", "Peach", "Orange", "Mushroom", "Candy", "Coconut", "Sapling", "Cedar Sapling", "White Pansy Bag", "Purple Pansy Bag", "Yellow Pansy Bag", "White Cosmos Bag", "Pink Cosmos Bag", "Blue Cosmos Bag", "Red Tulip Bag", "White Tulip Bag", "Yellow Tulip Bag", "January Ticket (1)", "January Ticket (2)", "January Ticket (3)", "January Ticket (4)", "January Ticket (5)", "January Ticket (1) (2)", "January Ticket (1) (3)", "January Ticket (1) (4)", "February Ticket (1)", "February Ticket (2)", "February Ticket (3)", "February Ticket (4)", "February Ticket (5)", "February Ticket (1) (2)", "February Ticket (1) (3)", "February Ticket (1) (4)", "March Ticket (1)", "March Ticket (2)", "March Ticket (3)", "March Ticket (4)", "March Ticket (5)", "March Ticket (1) (2)", "March Ticket (1) (3)", "March Ticket (1) (4)", "April Ticket (1)", "April Ticket (2)", "April Ticket (3)", "April Ticket (4)", "April Ticket (5)", "April Ticket (1) (2)", "April Ticket (1) (3)", "April Ticket (1) (4)", "May Ticket (1)", "May Ticket (2)", "May Ticket (3)", "May Ticket (4)", "May Ticket (5)", "May Ticket (1) (2)", "May Ticket (1) (3)", "May Ticket (1) (4)", "June Ticket (1)", "June Ticket (2)", "June Ticket (3)", "June Ticket (4)", "June Ticket (5)", "June Ticket (1) (2)", "June Ticket (1) (3)", "June Ticket (1) (4)", "July Ticket (1)", "July Ticket (2)", "July Ticket (3)", "July Ticket (4)", "July Ticket (5)", "July Ticket (1) (2)", "July Ticket (1) (3)", "July Ticket (1) (4)", "August Ticket (1)", "August Ticket (2)", "August Ticket (3)", "August Ticket (4)", "August Ticket (5)", "August Ticket (1) (2)", "August Ticket (1) (3)", "August Ticket (1) (4)", "September Ticket (1)", "September Ticket (2)", "September Ticket (3)", "September Ticket (4)", "September Ticket (5)", "September Ticket (1) (2)", "September Ticket (1) (3)", "September Ticket (1) (4)", "October Ticket (1)", "October Ticket (2)", "October Ticket (3)", "October Ticket (4)", "October Ticket (5)", "October Ticket (1) (2)", "October Ticket (1) (3)", "October Ticket (1) (4)", "November Ticket (1)", "November Ticket (2)", "November Ticket (3)", "November Ticket (4)", "November Ticket (5)", "November Ticket (1) (2)", "November Ticket (1) (3)", "November Ticket (1) (4)", "December Ticket (1)", "December Ticket (2)", "December Ticket (3)", "December Ticket (4)", "December Ticket (5)", "December Ticket (1) (2)", "December Ticket (1) (3)", "December Ticket (1) (4)", "K.K. Chorale", "K.K. March", "K.K. Waltz", "K.K. Swing", "K.K. Jazz", "K.K. Fusion", "K.K. Etude", "K.K. Lullaby", "K.K. Aria", "K.K. Samba", "K.K. Bossa", "K.K. Calypso", "K.K. Salsa", "K.K. Mambo", "K.K. Reggae", "K.K. Ska", "K.K. Tango", "K.K. Faire", "Aloha K.K.", "Lucky K.K.", "K.K. Condor", "K.K. Steppe", "Imperial K.K.", "K.K. Casbah", "K.K. Safari", "K.K. Folk", "K.K. Rock", "Rockin' K.K.", "K.K. Ragtime", "K.K. Gumbo", "The K. Funk", "K.K. Blues", "Soulful K.K.", "K.K. Soul", "K.K. Cruisin'", "K.K. Love Song", "K.K. D & B", "K.K. Technopop", "DJ K.K.", "Only Me", "K.K. Country", "Surfin' K.K.", "K.K. Ballad", "Comrade K.K.", "K.K. Lament", "Go K.K. Rider!", "K.K. Dirge", "K.K. Western", "Mr. K.K.", "Café K.K.", "K.K. Parade", "Señor K.K.", "K.K. Song", "I Love You", "Two Days Ago", "Net", "Axe", "Shovel", "Fishing Rod", "Gelato Umbrella", "Daffodil Parasol", "Berry Umbrella", "Orange Umbrella", "Mod Umbrella", "Petal Parasol", "Ribbon Parasol", "Gingham Parasol", "Plaid Parasol", "Lacy Parasol", "Elegant Umbrella", "Dainty Parasol", "Classic Umbrella", "Nintendo Parasol", "Bumbershoot", "Sunny Parasol", "Batbrella", "Checked Umbrella", "Yellow Umbrella", "Leaf Umbrella", "Lotus Parasol", "Paper Parasol", "Polka Parasol", "Sharp Umbrella", "Twig Parasol", "Noodle Parasol", "Hypno Parasol", "Pastel Parasol", "Retro Umbrella", "Icy Umbrella", "Blue Umbrella", "Flame Umbrella", "Pattern #1 (Umbrella)", "Pattern #2 (Umbrella)", "Pattern #3 (Umbrella)", "Pattern #4 (Umbrella)", "Pattern #5 (Umbrella)", "Pattern #6 (Umbrella)", "Pattern #7 (Umbrella)", "Pattern #8 (Umbrella)", "Sickle", "Red Paint", "Orange Paint", "Yellow Paint", "Pale Green Paint", "Green Paint", "Sky Blue Paint", "Blue Paint", "Purple Paint", "Pink Paint", "Black Paint", "White Paint", "Brown Paint", "Golden Net", "Golden Axe", "Golden Shovel", "Golden Rod", "Axe (Use #1)", "Axe (Use #2)", "Axe (Use #3)", "Axe (Use #4)", "Axe (Use #5)", "Axe (Use #6)", "Axe (Use #7)", "Red Balloon", "Yellow Balloon", "Blue Balloon", "Green Balloon", "Purple Balloon", "Bunny P. Balloon", "Bunny B. Balloon", "Bunny O. Balloon", "Yellow Pinwheel", "Red Pinwheel", "Tiger Pinwheel", "Green Pinwheel", "Pink Pinwheel", "Striped Pinwheel", "Flower Pinwheel", "Fancy Pinwheel", "Bluebell Fan", "Plum Fan", "Bamboo Fan", "Cloud Fan", "Maple Fan", "Fan Fan", "Flower Fan", "Leaf Fan", "Spirit (1)", "Spirit (2)", "Spirit (3)", "Spirit (4)", "Spirit (5)", "Grab Bag (1)", "Grab Bag (2)", "10 Turnips", "50 Turnips", "100 Turnips", "Spoiled Turnips",
        };

        static ushort[] Fish_IDs = new ushort[40]
        {
            0x2300, 0x2301, 0x2302, 0x2303, 0x2304, 0x2305, 0x2306, 0x2307, 0x2308, 0x2309, 0x230A, 0x230B, 0x230C, 0x230D, 0x230E, 0x230F, 0x2310, 0x2311, 0x2312, 0x2313, 0x2314, 0x2315, 0x2316, 0x2317, 0x2318, 0x2319, 0x231A, 0x231B, 0x231C, 0x231D, 0x231E, 0x231F, 0x2320, 0x2321, 0x2322, 0x2323, 0x2324, 0x2325, 0x2326, 0x2327
        };

        static string[] Fish_Names = new string[40]
        {
            "Crucian Carp", "Brook Trout", "Carp", "Koi", "Catfish", "Small Bass", "Bass", "Large Bass", "Bluegill", "Giant Catfish", "Giant Snakehead", "Barbel Steed", "Dace", "Pale Chub", "Bitterling", "Loach", "Pond Smelt", "Sweetfish", "Cherry Salmon", "Large Char", "Rainbow Trout", "Stringfish", "Salmon", "Goldfish", "Piranha", "Arowana", "Eel", "Freshwater Goby", "Angelfish", "Guppy", "Popeyed Goldfish", "Coelacanth", "Crawfish", "Frog", "Killifish", "Jellyfish", "Sea Bass", "Red Snapper", "Barred Knifejaw", "Arapaima"
        };

        static ushort[] Insect_IDs = new ushort[40]
        {
            0x2D00, 0x2D01, 0x2D02, 0x2D03, 0x2D04, 0x2D05, 0x2D06, 0x2D07, 0x2D08, 0x2D09, 0x2D0A, 0x2D0B, 0x2D0C, 0x2D0D, 0x2D0E, 0x2D0F, 0x2D10, 0x2D11, 0x2D12, 0x2D13, 0x2D14, 0x2D15, 0x2D16, 0x2D17, 0x2D18, 0x2D19, 0x2D1A, 0x2D1B, 0x2D1C, 0x2D1D, 0x2D1E, 0x2D1F, 0x2D20, 0x2D21, 0x2D22, 0x2D23, 0x2D24, 0x2D25, 0x2D26, 0x2D27
        };

        static string[] Insect_Names = new string[40]
        {
            "Common Butterfly", "Yellow Butterfly", "Tiger Butterfly", "Purple Butterfly", "Robust Cicada", "Walker Cicada", "Evening Cicada", "Brown Cicada", "Bee", "Common Dragonfly", "Red Dragonfly", "Darner Dragonfly", "Banded Dragonfly", "Long Locust", "Migratory Locust", "Cricket", "Grasshopper", "Bell Cricket", "Pine Cricket", "Drone Beetle", "Dynastid Beetle", "Flat Stag Beetle", "Jewel Beetle", "Longhorn Beetle", "Ladybug", "Spotted Ladybug", "Mantis", "Firefly", "Cockroach", "Saw Stag Beetle", "Mountain Beetle", "Giant Beetle", "Snail", "Mole Cricket", "Pond Skater", "Bagworm", "Pill Bug", "Spider", "Ant", "Mosquito"
        };

        static ushort[] Gyroid_IDs = new ushort[127]
        {
            0x15B0, 0x15B4, 0x15B8, 0x15BC, 0x15C0, 0x15C4, 0x15C8, 0x15CC, 0x15D0, 0x15D4, 0x15D8, 0x15DC, 0x15E0, 0x15E4, 0x15E8,
            0x15EC, 0x15F0, 0x15F4, 0x15F8, 0x15FC, 0x1600, 0x1604, 0x1608, 0x160C, 0x1610, 0x1614, 0x1618, 0x161C, 0x1620, 0x1624,
            0x1628, 0x162C, 0x1630, 0x1634, 0x1638, 0x163C, 0x1640, 0x1644, 0x1648, 0x164C, 0x1650, 0x1654, 0x1658, 0x165C, 0x1660,
            0x1664, 0x1668, 0x166C, 0x1670, 0x1674, 0x1678, 0x167C, 0x1680, 0x1684, 0x1688, 0x168C, 0x1690, 0x1694, 0x1698, 0x169C,
            0x16A0, 0x16A4, 0x16A8, 0x16AC, 0x16B0, 0x16B4, 0x16B8, 0x16BC, 0x16C0, 0x16C4, 0x16C8, 0x16CC, 0x16D0, 0x16D4, 0x16D8,
            0x16DC, 0x16E0, 0x16E4, 0x16E8, 0x16EC, 0x16F0, 0x16F4, 0x16F8, 0x16FC, 0x1700, 0x1704, 0x1708, 0x170C, 0x1710, 0x1714,
            0x1718, 0x171C, 0x1720, 0x1724, 0x1728, 0x172C, 0x1730, 0x1734, 0x1738, 0x173C, 0x1740, 0x1744, 0x1748, 0x174C, 0x1750,
            0x1754, 0x1758, 0x175C, 0x1760, 0x1764, 0x1768, 0x176C, 0x1770, 0x1774, 0x1778, 0x177C, 0x1780, 0x1784, 0x1788, 0x178C,
            0x1790, 0x1794, 0x1798, 0x179C, 0x17A0, 0x17A4, 0x17A8
        };

        static string[] Gyroid_Names = new string[127]
        {
            "Tall Gongoid", "Mega Gongoid", "Mini Gongoid", "Gongoid", "Mini Oombloid", "Oombloid", "Mega Oombloid", "Tall Oombloid", "Mega Echoid", "Mini Echoid", "Tall Echoid", "Mini Sputnoid", "Sputnoid", "Mega Sputnoid", "Tall Sputnoid",
            "Mini Dinkoid", "Mini Fizzoid", "Mega Fizzoid", "Mega Dinkoid", "Mini Gargloid", "Gargloid", "Tall Gargloid", "Mega Buzzoid", "Tall Buzzoid", "Buzzoid", "Mini Buzzoid", "Sproid", "Mini Sproid", "Mega Sproid", "Tall Sproid",
            "Tootoid", "Mini Tootoid", "Mega Tootoid", "Tall Droploid", "Mega Bovoid", "Tall Bovoid", "Mini Metatoid", "Metatoid", "Mini Bowtoid", "Bowtoid", "Mega Bowtoid", "Tall Bowtoid", "Mega Lamentoid", "Tall Lamentoid", "Lamentoid",
            "Mini Lamentoid", "Mini Timpanoid", "Timpanoid", "Mega Timpanoid", "Quazoid", "Mega Quazoid", "Mega Dekkoid", "Dekkoid", "Mini Dekkoid", "Mega Alloid", "Tall Alloid", "Mini Alloid", "Mini Freakoid", "Mega Feakoid", "Tall Quazoid",
            "Mini Quazoid", "Squat Dingloid", "Mega Dingloid", "Tall Dingloid", "Dingloid", "Mini Dingloid", "Wee Dingloid", "Mega Clankoid", "Clankoid", "Tall Clankoid", "Mini Clankoid", "Croakoid", "Mega Croakoid", "Tall Croakoid", "Mini Croakoid",
            "Mega Poltergoid", "Tall Poltergoid", "Poltergoid", "Mini Poltergoid", "Tall Warbloid", "Warbloid", "Mini Warbloid", "Mega Rustoid", "Rustoid", "Mini Rustoid", "Mega Percoloid", "Tall Percoloid", "Mega Puffoid", "Mini Puffoid", "Tall Puffoid",
            "Rhythmoid", "Mini Rythmoid", "Slim Quazoid", "Mega Oboid", "Oboid", "Tall Oboid", "Tall Timpanoid", "Mini Howloid", "Howloid", "Mega Howloid", "Mega Harmonoid", "Harmonoid", "Tall Harmonoid", "Mini Harmonoid", "Tall Strumboid",
            "Mega Strumboid", "Strumboid", "Mini Strumboid", "Mega Lullaboid", "Tall Lullaboid", "Lullaboid", "Mini Lullaboid", "Mega Drilloid", "Drilloid", "Mini Drilloid", "Mega Nebuloid", "Nebuloid", "Squat Nebuloid", "Tall Nebuloid", "Mini Nebuloid",
            "Slim Nebuloid", "Mega Plinkoid", "Plinkoid", "Mini Plinkoid", "Squelchoid", "Mega Squelchoid", "Mini Squelchoid"
        };

        static ushort[] Stationery_IDs = new ushort[256]
        {
            0x2000, 0x2001, 0x2002, 0x2003, 0x2004, 0x2005, 0x2006, 0x2007, 0x2008, 0x2009, 0x200A, 0x200B, 0x200C, 0x200D, 0x200E, 0x200F, 0x2010, 0x2011, 0x2012, 0x2013, 0x2014, 0x2015, 0x2016, 0x2017, 0x2018, 0x2019, 0x201A, 0x201B, 0x201C, 0x201D, 0x201E, 0x201F, 0x2020, 0x2021, 0x2022, 0x2023, 0x2024, 0x2025, 0x2026, 0x2027, 0x2028, 0x2029, 0x202A, 0x202B, 0x202C, 0x202D, 0x202E, 0x202F, 0x2030, 0x2031, 0x2032, 0x2033, 0x2034, 0x2035, 0x2036, 0x2037, 0x2038, 0x2039, 0x203A, 0x203B, 0x203C, 0x203D, 0x203E, 0x203F, 0x2040, 0x2041, 0x2042, 0x2043, 0x2044, 0x2045, 0x2046, 0x2047, 0x2048, 0x2049, 0x204A, 0x204B, 0x204C, 0x204D, 0x204E, 0x204F, 0x2050, 0x2051, 0x2052, 0x2053, 0x2054, 0x2055, 0x2056, 0x2057, 0x2058, 0x2059, 0x205A, 0x205B, 0x205C, 0x205D, 0x205E, 0x205F, 0x2060, 0x2061, 0x2062, 0x2063, 0x2064, 0x2065, 0x2066, 0x2067, 0x2068, 0x2069, 0x206A, 0x206B, 0x206C, 0x206D, 0x206E, 0x206F, 0x2070, 0x2071, 0x2072, 0x2073, 0x2074, 0x2075, 0x2076, 0x2077, 0x2078, 0x2079, 0x207A, 0x207B, 0x207C, 0x207D, 0x207E, 0x207F, 0x2080, 0x2081, 0x2082, 0x2083, 0x2084, 0x2085, 0x2086, 0x2087, 0x2088, 0x2089, 0x208A, 0x208B, 0x208C, 0x208D, 0x208E, 0x208F, 0x2090, 0x2091, 0x2092, 0x2093, 0x2094, 0x2095, 0x2096, 0x2097, 0x2098, 0x2099, 0x209A, 0x209B, 0x209C, 0x209D, 0x209E, 0x209F, 0x20A0, 0x20A1, 0x20A2, 0x20A3, 0x20A4, 0x20A5, 0x20A6, 0x20A7, 0x20A8, 0x20A9, 0x20AA, 0x20AB, 0x20AC, 0x20AD, 0x20AE, 0x20AF, 0x20B0, 0x20B1, 0x20B2, 0x20B3, 0x20B4, 0x20B5, 0x20B6, 0x20B7, 0x20B8, 0x20B9, 0x20BA, 0x20BB, 0x20BC, 0x20BD, 0x20BE, 0x20BF, 0x20C0, 0x20C1, 0x20C2, 0x20C3, 0x20C4, 0x20C5, 0x20C6, 0x20C7, 0x20C8, 0x20C9, 0x20CA, 0x20CB, 0x20CC, 0x20CD, 0x20CE, 0x20CF, 0x20D0, 0x20D1, 0x20D2, 0x20D3, 0x20D4, 0x20D5, 0x20D6, 0x20D7, 0x20D8, 0x20D9, 0x20DA, 0x20DB, 0x20DC, 0x20DD, 0x20DE, 0x20DF, 0x20E0, 0x20E1, 0x20E2, 0x20E3, 0x20E4, 0x20E5, 0x20E6, 0x20E7, 0x20E8, 0x20E9, 0x20EA, 0x20EB, 0x20EC, 0x20ED, 0x20EE, 0x20EF, 0x20F0, 0x20F1, 0x20F2, 0x20F3, 0x20F4, 0x20F5, 0x20F6, 0x20F7, 0x20F8, 0x20F9, 0x20FA, 0x20FB, 0x20FC, 0x20FD, 0x20FE, 0x20FF
        };

        static string[] Stationery_Names = new string[256]
        {
            "Airmail Paper (1)", "Sparkly Paper (1)", "Bamboo Paper (1)", "Orange Paper (1)", "Essay Paper (1)", "Panda Paper (1)", "Ranch Paper (1)", "Steel Paper (1)", "Blossom Paper (1)", "Vine Paper (1)", "Cloudy Paper (1)", "Petal Paper (1)", "Snowy Paper (1)", "Rainy Day Paper (1)", "Watermelon Paper (1)", "Deep Sea Paper (1)", "Starry Sky Paper (1)", "Daisy Paper (1)", "Bluebell Paper (1)", "Maple Leaf Paper (1)", "Woodcut Paper (1)", "Octopus Paper (1)", "Festive Paper (1)", "Skyline Paper (1)", "Museum Paper (1)", "Fortune Paper (1)", "Stageshow Paper (1)", "Thick Paper (1)", "Spooky Paper (1)", "Noodle Paper (1)", "Neat Paper (1)", "Horsetail Paper (1)", "Felt Paper (1)", "Parchment (1)", "Cool Paper (1)", "Elegant Paper (1)", "Lacy Paper (1)", "Polka-Dot Paper (1)", "Dizzy Paper (1)", "Rainbow Paper (1)", "Hot Neon Paper (1)", "Cool Neon Paper (1)", "Aloha Paper (1)", "Ribbon Paper (1)", "Fantasy Paper (1)", "Woodland Paper (1)", "Gingko Paper (1)", "Fireworks Paper (1)", "Winter Paper (1)", "Gyroid Paper (1)", "Ivy Paper (1)", "Wing Paper (1)", "Dragon Paper (1)", "Tile Paper (1)", "Misty Paper (1)", "Simple Paper (1)", "Honeybee Paper (1)", "Mystic Paper (1)", "Sunset Paper (1)", "Lattice Paper (1)", "Dainty Paper (1)", "Butterfly Paper (1)", "New Year's Card (1)", "Inky Paper (1)", "Airmail Paper (2)", "Sparkly Paper (2)", "Bamboo Paper (2)", "Orange Paper (2)", "Essay Paper (2)", "Panda Paper (2)", "Ranch Paper (2)", "Steel Paper (2)", "Blossom Paper (2)", "Vine Paper (2)", "Cloudy Paper (2)", "Petal Paper (2)", "Snowy Paper (2)", "Rainy Day Paper (2)", "Watermelon Paper (2)", "Deep Sea Paper (2)", "Starry Sky Paper (2)", "Daisy Paper (2)", "Bluebell Paper (2)", "Maple Leaf Paper (2)", "Woodcut Paper (2)", "Octopus Paper (2)", "Festive Paper (2)", "Skyline Paper (2)", "Museum Paper (2)", "Fortune Paper (2)", "Stageshow Paper (2)", "Thick Paper (2)", "Spooky Paper (2)", "Noodle Paper (2)", "Neat Paper (2)", "Horsetail Paper (2)", "Felt Paper (2)", "Parchment (2)", "Cool Paper (2)", "Elegant Paper (2)", "Lacy Paper (2)", "Polka-Dot Paper (2)", "Dizzy Paper (2)", "Rainbow Paper (2)", "Hot Neon Paper (2)", "Cool Neon Paper (2)", "Aloha Paper (2)", "Ribbon Paper (2)", "Fantasy Paper (2)", "Woodland Paper (2)", "Gingko Paper (2)", "Fireworks Paper (2)", "Winter Paper (2)", "Gyroid Paper (2)", "Ivy Paper (2)", "Wing Paper (2)", "Dragon Paper (2)", "Tile Paper (2)", "Misty Paper (2)", "Simple Paper (2)", "Honeybee Paper (2)", "Mystic Paper (2)", "Sunset Paper (2)", "Lattice Paper (2)", "Dainty Paper (2)", "Butterfly Paper (2)", "New Year's Card (2)", "Inky Paper (2)", "Airmail Paper (3)", "Sparkly Paper (3)", "Bamboo Paper (3)", "Orange Paper (3)", "Essay Paper (3)", "Panda Paper (3)", "Ranch Paper (3)", "Steel Paper (3)", "Blossom Paper (3)", "Vine Paper (3)", "Cloudy Paper (3)", "Petal Paper (3)", "Snowy Paper (3)", "Rainy Day Paper (3)", "Watermelon Paper (3)", "Deep Sea Paper (3)", "Starry Sky Paper (3)", "Daisy Paper (3)", "Bluebell Paper (3)", "Maple Leaf Paper (3)", "Woodcut Paper (3)", "Octopus Paper (3)", "Festive Paper (3)", "Skyline Paper (3)", "Museum Paper (3)", "Fortune Paper (3)", "Stageshow Paper (3)", "Thick Paper (3)", "Spooky Paper (3)", "Noodle Paper (3)", "Neat Paper (3)", "Horsetail Paper (3)", "Felt Paper (3)", "Parchment (3)", "Cool Paper (3)", "Elegant Paper (3)", "Lacy Paper (3)", "Polka-Dot Paper (3)", "Dizzy Paper (3)", "Rainbow Paper (3)", "Hot Neon Paper (3)", "Cool Neon Paper (3)", "Aloha Paper (3)", "Ribbon Paper (3)", "Fantasy Paper (3)", "Woodland Paper (3)", "Gingko Paper (3)", "Fireworks Paper (3)", "Winter Paper (3)", "Gyroid Paper (3)", "Ivy Paper (3)", "Wing Paper (3)", "Dragon Paper (3)", "Tile Paper (3)", "Misty Paper (3)", "Simple Paper (3)", "Honeybee Paper (3)", "Mystic Paper (3)", "Sunset Paper (3)", "Lattice Paper (3)", "Dainty Paper (3)", "Butterfly Paper (3)", "New Year's Card (3)", "Inky Paper (3)", "Airmail Paper (4)", "Sparkly Paper (4)", "Bamboo Paper (4)", "Orange Paper (4)", "Essay Paper (4)", "Panda Paper (4)", "Ranch Paper (4)", "Steel Paper (4)", "Blossom Paper (4)", "Vine Paper (4)", "Cloudy Paper (4)", "Petal Paper (4)", "Snowy Paper (4)", "Rainy Day Paper (4)", "Watermelon Paper (4)", "Deep Sea Paper (4)", "Starry Sky Paper (4)", "Daisy Paper (4)", "Bluebell Paper (4)", "Maple Leaf Paper (4)", "Woodcut Paper (4)", "Octopus Paper (4)", "Festive Paper (4)", "Skyline Paper (4)", "Museum Paper (4)", "Fortune Paper (4)", "Stageshow Paper (4)", "Thick Paper (4)", "Spooky Paper (4)", "Noodle Paper (4)", "Neat Paper (4)", "Horsetail Paper (4)", "Felt Paper (4)", "Parchment (4)", "Cool Paper (4)", "Elegant Paper (4)", "Lacy Paper (4)", "Polka-Dot Paper (4)", "Dizzy Paper (4)", "Rainbow Paper (4)", "Hot Neon Paper (4)", "Cool Neon Paper (4)", "Aloha Paper (4)", "Ribbon Paper (4)", "Fantasy Paper (4)", "Woodland Paper (4)", "Gingko Paper (4)", "Fireworks Paper (4)", "Winter Paper (4)", "Gyroid Paper (4)", "Ivy Paper (4)", "Wing Paper (4)", "Dragon Paper (4)", "Tile Paper (4)", "Misty Paper (4)", "Simple Paper (4)", "Honeybee Paper (4)", "Mystic Paper (4)", "Sunset Paper (4)", "Lattice Paper (4)", "Dainty Paper (4)", "Butterfly Paper (4)", "New Year's Card (4)", "Inky Paper (4)"
        };

        public static List<KeyValuePair<ushort, string>> ItemDatabase = new List<KeyValuePair<ushort, string>>();

        public static void SetupItemDictionary()
        {
            for (int i = 0; i < Furniture_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Furniture_IDs[i], Furniture_Names[i]));

            for (int i = 0; i < Shirt_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Shirt_IDs[i], Shirt_Names[i]));

            for (int i = 0; i < Gyroid_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Gyroid_IDs[i], Gyroid_Names[i]));

            for (int i = 0; i < Carpet_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Carpet_IDs[i], Carpet_Names[i]));

            for (int i = 0; i < Wallpaper_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Wallpaper_IDs[i], Wallpaper_Names[i]));

            for (int i = 0; i < Item_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Item_IDs[i], Item_Names[i]));

            for (int i = 0; i < acreItemIDs.Count; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(acreItemIDs[i], acreItemNames[i]));

            for (int i = 0; i < Fish_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Fish_IDs[i], Fish_Names[i]));

            for (int i = 0; i < Insect_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Insect_IDs[i], Insect_Names[i]));

            for (int i = 0; i < Stationery_IDs.Length; i++)
                ItemDatabase.Add(new KeyValuePair<ushort, string>(Stationery_IDs[i], Stationery_Names[i]));

            ItemDatabase.Add(new KeyValuePair<ushort, string>(0, "Empty")); //Empty Case
            ItemDatabase.Sort((x, y) => x.Key.CompareTo(y.Key));
        }

        public static string getItemType(ushort ID)
        {
            if (ID == 0) return "empty";
            if (ID == 0xFFFF) return "occupied";
            if (ID >= 0x8 && ID <= 0xA) return "weed";
            if (ID >= 0x845 && ID <= 0x84D) return "flower";
            if (ID >= 0x2100 && ID <= 0x2103) return "money";
            if (ID >= 0x63 && ID <= 0x67) return "rock";
            if (ID >= 0x2514 && ID <= 0x251B) return "shell";
            if (ID >= 0x2A00 && ID <= 0x2A36) return "song";
            if (ID >= 0x2000 && ID <= 0x20FF) return "paper";
            if (ID >= 0x2F00 && ID <= 0x2F03) return "turnip";
            if ((ID >= 0x2300 && ID <= 0x2327) || (ID >= 0x2D00 && ID <= 0x2D27)) return "catchable";
            if ((ID >= 0x2600 && ID <= 0x2642) || (ID >= 0x2700 && ID == 0x2742)) return "wallfloor";
            if (ID >= 0x2400 && ID <= 0x24FE) return "clothes";
            if (ID >= 0x15B0 && ID <= 0x17A8) return "gyroids";
            if (ID == 0x2511) return "fossil";
            if (ID >= 0x2200 && ID <= 0x225B) return "tool";
            if ((ID >= 0x005E && ID <= 0x0060) || (ID >= 0x0069 && ID <= 0x0082) || (ID >= 0x0800 && ID <= 0x0868)) return "tree";
            if ((ID >= 0x5 && ID <= 0x7) || (ID >= 0xB && ID <= 0x10) || (ID >= 0x5000 && ID <= 0xB000) || (ID == 0xFE1D || ID == 0xFE1E)) return "building";
            if (ID != 0xFFFF && ID != 0x0000) return "furniture";

            return "unknown";
        }
        public static uint getItemColor(string itemType)
        {
            switch (itemType)
            {
                case "furniture": return 0xc83cde30;
                case "flower": return 0xc8ec67b8;
                case "money": return 0xc8ffff00;
                case "rock": return 0xc8000000;
                case "song": return 0xc8a4ecb8;
                case "paper": return 0xc8a4ece8;
                case "turnip": return 0xc8bbac9d;
                case "catchable": return 0xc8bae33e;
                case "wallfloor": return 0xc8994040;
                case "clothes": return 0xc82874aa;
                case "gyroids": return 0xc8d48324;
                case "fossil": return 0xc8868686;
                case "tool": return 0xc8818181;
                case "tree": return 0xc800ff00;
                case "weed": return 0xc8008000;
                case "shell": return 0xc8FFC0CB;
                case "empty": return 0x00ffffff;
                case "occupied": return 0xDD999999;
                case "building": return 0xFF777777;
            }
            return 0xc8ff0000;
        }

        public static void AddVillagerHouses()
        {
            foreach (KeyValuePair<ushort, string> villager in VillagerData.Villagers)
                if (villager.Key >= 0xE000 && villager.Key <= 0xE0EB)
                {
                    ushort houseId = BitConverter.ToUInt16(new byte[2] { (byte)(villager.Key & 0xFF), 0x50 }, 0);
                    acreItemIDs.Add(houseId);
                    acreItemNames.Add(villager.Value + "'s House");
                }
        }

        public static ushort GetItemID(string itemName)
        {
            for(int i = 0; i < Item_Names.Length; i++)
            {
                if (Item_Names[i] == itemName)
                    return Item_IDs[i];
            }
            return 0;
        }

        public static string GetItemName(ushort itemID)
        {
            if (itemID == 0xFFFF)
                return "Occupied/Unavailable";
            int itemType = 1;
            ushort baseItemID = itemID;

            if (baseItemID >= 0x15B0 && baseItemID <= 0x17A8)
                itemType = 2;
            else if ((baseItemID >= 0x2100 && baseItemID <= 0x2103) || (baseItemID >= 0x2200 && baseItemID <= 0x225B) || (baseItemID >= 0x2503 && baseItemID <= 0x2530) || (baseItemID >= 0x2800 && baseItemID <= 0x2807) || (baseItemID >= 0x2900 && baseItemID <= 0x290A) || (baseItemID >= 0x2A00 && baseItemID <= 0x2A36) || (baseItemID >= 0x2B00 && baseItemID <= 0x2B0F) || (baseItemID >= 0x2C00 && baseItemID <= 0x2C5F) || (baseItemID >= 0x2D28 && baseItemID <= 0x2D2C) || (baseItemID >= 0x2E00 && baseItemID <= 0x2E01) || (baseItemID >= 0x2F00 && baseItemID <= 0x2F03))
                itemType = 3;
            else if ((baseItemID > 0 && baseItemID < 0x1000) || (baseItemID >= 0x5000 && baseItemID <= 0xB000))
                itemType = 4;
            else if (baseItemID >= 0x2600 && baseItemID <= 0x2642)
                itemType = 5;
            else if (baseItemID >= 0x2700 && baseItemID <= 0x2742)
                itemType = 6;
            else if (baseItemID >= 0x2400 && baseItemID <= 0x24FE)
                itemType = 7;
            else if (baseItemID >= 0x2300 && baseItemID <= 0x2327)
                itemType = 8;
            else if (baseItemID >= 0x2D00 && baseItemID <= 0x2D27)
                itemType = 9;
            else if (baseItemID >= 0x2000 && baseItemID <= 0x20FF)
                itemType = 10;

            if (itemType == 1 || itemType == 2)
                baseItemID = (ushort)(itemID - (itemID % 4)); //Account for furniture rotational variants
            string itemName = "";
            switch (itemType)
            {
                case 1:
                    itemName = Furniture_IDs.Contains(baseItemID) ? Furniture_Names[Array.IndexOf(Furniture_IDs, baseItemID)] : itemName;
                    break;
                case 2:
                    itemName = Gyroid_IDs.Contains(baseItemID) ? Gyroid_Names[Array.IndexOf(Gyroid_IDs, baseItemID)] : itemName;
                    break;
                case 3:
                    itemName = Item_IDs.Contains(baseItemID) ? Item_Names[Array.IndexOf(Item_IDs, baseItemID)] : itemName;
                    break;
                case 4:
                    itemName = acreItemIDs.Contains(baseItemID) ? acreItemNames[acreItemIDs.IndexOf(baseItemID)] : itemName;
                    break;
                case 5:
                    itemName = Carpet_IDs.Contains(baseItemID) ? Carpet_Names[Array.IndexOf(Carpet_IDs, baseItemID)] : itemName;
                    break;
                case 6:
                    itemName = Wallpaper_IDs.Contains(baseItemID) ? Wallpaper_Names[Array.IndexOf(Wallpaper_IDs, baseItemID)] : itemName;
                    break;
                case 7:
                    itemName = Shirt_IDs.Contains(baseItemID) ? Shirt_Names[Array.IndexOf(Shirt_IDs, baseItemID)] : itemName;
                    break;
                case 8:
                    itemName = Fish_IDs.Contains(baseItemID) ? Fish_Names[Array.IndexOf(Fish_IDs, baseItemID)] : itemName;
                    break;
                case 9:
                    itemName = Insect_IDs.Contains(baseItemID) ? Insect_Names[Array.IndexOf(Insect_IDs, baseItemID)] : itemName;
                    break;
                case 10:
                    itemName = Stationery_IDs.Contains(baseItemID) ? Stationery_Names[Array.IndexOf(Stationery_IDs, baseItemID)] : itemName;
                    break;
                default:
                    itemName = Furniture_IDs.Contains(baseItemID) ? Furniture_Names[Array.IndexOf(Furniture_IDs, baseItemID)] : itemName;
                    break;
            }
            return itemName;
        }
    }

    public class Item
    {
        public ushort ItemID = 0;
        public string Name = "";

        public Item(ushort itemId)
        {
            ItemID = itemId;
            Name = ItemID == 0 ? "Empty" : ItemData.GetItemName(ItemID);
        }
    }

    public class InventoryItem : Item
    {
        int Index = 0;

        public InventoryItem(ushort itemId, int index) : base(itemId)
        {
            Index = index;
        }
    }

    public class WorldItem : Item
    {
        public Point Location;
        public int Index = 0;
        public bool Burried = false;

        public WorldItem(ushort itemId, int position) : base(itemId)
        {
            Location = new Point(position % 16, position / 16);
            Index = position;
        }
    }

    public class Furniture : Item
    {
        public ushort BaseItemID = 0;
        public int Rotation = 0;

        public Furniture(ushort itemId) : base(itemId)
        {
            BaseItemID = (ushort)(ItemID - (ItemID % 4));
            if (ItemData.Furniture_IDs.Contains(BaseItemID))
            {
                Rotation = (ItemID % 4) * 90;
            }
        }

        public void SetRotation(int degrees)
        {
            if (degrees % 90 == 0)
            {
                Rotation = degrees;
                ItemID = (ushort)(BaseItemID + (degrees / 90));
            }
        }

        public bool IsFurniture()
        {
            return ItemData.Furniture_IDs.Contains(BaseItemID);
        }
    }

    public class Building : WorldItem
    {
        public Building(ushort itemId, int position) : base(itemId, position)
        {

        }
    }
}

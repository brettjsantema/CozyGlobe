using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public static class CozyGlobeUtils
    {
        public enum VillagerType { Elf, Craftsman, Snowman, Baker, GingerbreadMan, Iceman };
        public enum BuildingType { Shed, Igloo, PretzelStand, CookieStand, GingerbreadHouse, Workshop };
        public enum DecorationType { DeadTree, ElmTree, BlueFlowers, RedFlowers };

        public static string[] elfPrefixes = { "Jingle", "Sugarplum", "Twinkle", "Sparkle", "Candy", "Frosty", "Buddy", "Merry", "Jolly", "Peppermint", "Ginger", "Mistletoe", "Holly", "Snowy", "Joyful", "Cocoa", "Bells", "Cookie", "Angel", "Stocking", "Winter", "Festive", "Cheery", "Eggnog" };
        public static string[] elfFirstNames = { "Jolly", "Mistletoe", "Snowflake", "Cookie", "Ginger", "Bells", "Cocoa", "Holly", "Angel", "Stocking", "Frosty", "Twinkle", "Candy", "Jingle", "Joy", "Buddy", "Merry", "Peppermint", "Cheer", "Faith", "Hope", "Grace", "Carol", "Noel", "Star" };
        public static string[] elfLastNames = { "Winkletree", "Tinselberry", "Merrywood", "Candycane", "Snowberry", "Gingersnap", "Baubleton", "Mittensmith", "Elftree", "Hollybottom", "Frostwhisper", "Glowbell", "Snowspark", "Sugarplum",  "Jinglebell", "Bellweather", "Cocoaheart", "Winterglow", "Evergreen", "Candlebright" };

        public static string[] craftsmanPrefixes = { "Crafty", "Gift", "Joyful", "Festive", "Merry", "Cheery", "Whittle", "Toy", "Elfin", "Jolly", "Wreath", "Ornament", "Baubleton", "Bell", "Mittens", "Stocking", "Holly", "Bells", "Sugarplum", "Candle", "Snowflake", "Ginger", "Cocoa" };
        public static string[] craftsmanFirstNames = { "Wooden", "Jingle", "Ornament", "Wreath", "Snowflake", "Gingerbread","Holly", "Elfin", "Candle", "Baubleton", "Mittens", "Stocking", "Bell", "Cheer", "Festive", "Crafty", "Joyful", "Frosty", "Twinkle", "Merry", "Buddy", "Cookie" };
        public static string[] craftsmanLastNames = { "Whittlebottom", "Carpenterbell", "Toyhammer", "Elfinwork", "Joyfulmaker", "Festivecraft", "Wreathcarver", "Ornamentforge", "Cheerybuilder", "Mittenscarver", "Stockingbuilder", "Hollycarver", "Bellsforge", "Candlecarver", "Gingerforge", "Cocoasmith", "Joyfulcarver", "Frostyforge", "Twinklecraft"};

        public static string[] snowmanPrefixes = { "Frosty", "Snowy", "Chilly", "Ice", "Glacial", "Crystal", "Winter", "Blizzard", "Arctic", "Icicle", "Frostbite", "Glowing", "Shimmering", "Icy", "Polar", "Snowflake", "Frozen", "Snowbound", "Snowfall", "Wintermint", "Glittering", "Frigid", "Boreal", "Shiver" };
        public static string[] snowmanFirstNames = { "Flurry", "Snowstorm", "Chill", "Icy", "Blizzard", "Frostbite", "Shiver", "Frosty", "Crystal", "Polar", "Glow", "Snowy", "Glacier", "Arctic", "Winter", "Icicle", "Frozen", "Glitter", "Snowfall", "Shimmer", "Boreal", "Snowflake", "Frigid", "Wintermint", "Glistening" };
        public static string[] snowmanLastNames = { "Snowheart", "Icemantle", "Frostwhisper", "Chillmaker", "Crystalshade", "Snowbound", "Borealwind", "Polarbreeze", "Glacialwhisper", "Wintercrest", "Blizzardwalker", "Iciclefrost", "Snowdrift", "Glitterfrost", "Shiverglisten", "Frigidheart", "Snowyshadow", "Frozenwhisper", "Chillspark", "Arcticbreeze", "Glowingheart", "Snowfallshadow", "Frostbiteglisten", "Shimmeringwhisper", "Wintermintbreeze" };

        public static string[] bakerPrefixes = { "Sugar", "Ginger", "Cocoa", "Butter", "Frost", "Spice", "Cookie", "Holly", "Peppermint", "Festive", "Snickerdoodle", "Candy", "Snowflake", "Vanilla", "Mint", "Almond", "Choco", "Nutmeg", "Holiday", "Merry", "Crispy", "Golden", "Warm", "Sweet", "Jolly" };
        public static string[] bakerFirstNames = { "Snickerdoodle", "Sugarplum", "Ginger", "Cocoa", "Butter", "Frost", "Spice", "Cookie", "Holly", "Peppermint", "Festive", "Candy", "Snowflake", "Vanilla", "Mint", "Almond", "Choco", "Nutmeg", "Holiday", "Merry", "Crispy", "Golden", "Warm", "Sweet", "Jolly" };
        public static string[] bakerLastNames = { "Dougherty", "Bakerson", "Mixerstein", "Frostington", "Sprinkleton", "Ovenheimer", "Vanillington", "Sugarplumbottom", "Crispersmith", "Mintington", "Nutmegson", "Almondberry", "Chocobottom", "Candyman", "Warmington", "Goldenberg", "Delightson", "Sweetsworth", "Gingersmith", "Cocoason", "Butterman", "Holidayton", "Jollybottom", "Joyworth", "Treatson" };

        public static string[] gingerbreadPrefixes = { "Ginger", "Cinnamon", "Spicy", "Molasses", "Sweet", "Frosty", "Crispy", "Merry", "Doughy", "Candy", "Sugary", "Warm", "Happy", "Golden", "Smiling", "Snappy", "Cookie", "Cheery", "Spiced", "Festive", "Jolly", "Baked", "Joyful", "Cocoa", "Yummy" };
        public static string[] gingerbreadFirstNames = { "Ginger", "Crispy", "Merry", "Doughy", "Candy", "Sugary", "Warm", "Happy", "Golden", "Smiling", "Snappy", "Cookie", "Cheery", "Spiced", "Festive", "Jolly", "Baked", "Joyful", "Cocoa", "Yummy", "Cinnamon", "Spicy", "Molasses", "Sweet", "Frosty" };
        public static string[] gingerbreadLastNames = { "Bread", "Man", "Treat", "Joy", "Smile", "Delight", "Snack", "Crisp", "Munch", "Crunch", "Sweetie", "Cocoa", "Merry", "Spice", "Yum", "Baker", "Dough", "Chip", "Cookie", "Joyful", "Sugar", "Happy", "Ginger", "Festive", "Cheery" };

        public static string[] jackFrostPrefixes = { "Frost", "Ice", "Snow", "Crystal", "Winter", "Glacial", "Frozen", "Chill", "Arctic", "Icy", "Boreal", "Blizzard", "Polar", "Shiver", "Frigid", "Glistening", "Shimmering", "Chilled", "Icicle", "Cold", "Frostbite", "Snowflake", "Glittering", "Hoarfrost", "Numbing" };
        public static string[] jackFrostFirstNames = { "Jack", "Frost", "Ice", "Snow", "Crystal", "Winter", "Glacial", "Frozen", "Chill", "Arctic", "Icy", "Boreal", "Blizzard", "Polar", "Shiver", "Frigid", "Glistening", "Shimmering", "Chilled", "Icicle", "Cold", "Frostbite", "Snowflake", "Glittering", "Hoarfrost" };
        public static string[] jackFrostLastNames = { "Winterbourne", "Iceheart", "Snowfield", "Crystalshade", "Frostglow", "Glacialheart", "Frozenwater", "Chillbreeze", "Arcticstorm", "Icyveil", "Borealwind", "Blizzardwalker", "Polarfrost", "Shivermist", "Frigidwhisper", "Glistenfrost", "Shimmerglaze", "Chilledrift", "Iciclefrost", "Coldshiver", "Frostbiteshadow", "Snowflakewind", "Glitterfrost", "Hoarfrostchill", "Numbingshade" };


        public static string GetTileAssetName(BuildingType type, int x, int y)
		{
            string name = "winter_global_";
            if (type == BuildingType.Shed)
            {
                switch (y)
                {
                    case -1: return name + (54 + x);
                    case -2: return name + (84 + x);
                    case -3: return name + (120 + x);
                    case -4: return name + (156 + x);
                }
            }
            else if (type == BuildingType.PretzelStand)
            {
                switch (y)
                {
                    case -1: if (x == 0) return null;
                        return name + (237 + x);
                    case -2: return name + (266 + x);
                    case -3: return name + (296 + x);
                    case -4: return name + (329 + x);
                }
            }
            else if (type == BuildingType.CookieStand)
            {
                switch (y)
                {
                    case -1: return name + (233 + x);
                    case -2: return name + (258 + x);
                    case -3: return name + (286 + x);
                    case -4: return name + (319 + x);
                }
            }
            else if (type == BuildingType.Igloo)
            {
                switch (y)
                {
                    case -1: return name + (511 + x);
                    case -2: return name + (543 + x);
                }
            }
            else if (type == BuildingType.GingerbreadHouse)
            {
                switch (y)
                {
                    case -1: if (x == 0 || x == 3) return null;
                        return name + (597 + x);
                    case -2: return name + (628 + x);
                    case -3: return name + (660 + x);
                    case -4: return name + (692 + x);
                    case -5: return name + (715 + x);
                }
            }
            else if (type == BuildingType.Workshop)
			{
                switch (y)
                {
                    case -1: return name + (494 + x);
                    case -2: return name + (524 + x);
                    case -3: return name + (559 + x);
                    case -4: return name + (583 + x);
                    case -5: return name + (609 + x);
                    case -6: return name + (641 + x);
                    case -7: return name + (673 + x);
                    case -8: return name + (696 + x);
                }
            }
            return null;
		}
    }
}

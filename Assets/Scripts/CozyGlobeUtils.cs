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
        public static int[] ExpToNextLvl = new int[] { 0, 100, 500, 2000, 5000, 10000 }; // 1-indexed

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

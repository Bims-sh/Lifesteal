using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Structs;

namespace Lifesteal.Helpers;

public class GungameHelper
{    
    public static void GenerateLoadouts(LifestealServer server)
    {
        var weapons = Data.Items.WeaponList;
        var barrels = Data.Items.BarrelList;
        var underBarrels = Data.Items.UnderBarrelRailList;
        var sights = Data.Items.SightList;
        var gadgets = Data.Items.GadgetList;

        List<Loadout> loadouts = new();

        var random = new Random();

        foreach (var weapon in weapons)
        {
            var loadout = new Loadout();

            loadout.PrimaryWeapon = weapon.Name;
            loadout.PrimaryWeaponBarrel = random.Next(0, 100) < 69 ? null : ListHelper.GetRandomItem(barrels).Name;
            loadout.PrimaryWeaponUnderBarrel =
                random.Next(0, 100) < 69 ? null : ListHelper.GetRandomItem(underBarrels).Name;
            if (weapon.WeaponType == WeaponType.SniperRifle)
            {
                loadout.PrimaryWeaponSight = Attachments.Echo.Name;
                loadout.PrimaryWeaponCantedSight = Attachments.CantedRedDot.Name;
                loadout.PrimaryWeaponBolt = Attachments.BoltActionE.Name;
            }
            else
            {
                loadout.PrimaryWeaponSight = ListHelper.GetRandomItem(sights).Name;
            }

            loadouts.Add(loadout);
        }

        foreach (var gadget in gadgets)
        {
            var gadgetLoadout = new Loadout();

            switch (gadget.Name)
            {
                case "SuicideC4":
                    {
                        gadgetLoadout.LightGadgetName = gadget.Name;
                        break;
                    }
                default:
                    {
                        gadgetLoadout.HeavyGadgetName = gadget.Name;
                        gadgetLoadout.HeavyGadgetExtra = byte.MaxValue;
                        break;
                    }
            }

            loadouts.Add(gadgetLoadout);
        }

        server.LoadoutList = loadouts;
    }
}
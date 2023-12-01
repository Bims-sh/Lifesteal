using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Structs;

namespace Lifesteal.Helpers;

public class GungameHelper
{
    public static async void UpdateVisitors(LifestealServer server)
    {
        server.Visitors = await server.PlayerStatsData.EstimatedDocumentCountAsync();
        Program.Logger.Info($"Updated visitors, a total of {server.Visitors} visitors so far!");
    }
    
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
            loadout.PrimaryWeaponBarrel = random.Next(0, 100) < 69 ? null : ListHelper.GetRandomItem(barrels, random).Name;
            loadout.PrimaryWeaponUnderBarrel =
                random.Next(0, 100) < 69 ? null : ListHelper.GetRandomItem(underBarrels, random).Name;
            if (weapon.WeaponType == WeaponType.SniperRifle)
            {
                loadout.PrimaryWeaponSight = Attachments.Echo.Name;
                loadout.PrimaryWeaponCantedSight = Attachments.CantedRedDot.Name;
                loadout.PrimaryWeaponBolt = Attachments.BoltActionE.Name;
            }
            else
            {
                loadout.PrimaryWeaponSight = ListHelper.GetRandomItem(sights, random).Name;
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
    
    public static void StopServer(LifestealServer server)
    {
        foreach (var player in server.AllPlayers)
        {
            player.Kick("Server is restarting, please rejoin in a few minutes!");
        }

        server.ExecuteCommand("stop");

        var process = System.Diagnostics.Process.GetProcessesByName("BattleBit");
        if (process.Length > 0)
        {
            process[0].Kill();
        }
    }
}
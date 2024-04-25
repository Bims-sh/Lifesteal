using BattleBitAPI;
using BattleBitAPI.Common;
using Lifesteal.Enums;
using Lifesteal.Structs;

namespace Lifesteal.API;
public class LifestealPlayer : Player<LifestealPlayer>
{
    public int Kills = 0;
    public int Deaths = 0;
    public int Level = 0;
    public float Kd => Deaths == 0 ? Kills : (float)Math.Round((float)Kills / Deaths, 2);
    public float KillsOnCurrentStreak = 0;
    public bool HasKillStreak = false;
    public bool HasUsedCommand = false;
    public bool HasHudEnabled = true;
    
    public PlayerRoles[] PlayerRoles = {
        Enums.PlayerRoles.Default
    };

    public bool AddPlayerRole(PlayerRoles playerRole)
    {
        if (PlayerRoles.Contains(playerRole)) return false;
        
        var updatedRoles = new PlayerRoles[PlayerRoles.Length + 1];

        for (var i = 0; i < PlayerRoles.Length; i++)
        {
            updatedRoles[i] = PlayerRoles[i];
        }

        updatedRoles[PlayerRoles.Length] = playerRole;
        PlayerRoles = updatedRoles;

        return true;
    }
    
    public void UpdateLoadout(Loadout loadout)
    {
        var primaryWeapon = loadout.PrimaryWeapon;
        var primaryExtraMagazines = loadout.PrimaryExtraMagazines == 0
            ? default
            : loadout.PrimaryExtraMagazines;
        var secondaryWeapon = loadout.SecondaryWeapon;
        var secondaryExtraMagazines = loadout.SecondaryExtraMagazines == 0
            ? default
            : loadout.SecondaryExtraMagazines;
        var heavyGadgetName = loadout.HeavyGadgetName;
        var heavyGadgetExtra = loadout.HeavyGadgetExtra == 0
            ? default
            : loadout.HeavyGadgetExtra;
        var lightGadgetName = loadout.LightGadgetName;
        var lightGadgetExtra = loadout.LightGadgetExtra == 0
            ? default
            : loadout.LightGadgetExtra;

        if (primaryWeapon != null)
        {
            var cantedSight = loadout.PrimaryWeaponCantedSight == null
                ? default
                : new Attachment(loadout.PrimaryWeaponCantedSight, AttachmentType.CantedSight);
            SetPrimaryWeapon(
                new WeaponItem()
                {
                    ToolName = primaryWeapon,
                    MainSightName = loadout.PrimaryWeaponSight,
                    BarrelName = loadout.PrimaryWeaponBarrel,
                    UnderRailName = loadout.PrimaryWeaponUnderBarrel,
                    CantedSight = cantedSight,
                    BoltActionName = loadout.PrimaryWeaponBolt
                },
                primaryExtraMagazines, true);
        }

        if (secondaryWeapon != null)
            SetSecondaryWeapon(
                new WeaponItem() { ToolName = secondaryWeapon, MainSightName = loadout.SecondaryWeaponSight },
                secondaryExtraMagazines, true);

        if (heavyGadgetName != null)
            SetHeavyGadget(heavyGadgetName, heavyGadgetExtra, true);

        if (lightGadgetName != null)
            SetLightGadget(lightGadgetName, lightGadgetExtra, true);
    }

    public void ResetPlayer()
    {
        Kills = 0;
        Deaths = 0;
        Level = 0;
        KillsOnCurrentStreak = 0;
        HasKillStreak = false;
    }
}
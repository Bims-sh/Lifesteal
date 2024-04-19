using BattleBitAPI.Common;

namespace Lifesteal.Data;

public class Items
{
    public static readonly List<Attachment> SightList = new()
    {
        Attachments.Holographic,
        Attachments.RedDot,
        Attachments.Reflex,
        Attachments.Strikefire,
        Attachments.Kobra
    };

    public static readonly List<Attachment> BarrelList = new()
    {
        Attachments.Basic,
        Attachments.Tactical,
        Attachments.SDN6762,
        Attachments.LongBarrel,
        Attachments.SuppressorShort
    };

    public static readonly List<Attachment> UnderBarrelRailList = new()
    {
        Attachments.VerticalGrip,
        Attachments.B25URK,
        Attachments.StabilGrip,
        Attachments.FABDTFG,
        Attachments.AngledGrip
    };

    public static List<Weapon> WeaponList = new()
    {
        Weapons.AK74,
        Weapons.M4A1,
        Weapons.G36C,
        Weapons.ACR,
        Weapons.SCARH,
        Weapons.AUGA3,
        Weapons.SG550,
        Weapons.HK419,
        Weapons.AsVal,
        Weapons.FAL,
        Weapons.HoneyBadger,
        Weapons.KrissVector,
        Weapons.PP2000,
        Weapons.P90,
        Weapons.MP5,
        Weapons.MP7,
        Weapons.PP19,
        Weapons.UMP45,
        Weapons.Ultimax100,
        Weapons.MG36,
        Weapons.Glock18,
        Weapons.DesertEagle,
    };

    public static readonly List<Gadget> GadgetList = new()
    {
        Gadgets.Rpg7HeatExplosive,
        Gadgets.SledgeHammerSkinC,
        Gadgets.PickaxeIronPickaxe,
        Gadgets.SuicideC4
    };
}
namespace Lifesteal.Structs;

public struct Loadout
{
    public string? PrimaryWeapon { get; set; } = default;
    public string? PrimaryWeaponSight { get; set; } = default;
    public string? PrimaryWeaponCantedSight { get; set; } = default;
    public string? PrimaryWeaponBarrel { get; set; } = default;
    public string? PrimaryWeaponUnderBarrel { get; set; } = default;
    public string? PrimaryWeaponBolt { get; set; } = default;
    public byte PrimaryExtraMagazines { get; set; } = 3;
    public string? SecondaryWeapon { get; set; } = default;
    public string? SecondaryWeaponSight { get; set; } = default;
    public byte SecondaryExtraMagazines { get; set; } = 3;
    public string? HeavyGadgetName { get; set; } = default;
    public byte HeavyGadgetExtra { get; set; } = 3;
    public string? LightGadgetName { get; set; } = default;
    public byte LightGadgetExtra { get; set; } = 3;

    public Loadout()
    {
    }
}
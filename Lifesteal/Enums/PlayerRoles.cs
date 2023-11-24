namespace Lifesteal.Enums;

[Flags]
public enum PlayerRoles : int
{
    Default = 0,
    Special = 1 << 0,
    Vip = 1 << 1,
    Moderator = 1 << 2,
    Admin = 1 << 3
}
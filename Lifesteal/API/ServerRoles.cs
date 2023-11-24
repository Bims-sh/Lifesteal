using Lifesteal.Interfaces;

namespace Lifesteal.API;

public class ServerRoles : IServerRoles
{
    public string Admin => Enums.PlayerRoles.Admin.ToString();
    public string Moderator => Enums.PlayerRoles.Moderator.ToString();
    public string Vip => Enums.PlayerRoles.Vip.ToString();
    public string Special => Enums.PlayerRoles.Special.ToString();
    public string Default => Enums.PlayerRoles.Default.ToString();
}
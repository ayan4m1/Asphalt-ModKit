using Eco.Gameplay.Players;

namespace Asphalt.Storeable
{
    public interface IUserStorageContainer : IStorageContainer
    {
        IStorage GetUserStorage(User user);
    }
}

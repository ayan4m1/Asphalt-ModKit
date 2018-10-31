using Asphalt.Storeable;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;

namespace AsphaltFindEasterEggsPlugin
{
    public class CommandHandler : IChatCommandHandler
    {
        [ChatCommand("Create Easter Egg")]
        public static void CreateEasterEgg(User user)
        {
            if (!AsphaltFindEasterEggsPlugin.PermissionChecker.CheckPermission(user, "easteregg.create"))  //CheckPermission will notify the user if he doesn't have permission
                return;

            user.Player.SendTemporaryMessage($"Easter Egg created!");
        }

        [ChatCommand("Collect Easter Egg")]
        public static void CollectEasterEgg(User user)
        {
            if (!AsphaltFindEasterEggsPlugin.PermissionChecker.CheckPermission(user, "easteregg.collect"))  //CheckPermission will notify the user if he doesn't have permission
                return;

            IStorage userStorage = AsphaltFindEasterEggsPlugin.CollectedEggsStorage.GetUserStorage(user);

            int collectedEggs = userStorage.GetInt("collectedEggs"); //if no value was stored before, this will return 0

            collectedEggs++;

            userStorage.Set("collectedEggs", collectedEggs);

            user.Player.SendTemporaryMessage($"easter egg collected!");
            user.Player.SendTemporaryMessage($"You have collected {collectedEggs} eggs");
        }
    }
}

using Abstracts;
using Scriptables.Modules;

namespace Gameplay.Player.Inventory
{
    public class PlayerInventoryController : BaseController
    {
        private readonly PlayerInventoryConfig _config;

        public EngineModuleConfig Engine => _config.Engine;
        
        public PlayerInventoryController(PlayerInventoryConfig config)
        {
            _config = config;
        }
    }
}
static class Constants
{
    public record Configs
    {
        public record Player
        {
            public const string PlayerConfig = "Configs/Player/PlayerConfig";
        }

        public record Enemy
        {
            public const string EnemySpawnConfig = "Configs/Enemy/EnemySpawnConfig";
        }

        public record Space
        {
            public const string SpaceConfig = "Configs/Space/SpaceConfig";
            public const string DefaultStarSpawn = "Configs/Space/DefaultStarSpawn";
            public const string DefaultPlanetSpawn = "Configs/Space/DefaultPlanetSpawn";
        }
    }

    public record Prefabs
    {
        public record Input
        {
            public const string KeyboardInput = "Prefabs/Input/KeyboardInput";
        }

        public record Gameplay
        {
            public const string Player = "Prefabs/Gameplay/Player";
        }

        public record Stuff
        {
            public const string GunPoint = "Prefabs/Stuff/GunPoint";
        }

        public record Canvas
        {
            public const string UICamera = "Prefabs/Canvas/UICamera";
            public const string MainCanvas = "Prefabs/Canvas/MainCanvas";

            public record Game
            {
                public const string StatusBarCanvas = "Prefabs/Canvas/Game/StatusBarCanvas";
                public const string SpeedometerCanvas = "Prefabs/Canvas/Game/SpeedometerCanvas";
                public const string DestroyPlayerCanvas = "Prefabs/Canvas/Game/DestroyPlayerCanvas";
            }
        }
    }
}
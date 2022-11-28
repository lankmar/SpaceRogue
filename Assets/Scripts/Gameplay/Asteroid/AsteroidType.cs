namespace Asteroid
{
    public enum AsteroidType
    {
        SingleAsteroid = 0,
        AsteroidsSmallCloud,
        AsteroidsBiglCloud,
        FastAsteroid
    }

    public enum AsteroidSizeType
    {
        Small = 0,
        Medium,
        Big
    }

    public enum AsteroidDistractionType
    {
        FullDestruction = 0,
        ToAsteroidsSmallCloud,

    }

    public enum AsteroidMoveType
    {
        Static = 0,
        OrbitalMotion,
        LinearMotion,
        StrikingMotion
    }

    public enum AsteroidImpactObjectType
    {
        PlayerShip = 0,
        EnemyShip,
        Planet,
        Asteroid,
        Sun
    }
}
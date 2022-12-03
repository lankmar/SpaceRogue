using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay.Space.Generator
{
    public sealed class DebugLevelGenerator : SpaceGenerator
    {
        private readonly Tilemap _borderTilemap;
        private readonly Tilemap _borderMaskTilemap;
        private readonly Tilemap _nebulaTilemap;
        private readonly Tilemap _nebulaMaskTilemap;

        private readonly Tilemap _starTilemap;
        private readonly TileBase _starTileBase;

        public DebugLevelGenerator(DebugLevelGeneratorView debugLevelGeneratorView) : base(debugLevelGeneratorView.SpaceView, debugLevelGeneratorView.SpaceConfig, debugLevelGeneratorView.StarSpawnConfig)
        {
            _borderTilemap = debugLevelGeneratorView.SpaceView.BorderTilemap;
            _borderMaskTilemap = debugLevelGeneratorView.SpaceView.BorderMaskTilemap;
            _nebulaTilemap = debugLevelGeneratorView.SpaceView.NebulaTilemap;
            _nebulaMaskTilemap = debugLevelGeneratorView.SpaceView.NebulaMaskTilemap;

            _starTilemap = debugLevelGeneratorView.StarTilemap;
            _starTileBase = debugLevelGeneratorView.StarTileBase;
        }

        protected override void Draw()
        {
            ClearTileMaps();

            DrawLayer(_borderMap, _borderTilemap, _borderTileBase);
            DrawLayer(_borderMap, _borderMaskTilemap, _borderMaskTileBase);
            DrawLayer(_nebulaMap, _nebulaTilemap, _nebulaTileBase);
            DrawLayer(_nebulaMap, _nebulaMaskTilemap, _nebulaMaskTileBase);

            DrawLayer(_starMap, _starTilemap, _starTileBase);
        }

        private void DrawLayer(int[,] map, Tilemap tilemap, TileBase tileBase)
        {
            if (map == null)
            {
                return;
            }

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    var positionTile = new Vector3Int(-map.GetLength(0) / 2 + x, -map.GetLength(1) / 2 + y, 0);

                    if (map[x, y] == 1)
                    {
                        tilemap.SetTile(positionTile, tileBase);
                    }
                }
            }
        }

        public void ClearTileMaps()
        {
            if (_borderTilemap != null)
            {
                _borderTilemap.ClearAllTiles();
                _borderMaskTilemap.ClearAllTiles();
            }
            if (_nebulaTilemap != null)
            {
                _nebulaTilemap.ClearAllTiles();
                _nebulaMaskTilemap.ClearAllTiles();
            }
            if (_starTilemap != null)
            {
                _starTilemap.ClearAllTiles();
            }
        }
    }
}
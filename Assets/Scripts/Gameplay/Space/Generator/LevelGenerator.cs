using Scriptables.Space;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay.Space.Generator
{
    public sealed class LevelGenerator : SpaceGenerator
    {
        private readonly Tilemap _borderTilemap;
        private readonly Tilemap _borderMaskTilemap;
        private readonly Tilemap _nebulaTilemap;
        private readonly Tilemap _nebulaMaskTilemap;

        public LevelGenerator(SpaceView spaceView, SpaceConfig spaceConfig, StarSpawnConfig starSpawnConfig) : base(spaceView, spaceConfig, starSpawnConfig)
        {
            _borderTilemap = spaceView.BorderTilemap;
            _borderMaskTilemap = spaceView.BorderMaskTilemap;
            _nebulaTilemap = spaceView.NebulaTilemap;
            _nebulaMaskTilemap = spaceView.NebulaMaskTilemap;
        }

        protected override void Draw()
        {
            DrawLayer(_borderMap, _borderTilemap, _borderTileBase);
            DrawLayer(_borderMap, _borderMaskTilemap, _borderMaskTileBase);
            DrawLayer(_nebulaMap, _nebulaTilemap, _nebulaTileBase);
            DrawLayer(_nebulaMap, _nebulaMaskTilemap, _nebulaMaskTileBase);
        }

        public List<Vector3> GetStarSpawnPoints()
        {
            if (_starMap == null)
            {
                return new();
            }

            var starSpawnPoints = new List<Vector3>();

            for (int x = 0; x < _starMap.GetLength(0); x++)
            {
                for (int y = 0; y < _starMap.GetLength(1); y++)
                {
                    var positionTile = new Vector3Int(-_starMap.GetLength(0) / 2 + x, -_starMap.GetLength(1) / 2 + y, 0);

                    if (_starMap[x, y] == 1)
                    {
                        starSpawnPoints.Add(_nebulaTilemap.GetCellCenterWorld(positionTile));
                    }
                }
            }

            return starSpawnPoints;
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
    }
}
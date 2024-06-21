using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private List<Texture2D> _allMapTextures;

        [SerializeField] private BlockTagProvider _blockPrefab;
        [SerializeField] private Vector2 _spawnOffset;
        [SerializeField] private Vector3 _spawnStartPos;

        private List<BlockTagProvider> _spawnedBlocks = new List<BlockTagProvider>();

        public void SpawnRandomMap()
        {
            ClearSpawnedBlocks();

            Texture2D texture = _allMapTextures[Random.Range(0, _allMapTextures.Count)];

            Vector3 spawnPos = _spawnStartPos;

            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    if (texture.GetPixel(x, y) == Color.white)
                    {
                        BlockTagProvider block = Instantiate(_blockPrefab, spawnPos, Quaternion.identity, transform);
                        _spawnedBlocks.Add(block);
                    }

                    spawnPos.x += _spawnOffset.x;
                }

                spawnPos.x = _spawnStartPos.x;
                spawnPos.y += _spawnOffset.y;
            }
        }

        private void ClearSpawnedBlocks()
        {
            if (_spawnedBlocks != null && _spawnedBlocks.Count > 0)
            {
                foreach (var block in _spawnedBlocks)
                {
                    if (block.gameObject != null)
                        Destroy(block.gameObject);
                }
                _spawnedBlocks?.Clear();
            }
        }
    }
}
using System;
using System.Linq;
using UnityEngine;

namespace Player
{
    [Serializable]
    public enum SpawnRotation
    {
        R0 = 0,
        R90 = 90,
        R180 = 180,
        R270 = 270
    }
    
    [Serializable]
    public struct SpawnBound
    {
        public SpawnRotation rotation;
        public Vector2Int Lower;
        public Vector2Int Upper;
    }
    
    public class Model : MonoBehaviour
    {
        [SerializeField]
        private SpawnBound[] spawnBounds = new SpawnBound[0];

        public SpawnBound SpawnBound(SpawnRotation rotation) => spawnBounds.First(_ => _.rotation == rotation);
    }
}
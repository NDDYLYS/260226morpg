using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CustomTile : Tile
{
    [SerializeField] private bool isWalkable;
}
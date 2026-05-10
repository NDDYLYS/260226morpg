using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private List<Tilemap> wallTilemapList;
    [SerializeField] private Tilemap wallTilemap; // 이동 불가 타일맵

    void Update()
    {
        UnitFlip();
        getNearestTilemap();
        Move();
    }

    void UnitFlip()
    {
        var h = Input.GetAxis("Horizontal");
        if (h != 0)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(h) * -1;
            transform.localScale = scale;
        }
    }

    private void getNearestTilemap() 
    {
        if (wallTilemapList == null)
            wallTilemapList = new List<Tilemap>();
        else
            wallTilemapList.Clear();

        var notTiles = GameObject.FindGameObjectsWithTag("NotTiles");
        foreach (var tile in notTiles)
        {
            wallTilemapList.Add(tile.GetComponent<Tilemap>());
        }

        if (wallTilemapList == null || wallTilemapList.Count <= 0)
            return;

        Tilemap closest = null;
        var closestDistance = Mathf.Infinity;

        foreach (var tilemap in wallTilemapList) 
        {
            if (tilemap == transform)
                continue;

            var distance = Vector3.SqrMagnitude(transform.position - tilemap.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = tilemap;
            }
        }

        wallTilemap = closest;
    }

    private void Move()
    {
        var h = Input.GetAxis("Horizontal"); // ← →
        var v = Input.GetAxis("Vertical"); // ↑ ↓

        var dir_v = transform.up * v;
        var dir_h = transform.right * h;
        var dir = new Vector3(h, v, 0f);
        var nextPos = transform.position + dir * moveSpeed * Time.deltaTime;

        if (CanMove(nextPos))
        {
            transform.position = nextPos;
        }
    }

    private bool CanMove(Vector3 worldPos)
    {
        var dir = (worldPos - transform.position).normalized;
        var checkPos = worldPos + dir * 0.3f;
        var cellPos = wallTilemap.WorldToCell(checkPos);
        var movable = true;

        if (wallTilemap.HasTile(cellPos))
        {
            movable = false;
        }
        else 
        {
            
        }

        return movable;
    }
}
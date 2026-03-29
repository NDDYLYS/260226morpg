using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private Tilemap wallTilemap; // 이동 불가 타일맵

    void Update()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        var h = Input.GetAxis("Horizontal"); // ← →
        transform.Rotate(0, 0, -h * rotateSpeed * Time.deltaTime);
    }

    void Move()
    {
        var v = Input.GetAxis("Vertical"); // ↑ ↓

        var dir = transform.up * v;
        var nextPos = transform.position + dir * moveSpeed * Time.deltaTime;

        if (CanMove(nextPos))
        {
            transform.position = nextPos;
        }
    }

    bool CanMove(Vector3 worldPos)
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
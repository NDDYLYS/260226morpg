using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunks : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] GameObject playerObj;
    [SerializeField] private Dictionary<TilemapEnum, List<TilemapRenderer>> dics;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerObj = player.gameObject;

        dics = new Dictionary<TilemapEnum, List<TilemapRenderer>>();
        var childCount = this.transform.childCount;
        for (var c = 0; c < childCount; c++)
        {
            var child = this.transform.GetChild(c);
            if (child != null) 
            {
                var tilemaps = child.GetComponentsInChildren<TilemapRenderer>().ToList();                
                if (tilemaps != null) 
                {
                    var e = (TilemapEnum) c;
                    if (!dics.ContainsKey(e))
                        dics.Add(e, tilemaps);
                    else
                        dics[e].AddRange(tilemaps);
                }
            }
        }
    }

    private void Update()
    {
        updateChunks();
    }

    private void updateChunks()
    {
        if (dics == null)
            return;

        var iter = dics.GetEnumerator();
        while (iter.MoveNext()) 
        {
            if (iter.Current.Key == TilemapEnum.Deco)
                return;

            var chunks = iter.Current.Value;
            foreach (var chunk in chunks)
            {
                var dist = Vector3.Distance(playerObj.transform.position, chunk.transform.position);
                chunk.enabled = dist < distance;
            }
        }
    }
}

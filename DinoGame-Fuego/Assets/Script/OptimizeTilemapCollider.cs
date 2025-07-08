using UnityEngine;
using UnityEngine.Tilemaps;

public class OptimizeTilemapCollider : MonoBehaviour {
    public Tilemap tilemap;

    void Start() {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null) {
                    // Menghapus collider pada tile kosong
                    Vector3Int position = new Vector3Int(x + bounds.x, y + bounds.y, 0);
                    tilemap.SetColliderType(position, Tile.ColliderType.None);
                }
            }
        }
    }
}

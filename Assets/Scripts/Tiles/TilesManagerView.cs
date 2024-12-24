using Tiles.Manager;
using Tiles.Model;
using UnityEngine;

namespace Tiles
{
    public class TilesManagerView : MonoBehaviour
    {
        [SerializeField] private TileView tilePattern;

        private TilesManager _model;

        public void Init(TilesManager model)
        {
            _model = model;
            _model.OnGridCreated += GridCreated;
        }

        private void GridCreated(Tile[][] tiles)
        {
            for (var i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    CreateTile(tiles[i][j]);
                }
            }
        }

        private void CreateTile(Tile tile)
        {
            var tileView = Instantiate(tilePattern,new Vector3(tile.Position.x, 0, tile.Position.y), Quaternion.identity, transform);
            tile.BindView(tileView);
        }
    }
}
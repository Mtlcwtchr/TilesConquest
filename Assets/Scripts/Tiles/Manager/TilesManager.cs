using System;
using System.Collections.Generic;
using Tiles.Model;
using UnityEngine;
using Utils;

namespace Tiles.Manager
{
	public class TilesManager
    {
        public event Action<Tile[][]> OnGridCreated;
        public event Action<TileFill> OnFillSelected;

        public event Action<Tile> OnTileSelected;
		
		private Tile[][] _tiles;

		private List<Tile> _activeTiles = new();
		private List<Tile> _availablePlaces = new();

        private TileFill _selectedFill;

        public void Update(World.World world)
        {
            for (var i = 0; i < _activeTiles.Count; i++)
            {
                _activeTiles[i].ApplyEffects(world);
            }
        }
		
		public void CreateGrid(Vector2Int size)
        {
            _tiles = new Tile[size.x][];
            for (var i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = new Tile[size.y];
                for (int j = 0; j < _tiles[i].Length; j++)
                {
                    var tile = new Tile(new(i, j));
                    _tiles[i][j] = tile;
                    tile.OnMouseClick += TileClick;
                }
            }

            CalculateAvailablePlaceholders();
            OnGridCreated?.Invoke(_tiles);
        }

        private void CalculateAvailablePlaceholders()
        {
            for (var i = 0; i < _availablePlaces.Count; i++)
            {
                _availablePlaces[i].Available = false;
            }
            _availablePlaces.Clear();
            if (_activeTiles.Count == 0)
            {
                for (var i = 0; i < _tiles.Length; i++)
                {
                    for (var j = 0; j < _tiles[i].Length; j++)
                    {
                        var tile = _tiles[i][j];
                        tile.Available = true;
                        _availablePlaces.Add(tile);
                    }
                }

                return;
            }
            
            for (var i = 0; i < _activeTiles.Count; i++)
            {
                CalculateAvailablePlaceholders(_activeTiles[i]);
            }
        }

        private void CalculateAvailablePlaceholders(Tile holder)
        {
            var pos = holder.Position;
            for (int i = _tiles.CheckIndex(pos.x - 1); i <= _tiles.CheckIndex(pos.x + 1); ++i)
            {
                for (int j = _tiles[i].CheckIndex(pos.y - 1); j <= _tiles[i].CheckIndex(pos.y + 1); ++j)
                {
                    if (i == pos.x && j == pos.y)
                        continue;

                    var checking = _tiles[i][j];
                    if (!checking.Filled)
                    {
                        if (!_availablePlaces.Contains(checking))
                        {
                            checking.Available = true;
                            _availablePlaces.Add(checking);
                        }
                    }
                }
            }
        }

        public void SetFill(Vector2Int pos, TileFill fill)
        {
            var tile = _tiles[pos.x][pos.y];
            if (!tile.Available)
                return;
            
            tile.SetFill(fill);
            
            tile.Available = false;
            tile.Highlighted = false;
            
            _activeTiles.Add(tile);
            _availablePlaces.Remove(tile);
        }

        public void NotifyFillSelected(TileFill fill)
        {
            _selectedFill = fill;
            SetHighlighted(true);

            OnFillSelected?.Invoke(fill);
        }

        public void NotifyTileFilled()
        {
            _selectedFill = null;
            SetHighlighted(false);
            
            CalculateAvailablePlaceholders();
            OnFillSelected?.Invoke(null);
        }

        private void TileClick(Tile tile)
        {
            if (tile.Filled)
            {
                OnTileSelected?.Invoke(tile);
                return;
            }
            
            if (_selectedFill == null)
                return;

            if (!tile.Available)
                return;
            
            SetFill(tile.Position, _selectedFill);
            NotifyTileFilled();
        }

        public Tile GetTile(Vector2Int position)
        {
            return _tiles[position.x][position.y];
        }

        public Tile GetRandomActive()
        {
            return _activeTiles.GetRandom();
        }

        public List<Tile> GetActiveTiles()
        {
            return _activeTiles;
        }

        public void SetHighlighted(bool isHighlighted)
        {
            for (var i = 0; i < _availablePlaces.Count; i++)
            {
                _availablePlaces[i].Highlighted = isHighlighted;
            }
        }
	}
}
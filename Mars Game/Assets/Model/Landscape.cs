using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landscape {

	Tile[,] tiles;

	int width;
	public int Width{
		get{
			return width;
		}
	}

	int height;
	public int Height{
		get{
			return height;
		}
	}


	public Landscape( int width = 100, int height = 100) {
		this.width = width;
		this.height = height;

		tiles = new Tile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles [x, y] = new Tile (this, x, y);
			}
		}

		Debug.Log ("Landscape created with " + (width * height) + " tiles.");

	}

	public void RandomizeTiles( int seed){
		float taken = Time.time;
		int heightScale = 10;
		float detailScale = 25.0f;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				int z = (int)(Mathf.PerlinNoise ((x + seed) / detailScale, (y + seed) / detailScale) * heightScale);
				if (z <= 2) {
					tiles [x, y].Type = Tile.TileType.Ancient_Glacier;
				} else if (z <= 3) {
					tiles [x, y].Type = Tile.TileType.Loose_Dirt;
				} else if (z <= 4) {
					tiles [x, y].Type = Tile.TileType.Dirt;
				} else if (z <= 8) {
					tiles [x, y].Type = Tile.TileType.Rocky;
				}
			}
		}
	}

	public Tile GetTileAt(int x, int y){
		if (x > width || x < 0 || y > height || y < 0) {
			Debug.LogError ("Tile (" + x + ", " + y + ") is out of range.");
			return null;
		}
		return tiles [x, y];
	}

}

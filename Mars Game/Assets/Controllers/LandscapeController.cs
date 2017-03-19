using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeController : MonoBehaviour {

	public Sprite dirtSprite;
	public Sprite rockySprite;

	Landscape land;

	// Use this for initialization
	void Start () {
		//Create a landscape and randomize types
		land = new Landscape ();
		land.RandomizeTiles ();

		//Create GameObject for each tile, to show visually
		for (int x = 0; x < land.Width; x++) {
			for (int y = 0; y < land.Height; y++) {
				Tile tile_data = land.GetTileAt (x, y);

				GameObject tile_go = new GameObject ();
				tile_go.name = "Tile_" + x + "_" + y;
				tile_go.transform.position = new Vector3 (tile_data.X, tile_data.Y, 0);

				SpriteRenderer tile_sr = tile_go.AddComponent<SpriteRenderer> ();
				if (tile_data.Type == Tile.TileType.Dirt) {
					tile_sr.sprite = dirtSprite;
				}
				if (tile_data.Type == Tile.TileType.Rocky) {
					tile_sr.sprite = rockySprite;
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTileTypeChanged(Tile tile_data, GameObject tile_go){
		
		if (tile_data.Type == Tile.TileType.Rocky) {
			tile_go.GetComponent<SpriteRenderer> ().sprite = rockySprite;
		} else if (tile_data.Type == Tile.TileType.Dirt) {
			tile_go.GetComponent<SpriteRenderer> ().sprite = dirtSprite;
		} else {
			Debug.LogError ("OnTileTypeChanged - Inrecognized tile type");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeController : MonoBehaviour {

	public static LandscapeController Instance { get; protected set;}

	public Sprite dirtSprite;
	public Sprite rockySprite;
	public Sprite ancientGlacierSprite;

	public Landscape Landscape { get; protected set; }

	// Use this for initialization
	void Start () {
		if (Instance != null) {
			Debug.LogError ("There should never be 2 LandscapeControllers.");
		}
		Instance = this;

		//Create a landscape and randomize types
		Landscape = new Landscape ();
		Landscape.RandomizeTiles ();

		//Create GameObject for each tile, to show visually
		for (int x = 0; x < Landscape.Width; x++) {
			for (int y = 0; y < Landscape.Height; y++) {
				Tile tile_data = Landscape.GetTileAt (x, y);

				GameObject tile_go = new GameObject ();
				tile_go.name = "Tile_" + x + "_" + y;
				tile_go.transform.position = new Vector3 (tile_data.X, tile_data.Y, 0);
				tile_go.transform.SetParent (this.transform, true);

				SpriteRenderer tile_sr = tile_go.AddComponent<SpriteRenderer> ();

				if (tile_data.Type == Tile.TileType.Dirt) {
					tile_sr.sprite = dirtSprite;
				}
				if (tile_data.Type == Tile.TileType.Rocky) {
					tile_sr.sprite = rockySprite;
				}
				if (tile_data.Type == Tile.TileType.Ancient_Glacier) {
					tile_sr.sprite = ancientGlacierSprite;
				}

				tile_data.RegisterTileTypeChangedCallback ((tile) => {
					OnTileTypeChanged (tile, tile_go);
				});
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
			Debug.LogError ("OnTileTypeChanged - Unrecognized tile type.");
		}
	}

	public Tile GetTileAtWorldCoord(Vector3 coord){
		int x = Mathf.FloorToInt (coord.x);
		int y = Mathf.FloorToInt (coord.y);

		return Landscape.GetTileAt (x, y);
	}
}

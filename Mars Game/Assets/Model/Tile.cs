using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	public enum TileType {Loose_Dirt, Rocky, Dirt, Ancient_Glacier};

	TileType type = TileType.Dirt;
	public TileType Type{
		get{
			return type;
		}
		set{
			type = value;
			// Call the callback and let things know we've changed.

		}
	}

	LooseObject looseObject;
	PlacedObject placedObject;

	Landscape land;
	int x;
	public int X{
		get{ 
			return x;
		}
	}

	int y;
	public int Y {
		get {
			return y;
		}
	}

	public Tile( Landscape land, int x, int y ){
		this.land = land;
		this.x = x;
		this.y = y;
	}

}

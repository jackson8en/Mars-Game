using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {

	public enum TileType {Loose_Dirt, Rocky, Dirt, Ancient_Glacier};

	Action<Tile> cbTileTypeChanged;

	TileType type = TileType.Dirt;
	public TileType Type{
		get{
			return type;
		}
		set{
			TileType oldType = type;
			type = value;
			// Call the callback and let things know we've changed.
			if (cbTileTypeChanged != null && oldType != type) {
				cbTileTypeChanged(this);
			}
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

	public void RegisterTileTypeChangedCallback(Action<Tile> callback){
		cbTileTypeChanged += callback;
	}
	public void UnRegisterTileTypeChangedCallback(Action<Tile> callback){
		cbTileTypeChanged -= callback;
	}
}

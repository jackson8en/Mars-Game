using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	public GameObject cursorHoverPrefab;
	public float MIN_X, MAX_X, MIN_Y, MAX_Y;

	private float cameraPanSpeed = 4.0f;

	Vector3 lastFramePosition;
	Vector3 currentFramePosition;

	Vector3 dragStartPosition;
	List<GameObject> dragPreviewGameObjects;

	// Use this for initialization
	void Start () {
		dragPreviewGameObjects = new List<GameObject> ();

		SimplePool.Preload (cursorHoverPrefab, 250);
	}
	
	// Update is called once per frame
	void Update () {

		currentFramePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentFramePosition.z = 0;

		//UpdateHoveringCursor ();

		//handle leftMB
		//startDrag
		if (Input.GetMouseButtonDown(0)) {
			dragStartPosition = currentFramePosition;
		}

		int start_x = Mathf.FloorToInt (dragStartPosition.x);
		int end_x = Mathf.FloorToInt (currentFramePosition.x);
		if (end_x < start_x) {
			int tmp = end_x;
			end_x = start_x;
			start_x = tmp;
		}

		int start_y = Mathf.FloorToInt (dragStartPosition.y);
		int end_y = Mathf.FloorToInt (currentFramePosition.y);
		if (end_y < start_y) {
			int tmp = end_y;
			end_y = start_y;
			start_y = tmp;
		}

		//cleanUpOldDragPreviews
		while (dragPreviewGameObjects.Count > 0) {
			GameObject go = dragPreviewGameObjects [0];
			dragPreviewGameObjects.RemoveAt (0);
			SimplePool.Despawn(go);
		}

		//previewDrag
		if (Input.GetMouseButton (0)) {
			//Display preview of "dragged" area
			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					Tile t = LandscapeController.Instance.Landscape.GetTileAt (x, y);
					if (t != null) {
						//Display ghosting on top of this tile position
						GameObject go = SimplePool.Spawn(cursorHoverPrefab, new Vector3 (x, y, 0), Quaternion.identity);
						go.transform.SetParent (this.transform);
						dragPreviewGameObjects.Add (go);
					}
				}
			}
		}

		//endDrag
		if (Input.GetMouseButtonUp(0)) {
			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					Tile t = LandscapeController.Instance.Landscape.GetTileAt (x, y);
					if (t != null) {
						t.Type = Tile.TileType.Rocky;
					}
				}
			}
		}

		//moveCamera with middleMouse and drag
		// 0 - leftMB, 1 - rightMB, 2 - middleMB
		if (Input.GetMouseButton(2)) {
			Vector3 difference = lastFramePosition - currentFramePosition;
			Camera.main.transform.Translate (difference);
		}

		lastFramePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lastFramePosition.z = 0;

		//Take keyboard inputs and move camera accordingly
		if (Input.GetKey (KeyCode.W)) {
			Camera.main.transform.position += Vector3.up * cameraPanSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) {
			Camera.main.transform.position += Vector3.left * cameraPanSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) {
			Camera.main.transform.position += Vector3.down * cameraPanSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D)) {
			Camera.main.transform.position += Vector3.right * cameraPanSpeed * Time.deltaTime;
		}

		//zoomCamera with scroll wheel
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll > 0) {
			if (!(Camera.main.orthographicSize < 5)) {
				Camera.main.orthographicSize -= Camera.main.orthographicSize * scroll;
			}
		} else if (scroll < 0) {
			if (!(Camera.main.orthographicSize > 25)) {
				Camera.main.orthographicSize -= Camera.main.orthographicSize * scroll;
			}
		}

		//set boundaries for camera movement
		Camera.main.transform.position = new Vector3 (
			Mathf.Clamp(Camera.main.transform.position.x, MIN_X, MAX_X),
			Mathf.Clamp(Camera.main.transform.position.y, MIN_Y, MAX_Y),
			-10f);

	}

//	void UpdateHoveringCursor(){
//		//Update cursorHover position
//		Tile tileUnderMouse = LandscapeController.Instance.GetTileAtWorldCoord (currentFramePosition);
//		if (tileUnderMouse != null) {
//			cursorHover.SetActive(true);
//			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, 0);
//			cursorHover.transform.position = cursorPosition;
//		} else {
//			cursorHover.SetActive(false);
//		}
//	}
}

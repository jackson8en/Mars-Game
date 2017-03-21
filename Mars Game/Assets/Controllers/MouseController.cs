using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	public GameObject cursorHover;
	public float MIN_X, MAX_X, MIN_Y, MAX_Y, MIN_Z, MAX_Z;

	private float speed = 4.0f;
	private float zoomSpeed = 2.0f;

	Vector3 lastFramePosition;
	Vector3 dragStartPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 currentFramePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentFramePosition.z = 0;

		//Update cursorHover position
		Tile tileUnderMouse = GetTileAtWorldCoord (currentFramePosition);
		if (tileUnderMouse != null) {
			cursorHover.SetActive(true);
			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, 0);
			cursorHover.transform.position = cursorPosition;
		} else {
			cursorHover.SetActive(false);
		}

		//handle leftMB
		//startDrag
		if (Input.GetMouseButtonDown(0)) {
			dragStartPosition = currentFramePosition;
		}

		//endDrag
		if (Input.GetMouseButtonUp(0)) {
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
			transform.Translate (difference);
		}

		lastFramePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lastFramePosition.z = 0;

		//Take keyboard inputs and move camera accordingly
		if (Input.GetKey (KeyCode.W)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//zoomCamera with scroll wheel
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		Camera.main.orthographicSize += scroll * zoomSpeed;

		//set boundaries for camera movement
		transform.position = new Vector3 (
			Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
			Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
			Mathf.Clamp(transform.position.z, MIN_Z, MAX_Z));

	}

	Tile GetTileAtWorldCoord(Vector3 coord){
		int x = Mathf.FloorToInt (coord.x);
		int y = Mathf.FloorToInt (coord.y);

		return LandscapeController.Instance.Landscape.GetTileAt (x, y);
	}
}

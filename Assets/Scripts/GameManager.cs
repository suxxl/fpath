using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject gameNode;
	private Field gameField;

	// Use this for initialization
	void Start () {
		gameField = new Field (Screen.width, Screen.height);
//		List<Node> nodeList = new List<Node> ();
		Path path = new Path (gameField);
		path.findPath (new Vector2 (3.3f, 4.0f), new Vector2 (1.0f, 2.0f));
//		Debug.Log (nodeList);
		showNodesMarkers ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void showNodesMarkers()
	{
		Color cCol = gameNode.GetComponent<SpriteRenderer> ().color;
		for(int x = 0; x < gameField.getWNodesNumber(); x++)
		{
			for(int y = 0; y < gameField.getHNodesNumber(); y++)
			{
				cCol.a = (gameField.getNodes (x, y).getD () + 1.0f) / 100f;
				gameNode.GetComponent<SpriteRenderer> ().color = cCol;
				GameObject.Instantiate(gameNode, Camera.main.ScreenToWorldPoint(new Vector3(gameField.getNodes (x, y).getNodeX(), gameField.getNodes (x, y).getNodeY(), 10.0f)), Quaternion.identity);					//				Debug.Log (gameField.getNodes (x, y));
			}
		}

	}
}

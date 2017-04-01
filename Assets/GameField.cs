using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {
	private const float WallCost = 1000;

	class Cell
	{
		public int cameFrom = -1;
		public float f = 0;
		public float g = 0;
		public float cost = 0;
	}

	struct Direction
	{
		public int x;
		public int y;
		public float cost;

		public Direction(int x, int y, float cost = 1.0f)
		{
			this.x = x;
			this.y = y;

			this.cost = cost;
		}
	}

	private static Direction[] directions = new Direction[] {
		new Direction(-1, 0),
		new Direction(1, 0),
		new Direction(0, -1),
		new Direction(0, 1),
		new Direction(-1, -1, 1.41f),
		new Direction(1, 1, 1.41f),
		new Direction(1, -1, 1.41f),
		new Direction(-1, 1, 1.41f)};

	public int Width = 750;
	public int Height = 1334;

	public int CellSize = 10;

	public GameObject Marker;

	private Cell[] cells;

	private int cellsX;
	private int cellsY;

	void Awake()
	{
		cellsX = Mathf.CeilToInt ((float)Width / CellSize);
		cellsY = Mathf.CeilToInt ((float)Height / CellSize);

		cells = new Cell[cellsX * cellsY];

		for (int i = 0, count = cellsX * cellsY; i < count; ++i)
			cells [i] = new Cell ();
	}

	float F(int aNodeIndex, int bNodeIndex)
	{
		return
			Mathf.Abs(aNodeIndex / cellsX - bNodeIndex / cellsX) +
			Mathf.Abs(aNodeIndex % cellsX - bNodeIndex % cellsX);
	}

	int GetCellIndex(float x, float y)
	{
		int index = Mathf.FloorToInt (x / CellSize) + Mathf.FloorToInt (y / CellSize) * cellsX;

		if (index < 0)
			return -1;
		if (index >= cellsX * cellsY)
			return -1;

		return index;
	}

	List<int> FindPath(int fromCell, int toCell)
	{
		foreach (var cell in cells)
		{
			cell.cameFrom = -1;
			cell.f = Mathf.Infinity;
			cell.g = Mathf.Infinity;
		}

		cells [fromCell].g = cells [fromCell].cost;
		cells [fromCell].f = F (fromCell, toCell);

		var openSet = new HashSet<int> ();
		var closedSet = new HashSet<int> ();

		openSet.Add (fromCell);

		bool found = false;

		while (openSet.Count > 0)
		{
			float minF = Mathf.Infinity;
			int minFCellIndex = -1;

			foreach (var i in openSet)
			{
				if (cells[i].f < minF)
				{
					minF = cells [i].f;
					minFCellIndex = i;
				}
			}
				
			//
			var cell = minFCellIndex;

			if (cell == toCell)
			{
				found = true;
				break;
			}

			openSet.Remove (cell);
			closedSet.Add (cell);

			int x = cell % cellsX;
			int y = cell / cellsX;

			foreach (var dir in directions)
			{
				int nx = x + dir.x;
				int ny = y + dir.y;

				if (nx < 0 || nx >= cellsX ||
					ny < 0 || ny >= cellsY)
				{
					continue;
				}

				int nindex = nx + ny * cellsX;

				if (closedSet.Contains (nindex))
					continue;

				if (cells [nindex].cost >= WallCost)
					continue;

				// add cell cost
				float tentativeG = cells [cell].g + dir.cost + cells[nindex].cost;

				if (openSet.Contains(nindex))
				{
					if (tentativeG >= cells [nindex].g)
						continue;
				}
				else
				{
					openSet.Add (nindex);
				}

				cells [nindex].cameFrom = cell;
				cells [nindex].g = tentativeG;
				cells [nindex].f = tentativeG + F (nindex, toCell);
			}
		}

		if (found)
		{
			var path = new List<int> ();

			int cameFrom = toCell;

			do {
				path.Add (cameFrom);
				cameFrom = cells [cameFrom].cameFrom;
			} while (cameFrom != -1);

			path.Reverse ();

			return path;
		}

		return new List<int> ();
	}

	// Use this for initialization
	void Start ()
	{
		float FieldWidth = 44.0f;
		float FieldHeight = 84.0f;

		int a = GetCellIndex (100, 100);
		int b = GetCellIndex (200, 200);

		var path = FindPath (a, b);

		Debug.Log (path.Count);

		foreach (var cell in path)
		{
			//float x = ((float)(cell % cellsX) + 0.5f) * (float)CellSize;
			//float y = ((float)(cell / cellsX) + 0.5f) * (float)CellSize;
			float x = ((float)(cell % cellsX) / (float)cellsX - 0.5f) * FieldWidth;
			float y = ((float)(cell / cellsX) / (float)cellsY - 0.5f) * FieldHeight;

			GameObject.Instantiate (Marker, new Vector3 (x, y, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

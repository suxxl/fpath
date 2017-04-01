using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the field of the nodes is using to find the path to the target
public class Field{

	private int width;
	private int height;

	private int wNodesNumber;
	private int hNodesNumber;
	private Node[,] nodes;


	public Field(int _width, int _height)
	{
		width = _width;
		height = _height;
		createField (width, height);
	}

	public Node getNodes(int x, int y)
	{
		return nodes[x, y];
	}

	private void createField(int _width, int _height) //create nodes 
	{
		wNodesNumber = Mathf.FloorToInt(width / Node.nodeSize);
		Debug.Log (wNodesNumber);
		hNodesNumber = Mathf.FloorToInt(height / Node.nodeSize);
		Debug.Log (hNodesNumber);

		nodes = new Node[wNodesNumber, hNodesNumber];

		for(int w = 0; w < wNodesNumber; w++)
		{
			for(int h = 0; h < hNodesNumber; h++)
			{
				nodes [w, h] = new Node (1, w, h);
				//				Debug.Log (nodes [w, h]);
			}
		}
		Debug.Log (nodes.Length);

	}

	public int getWNodesNumber()
	{
		return wNodesNumber;
	}

	public int getHNodesNumber()
	{
		return hNodesNumber;
	}

	public Node getNodeByCoordinates(Vector2 _nodeCoordinates)
	{
		float x = Camera.main.WorldToScreenPoint (_nodeCoordinates).x;
		float y = Camera.main.WorldToScreenPoint (_nodeCoordinates).y;

		int w = Mathf.FloorToInt (x) / Node.nodeSize;
		int h = Mathf.FloorToInt (y) / Node.nodeSize;

		return nodes [w, h];
	}

//getting next step
	public Node getNextStep(Node _currentNode)
	{
		Node currentNode = _currentNode;
		Node nextNode = null;
		int currentD = currentNode.getD ();

		for(int dx = -1; dx <= 1; dx++)
		{
			for(int dy = -1; dy <= 1; dy++)
			{
				if (dx == 0 && dy == 0)
					continue;
				if(dx == 0 || dy == 0)
				{
					int x = currentNode.getNodeWIndex () + dx;
					int y = currentNode.getNodeHIndex () + dy;
					if(x >= 0 || x < wNodesNumber || y >=0 || y < hNodesNumber)
					{
						if(currentD == 0)
						{
							return null;
						}
						if(currentD == nodes[x,y].getD() - 1) //we move from higher D value to the lower
						{
							nextNode = nodes [x, y];
						}
					}
				}
			}
		}
		return nextNode;
	}
//end of getting next step

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
//	private bool isWalkable;
	private int nodeX;
	private int nodeY;
	private int d;
	public static int nodeSize = 50;

	public Node parent;

	public Node(int _price, int _nodeX, int _nodeY)
	{
//		isWalkable = _price <= 1000;
		nodeX = _nodeX;
		nodeY = _nodeY;
		d = -1;
	}

	//get node center coordinates
	public int getNodeX()
	{
		return nodeX * nodeSize + nodeSize/2;
	}

	public int getNodeY()
	{
		return nodeY * nodeSize + nodeSize/2;
	}

	public Vector2 getNodeCoordinates()
	{
		return new Vector2 (nodeX * nodeSize + nodeSize / 2, nodeY * nodeSize + nodeSize / 2);
	}

	public void setD(int _d)
	{
		d = _d;
	}

	public int getD()
	{
		return d;
	}

	public int getNodeWIndex()
	{
		return nodeX;
	}

	public int getNodeHIndex()
	{
		return nodeY;
	}
}

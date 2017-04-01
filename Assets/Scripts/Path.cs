using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path{
//	List<Node> nodeList = new List<Node> ();
	Queue<Node> nodeQueue = new Queue<Node> ();
	Field field;
	Node currentNode;
	int d;

	public Path(Field _field)
	{
		field = _field;

	}


	public void findPath(Vector2 _startPoint, Vector2 _endPoint)
	{
		
//		nodeList.Clear ();
		markD ();
		d = 0;
		//setting starting node D value to 0
		Node endNode = field.getNodeByCoordinates (_endPoint);
//		Node startNode = field.getNodeByCoordinates (_startPoint);
		endNode.setD (d);
//		nodeList.Add (endNode);
		currentNode = endNode;

		sendWave ();

//		currentNode = endNode;
//		while(currentNode != startNode)
//		{
//			for(int x = 0; x < field.getWNodesNumber(); x++)
//			{
//				for(int y = 0; y < field.getHNodesNumber(); y++)
//				{
//					if(field.getNodes(x, y).getD() == (currentNode.getD()+1)){
//						currentNode = field.getNodes (x, y);
//						nodeList.Add (currentNode);
//					}
//				}
//			}
//		}
//		return nodeList;
	}

	//if step will exit field
	private bool isValidStep(int _x, int _y)
	{
		int x = currentNode.getNodeWIndex() + _x;
		int y = currentNode.getNodeHIndex() + _y;

		if (x < 0 || x >= field.getWNodesNumber() || y < 0 || y >= field.getHNodesNumber()) {
			return false;
		} else
			return true;
	}

	private void addQ(int _x, int _y)
	{
		if(isValidStep(_x, _y))
		{
			Node node = field.getNodes (currentNode.getNodeWIndex() + _x, currentNode.getNodeHIndex() + _y);
			if(node.getD () < 0)
			{
				nodeQueue.Enqueue (node);
				node.setD (d + 1);
				d++;
//				Debug.Log (currentNode.getNodeX ());
//				Debug.Log (currentNode.getNodeX ());
				Debug.Log (d);
			}
		}
	}

	private void sendWave()
	{
		nodeQueue.Enqueue (currentNode);
		while(nodeQueue.Count > 0)
		{
			currentNode = nodeQueue.Dequeue ();
			for(int dx = -1; dx <= 1; dx++)
			{
				for(int dy = -1; dy <=1; dy++)
				{
					if (dx == 0 && dy == 0)
						continue;
					if(dx == 0 || dy == 0)
					{
						addQ (dx, dy);
					}
				}
			}
		}
	}

	private void markD()
	{
		for(int x = 0; x < field.getWNodesNumber(); x++)
		{
			for(int y = 0; y < field.getHNodesNumber(); y++)
			{
				field.getNodes (x, y).setD (-1);
			}
		}
	}

}
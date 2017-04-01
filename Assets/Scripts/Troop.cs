using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop: MonoBehaviour {

	private float x; //troop x coordinates
	private float y; //troop y coordinates
	public float moveSpeed = 0.1f;
	Vector2 pos;

	public enum TroopState
	{
		MOVE = 1,
		STAND = 2,
		ATTACK = 3,
		stateCount
	};

	public TroopState mState;

	Field gameField;
	Node nextStep;

	public Troop(Field _field)
	{
		gameField = _field;
		mState = TroopState.STAND;
		pos = transform.position;
	}

	//update is called once per frame
	void Update()
	{
		checkState ();
	}
		
	public Vector2 gNextStep(Vector2 _currentCoordinates) //if step is made return true, else return false
	{
		nextStep = gameField.getNextStep (gameField.getNodeByCoordinates (_currentCoordinates));
		if(nextStep == null)
		{
			mState = TroopState.STAND;
			return _currentCoordinates;
		}
		else
		{
			mState = TroopState.MOVE;
			return nextStep.getNodeCoordinates ();	
		}
	}

	public void makeNextStep(Vector2 _moveTo)
	{
		pos = transform.position;
		if(pos.x < _moveTo.x)
		{
			pos = new Vector2 (transform.position.x + moveSpeed, 0);
			transform.position = pos;
		}
		if(pos.x > _moveTo.x)
		{
			pos = new Vector2 (transform.position.x - moveSpeed, 0);
			transform.position = pos;
		}
		if(pos.y < _moveTo.y)
		{
			pos = new Vector2 (0, transform.position.y + moveSpeed);
			transform.position = pos;
		}
		if(pos.y > _moveTo.y)
		{
			pos = new Vector2 (0, transform.position.y + moveSpeed);
			transform.position = pos;
		}
	}

	public void checkState()
	{
		Vector2 nextStep = gNextStep (pos);
		switch(mState)
		{
		case TroopState.STAND:
			gNextStep (pos);
			mState = TroopState.MOVE;
			break;
		case TroopState.MOVE:
			makeNextStep (nextStep);
			break;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerActions : PlayerActionSet
{
	//Move
	public PlayerAction moveLeft;
	public PlayerAction moveRight;
	public PlayerAction moveUp;
	public PlayerAction moveDown;
	public PlayerTwoAxisAction move;

	//Aim
	public PlayerAction aimLeft;
	public PlayerAction aimRight;
	public PlayerAction aimUp;
	public PlayerAction aimDown;
	public PlayerTwoAxisAction aimMove;

	//Actions
	public PlayerAction shoot;


	public PlayerActions()
	{
		//Move
		moveLeft = CreatePlayerAction("MoveLeft");
		moveRight = CreatePlayerAction("MoveRight");
		moveUp = CreatePlayerAction("MoveUp");
		moveDown = CreatePlayerAction("MoveDown");
		move = CreateTwoAxisPlayerAction(moveLeft, moveRight, moveDown, moveUp);

		//Aim
		aimLeft = CreatePlayerAction("AimLeft");
		aimRight = CreatePlayerAction("AimRight");
		aimUp = CreatePlayerAction("AimUp");
		aimDown = CreatePlayerAction("AimDown");
		aimMove = CreateTwoAxisPlayerAction(aimLeft, aimRight, aimDown, aimUp);

		//Actions
		shoot = CreatePlayerAction("Shoot");
	}

	public static PlayerActions CreateWithKeyboardBindings()
	{
		var actions = new PlayerActions();

		//Move
		actions.moveUp.AddDefaultBinding(Key.UpArrow);
		actions.moveDown.AddDefaultBinding(Key.DownArrow);
		actions.moveLeft.AddDefaultBinding(Key.LeftArrow);
		actions.moveRight.AddDefaultBinding(Key.RightArrow);
	
		actions.moveUp.AddDefaultBinding(Key.W);
		actions.moveDown.AddDefaultBinding(Key.S);
		actions.moveLeft.AddDefaultBinding(Key.A);
		actions.moveRight.AddDefaultBinding(Key.D);

		//Aim
		/*actions.aimUp.AddDefaultBinding(Mouse.PositiveY);
		actions.aimDown.AddDefaultBinding(Mouse.NegativeY);
		actions.aimLeft.AddDefaultBinding(Mouse.NegativeX);
		actions.aimRight.AddDefaultBinding(Mouse.PositiveX);*/

		//Actions
		actions.shoot.AddDefaultBinding(Mouse.LeftButton); // we need to find the way to change this to left click button http://www.gallantgames.com/incontrol-api/html/class_in_control_1_1_mouse_binding_source.html this can help
		return actions;
	}


	public static PlayerActions CreateWithJoystickBindings()
	{
		var actions = new PlayerActions();
		//Move
		actions.moveUp.AddDefaultBinding(InputControlType.LeftStickUp);
		actions.moveDown.AddDefaultBinding(InputControlType.LeftStickDown);
		actions.moveLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
		actions.moveRight.AddDefaultBinding(InputControlType.LeftStickRight);

		actions.moveUp.AddDefaultBinding(InputControlType.DPadUp);
		actions.moveDown.AddDefaultBinding(InputControlType.DPadDown);
		actions.moveLeft.AddDefaultBinding(InputControlType.DPadLeft);
		actions.moveRight.AddDefaultBinding(InputControlType.DPadRight);

		//Aim
		actions.aimUp.AddDefaultBinding(InputControlType.RightStickUp);
		actions.aimDown.AddDefaultBinding(InputControlType.RightStickDown);
		actions.aimLeft.AddDefaultBinding(InputControlType.RightStickLeft);
		actions.aimRight.AddDefaultBinding(InputControlType.RightStickRight);


		//Actions
		actions.shoot.AddDefaultBinding(InputControlType.RightTrigger);

		return actions;
	}

}

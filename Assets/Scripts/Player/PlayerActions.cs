using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerActions : PlayerActionSet
{

	public PlayerAction left;
	public PlayerAction right;
	public PlayerAction up;
	public PlayerAction down;
	public PlayerAction shoot;
	public PlayerTwoAxisAction move;


	public PlayerActions()
	{
		shoot = CreatePlayerAction("Shoot");
		left = CreatePlayerAction("Left");
		right = CreatePlayerAction("Right");
		up = CreatePlayerAction("Up");
		down = CreatePlayerAction("Down");
		move = CreateTwoAxisPlayerAction(left, right, down, up);
	}

	public static PlayerActions CreateWithKeyboardBindings()
	{
		var actions = new PlayerActions();

		actions.up.AddDefaultBinding(Key.UpArrow);
		actions.down.AddDefaultBinding(Key.DownArrow);
		actions.left.AddDefaultBinding(Key.LeftArrow);
		actions.right.AddDefaultBinding(Key.RightArrow);
		actions.shoot.AddDefaultBinding(Key.Space); // we need to find the way to change this to left click button http://www.gallantgames.com/incontrol-api/html/class_in_control_1_1_mouse_binding_source.html this can help

		return actions;
	}


	public static PlayerActions CreateWithJoystickBindings()
	{
		var actions = new PlayerActions();

		actions.up.AddDefaultBinding(InputControlType.LeftStickUp);
		actions.down.AddDefaultBinding(InputControlType.LeftStickDown);
		actions.left.AddDefaultBinding(InputControlType.LeftStickLeft);
		actions.right.AddDefaultBinding(InputControlType.LeftStickRight);
		actions.shoot.AddDefaultBinding(InputControlType.RightTrigger);

		actions.up.AddDefaultBinding(InputControlType.DPadUp);
		actions.down.AddDefaultBinding(InputControlType.DPadDown);
		actions.left.AddDefaultBinding(InputControlType.DPadLeft);
		actions.right.AddDefaultBinding(InputControlType.DPadRight);

		return actions;
	}

}

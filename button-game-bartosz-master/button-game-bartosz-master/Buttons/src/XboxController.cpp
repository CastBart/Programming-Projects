#include "XboxController.h"
#include <iostream>


XboxController::XboxController()
{
	
}


XboxController::~XboxController()
{
}

/// <summary>
/// updates current and previous key presses
/// </summary>
void XboxController::update()
{
	currentPressed();
	previousPressed();
	analogStick(m_currentState.m_LeftThumbStick);
	dpadControll(m_currentState.m_DpadRight);
}

void XboxController::render(sf::RenderWindow &window)
{
	
}

/// <summary>
/// sets joystick index depending on what controller is connected
/// </summary>
/// <returns></returns>
bool XboxController::isConnected()
{

	if (sf::Joystick::isConnected(0))
	{
		sf_Joystick_index = 0;
		return true;
	}
	else
	{
		return false;
	}
	
}
/// <summary>
/// no idea what this should do #pete is cool
/// </summary>
/// <returns></returns>
bool XboxController::connect()
{
	return false;
}

/// <summary>
/// controll the bool is key is pressed
/// </summary>
/// <param name="button"></param>
/// <param name="index"></param>
void XboxController::buttonPressed(bool &button, int index)
{
	if (sf::Joystick::isButtonPressed(0, index))
	{
		button = true;
	}
	else
	{
		button = false;
	}
}

void XboxController::analogStick(sf::Vector2f& vector)
{
	vector.x = sf::Joystick::getAxisPosition(0, sf::Joystick::Axis::X);
	vector.y = sf::Joystick::getAxisPosition(0, sf::Joystick::Axis::Y);
}

void XboxController::dpadControll(float & dpad)
{
	dpad = sf::Joystick::getAxisPosition(0, sf::Joystick::Axis::PovX);
//	std::cout << dpad << std::endl;
}

/// <summary>
///  update current key
/// </summary>
void XboxController::currentPressed()
{
	buttonPressed(m_currentState.m_A, static_cast<int>(XboxController::JoystickIndex::A_BUTTON));
	buttonPressed(m_currentState.m_B, static_cast<int>(XboxController::JoystickIndex::B_BUTTON));
	buttonPressed(m_currentState.m_X, static_cast<int>(XboxController::JoystickIndex::X_BUTTON));
	buttonPressed(m_currentState.m_Y, static_cast<int>(XboxController::JoystickIndex::Y_BUTTON));
	buttonPressed(m_currentState.m_LB, static_cast<int>(XboxController::JoystickIndex::LEFT_BUTTON));
	buttonPressed(m_currentState.m_RB, static_cast<int>(XboxController::JoystickIndex::RIGHT_BUTTON));
	buttonPressed(m_currentState.m_Back, static_cast<int>(XboxController::JoystickIndex::BACK_BUTTON));
	buttonPressed(m_currentState.m_Start, static_cast<int>(XboxController::JoystickIndex::START_BUTTON));
	buttonPressed(m_currentState.m_LeftThumbStickClick, static_cast<int>(XboxController::JoystickIndex::LEFT_STICK_BUTTON));
	buttonPressed(m_currentState.m_RightThumbStickClick, static_cast<int>(XboxController::JoystickIndex::RIGHT_STICK_BUTTON));

}

/// <summary>
/// update previous key
/// </summary>
void XboxController::previousPressed()
{
	buttonPressed(m_previousState.m_A, static_cast<int>(XboxController::JoystickIndex::A_BUTTON));
	buttonPressed(m_previousState.m_B, static_cast<int>(XboxController::JoystickIndex::B_BUTTON));
	buttonPressed(m_previousState.m_X, static_cast<int>(XboxController::JoystickIndex::X_BUTTON));
	buttonPressed(m_previousState.m_Y, static_cast<int>(XboxController::JoystickIndex::Y_BUTTON));
	buttonPressed(m_previousState.m_LB, static_cast<int>(XboxController::JoystickIndex::LEFT_BUTTON));
	buttonPressed(m_previousState.m_RB, static_cast<int>(XboxController::JoystickIndex::RIGHT_BUTTON));
	buttonPressed(m_previousState.m_Back, static_cast<int>(XboxController::JoystickIndex::BACK_BUTTON));
	buttonPressed(m_previousState.m_Start, static_cast<int>(XboxController::JoystickIndex::START_BUTTON));
	buttonPressed(m_previousState.m_LeftThumbStickClick, static_cast<int>(XboxController::JoystickIndex::LEFT_STICK_BUTTON));
	buttonPressed(m_previousState.m_RightThumbStickClick, static_cast<int>(XboxController::JoystickIndex::RIGHT_STICK_BUTTON));

}





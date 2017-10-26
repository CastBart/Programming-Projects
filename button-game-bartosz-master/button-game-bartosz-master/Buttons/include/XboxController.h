#pragma once
#ifndef  XBOX360CONTROLLER
#define  XBOX360CONTROLLER


#include <SFML\Graphics.hpp>

struct GamePadState
{
	bool m_A = false;
	bool m_B = false;
	bool m_Y = false;
	bool m_X = false;
	bool m_LB = false;
	bool m_RB = false;
	bool m_LeftThumbStickClick = false;
	bool m_RightThumbStickClick = false;
	float m_DpadUp;
	float m_DpadDown;
	float m_DpadLeft;
	float m_DpadRight;
	bool m_Start = false;
	bool m_Back = false;
	bool m_Xbox = false;
	float m_RTriger;
	float m_LTriger;
	sf::Vector2f m_RightThumbStick;
	sf::Vector2f m_LeftThumbStick;
};



class XboxController
{

private:
	
	

public:
	static enum class JoystickIndex
	{
		A_BUTTON,
		B_BUTTON,
		X_BUTTON,
		Y_BUTTON,
		LEFT_BUTTON,
		RIGHT_BUTTON,
		BACK_BUTTON,
		START_BUTTON,
		LEFT_STICK_BUTTON,
		RIGHT_STICK_BUTTON,

	};
	int sf_Joystick_index;

	GamePadState m_currentState;

	GamePadState m_previousState;

	XboxController();
	~XboxController();
	void update();
	void render(sf::RenderWindow &window);
	bool isConnected();//see if cotroller is connected
	bool connect();//to be honest i have no idea why thats here #pete is cool
	void buttonPressed(bool &button, int index);
	void analogStick(sf::Vector2f& vector);
	void dpadControll(float & dpad);
	void currentPressed();//to update current press
	void previousPressed();//to update previous press



};
#endif // ! XBOX360CONTROLLER

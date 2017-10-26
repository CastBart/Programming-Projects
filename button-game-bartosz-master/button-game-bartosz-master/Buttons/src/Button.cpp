#include "Button.h"

Button::Button()
{
}

Button::Button(sf::Font font, std::string text, int textSize):
	Label(font, text, textSize)
{
	
	/* setting up the button rect */
	sf::Vector2f size(m_text.getLocalBounds().width + MARGIN, m_text.getLocalBounds().height + MARGIN);
	m_rect = sf::RectangleShape(size);

	/*setting the origin and the position*/
	sf::FloatRect buttonRect = m_rect.getLocalBounds();
	m_rect.setOrigin(buttonRect.width / 2, buttonRect.height / 2);
	m_rect.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));
	m_rect.setFillColor(sf::Color::Red);

	/* setting up the in focus rect */
	sf::Vector2f inFocusSize(m_rect.getLocalBounds().width + MARGIN, m_rect.getLocalBounds().height + MARGIN);
	m_inFocusRect = sf::RectangleShape(inFocusSize);

	/*setting the origin and position of focus rectangle*/
	sf::FloatRect inFocusButtonRect = m_inFocusRect.getLocalBounds();
	m_inFocusRect.setOrigin(inFocusButtonRect.width / 2, inFocusButtonRect.height / 2);
	m_inFocusRect.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));
	m_inFocusRect.setFillColor(sf::Color(0 ,250, 0, 100));
}

void Button::draw(sf::RenderTarget & target, sf::RenderStates state) const
{
	target.draw(m_rect);
	target.draw(m_text);
	if (focus)
	{
		target.draw(m_inFocusRect);
	}
}

/// <summary>
/// set the positions of all elements that belong to button
/// </summary>
/// <param name="rad"></param>
/// <param name="centre"></param>
void Button::setWidgetPosition(float rad, sf::CircleShape centre)
{
	float x = (centre.getRadius() + OFFSET_X)*cos(rad) + centre.getPosition().x;
	float y = (centre.getRadius() + OFFSET_Y)*sin(rad) + centre.getPosition().y;
	m_position.x = x;
	m_position.y = y;
	m_text.setPosition(m_position.x, m_position.y);
	m_rect.setPosition(m_position.x, m_position.y);
	m_inFocusRect.setPosition(m_position.x, m_position.y);
}

/// <summary>
/// processs the input of the button
/// </summary>
/// <param name="controller"></param>
/// <returns></returns>
bool Button::processInput(XboxController& controller)
{
	if (focus)
	{
		if (controller.m_currentState.m_A && controller.m_previousState.m_A)
		{
			return true;
		}
	}
	return false;
}

void Button::getFocus()
{
	focus = true;
}

void Button::loseFocus()
{
	focus = false;
}

Button::~Button()
{
}

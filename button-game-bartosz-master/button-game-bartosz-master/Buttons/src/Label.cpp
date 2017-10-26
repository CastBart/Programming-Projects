#include "Label.h"



Label::Label()
{

}


Label::Label(sf::Font font, std::string text, int textSize):
	m_font(font)
{
	//set up the  text with the font
	m_text.setFont(m_font);
	m_text.setColor(sf::Color::White);
	
	m_text.setString(text);
	m_text.setCharacterSize(textSize);

	//setting the starting position and the origin of the label to centre of the screen
	sf::FloatRect textRect = m_text.getLocalBounds();
	m_text.setOrigin(textRect.width / 2, textRect.height / 2);
	m_text.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));
	OFFSET_X = (textRect.width / 2) + (25 * 2);
	OFFSET_Y = (textRect.height / 2) + (25 * 2);
}

Label::~Label()
{

}

void Label::draw(sf::RenderTarget & target, sf::RenderStates state) const
{
	target.draw(m_text);
}

/// <summary>
/// set the position of the label
/// </summary>
/// <param name="rad"></param>
/// <param name="centre"></param>
void Label::setWidgetPosition(float rad, sf::CircleShape centre) 
{
	
	float x = (centre.getRadius() + OFFSET_X)*cos(rad) + centre.getPosition().x;
	float y = (centre.getRadius() + OFFSET_Y)*sin(rad) + centre.getPosition().y;
	m_position.x = x;
	m_position.y = y;
	m_text.setPosition(m_position.x, m_position.y);
}

bool Label::processInput(XboxController& controller)
{
	return false;
}

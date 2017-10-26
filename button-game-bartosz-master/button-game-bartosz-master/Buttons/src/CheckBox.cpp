#include "CheckBox.h"



CheckBox::CheckBox()
{
}

CheckBox::CheckBox(sf::Font font, std::string text, int textSize, sf::Texture checked, sf::Texture unchecked):
	Label(font, text, textSize),
	m_checkedTexture(checked),
	m_uncheckedTexture(unchecked),
	m_isChecked(true)
{
	/*set texture*/
	m_checkedTexture.setSmooth(true);
	m_uncheckedTexture.setSmooth(true);
	m_sprite.setTexture(m_checkedTexture);
	
	

	/*set origin and position to centre of the screen*/
	m_sprite.setOrigin(m_sprite.getLocalBounds().width / 2, m_sprite.getLocalBounds().height / 2);
	m_sprite.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));


	float textWidth = m_text.getLocalBounds().width;
	float spriteWidth = m_sprite.getLocalBounds().width;
	sf::Vector2f inFocusSize;
	/*CHECK IF TEXT WIDTH IS BIGGER OR CHECKBOX SPRITE*/
	if (textWidth >= spriteWidth)
	{
		//set size of the "in focus" rectangle that will be drawn then checkbox is in focus
		inFocusSize = sf::Vector2f(textWidth + MARGIN, m_text.getLocalBounds().height + MARGIN + m_sprite.getLocalBounds().height + (GAP_BETWEEN_BOX_AND_TEXT ));
		m_inFocusRect = sf::RectangleShape(inFocusSize);
	}
	else
	{//set size of the "in focus" rectangle that will be drawn then checkbox is in focus
		inFocusSize = sf::Vector2f(spriteWidth + MARGIN, m_text.getLocalBounds().height + MARGIN + m_sprite.getLocalBounds().height + (GAP_BETWEEN_BOX_AND_TEXT));
		m_inFocusRect = sf::RectangleShape(inFocusSize);
	}
	/*SETTING UP FOCUS RECTANGLE*/
	sf::FloatRect inFocusButtonRect = m_inFocusRect.getLocalBounds();
	m_inFocusRect.setOrigin(inFocusButtonRect.width / 2, (inFocusButtonRect.height / 2));
	m_inFocusRect.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));
	m_inFocusRect.setFillColor(sf::Color(0, 255, 0, 100));

}


CheckBox::~CheckBox()
{
}

void CheckBox::draw(sf::RenderTarget &target, sf::RenderStates states) const
{
	
	target.draw(m_sprite);
	target.draw(m_text);
	if (focus)
	{
		target.draw(m_inFocusRect);
	}
}

/// <summary>
/// set the position of each element that the check box contains
/// </summary>
/// <param name="rad"></param>
/// <param name="centre"></param>
void CheckBox::setWidgetPosition(float rad, sf::CircleShape centre)
{
	float x = (centre.getRadius() + OFFSET_X)*cos(rad) + centre.getPosition().x;
	float y = (centre.getRadius() + OFFSET_Y)*sin(rad) + centre.getPosition().y;
	m_position.x = x;
	m_position.y = y;

	//set the position of the text
	m_text.setPosition(m_position.x, m_position.y + (m_text.getLocalBounds().height/2) + (m_sprite.getLocalBounds().height/2) + GAP_BETWEEN_BOX_AND_TEXT);
	//set the postion of the sprite
	m_sprite.setPosition(m_position.x, m_position.y);
	//set the position of the focus rectangle
	m_inFocusRect.setPosition(m_position.x, m_position.y + (GAP_BETWEEN_BOX_AND_TEXT/2) + m_text.getLocalBounds().height/2);
}

/// <summary>
/// proccess input when checkbox is in focus
/// </summary>
/// <param name="controller"></param>
/// <returns></returns>
bool CheckBox::processInput(XboxController & controller)
{
	if (focus)
	{
		if (controller.m_currentState.m_A && controller.m_previousState.m_A)
		{
			
			if (m_isChecked)
			{
				m_sprite.setTexture(m_uncheckedTexture);
				m_isChecked = false;
			}
			else
			{
				m_sprite.setTexture(m_checkedTexture);
				m_isChecked = true;
			}
			return true;
		}
	}
	return false;
}

void CheckBox::getFocus()
{
	focus = true;
}

void CheckBox::loseFocus()
{
	focus = false;
}

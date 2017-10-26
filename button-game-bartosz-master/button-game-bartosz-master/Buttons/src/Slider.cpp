#include "Slider.h"

Slider::Slider()
{
}

/// <summary>
/// slider constructor which
/// sets up a slider how u want it
/// </summary>
/// <param name="font">for the text</param>
/// <param name="text">string</param>
/// <param name="textSize">size of te text</param>
/// <param name="lenght">lenght of the slider</param>
/// <param name="minValue">minimum value of the slider: default is 0</param>
/// <param name="maxValue">maximum value of the slider: default is 100</param>
Slider::Slider(sf::Font font, std::string text, int textSize, float lenght, float minValue, float maxValue) :
	Label(font, text, textSize),
	m_sliderText(text),
	m_barLenght(lenght),
	m_sliderLenght((lenght / 4.5)),
	m_value(maxValue),
	m_minValue(minValue),
	m_maxValue(maxValue)
{
	/*set the value to centre of the slider*/
	m_value = m_maxValue;

	/*set up the text for the value of the slider*/
	m_slideValueText.setFont(m_font);
	m_slideValueText.setColor(sf::Color::White);

	m_slideValueText.setString(std::to_string(static_cast<int>(m_value)));
	m_slideValueText.setCharacterSize(textSize);

	sf::FloatRect textRect = m_slideValueText.getLocalBounds();
	m_slideValueText.setOrigin(textRect.width / 2, textRect.height / 2);
	m_slideValueText.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));
	



	/*work out how much to add when moving the slider*/
	addValue = 100 / m_barLenght;
	addValue = ((m_maxValue - m_minValue) / m_barLenght);
	

	
	
	/*set the bar of the slider*/
	sf::Vector2f barSize(m_barLenght, m_barLenght / 8);
	m_bar = sf::RectangleShape(barSize);
	sf::FloatRect barRect = m_bar.getLocalBounds();
	m_bar.setOrigin(barRect.width / 2, barRect.height / 2);
	m_bar.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));
	m_bar.setFillColor(sf::Color::Magenta);

	/*set the slider size*/
	sf::Vector2f sliderSize(m_sliderLenght/3, m_sliderLenght);
	m_slider = sf::RectangleShape(sliderSize);
	sf::FloatRect sliderRect = m_slider.getLocalBounds();
	m_slider.setOrigin(sliderRect.width / 2, sliderRect.height / 2);
	m_slider.setPosition(m_bar.getPosition());
	m_slider.setFillColor(sf::Color::Green);

	float textWith = m_text.getLocalBounds().width;
	float barWidth = m_bar.getLocalBounds().width;
	sf::Vector2f inFocusSize;

	/*see which is bigger the text or bar and set the focus rectangle depending on which ine is bigger*/
	if (textWith >= barWidth)
	{
		inFocusSize = sf::Vector2f(textWith + MARGIN, m_text.getLocalBounds().height + MARGIN + m_bar.getLocalBounds().height + (GAP_BETWEEN_SLIDER_AND_TEXT*2) + m_slideValueText.getLocalBounds().height  );
		m_inFocusRect = sf::RectangleShape(inFocusSize);
	}
	else
	{
		inFocusSize = sf::Vector2f(barWidth + MARGIN, m_text.getLocalBounds().height + MARGIN + m_bar.getLocalBounds().height + (GAP_BETWEEN_SLIDER_AND_TEXT * 2) + m_slideValueText.getLocalBounds().height);
		m_inFocusRect = sf::RectangleShape(inFocusSize);
	}
	

	/*setting the origin and position of focus rectangle*/
	sf::FloatRect inFocusButtonRect = m_inFocusRect.getLocalBounds();
	m_inFocusRect.setOrigin(inFocusButtonRect.width / 2, (inFocusButtonRect.height / 2));
	m_inFocusRect.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2.0f, GUI::WINDOW_HIGHT / 2.0f));
	m_inFocusRect.setFillColor(sf::Color(0, 255, 0, 100));

	
}

Slider::~Slider()
{
}

/// <summary>
/// draw function
/// draws all esential parts of the slider to the screen
/// </summary>
/// <param name="target"></param>
/// <param name="state"></param>
void Slider::draw(sf::RenderTarget & target, sf::RenderStates state) const
{
	//std::cout << m_value << std::endl;
	target.draw(m_bar);
	target.draw(m_slider);
	target.draw(m_text);
	target.draw(m_slideValueText);
	if (focus)
	{
		target.draw(m_inFocusRect);
	}
}

/// <summary>
/// set up the positioning of the sliders elements around the circle 
/// </summary>
/// <param name="rad">spacing between widgets</param>
/// <param name="centre">cirlce object</param>
void Slider::setWidgetPosition(float rad, sf::CircleShape centre)
{
	float x = (centre.getRadius() + OFFSET_X)*cos(rad) + centre.getPosition().x;
	float y = (centre.getRadius() + OFFSET_Y)*sin(rad) + centre.getPosition().y;
	m_position.x = x;
	m_position.y = y;
	
	m_bar.setPosition(m_position.x, m_position.y);
	m_slider.setPosition(m_bar.getPosition().x + m_barLenght / 2, m_bar.getPosition().y);
	m_text.setPosition(m_position.x, m_position.y + (m_text.getLocalBounds().height/2) + GAP_BETWEEN_SLIDER_AND_TEXT + m_bar.getLocalBounds().height/2 );
	m_slideValueText.setPosition(m_position.x, m_position.y - (m_slideValueText.getLocalBounds().height / 2) - GAP_BETWEEN_SLIDER_AND_TEXT - m_bar.getLocalBounds().height / 2);
	m_inFocusRect.setPosition(m_position.x, m_position.y );
}

/// <summary>
/// process input of the slide
/// </summary>
/// <param name="controller"></param>
/// <returns></returns>
bool Slider::processInput(XboxController & controller)
{
	if (focus)
	{
		if (controller.m_currentState.m_DpadRight <= 100 && controller.m_currentState.m_DpadRight >= 50 && m_slider.getPosition().x < m_bar.getPosition().x+m_barLenght/2)
		{
			m_slider.setPosition( sf::Vector2f(m_slider.getPosition().x + 1, m_slider.getPosition().y) );
			m_value += addValue;
			
			m_slideValueText.setString(std::to_string(static_cast<int>(std::round(m_value))));
			return true;
		}
		else if (controller.m_currentState.m_DpadRight >= -100 && controller.m_currentState.m_DpadRight <= -50 && m_slider.getPosition().x > m_bar.getPosition().x - m_barLenght / 2)
		{
			m_slider.setPosition(sf::Vector2f(m_slider.getPosition().x - 1, m_slider.getPosition().y));
			m_value -= addValue;
			std::round(m_value);

			m_slideValueText.setString(std::to_string(static_cast<int>(std::round(m_value))));
			return true;
		}
		
	}
	return false;
}

void Slider::getFocus()
{
	focus = true;
}

void Slider::loseFocus()
{
	focus = false;
}

float Slider::getValue()
{
	return m_value;
}

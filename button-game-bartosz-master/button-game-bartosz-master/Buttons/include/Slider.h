#ifndef SLIDER
#define SLIDER

#include "Label.h"
#include <iostream>

class Slider : public Label
{
public:
	Slider();
	Slider(sf::Font, std::string text, int textSize, float lenght = 100, float minValue = 0, float maxValue = 100);
	~Slider();
	void draw(sf::RenderTarget&, sf::RenderStates) const override;
	void setWidgetPosition(float  deg, sf::CircleShape centre)  override;
	virtual bool processInput(XboxController& controller) override;
	void getFocus() override;
	void loseFocus() override;
	bool focus = false;
	bool pressed = false;
	virtual float getValue()override;
	

private:
	std::string m_sliderText;
	sf::Text m_slideValueText;
	sf::RectangleShape m_slider;
	sf::RectangleShape m_bar;
	sf::RectangleShape m_inFocusRect;
	const unsigned int MARGIN = 25;
	const int GAP_BETWEEN_SLIDER_AND_TEXT = 10;
	float m_barLenght;
	float m_sliderLenght;
	float m_value;
	float addValue;
	float m_minValue;
	float m_maxValue;

};

#endif // !SLIDER


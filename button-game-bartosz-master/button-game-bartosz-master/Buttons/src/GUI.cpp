#include "GUI.h"

GUI::GUI()
{
	centre = sf::CircleShape(120.0f);
	centre.setFillColor(sf::Color::Blue);
	centre.setOrigin(centre.getGlobalBounds().width / 2, centre.getGlobalBounds().height / 2);
	centre.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2, GUI::WINDOW_HIGHT / 2));

	

	
}

GUI::GUI(XboxController & controller) :
	m_controller(controller)
{
	centre = sf::CircleShape(120.0f);
	centre.setFillColor(sf::Color::Blue);
	centre.setOrigin(centre.getGlobalBounds().width / 2, centre.getGlobalBounds().height / 2);
	centre.setPosition(sf::Vector2f(GUI::WINDOW_WIDTH / 2, GUI::WINDOW_HIGHT / 2));

	
}

void GUI::addWidget(Widget * widget)
{
	m_widgetHolder.push_back(&(*widget));
}

void GUI::draw(sf::RenderTarget & target, sf::RenderStates states) const
{
	target.draw(centre);
	for (auto &widget : m_widgetHolder)
	{
		widget->draw(target, states);
	}
}

void GUI::update(sf::Time const & dt)
{
	setFocusOfWidgets();
	loseFocusOfWidgets();
	m_controller.update();
}

void GUI::setPositionOfWidgets()
{
	widNum = m_widgetHolder.size();
	currentFocus = widNum + 1;

	const float SEPARATION_SIZE = 360 / widNum;

	const float RADIANS_SEPARATION = SEPARATION_SIZE * acos(-1) / 180;
	
	for (int i = 0; i < m_widgetHolder.size(); i++)
	{
		m_widgetHolder[i]->setWidgetPosition(RADIANS_SEPARATION * i, centre);
	}
	
}

void GUI::setFocusOfWidgets()
{
	for (int i = 0; i < m_widgetHolder.size(); i++)
	{
		if (i == currentFocus)
		{
			m_widgetHolder[i]->getFocus();
		}
	}
}

void GUI::loseFocusOfWidgets()
{

	
	for (int i = 0; i < m_widgetHolder.size(); i++)
	{
		if (i != currentFocus)
		{
			m_widgetHolder[i]->loseFocus();
		}
	}
}


std::vector<Widget*> GUI::getWidgetHolder() const
{
	return m_widgetHolder;
}

GUI::~GUI()
{

}

#include "Widget.h"


Widget::Widget(Widget * parent) :
	m_parent(parent)
{

}



bool Widget::processInput(XboxController& controller)
{
	return false;
}

void Widget::getFocus()
{
	m_parent->getFocus();
}

void Widget::loseFocus()
{
	m_parent->loseFocus();
}

void Widget::setWidgetPosition(float deg, sf::CircleShape centre)
{
	m_parent->setWidgetPosition(deg, centre);
}

void Widget::update(sf::Time dt)
{

}

float Widget::getValue()
{
	return 0.0f;
}

Widget::~Widget()
{

}

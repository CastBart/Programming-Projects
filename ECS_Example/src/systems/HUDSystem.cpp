#include "systems\HUDSystem.h"



HUDSystem::HUDSystem(sf::RenderWindow & window, const std::shared_ptr<sf::Font>& font)
	: m_window(window)
	, m_font(font)
	, m_timer(0)
{
	m_pointText.setFont(*m_font);
	m_pointText.setCharacterSize(20);
	m_pointText.setColor(sf::Color::Red);
	m_pointText.setString(std::to_string(0));

	m_timerText.setFont(*m_font);
	m_timerText.setCharacterSize(50);
	m_timerText.setColor(sf::Color::Red);
	m_timerText.setPosition(sf::Vector2f(100, 100));
	m_timerText.setString(std::to_string(0));
}

/// <summary>
/// update the hud system
/// </summary>
/// <param name="entities"></param>
/// <param name="events"></param>
/// <param name="dt"></param>
void HUDSystem::update(entityx::EntityManager & entities, entityx::EventManager & events, double dt)
{

	
	m_window.draw(m_pointText);

	//lap time incrementing
	m_elapsed = m_clock.restart();
	m_accumulatedTime += m_elapsed.asSeconds();
	if (m_accumulatedTime > 1.0)
	{
		m_accumulatedTime = 0;
		m_timer++;

	}
	
	m_timerText.setString(std::to_string(m_timer));

	
	m_window.draw(m_timerText);
}

/// <summary>
/// configure method that subsribes an event to the hud system
/// </summary>
/// <param name="events"></param>
void HUDSystem::configure(entityx::EventManager& events)
{
	events.subscribe<EvReportNextPoint>(*this);
}

/// <summary>
/// reciver method for hud system
/// </summary>
/// <param name="e"></param>
void HUDSystem::receive(const EvReportNextPoint & e)
{
	m_currentPathPoint = e.m_nextPoint;/*sets current pathpoint to the next path point that will be emitted by the tank*/
	m_pathPosition = e.m_position;/*sets current position to the next path point's position that will be emitted by the tank*/
	m_pointText.setPosition(m_pathPosition);
	m_pointText.setString(std::to_string(m_currentPathPoint+1));
	if (m_currentPathPoint == 0)
	{
		m_timer = 0;
	}
}


HUDSystem::~HUDSystem()
{
}

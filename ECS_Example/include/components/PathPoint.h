#pragma once

#include "entityx\Entity.h"
#include <SFML/Graphics.hpp>


struct  PathPoint : public entityx::Component<PathPoint>
{
	PathPoint();
	PathPoint(float radius);
	float m_radius;
};
#pragma once
#include "Quaternion.h"


Quaternion::Quaternion():
	m_w(0),
	m_x(0),
	m_y(0),
	m_z(0)
{
}

Quaternion::Quaternion(double w, double x, double y, double z):
	m_w(w),
	m_x(x),
	m_y(y),
	m_z(z)
{
}

Quaternion::Quaternion(float w, float x , float y, float z):
	m_w(w),
	m_x(x),
	m_y(y),
	m_z(z)
{
}

Quaternion::Quaternion(Vector3 &V) :
	m_w(0),
	m_x(V.M_X()),
	m_y(V.M_Y()),
	m_z(V.M_Z())
{

}

double Quaternion::length()
{
	return sqrt((m_w * m_w) + (m_x * m_x) + (m_y * m_y) + (m_z * m_z));
}

double Quaternion::lengthSquared()
{
	return ((m_w * m_w) + (m_x * m_x) + (m_y * m_y) + (m_z * m_z));
}

Quaternion operator*(const Quaternion &leftSide, const double &s)
{
	return Quaternion(
		leftSide.M_W() * s,
		leftSide.M_X() * s,
		leftSide.M_Y() * s,
		leftSide.M_Z() * s
	);
}

Quaternion Quaternion::conjugate()
{
	return Quaternion(m_w, -m_x, -m_y, -m_z);
}

Quaternion Quaternion::inverse()
{
	return Quaternion( conjugate()  * (1/ lengthSquared()) );
}

Quaternion Quaternion::normalise()
{
	Quaternion q(m_w, m_x, m_y, m_z);
	return Quaternion( q * (1/length()) );
}

Vector3 Quaternion::Rotate(Vector3 &thisVector, const float &angle)
{
    
	float angleRads = (float)(angle *(acos(-1) / 180));
	Quaternion Q1 = normalise();
	Quaternion Q2 = Quaternion(static_cast<float>(cos(angleRads / 2)), static_cast<float>(sin(angleRads / 2) * Q1.M_X()), static_cast<float>(sin(angleRads / 2) * Q1.M_Y()), static_cast<float>(sin(angleRads / 2) * Q1.M_Z()));
	Quaternion Q3 = Q2.inverse();
	Quaternion Q4 = Quaternion(thisVector);
	Q4 = Q2 * Q4 * Q3;

	return Q4.convertToVector();
}

Vector3 Quaternion::convertToVector()
{
	return Vector3(m_x, m_y, m_z);
}

double Quaternion::M_W() const
{
	return m_w;
}

double Quaternion::M_X() const
{
	return m_x;
}

double Quaternion::M_Y() const
{
	return m_y;
}

double Quaternion::M_Z() const
{
	return m_z;
}


Quaternion::~Quaternion()
{
}

Quaternion operator+(const Quaternion &leftSide, const Quaternion &rightSide)
{
	return Quaternion(
		leftSide.M_W() + rightSide.M_W(),
		leftSide.M_X() + rightSide.M_X(),
		leftSide.M_Y() + rightSide.M_Y(),
		leftSide.M_Z() + rightSide.M_Z()
	);
}

Quaternion operator-(const Quaternion &Q)
{
	return Quaternion(-Q.m_w, -Q.m_x, -Q.m_y, -Q.m_z);
}

Quaternion operator*(const Quaternion &leftSide, const Quaternion &rightSide)
{
	return Quaternion(
		(leftSide.M_W() * rightSide.M_W()) - (leftSide.M_X() * rightSide.M_X()) - (leftSide.M_Y() * rightSide.M_Y()) - (leftSide.M_Z() * rightSide.M_Z()),
		(leftSide.M_W() * rightSide.M_X()) + (leftSide.M_X() * rightSide.M_W()) + (leftSide.M_Y() * rightSide.M_Z()) - (leftSide.M_Z() * rightSide.M_Y()),
		(leftSide.M_W() * rightSide.M_Y()) + (leftSide.M_Y() * rightSide.M_W()) + (leftSide.M_Z() * rightSide.M_X()) - (leftSide.M_X() * rightSide.M_Z()),
		(leftSide.M_W() * rightSide.M_Z()) + (leftSide.M_Z() * rightSide.M_W()) + (leftSide.M_X() * rightSide.M_Y()) - (leftSide.M_Y() * rightSide.M_X())
	);
}



Quaternion operator*(const Quaternion &leftSide, const float &f)
{
	return leftSide * static_cast<float>(f);
}

Quaternion operator-(const Quaternion &leftSide, const Quaternion &rightSide)
{
	return Quaternion(
		leftSide.M_W() - rightSide.M_W(),
		leftSide.M_X() - rightSide.M_X(),
		leftSide.M_Y() - rightSide.M_Y(),
		leftSide.M_Z() - rightSide.M_Z()
	);
}

#pragma once
#include<Windows.h>
#include"object.h"
class SNACK
{
	
	int flag;
	int size;
	int dx;
	int dy;
	COORD head;
	COORD next;
public:
	SNACK *s;
	COORD *snack;
	int H;
	int W;
	int Move();
	SNACK();
	~SNACK();
	void Right();
	void Left();
	void Up();
	void Down();
	int Getsize();
	void BigSnack();
	

};
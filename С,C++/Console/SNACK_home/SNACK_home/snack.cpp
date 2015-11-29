#include"snack.h"
#include <iostream>
using namespace std;
SNACK::SNACK()
{
	flag=1;
	H=20;
	W=60;
	size=3;
	dx=1;
	dy=0;
	snack=new COORD[size];
	for(int i=0;i<size;i++)
	{
		snack[i].X=i+1;
		snack[i].Y=8;
	}
	head=snack[size-1];
	next.X=head.X+dx;
	next.Y=head.Y+dy;
}
SNACK::~SNACK()
{
	delete [] snack;
}
void SNACK::BigSnack()
{
	size++;
	COORD *tmp=new COORD[size];
	for(int i=0; i<size-1; i++)
		tmp[i]=snack[i];
	tmp[size-1]=next;
	next.X=head.X+dx;
	next.Y=head.Y+dy;
	delete []snack;
	snack=tmp;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE),head);
	cout<<"*";
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE),next);
	cout<<"@";
}
int SNACK:: Move()
{
	static OBJECT bonus;
	if(flag)
	{
		bonus.GetBonus();
		flag*=0;
	}
	
	if(next.X==bonus.xy.X && next.Y==bonus.xy.Y)
	{
		BigSnack();
		bonus.GetBonus();
		return 1;
	}
	if(size>4)
	{
		for(int i=1; i<size-2; i++)
			if(next.X==snack[i].X && next.Y==snack[i].Y)
				return 0;
	}

	if(next.X<1||next.X>58||next.Y<4||next.Y>21)
		return 0;

	for(int i=0; i<size-1; i++)
		snack[i]=snack[i+1];
	snack[size-1]=next;
	head=snack[size-1];
	next.X=head.X+dx;
	next.Y=head.Y+dy;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE),snack[0]);
	cout<<" ";

	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), head);
	cout<<"*";
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE),next);
	cout<<"@";
	return 3;

}
void SNACK::Down()
{
	dy++;
	dx=0;
}
void SNACK::Up()
{
	dy--;
	dx=0;

}
void SNACK::Left()
{
	dx--;
	dy=0;
}
void SNACK::Right()
{
	dx++;
	dy=0;
}
int SNACK::Getsize()
{
	return size;
}
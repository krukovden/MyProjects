#include"snack.h"
#include<iostream>
#include<time.h>
#include<conio.h>
#include "object.h"

using namespace std;
void main()
{
	srand(time(0));
	int vub=0;
	int tmp;
	SNACK s;
	OBJECT b;
	b.DrawStena();
	do
	{

	while(!kbhit())
	{
		if(!(s.Move()))
		{
				system("cls");
				cout<<"\n\n\n\n\n\n\n\n\n \t\t\t\tGame Over";
		}
		Sleep(100);
	}
		vub=getch();
		if(vub==224)
		{
			vub=getch();
			switch(vub)
			{
			case 72:
				s.Up();
				break;
			case 80:
				s.Down();
				break;
			case 75:
				s.Left();
				break;
			case 77:
				s.Right();
				break;


			}
			
		}
	
	}while(vub!=27);
}
#include"object.h"
#include<iostream>
using namespace std;
OBJECT::OBJECT()
{
	xy.X=0;
	xy.Y=0;


}
void OBJECT::GetBonus()
{
	int i;
	/*do     хотел организовать проверку на то, чтоб яблоко не упало на змею, но не смог правильно засунуть параметр на вход ф-ции 
	{*/
		SNACK s;
		xy.X=(rand()%s.W)<2? 4:(rand()%s.W);
		xy.Y=(rand()%s.H)<4? 5:(rand()%s.H);
		/*for(i=0; i<s->Getsize(); i++)
			if(xy.X==s.snack[i].X && xy.Y==s.snack[i].Y)
				break;
	}while(i<s.Getsize());*/

	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE),xy);
	cout<<"0";

}
void OBJECT::DrawStena()
{
	SNACK s;
	COORD start={0,3};
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE),start);
	for(int i=0;i<s.H; i++)
	{
		for(int j=0; j<s.W;j++)
			if(i==0|| j==0 ||i==s.H-1||j==s.W-1)
				cout<<"&";
			else
				cout<<" ";
		cout<<endl;
	}
	

}

#include "kubik_rubik.h"
#include <iostream>
#include<conio.h>
#include<time.h>
using namespace std;
void main()
{
	cout<<"Turn size of the mas :";
	int size;
	cin>>size;
	int **mas=new int*[size];
	int **copy=new int*[size];
	int a=0;
	int n=0;
	srand(time(0));
	for(int i=0; i<size; i++)
	{ 
		mas[i]=new int[size];
		copy[i]=new int [size];
		for(int j=0; j<size; j++)
		{
			mas[i][j]=a++;
			copy[i][j]=mas[i][j];
		}
	}

	int cursorver;
	int cursorgor;
	int regum=1;
	int otvet=rand()%size;

	void (*P[4])(int**,int, int&)={PRAVO,LEVO,UP,DOWN};

	for(int i=0; i<otvet; i++)
	{
		int cursorver=rand()%size;
		int cursorgor=rand()%size;
		P[rand()%4](mas,size,cursorver);
		P[rand()%4](mas,size,cursorgor);

		PRINTmark(size,cursorgor,regum);
		PRINTmas(mas, size, cursorver,regum);
	}



	int mark;
	do {
		int regum=1;
		PRINTmark(size,cursorgor,regum);
		PRINTmas(mas, size, cursorver,regum);
		mark=getch();
		if(mark==224)
		{
			mark=getch();
			switch(mark)
			{
			case 77:
				cursorgor++;
				break;

			case 75:
				cursorgor--;
				break;

			case 72:
				cursorver--;
				break;

			case 80:
				cursorver++;
				break;

			}
			PRINTmark(size,cursorgor,regum);
			PRINTmas(mas, size, cursorver,regum);
		}

		else 
		{ 
			if(mark==32)
		{ 
			n++;
			regum*=0;
			PRINTmark(size,cursorgor,regum);
			PRINTmas(mas, size, cursorver,regum);
			int out;
			do
			{	out=getch();							
			if(out==224)
			{
				out=getch();
				switch(out)
				{ 
				case 77:
					PRAVO (mas, size,cursorver);
					break;
				case 75:
					LEVO (mas,size,cursorver);
					break;
				case 80:
					DOWN (mas, size,cursorgor);
					break;
				case 72:
					UP (mas,size,cursorgor);
					break;
				}
			}
			PRINTmark(size,cursorgor,regum);
			PRINTmas(mas, size, cursorver,regum);

			}while(out!=32);
		}
			if(mark==49)
			{
				if(!COMPER(mas,copy,size, n,otvet))
				{
					system("cls");
					cout<<"You are loser";
					getch();
				}
				else
				{
					system("cls");
					cout<<"You are Winner!!";
					getch();
				}
			}
		}
	}while(mark!=27);

	_getch();}


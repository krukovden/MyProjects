#include "kubik_rubik.h"
#include <iostream>
#include <conio.h>
#include <iomanip>
using namespace std;

void PRINTmas(int**mas, int size,int & cursorver, int& regum)
{
	
	if(cursorver==size)
         cursorver=0;
     else
         if(cursorver<0)
             cursorver=size-1;
//system("cls");
	for(int i=0; i<size; i++)
		{
			if(i==cursorver)
			{ 
				if(regum==1)
				cout<<(char)16;
				else
			    cout<<(char)177;
			}
			else
				cout<<" ";
			for(int j=0; j<size; j++)
				cout<<setw(5)<<mas[i][j];
			cout<<endl;
	}
			
    
}
void PRINTmark( int size,int & cursorgor, int& regum)
{
		if(cursorgor==size)
         cursorgor=0;
     else
         if(cursorgor<0)
             cursorgor=size-1;
		
system("cls");	
for(int i=0; i<=cursorgor; i++)
		cout<<setw(5)<<" ";
if(regum==1)
cout<<(char)16<<endl;
else
cout<<(char)177<<endl;
}

void LEVO (int**mas, int size,int & cursorver)
{ 
	int tmp1=mas[cursorver][size-1],tmp2=mas[cursorver][size-2];
	mas[cursorver][size-1]=mas[cursorver][0];
	for(int i=size-2; i>=0; i--)
		{
			mas[cursorver][i]=tmp1;
			tmp1=tmp2;
			tmp2=mas[cursorver][i-1];
	}
}

void PRAVO (int**mas, int size,int & cursorver)
{ 
	int tmp1=mas[cursorver][0],tmp2=mas[cursorver][1];
	mas[cursorver][0]=mas[cursorver][size-1];
	for(int i=1; i<size; i++)
		{
			mas[cursorver][i]=tmp1;
			tmp1=tmp2;
			tmp2=mas[cursorver][i+1];
	}
}

void DOWN (int**mas, int size,int & cursorgor)
{ 
	int tmp1=mas[size-1][cursorgor],tmp2=mas[size-2][cursorgor];
	mas[size-1][cursorgor]=mas[0][cursorgor];
	for(int i=size-2; i>=0; i--)
		{
			mas[i][cursorgor]=tmp1;
			tmp1=tmp2;
			if(i>0)
			tmp2=mas[i-1][cursorgor];
	}
}	
void UP (int**mas, int size,int & cursorgor)
{ 
	int tmp1=mas[0][cursorgor],tmp2=mas[1][cursorgor];
	mas[0][cursorgor]=mas[size-1][cursorgor];
	for(int i=1; i<size; i++)
		{
			mas[i][cursorgor]=tmp1;
			tmp1=tmp2;
			if(i<size-1)
			tmp2=mas[i+1][cursorgor];
	}
}
bool COMPER(int **mas, int **copy, int size, int n,int otvet)
{
	if(otvet*2>=n)
	{
		for(int i=0; i<size;i++)
		{
			for(int j=0;j<size;j++)
			{
				if (mas[i][j]!=copy[i][j])
					return 0;
			}
		}
		return 1;
		}
	else
		return 0;
}
#include "chernovik.h"
#include <iostream>
#include<conio.h>
using namespace std;
void main(int argc, char* argv[])
{
	char * fileName = "test.txt";
	if (argc > 1)
	{
		fileName=new char[strlen(argv[1])+1];
		strcpy(fileName,argv[1]);
	}
	else
	{
		cout<<"Give puth and name of file(example: D:\\\\test.txt) \n or \nturn turn \"no\"--default value current location of exe-file and name test.txt\n";
		char name[256];
		cin >> name;		
		if(strlen(name)>4)
		{
			fileName=new char[strlen(name)+1];
			strcpy(fileName,name);
		}
	}
	cout <<"\nUse button \"space\" to select\n";
	getch();
	int cursor=0;

	int vub;

	char menu[][35]={" 1. Print file on display", " 2. Write in file"," 3. Change file", " 4. Sort file"," 5. Search the same"," 6. Delete str"," 0. Exit"};

	PRINTmenu(menu,7,cursor);

	do
	{
		//char menu[][35]={" 1. Print file on display", " 2. Write in file"," 3. Change file", " 4. Sort file"," 5. Search the same"," 6. Delete str"," 0. Exit"}; 

		vub=getch();
		if(vub==224)
		{
			vub=getch();
			switch (vub)
			{
			case 72:
				cursor--;
				break;
			case 80:
				cursor++;
				break;
			}
			PRINTmenu(menu,7,cursor);
		}
		else
			if(vub==32)
			{	
				switch (cursor)
				{
				case 0:
											PrintFile(fileName,false);
						
					
					break;
				case 1:
					
						WriteFile(fileName);
						
					break;
				case 2:
					
						FileStringsToArray(fileName);
						
					break;
				case 3:
					
						do
						{
							system("cls");
							cout<<"\n If you want sort from max turn--1\n";
							cout<<"\n If you want sort from min turn--2\n";
							cout<<"\n If you want exit turn--0\n";
							cin>>vub;
							system("cls");
							switch(vub)
							{
							case 1:
								{
									Sort(fileName,MIN);
									getch();
								}
								break;
							case 2:
								{
									Sort(fileName,MAX);
									getch();
								}
								break;
							}
						}while(vub!=0);
						PRINTmenu(menu,7,cursor);
					
					break;
				case 4:
					
						Dublicat(fileName);
						
					
					break;
				case 5:
					
						DeleteStr(fileName);						
					
					break;
				case 6:
					vub=27;
					break;
				}		
			}
	}while(vub!=27);


}

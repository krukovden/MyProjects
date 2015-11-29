#include <iostream>
#include<conio.h>
#include <iostream>
using namespace std;
void PrintFile(char * fileName, bool withNumberString)
{

	char load_string[256] = "";
	FILE * file = fopen(fileName, "r");
	int t=1;
	if(file)
	{
		while(fgets(load_string,sizeof(load_string), file))
		{

			if(withNumberString)
				cout<<(t++)<<") ";
			cout<<load_string<<endl;
		}
	}
	else
	{
		cout<<"File not found"<<endl;
		return;
	}

	fclose(file);

}
void PRINTmenu(char menu[][35],int size, int &cursor)
{
	if(cursor==size)
		cursor=0;
	else
		if(cursor<0)
			cursor=size-1;

	system("cls");

	for(int i=0; i<size; i++)
	{
		if(cursor==i)
			cout<<(char)16;
		cout<<" ";
		cout<<menu[i]<<endl;

	}}
void WriteFile(char * fileName)
{
	FILE * file = fopen(fileName, "a");
	char a[256];
	cout << "Write data:\n";
	cin >> a;
	cout << endl;	
	fputs(a, file);	
	fputs("\n", file);
	fclose(file);
}
int StringCount(char * fileName)
{
	char load_string[256] = "";
	int n = 0;
	FILE * file = fopen(fileName, "r");
	if(file)
	{
		for(;fgets(load_string, 50, file); n++);
	}
	else
	{
		cout<<"File not found"<<endl;
		return 0;
	}

	fclose(file);

	return n;
}
void FileStringsToArray(char * fileName)
{
	int stringCount = StringCount(fileName);
	
	int m;

	char load_string[256] = "";
	FILE * file = fopen(fileName, "r");

	char **p = new char*[stringCount+1];

	if(file)
	{
		for(int i = 0; i < stringCount; i++)
		{
			fgets(load_string, sizeof(load_string), file);
			m = strlen(load_string);
			p[i] = new char [m + 1];
			strcpy(p[i], strrev(load_string));
		}
	}
	else
	{
		cout<<"File not open";
		return;
	}

	fclose(file);
	for(int i = 0; i < stringCount; ++i)
	{
		cout << p[i] << endl;
	}

	char tmp[256];
	cout<<"Add str: ";
	cin>>tmp;

	p[stringCount]=new char[strlen(tmp)+1];

	strcpy(p[stringCount],tmp);

	file = fopen(fileName, "a");

	for(int i = 0; i < stringCount+1; i++)
	{
		if(i==stringCount)
			fputs("\n", file);
		fputs(p[i], file);	
	}
	fclose(file);

	for(int i = 0; i < stringCount+1; ++i)
	{
		delete[] p[i];
	}

	delete[] p;
}
void Sort(char * fileName,bool (*Predicat)(int a, int b))
{
	int stringCount = StringCount(fileName);
	int m;

	char load_string[256] = "";
	FILE * file = fopen(fileName, "r");

	char **p = new char*[stringCount];
	if(file)
	{
		for(int i = 0; i < stringCount; i++)
		{
			fgets(load_string, sizeof(load_string), file);
			m= strlen(load_string);
			p[i] = new char [m+1];
			strcpy(p[i], load_string);
		}


	}
	else
	{
		cout<<"File not open";
		return;
	}

	fclose(file);
	int i,j;
	for( i = 0; i < stringCount; i++)
	{
		for( j=stringCount-1; j>i; j--)
		{
			int m=strlen(p[j-1]);
			char *tmp=new char[m+1];
			if(Predicat(strlen(p[j-1]),strlen(p[j])))
			{
				tmp=p[j-1];
				p[j-1]=p[j];
				p[j]=tmp;	
			}
		}

	}




	for(int i = 0; i < stringCount; ++i)
	{
		cout << p[i] << endl;
	}


	for(int i = 0; i < stringCount; ++i)
	{
		delete[] p[i];
	}

	delete[] p;

}
bool MAX(int a, int b)
{return a<b;}
bool MIN(int a, int b)
{return a>b;}
void Dublicat(char * fileName)
{
	int stringCount = StringCount(fileName);
	int m;

	char load_string[256] = "";
	FILE * file = fopen(fileName, "r");

	char **p = new char*[stringCount];

	if(file)
	{
		for(int i = 0; i < stringCount; i++)
		{
			fgets(load_string, sizeof(load_string), file);
			m = strlen(load_string);
			p[i] = new char [m + 1];
			strcpy(p[i], load_string);
		}
	}
	else
	{
		cout<<"File not open";
		return;
	}

	fclose(file);
	int n=1;
	int i,j;
	for( i=0; i < stringCount; i++)
	{
		for( j=stringCount-1; j>i; j--)
		{
			if (!(strcmp(p[i],p[j])))
			{
				cout<<n++<<". "<<"Str number "<<i<<"--"<<p[i]<<"Str number "<<j<<"--"<<p[j]<<endl;
			}
		}
	}
	if(n==1)
		cout<<"The same str not find";
	for(int i = 0; i < stringCount; ++i)
	{
		delete[] p[i];
	}
	delete [] p;
}
void DeleteStr(char * fileName)
{
	int stringCount = StringCount(fileName);
	if(stringCount==0)
	{
		cout<<"File  is empty"; return;
	}
	int m;
	char load_string[256] = "";

	FILE * file = fopen(fileName, "r");

	char **p = new char*[stringCount];

	if(file)
		for(int i = 0; i < stringCount; i++)
		{
			fgets(load_string, sizeof(load_string), file);
			m = strlen(load_string);
			p[i] = new char [m + 1];
			strcpy(p[i], load_string);
		}	
	else
	{
		cout<<"File not open";
		return;
	}
	fclose(file);

	PrintFile(fileName,true);

	cout<<"\n Please, turn number of str which do you want delete :";

	int del;

	cin>>del;

	if(del>0)
	{
		system("cls");

		char **newMas=new char*[stringCount-1];

		for	(int i=0, y=0; i<stringCount; i++)
		{
			if(i==del-1)
				continue;
			m=strlen(p[i]);
			newMas[y]=new char[m+1];
			newMas[y]=p[i];
			y++;
		}	

		file = fopen(fileName, "w");
		if(file)
			for(int i=0; i<stringCount-1; i++)
				fputs(newMas[i], file);	
		else 
			cout<<"Cannot write!!";

		fclose(file);


		for(int i = 0; i < stringCount-1; ++i)
			delete[] newMas[i];
		delete[] newMas;

	}

	for(int i = 0; i < stringCount; ++i)
		delete[] p[i];
	delete[] p;

	PrintFile(fileName,true);

}

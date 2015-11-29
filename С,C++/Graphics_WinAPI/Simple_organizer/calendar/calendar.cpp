#include<Windows.h>
#include"resource.h"
#include<fstream>
#include<string>
#include<conio.h>
#include <vector>
#pragma comment(lib,"comctl32.lib")
#include<CommCtrl.h>
#include<ctime>
#include<tchar.h>
using namespace std;
#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' \processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
HWND hCalendar, hStop, hPicture,hDIALOG2,hNAME,hABOUT,hTIME;
static HWND hMAINform;
int month; 
MCGRIDINFO q;
static _SYSTEMTIME sys;
static _SYSTEMTIME sys1; //for time now
tm newdata;

struct DATAch
{
	CHAR name[100];
	CHAR eventAbout[100];
	CHAR data[100];
	time_t second;
};

static vector <DATAch> ALLch;
DATAch tmpCH;
static WCHAR namefile[50];

BOOL CALLBACK DlgProc(HWND, UINT, WPARAM, LPARAM); 
BOOL CALLBACK	DIALOG(HWND, UINT, WPARAM, LPARAM);

bool sysTOtm(tm &tmp,_SYSTEMTIME sys);
DATAch SetDATAch(CHAR* name, CHAR* aboutNAME, int second);
void FindSECOND(WCHAR *str4,vector <DATAch> &ALLch);
void DELETEdata(vector <DATAch> &ALLch);

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst,  
	LPSTR lpszCmdLine, int nCmdShow) 
{ 
	InitCommonControls();   
	/*HANDLE hMutex = CreateMutex(0,TRUE,_T("Mutex!")); 
   if(GetLastError() == ERROR_ALREADY_EXISTS) 
  { 
    CloseHandle(hMutex); 
	ShowWindow(hMAINform,1);
    return -1; 
  }
	hMAINform=GetDlgItem(0, IDD_DIALOG1);*/
	return DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), NULL, DlgProc);  
} 

BOOL CALLBACK DlgProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) 
{ 
	WCHAR str[50];
	CHAR dataONdis[100]={0};
	int t=0;
	switch(message) 
	{ 
	case WM_TIMER:
				
		t=time(0);
		if(ALLch.size()>0)
			{
 		for( int i=0; i<ALLch.size()-1;i++)
			
			if(t>=ALLch[i].second)
			{
				strcat(dataONdis,"Today ");
				strcat(dataONdis,ALLch[i].name);
				strcat(dataONdis," this is about ");
				strcat(dataONdis,ALLch[i].eventAbout);
				
				ALLch.erase(ALLch.begin()+i);
				DELETEdata(ALLch);
				MessageBoxA(hWnd,(LPCSTR) dataONdis,"CONGRATULATION",MB_OK);
			}		
			}
		break;
	case WM_NOTIFY:
		switch(((LPNMHDR) lParam)->code)
		{
		case MCN_SELECT:
			SendMessage(hCalendar,MCM_GETCURSEL,0,(LPARAM)&sys);
			DialogBox(0,MAKEINTRESOURCE(IDD_DIALOG2),hWnd,DIALOG);
			break;
		}
		
		break;
	case WM_CLOSE: 
		EndDialog(hWnd, 0); 
		//ShowWindow(hWnd,0);

		return TRUE; 

	case WM_INITDIALOG: 
		hCalendar = GetDlgItem(hWnd, IDC_MONTHCALENDAR1);
		wsprintf(namefile,TEXT("H:\\CALENDAR\\%d.txt"),SendMessage(hCalendar,MCM_GETMONTHRANGE,GMR_VISIBLE|GMR_DAYSTATE,0)+1);
		FindSECOND(namefile,ALLch);
		SetTimer(hWnd,1,1000,0);
		return TRUE; 

}      
	return FALSE; 

}
BOOL CALLBACK DIALOG (HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	WCHAR str4[100];
		
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_CLOSE:
		EndDialog(hDlg,0);
		break;
	case WM_INITDIALOG:
		hNAME=GetDlgItem(hDlg,IDC_EDIT1);
		hABOUT=GetDlgItem(hDlg,IDC_EDIT2);
		hTIME=GetDlgItem(hDlg,IDC_DATETIMEPICKER1);

		return TRUE;

	case WM_COMMAND:
		switch(wParam)
		{
		case IDOK:
			
			GetWindowTextA(hNAME,tmpCH.name,100);
			GetWindowTextA(hABOUT,tmpCH.eventAbout,100);
			SendMessage(hTIME,DTM_GETSYSTEMTIME,0,(LPARAM)&sys1);
			sys.wHour=sys1.wHour;
			sys.wMinute=sys1.wMinute;
			sys.wSecond=sys1.wSecond;
			
			sysTOtm(newdata,sys);
			tmpCH.second=	mktime(&newdata);
			HANDLE hFile,hDIRECT;
			wsprintf(str4,TEXT("H:\\CALENDAR\\%d.txt"),sys1.wMonth);
			ofstream red(str4,ios::app);
			if(!red)
			{
				CreateDirectoryA("H:\\CALENDAR",0);
				hFile=CreateFile(str4,GENERIC_WRITE|GENERIC_READ,0,0,CREATE_ALWAYS,FILE_ATTRIBUTE_NORMAL|FILE_ATTRIBUTE_ARCHIVE,0);
				CloseHandle(hFile);
			}
			if(red)
			{
				red<<"."<<(int)tmpCH.second<<endl<<tmpCH.name<<endl<<tmpCH.eventAbout<<endl;
			}
			red.close();
			SendMessage(hDlg,WM_CLOSE,0,0);
			wsprintf(namefile,TEXT("H:\\CALENDAR\\%d.txt"),SendMessage(hCalendar,MCM_GETMONTHRANGE,GMR_VISIBLE|GMR_DAYSTATE,0)+1);
		FindSECOND(namefile,ALLch);
			break;
		}
		break;
	}
	return FALSE;
}
bool sysTOtm(tm &tmp,_SYSTEMTIME sys)
{
	tmp.tm_year=sys.wYear-1900;
	tmp.tm_mday=sys.wDay;
	tmp.tm_mon=sys.wMonth-1;
	tmp.tm_hour=sys.wHour-1;
	tmp.tm_min=sys.wMinute;
	tmp.tm_sec=sys.wSecond;

	return 0;


}

DATAch SetDATAch (CHAR* name, CHAR* aboutNAME, int second)
{
	DATAch result;
	strcpy(result.name,name);
	strcpy(result.eventAbout,aboutNAME);
	result.second=second;
	return result;
}
void FindSECOND(WCHAR *str4, vector <DATAch> &ALLch)
{
	ALLch.clear();
	CHAR ch;
	HANDLE hFile;
	CHAR str[100];
	CHAR str2[100];
	int sec;
	ifstream red(str4,ios::in);
	if(!red)
	{
		MessageBox(0,L" you dosn't have plans",L"ff",MB_OK);
		CreateDirectoryA("H:\\CALENDAR",0);
		hFile=CreateFile(str4,GENERIC_WRITE|GENERIC_READ,0,0,CREATE_ALWAYS,FILE_ATTRIBUTE_NORMAL|FILE_ATTRIBUTE_ARCHIVE,0);
		CloseHandle(hFile);
	}
	else
	{
		
			while(!red.eof())
			{
				red.get(ch);
				if(ch=='.')
			{
			red.getline(str,100);
			sec=atoi(str);
			red.getline(str,100);
			red.getline(str2,100);
			ALLch.push_back(SetDATAch(str,str2,sec));
			}
			}
	}

		red.close();
	}
	
void DELETEdata(vector <DATAch> &ALLch)
{
	
	WCHAR str4[50];
	wsprintf(str4,TEXT("H:\\CALENDAR\\%d.txt"),SendMessage(hCalendar,MCM_GETMONTHRANGE,GMR_DAYSTATE|GMR_VISIBLE,0)+1);
	ofstream red(str4,ios::out);
	if(red)
			{
				for(int i=0; i<ALLch.size()-1; i++)
				{
					red<<"."<<(int)ALLch[i].second<<endl<<ALLch[i].name<<endl<<ALLch[i].eventAbout<<endl;
				}
			}
			red.close();
}
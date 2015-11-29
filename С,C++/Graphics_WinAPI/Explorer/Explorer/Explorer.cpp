#include<Windows.h>
#include"resource.h"
#include<CommCtrl.h>
#include<stdio.h>
#include<tchar.h>
#include<TlHelp32.h>
#include<locale.h>
#include<conio.h>
#pragma comment(lib,"comctl32")

#define ID_STATUS_BAR 4444
int part[5]={200,300,400,500,-1};
TCHAR str[150];
int * mas;
static int chet=1,levo=0;
PROCESSENTRY32 pe32;
static HWND hLIST_L,hLIST_R,hEdit,hBUTTON_start,hBUTTON_retry,hBUTTON_close,hBUTTON_all,hBUTTON_left,hBUTTON_right,hBUTTON_clear,hSTATUSbar,hSTATIC;
void ProcessList();
int FindId(TCHAR *str1);
BOOL CALLBACK DlgProc(HWND, UINT, WPARAM, LPARAM); 
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst,  
        LPSTR lpszCmdLine, int nCmdShow) 
{ 
   
  return DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), NULL, DlgProc);  
} 
 
BOOL CALLBACK DlgProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) 
{ 
	int wmId,wmEvent,tmp1=0,tmp=0;
	STARTUPINFO sui;
	PROCESS_INFORMATION proctmp;
	BOOL newproc;
	TCHAR strID[150],strTMP[150];

  switch(message) 
  { 
    case WM_CLOSE: 
      EndDialog(hWnd, 0); 
      return TRUE; 
     
    case WM_INITDIALOG: 
       		hLIST_L=GetDlgItem(hWnd,IDC_LIST1);
		hLIST_R=GetDlgItem(hWnd,IDC_LIST2);
		hEdit=GetDlgItem(hWnd,IDC_EDIT1);
		hBUTTON_start=GetDlgItem(hWnd,IDC_BUTTON_start);
		hBUTTON_retry=GetDlgItem(hWnd,IDC_BUTTON_retry);
		hBUTTON_close=GetDlgItem(hWnd,IDC_BUTTON_close);
		hBUTTON_all=GetDlgItem(hWnd,IDC_BUTTON_all);
		hBUTTON_left=GetDlgItem(hWnd,IDC_BUTTON_left);
		hBUTTON_right=GetDlgItem(hWnd,IDC_BUTTON_right);
		hBUTTON_clear=GetDlgItem(hWnd,IDC_BUTTON_clear);
		hSTATIC=GetDlgItem(hWnd,IDC_STATIC);
		hSTATUSbar=CreateWindowEx(0,STATUSCLASSNAME,NULL,WS_CHILD|WS_VISIBLE|CCS_BOTTOM|SBARS_TOOLTIPS|SBARS_SIZEGRIP,0,0,0,0,hWnd,(HMENU)ID_STATUS_BAR,0,0);
		SendMessage(hSTATUSbar,SB_SETPARTS,5,(LPARAM)part);
		ProcessList();
      return TRUE; 
 
    case WM_COMMAND: 
       wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
      
		switch(wmId)
		{
		case IDC_BUTTON_close:

			tmp=SendMessage(hLIST_R,LB_GETCOUNT,0,0);
			mas=new int [tmp];
			tmp=SendMessage(hLIST_R,LB_GETSELCOUNT,0,0);
			SendMessage(hLIST_R,LB_GETSELITEMS,tmp,(LPARAM) mas);
			for(int i=0; i<SendMessage(hLIST_R,LB_GETSELCOUNT,0,0);i++)
			{
				SendMessage(hLIST_R,LB_GETTEXT,mas[i],(LPARAM)str);
				wcscpy(strTMP,str);
				wcscpy(strID, wcstok(strTMP,L"."));
				tmp=_ttoi(strID);
				//GetExitCodeProcess((HANDLE)tmp,(LPDWORD)tmp1);
				if(TerminateProcess(OpenProcess(PROCESS_ALL_ACCESS,0,tmp),0))
				{
				SendMessage(hLIST_R,LB_DELETESTRING,mas[i],0);
				chet--;
				}
				else
					MessageBox(hWnd,TEXT("Не получилось закрыть процесс"),TEXT("Ты что творишь??"),MB_OK);
				wsprintf(str,TEXT("количество процессов: %d"),chet);
				SendMessage(hSTATUSbar,SB_SETTEXT,0,(LPARAM)str);
			}




			
			break;
		case IDC_BUTTON_all:
			tmp=SendMessage(hLIST_L,LB_GETCOUNT,0,0);
				
			for(int i=0; i<tmp;i++)
			{
				SendMessage(hLIST_L,LB_GETTEXT,i,(LPARAM)str);
				wcscpy(strTMP,str);
				wcscpy(strID, wcstok(strTMP,L"."));
				tmp1=_ttoi(strID);
				if(OpenProcess(PROCESS_ALL_ACCESS,0,tmp1))
				{
				SendMessage(hLIST_L,LB_DELETESTRING,i,0);
				SendMessage(hLIST_R,LB_ADDSTRING,0,(LPARAM)str);
				chet--;
				levo++;
				}
				wsprintf(str,TEXT("количество процессов: %d"),chet);
				SendMessage(hSTATUSbar,SB_SETTEXT,0,(LPARAM)str);
				wsprintf(str,TEXT("количество процессов: %d"),levo);
				SendMessage(hSTATUSbar,SB_SETTEXT,4,(LPARAM)str);

			}
			
			break;
		case IDC_BUTTON_left:
			tmp=SendMessage(hLIST_R,LB_GETCOUNT,0,0);
			mas=new int [tmp];
			tmp=SendMessage(hLIST_R,LB_GETSELCOUNT,0,0);
			SendMessage(hLIST_R,LB_GETSELITEMS,tmp,(LPARAM) mas);
			for(int i=0; i<SendMessage(hLIST_R,LB_GETSELCOUNT,0,0);i++)
			{
				SendMessage(hLIST_R,LB_GETTEXT,mas[i],(LPARAM)str);
				SendMessage(hLIST_R,LB_DELETESTRING,mas[i],0);
				SendMessage(hLIST_L,LB_ADDSTRING,0,(LPARAM)str);
				chet++;
				levo--;
			}
			wsprintf(str,TEXT("количество процессов: %d"),chet);
			SendMessage(hSTATUSbar,SB_SETTEXT,0,(LPARAM)str);
			wsprintf(str,TEXT("количество процессов: %d"),levo);
				SendMessage(hSTATUSbar,SB_SETTEXT,4,(LPARAM)str);

			
			break;
		case IDC_BUTTON_retry:
			ProcessList();
			break;
		case IDC_BUTTON_start:
			memset(&sui,0,sizeof(STARTUPINFO));
			sui.cb=sizeof(STARTUPINFO);
			GetWindowText(hEdit,str,150);
			newproc=CreateProcess(str,0,0,0,0,CREATE_NEW_CONSOLE,0,0,&sui,&proctmp);
			if(!newproc)
				MessageBox(hWnd,L"File not find",L"Warning",MB_OK);
			else
				chet++;
			ProcessList();
			wsprintf(str,TEXT("количество процессов: %d"),chet);
			SendMessage(hSTATUSbar,SB_SETTEXT,0,(LPARAM)str);
			break;
		case IDC_BUTTON_right:
			tmp=SendMessage(hLIST_L,LB_GETCOUNT,0,0);
			mas=new int [tmp];
			tmp=SendMessage(hLIST_L,LB_GETSELCOUNT,0,0);
			SendMessage(hLIST_L,LB_GETSELITEMS,tmp,(LPARAM) mas);
			for(int i=0; i<SendMessage(hLIST_L,LB_GETSELCOUNT,0,0);i++)
			{
				SendMessage(hLIST_L,LB_GETTEXT,mas[i],(LPARAM)str);
				wcscpy(strTMP,str);
				wcscpy(strID, wcstok(strTMP,L"."));
				tmp=_ttoi(strID);
				if(OpenProcess(PROCESS_ALL_ACCESS,0,tmp))
				{
				SendMessage(hLIST_L,LB_DELETESTRING,mas[i],0);
				SendMessage(hLIST_R,LB_ADDSTRING,0,(LPARAM)str);
				chet--;
				levo++;
				}
				else
					MessageBox(hWnd,TEXT("Не лезь, у тебя нет доступа"),TEXT("Ты что творишь??"),MB_OK);
				wsprintf(str,TEXT("количество процессов: %d"),chet);
				SendMessage(hSTATUSbar,SB_SETTEXT,0,(LPARAM)str);
				wsprintf(str,TEXT("количество процессов: %d"),levo);
				SendMessage(hSTATUSbar,SB_SETTEXT,4,(LPARAM)str);

			}
			
			break;
		case IDC_BUTTON_clear:
			SendMessage(hLIST_R,LB_RESETCONTENT,0,0);
			ProcessList();
			wsprintf(str,TEXT("количество процессов: %d"),levo);
				SendMessage(hSTATUSbar,SB_SETTEXT,4,(LPARAM)str);

			break;
		}
		return TRUE; 
     
  } 
  return FALSE; 
}

void ProcessList(){
	SendMessage(hLIST_L,LB_RESETCONTENT,0,0);
	HANDLE hSnapShot=CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS,0);
	
	WCHAR str[50];
	TCHAR exit[100];
	chet=1;
	memset(&pe32,0,sizeof(PROCESSENTRY32));
	pe32.dwSize=sizeof(PROCESSENTRY32);
	
	if(Process32First(hSnapShot,&pe32))
		{
			
			wsprintf(exit,TEXT("%4u .  %16s %8uкб  %15d"),pe32.th32ProcessID,pe32.szExeFile,pe32.dwSize,pe32.pcPriClassBase);
			SendMessage(hLIST_L,LB_ADDSTRING,0,(LPARAM)exit);

			
			
	}
	while(Process32Next(hSnapShot,&pe32))
		{
			//SendMessage(hLIST_L,LB_ADDSTRING,0,(LPARAM)pe32.szExeFile);
			wsprintf(exit,TEXT("%4u .  %13s %13uкб   %17d"),pe32.th32ProcessID,pe32.szExeFile,pe32.dwSize,pe32.pcPriClassBase);
			SendMessage(hLIST_L,LB_ADDSTRING,0,(LPARAM)exit);
			chet++;
	}
	wsprintf(str,TEXT("количество процессов: %d"),chet);
	SendMessage(hSTATUSbar,SB_SETTEXT,0,(LPARAM)str);
}
//int FindId(TCHAR *str1)
//{
//	HANDLE hSnapShot=CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS,0);
//	memset(&pe32,0,sizeof(PROCESSENTRY32));
//	pe32.dwSize=sizeof(PROCESSENTRY32);
//	if(Process32First(hSnapShot,&pe32))
//		{
//			if(!wcsncmp(str1,pe32.th32ProcessID,wcslen(str1)))
//				return pe32.th32ProcessID;
//			
//	}
//	while(Process32Next(hSnapShot,&pe32))
//		{
//			if(!wcsncmp(str1,pe32.szExeFile,wcslen(str1)))
//				return pe32.th32ProcessID;
//	}
//}
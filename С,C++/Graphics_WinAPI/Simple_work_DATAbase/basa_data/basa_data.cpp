// basa_data.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "basa_data.h"
#include <vector>

#define MAX_LOADSTRING 100

#define ID_LIST 4001
#define ID_STATIC 5001
#define ID_BUTTON_delete 1001
#define ID_BUTTON_add 1002
#define ID_BUTTON_redact 1003

#define ID_add_LISTname 5001
#define ID_add_LISTbirthday 5002
#define ID_add_LISTphone 5003
#define ID_add_LISTemail 5004
#define ID_add_LISTadress 5005

#define ID_add_STATICname 6001
#define ID_add_STATICbirthday 6002
#define ID_add_STATICphone 6003
#define ID_add_STATICemail 6004
#define ID_add_STATICadress 6005

void ADDdata(WCHAR *mSURNAME, WCHAR* mBIRTHDAY,WCHAR* mPHONE,	TCHAR *mEMAIL,	TCHAR* mADRESS);
void DELETEdata(int x);

// Global Variables:
using namespace std;
static HWND hBUTTONdelete,hBUTTONadd,hBUTTONredact,hLIST,hSTATIC;
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
struct STUDENT
{
	TCHAR SURNAME[50];
	TCHAR BIRTHDAY[50];
	TCHAR PHONE[50];
	TCHAR EMAIL[50];
	TCHAR ADRESS[50];

};

static vector <STUDENT> database;
TCHAR* stroka(STUDENT x);
// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);
BOOL CALLBACK Dlgcorect	(HWND, UINT, WPARAM, LPARAM);
BOOL CALLBACK	Dlgadd (HWND, UINT, WPARAM, LPARAM);


int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_BASA_DATA, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_BASA_DATA));

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
//  COMMENTS:
//
//    This function and its usage are only necessary if you want this code
//    to be compatible with Win32 systems prior to the 'RegisterClassEx'
//    function that was added to Windows 95. It is important to call this function
//    so that the application will get 'well formed' small icons associated
//    with it.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_BASA_DATA));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_BASA_DATA);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   HWND hWnd;

   hInst = hInstance; // Store instance handle in our global variable

   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, 425, 430, NULL, NULL, hInstance, NULL);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent, index;
	PAINTSTRUCT ps;
	HDC hdc;
	
	switch (message)
	{
	case WM_CREATE:
		hLIST=CreateWindowEx(0,L"LISTbox",NULL,WS_CHILD|WS_VISIBLE|LBS_NOTIFY|WS_VSCROLL|WS_BORDER,0,0,200,320,hWnd,(HMENU)ID_LIST,hInst,0);
		hSTATIC=CreateWindowEx(0,L"Static",NULL,WS_CHILD|WS_VISIBLE|WS_BORDER,210,0,200,300,hWnd,(HMENU)ID_STATIC,hInst,0);
		hBUTTONdelete=CreateWindowEx(0,L"Button",L"delete",WS_CHILD|WS_VISIBLE,30,320,100,40,hWnd,(HMENU)ID_BUTTON_delete,hInst,0);
		hBUTTONadd=CreateWindowEx(0,L"Button",L"add"      ,WS_CHILD|WS_VISIBLE,140,320,100,40,hWnd,(HMENU)ID_BUTTON_add,hInst,0);
		hBUTTONredact=CreateWindowEx(0,L"Button",L"corect",WS_CHILD|WS_VISIBLE,250,320,100,40,hWnd,(HMENU)ID_BUTTON_redact,hInst,0);


		ADDdata(L"Den",L"11.08.1991",L"433-55-37",L"denkrukov@ukr.net",L"Kiev");
		ADDdata(L"Anton",L"23.06.1992",L"433-44-67",L"toxa@ukr.net",L"Moscow");
		ADDdata(L"Rostic",L"01.10.1993",L"423-33-67",L"rostic@ukr.net",L"New-York");
		ADDdata(L"Nazar",L"14.02.1994",L"543-87-37",L"nazar@ukr.net",L"Brovaru");
		ADDdata(L"Dima",L"08.08.1989",L"433-33-37",L"dima@ukr.net",L"Donezk");
		

		for(int i=0; i<(database.size());i++)
			SendMessage(hLIST,LB_ADDSTRING,0,(LPARAM)database[i].SURNAME);
		SendMessage(hLIST,LB_SETCURSEL,0,0);
		SetWindowText(hSTATIC,stroka(database[SendMessage(hLIST,LB_GETCURSEL,0,0)]));
		
		break;
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		case ID_BUTTON_redact:
			DialogBox(hInst,MAKEINTRESOURCE(IDD_DIALOG_add),hWnd,Dlgcorect);
			break;
		case ID_BUTTON_add:
			DialogBox(hInst,MAKEINTRESOURCE(IDD_DIALOG_add),hWnd,Dlgadd);
			break;
		case ID_BUTTON_delete:
			DELETEdata(	SendMessage(hLIST,LB_GETCURSEL,0,0));
			SendMessage(hLIST,LB_RESETCONTENT,0,0);
			for(int i=0; i<(database.size());i++)
				SendMessage(hLIST,LB_ADDSTRING,0,(LPARAM)database[i].SURNAME);
			SendMessage(hLIST,LB_SETCURSEL,0,0);
			SetWindowText(hSTATIC,stroka(database[SendMessage(hLIST,LB_GETCURSEL,0,0)]));


			break;
		case ID_LIST:
			SetWindowText(hSTATIC,stroka(database[SendMessage(hLIST,LB_GETCURSEL,0,0)]));
			
			break;
			
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		// TODO: Add any drawing code here...
		EndPaint(hWnd, &ps);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}
void ADDdata(TCHAR *mSURNAME, TCHAR* mBIRTHDAY,TCHAR* mPHONE,	TCHAR *mEMAIL,	TCHAR* mADRESS)
{
	STUDENT tmp;
	wcscpy(tmp.SURNAME,mSURNAME);
	wcscpy(tmp.BIRTHDAY,mBIRTHDAY);
	wcscpy(tmp.PHONE,mPHONE);
	wcscpy(tmp.EMAIL,mEMAIL);
	wcscpy(tmp.ADRESS,mADRESS);
	database.push_back(tmp);
}

void DELETEdata(int x)
{
	database.erase(database.begin()+x);
}
TCHAR* stroka(STUDENT x)
{
	TCHAR str[100];
	wcscpy(str,x.SURNAME);
	wcscat(str,L"\n");
	wcscat(str,x.BIRTHDAY);
	wcscat(str,L"\n");
	wcscat(str,x.PHONE);
	wcscat(str,L"\n");
	wcscat(str,x.EMAIL);
	wcscat(str,L"\n");
	wcscat(str,x.ADRESS);
	wcscat(str,L"\n");
	return str;
}

BOOL CALLBACK Dlgadd(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId,wmEvent, tmp,tmp1;
	static HWND masLIST[5],masSTATIC[5];
	TCHAR str[5][100];

	switch (message)
	{
	
	case WM_INITDIALOG:
		for(int i=0;i<5;i++)
			{
				tmp=ID_add_LISTname+i;
				tmp1=ID_add_STATICname+i;
				masLIST[i]=CreateWindowEx(0,L"Edit",NULL,WS_CHILD|WS_VISIBLE|WS_BORDER,200,50*i,180,40,hDlg,(HMENU)tmp,hInst,0);
				masSTATIC[i]=CreateWindowEx(0,L"Static",NULL,WS_CHILD|WS_VISIBLE,90,50*i,80,40,hDlg,(HMENU)tmp1,hInst,0);
		}
		SetWindowText(masSTATIC[0],L"Name");
		SetWindowText(masSTATIC[1],L"Birthday");
		SetWindowText(masSTATIC[2],L"Phone");
		SetWindowText(masSTATIC[3],L"Email");
		SetWindowText(masSTATIC[4],L"Adress");

		
		
		return TRUE;

	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		
		switch (wmId)
		{
		case IDOK:
			for(int i=0; i<5;i++)
			GetWindowText(masLIST[i],str[i],50);
			SendMessage(hLIST,LB_ADDSTRING,0,(LPARAM) str[0]);
			ADDdata(str[0],str[1],str[2],str[3],str[4]);
			EndDialog(hDlg,0);
			break;
		case IDCANCEL:
			EndDialog(hDlg,0);
			break;

		}
		break;

		case WM_CLOSE:
		EndDialog(hDlg,0);
		return TRUE;
	}
	return FALSE;
}

BOOL CALLBACK Dlgcorect(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId,wmEvent, tmp,tmp1;
	static HWND masLIST[5],masSTATIC[5];
	TCHAR str[5][100];

	switch (message)
	{
	
	case WM_INITDIALOG:
		for(int i=0;i<5;i++)
			{
				tmp=ID_add_LISTname+i;
				tmp1=ID_add_STATICname+i;
				masLIST[i]=CreateWindowEx(0,L"Edit",NULL,WS_CHILD|WS_VISIBLE|WS_BORDER,200,50*i,180,40,hDlg,(HMENU)tmp,hInst,0);
				masSTATIC[i]=CreateWindowEx(0,L"Static",NULL,WS_CHILD|WS_VISIBLE,90,50*i,80,40,hDlg,(HMENU)tmp1,hInst,0);
		}
		SetWindowText(masSTATIC[0],L"Name");
		SetWindowText(masSTATIC[1],L"Birthday");
		SetWindowText(masSTATIC[2],L"Phone");
		SetWindowText(masSTATIC[3],L"Email");
		SetWindowText(masSTATIC[4],L"Adress");

		
			
				SetWindowText(masLIST[0],database[SendMessage(hLIST,LB_GETCURSEL,0,0)].SURNAME);
				SetWindowText(masLIST[1],database[SendMessage(hLIST,LB_GETCURSEL,0,0)].ADRESS);
				SetWindowText(masLIST[2],database[SendMessage(hLIST,LB_GETCURSEL,0,0)].PHONE);
				SetWindowText(masLIST[3],database[SendMessage(hLIST,LB_GETCURSEL,0,0)].EMAIL);
				SetWindowText(masLIST[4],database[SendMessage(hLIST,LB_GETCURSEL,0,0)].ADRESS);
			
		
		return TRUE;

	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		
		switch (wmId)
		{
		case IDOK:
			DELETEdata(	SendMessage(hLIST,LB_GETCURSEL,0,0));
			SendMessage(hLIST,LB_RESETCONTENT,0,0);
			for(int i=0; i<(database.size());i++)
				SendMessage(hLIST,LB_ADDSTRING,0,(LPARAM)database[i].SURNAME);
			SendMessage(hLIST,LB_SETCURSEL,0,0);
			SetWindowText(hSTATIC,stroka(database[SendMessage(hLIST,LB_GETCURSEL,0,0)]));


			for(int i=0; i<5;i++)
			GetWindowText(masLIST[i],str[i],50);
			SendMessage(hLIST,LB_ADDSTRING,0,(LPARAM) str[0]);
			ADDdata(str[0],str[1],str[2],str[3],str[4]);
			EndDialog(hDlg,0);
			break;
		case IDCANCEL:
			EndDialog(hDlg,0);
			break;

		}
		break;

		case WM_CLOSE:
		EndDialog(hDlg,0);
		return TRUE;
	}
	return FALSE;
}
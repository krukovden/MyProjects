// GAMEchetchik.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "GAMEchetchik.h"
#include<CommCtrl.h>
#include<conio.h>
#include<ctime>
#pragma comment(lib,"comctl32")
#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' \processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#define MAX_LOADSTRING 100

#define ID_BUTTON_1 1001
#define ID_BUTTON_2 1002
#define ID_BUTTON_3 1003
#define ID_BUTTON_4 1004
#define ID_BUTTON_5 1005
#define ID_BUTTON_6 1006
#define ID_BUTTON_7 1007
#define ID_BUTTON_8 1008
#define ID_BUTTON_9 1009
#define ID_BUTTON_10 1010
#define ID_BUTTON_11 1011
#define ID_BUTTON_12 1012
#define ID_BUTTON_13 1013
#define ID_BUTTON_14 1014
#define ID_BUTTON_15 1015
#define ID_BUTTON_16 1016
#define ID_BUTTON_start 1017
#define ID_SPLINT 5001
#define ID_EDITBOX 5002

#define ID_LISTBOX 2001
#define ID_PROCESBAR 3001




// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
static int arr[16],sort[16];
// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPTSTR    lpCmdLine,
	int       nCmdShow)
{
	srand(time(0));
	InitCommonControls();
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_GAMECHETCHIK, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_GAMECHETCHIK));

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_GAMECHETCHIK));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+9);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_GAMECHETCHIK);
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
		CW_USEDEFAULT, 0, 410, 410, NULL, NULL, hInstance, NULL);

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
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;
	static HWND mas[16];
	WCHAR str[100],str2[50];
	static HWND hPROC,hList,hSPLINT,hEDIT, hSTART;
	UDACCEL pACESL[2]={{1,5},{3,10}};
	int t;
	static int time;
	bool *pferror;
	static int chet=0;
	static float tmp1=0,tmp;
	static bool flag=0;
	switch (message)
	{
	case WM_TIMER:
		SendMessage(hPROC,PBM_STEPIT,0,0);
		tmp=SendMessage(hPROC,PBM_GETPOS,0,0);
		if(tmp>=time)
		{
			KillTimer(hWnd,1);
			GetWindowText(hList,str,100);
			/*wcstok(str,L" ");
			while(wcstok(NULL,L" "))
			finish++;

			if(finish==16)
			MessageBox(hWnd,L"Congratulation, you win!!",L"WARNING",MB_OK|MB_ICONSTOP);
			else*/
			if(chet!=16)
				MessageBox(hWnd,L"You are losser!!",L"WARNING",MB_OK|MB_ICONSTOP);
			if(MessageBox(hWnd,L"Do you want play again?",L"WARNING",MB_OKCANCEL|MB_ICONSTOP)==IDCANCEL)
				DestroyWindow(hWnd);
			break;

		}
		break;
	case WM_VSCROLL:

		break;
	case WM_CREATE:
		for(int i=0,k=0; i<4;i++)
			for(int j=0;j<4;j++,k++)
			{
				t=ID_BUTTON_1+k;
				mas[k]=CreateWindowEx(7,L"Button",L"",WS_CHILD|WS_VISIBLE,79*i,80*j,70,70,hWnd,(HMENU) t,hInst,0);
				
				SetWindowText(mas[k],L"?");
				/*sort[k]=arr[k]=rand()%100;
				_itow(arr[k],str,10);
				SetWindowText(mas[k],str);*/
			}




			hSTART=CreateWindowEx(0,L"Button",L"START",WS_VISIBLE|WS_CHILD|WS_BORDER,310,225,80,40,hWnd,(HMENU)ID_BUTTON_start,hInst,0);
			hPROC=CreateWindowEx(0,PROGRESS_CLASS,NULL,WS_VISIBLE|WS_CHILD,0,320,390,20,hWnd,(HMENU)ID_PROCESBAR,hInst,0);
			SendMessage(hPROC,PBM_SETBKCOLOR,0,LPARAM(RGB(0,0,255)));

			SendMessage(hPROC,PBM_SETBARCOLOR,0,LPARAM(RGB(255,0,0)));

			hList=CreateWindowEx(0,L"LISTBOX",0,WS_VISIBLE|WS_CHILD|WS_BORDER|LBS_MULTIPLESEL|LBS_NOTIFY,310,0,80,220,hWnd,(HMENU)ID_LISTBOX,hInst,0);
			hEDIT=CreateWindowEx(0,L"Edit",L" ",WS_VISIBLE|WS_CHILD,315,270,70,40,hWnd,(HMENU)ID_EDITBOX,hInst,0);

			hSPLINT=CreateUpDownControl(WS_VISIBLE|WS_CHILD|UDS_WRAP|UDS_ALIGNRIGHT|UDS_SETBUDDYINT,0,0,0,0,hWnd,ID_SPLINT,hInst,hEDIT,100,1,20);
			SendMessage(hSPLINT,UDM_SETACCEL,2,LPARAM(pACESL));




			break;

	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		for(int i=0; i<16;i++)
		{
			if(ID_BUTTON_1+i==wmId)
			{
				if(arr[i]==sort[chet])
				{
					chet++;
					GetWindowText(hList,str,100);
					_itow(arr[i],str2,10);
					wcscat(str,str2);
					SendMessage(hList,LB_ADDSTRING,0,(LPARAM)str);
					EnableWindow(mas[i],0);
					if(chet==16)
						MessageBox(hWnd,L"Congratulation, you win!!",L"WARNING",MB_OK|MB_ICONSTOP);
				}
				else 
					MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			}
		}

		switch (wmId)
		{
		case ID_BUTTON_start:
			if(flag==0)
			{
				flag=1;
				chet=0;
				SetWindowText(hSTART,L"Stop");
				SendMessage(hList,LB_RESETCONTENT,0,0);
				time=SendMessage(hSPLINT,UDM_GETPOS32,0,0);
				SendMessage(hPROC,PBM_SETRANGE,0,MAKELPARAM(0,time));
				SendMessage(hPROC,PBM_SETSTEP,1,0);
				SendMessage(hPROC,PBM_SETPOS,0,0);



				for(int i=0; i<16; i++)
				{
					EnableWindow(mas[i],1);
					sort[i]=arr[i]=rand()%100;
					_itow(arr[i],str,10);
					SetWindowText(mas[i],str);
				}
				for(int i=0;i<16;i++)
				{          
					for(int j=16-1;j>i;j--)
					{    
						if(sort[j-1]>sort[j])
						{
							t=sort[j-1];
							sort[j-1]=sort[j];
							sort[j]=t;
						}
					}
				}
				SetTimer(hWnd,1,1000,0);
			}
			else
			{
				if(MessageBox(hWnd,L"Your game not saved,You really want exit this game?",L"WARNING",MB_OKCANCEL|MB_ICONSTOP)==IDCANCEL)
				break;
				KillTimer(hWnd,1);
				flag=0;
				SetWindowText(hSTART,L"Start");

			}
			break;




			/*case ID_BUTTON_1:
			if(arr[0]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[0],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[0],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_2:
			if(arr[1]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[1],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[1],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_3:
			if(arr[2]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[2],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[2],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_4:
			if(arr[3]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[3],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[3],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_5:
			if(arr[4]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[4],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[4],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_6:
			if(arr[5]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[5],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[5],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_7:
			if(arr[6]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[6],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[6],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_8:
			if(arr[7]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[7],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[7],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_9:
			if(arr[8]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[8],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[8],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_10:
			if(arr[9]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[9],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[9],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_11:
			if(arr[10]==sort[chet])
			{
			chet++;
			GetWindowText(hList,str,100);
			_itow(arr[10],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[10],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_12:
			if(arr[11]==sort[chet])
			{chet++;
			GetWindowText(hList,str,100);
			_itow(arr[11],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[11],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_13:
			if(arr[12]==sort[chet])
			{chet++;
			GetWindowText(hList,str,100);
			_itow(arr[12],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[12],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_14:
			if(arr[13]==sort[chet])
			{chet++;
			GetWindowText(hList,str,100);
			_itow(arr[13],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[13],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_15:
			if(arr[14]==sort[chet])
			{chet++;
			GetWindowText(hList,str,100);
			_itow(arr[14],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[14],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;
			case ID_BUTTON_16:
			if(arr[15]==sort[chet])
			{chet++;
			GetWindowText(hList,str,100);
			_itow(arr[15],str2,10);
			wcscat(str,L" \0 ");
			wcscat(str,str2);
			SetWindowText(hList,str);
			EnableWindow(mas[15],0);
			}
			else 
			MessageBox(hWnd,L"Wrong number",L"WARNING",MB_OK);
			break;*/
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

// calculator.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "calculator.h"
#include <strsafe.h>

#define MAX_LOADSTRING 100
#define ID_BUTTON_number1 1001
#define ID_BUTTON_number2 1002
#define ID_BUTTON_number3 1003
#define ID_BUTTON_number4 1004
#define ID_BUTTON_number5 1005
#define ID_BUTTON_number6 1006
#define ID_BUTTON_number7 1007
#define ID_BUTTON_number8 1008
#define ID_BUTTON_number9 1009
#define ID_BUTTON_number0 1000
#define ID_BUTTON_plus 1101
#define ID_BUTTON_rovno 1102
#define ID_BUTTON_minus 1103
#define ID_BUTTON_CE 1104
#define ID_BUTTON_dil 1105
#define ID_BUTTON_ymn 1106
#define ID_BUTTON_C 1107
#define ID_BUTTON_point 1108
#define ID_EDIT1 2001
#define ID_EDIT2 2002
#define ID_EDIT3 2003
#define ID_EDIT4 2004
// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name

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
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_CALCULATOR, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_CALCULATOR));

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_CALCULATOR));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+21|COLOR_GRAYTEXT+2);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_CALCULATOR);
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

	hWnd = CreateWindow(szWindowClass, L"simple calculator", WS_SYSMENU,
		100, 100,245, 330, NULL, NULL, hInstance, NULL);

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
	HWND hBUTTON1,hBUTTON2,hBUTTON3,hBUTTON4,hBUTTON5,hBUTTON6,hBUTTON7,hBUTTON8,hBUTTON9,hBUTTON0,hBUTTONplus,
		hBUTTONrovno,hBUTTONminus,hBUTTON_CE,hBUTTONymn,hBUTTONdil, hBUTTON_C, hBUTTON_point;
	static HWND hDISPLAY;
	WCHAR str[50],str1[50];
	static WCHAR sym;
	static double  tmp=0,otvet=0;
	static int count=0;
	int kod;
	switch (message)
	{
	case WM_CREATE:
		hDISPLAY=CreateWindowEx(4,L"Edit",L"",WS_CHILD|WS_VISIBLE|WS_TABSTOP|WS_BORDER|ES_RIGHT,0,10,240,40,hWnd,(HMENU)ID_EDIT1,hInst,0);

		hBUTTON1=CreateWindowEx(5,L"Button",L"1",WS_CHILD|WS_VISIBLE|WS_BORDER,0,60,40,40,hWnd,(HMENU)ID_BUTTON_number1,hInst,0);
		hBUTTON4=CreateWindowEx(5,L"Button",L"4",WS_CHILD|WS_VISIBLE|WS_BORDER,0,110,40,40,hWnd,(HMENU)ID_BUTTON_number4,hInst,0);
		hBUTTON7=CreateWindowEx(5,L"Button",L"7",WS_CHILD|WS_VISIBLE|WS_BORDER,0,160,40,40,hWnd,(HMENU)ID_BUTTON_number7,hInst,0);
		hBUTTON2=CreateWindowEx(5,L"Button",L"2",WS_CHILD|WS_VISIBLE|WS_BORDER,50,60,40,40,hWnd,(HMENU)ID_BUTTON_number2,hInst,0);
		hBUTTON5=CreateWindowEx(5,L"Button",L"5",WS_CHILD|WS_VISIBLE|WS_BORDER,50,110,40,40,hWnd,(HMENU)ID_BUTTON_number5,hInst,0);
		hBUTTON8=CreateWindowEx(5,L"Button",L"8",WS_CHILD|WS_VISIBLE|WS_BORDER,50,160,40,40,hWnd,(HMENU)ID_BUTTON_number8,hInst,0);
		hBUTTON3=CreateWindowEx(5,L"Button",L"3",WS_CHILD|WS_VISIBLE|WS_BORDER,100,60,40,40,hWnd,(HMENU)ID_BUTTON_number3,hInst,0);
		hBUTTON6=CreateWindowEx(5,L"Button",L"6",WS_CHILD|WS_VISIBLE|WS_BORDER,100,110,40,40,hWnd,(HMENU)ID_BUTTON_number6,hInst,0);
		hBUTTON9=CreateWindowEx(5,L"Button",L"9",WS_CHILD|WS_VISIBLE|WS_BORDER,100,160,40,40,hWnd,(HMENU)ID_BUTTON_number9,hInst,0);
		hBUTTONplus=CreateWindowEx(5,L"Button",L"+",WS_CHILD|WS_VISIBLE|WS_BORDER,150,210,40,40,hWnd,(HMENU)ID_BUTTON_plus,hInst,0);
		hBUTTONrovno=CreateWindowEx(5,L"Button",L"=",WS_CHILD|WS_VISIBLE|WS_BORDER,150,110,40,90,hWnd,(HMENU)ID_BUTTON_rovno,hInst,0);
		hBUTTONminus=CreateWindowEx(5,L"Button",L"-",WS_CHILD|WS_VISIBLE|WS_BORDER,150,60,40,40,hWnd,(HMENU)ID_BUTTON_minus,hInst,0);
		hBUTTON0=CreateWindowEx(5,L"Button",L"0",WS_CHILD|WS_VISIBLE|WS_BORDER,50,210,40,40,hWnd,(HMENU)ID_BUTTON_number0,hInst,0);
		hBUTTON_CE=CreateWindowEx(5,L"Button",L"CE",WS_CHILD|WS_VISIBLE|WS_BORDER,200,60,40,80,hWnd,(HMENU)ID_BUTTON_CE,hInst,0);
		hBUTTONdil=CreateWindowEx(5,L"Button",L"/",WS_CHILD|WS_VISIBLE|WS_BORDER,0,210,40,40,hWnd,(HMENU)ID_BUTTON_dil,hInst,0);
		hBUTTONymn=CreateWindowEx(5,L"Button",L"*",WS_CHILD|WS_VISIBLE|WS_BORDER,100,210,40,40,hWnd,(HMENU)ID_BUTTON_ymn,hInst,0);
		hBUTTON_C=CreateWindowEx(5,L"Button",L"C",WS_CHILD|WS_VISIBLE|WS_BORDER,200,150,40,40,hWnd,(HMENU)ID_BUTTON_C,hInst,0);
		hBUTTON_point=CreateWindowEx(5,L"Button",L".",WS_CHILD|WS_VISIBLE|WS_BORDER,200,210,40,40,hWnd,(HMENU)ID_BUTTON_point,hInst,0);
		break;
	case WM_KEYDOWN:
		
		switch(wParam)
		{
			
		case 0x30:
		case VK_NUMPAD0:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"0");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case 0x31:
		case VK_NUMPAD1:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"1");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		
			case 0x32:
		case VK_NUMPAD2:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"2");
			SetWindowText(hDISPLAY,str);
			count++;
			break;

			case 0x33:
		case VK_NUMPAD3:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"3");
			SetWindowText(hDISPLAY,str);
			count++;
			break;


			case 0x34:
		case VK_NUMPAD4:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"4");
			SetWindowText(hDISPLAY,str);
			count++;
			break;

			case 0x35:
		case VK_NUMPAD5:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"5");
			SetWindowText(hDISPLAY,str);
			count++;
			break;

			case 0x36:
		case VK_NUMPAD6:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"6");
			SetWindowText(hDISPLAY,str);
			count++;
			break;

			case 0x37:
		case VK_NUMPAD7:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"7");
			SetWindowText(hDISPLAY,str);
			count++;
			break;

			case 0x38:
		case VK_NUMPAD8:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"8");
			SetWindowText(hDISPLAY,str);
			count++;
			break;

			case 0x39:
		case VK_NUMPAD9:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"9");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case VK_DECIMAL:
		case VK_OEM_PERIOD:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L".");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
			case VK_ADD:
		case VK_OEM_PLUS:
			GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);
			SetWindowText(hDISPLAY,L" ");
			sym='+';
			break;
			
		case VK_SUBTRACT:
		case VK_OEM_MINUS:
			GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);
			SetWindowText(hDISPLAY,L" ");
			sym='-';
			break;
			case VK_DIVIDE:
				GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);
			SetWindowText(hDISPLAY,L" ");
			sym='/';
			break;

			case VK_RETURN:
			GetWindowText(hDISPLAY,str,50);
						if(sym=='+')
				otvet=tmp+wcstod(str,0);
			if(sym=='-')
				otvet=tmp-wcstod(str,0);
			if(sym=='*')
				otvet=tmp*wcstod(str,0);
			if(sym=='/')
				otvet=tmp/wcstod(str,0);
			
			//_itow(otvet,str,10);
			StringCbPrintf(str,50,TEXT("%.2f"),otvet);
			SetWindowText(hDISPLAY,str);
			sym='0';
			break;
				case VK_BACK:
				GetWindowText(hDISPLAY,str,50);
			str[count-1]='\0';
			SetWindowText(hDISPLAY,str);
			count--;
			break;
				case VK_MULTIPLY:
					GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);
			SetWindowText(hDISPLAY,L" ");
			sym='*';
					break;
				case VK_ESCAPE:
					DestroyWindow(hWnd);
					break;
					
		}
		
		break;
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		case ID_BUTTON_point:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L".");
			SetWindowText(hDISPLAY,str);
			count++;
			
			break;
		case ID_BUTTON_number1:
				SendMessage(hWnd,WM_KEYDOWN,VK_NUMPAD1,0);
			break;
		case ID_BUTTON_number2:
			SendMessage(hWnd,WM_KEYDOWN,VK_NUMPAD2,0);
			break;
		case ID_BUTTON_number3:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"3");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_number4:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"4");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_number5:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"5");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_number6:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"6");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_number7:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"7");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_number8:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"8");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_number9:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"9");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_number0:
			GetWindowText(hDISPLAY,str,50);
			wcscat(str,L"0");
			SetWindowText(hDISPLAY,str);
			count++;
			break;
		case ID_BUTTON_plus:
			GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);
			SetWindowText(hDISPLAY,L" ");
			sym='+';
			break;
		case ID_BUTTON_minus:
			GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);//-tmp;
			SetWindowText(hDISPLAY,L" ");
			sym='-';
			break;
		case ID_BUTTON_dil:
			GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);
			SetWindowText(hDISPLAY,L" ");
			sym='/';
			break;
		case ID_BUTTON_ymn:
			GetWindowText(hDISPLAY,str,50);
			tmp=wcstod(str,0);
			SetWindowText(hDISPLAY,L" ");
			sym='*';
			break;
		case ID_BUTTON_CE:
			SetWindowText(hDISPLAY,L" ");
			break;
		case ID_BUTTON_C:
			GetWindowText(hDISPLAY,str,50);
			str[count-1]='\0';
			SetWindowText(hDISPLAY,str);
			count--;
			break;
		case ID_BUTTON_rovno:

			GetWindowText(hDISPLAY,str,50);
			/*if(count==1)
			{*/
			if(sym=='+')
				otvet=tmp+wcstod(str,0);
			if(sym=='-')
				otvet=tmp-wcstod(str,0);
			if(sym=='*')
				otvet=tmp*wcstod(str,0);
			if(sym=='/')
				otvet=tmp/wcstod(str,0);
			//count*=0;
			/*}
			else
			{
			if(sym=='+')
			otvet+=_wtoi(str);
			if(sym=='-')
			otvet-=_wtoi(str);
			if(sym=='*')
			otvet=tmp*_wtoi(str);
			if(sym=='/')
			otvet=tmp/_wtoi(str);
			}*/
			//_itow(otvet,str,10);
			StringCbPrintf(str,50,TEXT("%.2f"),otvet);
			SetWindowText(hDISPLAY,str);
			sym='0';
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
		SetFocus(hWnd);
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

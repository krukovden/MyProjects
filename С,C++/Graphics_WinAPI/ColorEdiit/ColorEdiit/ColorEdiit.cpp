// ColorEdiit.cpp : Defines the entry point for the application.
//
#pragma once
#include "stdafx.h"
#include "ColorEdiit.h"
#include<CommCtrl.h>
#include<strsafe.h>
#pragma comment(lib,"comctl32")
//#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' \processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#define MAX_LOADSTRING 100
#define ID_SLIDER_red 4001
#define ID_SLIDER_green 4002
#define ID_SLIDER_blue 4003
#define ID_PROGRESbar 5001
#define ID_STATIC_red 3001
#define ID_STATIC_green 3002
#define ID_STATIC_blue 3003
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
	//InitCommonControls();
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_COLOREDIIT, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_COLOREDIIT));

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_COLOREDIIT));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+18);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_COLOREDIIT);
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
      CW_USEDEFAULT, 0, 650, 400, NULL, NULL, hInstance, NULL);

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
	static HWND hRED,hBLUE,hGREEN,hPROC;
	HWND hSTATICred,hSTATICblue,hSTATICgreen;
	static WCHAR str[50];
	static int tmp;
	switch (message)
	{
case WM_HSCROLL:
	StringCbPrintf(str,50*sizeof(WCHAR),TEXT("red: %d green: %d blue: %d"),SendMessage(hRED,TBM_GETPOS,0,0),SendMessage(hGREEN,TBM_GETPOS,0,0),SendMessage(hBLUE,TBM_GETPOS,0,0));
	SetWindowText(hWnd,str);
	SendMessage(hPROC,PBM_SETBKCOLOR,0,RGB(SendMessage(hRED,TBM_GETPOS,0,0),SendMessage(hGREEN,TBM_GETPOS,0,0),SendMessage(hBLUE,TBM_GETPOS,0,0)));
		break;
	case WM_CREATE:
		hRED=CreateWindowEx(4,TRACKBAR_CLASS,NULL,
			WS_CHILD|WS_VISIBLE|WS_BORDER|TBS_TOOLTIPS|TBS_AUTOTICKS,
			10,40,250,50,hWnd,(HMENU) ID_SLIDER_red,hInst,0);
		hGREEN=CreateWindowEx(4,TRACKBAR_CLASS,NULL,
			WS_CHILD|WS_VISIBLE|WS_BORDER|TBS_TOOLTIPS|TBS_AUTOTICKS,
			10,210,250,50,hWnd,(HMENU) ID_SLIDER_green,hInst,0);
		hBLUE=CreateWindowEx(4,TRACKBAR_CLASS,NULL,
			WS_CHILD|WS_VISIBLE|WS_BORDER|TBS_TOOLTIPS|TBS_AUTOTICKS,
			10,130,250,50,hWnd,(HMENU) ID_SLIDER_blue,hInst,0);
		
		SendMessage(hRED,TBM_SETRANGE, TRUE, MAKELPARAM(0,255));
		SendMessage(hBLUE,TBM_SETRANGE, TRUE, MAKELPARAM(0,255));
		SendMessage(hGREEN,TBM_SETRANGE, TRUE, MAKELPARAM(0,255));

		SendMessage(hRED,TBM_SETTICFREQ,WPARAM(5),0);
		SendMessage(hBLUE,TBM_SETTICFREQ,WPARAM(5),0);
		SendMessage(hGREEN,TBM_SETTICFREQ,WPARAM(5),0);
		
		hPROC=CreateWindowEx(0,PROGRESS_CLASS,NULL,WS_CHILD|WS_VISIBLE,280,10,300,300,hWnd,(HMENU) ID_PROGRESbar,hInst,0);
		
		hSTATICred=CreateWindowEx(0,L"Static",L"red",WS_CHILD|WS_VISIBLE,10,10,50,20,hWnd,(HMENU) ID_STATIC_blue,0,0);
		hSTATICblue=CreateWindowEx(0,L"Static",L"blue",WS_CHILD|WS_VISIBLE,10,100,50,20,hWnd,(HMENU) ID_STATIC_red,0,0);
		hSTATICgreen=CreateWindowEx(0,L"Static",L"green",WS_CHILD|WS_VISIBLE,10,180,50,20,hWnd,(HMENU) ID_STATIC_green,0,0);

		
		break;

	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
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

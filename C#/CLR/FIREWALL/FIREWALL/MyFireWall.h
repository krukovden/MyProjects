#pragma once
#include <winsock2.h>
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <cliext\vector>
#include "IPS.h"


#include "windivert.h"
#define MAXBUF  0xFFFF


namespace FIREWALL {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::Collections::Generic;
	using namespace System::Threading; 
	using namespace ADClasses;
	using namespace System::Net;
	using namespace PacketDotNet;
	using namespace SharpPcap;

	/*
	* Pre-fabricated packets.
	*/
	typedef struct
	{
		WINDIVERT_IPHDR ip;
		WINDIVERT_TCPHDR tcp;
	} TCPPACKET, *PTCPPACKET;

	typedef struct
	{
		WINDIVERT_IPV6HDR ipv6;
		WINDIVERT_TCPHDR tcp;
	} TCPV6PACKET, *PTCPV6PACKET;


	typedef struct
	{
		WINDIVERT_IPHDR ip;
		WINDIVERT_ICMPHDR icmp;
		UINT8 data[];
	} ICMPPACKET, *PICMPPACKET;

	typedef struct
	{
		WINDIVERT_IPV6HDR ipv6;
		WINDIVERT_ICMPV6HDR icmpv6;
		UINT8 data[];
	} ICMPV6PACKET, *PICMPV6PACKET;

	/*
	* Prototypes.
	*/
	static void PacketIpInit(PWINDIVERT_IPHDR packet);
	static void PacketIpTcpInit(PTCPPACKET packet);
	static void PacketIpIcmpInit(PICMPPACKET packet);
	static void PacketIpv6Init(PWINDIVERT_IPV6HDR packet);
	static void PacketIpv6TcpInit(PTCPV6PACKET packet);
	static void PacketIpv6Icmpv6Init(PICMPV6PACKET packet);
	static void PacketIpIcmpInit(PICMPPACKET packet);


	/// <summary>
	/// Сводка для MyFireWall
	/// </summary>
	public ref class MyFireWall : public System::Windows::Forms::Form
	{


	public:
		MyFireWall(void)
		{
			InitializeComponent();
			myIP=IPAddress::Parse( System::Net::Dns::GetHostByName(System::Net::Dns::GetHostName())->AddressList[0]->ToString());

			if(!StartSniffer())
			{
				if(MessageBox::Show(this,"Program cannot open net card for listenning all packets. Do you want continue work as FIREWALL?","Problem",MessageBoxButtons::YesNo)==System::Windows::Forms::DialogResult::No )
					this->Close();
			}


			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~MyFireWall()
		{
			if (components)
			{
				delete components;
			}


		}

	protected: 

	private: System::Windows::Forms::Button^  btStart;

	private: System::Windows::Forms::TextBox^  textBoxIP1;

	private: System::Windows::Forms::CheckBox^  chAll;

	private: System::Windows::Forms::CheckBox^  chIP;

	private: System::Windows::Forms::CheckBox^  chIP6;

	private: System::Windows::Forms::CheckBox^  chICMP;

	private: System::Windows::Forms::CheckBox^  chICMP6;

	private: System::Windows::Forms::Button^  btAddIp;
	private: System::Windows::Forms::CheckBox^  chTCP;

	private: System::Windows::Forms::ListView^  listView1;





	private: System::Windows::Forms::CheckBox^  chUDP;
	private: System::Windows::Forms::GroupBox^  groupBox1;
	private: System::Windows::Forms::RadioButton^  radioButton2;
	private: System::Windows::Forms::RadioButton^  radioButton1;
	private: System::Windows::Forms::ListView^  iptables;

	private: System::Windows::Forms::ColumnHeader^  columnHeader1;
	private: System::Windows::Forms::ColumnHeader^  columnHeader2;

	private: System::Windows::Forms::Button^  button1;
	private: System::Windows::Forms::TabControl^  tabControl1;
	private: System::Windows::Forms::TabPage^  tabPage1;
	private: System::Windows::Forms::TabPage^  tabPage2;
	private: System::Windows::Forms::GroupBox^  groupBox2;
	private: System::Windows::Forms::ColumnHeader^  columnHeader3;
	private: System::Windows::Forms::ColumnHeader^  columnHeader4;
	private: System::Windows::Forms::ColumnHeader^  columnHeader5;
	private: System::Windows::Forms::ColumnHeader^  columnHeader6;
	private: System::Windows::Forms::ColumnHeader^  columnHeader7;
	private: System::Windows::Forms::TabPage^  tabPage3;
	private: System::Windows::Forms::ListView^  listView2;
	private: System::Windows::Forms::ColumnHeader^  columnHeader8;
	private: System::Windows::Forms::ColumnHeader^  columnHeader9;
	private: System::Windows::Forms::ColumnHeader^  columnHeader10;
	private: System::Windows::Forms::ColumnHeader^  columnHeader11;


	protected: 

	private:
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;



#pragma region Windows Form Designer generated code
		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->btStart = (gcnew System::Windows::Forms::Button());
			this->textBoxIP1 = (gcnew System::Windows::Forms::TextBox());
			this->chAll = (gcnew System::Windows::Forms::CheckBox());
			this->chIP = (gcnew System::Windows::Forms::CheckBox());
			this->chIP6 = (gcnew System::Windows::Forms::CheckBox());
			this->chICMP = (gcnew System::Windows::Forms::CheckBox());
			this->chICMP6 = (gcnew System::Windows::Forms::CheckBox());
			this->btAddIp = (gcnew System::Windows::Forms::Button());
			this->chTCP = (gcnew System::Windows::Forms::CheckBox());
			this->listView1 = (gcnew System::Windows::Forms::ListView());
			this->columnHeader3 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader4 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader5 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader6 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader7 = (gcnew System::Windows::Forms::ColumnHeader());
			this->chUDP = (gcnew System::Windows::Forms::CheckBox());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->radioButton2 = (gcnew System::Windows::Forms::RadioButton());
			this->radioButton1 = (gcnew System::Windows::Forms::RadioButton());
			this->iptables = (gcnew System::Windows::Forms::ListView());
			this->columnHeader1 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader2 = (gcnew System::Windows::Forms::ColumnHeader());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
			this->tabPage1 = (gcnew System::Windows::Forms::TabPage());
			this->tabPage2 = (gcnew System::Windows::Forms::TabPage());
			this->tabPage3 = (gcnew System::Windows::Forms::TabPage());
			this->listView2 = (gcnew System::Windows::Forms::ListView());
			this->columnHeader8 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader9 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader10 = (gcnew System::Windows::Forms::ColumnHeader());
			this->columnHeader11 = (gcnew System::Windows::Forms::ColumnHeader());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->groupBox1->SuspendLayout();
			this->tabControl1->SuspendLayout();
			this->tabPage1->SuspendLayout();
			this->tabPage2->SuspendLayout();
			this->tabPage3->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->SuspendLayout();
			// 
			// btStart
			// 
			this->btStart->BackColor = System::Drawing::Color::Lime;
			this->btStart->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 14, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->btStart->Location = System::Drawing::Point(929, 13);
			this->btStart->Name = L"btStart";
			this->btStart->Size = System::Drawing::Size(163, 73);
			this->btStart->TabIndex = 2;
			this->btStart->Text = L"Старт";
			this->btStart->UseVisualStyleBackColor = false;
			this->btStart->Click += gcnew System::EventHandler(this, &MyFireWall::btStart_Click);
			// 
			// textBoxIP1
			// 
			this->textBoxIP1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 10, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->textBoxIP1->Location = System::Drawing::Point(22, 39);
			this->textBoxIP1->Name = L"textBoxIP1";
			this->textBoxIP1->Size = System::Drawing::Size(152, 23);
			this->textBoxIP1->TabIndex = 3;
			// 
			// chAll
			// 
			this->chAll->AutoSize = true;
			this->chAll->Location = System::Drawing::Point(971, 272);
			this->chAll->Name = L"chAll";
			this->chAll->Size = System::Drawing::Size(45, 17);
			this->chAll->TabIndex = 6;
			this->chAll->Text = L"Все";
			this->chAll->UseVisualStyleBackColor = true;
			this->chAll->CheckedChanged += gcnew System::EventHandler(this, &MyFireWall::chAll_CheckedChanged);
			// 
			// chIP
			// 
			this->chIP->AutoSize = true;
			this->chIP->Location = System::Drawing::Point(971, 309);
			this->chIP->Name = L"chIP";
			this->chIP->Size = System::Drawing::Size(36, 17);
			this->chIP->TabIndex = 7;
			this->chIP->Text = L"IP";
			this->chIP->UseVisualStyleBackColor = true;
			// 
			// chIP6
			// 
			this->chIP6->AutoSize = true;
			this->chIP6->Location = System::Drawing::Point(971, 345);
			this->chIP6->Name = L"chIP6";
			this->chIP6->Size = System::Drawing::Size(48, 17);
			this->chIP6->TabIndex = 8;
			this->chIP6->Text = L"IPv6";
			this->chIP6->UseVisualStyleBackColor = true;
			// 
			// chICMP
			// 
			this->chICMP->AutoSize = true;
			this->chICMP->Location = System::Drawing::Point(971, 384);
			this->chICMP->Name = L"chICMP";
			this->chICMP->Size = System::Drawing::Size(52, 17);
			this->chICMP->TabIndex = 9;
			this->chICMP->Text = L"ICMP";
			this->chICMP->UseVisualStyleBackColor = true;
			// 
			// chICMP6
			// 
			this->chICMP6->AutoSize = true;
			this->chICMP6->Location = System::Drawing::Point(971, 427);
			this->chICMP6->Name = L"chICMP6";
			this->chICMP6->Size = System::Drawing::Size(64, 17);
			this->chICMP6->TabIndex = 10;
			this->chICMP6->Text = L"ICMPv6";
			this->chICMP6->UseVisualStyleBackColor = true;
			// 
			// btAddIp
			// 
			this->btAddIp->Location = System::Drawing::Point(57, 68);
			this->btAddIp->Name = L"btAddIp";
			this->btAddIp->Size = System::Drawing::Size(75, 23);
			this->btAddIp->TabIndex = 11;
			this->btAddIp->Text = L"Добавить";
			this->btAddIp->UseVisualStyleBackColor = true;
			this->btAddIp->Click += gcnew System::EventHandler(this, &MyFireWall::btAddIp_Click);
			// 
			// chTCP
			// 
			this->chTCP->AutoSize = true;
			this->chTCP->Location = System::Drawing::Point(971, 468);
			this->chTCP->Name = L"chTCP";
			this->chTCP->Size = System::Drawing::Size(47, 17);
			this->chTCP->TabIndex = 12;
			this->chTCP->Text = L"TCP";
			this->chTCP->UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this->listView1->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(192)), static_cast<System::Int32>(static_cast<System::Byte>(255)), 
				static_cast<System::Int32>(static_cast<System::Byte>(255)));
			this->listView1->Columns->AddRange(gcnew cli::array< System::Windows::Forms::ColumnHeader^  >(5) {this->columnHeader3, this->columnHeader4, 
				this->columnHeader5, this->columnHeader6, this->columnHeader7});
			this->listView1->Dock = System::Windows::Forms::DockStyle::Fill;
			this->listView1->Font = (gcnew System::Drawing::Font(L"Times New Roman", 10, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->listView1->FullRowSelect = true;
			this->listView1->GridLines = true;
			this->listView1->Location = System::Drawing::Point(3, 3);
			this->listView1->Name = L"listView1";
			this->listView1->Size = System::Drawing::Size(925, 382);
			this->listView1->TabIndex = 13;
			this->listView1->UseCompatibleStateImageBehavior = false;
			this->listView1->View = System::Windows::Forms::View::Details;
			this->listView1->SelectedIndexChanged += gcnew System::EventHandler(this, &MyFireWall::listView1_SelectedIndexChanged);
			// 
			// columnHeader3
			// 
			this->columnHeader3->Text = L"IP отправителя";
			this->columnHeader3->Width = 140;
			// 
			// columnHeader4
			// 
			this->columnHeader4->Text = L"IP получателя";
			this->columnHeader4->Width = 140;
			// 
			// columnHeader5
			// 
			this->columnHeader5->Text = L"Протокол";
			this->columnHeader5->Width = 80;
			// 
			// columnHeader6
			// 
			this->columnHeader6->Text = L"  ";
			this->columnHeader6->Width = 140;
			// 
			// columnHeader7
			// 
			this->columnHeader7->Text = L" ";
			this->columnHeader7->Width = 140;
			// 
			// chUDP
			// 
			this->chUDP->AutoSize = true;
			this->chUDP->Location = System::Drawing::Point(971, 506);
			this->chUDP->Margin = System::Windows::Forms::Padding(2);
			this->chUDP->Name = L"chUDP";
			this->chUDP->Size = System::Drawing::Size(49, 17);
			this->chUDP->TabIndex = 14;
			this->chUDP->Text = L"UDP";
			this->chUDP->UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->radioButton2);
			this->groupBox1->Controls->Add(this->radioButton1);
			this->groupBox1->Location = System::Drawing::Point(969, 168);
			this->groupBox1->Margin = System::Windows::Forms::Padding(2);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Padding = System::Windows::Forms::Padding(2);
			this->groupBox1->Size = System::Drawing::Size(123, 95);
			this->groupBox1->TabIndex = 15;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"Пакеты для запрета";
			// 
			// radioButton2
			// 
			this->radioButton2->AutoSize = true;
			this->radioButton2->Location = System::Drawing::Point(5, 74);
			this->radioButton2->Margin = System::Windows::Forms::Padding(2);
			this->radioButton2->Name = L"radioButton2";
			this->radioButton2->Size = System::Drawing::Size(83, 17);
			this->radioButton2->TabIndex = 1;
			this->radioButton2->Text = L"Исходящие";
			this->radioButton2->UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this->radioButton1->AutoSize = true;
			this->radioButton1->Checked = true;
			this->radioButton1->Location = System::Drawing::Point(5, 42);
			this->radioButton1->Margin = System::Windows::Forms::Padding(2);
			this->radioButton1->Name = L"radioButton1";
			this->radioButton1->Size = System::Drawing::Size(76, 17);
			this->radioButton1->TabIndex = 0;
			this->radioButton1->TabStop = true;
			this->radioButton1->Text = L"Входящие";
			this->radioButton1->UseVisualStyleBackColor = true;
			// 
			// iptables
			// 
			this->iptables->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(255)), static_cast<System::Int32>(static_cast<System::Byte>(192)), 
				static_cast<System::Int32>(static_cast<System::Byte>(192)));
			this->iptables->Columns->AddRange(gcnew cli::array< System::Windows::Forms::ColumnHeader^  >(2) {this->columnHeader1, this->columnHeader2});
			this->iptables->Dock = System::Windows::Forms::DockStyle::Fill;
			this->iptables->Font = (gcnew System::Drawing::Font(L"Times New Roman", 12, static_cast<System::Drawing::FontStyle>((System::Drawing::FontStyle::Italic | System::Drawing::FontStyle::Underline)), 
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(204)));
			this->iptables->FullRowSelect = true;
			this->iptables->Location = System::Drawing::Point(3, 3);
			this->iptables->Margin = System::Windows::Forms::Padding(2);
			this->iptables->Name = L"iptables";
			this->iptables->Size = System::Drawing::Size(925, 382);
			this->iptables->TabIndex = 16;
			this->iptables->UseCompatibleStateImageBehavior = false;
			this->iptables->View = System::Windows::Forms::View::Details;
			// 
			// columnHeader1
			// 
			this->columnHeader1->Text = L"IP";
			this->columnHeader1->Width = 150;
			// 
			// columnHeader2
			// 
			this->columnHeader2->Text = L"Кем запрещенно";
			this->columnHeader2->Width = 130;
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(19, 525);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(116, 23);
			this->button1->TabIndex = 18;
			this->button1->Text = L"Очистить список";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyFireWall::button1_Click_1);
			// 
			// tabControl1
			// 
			this->tabControl1->Controls->Add(this->tabPage1);
			this->tabControl1->Controls->Add(this->tabPage2);
			this->tabControl1->Controls->Add(this->tabPage3);
			this->tabControl1->Location = System::Drawing::Point(12, 109);
			this->tabControl1->Name = L"tabControl1";
			this->tabControl1->SelectedIndex = 0;
			this->tabControl1->Size = System::Drawing::Size(939, 414);
			this->tabControl1->TabIndex = 19;
			this->tabControl1->SelectedIndexChanged += gcnew System::EventHandler(this, &MyFireWall::tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this->tabPage1->Controls->Add(this->listView1);
			this->tabPage1->Location = System::Drawing::Point(4, 22);
			this->tabPage1->Name = L"tabPage1";
			this->tabPage1->Padding = System::Windows::Forms::Padding(3);
			this->tabPage1->Size = System::Drawing::Size(931, 388);
			this->tabPage1->TabIndex = 0;
			this->tabPage1->Text = L"Блокированные пакеты";
			this->tabPage1->UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this->tabPage2->Controls->Add(this->iptables);
			this->tabPage2->Location = System::Drawing::Point(4, 22);
			this->tabPage2->Name = L"tabPage2";
			this->tabPage2->Padding = System::Windows::Forms::Padding(3);
			this->tabPage2->Size = System::Drawing::Size(931, 388);
			this->tabPage2->TabIndex = 1;
			this->tabPage2->Text = L"Блокированные IP";
			this->tabPage2->UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this->tabPage3->Controls->Add(this->listView2);
			this->tabPage3->Location = System::Drawing::Point(4, 22);
			this->tabPage3->Name = L"tabPage3";
			this->tabPage3->Size = System::Drawing::Size(931, 388);
			this->tabPage3->TabIndex = 2;
			this->tabPage3->Text = L"Все пакеты в сети";
			this->tabPage3->UseVisualStyleBackColor = true;
			// 
			// listView2
			// 
			this->listView2->BackColor = System::Drawing::Color::LightCyan;
			this->listView2->Columns->AddRange(gcnew cli::array< System::Windows::Forms::ColumnHeader^  >(4) {this->columnHeader8, this->columnHeader9, 
				this->columnHeader10, this->columnHeader11});
			this->listView2->Dock = System::Windows::Forms::DockStyle::Fill;
			this->listView2->Font = (gcnew System::Drawing::Font(L"Times New Roman", 11.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->listView2->Location = System::Drawing::Point(0, 0);
			this->listView2->Name = L"listView2";
			this->listView2->Size = System::Drawing::Size(931, 388);
			this->listView2->TabIndex = 0;
			this->listView2->UseCompatibleStateImageBehavior = false;
			this->listView2->View = System::Windows::Forms::View::Details;
			// 
			// columnHeader8
			// 
			this->columnHeader8->Text = L"IP отправителя";
			this->columnHeader8->Width = 140;
			// 
			// columnHeader9
			// 
			this->columnHeader9->Text = L"IP получателя";
			this->columnHeader9->Width = 140;
			// 
			// columnHeader10
			// 
			this->columnHeader10->Text = L"Протокол";
			this->columnHeader10->Width = 100;
			// 
			// columnHeader11
			// 
			this->columnHeader11->Text = L"Длина пакета";
			this->columnHeader11->Width = 100;
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->textBoxIP1);
			this->groupBox2->Controls->Add(this->btAddIp);
			this->groupBox2->Location = System::Drawing::Point(545, 3);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(200, 100);
			this->groupBox2->TabIndex = 20;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"Введите IP от которого не будут поступать пакеты";
			// 
			// MyFireWall
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::SystemColors::InactiveCaption;
			this->ClientSize = System::Drawing::Size(1104, 565);
			this->Controls->Add(this->groupBox2);
			this->Controls->Add(this->tabControl1);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->groupBox1);
			this->Controls->Add(this->chUDP);
			this->Controls->Add(this->chTCP);
			this->Controls->Add(this->chICMP6);
			this->Controls->Add(this->chICMP);
			this->Controls->Add(this->chIP6);
			this->Controls->Add(this->chIP);
			this->Controls->Add(this->chAll);
			this->Controls->Add(this->btStart);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedDialog;
			this->MaximizeBox = false;
			this->MinimizeBox = false;
			this->Name = L"MyFireWall";
			this->Text = L" Firewall";
			this->FormClosing += gcnew System::Windows::Forms::FormClosingEventHandler(this, &MyFireWall::MyFireWall_FormClosing);
			this->Load += gcnew System::EventHandler(this, &MyFireWall::MyFireWall_Load);
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->tabControl1->ResumeLayout(false);
			this->tabPage1->ResumeLayout(false);
			this->tabPage2->ResumeLayout(false);
			this->tabPage3->ResumeLayout(false);
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void MyFireWall_Load(System::Object^  sender, System::EventArgs^  e) 
			 {				

			 }
	private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) 
			 {
				 btStart->Enabled=true;
				 chAll->Enabled=true;
				 chIP	->Enabled=true;			 
				 chIP6	->Enabled=true;			 
				 chICMP	->Enabled=true;			 
				 chICMP6->Enabled=true;
				 btAddIp->Enabled=true;
				 chTCP->Enabled=true;
				 chUDP->Enabled=true;
				 radioButton2->Enabled=true;
				 radioButton1->Enabled=true;
				 t->Abort();
				 t->Join();
				 t2->Abort();
				 t2->Join();
				 WinDivertClose(handle);


			 }
	private: static bool stop=false;
	private: static IPAddress^ myIP;
	private: static List<IPAddress^>^ table=gcnew List<IPAddress^>();
	private: static Dictionary<IPAddress^, Block^>^ blockTable=gcnew Dictionary<IPAddress^, Block^>();
	private: Thread^ t;
	private: Thread^ t2;
	private: ICaptureDevice^ GlobalDevice;
	private: void StartBlock()
			 {
				 if(handle!=NULL)
					 DivertClose(handle);
				 if(t!=nullptr)
					 t->Abort();
				 if(t2!=nullptr)
					 t2->Abort();
				 t=gcnew Thread(gcnew ThreadStart(this,&MyFireWall::XXX));			 

				 t2=gcnew Thread(gcnew ThreadStart(this,&MyFireWall::Razbor));		
				 t->Start(); t2->Start(); 
			 }

	private: System::Void btStart_Click(System::Object^  sender, System::EventArgs^  e) 
			 {	
				 if(btStart->Text=="Старт")
				 {

					 btStart->BackColor = System::Drawing::Color::Red; 
					 chAll->Enabled=false;
					 chIP	->Enabled=false;			 
					 chIP6	->Enabled=false;			 
					 chICMP	->Enabled=false;			 
					 chICMP6->Enabled=false;
					 btAddIp->Enabled=false;
					 chTCP->Enabled=false;
					 chUDP->Enabled=false;
					 radioButton2->Enabled=false;
					 radioButton1->Enabled=false;
					 StartBlock();
					 btStart->Text="Стоп";
				 }
				 else
				 {
					 btStart->BackColor = System::Drawing::Color::Lime;
					 chAll->Enabled=true;
					 chIP	->Enabled=true;			 
					 chIP6	->Enabled=true;			 
					 chICMP	->Enabled=true;			 
					 chICMP6->Enabled=true;
					 btAddIp->Enabled=true;
					 chTCP->Enabled=true;
					 chUDP->Enabled=true;
					 radioButton2->Enabled=true;
					 radioButton1->Enabled=true;
					 if(handle!=NULL)
						 DivertClose(handle);
					 if(t!=nullptr)
						 t->Abort();
					 if(t2!=nullptr)
						 t2->Abort();					
					 WinDivertClose(handle);
					 btStart->Text="Старт";
				 }


			 }
			 delegate void AddListItem(ListViewItem^ var);

			 void AddToList(ListViewItem^ var)
			 {
				 listView1->Items->Add(var);
			 }
			 void AddToList2(ListViewItem^ var)
			 {
				 listView2->Items->Add(var);
			 }

	private: void Razbor()
			 {
				 try	{
					 for each (ListViewItem^ var in raw)
					 {
						 this->Invoke(gcnew AddListItem( this,  &MyFireWall::AddToList ), var);					

					 }
				 }
				 catch(Exception^ e)
				 {
				 }

			 }

	private: void SNiffer(Object^ sender, CaptureEventArgs^ e)
			 {
				 try{
					 Packet^ packet=Packet::ParsePacket(e->Packet->LinkLayerType,e->Packet->Data);
					 IpPacket^ ip=IpPacket::GetEncapsulated(packet);
					 if(ip==nullptr)
						 return;
					 array<String^>^stra=gcnew array<String^>(4);
					 stra[0]=ip->SourceAddress->ToString();
					 stra[1]=ip->DestinationAddress->ToString();
					 switch(ip->Protocol)
					 {
					 case IPProtocolType::TCP:
						 stra[2]="TCP";
						 break;
					 case IPProtocolType::UDP:
						 stra[2]="UDP";
						 break;
					 case IPProtocolType::ICMP:
						 stra[2]="ICMP";
						 break;
					 case IPProtocolType::ICMPV6:
						 stra[2]="ICMPV6";
						 break;
					 case IPProtocolType::IPV6:
						 stra[2]="IPV6";
						 break;
					 default:
						 stra[2]="Unnown";
						 break;
					 }
					 stra[3]=Convert::ToString( ip->TotalLength);


					 if(String::Compare(stra[0],myIP->ToString())!=0)
					 { 
						 IPAddress^ ii=IPAddress::Parse(stra[0]);
						 if(ii->AddressFamily==System::Net::Sockets::AddressFamily::InterNetwork)
						 {
						 if(blockTable->ContainsKey(ii))
							 blockTable[ii]->Add();
						 else
							 blockTable->Add(ii,gcnew Block(ii));

						 ClearBlockTable();
						 }
					 }
					 ListViewItem^ item=gcnew ListViewItem(stra);

					 this->Invoke(gcnew AddListItem( this,  &MyFireWall::AddToList2 ), item);		
				 }
				 catch(Exception^ ex)
				 {}
			 }
	private: bool StartSniffer()
			 {
				 try{
					 CaptureDeviceList^ deviceList=CaptureDeviceList::Instance;
					 GlobalDevice=deviceList[0];
					 GlobalDevice->OnPacketArrival+=gcnew PacketArrivalEventHandler(this,&MyFireWall::SNiffer);
					 GlobalDevice->Open();
					 GlobalDevice->StartCapture();
					 return true;
				 }
				 catch(Exception^ ex)
				 {
					 return false;
				 }
			 }
	private: System::Void listView1_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e) {
			 }
	private: System::Void chAll_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
			 {
				 bool a=true;
				 if(chAll->Checked)
					 a=true;
				 else
					 a=false;

				 chTCP->Checked=a;
				 chUDP->Checked=a;
				 chICMP->Checked=a;
				 chICMP6->Checked=a;
				 chIP6->Checked=a;
				 chIP->Checked=a;


			 }
	private: static SyncQueue<ListViewItem^>^ raw=gcnew SyncQueue <ListViewItem^>();
			 private: static bool IsSysytemBlock=false;
	private: void GetFilter(char * filter)
			 {
				 int j=0;
				 int first=0;

				 if(radioButton1->Checked)
					 first=j=sprintf(filter,"inbound and (");
				 else
					 first=j=sprintf(filter,"outbound and (");

				 if(chAll->Checked)
				 { sprintf(filter,"true"); return;}

				 if(chTCP->Checked)
					 j+=sprintf(filter+j,first<j?" or tcp ": " tcp ");				
				 if(chUDP->Checked)
					 j+=sprintf(filter+j,first<j?" or udp ": " udp ");				
				 if(chICMP6->Checked)
					 j+=sprintf(filter+j,first<j?" or icmpv6 ": " icmpv6 ");
				 if(chICMP->Checked)
					 j+=sprintf(filter+j,first<j?" or icmp ": " icmp ");
				 if(chIP6->Checked)
					 j+=sprintf(filter+j,first<j?" or ipv6 ": " ipv6 ");
				 if(chIP->Checked)
					 j+=sprintf(filter+j,first<j?" or ip ": " ip ");

				 if(table->Count>0)
					 for each (IPAddress^ item in table)
					 {					
						 j+=sprintf(filter+j,first<j?" or ip.SrcAddr==%s ": " ip.SrcAddr==%s ",item->ToString());
					 }


					 sprintf(filter+j," )");

					 if(!chTCP->Checked && !chUDP->Checked && !chICMP6->Checked && !chICMP->Checked && !chIP6->Checked && !chIP->Checked && !IsSysytemBlock)
						 sprintf(filter,"false");

					 return;

			 }
	private: static HANDLE handle;
	private : void XXX()
			  {

				  char text[5][80];

				  HANDLE  console;
				  UINT i;
				  INT16 priority = 0;
				  unsigned char packet[MAXBUF];
				  UINT packet_len;
				  WINDIVERT_ADDRESS recv_addr, send_addr;
				  PWINDIVERT_IPHDR ip_header;
				  PWINDIVERT_IPV6HDR ipv6_header;
				  PWINDIVERT_ICMPHDR icmp_header;
				  PWINDIVERT_ICMPV6HDR icmpv6_header;
				  PWINDIVERT_TCPHDR tcp_header;
				  PWINDIVERT_UDPHDR udp_header;
				  UINT payload_len;

				  TCPPACKET reset0;
				  PTCPPACKET reset = &reset0;
				  UINT8 dnr0[sizeof(ICMPPACKET) + 0x0F*sizeof(UINT32) + 8 + 1];
				  PICMPPACKET dnr = (PICMPPACKET)dnr0;

				  TCPV6PACKET resetv6_0;
				  PTCPV6PACKET resetv6 = &resetv6_0;
				  UINT8 dnrv6_0[sizeof(ICMPV6PACKET) + sizeof(WINDIVERT_IPV6HDR) +
					  sizeof(WINDIVERT_TCPHDR)];
				  PICMPV6PACKET dnrv6 = (PICMPV6PACKET)dnrv6_0;

				  // Initialize all packets.
				  PacketIpTcpInit(reset);
				  reset->tcp.Rst = 1;
				  reset->tcp.Ack = 1;
				  PacketIpIcmpInit(dnr);
				  dnr->icmp.Type = 3;         // Destination not reachable.
				  dnr->icmp.Code = 3;         // Port not reachable.
				  PacketIpv6TcpInit(resetv6);
				  resetv6->tcp.Rst = 1;
				  resetv6->tcp.Ack = 1;
				  PacketIpv6Icmpv6Init(dnrv6);
				  dnrv6->ipv6.Length = htons(sizeof(WINDIVERT_ICMPV6HDR) + 4 +
					  sizeof(WINDIVERT_IPV6HDR) + sizeof(WINDIVERT_TCPHDR));
				  dnrv6->icmpv6.Type = 1;     // Destination not reachable.
				  dnrv6->icmpv6.Code = 4;     // Port not reachable.

				  ///////////////////////////////////////////////////////////////////////////
				  char filter[255];
				  GetFilter(&filter[0]);

				  // Divert traffic matching the filter:
				  handle = WinDivertOpen(filter, WINDIVERT_LAYER_NETWORK, priority, 0); //WINDIVERT_FLAG_SNIFF
				  if (handle == INVALID_HANDLE_VALUE)
				  {
					  if (GetLastError() == ERROR_INVALID_PARAMETER)
					  {
						  MessageBox::Show("error: filter syntax error");
						  //fprintf(stderr, "error: filter syntax error\n");
						  exit(EXIT_FAILURE);
					  }
					  MessageBox::Show("error: failed to open the network device");
					  //fprintf(stderr, "error: failed to open the WinDivert device (%d)\n",
					  // GetLastError());
					  exit(EXIT_FAILURE);
				  }



				  while (TRUE)
				  {
					  for(int i=0; i<5;i++)
						  strcpy(text[i],"  ");

					  // Read a matching packet.
					  if (!WinDivertRecv(handle, packet, sizeof(packet), &recv_addr, &packet_len))
					  {						  					
						  continue;
					  }

					  // Print info about the matching packet.
					  WinDivertHelperParsePacket(packet, packet_len, &ip_header,
						  &ipv6_header, &icmp_header, &icmpv6_header, &tcp_header,
						  &udp_header, NULL, &payload_len);
					  if (ip_header == NULL && ipv6_header == NULL)
					  {
						  continue;
					  }
#pragma region ip
					  if (ip_header != NULL)
					  {
						  UINT8 *src_addr = (UINT8 *)&ip_header->SrcAddr;
						  UINT8 *dst_addr = (UINT8 *)&ip_header->DstAddr;

						  sprintf(text[0],"%u.%u.%u.%u",src_addr[0], src_addr[1], src_addr[2], src_addr[3]);
						  sprintf(text[1],"%u.%u.%u.%u",dst_addr[0], dst_addr[1], dst_addr[2], dst_addr[3]);
						  strcpy(text[2],"IPv4");



					  }
#pragma endregion
#pragma region ipv6
					  if (ipv6_header != NULL)
					  {
						  UINT16 *src_addr = (UINT16 *)&ipv6_header->SrcAddr;
						  UINT16 *dst_addr = (UINT16 *)&ipv6_header->DstAddr;
						  //fputs("ipv6.SrcAddr=", stdout);						 
						  int j=0;		 
						  for (i = 0; i < 8; i++)
						  {
							  j+=sprintf(text[0]+j,"%x%c", ntohs(src_addr[i]), (i == 7? ' ': ':'));
						  } 
						  j=0;

						  fputs(" ipv6.DstAddr=", stdout);
						  for (i = 0; i < 8; i++)
						  {
							  j+=sprintf(text[1]+j,"%x%c", ntohs(dst_addr[i]), (i == 7? ' ': ':'));
						  }
						  strcpy(text[2],"IPv6");


					  }
#pragma endregion
#pragma region icmp
					  if (icmp_header != NULL)
					  {
						  sprintf(text[3],"icmp.Type=%u ",icmp_header->Type);
						  sprintf(text[4],"icmp.Code=%u ",icmp_header->Code);
						  strcpy(text[2],"ICMP");
						  // Simply drop ICMP
					  }
#pragma endregion
#pragma region icmpv6

					  if (icmpv6_header != NULL)
					  {
						  sprintf(text[3],"icmpv6.Type=%u ",icmpv6_header->Type);
						  sprintf(text[4],"icmpv6.Code=%u ",icmpv6_header->Code);
						  strcpy(text[2],"ICMPv6");
						  // Simply drop ICMPv6
					  }
#pragma endregion
#pragma region tcp

					  if (tcp_header != NULL)
					  {
						  strcpy(text[2],"TCP");
						  sprintf(text[3],"tcp.SrcPort=%u ",ntohs(tcp_header->SrcPort));
						  sprintf(text[4],"tcp.DstPort=%u ",ntohs(tcp_header->DstPort));

						  // printf("tcp.SrcPort=%u tcp.DstPort=%u tcp.Flags=",
						  // ntohs(tcp_header->SrcPort), ntohs(tcp_header->DstPort));
						  char *ff;						
						  if (tcp_header->Fin)
						  {
							  ff=strdup("[FIN]");							
						  }
						  if (tcp_header->Rst)
						  {
							  ff=strdup("[RST]");							 
						  }
						  if (tcp_header->Urg)
						  {
							  ff=strdup("[URG]"); 
						  }
						  if (tcp_header->Syn)
						  {
							  ff=strdup("[SYN]"); 
						  }
						  if (tcp_header->Psh)
						  {
							  ff=strdup("[PSH]"); 
						  }
						  if (tcp_header->Ack)
						  {
							  ff=strdup("[ACK]"); 
						  }

						  strcat(text[4],ff);

						  delete[] ff;

						  if (ip_header != NULL)
						  {
							  reset->ip.SrcAddr = ip_header->DstAddr;
							  reset->ip.DstAddr = ip_header->SrcAddr;
							  reset->tcp.SrcPort = tcp_header->DstPort;
							  reset->tcp.DstPort = tcp_header->SrcPort;
							  reset->tcp.SeqNum = 
								  (tcp_header->Ack? tcp_header->AckNum: 0);
							  reset->tcp.AckNum =
								  (tcp_header->Syn?
								  htonl(ntohl(tcp_header->SeqNum) + 1):
								  htonl(ntohl(tcp_header->SeqNum) + payload_len));

							  WinDivertHelperCalcChecksums((PVOID)reset, sizeof(TCPPACKET),
								  0);

							  memcpy(&send_addr, &recv_addr, sizeof(send_addr));
							  send_addr.Direction = !recv_addr.Direction; 
							  if (!WinDivertSend(handle, (PVOID)reset, sizeof(TCPPACKET),
								  &send_addr, NULL))
							  {
								  fprintf(stderr, "warning: failed to send TCP reset (%d)\n",
									  GetLastError());
							  }
						  }

						  if (ipv6_header != NULL)
						  {
							  memcpy(resetv6->ipv6.SrcAddr, ipv6_header->DstAddr,
								  sizeof(resetv6->ipv6.SrcAddr));
							  memcpy(resetv6->ipv6.DstAddr, ipv6_header->SrcAddr,
								  sizeof(resetv6->ipv6.DstAddr));
							  resetv6->tcp.SrcPort = tcp_header->DstPort;
							  resetv6->tcp.DstPort = tcp_header->SrcPort;
							  resetv6->tcp.SeqNum =
								  (tcp_header->Ack? tcp_header->AckNum: 0);
							  resetv6->tcp.AckNum =
								  (tcp_header->Syn?
								  htonl(ntohl(tcp_header->SeqNum) + 1):
								  htonl(ntohl(tcp_header->SeqNum) + payload_len));

							  WinDivertHelperCalcChecksums((PVOID)resetv6,
								  sizeof(TCPV6PACKET), 0);

							  memcpy(&send_addr, &recv_addr, sizeof(send_addr));
							  send_addr.Direction = !recv_addr.Direction;
							  if (!WinDivertSend(handle, (PVOID)resetv6, sizeof(TCPV6PACKET),
								  &send_addr, NULL))
							  {
								  MessageBox::Show("warning: failed to send TCP (IPV6) --reset seans");

							  }
						  }
					  }

#pragma endregion
#pragma region udp
					  if (udp_header != NULL)
					  {

						  strcpy(text[2],"UDP");
						  sprintf(text[3],"udp.SrcPort=%u ",ntohs(udp_header->SrcPort));
						  sprintf(text[4],"udp.DstPort=%u ",ntohs(udp_header->DstPort));						

						  if (ip_header != NULL)
						  {
							  // NOTE: For some ICMP error messages, WFP does not seem to
							  //       support INBOUND injection.  As a work-around, we
							  //       always inject OUTBOUND.
							  UINT icmp_length = ip_header->HdrLength*sizeof(UINT32) + 8;
							  memcpy(dnr->data, ip_header, icmp_length);
							  icmp_length += sizeof(ICMPPACKET);
							  dnr->ip.Length = htons((UINT16)icmp_length);
							  dnr->ip.SrcAddr = ip_header->DstAddr;
							  dnr->ip.DstAddr = ip_header->SrcAddr;

							  WinDivertHelperCalcChecksums((PVOID)dnr, icmp_length, 0);

							  memcpy(&send_addr, &recv_addr, sizeof(send_addr));
							  send_addr.Direction = WINDIVERT_DIRECTION_OUTBOUND;
							  if (!WinDivertSend(handle, (PVOID)dnr, icmp_length, &send_addr,
								  NULL))
							  {
								  fprintf(stderr, "warning: failed to send ICMP message "
									  "(%d)\n", GetLastError());
							  }
						  }

						  if (ipv6_header != NULL)
						  {
							  UINT icmpv6_length = sizeof(WINDIVERT_IPV6HDR) +
								  sizeof(WINDIVERT_TCPHDR);
							  memcpy(dnrv6->data, ipv6_header, icmpv6_length);
							  icmpv6_length += sizeof(ICMPV6PACKET);
							  memcpy(dnrv6->ipv6.SrcAddr, ipv6_header->DstAddr,
								  sizeof(dnrv6->ipv6.SrcAddr));
							  memcpy(dnrv6->ipv6.DstAddr, ipv6_header->SrcAddr,
								  sizeof(dnrv6->ipv6.DstAddr));

							  WinDivertHelperCalcChecksums((PVOID)dnrv6, icmpv6_length, 0);

							  memcpy(&send_addr, &recv_addr, sizeof(send_addr));
							  send_addr.Direction = WINDIVERT_DIRECTION_OUTBOUND;
							  if (!WinDivertSend(handle, (PVOID)dnrv6, icmpv6_length,
								  &send_addr, NULL))
							  {
								  MessageBox::Show("warning: failed to send ICMPv6 message");

							  }
						  }
					  }
#pragma endregion 
					  /////////////////check
					  array<String^>^stra=gcnew array<String^>(5);
					  for(i=0;i<5;i++)
						  stra[i]=gcnew String(text[i]);
					  ListViewItem^ itm=gcnew System::Windows::Forms ::ListViewItem(stra);
					  raw->Put(itm);


				  }

			  }
	private: void ClearBlockTable()
			 {
				 for each (KeyValuePair< IPAddress^,Block^> var in blockTable)
				 {
					 if(var.Value->time->AddMinutes((double)10)<DateTime::Now)
						 blockTable->Remove(var.Key);
					 if(var.Value->count>10)
					 {

						 if(!table->Contains(var.Key))						 
						 {
							 table->Add(var.Key);
							 array<String^>^st=gcnew array<String^>(2);

							 st[0]=gcnew String(var.Key->ToString());
							 st[1]=gcnew String("system");

							 ListViewItem^ itm=gcnew System::Windows::Forms ::ListViewItem(st);						 
							 iptables->Items->Add(itm);
							 IsSysytemBlock=true;
							 StartBlock();
						 }

					 }
				 }

			 }

	private: System::Void btAddIp_Click(System::Object^  sender, System::EventArgs^  e) {
				 try
				 {
					 if(String::IsNullOrEmpty(textBoxIP1->Text))
						 return;

					 System::Net::IPAddress^ ip=System::Net::IPAddress::Parse(textBoxIP1->Text);



					 table->Add(ip);

					 array<String^>^st=gcnew array<String^>(2);

					 st[0]=gcnew String(ip->ToString());
					 st[1]=gcnew String("user");

					 ListViewItem^ itm=gcnew System::Windows::Forms ::ListViewItem(st);
					 iptables->Items->Add(itm);
					 textBoxIP1->Text="";


				 }
				 catch(char* ex)
				 {
					 MessageBox::Show("error: wrong ip");

				 }

			 }
	private: System::Void button1_Click_1(System::Object^  sender, System::EventArgs^  e) {
				 if(tabControl1->SelectedIndex==0)
				 listView1->Items->Clear();
				 else
					 if(tabControl1->SelectedIndex==2)
					listView2->Items->Clear();
			 }
	private: System::Void MyFireWall_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e) 
			 {
				 try{
					 if(handle!=NULL)
						 DivertClose(handle);
					 if(t!=nullptr)
						 t->Abort();
					 if(t2!=nullptr)
						 t2->Abort();
					 GlobalDevice->Close();
					 GlobalDevice->StopCapture();


				 }
				 catch(Exception^ e)
				 {

				 }



			 }
	private: System::Void tabControl1_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e) {
				  if(tabControl1->SelectedIndex==1)
					  button1->Enabled=false;
				  else
					   button1->Enabled=true;

			 }
};


#pragma region func




	/*
	* Initialize an ICMPPACKET.
	*/
	static void PacketIpIcmpInit(PICMPPACKET packet)
	{
		memset(packet, 0, sizeof(ICMPPACKET));
		PacketIpInit(&packet->ip);
		packet->ip.Protocol = IPPROTO_ICMP;
	}

	/*
	* Initialize a PACKETV6.
	*/
	static void PacketIpv6Init(PWINDIVERT_IPV6HDR packet)
	{
		memset(packet, 0, sizeof(WINDIVERT_IPV6HDR));
		packet->Version = 6;
		packet->HopLimit = 64;
	}

	/*
	* Initialize a TCPV6PACKET.
	*/
	static void PacketIpv6TcpInit(PTCPV6PACKET packet)
	{
		memset(packet, 0, sizeof(TCPV6PACKET));
		PacketIpv6Init(&packet->ipv6);
		packet->ipv6.Length = htons(sizeof(WINDIVERT_TCPHDR));
		packet->ipv6.NextHdr = IPPROTO_TCP;
		packet->tcp.HdrLength = sizeof(WINDIVERT_TCPHDR) / sizeof(UINT32);
	}

	/*
	* Initialize an ICMP PACKET.
	*/
	static void PacketIpv6Icmpv6Init(PICMPV6PACKET packet)
	{
		memset(packet, 0, sizeof(ICMPV6PACKET));
		PacketIpv6Init(&packet->ipv6);
		packet->ipv6.NextHdr = IPPROTO_ICMPV6;
	}

	/*
	* Initialize a PACKET.
	*/
	static void PacketIpInit(PWINDIVERT_IPHDR packet)
	{
		memset(packet, 0, sizeof(WINDIVERT_IPHDR));
		packet->Version = 4;
		packet->HdrLength = sizeof(WINDIVERT_IPHDR) / sizeof(UINT32);
		packet->Id = ntohs(0xDEAD);
		packet->TTL = 64;
	}

	/*
	* Initialize a TCPPACKET.
	*/
	static void PacketIpTcpInit(PTCPPACKET packet)
	{
		memset(packet, 0, sizeof(TCPPACKET));
		PacketIpInit(&packet->ip);
		packet->ip.Length = htons(sizeof(TCPPACKET));
		packet->ip.Protocol = IPPROTO_TCP;
		packet->tcp.HdrLength = sizeof(WINDIVERT_TCPHDR) / sizeof(UINT32);
	}
#pragma endregion
}

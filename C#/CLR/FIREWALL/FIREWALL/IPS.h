#pragma once
ref class IPS
{
public:
	System::String^ verdict;
	System::Net::IPAddress^ ip1;

	IPS(System::String^ ipw, System::Net::IPAddress^ ip_1);
};

ref class Block
{
public :
	System::Net::IPAddress^ ip;
	int count;
	System::DateTime^ time;
	Block(System::Net::IPAddress^ ip);
	void Add();
};
#include "IPS.h"


IPS::IPS(System::String^ ipw, System::Net::IPAddress^ ip_1)
{
	ip1=ip_1;

	verdict=ipw;
}

Block::Block(System::Net::IPAddress^ ip1)
{
	ip=ip1;
	count=0;
	time=System::DateTime::Now;
}

void Block::Add()
{
	System::DateTime tmp=time->AddMinutes(2);
	if(System::DateTime::Compare(System::DateTime::Now,tmp)<0)
		count++;
	else
	{time=System::DateTime::Now; count=0;}

}
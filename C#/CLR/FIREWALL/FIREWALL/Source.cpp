#include "MyFireWall.h"
using namespace FIREWALL;

[STAThreadAttribute]
int main()
{
	MyFireWall fm;
	fm.ShowDialog();
	return 0;
}

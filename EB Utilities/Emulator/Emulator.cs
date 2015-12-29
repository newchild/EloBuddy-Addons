using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    public class Emulator
    {
	    private Registers _registers;
	    private Clock _clock;
	    public Emulator()
	    {
		    _registers = new Registers();
			_clock = new Clock();
	    }
    }
}

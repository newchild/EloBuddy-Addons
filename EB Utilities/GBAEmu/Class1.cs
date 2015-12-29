using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBAEmu
{
    public class Emulator
    {
	    private Clock _clock;
	    private Registers _registers;

	    public Emulator()
	    {
		    _clock = new Clock();
			_registers = new Registers();
	    }

	    private enum Flags
	    {
		    Carry = 0x10,
			HalfCarry = 0x20,
			Operation = 0x40,
			Zero = 0x80
	    }

	    private class Clock
	    {
		    public int m = 0;
			public int t = 0;
		}
		private class Registers
		{
			public int a = 0;
			public int b = 0;
			public int c = 0;
			public  int d = 0;
			public  int e = 0;
			public  int h = 0;
			public  int l = 0;
			public int f = 0;
			public int pc = 0;
			public int sp = 0;
			public int m = 0;
			public int t = 0;
		}

	}
}

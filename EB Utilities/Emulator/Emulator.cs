using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    public class Emulator
    {
	    private Registers _registers;
	    private Clock _clock;
	    private MMU _mmu;
	    public Emulator(string FilePath)
	    {
		    _registers = new Registers();
			_clock = new Clock();
			FileStream x = new FileStream(FilePath, FileMode.Open);
			MemoryStream test = new MemoryStream();
			x.CopyTo(test);
			_mmu = new MMU(test);
	    }
    }
}

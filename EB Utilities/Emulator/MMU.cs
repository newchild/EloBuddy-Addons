using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
	class MMU
	{
		private bool _StartUpCompleted = false;
		private byte[] _BIOS = new byte[0xff];
		private byte[] _ROM = new byte[0x7fff];
		private byte[] _WRAM = new byte[0x3dff];
		private byte[] _ERAM = new byte[0x1fff];
		private byte[] _ZRAM = new byte[0x7f];
		public MMU(MemoryStream ROM)
		{
			_ROM = ROM.ToArray();
		}

		public byte ReadByte(int adress, Registers registers)
		{
			switch (adress & 0xF000)
			{
				case 0x0:
					if (!_StartUpCompleted)
					{
						if (adress < 0x0100)
							return _BIOS[adress];
						if (registers.PC == 0x0100)
							_StartUpCompleted = !_StartUpCompleted;
					}
					return _ROM[adress];
				case 0x1000:
				case 0x2000:
				case 0x3000:
					return _ROM[adress];
				case 0x4000:
				case 0x5000:
				case 0x6000:
				case 0x7000:
					return _ROM[adress];
				case 0x8000:
				case 0x9000:
					return 0; //Here must be VRAM!
				case 0xA000:
				case 0xB000:
					return _ERAM[adress & 0x1FFF];
				case 0xC000:
				case 0xD000:
					return _WRAM[adress & 0x1FFF];
				case 0xE000:
					return _WRAM[adress & 0x1FFF];
				case 0xF000:
					switch (adress & 0x0F00)
					{
						case 0x000:
						case 0x100:
						case 0x200:
						case 0x300:
						case 0x400:
						case 0x500:
						case 0x600:
						case 0x700:
						case 0x800:
						case 0x900:
						case 0xA00:
						case 0xB00:
						case 0xC00:
						case 0xD00:
							return _WRAM[adress & 0x1FFF];
						case 0xE00:
							if (adress < 0xFEA0)
								return 0; //Here must be OAM!
							return 0;
						case 0xF00:
							if (adress >= 0xFF80)
								return _ZRAM[adress & 0x7F];
							return 0; //Here must be the I/O handling
						default:
							throw new Exception("Out of Memory Exception");

					}
				default:
					throw new Exception("Out of Memory Exception");
			}
		}

		public void WriteByte(int adress, Registers registers, byte value)
		{
			switch (adress & 0xF000)
			{
				case 0x0:
					if (!_StartUpCompleted)
					{
						if (adress < 0x0100)
							 _BIOS[adress] = value;
						if (registers.PC == 0x0100)
							_StartUpCompleted = !_StartUpCompleted;
					}
					_ROM[adress] = value;
					break;
				case 0x1000:
				case 0x2000:
				case 0x3000:
					_ROM[adress] = value;
					break;
				case 0x4000:
				case 0x5000:
				case 0x6000:
				case 0x7000:
					_ROM[adress] = value;
					break;
				case 0x8000:
				case 0x9000:
					return; //Here must be VRAM!
				case 0xA000:
				case 0xB000:
					_ERAM[adress & 0x1FFF] = value;
					break;
				case 0xC000:
				case 0xD000:
					_WRAM[adress & 0x1FFF] = value;
					break;
				case 0xE000:
					_WRAM[adress & 0x1FFF] = value;
					break;
				case 0xF000:
					switch (adress & 0x0F00)
					{
						case 0x000:
						case 0x100:
						case 0x200:
						case 0x300:
						case 0x400:
						case 0x500:
						case 0x600:
						case 0x700:
						case 0x800:
						case 0x900:
						case 0xA00:
						case 0xB00:
						case 0xC00:
						case 0xD00:
							_WRAM[adress & 0x1FFF] = value;
							break;
						case 0xE00:
							if (adress < 0xFEA0)
								return; //Here must be OAM!
							return;
						case 0xF00:
							if (adress >= 0xFF80)
								_ZRAM[adress & 0x7F] = value;
							return; //Here must be the I/O handling
						default:
							throw new Exception("Out of Memory Exception");

					}
					break;
				default:
					throw new Exception("Out of Memory Exception");
			}
		}

		public void WriteWord(int adress, Registers registers, int value)
		{
			return;
		}

		public int ReadWord(int adress, Registers registers)
		{
			return ReadByte(adress, registers) + (ReadByte(adress + 1, registers) << 8);
		}
	}
}

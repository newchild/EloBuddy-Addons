using EBPy;
using EloBuddy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PythonLoader
{
	public partial class Form1 : Form
	{
		private EBPy.ScriptHandler _handler = new EBPy.ScriptHandler();
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog x = new OpenFileDialog();
			x.ShowDialog();
			_handler.LoadScript(x.SafeFileName, File.ReadAllText(x.FileName));
			dataGridView1.DataSource = _handler.loadedScripts;
			_handler.RunAllEnabledScripts();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}

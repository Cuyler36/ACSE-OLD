using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACSE
{
    public partial class TuneEditorForm : Form
    {
        private byte[] Decoded_Town_Tune = new byte[16];

        public TuneEditorForm()
        {
            InitializeComponent();
        }

        private void DecodeTune(byte[] townTune)
        {
            for (int i = 0; i < 8; i++)
            {
                byte A = (byte)((townTune[i] & 0xF0) >> 4);
                byte B = (byte)(townTune[i] & 0x0F);
                Decoded_Town_Tune[i * 2] = A;
                Decoded_Town_Tune[i * 2 + 1] = B;
            }
        }

        private byte[] EncodeTune()
        {
            byte[] Encoded_Town_Tune = new byte[8];
            for (int i = 0; i < 16; i += 2)
                Encoded_Town_Tune[i / 2] = (byte)(Decoded_Town_Tune[i] << 4 + Decoded_Town_Tune[i + 1]);
            return Encoded_Town_Tune;
        }

        
    }
}

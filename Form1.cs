using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Webcam
{
    public partial class Form1 : Form
    {
        VideoCaptureDevice frame;
        FilterInfoCollection Devices;

        public Form1()
        {
            InitializeComponent();
        }

        void Start_cam()
        {
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            frame = new VideoCaptureDevice(Devices[0].MonikerString);
            frame.NewFrame += new AForge.Video.NewFrameEventHandler(NewFrame_event);
            frame.Start();
        }
        string output;
        void NewFrame_event(object send,NewFrameEventArgs e)
        {
            try {
                pictureBox1.Image = (Image)e.Frame.Clone();
            } catch(Exception ex) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Start_cam();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frame.Stop();
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
            output = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (output != "" && pictureBox1.Image != null)
            {
                pictureBox1.Image.Save(output+"\\Image.png");
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frame.Stop();
        }
    }
}

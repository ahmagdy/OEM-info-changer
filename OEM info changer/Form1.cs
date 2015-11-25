using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;


namespace OEM_info_changer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string imgsrc;
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select the patch of image";
            ofd.Filter = "Images Only|*.bmp*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Load(ofd.FileName);
                imgsrc = ofd.FileName.ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey mykey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\OEMInformation", true);
            mykey.SetValue("Manufacturer", textBox1.Text);
            mykey.SetValue("Model", textBox2.Text);
            mykey.SetValue("SupportURL", textBox4.Text);
            mykey.SetValue("SupportHours", textBox5.Text);
            mykey.SetValue("SupportPhone", textBox3.Text);

            if (imgsrc == null)
            {
                mykey.SetValue("Logo", "");
            }
            else
            {
                mykey.SetValue("Logo", imgsrc);
            }

            mykey.Close();
            MessageBox.Show("Done :D", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey mykey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\OEMInformation", true);
                textBox1.Text = mykey.GetValue("Manufacturer").ToString();
                textBox2.Text = mykey.GetValue("Model").ToString();
                textBox4.Text = mykey.GetValue("SupportURL").ToString();
                textBox5.Text = mykey.GetValue("SupportHours").ToString();
                textBox3.Text = mykey.GetValue("SupportPhone").ToString();
                if (mykey.GetValue("Logo").ToString() == "")
                {

                }
                else
                {
                    pictureBox1.Load(mykey.GetValue("Logo").ToString());
                }
            }
            catch
            {
                return;
            }

        }


    }
}

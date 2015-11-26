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
        private void imgLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select the patch of image";
            ofd.Filter = "Images Only|*.bmp*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                OEMlogo.Load(ofd.FileName);
                imgsrc = ofd.FileName.ToString();
            }

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            RegistryKey oemKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\OEMInformation", true);
            oemKey.SetValue("Manufacturer", txtManf.Text);
            oemKey.SetValue("Model", txtModel.Text);
            oemKey.SetValue("SupportURL", txtUrl.Text);
            oemKey.SetValue("SupportHours", txths.Text);
            oemKey.SetValue("SupportPhone", txtPhone.Text);

            if (imgsrc == null)
            {
                oemKey.SetValue("Logo", "");
            }
            else
            {
                oemKey.SetValue("Logo", imgsrc);
            }

            oemKey.Close();
            MessageBox.Show(string.Format("OEM Information has been changed  Manufacturer: {0} and Model: {1} .....", txtManf.Text, txtModel.Text), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey mykey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\OEMInformation", true);
                txtManf.Text = mykey.GetValue("Manufacturer").ToString();
                txtModel.Text = mykey.GetValue("Model").ToString();
                txtUrl.Text = mykey.GetValue("SupportURL").ToString();
                txths.Text = mykey.GetValue("SupportHours").ToString();
                txtPhone.Text = mykey.GetValue("SupportPhone").ToString();
                if (mykey.GetValue("Logo").ToString() == "")
                {

                }
                else
                {
                    OEMlogo.Load(mykey.GetValue("Logo").ToString());
                }
            }
            catch
            {
                return;
            }

        }


    }
}

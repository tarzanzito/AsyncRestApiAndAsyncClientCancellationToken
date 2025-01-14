
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace WinFormExample
{
    internal static class Program
    {

        #region Public static

        [STAThread]
        public static int Main()
        {
            int retValue = 0;

            try
            {
                Application.EnableVisualStyles(); //native
                Application.SetCompatibleTextRenderingDefault(false);  //native
                Application.Run(new Form1()); //native
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplication Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                retValue = 1;
            }

            return retValue;
        }

        #endregion
    }
}
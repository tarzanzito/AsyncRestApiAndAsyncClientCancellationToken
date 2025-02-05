﻿
using System;
using System.Configuration;
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
                string? apiUrl = ConfigurationManager.AppSettings["ApiUrl"] ??
                    throw new Exception("Entry 'ApiUrl' not found in 'app.config'");
                
                //if (Uri.IsWellFormedUriString(apiUrl, UriKind.Absolute))

                    Application.EnableVisualStyles(); //native
                Application.SetCompatibleTextRenderingDefault(false);  //native
                Application.Run(new Form1(apiUrl)); //native
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
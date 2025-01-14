namespace WinFormExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonRun = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            textBoxParam = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            textBoxResponse = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // buttonRun
            // 
            buttonRun.Location = new System.Drawing.Point(426, 16);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new System.Drawing.Size(99, 32);
            buttonRun.TabIndex = 0;
            buttonRun.Text = "Run Action";
            buttonRun.UseVisualStyleBackColor = true;
            buttonRun.Click += ButtonRun_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new System.Drawing.Point(426, 54);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(99, 32);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel Action";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(102, 85);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(130, 15);
            label1.TabIndex = 2;
            label1.Text = "Free Text (async prove):";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(102, 103);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(264, 23);
            textBox1.TabIndex = 3;
            // 
            // progressBar1
            // 
            progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            progressBar1.Location = new System.Drawing.Point(0, 0);
            progressBar1.MarqueeAnimationSpeed = 10;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(537, 10);
            progressBar1.TabIndex = 4;
            // 
            // textBoxParam
            // 
            textBoxParam.Location = new System.Drawing.Point(102, 22);
            textBoxParam.Name = "textBoxParam";
            textBoxParam.Size = new System.Drawing.Size(304, 23);
            textBoxParam.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 25);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(60, 15);
            label2.TabIndex = 6;
            label2.Text = "Send Text:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 161);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(83, 15);
            label3.TabIndex = 7;
            label3.Text = "Call Response:";
            // 
            // textBoxResponse
            // 
            textBoxResponse.Location = new System.Drawing.Point(102, 158);
            textBoxResponse.Name = "textBoxResponse";
            textBoxResponse.ReadOnly = true;
            textBoxResponse.Size = new System.Drawing.Size(423, 23);
            textBoxResponse.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(537, 190);
            Controls.Add(textBoxResponse);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBoxParam);
            Controls.Add(progressBar1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonRun);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "Form1";
            Activated += Form1_Activated;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBoxParam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxResponse;
    }
}
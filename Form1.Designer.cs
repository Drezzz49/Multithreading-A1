namespace Multithreading_A1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAssignment2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeadlock = new System.Windows.Forms.Button();
            this.btnDeadlockFix = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxLog);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 525);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Event Log";
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(6, 19);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(480, 498);
            this.listBoxLog.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxItems);
            this.groupBox2.Location = new System.Drawing.Point(510, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 525);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Items";
            // 
            // listBoxItems
            // 
            this.listBoxItems.FormattingEnabled = true;
            this.listBoxItems.Location = new System.Drawing.Point(6, 19);
            this.listBoxItems.Name = "listBoxItems";
            this.listBoxItems.Size = new System.Drawing.Size(356, 498);
            this.listBoxItems.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(18, 543);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(130, 40);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "A1 Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(18, 586);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(130, 40);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "A1 Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAssignment2
            // 
            this.btnAssignment2.Location = new System.Drawing.Point(154, 543);
            this.btnAssignment2.Name = "btnAssignment2";
            this.btnAssignment2.Size = new System.Drawing.Size(107, 40);
            this.btnAssignment2.TabIndex = 4;
            this.btnAssignment2.Text = "A2 Race Condition";
            this.btnAssignment2.UseVisualStyleBackColor = true;
            this.btnAssignment2.Click += new System.EventHandler(this.btnAssignment2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(267, 543);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "A2 Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDeadlock
            // 
            this.btnDeadlock.Location = new System.Drawing.Point(154, 589);
            this.btnDeadlock.Name = "btnDeadlock";
            this.btnDeadlock.Size = new System.Drawing.Size(107, 40);
            this.btnDeadlock.TabIndex = 6;
            this.btnDeadlock.Text = "A2 Deadlock";
            this.btnDeadlock.UseVisualStyleBackColor = true;
            this.btnDeadlock.Click += new System.EventHandler(this.btnDeadlock_Click);
            // 
            // btnDeadlockFix
            // 
            this.btnDeadlockFix.Location = new System.Drawing.Point(267, 589);
            this.btnDeadlockFix.Name = "btnDeadlockFix";
            this.btnDeadlockFix.Size = new System.Drawing.Size(107, 40);
            this.btnDeadlockFix.TabIndex = 7;
            this.btnDeadlockFix.Text = "A2 Deadlock Fix";
            this.btnDeadlockFix.UseVisualStyleBackColor = true;
            this.btnDeadlockFix.Click += new System.EventHandler(this.btnDeadlockFix_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 638);
            this.Controls.Add(this.btnDeadlockFix);
            this.Controls.Add(this.btnDeadlock);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAssignment2);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Equipment Loading Managment System";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.ListBox listBoxItems;
        private System.Windows.Forms.Button btnAssignment2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDeadlock;
        private System.Windows.Forms.Button btnDeadlockFix;
    }
}


namespace WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            CapacityBox = new TextBox();
            NumberItemsBox = new TextBox();
            SeedBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            GenerationBox = new TextBox();
            label4 = new Label();
            label5 = new Label();
            ResultsBox = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(41, 186);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // CapacityBox
            // 
            CapacityBox.Location = new Point(41, 142);
            CapacityBox.Name = "CapacityBox";
            CapacityBox.Size = new Size(131, 23);
            CapacityBox.TabIndex = 1;
            CapacityBox.TextChanged += CapacityBox_TextChanged;
            // 
            // NumberItemsBox
            // 
            NumberItemsBox.Location = new Point(41, 98);
            NumberItemsBox.Name = "NumberItemsBox";
            NumberItemsBox.Size = new Size(131, 23);
            NumberItemsBox.TabIndex = 2;
            NumberItemsBox.TextChanged += textBox2_TextChanged;
            // 
            // SeedBox
            // 
            SeedBox.Location = new Point(41, 55);
            SeedBox.Name = "SeedBox";
            SeedBox.Size = new Size(131, 23);
            SeedBox.TabIndex = 3;
            SeedBox.TextChanged += textBox3_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 37);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 4;
            label1.Text = "Ziarno Generatora";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 80);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 5;
            label2.Text = "Ilość przedmiotów";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 124);
            label3.Name = "label3";
            label3.Size = new Size(109, 15);
            label3.TabIndex = 6;
            label3.Text = "Pojemność plecaka";
            // 
            // GenerationBox
            // 
            GenerationBox.Location = new Point(269, 55);
            GenerationBox.Multiline = true;
            GenerationBox.Name = "GenerationBox";
            GenerationBox.ReadOnly = true;
            GenerationBox.Size = new Size(243, 425);
            GenerationBox.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(360, 37);
            label4.Name = "label4";
            label4.Size = new Size(152, 15);
            label4.TabIndex = 8;
            label4.Text = "Wygenerowane przedmioty";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(186, 219);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 9;
            label5.Text = "Wyniki";
            label5.Click += label5_Click;
            // 
            // ResultsBox
            // 
            ResultsBox.Location = new Point(41, 237);
            ResultsBox.Multiline = true;
            ResultsBox.Name = "ResultsBox";
            ResultsBox.ReadOnly = true;
            ResultsBox.Size = new Size(188, 243);
            ResultsBox.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 498);
            Controls.Add(ResultsBox);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(GenerationBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SeedBox);
            Controls.Add(NumberItemsBox);
            Controls.Add(CapacityBox);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Algorytm Plecakowy";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox CapacityBox;
        private TextBox NumberItemsBox;
        private TextBox SeedBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox GenerationBox;
        private Label label4;
        private Label label5;
        private TextBox ResultsBox;
    }
}

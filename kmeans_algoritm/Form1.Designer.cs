namespace kmeans_algoritm
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_vecCount = new System.Windows.Forms.TextBox();
            this.txt_vecRange = new System.Windows.Forms.TextBox();
            this.txt_Result = new System.Windows.Forms.RichTextBox();
            this.txt_clusterCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Epsilon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnClusterization = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_minEqual = new System.Windows.Forms.TextBox();
            this.txt_countIteration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.btnPaint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(9, 294);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(166, 38);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Генерация значений";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Количество векторов в кластере";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Размерность векторов";
            // 
            // txt_vecCount
            // 
            this.txt_vecCount.Location = new System.Drawing.Point(290, 20);
            this.txt_vecCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_vecCount.MaxLength = 10;
            this.txt_vecCount.Name = "txt_vecCount";
            this.txt_vecCount.Size = new System.Drawing.Size(373, 22);
            this.txt_vecCount.TabIndex = 5;
            this.txt_vecCount.Text = "50";
            this.txt_vecCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txt_vecRange
            // 
            this.txt_vecRange.Location = new System.Drawing.Point(291, 46);
            this.txt_vecRange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_vecRange.MaxLength = 10;
            this.txt_vecRange.Name = "txt_vecRange";
            this.txt_vecRange.Size = new System.Drawing.Size(373, 22);
            this.txt_vecRange.TabIndex = 6;
            this.txt_vecRange.Text = "2";
            this.txt_vecRange.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txt_Result
            // 
            this.txt_Result.Location = new System.Drawing.Point(12, 406);
            this.txt_Result.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Result.Name = "txt_Result";
            this.txt_Result.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txt_Result.Size = new System.Drawing.Size(669, 230);
            this.txt_Result.TabIndex = 11;
            this.txt_Result.Text = "";
            this.txt_Result.TextChanged += new System.EventHandler(this.txt_Result_TextChanged);
            // 
            // txt_clusterCount
            // 
            this.txt_clusterCount.Location = new System.Drawing.Point(291, 72);
            this.txt_clusterCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_clusterCount.MaxLength = 10;
            this.txt_clusterCount.Name = "txt_clusterCount";
            this.txt_clusterCount.Size = new System.Drawing.Size(373, 22);
            this.txt_clusterCount.TabIndex = 14;
            this.txt_clusterCount.Text = "3";
            this.txt_clusterCount.TextChanged += new System.EventHandler(this.txt_clusterCount_TextChanged);
            this.txt_clusterCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Количество кластеров";
            // 
            // txt_Epsilon
            // 
            this.txt_Epsilon.Location = new System.Drawing.Point(291, 98);
            this.txt_Epsilon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Epsilon.MaxLength = 10;
            this.txt_Epsilon.Name = "txt_Epsilon";
            this.txt_Epsilon.Size = new System.Drawing.Size(373, 22);
            this.txt_Epsilon.TabIndex = 16;
            this.txt_Epsilon.Text = "100";
            this.txt_Epsilon.TextChanged += new System.EventHandler(this.txt_Epsilon_TextChanged);
            this.txt_Epsilon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Дельта";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.btnSaveFile);
            this.groupBox1.Controls.Add(this.btnReadFile);
            this.groupBox1.Controls.Add(this.btnClusterization);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_minEqual);
            this.groupBox1.Controls.Add(this.txt_countIteration);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_vecCount);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Controls.Add(this.txt_Epsilon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_vecRange);
            this.groupBox1.Controls.Add(this.txt_clusterCount);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(669, 343);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исходные данные";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(290, 258);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(219, 21);
            this.radioButton4.TabIndex = 28;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Все центры в одной области";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(291, 231);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(77, 21);
            this.radioButton3.TabIndex = 27;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "2 : 1 : 0";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(291, 204);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(225, 21);
            this.radioButton2.TabIndex = 26;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "В каждой области свой центр";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 179);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(184, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "Выбор начальных центров";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(290, 177);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(103, 21);
            this.radioButton1.TabIndex = 24;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Случайный";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(508, 294);
            this.btnSaveFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(155, 38);
            this.btnSaveFile.TabIndex = 23;
            this.btnSaveFile.Text = "Сохранить в файл";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(347, 294);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(155, 38);
            this.btnReadFile.TabIndex = 22;
            this.btnReadFile.Text = "Читать из файла";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnClusterization
            // 
            this.btnClusterization.Location = new System.Drawing.Point(181, 294);
            this.btnClusterization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClusterization.Name = "btnClusterization";
            this.btnClusterization.Size = new System.Drawing.Size(155, 38);
            this.btnClusterization.TabIndex = 21;
            this.btnClusterization.Text = "Кластеризация";
            this.btnClusterization.UseVisualStyleBackColor = true;
            this.btnClusterization.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Эпсилон, E";
            // 
            // txt_minEqual
            // 
            this.txt_minEqual.Location = new System.Drawing.Point(291, 150);
            this.txt_minEqual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_minEqual.MaxLength = 10;
            this.txt_minEqual.Name = "txt_minEqual";
            this.txt_minEqual.Size = new System.Drawing.Size(373, 22);
            this.txt_minEqual.TabIndex = 19;
            this.txt_minEqual.Text = "10";
            this.txt_minEqual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txt_countIteration
            // 
            this.txt_countIteration.Location = new System.Drawing.Point(291, 124);
            this.txt_countIteration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_countIteration.MaxLength = 10;
            this.txt_countIteration.Name = "txt_countIteration";
            this.txt_countIteration.Size = new System.Drawing.Size(373, 22);
            this.txt_countIteration.TabIndex = 18;
            this.txt_countIteration.Text = "10";
            this.txt_countIteration.TextChanged += new System.EventHandler(this.countIteration_TextChanged);
            this.txt_countIteration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Количество итераций";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(679, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Перед запуском алгоритма кластеризации, пожалуйста, введите исходные данные в пол" +
    "я на форме.";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(12, 387);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(58, 17);
            this.lblError.TabIndex = 17;
            this.lblError.Text = "ошибки";
            // 
            // btnPaint
            // 
            this.btnPaint.Location = new System.Drawing.Point(12, 642);
            this.btnPaint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPaint.Name = "btnPaint";
            this.btnPaint.Size = new System.Drawing.Size(201, 39);
            this.btnPaint.TabIndex = 19;
            this.btnPaint.Text = "Визуализация";
            this.btnPaint.UseVisualStyleBackColor = true;
            this.btnPaint.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(480, 642);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(201, 38);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 694);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPaint);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_Result);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "K-MEANS ALGORITHM";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_vecCount;
        private System.Windows.Forms.TextBox txt_vecRange;
        private System.Windows.Forms.RichTextBox txt_Result;
        private System.Windows.Forms.TextBox txt_clusterCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Epsilon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_minEqual;
        private System.Windows.Forms.TextBox txt_countIteration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPaint;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnClusterization;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}


namespace SpectroNamespace
{
    partial class Spectro
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 2D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 2D);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.observerComboBox = new System.Windows.Forms.ComboBox();
            this.lightSourceComboBox = new System.Windows.Forms.ComboBox();
            this.materialComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LightStrengthCounter = new System.Windows.Forms.Label();
            this.LightStrengthBar = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.clipInvisbleCheckBox = new System.Windows.Forms.CheckBox();
            this.objectSelectedMessage = new System.Windows.Forms.Label();
            this.MaterialChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.LightSourceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ObserverChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.fullSpectralScene1 = new SpectroNamespace.FullSpectralScene();
            this.rgbScene1 = new SpectroNamespace.RGBScene();
            this.spectroBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.animateCheckbox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyValueCounter = new System.Windows.Forms.Label();
            this.KeyValueBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.LightStrengthBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObserverChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spectroBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValueBar)).BeginInit();
            this.SuspendLayout();
            // 
            // observerComboBox
            // 
            this.observerComboBox.FormattingEnabled = true;
            this.observerComboBox.Location = new System.Drawing.Point(12, 29);
            this.observerComboBox.Name = "observerComboBox";
            this.observerComboBox.Size = new System.Drawing.Size(266, 21);
            this.observerComboBox.TabIndex = 0;
            this.observerComboBox.SelectedIndexChanged += new System.EventHandler(this.observerComboBox_SelectedIndexChanged);
            // 
            // lightSourceComboBox
            // 
            this.lightSourceComboBox.FormattingEnabled = true;
            this.lightSourceComboBox.Location = new System.Drawing.Point(12, 75);
            this.lightSourceComboBox.Name = "lightSourceComboBox";
            this.lightSourceComboBox.Size = new System.Drawing.Size(266, 21);
            this.lightSourceComboBox.TabIndex = 1;
            this.lightSourceComboBox.SelectedIndexChanged += new System.EventHandler(this.lightSourceComboBox_SelectedIndexChanged);
            // 
            // materialComboBox
            // 
            this.materialComboBox.FormattingEnabled = true;
            this.materialComboBox.Location = new System.Drawing.Point(12, 126);
            this.materialComboBox.Name = "materialComboBox";
            this.materialComboBox.Size = new System.Drawing.Size(266, 21);
            this.materialComboBox.TabIndex = 2;
            this.materialComboBox.SelectedIndexChanged += new System.EventHandler(this.materialComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Observer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Light Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Material";
            // 
            // LightStrengthCounter
            // 
            this.LightStrengthCounter.AutoSize = true;
            this.LightStrengthCounter.Location = new System.Drawing.Point(244, 216);
            this.LightStrengthCounter.Name = "LightStrengthCounter";
            this.LightStrengthCounter.Size = new System.Drawing.Size(13, 13);
            this.LightStrengthCounter.TabIndex = 13;
            this.LightStrengthCounter.Text = "0";
            // 
            // LightStrengthBar
            // 
            this.LightStrengthBar.Location = new System.Drawing.Point(15, 236);
            this.LightStrengthBar.Maximum = 100;
            this.LightStrengthBar.Name = "LightStrengthBar";
            this.LightStrengthBar.Size = new System.Drawing.Size(265, 45);
            this.LightStrengthBar.TabIndex = 12;
            this.LightStrengthBar.TickFrequency = 10;
            this.LightStrengthBar.Scroll += new System.EventHandler(this.LightStrengthBar_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Light Strength %";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(294, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Full Spectral Rendering";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(739, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "RGB Rendering";
            // 
            // clipInvisbleCheckBox
            // 
            this.clipInvisbleCheckBox.AutoSize = true;
            this.clipInvisbleCheckBox.Location = new System.Drawing.Point(15, 282);
            this.clipInvisbleCheckBox.Name = "clipInvisbleCheckBox";
            this.clipInvisbleCheckBox.Size = new System.Drawing.Size(82, 17);
            this.clipInvisbleCheckBox.TabIndex = 16;
            this.clipInvisbleCheckBox.Text = "Clip Invisble";
            this.clipInvisbleCheckBox.UseVisualStyleBackColor = true;
            this.clipInvisbleCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // objectSelectedMessage
            // 
            this.objectSelectedMessage.Location = new System.Drawing.Point(139, 110);
            this.objectSelectedMessage.Name = "objectSelectedMessage";
            this.objectSelectedMessage.Size = new System.Drawing.Size(139, 13);
            this.objectSelectedMessage.TabIndex = 17;
            this.objectSelectedMessage.Text = "Nothing Selected";
            this.objectSelectedMessage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MaterialChart
            // 
            this.MaterialChart.BackColor = System.Drawing.SystemColors.Control;
            chartArea7.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea7.AxisX.MajorGrid.Enabled = false;
            chartArea7.AxisY.LabelAutoFitMaxFontSize = 9;
            chartArea7.AxisY.MajorGrid.Enabled = false;
            chartArea7.BackColor = System.Drawing.SystemColors.Control;
            chartArea7.Name = "ChartArea1";
            this.MaterialChart.ChartAreas.Add(chartArea7);
            this.MaterialChart.Location = new System.Drawing.Point(410, 530);
            this.MaterialChart.Name = "MaterialChart";
            this.MaterialChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series11.Name = "Series1";
            series11.Points.Add(dataPoint11);
            series11.Points.Add(dataPoint12);
            series11.Points.Add(dataPoint13);
            series11.Points.Add(dataPoint14);
            series11.Points.Add(dataPoint15);
            this.MaterialChart.Series.Add(series11);
            this.MaterialChart.Size = new System.Drawing.Size(360, 170);
            this.MaterialChart.TabIndex = 18;
            this.MaterialChart.Text = "chart1";
            // 
            // LightSourceChart
            // 
            this.LightSourceChart.BackColor = System.Drawing.SystemColors.Control;
            this.LightSourceChart.BackImageTransparentColor = System.Drawing.Color.White;
            this.LightSourceChart.BorderlineWidth = 0;
            chartArea8.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea8.AxisX.MajorGrid.Enabled = false;
            chartArea8.AxisX.MajorTickMark.Enabled = false;
            chartArea8.AxisY.LabelAutoFitMaxFontSize = 9;
            chartArea8.AxisY.MajorGrid.Enabled = false;
            chartArea8.AxisY.MajorTickMark.Enabled = false;
            chartArea8.BackColor = System.Drawing.SystemColors.Control;
            chartArea8.Name = "ChartArea1";
            this.LightSourceChart.ChartAreas.Add(chartArea8);
            this.LightSourceChart.Location = new System.Drawing.Point(12, 491);
            this.LightSourceChart.Name = "LightSourceChart";
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series12.Name = "Series1";
            this.LightSourceChart.Series.Add(series12);
            this.LightSourceChart.Size = new System.Drawing.Size(360, 209);
            this.LightSourceChart.TabIndex = 19;
            this.LightSourceChart.Text = "chart2";
            // 
            // ObserverChart
            // 
            this.ObserverChart.BackColor = System.Drawing.SystemColors.Control;
            chartArea9.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea9.AxisX.MajorGrid.Enabled = false;
            chartArea9.AxisY.LabelAutoFitMaxFontSize = 9;
            chartArea9.AxisY.MajorGrid.Enabled = false;
            chartArea9.BackColor = System.Drawing.SystemColors.Control;
            chartArea9.Name = "ChartArea1";
            this.ObserverChart.ChartAreas.Add(chartArea9);
            this.ObserverChart.Location = new System.Drawing.Point(812, 491);
            this.ObserverChart.Name = "ObserverChart";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series13.Color = System.Drawing.Color.Red;
            series13.Name = "Series1";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series14.Color = System.Drawing.Color.Lime;
            series14.Name = "Series2";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series15.Color = System.Drawing.Color.Blue;
            series15.Name = "Series3";
            this.ObserverChart.Series.Add(series13);
            this.ObserverChart.Series.Add(series14);
            this.ObserverChart.Series.Add(series15);
            this.ObserverChart.Size = new System.Drawing.Size(360, 209);
            this.ObserverChart.TabIndex = 20;
            this.ObserverChart.Text = "chart3";
            // 
            // fullSpectralScene1
            // 
            this.fullSpectralScene1.Animate = false;
            this.fullSpectralScene1.DataManager = null;
            this.fullSpectralScene1.Location = new System.Drawing.Point(294, 29);
            this.fullSpectralScene1.Name = "fullSpectralScene1";
            this.fullSpectralScene1.Recompute = false;
            this.fullSpectralScene1.RenderSettings = null;
            this.fullSpectralScene1.SceneGraph = null;
            this.fullSpectralScene1.Size = new System.Drawing.Size(430, 456);
            this.fullSpectralScene1.TabIndex = 3;
            this.fullSpectralScene1.Text = "fullSpectralScene1";
            this.fullSpectralScene1.Click += new System.EventHandler(this.fullSpectralScene1_Click);
            // 
            // rgbScene1
            // 
            this.rgbScene1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rgbScene1.Animate = false;
            this.rgbScene1.DataManager = null;
            this.rgbScene1.Location = new System.Drawing.Point(742, 29);
            this.rgbScene1.Name = "rgbScene1";
            this.rgbScene1.Recompute = false;
            this.rgbScene1.RenderSettings = null;
            this.rgbScene1.SceneGraph = null;
            this.rgbScene1.Size = new System.Drawing.Size(430, 456);
            this.rgbScene1.TabIndex = 4;
            this.rgbScene1.Text = "rgbScene1";
            // 
            // spectroBindingSource
            // 
            this.spectroBindingSource.DataSource = typeof(SpectroNamespace.Spectro);
            // 
            // animateCheckbox
            // 
            this.animateCheckbox.AutoSize = true;
            this.animateCheckbox.Checked = true;
            this.animateCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.animateCheckbox.Location = new System.Drawing.Point(142, 282);
            this.animateCheckbox.Name = "animateCheckbox";
            this.animateCheckbox.Size = new System.Drawing.Size(64, 17);
            this.animateCheckbox.TabIndex = 21;
            this.animateCheckbox.Text = "Animate";
            this.animateCheckbox.UseVisualStyleBackColor = true;
            this.animateCheckbox.CheckedChanged += new System.EventHandler(this.animateCheckbox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Key value";
            // 
            // KeyValueCounter
            // 
            this.KeyValueCounter.AutoSize = true;
            this.KeyValueCounter.Location = new System.Drawing.Point(242, 156);
            this.KeyValueCounter.Name = "KeyValueCounter";
            this.KeyValueCounter.Size = new System.Drawing.Size(13, 13);
            this.KeyValueCounter.TabIndex = 10;
            this.KeyValueCounter.Text = "0";
            // 
            // KeyValueBar
            // 
            this.KeyValueBar.Enabled = false;
            this.KeyValueBar.Location = new System.Drawing.Point(13, 176);
            this.KeyValueBar.Name = "KeyValueBar";
            this.KeyValueBar.Size = new System.Drawing.Size(265, 45);
            this.KeyValueBar.TabIndex = 9;
            this.KeyValueBar.Scroll += new System.EventHandler(this.KeyValueBar_Scroll);
            // 
            // Spectro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 712);
            this.Controls.Add(this.animateCheckbox);
            this.Controls.Add(this.ObserverChart);
            this.Controls.Add(this.LightSourceChart);
            this.Controls.Add(this.MaterialChart);
            this.Controls.Add(this.objectSelectedMessage);
            this.Controls.Add(this.clipInvisbleCheckBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LightStrengthCounter);
            this.Controls.Add(this.LightStrengthBar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.KeyValueCounter);
            this.Controls.Add(this.KeyValueBar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.materialComboBox);
            this.Controls.Add(this.lightSourceComboBox);
            this.Controls.Add(this.observerComboBox);
            this.Controls.Add(this.fullSpectralScene1);
            this.Controls.Add(this.rgbScene1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 750);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "Spectro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Spectro";
            this.Load += new System.EventHandler(this.Spectro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LightStrengthBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObserverChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spectroBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValueBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox observerComboBox;
        private System.Windows.Forms.ComboBox lightSourceComboBox;
        private System.Windows.Forms.ComboBox materialComboBox;
        private FullSpectralScene fullSpectralScene1;
        private RGBScene rgbScene1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource spectroBindingSource;
        private System.Windows.Forms.Label LightStrengthCounter;
        private System.Windows.Forms.TrackBar LightStrengthBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox clipInvisbleCheckBox;
        private System.Windows.Forms.Label objectSelectedMessage;
        private System.Windows.Forms.DataVisualization.Charting.Chart MaterialChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart LightSourceChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart ObserverChart;
        private System.Windows.Forms.CheckBox animateCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label KeyValueCounter;
        private System.Windows.Forms.TrackBar KeyValueBar;
    }
}


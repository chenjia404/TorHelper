
namespace TorHelper
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.lbl_network_type = new System.Windows.Forms.Label();
            this.cmb_network_type = new System.Windows.Forms.ComboBox();
            this.lbl_port = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.chb_snowflake = new System.Windows.Forms.CheckBox();
            this.llbl = new System.Windows.Forms.LinkLabel();
            this.lbl_tor = new System.Windows.Forms.LinkLabel();
            this.timer_updater = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_version = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(110, 544);
            this.btn_start.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(150, 44);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "启动";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(370, 544);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(150, 44);
            this.btn_stop.TabIndex = 0;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.Btn_stop_Click);
            // 
            // lbl_network_type
            // 
            this.lbl_network_type.AutoSize = true;
            this.lbl_network_type.Location = new System.Drawing.Point(46, 46);
            this.lbl_network_type.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_network_type.Name = "lbl_network_type";
            this.lbl_network_type.Size = new System.Drawing.Size(102, 25);
            this.lbl_network_type.TabIndex = 1;
            this.lbl_network_type.Text = "连接方式:";
            // 
            // cmb_network_type
            // 
            this.cmb_network_type.FormattingEnabled = true;
            this.cmb_network_type.Items.AddRange(new object[] {
            "直连",
            "snowflake",
            "meek"});
            this.cmb_network_type.Location = new System.Drawing.Point(168, 40);
            this.cmb_network_type.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmb_network_type.Name = "cmb_network_type";
            this.cmb_network_type.Size = new System.Drawing.Size(238, 33);
            this.cmb_network_type.TabIndex = 2;
            this.cmb_network_type.SelectedIndexChanged += new System.EventHandler(this.Cmb_network_type_SelectedIndexChanged);
            // 
            // lbl_port
            // 
            this.lbl_port.AutoSize = true;
            this.lbl_port.Location = new System.Drawing.Point(44, 137);
            this.lbl_port.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_port.Name = "lbl_port";
            this.lbl_port.Size = new System.Drawing.Size(102, 25);
            this.lbl_port.TabIndex = 3;
            this.lbl_port.Text = "代理端口:";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(172, 131);
            this.txt_port.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(196, 31);
            this.txt_port.TabIndex = 4;
            this.txt_port.Text = "9050";
            this.txt_port.TextChanged += new System.EventHandler(this.Txt_port_TextChanged);
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(50, 260);
            this.txt_log.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.Size = new System.Drawing.Size(556, 246);
            this.txt_log.TabIndex = 5;
            // 
            // chb_snowflake
            // 
            this.chb_snowflake.AutoSize = true;
            this.chb_snowflake.Location = new System.Drawing.Point(52, 200);
            this.chb_snowflake.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chb_snowflake.Name = "chb_snowflake";
            this.chb_snowflake.Size = new System.Drawing.Size(224, 29);
            this.chb_snowflake.TabIndex = 6;
            this.chb_snowflake.Text = "运行snowflake网桥";
            this.chb_snowflake.UseVisualStyleBackColor = true;
            this.chb_snowflake.CheckedChanged += new System.EventHandler(this.Chb_snowflake_CheckedChanged);
            // 
            // llbl
            // 
            this.llbl.AutoSize = true;
            this.llbl.Location = new System.Drawing.Point(52, 613);
            this.llbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.llbl.Name = "llbl";
            this.llbl.Size = new System.Drawing.Size(54, 25);
            this.llbl.TabIndex = 7;
            this.llbl.TabStop = true;
            this.llbl.Text = "主页";
            this.llbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Llbl_LinkClicked);
            // 
            // lbl_tor
            // 
            this.lbl_tor.AutoSize = true;
            this.lbl_tor.Location = new System.Drawing.Point(162, 613);
            this.lbl_tor.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_tor.Name = "lbl_tor";
            this.lbl_tor.Size = new System.Drawing.Size(37, 25);
            this.lbl_tor.TabIndex = 7;
            this.lbl_tor.TabStop = true;
            this.lbl_tor.Text = "tor";
            this.lbl_tor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_tor_LinkClicked);
            // 
            // timer_updater
            // 
            this.timer_updater.Enabled = true;
            this.timer_updater.Interval = 600000;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_version});
            this.statusStrip1.Location = new System.Drawing.Point(0, 651);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(690, 41);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_version
            // 
            this.tssl_version.Name = "tssl_version";
            this.tssl_version.Size = new System.Drawing.Size(92, 31);
            this.tssl_version.Text = "版本号:";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 692);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbl_tor);
            this.Controls.Add(this.llbl);
            this.Controls.Add(this.chb_snowflake);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.lbl_port);
            this.Controls.Add(this.cmb_network_type);
            this.Controls.Add(this.lbl_network_type);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FrmMain";
            this.Text = "tor助手";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Label lbl_network_type;
        private System.Windows.Forms.ComboBox cmb_network_type;
        private System.Windows.Forms.Label lbl_port;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.CheckBox chb_snowflake;
        private System.Windows.Forms.LinkLabel llbl;
        private System.Windows.Forms.LinkLabel lbl_tor;
        private System.Windows.Forms.Timer timer_updater;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_version;
    }
}


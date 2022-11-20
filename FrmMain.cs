using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorHelper
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        Process process_tor;

        private void btn_start_Click(object sender, EventArgs e)
        {
            Process[] pa = Process.GetProcesses();//获取当前进程数组。
            int proc_amount = 0;
            foreach (Process PTest in pa)
            {
                //关闭之前的进程
                if (PTest.ProcessName == "tor")
                {
                    PTest.Kill();
                }
            }
            string torcc_str = "ControlPort 9051\n" +
                "DataDirectory ./Data\n" +
                "GeoIPFile Data/Tor/geoip/geoip\n" +
                "GeoIPv6File Data/Tor/geoip/geoip6\n" +
                "AvoidDiskWrites 1\n"
                ;
            if(cmb_network_type.SelectedItem == "snowflake")
            {
                torcc_str += "UseBridges 1\n" +
                    "Bridge snowflake 192.0.2.3:80 2B280B23E1107BB62ABFC40DDCC8824814F80A72 fingerprint=2B280B23E1107BB62ABFC40DDCC8824814F80A72 url=https://snowflake-broker.torproject.net.global.prod.fastly.net/ front=cdn.sstatic.net ice=stun:stun.l.google.com:19302,stun:stun.altar.com.pl:3478,stun:stun.antisip.com:3478,stun:stun.bluesip.net:3478,stun:stun.dus.net:3478,stun:stun.epygi.com:3478,stun:stun.sonetel.com:3478,stun:stun.sonetel.net:3478,stun:stun.stunprotocol.org:3478,stun:stun.uls.co.za:3478,stun:stun.voipgate.com:3478,stun:stun.voys.nl:3478 utls-imitate=hellorandomizedalpn\n" +
"ClientTransportPlugin snowflake exec Tor/PluggableTransports/snowflake-client.exe\n";
            }

            torcc_str += "SocksPort " + txt_port.Text + "\n";

            WriteFile("./Tor/torrc", torcc_str);

            //要执行的程序名称
            process_tor = new Process();
            process_tor.StartInfo.FileName = "./tor/tor.exe";
            process_tor.StartInfo.Arguments = " -f ./Tor/torrc";
            process_tor.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            process_tor.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process_tor.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process_tor.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process_tor.StartInfo.CreateNoWindow = true;//不显示程序窗口

            process_tor.OutputDataReceived += OnDataReceived;

            process_tor.Start();//启动程序
            process_tor.BeginOutputReadLine();
            process_tor.StandardInput.AutoFlush = true;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="txt"></param>
        public static void WriteFile(string path, string txt)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(txt);
                sw.Close();
                file.Close();
            }
            catch
            {
                throw;
            }
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            AddItemToTextBox(e.Data);
        }


        delegate void AddItemToTextBoxDelegate(string str);

        /// <summary>  
        /// 在ListBox中追加状态信息  
        /// </summary>  
        /// <param name="str">要追加的信息</param>
        private void AddItemToTextBox(string str)
        {
            if (str == null)
                return;
            if(txt_log.InvokeRequired)
            {
                AddItemToTextBoxDelegate d = AddItemToTextBox;
                txt_log.Invoke(d, str);
            }
            else
            {
                txt_log.AppendText(str);
                txt_log.AppendText(Environment.NewLine);
                txt_log.ScrollToCaret();
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            process_tor.Kill();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cmb_network_type.SelectedIndex = 1;
            
        }
    }
}

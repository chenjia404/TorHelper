using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorHelper
{
    public partial class FrmMain : Form
    {
        private INI config;

        public FrmMain()
        {
            InitializeComponent();
        }

        Process process_tor;

        private void Btn_start_Click(object sender, EventArgs e)
        {
            Process[] pa = Process.GetProcesses();//获取当前进程数组。
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
            if(cmb_network_type.SelectedItem.ToString() == "snowflake")
            {
                torcc_str += "UseBridges 1\n" +
                    "Bridge snowflake 192.0.2.3:80 2B280B23E1107BB62ABFC40DDCC8824814F80A72 fingerprint=2B280B23E1107BB62ABFC40DDCC8824814F80A72 url=https://snowflake-broker.torproject.net.global.prod.fastly.net/ front=cdn.sstatic.net ice=stun:stun.l.google.com:19302,stun:stun.altar.com.pl:3478,stun:stun.antisip.com:3478,stun:stun.bluesip.net:3478,stun:stun.dus.net:3478,stun:stun.epygi.com:3478,stun:stun.sonetel.com:3478,stun:stun.sonetel.net:3478,stun:stun.stunprotocol.org:3478,stun:stun.uls.co.za:3478,stun:stun.voipgate.com:3478,stun:stun.voys.nl:3478 utls-imitate=hellorandomizedalpn\n" +
"ClientTransportPlugin snowflake exec Tor/PluggableTransports/snowflake-client.exe\n";
            } else if (cmb_network_type.SelectedItem.ToString() == "meek")
            {
                torcc_str += "UseBridges 1\n" +
                    "Bridge meek_lite 192.0.2.18:80 BE776A53492E1E044A26F17306E1BC46A55A1625 url=https://meek.azureedge.net/ front=ajax.aspnetcdn.com\n" +
                @"ClientTransportPlugin meek_lite,obfs2,obfs3,obfs4,scramblesuit exec Tor\PluggableTransports\obfs4proxy.exe" +
                "\n";
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

        public void LoadConfig()
        {
            #region 配置文件

            //不存在就创建
            if (!File.Exists(Application.StartupPath + @"\config.ini"))
                System.IO.File.WriteAllLines(Application.StartupPath + @"\config.ini", new string[0]);

            if (File.Exists(Application.StartupPath + @"\config.ini"))
            {
                this.config = new INI(Application.StartupPath + @"\config.ini");


                string net_port = config.ReadValue("proxy", "port");
                if(net_port.Length > 0)
                    txt_port.Text = net_port;
            }
            #endregion
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

        private void Btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", "");
                process_tor.Kill();
            }catch(NullReferenceException ex)
            {
                MessageBox.Show("程序还没有启动，不能停止");
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            tssl_version.Text = "版本号:" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            LoadConfig();
            cmb_network_type.SelectedIndex = 1;
            if (!File.Exists(@"tor\tor.exe"))
            {
                txt_log.AppendText("没有找到tor程序，请去这里下载  https://www.torproject.org/download/tor/");
                txt_log.AppendText(Environment.NewLine);
            }
            Thread th2 = new Thread(CheckGithubVersion);
            th2.Start();
        }

        private void CheckGithubVersion()
        {
            string api_url = "https://api.github.com/repos/chenjia404/TorHelper/releases/latest";
            string github_str = Util.Util.GetWebContent(api_url);
            var json = (IDictionary<string, object>)SimpleJson.SimpleJson.DeserializeObject(github_str);
            AddItemToTextBox("GitHub最新版本:"+json["tag_name"].ToString());

            Version github_version = Version.Parse(json["tag_name"].ToString().Replace("v",""));

            if(github_version.CompareTo(Assembly.GetExecutingAssembly().GetName().Version) > 0)
            {
                var assets = SimpleJson.SimpleJson.DeserializeObject<SimpleJson.JsonArray>(json["assets"].ToString());
                var asset = SimpleJson.SimpleJson.DeserializeObject<SimpleJson.JsonObject>(assets[0].ToString());
                AddItemToTextBox("下载链接:" + asset["browser_download_url"].ToString());
            }
        }

        Process process_snow;
        private void Chb_snowflake_CheckedChanged(object sender, EventArgs e)
        {
            if (!File.Exists(@"tor\proxy.exe"))
            {
                txt_log.AppendText("没有找到snowflake程序");
                txt_log.AppendText(Environment.NewLine);
                return;
            }
            if (chb_snowflake.Checked)
            {
                process_snow = new Process();
                process_snow.StartInfo.FileName = "./tor/proxy.exe";
                process_snow.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                process_snow.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                process_snow.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                process_snow.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                process_snow.StartInfo.CreateNoWindow = true;//不显示程序窗口

                process_snow.OutputDataReceived += OnDataReceived;

                process_snow.Start();//启动程序
                process_snow.BeginOutputReadLine();
                process_snow.StandardInput.AutoFlush = true;
                txt_log.AppendText("启动snowflake");
                txt_log.AppendText(Environment.NewLine);
            }
            else
            {
                process_snow.Kill();
            }
        }

        private void Txt_port_TextChanged(object sender, EventArgs e)
        {
            config.Writue("proxy", "port", txt_port.Text);
        }

        private void Cmb_network_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.Writue("proxy", "network_type", cmb_network_type.SelectedText);
        }

        private void Llbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/chenjia404/TorHelper");
        }

        private void lbl_tor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.torproject.org/download/tor/");
        }

        private void btc_check_Click(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", "--proxy-server=\"socks5://127.0.0.1:"+ txt_port.Text + "\"");
            FrmBrowser f = new FrmBrowser();
            f.Show();
        }

        private void lbl_gettorbrowser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/TheTorProject/gettorbrowser/releases");
        }
    }
}

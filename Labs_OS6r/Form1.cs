using System;
using System.Windows.Forms;
using SimpleTCP;

namespace Labs_OS6r
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = System.Text.Encoding.UTF8;
            richTextBox1.ReadOnly = true;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            richTextBox1.Invoke((MethodInvoker)delegate ()
            {
                try
                {
                    int amount=int.Parse(e.MessageString);
                    for (int i = 0; i < amount; i++)
                    {
                        comboBox1.Items.Add(i.ToString());
                    }
                }
                catch
                {
                    richTextBox1.Text = e.MessageString;
                }
            });
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(textBox1.Text);
            client.Connect("127.0.0.1", int.Parse(textBox2.Text));
            label5.Text = "Status: connected\n";
            client.Write("get");
            comboBox1.Items.Clear();
        }

        private void sendAnswerButton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                client.Write("a");
            }
            if (radioButton2.Checked)
            {
                client.Write("b");
            }
            if (radioButton3.Checked)
            {
                client.Write("c");
            }
            if (radioButton4.Checked)
            {
                client.Write("d");
            }
        }

        private void GetQuestionButton_Click(object sender, EventArgs e)
        {
            int n = comboBox1.SelectedIndex;
            client.Write(n.ToString());
        }
    }
}

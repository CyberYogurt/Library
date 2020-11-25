using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Biblieoteka
{
    public partial class Form3 : Form
    { //добавить отчество
        public Form3()
        {
            InitializeComponent();
        }
        static readers r = new readers();
        public static HttpClient client = new HttpClient();
        private void Form3_Load(object sender, EventArgs e)
        {

        }
        public async Task<HttpContent> ServerSendR(readers r)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync($"http://localhost:3000/readers/insert", r);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content;
        }
        private  void reader_add(object sender, EventArgs e)
        {
            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                r.first_n = textBox1.Text;
                r.last_n = textBox2.Text;
                r.class_let = textBox4.Text;
                r.class_num = Convert.ToInt32(textBox3.Text);
                r.inv_num = Convert.ToInt32(textBox5.Text);
                r.birth_date = dateTimePicker1.Value;
                r.reg_date = DateTime.Today;
                r.liv_add = textBox6.Text;
                r.other = radioButton2.Checked;
                r.teacher = radioButton3.Checked;
                r.login = textBox7.Text;
                r.password = textBox8.Text;

                StreamContent contentSK = await ServerSendR(r) as StreamContent;
                this.Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }
    }
}

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
    public partial class Form2 : Form
    {
        static knigs k = new knigs();
        public static HttpClient client = new HttpClient();

        public Form2()
        {
            InitializeComponent();
        }

        public async Task<HttpContent> ServerSend(knigs k)
        {
            HttpResponseMessage responseMessageSK = await client.PostAsJsonAsync($"http://localhost:3000/knigs/insert", k);
            responseMessageSK.EnsureSuccessStatusCode();
            return responseMessageSK.Content;
        }

        private async void button1_Click(object sender, EventArgs e)//кнопка "добавление книги"
        {
            try
            {
                k.id_bbk = Convert.ToInt32(textBox5.Text);
                k.inv_num = Convert.ToInt32(textBox4.Text);
                k.knig_name = textBox1.Text;
                k.author_name = textBox2.Text;
                k.author_sign = textBox3.Text;
                k.god_izd = dateTimePicker1.Value;
                k.kol_vo = Convert.ToInt32(numericUpDown1.Value);
                k.krae = checkBox2.Checked;
                k.uchebnik = checkBox1.Checked;
                k.v_nal = true;

                StreamContent contentSK = await ServerSend(k) as StreamContent;
                this.Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)//Сброс текстовых полей
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            numericUpDown1.Value = 0;
        }
    }
}

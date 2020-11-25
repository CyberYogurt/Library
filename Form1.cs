using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
//
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
//
using Excel = Microsoft.Office.Interop.Excel;

namespace Biblieoteka
{
    public partial class Form1 : Form
    {   //Добавить авторский знак в форме добавить книгу
        static filterK k = new filterK();
        static filterR r = new filterR();
        public Form1()
        {
            InitializeComponent();
        }
        static knigs[] a = null;
        static readers[] b = null;
        public static HttpClient client = new HttpClient();
        public async Task<HttpContent> ServerLoadK() //загрузка данных книг
        {
            HttpResponseMessage responseMessageK = await client.GetAsync("http://localhost:3000/knigs");
            responseMessageK.EnsureSuccessStatusCode();
            return responseMessageK.Content;
        }
        public async Task<HttpContent> ServerLoadR() //загрузка данных читателей
        {
            HttpResponseMessage responseMessageR = await client.GetAsync("http://localhost:3000/readers");
            responseMessageR.EnsureSuccessStatusCode();
            return responseMessageR.Content;
        }
        public static async Task<HttpContent> SearchK(filterK k)//Поиск по книгам
        {
            HttpResponseMessage responseMessageSK = await client.PostAsJsonAsync($"http://localhost:3000/knigs/search", k);
            responseMessageSK.EnsureSuccessStatusCode();
            return responseMessageSK.Content;
        }
        public static async Task<HttpContent> SearchR(filterR r)//Поиск по читателям
        {
            HttpResponseMessage responseMessageSR = await client.PostAsJsonAsync($"http://localhost:3000/readers/search", r);
            responseMessageSR.EnsureSuccessStatusCode();
            return responseMessageSR.Content;
        }
        private async void Form1_Load(object sender, EventArgs e) //загрузка данных в таблицу
        {
            textBox2.Enabled = false;
           
            try
            {
                StreamContent content1 = await ServerLoadR() as StreamContent;
                b = JsonSerializer.Deserialize<readers[]>(content1.ReadAsStringAsync().Result.ToString());
                column_changedR();

                StreamContent content = await ServerLoadK() as StreamContent;
                a = JsonSerializer.Deserialize<knigs[]>(content.ReadAsStringAsync().Result.ToString());
                column_changedK();

              
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == false)
            {
                textBox2.Clear();
                textBox2.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                k.name = textBox1.Text;
                dataGridView1.DataSource = null;
                StreamContent contentSK = await SearchK(k) as StreamContent;
                a = JsonSerializer.Deserialize<knigs[]>(contentSK.ReadAsStringAsync().Result.ToString());
                column_changedK();
            }
            catch (Exception errorFind)
            {
                MessageBox.Show(errorFind.Message);
            }

        }

        private async void filterR(object sender, EventArgs e)//поиск по фамилии читателя
        {
            dataGridView2.DataSource = null;
            r.last_name = textBox3.Text;
            StreamContent contentSR = await SearchR(r) as StreamContent;
            b = JsonSerializer.Deserialize<readers[]>(contentSR.ReadAsStringAsync().Result.ToString());
            column_changedR();
        }

        private async void filter_refreshR(object sender, EventArgs e)//сброс фильтра поиска читателей
        {
            dataGridView2.DataSource = null;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            StreamContent contentSR = await ServerLoadR() as StreamContent;
            b = JsonSerializer.Deserialize<readers[]>(contentSR.ReadAsStringAsync().Result.ToString());
            column_changedR();
        }

        private async void filterRF(object sender, EventArgs e)//Поиск читателя по фильтрам
        {
            dataGridView2.DataSource = null;
            r.num = numericUpDown1.Value;
            r.other = radioButton4.Checked;
            r.teacher = radioButton5.Checked; 
            StreamContent content = await SearchR(r) as StreamContent;
            b = JsonSerializer.Deserialize<readers[]>(content.ReadAsStringAsync().Result.ToString());
            column_changedR();
        }

        private async void filterK(object sender, EventArgs e)//поиск по названию или автора
        {
            dataGridView1.DataSource = null;
            k.name = textBox1.Text;
            StreamContent content = await SearchK(k) as StreamContent;
            a = JsonSerializer.Deserialize<knigs[]>(content.ReadAsStringAsync().Result.ToString());
            column_changedK();
        }

        private async void filter_refreshK(object sender, EventArgs e)//сброс фильтра поиска книг
        {
            textBox1.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            k.bbk = radioButton3.Checked;
            k.krae = radioButton1.Checked;
            k.uchebnik = radioButton2.Checked;
            StreamContent content = await ServerLoadK() as StreamContent;
            a = JsonSerializer.Deserialize<knigs[]>(content.ReadAsStringAsync().Result.ToString());
            column_changedK();
        }

        private async void filterKF(object sender, EventArgs e)//поиск книг по фильтру
        {
             k.name = textBox1.Text;
             k.bbk = radioButton3.Checked;
             k.bbk_name = textBox2.Text;
             k.krae = radioButton1.Checked;
             k.uchebnik = radioButton2.Checked;

             dataGridView1.DataSource = null;
             StreamContent contentSK = await SearchK(k) as StreamContent;
             a = JsonSerializer.Deserialize<knigs[]>(contentSK.ReadAsStringAsync().Result.ToString());
             column_changedK();
        
        }
        //Смена названия колонок
        public void column_changedK()
        {
            dataGridView1.DataSource = a;
            dataGridView1.Columns[0].Visible = false; 
            dataGridView1.Columns[1].HeaderCell.Value = "ББК";
            dataGridView1.Columns[2].HeaderCell.Value = "Инвентарный номер";
            dataGridView1.Columns[3].HeaderCell.Value = "Название книги";
            dataGridView1.Columns[4].HeaderCell.Value = "Имя автора";
            dataGridView1.Columns[5].HeaderCell.Value = "Подпись автора";
            dataGridView1.Columns[6].HeaderCell.Value = "Год издания";
            dataGridView1.Columns[7].HeaderCell.Value = "Количество";
            dataGridView1.Columns[8].HeaderCell.Value = "Краеведение";
            dataGridView1.Columns[9].HeaderCell.Value = "Учебник";
            dataGridView1.Columns[10].HeaderCell.Value = "В наличии";
        }

        public void column_changedR()
        {
            dataGridView2.DataSource = b;
            dataGridView2.Columns[0].HeaderCell.Value = "Порядковый номер";
            dataGridView2.Columns[1].HeaderCell.Value = "Имя";
            dataGridView2.Columns[2].HeaderCell.Value = "Фамилия";
            dataGridView2.Columns[3].HeaderCell.Value = "Взятая книга";
            dataGridView2.Columns[4].HeaderCell.Value = "Номер класса";
            dataGridView2.Columns[5].HeaderCell.Value = "Буква класса";
            dataGridView2.Columns[6].HeaderCell.Value = "Дата рождения";
            dataGridView2.Columns[7].HeaderCell.Value = "Дата регистрации";
            dataGridView2.Columns[8].HeaderCell.Value = "Адрес проживания";
            dataGridView2.Columns[9].HeaderCell.Value = "Учитель";
            dataGridView2.Columns[10].HeaderCell.Value = "Прочее";
        }
        private void button1_Click_1(object sender, EventArgs e)//Форма добавления книги
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)//Форма добавления читателя
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e) //Форма выдачи книги
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public static HttpClient client = new HttpClient();
        static readers r = new readers();
        public static async Task<HttpContent> give_out(readers r)
        {
            HttpResponseMessage responseMessageSR = await client.PostAsJsonAsync($"http://localhost:3000/readers/update", r);
            responseMessageSR.EnsureSuccessStatusCode();
            return responseMessageSR.Content;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            r.last_n = textBox2.Text;
            r.first_n = textBox1.Text;
            r.inv_num = Convert.ToInt32(textBox3.Text);
            StreamContent content = await give_out(r) as StreamContent;
            this.Visible = false;

        }
    }
}

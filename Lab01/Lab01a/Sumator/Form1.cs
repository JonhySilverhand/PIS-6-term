using System.Net.Http;
using System.Text;

namespace Sumator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private async void button_calc_Click(object sender, EventArgs e)
		{
			try
			{
				if (Double.TryParse(textBoxX.Text, out double x) && Double.TryParse(textBoxY.Text, out double y))
				{
					HttpClient client = new HttpClient();
					string url = "http://localhost:5289/sum";
					string queryString = $"?X={x}&Y={y}";

					var response = await client.PostAsync(url + queryString, null);

					if (response.IsSuccessStatusCode)
					{
						string result = await response.Content.ReadAsStringAsync();
						textBoxResult.Text = result;
					}
					else
					{
						MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
					}
				}
				else
				{
					MessageBox.Show("Invalid input for X or Y. Please enter valid numeric values.");
					textBoxX.Text = string.Empty; 
					textBoxY.Text = string.Empty; 
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred: {ex.Message}");
			}
		}

	}
}

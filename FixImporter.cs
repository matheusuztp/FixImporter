namespace FixImporter
{
    public partial class FixImporter : Form
    {
        public FixImporter()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string data = txtData.Text;

                if (string.IsNullOrWhiteSpace(data))
                {
                    lblInfo.Text = "Erro: Nenhum dado foi inserido!";
                    lblInfo.Visible = true;
                    return;
                }

                string[] lines = data
                    .Replace("\\n", "\n")  // Converter \n literal em quebra real
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                
                var filteredLines = lines
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Trim())
                    .ToList();

                int totalRecords = filteredLines.Count;

                var distinctList = filteredLines.Distinct().ToList();
                int distinctCount = distinctList.Count;
                int duplicates = totalRecords - distinctCount;

                string distinctOutput;
                if (cbSQL.Checked)
                {
                    distinctOutput = string.Join(",\r\n", 
                        distinctList.Select(item => $"'{item.Replace("'", "''")}'"));
                }
                else
                {
                    distinctOutput = string.Join(Environment.NewLine, distinctList);
                }

                Clipboard.SetText(distinctOutput);

                lblInfo.Text = $"Total de registros: {totalRecords} | Únicos: {distinctCount} | Duplicidades: {duplicates}";
                lblInfo.Visible = true;

                MessageBox.Show("Lista distinct copiada para o clipboard!\nVocê pode colar com Ctrl+V", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblInfo.Text = $"Erro: {ex.Message}";
                lblInfo.ForeColor = Color.Red;
                lblInfo.Visible = true;
                MessageBox.Show($"Erro ao processar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

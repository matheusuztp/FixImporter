using System.Diagnostics.CodeAnalysis;

namespace FixImporter;

[ExcludeFromCodeCoverage]
public partial class FixImporter : Form
{
    private readonly ImportProcessor _importProcessor;

    public FixImporter()
    {
        InitializeComponent();
        _importProcessor = new ImportProcessor(new WindowsClipboardService());
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
        var result = _importProcessor.Process(txtData.Text, cbSQL.Checked);

        switch (result.Status)
        {
            case ProcessingStatus.InvalidParameters:
            case ProcessingStatus.Unauthorized:
            case ProcessingStatus.ProcessingError:
                lblInfo.Text = result.Message;
                lblInfo.ForeColor = Color.Red;
                lblInfo.Visible = true;
                MessageBox.Show(result.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            case ProcessingStatus.Success:
                lblInfo.ForeColor = Color.Black;
                lblInfo.Text = $"Total de registros: {result.TotalRecords} | Unicos: {result.DistinctRecords} | Duplicidades: {result.Duplicates}";
                lblInfo.Visible = true;
                MessageBox.Show("Lista distinct copiada para o clipboard!\nVoce pode colar com Ctrl+V", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            default:
                lblInfo.Text = "Erro: status de processamento invalido.";
                lblInfo.ForeColor = Color.Red;
                lblInfo.Visible = true;
                MessageBox.Show(lblInfo.Text, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
        }
    }
}

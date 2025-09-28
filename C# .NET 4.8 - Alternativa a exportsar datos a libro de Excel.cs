using ClosedXML.Excel;
using System;
using System.Data;
using System.Windows.Forms;

public void ExportarConSaveFileDialog(DataGridView dataGridView)
{
    try
    {
        // Crear un SaveFileDialog
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Guardar archivo Excel";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;

            // Mostrar el diálogo y verificar si el usuario seleccionó una ruta
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Crear un DataTable a partir del DataGridView
                DataTable dataTable = new DataTable();
                foreach (DataGridViewColumn columna in dataGridView.Columns)
                {
                    dataTable.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow fila in dataGridView.Rows)
                {
                    if (!fila.IsNewRow) // Evitar filas vacías
                    {
                        DataRow dataRow = dataTable.NewRow();
                        foreach (DataGridViewCell celda in fila.Cells)
                        {
                            dataRow[celda.ColumnIndex] = celda.Value?.ToString() ?? string.Empty;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }

                // Crear un archivo Excel con ClosedXML
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(dataTable, "Datos");
                    workbook.SaveAs(saveFileDialog.FileName); // Guardar en la ruta seleccionada
                }

                MessageBox.Show("Datos exportados exitosamente a Excel.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error al exportar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

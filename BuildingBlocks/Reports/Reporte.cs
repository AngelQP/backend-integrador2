using Bigstick.BuildingBlocks.Extensions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Reports
{
    public class Reporte : IReporte
    {
        public async Task<byte[]> GenerarReporte<T>(IEnumerable<T> list, string tituloReporte, params string[] excluido)
        {
            var dataTable = list.ToDataTable();

            int limit = dataTable.Columns.Count;
            for (int i = 0; i < limit; i++)
            {
                if (excluido.ToList().Contains(dataTable.Columns[i].Caption))
                {
                    dataTable.Columns.RemoveAt(i);
                    limit--;
                }
            }


            using (var memoryStream = new MemoryStream())
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("reporte");

                var styleCenter = workbook.CreateCellStyle();
                styleCenter.Alignment = HorizontalAlignment.Center;

                var fontBold = workbook.CreateFont();
                fontBold.FontHeightInPoints = 25;
                fontBold.IsBold = true;

                IRow row = excelSheet.CreateRow(0);
                row.CreateCell(0).SetCellValue(tituloReporte);
                row.Height = 800;

                var cra = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dataTable.Columns.Count - 1);
                excelSheet.AddMergedRegion(cra);
                row.GetCell(0).CellStyle = styleCenter;
                row.GetCell(0).CellStyle.SetFont(fontBold);

                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue($"Generado: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");

                var boldFont = workbook.CreateFont();
                boldFont.IsBold = true;
                boldFont.Color = IndexedColors.White.Index;
                ICellStyle boldStyle = workbook.CreateCellStyle();
                boldStyle.SetFont(boldFont);

                boldStyle.FillForegroundColor = IndexedColors.Black.Index;
                boldStyle.FillPattern = FillPattern.SolidForeground;


                row = excelSheet.CreateRow(3);
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dataTable.Columns[i].Caption);
                    row.GetCell(i).CellStyle = boldStyle;
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    row = excelSheet.CreateRow(4 + i);
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        string value = dataTable.Rows[i][j].ToString();
                        row.CreateCell(j).SetCellValue(value);
                        excelSheet.SetColumnWidth(j, 5500);
                    }
                }
                workbook.Write(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}

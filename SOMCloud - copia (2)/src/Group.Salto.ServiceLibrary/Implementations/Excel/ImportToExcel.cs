using System.Collections.Generic;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Text.RegularExpressions;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Contracts.Excel;
using Group.Salto.Common.Constants;

namespace Group.Salto.ServiceLibrary.Implementations.Excel
{
    public class ImportToExcel : IImportToExcel
    {
        public ImportToExcel() { }
        private IWorkbook Workbook = null;
        private ISheet Sheet = null;

        public byte[] GenerateFile(IList<GridDataDto> data, IList<string> columns)
        {
            using (MemoryStream excelFile = new MemoryStream())
            {
                Workbook = new XSSFWorkbook();
                FillExcel(data, columns);
                Workbook.Write(excelFile);
                return excelFile.ToArray();
            }
        }

        private void FillExcel(IList<GridDataDto> data, IList<string> columns)
        {
            Sheet = Workbook.CreateSheet(SetSheetName(LocalizationsConstants.SheetName));
            SetHeader(columns);
            int rowNumber = SetDocumentBody(data);
            AutoSizeColumn(columns, rowNumber);
            SetAuthor(Workbook, LocalizationsConstants.ExcelAutor);
        }

        private int SetDocumentBody(IList<GridDataDto> data)
        {
            int rowNumber = 1;
            int cellNumber = 0;

            foreach (GridDataDto dataRow in data)
            {
                IRow row = Sheet.CreateRow(rowNumber);
                cellNumber = 0;

                foreach (string column in dataRow.Data)
                {
                    ICell cell = row.CreateCell(cellNumber);

                    if (!string.IsNullOrEmpty(column))
                    {
                        cell.SetCellValue(column);
                    }
                    else
                    {
                        cell.SetCellValue(string.Empty);
                    }
                    cellNumber++;
                }
                rowNumber++;
            }

            return rowNumber;
        }

        private void SetHeader(IList<string> columns)
        {
            int cellNumber = 0;
            IRow row = Sheet.CreateRow(0);

            ICellStyle headerCellStyle = Workbook.CreateCellStyle();
            headerCellStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            headerCellStyle.FillPattern = FillPattern.SolidForeground;

            foreach (string column in columns)
            {
                ICell cell = row.CreateCell(cellNumber);
                cell.SetCellValue(column);
                cell.CellStyle = headerCellStyle;
                cellNumber++;
            }
        }

        private void AutoSizeColumn(IList<string> columns, int rowNumber)
        {
            if (rowNumber <= 25000)
            {
                int cellNumber = 0;
                foreach (string column in columns)
                {
                    Sheet.AutoSizeColumn(cellNumber++);
                }
            }
        }

        private string SetSheetName(string worksheetName)
        {
            worksheetName = RemoveSheetChararcterNotAllowed(worksheetName);
            worksheetName = MaxSheetName(worksheetName);

            return worksheetName;
        }

        private string RemoveSheetChararcterNotAllowed(string worksheetName)
        {
            const string invalidCharsRegex = @"[/\\*'?[\]:]+";
            return Regex.Replace(worksheetName, invalidCharsRegex, "-").Replace("  ", " ").Trim();
        }

        private string MaxSheetName(string workseetName)
        {
            const int maxLength = 31;
            if (string.IsNullOrEmpty(workseetName))
            {
                workseetName = LocalizationsConstants.DefaultSheetName;
            }
            else if (workseetName.Length > maxLength)
            {
                workseetName = workseetName.Substring(0, maxLength);
            }

            return workseetName;
        }

        public void SetAuthor(IWorkbook workbook, string author)
        {
            if (workbook is XSSFWorkbook)
            {
                XSSFWorkbook xssfWorkbook = workbook as XSSFWorkbook;
                POIXMLProperties xmlProps = xssfWorkbook.GetProperties();
                CoreProperties coreProps = xmlProps.CoreProperties;
                coreProps.Creator = author;

                return;
            }

            if (workbook is NPOI.HSSF.UserModel.HSSFWorkbook)
            {
                NPOI.HSSF.UserModel.HSSFWorkbook hssfWorkbook = workbook as NPOI.HSSF.UserModel.HSSFWorkbook;
                SummaryInformation summaryInfo = hssfWorkbook.SummaryInformation;

                if (summaryInfo != null)
                {
                    summaryInfo.Author = author;
                    return;
                }

                DocumentSummaryInformation newDocInfo = PropertySetFactory.CreateDocumentSummaryInformation();

                SummaryInformation newInfo = PropertySetFactory.CreateSummaryInformation();
                newInfo.Author = author;

                hssfWorkbook.DocumentSummaryInformation = newDocInfo;
                hssfWorkbook.SummaryInformation = newInfo;

                return;
            }
        }
    }
}
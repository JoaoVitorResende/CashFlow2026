using ClosedXML.Excel;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel
{
    public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository;

        public GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> Execute(DateOnly month)
        {
            var expenses = await _repository.FilterByMonth(month);
            var workbook = new XLWorkbook();
            workbook.Style.Font.FontSize = 14;
            workbook.Style.Font.FontName = "Times New Roman";
            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
            InsertHeader(worksheet);
            var file = new MemoryStream();
            workbook.SaveAs(file);
            return file.ToArray();
        }

        private void InsertHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = ResourceReportGenerationMessage.TITLE;
            worksheet.Cell("B1").Value = ResourceReportGenerationMessage.DATE;
            worksheet.Cell("C1").Value = ResourceReportGenerationMessage.PAYMNENT_TYPE;
            worksheet.Cell("D1").Value = ResourceReportGenerationMessage.AMOUNT;
            worksheet.Cell("E1").Value = ResourceReportGenerationMessage.DESCRIPTION;

            worksheet.Cells("A1:E1").Style.Font.Bold = true;
            worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }
    }
}

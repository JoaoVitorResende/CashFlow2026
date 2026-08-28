using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel
{
    public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
    {
        public Task<byte[]> Execute(DateOnly month)
        {
            var workbook = new XLWorkbook();
            workbook.Style.Font.FontSize = 14;
            workbook.Style.Font.FontName = "Times New Roman";
            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        }
    }
}

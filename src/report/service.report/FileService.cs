using ClosedXML.Excel;
using entity.report;

namespace service.report;

public class FileService : IFileService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns>Generated file path.</returns>
    public string GenerateExcel(List<ContactReportResponse> data)
    {
        ArgumentNullException.ThrowIfNull(data);

        using var workBook = new XLWorkbook();

        //Sheet
        var workSheet = workBook.Worksheets.Add("Report");

        //Headers
        workSheet.Cell("A1").Value = "Location";
        workSheet.Cell("B1").Value = "Contact Count in that Location";
        workSheet.Cell("C1").Value = "Telephone Number Count in that Location";

        //Body
        for (int i = 0; i < data.Count; i++)
        {
            var row = data[i];

            workSheet.Cell($"A{i + 2}").Value = row.Location;
            workSheet.Cell($"B{i + 2}").Value = row.ContactCountInThatLocation;
            workSheet.Cell($"C{i + 2}").Value = row.TelephoneNumberCountInThatLocation;
        }

        //Styles
        var headerRow = workSheet.Row(1);
        headerRow.Style.Font.SetBold();
        headerRow.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        workSheet.Columns("A,B,C").Width = 40;
        workSheet.Cells("A1:C1").Style.Fill.SetBackgroundColor(XLColor.DarkGray);

        //Save
        var filePath = $"Reports\\{Guid.NewGuid()}.xlsx";
        workBook.SaveAs(filePath);

        return filePath;
    }
}
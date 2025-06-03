using H;
using Microsoft.Reporting.WinForms;

namespace Monitoring.Models;

public static class Report
{
    public static void Load(LocalReport report)
    {
        var items = new[]
        {
            new ReportItem
            {
                Description = "Widget 6000",
                Price = 104.99m,
                Qty = 1,
            },
            new ReportItem
            {
                Description = "Gizmo MAX",
                Price = 1.41m,
                Qty = 25,
            },
        };
        var parameters = new[] { new ReportParameter("Title", "Invoice 4/2020") };
        using var stream = Resources.TestReport_rdlc.AsStream();
        report.LoadReportDefinition(stream);
        // report.ReportEmbeddedResource = "Monitoring.Resources.Reports.TestReport.rdlc";
        report.DataSources.Add(new ReportDataSource("Items", items));
        report.SetParameters(parameters);
    }
}

public class ReportItem
{
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Qty { get; set; }
    public decimal Total => Price * Qty;
}

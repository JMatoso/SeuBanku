using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SeuBanku.Entities;
using System.Globalization;

namespace SeuBanku.Helpers.Document
{
    public class InvoiceDocument : IDocument
    {
        private readonly Extract _extract;

        public InvoiceDocument(Extract extract) => _extract = extract;

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeFooter);
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default
                                .FontSize(14)
                                .NormalWeight()
                                .FontColor(Colors.Grey.Medium);

            container
                .Background(Colors.White)
                .BorderBottom(2)
                .BorderColor(Colors.Grey.Lighten3)
                .PaddingBottom(25)
                .Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text($"Invoice #{_extract.Reference}").Style(titleStyle);

                        column.Item().Text(text =>
                        {
                            text.Span("Issue date: ").SemiBold();
                            text.Span(DateTime.UtcNow.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture));
                        });
                    });

                    row.ConstantItem(100)
                        .Height(50)
                        .Background(Colors.White)
                        .Text(e =>
                        {
                            e.Span("SeuBanku")
                                .NormalWeight()
                                .FontColor(Colors.Blue.Medium)
                                .FontSize(18);
                        });
                });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().PaddingTop(25).Element(ComposeComments);

                column.Item().Element(ComposeTable);
            });
        }

        void ComposeTable(IContainer container)
        {
            container
                .Padding(15)
                .PaddingLeft(135)
                .AlignMiddle()
                .AlignCenter()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(100);
                        columns.ConstantColumn(150);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(3);
                    });


                    table.Cell().Row(1).Column(1).Width(100).AlignRight().BorderRight(1).BorderColor(Colors.Blue.Medium).PaddingRight(25).Text("Date - Time").SemiBold();
                    table.Cell().Row(1).Column(2).Width(150).PaddingLeft(25).Text(_extract.OperationDate.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture));

                    table.Cell().Row(2).Column(1).Width(100).AlignRight().BorderRight(1).BorderColor(Colors.Blue.Medium).PaddingRight(25).Text("Operation").SemiBold();
                    table.Cell().Row(2).Column(2).Width(100).PaddingLeft(25).Text(_extract.Operation);

                    table.Cell().Row(3).Column(1).Width(100).AlignRight().BorderRight(1).BorderColor(Colors.Blue.Medium).PaddingRight(25).Text("Recipient").SemiBold();
                    table.Cell().Row(3).Column(2).Width(100).PaddingLeft(25).Text(_extract.To.ToUpper());

                    table.Cell().Row(4).Column(1).Width(100).AlignRight().BorderRight(1).BorderColor(Colors.Blue.Medium).PaddingRight(25).Text("Account").SemiBold();
                    table.Cell().Row(4).Column(2).Width(100).PaddingLeft(25).Text(_extract.InAccountNumber);

                    table.Cell().Row(5).Column(1).Width(100).AlignRight().BorderRight(1).BorderColor(Colors.Blue.Medium).PaddingRight(25).Text("Amount").SemiBold();
                    table.Cell().Row(5).Column(2).Width(100).PaddingLeft(25).Text($"{_extract.OutcomingBalance} AKZ");

                    table.Cell().Row(6).Column(1).Width(100).AlignRight().BorderRight(1).BorderColor(Colors.Blue.Medium).PaddingRight(25).Text("Reference").SemiBold();
                    table.Cell().Row(6).Column(2).Width(100).PaddingLeft(25).Text($"{new Random().Next(1000000, 9999999)}");
                });
        }

        void ComposeFooter(IContainer container)
        {
            container
                .Background(Colors.Grey.Lighten3)
                .DefaultTextStyle(x => x.FontSize(12))
                .Padding(20)
                .Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.AlignCenter();

                        text.Span(_extract.From);
                    });
                    column.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                    column.Item().Text(text =>
                    {
                        text.AlignCenter();

                        text.Span("If you need any information, please contact our support line (24h):");
                    });

                    column.Item().Text(text =>
                    {
                        text.AlignCenter();

                        text.Span("(+244) 222 333 333 | 999 999 999");
                    });
                });
        }

        void ComposeComments(IContainer container)
        {
            container.Background(Colors.White).Padding(10).Column(column =>
            {
                column.Spacing(5);

                column.Item()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Digital Receipt")
                            .Weight(FontWeight.Medium)
                            .FontColor(Colors.Blue.Medium)
                            .FontSize(14);

                    });

                column.Item()
                    .AlignCenter()
                    .Text("Detail of the operation carried out through the SeuBanku online channel.");
            });
        }
    }
}

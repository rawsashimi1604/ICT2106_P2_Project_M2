﻿using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Entities;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout.Borders;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using iText.Kernel.Colors;



namespace SmartHomeManager.Domain.AnalysisDomain.Builders
{
    public class PdfBuilder
    {
        private readonly Document _document;
        private readonly string _fileName;
        private readonly PdfDocument _pdfDoc;
        private const string FILEPATH = "../SmartHomeManager.Domain/AnalysisDomain/Files/";

        public PdfBuilder(string fileName, PdfDocument pdfDocument)
        {
            _pdfDoc = pdfDocument;
            _document = new Document(pdfDocument);
            _fileName = fileName;
        }
//-----------------------------------------------------------------------------
        // Builder to add details for the device
        public PdfBuilder addDeviceDetails(Device device)
        {
            // Add header of the report
            _document.Add(new Paragraph($"Device {device.DeviceId} REPORT")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBold()
                .SetFontSize(15));

            // Create a table for device
            float[] deviceTableWidths = { 150F, 300F };
            Table deviceTable = new Table(deviceTableWidths);

            // Add cells to the device table
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device ID")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceId}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Name")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceName}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Brand")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceBrand}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Model")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceModel}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Type")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceType}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Type Name")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceTypeName}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Serial Number")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceSerialNumber}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Watts")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceWatts}")));

            _document.Add(deviceTable);

            return this;
        }

//-----------------------------------------------------------------------------
        // Builder to add the device total energy usage
        public PdfBuilder addDeviceLogTotalUsage(double totalUsage)
        {
            _document.Add(new Paragraph($"Total Usage for Device : {totalUsage}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(15));
            return this;
        }


//-----------------------------------------------------------------------------
        // Builder to add the devices
        public PdfBuilder addHouseholdDetails(Device device) {

            // Create header for device table
            _document.Add(new Paragraph($"Device {device.DeviceId}")
                .SetBold()
                .SetFontSize(15)
                .SetUnderline()
                .SetTextAlignment(TextAlignment.LEFT));

            // Create a table for device
            float[] deviceTableWidths = { 150F, 300F };
            Table deviceTable = new Table(deviceTableWidths);


            // Add cells to the device table

            // Device ID
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device ID")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceId}")));

            // Device Name
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Name")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceName}")));

            // Device Brand
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Brand")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceBrand}")));

            // Device Model
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Model")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceModel}")));

            // Device Type Name
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Type Name")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceTypeName}")));


            //Device Serial Number
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Serial Number")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceSerialNumber}")));

            // Device Watts
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Watts")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceWatts}")));

            _document.Add(deviceTable).SetTextAlignment(TextAlignment.CENTER);

            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to add header for the household
        public PdfBuilder addHouseholdHeader(Guid accId)
        {
            _document.Add(new Paragraph($"Household Report For Account {accId}")
                .SetBold()
                .SetFontSize(20)
                .SetBorder(new SolidBorder(1))
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY));
            return this;
        }
//-----------------------------------------------------------------------------
        //Builder to add total household energy usage
        public PdfBuilder addTotalHouseUsage(double householdUsage)
        {
            _document.Add(new Paragraph($"Total household energy usage is {householdUsage}")
                .SetTextAlignment(TextAlignment.CENTER).
                SetBold()
                .SetFontSize(15));
            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to generated the current time 
        public PdfBuilder addGeneratedTime()
        {
            // create date time 
            DateTime now = DateTime.Now;
            _document.Add(new Paragraph("Report Generated on: " + now)
                .SetTextAlignment(TextAlignment.RIGHT));
            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to test
        public PdfBuilder Date(DateTime start, DateTime end)
        {
            _document.Add(new Paragraph($"Report From {start} to {end}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(15));
            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to build the table for monthly stats

        public PdfBuilder addMonthlyStats(
            int lastMonths,
            List<String> allMonthYearStrings,
            List<double> allEnergyCost,
            List<double> allEnergyUsage
            )
        {
            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));

            _document.Add(new Paragraph("The table below is the statistics of the device")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBold());

            // Create a table for device
            float[] tableWidths = { 150F, 150F, 150F };
            Table table = new Table(tableWidths);

            // Add table headers to the table
            table.AddCell(new Cell().Add(new Paragraph("Month")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            table.AddCell(new Cell().Add(new Paragraph("Usage (W)")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            table.AddCell(new Cell().Add(new Paragraph("Cost ($)")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));


            // Add data to the table
            for(int i = 0; i < lastMonths; i++)
            {
                table.AddCell(new Cell().Add(new Paragraph(allMonthYearStrings[i])));
                table.AddCell(new Cell().Add(new Paragraph(allEnergyUsage[i].ToString("0.##"))));
                table.AddCell(new Cell().Add(new Paragraph(allEnergyCost[i].ToString("0.##"))));
            }

            _document.Add(table).SetTextAlignment(TextAlignment.CENTER);

            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to add the total usage and total cost
        public PdfBuilder addTotalUsageCost(double overallUsage, double overallCost)
        {
            Paragraph front = new Paragraph().SetTextAlignment(TextAlignment.LEFT);
            front.Add("The total Usage for this device is ");
            Text usage = new Text($"{overallUsage.ToString("0.##")} Watts ")
                .SetFontColor(ColorConstants.RED)
                .SetBold();

            Text between = new Text("and total cost is ");
            
            Text cost = new Text($"${overallCost.ToString("0.##")}")
                .SetFontColor(ColorConstants.RED)
                .SetBold();

            front.Add(usage);
            front.Add(between);
            front.Add(cost);

            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));

            _document.Add(front)
                .SetTextAlignment(TextAlignment.LEFT);

            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));

            //_document.Add(new Paragraph($"The total Usage for this device is {overallUsage.ToString("0.##")} and total cost is ${overallCost.ToString("0.##")}"));
            //_document.Add(new Paragraph($"Total Usage (W): {overallUsage.ToString("0.##")}")
            //    .SetTextAlignment(TextAlignment.CENTER)
            //    .SetBold());

            //_document.Add(new Paragraph($"Total Cost ($): {overallCost.ToString("0.##")}")
            //    .SetTextAlignment(TextAlignment.CENTER)
            //    .SetBold());

            return this;
        }


//-----------------------------------------------------------------------------
        public PdfBuilder addDeviceParagraph()
        {
            _document.Add(new Paragraph("Section A: Device Reports")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(15));

            _document.Add(new Paragraph("This section holds the information of the devices held in your household with the breakdown of the monthly statistics of the individual device")
                .SetTextAlignment(TextAlignment.CENTER));
            return this;
        }

//-----------------------------------------------------------------------------
        // Builder to add forecast to household report
        public PdfBuilder addForecastReport(IEnumerable<ForecastChartData> forecast)
        {

            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));

            DateTime dt = DateTime.Now;

            var currentMonth = dt.Month;

            _document.Add(new Paragraph("Section B: Upcoming Forecast Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(15));

            _document.Add(new Paragraph("This section shows the forecasted report for your household with the forecast data of energy usage and cost for the rest of the year")
                .SetTextAlignment(TextAlignment.CENTER));

            // Create a table for device
            float[] tableWidths = { 150F, 150F, 150F };
            Table table = new Table(tableWidths);

            // Add table headers to the table
            table.AddCell(new Cell().Add(new Paragraph("Month")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            table.AddCell(new Cell().Add(new Paragraph("Usage (W)")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            table.AddCell(new Cell().Add(new Paragraph("Cost ($)")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));



            foreach (var data in forecast)
            {
                
                for (var i = dt.Month + 1; i <= 12; i++)
                {
                    DateTime newDt = dt.AddMonths(i - dt.Month);
                    if(newDt.ToString("MMMM") == data.Label)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(data.Label)));
                        table.AddCell(new Cell().Add(new Paragraph(data.WattsValue.ToString("0.##"))));
                        table.AddCell(new Cell().Add(new Paragraph(data.PriceValue.ToString("0.##"))));
                    }
                }


            }

            _document.Add(new Paragraph(" "));
            _document.Add(new Paragraph(" "));


            _document.Add(table).SetTextAlignment(TextAlignment.CENTER);

            return this;
        }

//-----------------------------------------------------------------------------
        public PdfBuilder addHouseholdOverall(double usage, double cost)
        {

            Paragraph front = new Paragraph()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(1))
                .SetBackgroundColor(ColorConstants.YELLOW);

            front.Add("The total Household Usage is ");
            Text usageT = new Text($"{usage.ToString("0.##")} Watts ")
                .SetFontColor(ColorConstants.RED)
                .SetBold();

            Text between = new Text("and total cost is ");

            Text costT = new Text($"${cost.ToString("0.##")}")
                .SetFontColor(ColorConstants.RED)
                .SetBold();

            front.Add(usageT);
            front.Add(between);
            front.Add(costT);

            _document.Add(front);

            return this;
        }
//-----------------------------------------------------------------------------

        public PdfBuilder addEnergyEfficiency(IEnumerable<EnergyEfficiency> energyEfficiency)
        {

            // Create a table for device
            float[] tableWidths = { 150F, 150F, 150F };
            Table table = new Table(tableWidths);

            // Add table headers to the table
            table.AddCell(new Cell().Add(new Paragraph("Device ID")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            table.AddCell(new Cell().Add(new Paragraph("Energy Efficiency Index")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));
            table.AddCell(new Cell().Add(new Paragraph("Date of Analysis")
                .SetBold()
                .SetBackgroundColor(ColorConstants.GREEN)));

            foreach(var data in energyEfficiency)
            {
                table.AddCell(new Cell().Add(new Paragraph($"{data.DeviceId}")));
                table.AddCell(new Cell().Add(new Paragraph($"{data.EnergyEfficiencyIndex}")));
                table.AddCell(new Cell().Add(new Paragraph($"{data.DateOfAnalysis}")));
            }

            _document.Add(table);


            return this;
        }

//-----------------------------------------------------------------------------
        // Build the file
        public byte[] Build()
        {
            _document.Close();
            return System.IO.File.ReadAllBytes(FILEPATH + _fileName);
        }
    }
}

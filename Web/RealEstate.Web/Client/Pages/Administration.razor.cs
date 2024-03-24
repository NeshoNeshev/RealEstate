﻿using BlazorBootstrap;

namespace RealEstate.Web.Client.Pages
{
    partial class Administration
    {
        private BarChart barChart = default!;
        private BarChartOptions barChartOptions = default!;
        private ChartData chartData = default!;

        private int datasetsCount = 0;
        private int labelsCount = 0;
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private Random random = new();

        protected override void OnInitialized()
        {
            chartData = new ChartData { Labels = GetDefaultDataLabels(6), Datasets = GetDefaultDataSets(3) };
            barChartOptions = new BarChartOptions { Responsive = true, Interaction = new Interaction { Mode = InteractionMode.Index } };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await barChart.InitializeAsync(chartData, barChartOptions);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task RandomizeAsync()
        {
            if (chartData is null || chartData.Datasets is null || !chartData.Datasets.Any()) return;

            var newDatasets = new List<IChartDataset>();

            foreach (var dataset in chartData.Datasets)
            {
                if (dataset is BarChartDataset barChartDataset
                    && barChartDataset is not null
                    && barChartDataset.Data is not null)
                {
                    var count = barChartDataset.Data.Count;

                    var newData = new List<double>();
                    for (var i = 0; i < count; i++)
                    {
                        newData.Add(random.Next(200));
                    }

                    barChartDataset.Data = newData;
                    newDatasets.Add(barChartDataset);
                }
            }

            chartData.Datasets = newDatasets;

            await barChart.UpdateAsync(chartData, barChartOptions);
        }

        private async Task AddDatasetAsync()
        {
            if (chartData is null || chartData.Datasets is null) return;

            if (datasetsCount >= 12)
                return;

            var chartDataset = GetRandomBarChartDataset();
            chartData = await barChart.AddDatasetAsync(chartData, chartDataset, barChartOptions);
        }

        private async Task AddDataAsync()
        {
            if (chartData is null || chartData.Datasets is null)
                return;

            if (labelsCount >= 12)
                return;

            var data = new List<IChartDatasetData>();
            foreach (var dataset in chartData.Datasets)
            {
                if (dataset is BarChartDataset barChartDataset)
                    data.Add(new BarChartDatasetData(barChartDataset.Label, random.Next(200)));
            }

            chartData = await barChart.AddDataAsync(chartData, GetNextDataLabel(), data);
        }

        private async Task ShowHorizontalBarChartAsync()
        {
            barChartOptions.IndexAxis = "y";
            await barChart.UpdateAsync(chartData, barChartOptions);
        }

        private async Task ShowVerticalBarChartAsync()
        {
            barChartOptions.IndexAxis = "x";
            await barChart.UpdateAsync(chartData, barChartOptions);
        }


        private List<IChartDataset> GetDefaultDataSets(int numberOfDatasets)
        {
            var datasets = new List<IChartDataset>();

            for (var index = 0; index < numberOfDatasets; index++)
            {
                datasets.Add(GetRandomBarChartDataset());
            }

            return datasets;
        }

        private BarChartDataset GetRandomBarChartDataset()
        {
            var c = ColorBuilder.CategoricalTwelveColors[datasetsCount].ToColor();

            datasetsCount += 1;

            return new BarChartDataset()
            {
                Label = $"Product {datasetsCount}",
                Data = GetRandomData(),
                BackgroundColor = new List<string> { c.ToRgbString() },
                BorderColor = new List<string> { c.ToRgbString() },
                BorderWidth = new List<double> { 0 },
            };
        }

        private List<double> GetRandomData()
        {
            var data = new List<double>();
            for (var index = 0; index < labelsCount; index++)
            {
                data.Add(random.Next(200));
            }

            return data;
        }

        private List<string> GetDefaultDataLabels(int numberOfLabels)
        {
            var labels = new List<string>();
            for (var index = 0; index < numberOfLabels; index++)
            {
                labels.Add(GetNextDataLabel());
            }

            return labels;
        }

        private string GetNextDataLabel()
        {
            labelsCount += 1;
            return months[labelsCount - 1];
        }
    }
}

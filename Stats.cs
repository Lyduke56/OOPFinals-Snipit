using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using SnipIt.Models;
using SnipIt.Managers;

namespace SnipIt
{
    public partial class Stats : UserControl
    {
        private Color panelBackground = ColorTranslator.FromHtml("#334155"); 

        private OxyColor oxyCColor = OxyColor.FromRgb(14, 165, 233);     
        private OxyColor oxyCppColor = OxyColor.FromRgb(249, 115, 22);  
        private OxyColor oxyPythonColor = OxyColor.FromRgb(234, 179, 8);

        private OxyColor titleColor = OxyColor.FromRgb(248, 250, 252);  
        private OxyColor labelColor = OxyColor.FromRgb(226, 232, 240); 
        private OxyColor gridColor = OxyColor.FromRgb(100, 116, 139);

        private OxyColor oxyBackgroundColor;

        private PlotView donutPlotView;
        private PlotView cPlotView;
        private PlotView cppPlotView;
        private PlotView pythonPlotView;
        private PlotModel plotModel;

        private List<Snippet> snippets;

        public Stats()
        {
            InitializeComponent();

            pnlDonut.BackColor = panelBackground;
            pnlCdist.BackColor = panelBackground;
            pnlCppdist.BackColor = panelBackground;
            pnlPydist.BackColor = panelBackground;

            oxyBackgroundColor = OxyColor.FromRgb(
                panelBackground.R,
                panelBackground.G,
                panelBackground.B);

            LoadSnippetData();

            InitializeCharts();
        }

        public Dashboard stats
        {
            get => default;
            set
            {
            }
        }

        internal Dashboard Dashboard
        {
            get => default;
            set
            {
            }
        }

        private void LoadSnippetData()
        {
            try
            {
                List<Snippet> allSnippets = SnippetManager.LoadAllSnippets();

                if (timer.SessionManager.UserId > 0)
                {
                    string userId = timer.SessionManager.UserId.ToString();
                    snippets = allSnippets.Where(s => s.UserId == userId).ToList();
                }
                else
                {
                    snippets = allSnippets;
                }

                if (snippets == null || snippets.Count == 0)
                {
                    MessageBox.Show("No snippets found. Create some snippets to see statistics.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    snippets = new List<Snippet>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading snippet data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                snippets = new List<Snippet>();
            }
        }

        private void InitializeCharts()
        {
            try
            {
                InitializeDonutChart();
                InitializeCDistributionChart();
                InitializeCppDistributionChart();
                InitializePythonDistributionChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing charts: {ex.Message}", "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDonutChart()
        {
            // group snippets by language and count them
            var languageCounts = snippets
                .GroupBy(s => s.Language.ToLower())
                .Select(g => new { Language = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // calculate total for percentage
            int total = languageCounts.Sum(x => x.Count);

            // If no snippets, don't try to create the chart
            if (total == 0)
            {
                var emptyModel = CreateEmptyPlotModel("No Snippets Available");

                donutPlotView = new PlotView
                {
                    Dock = DockStyle.Fill,
                    Model = emptyModel,
                    BackColor = panelBackground
                };

                pnlDonut.Controls.Clear();
                pnlDonut.Controls.Add(donutPlotView);
                return;
            }

            // plot model
            plotModel = new PlotModel
            {
                Title = "Snippets by Language",
                TitleFontSize = 30,
                TitleFontWeight = FontWeights.Bold,
                TitleColor = titleColor,
                Background = oxyBackgroundColor,
                PlotAreaBorderThickness = new OxyThickness(0),
                PlotMargins = new OxyThickness(40, 50, 40, 40)
            };  

            var pieSeries = new PieSeries
            {
                StrokeThickness = 1.0,
                InnerDiameter = 0.5,
                Diameter = 0.7,

                // Position settings
                AngleSpan = 360,
                StartAngle = 0,
                ExplodedDistance = 0,

                // Text formatting
                TextColor = OxyColors.White,
                FontSize = 12,
                FontWeight = FontWeights.Bold,

                // Label settings for outside positioning
                LabelField = "{1}",
                TrackerFormatString = "{1}: {2:0} ({3:P0})",
                OutsideLabelFormat = "{0} ({1:P0})",  // Show label with percentage outside
                InsideLabelFormat = "",  // Hide inside labels
            
            };

            // Add slices for each language
            foreach (var item in languageCounts)
            {
                string languageDisplay = item.Language == "c" ? "C" :
                                        item.Language == "cpp" ? "C++" :
                                        item.Language == "python" ? "Python" :
                                        item.Language;

                // Calculate percentage
                double percentage = (double)item.Count / total * 100;

                OxyColor sliceColor;
                if (item.Language == "c")
                    sliceColor = oxyCColor;
                else if (item.Language == "cpp")
                    sliceColor = oxyCppColor;
                else if (item.Language == "python")
                    sliceColor = oxyPythonColor;
                else
                    sliceColor = OxyColors.Gray;

                // Add the slice with label including both percentage and actual count
                pieSeries.Slices.Add(new PieSlice(
                $"{languageDisplay} - {Math.Round(percentage)}%",
                item.Count)
                {
                    Fill = sliceColor,
                    IsExploded = false
                });
            }

            plotModel.Series.Add(pieSeries);
            donutPlotView = new PlotView
            {
                Dock = DockStyle.Fill,
                Model = plotModel,
                BackColor = panelBackground
            };

            pnlDonut.Controls.Clear();
            pnlDonut.Controls.Add(donutPlotView);
        }

        private void InitializeCDistributionChart()
        {
            // Filter for C snippets only and group by code type
            var cTypeDistribution = snippets
                .Where(s => s.Language.ToLower() == "c")
                .GroupBy(s => s.CodeType)
                .Select(g => new { CodeType = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // If no C snippets, display a message
            if (cTypeDistribution.Count == 0)
            {
                var emptyModel = CreateEmptyPlotModel("No C Snippets Available");

                cPlotView = new PlotView
                {
                    Dock = DockStyle.Fill,
                    Model = emptyModel,
                    BackColor = panelBackground
                };

                pnlCdist.Controls.Clear();
                pnlCdist.Controls.Add(cPlotView);
                return;
            }

            plotModel = CreateBarChart("C Snippets by Type", cTypeDistribution, oxyCColor);
            cPlotView = new PlotView
            {
                Dock = DockStyle.Fill,
                Model = plotModel,
                BackColor = panelBackground
            };

            pnlCdist.Controls.Clear();
            pnlCdist.Controls.Add(cPlotView);
        }

        private void InitializeCppDistributionChart()
        {
            // Filter for C++ snippets only and group by code type
            var cppTypeDistribution = snippets
                .Where(s => s.Language.ToLower() == "cpp")
                .GroupBy(s => s.CodeType)
                .Select(g => new { CodeType = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // If no C++ snippets, display a message
            if (cppTypeDistribution.Count == 0)
            {
                var emptyModel = CreateEmptyPlotModel("No C++ Snippets Available");

                cppPlotView = new PlotView
                {
                    Dock = DockStyle.Fill,
                    Model = emptyModel,
                    BackColor = panelBackground
                };

                pnlCppdist.Controls.Clear();
                pnlCppdist.Controls.Add(cppPlotView);
                return;
            }

            plotModel = CreateBarChart("C++ Snippets by Type", cppTypeDistribution, oxyCppColor);

            cppPlotView = new PlotView
            {
                Dock = DockStyle.Fill,
                Model = plotModel,
                BackColor = panelBackground
            };

            pnlCppdist.Controls.Clear();
            pnlCppdist.Controls.Add(cppPlotView);
        }

        private void InitializePythonDistributionChart()
        {
            // Filter for Python snippets only and group by code type
            var pythonTypeDistribution = snippets
                .Where(s => s.Language.ToLower() == "python")
                .GroupBy(s => s.CodeType)
                .Select(g => new { CodeType = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // If no Python snippets, display a message
            if (pythonTypeDistribution.Count == 0)
            {
                var emptyModel = CreateEmptyPlotModel("No Python Snippets Available");

                pythonPlotView = new PlotView
                {
                    Dock = DockStyle.Fill,
                    Model = emptyModel,
                    BackColor = panelBackground
                };

                pnlPydist.Controls.Clear();
                pnlPydist.Controls.Add(pythonPlotView);
                return;
            }

            plotModel = CreateBarChart("Python Snippets by Type", pythonTypeDistribution, oxyPythonColor);

            pythonPlotView = new PlotView
            {
                Dock = DockStyle.Fill,
                Model = plotModel,
                BackColor = panelBackground
            };


            pnlPydist.Controls.Clear();
            pnlPydist.Controls.Add(pythonPlotView);
        }

        private PlotModel CreateEmptyPlotModel(string title)
        {
            return new PlotModel
            {
                Title = title,
                TitleFontSize = 16,
                TitleFontWeight = FontWeights.Bold,
                TitleColor = titleColor,
                Background = oxyBackgroundColor
            };
        }

        private PlotModel CreateBarChart<T>(string title, List<T> data, OxyColor barColor) where T : class
        {
            var model = new PlotModel
            {
                Title = title,
                TitleFontSize = 16,
                TitleFontWeight = FontWeights.Bold,
                TitleColor = titleColor,
                Background = oxyBackgroundColor,
                PlotAreaBorderThickness = new OxyThickness(0)
            };

            // Extract code types and counts
            var codeTypes = data.Select(x => x.GetType().GetProperty("CodeType").GetValue(x).ToString()).ToArray();
            var counts = data.Select(x => Convert.ToInt32(x.GetType().GetProperty("Count").GetValue(x))).ToArray();

            // Create category axis (Y-axis for horizontal bars)
            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "CodeTypes",
                ItemsSource = codeTypes,
                Title = "Code Type",
                TitleFontWeight = FontWeights.Bold,
                TitleColor = labelColor,
                TextColor = labelColor,
                AxislineStyle = LineStyle.Solid,
                AxislineThickness = 1,
                AxislineColor = labelColor,
                TicklineColor = labelColor
            };
            model.Axes.Add(categoryAxis);

            // find the maximum count to set the axis maximum so it looks nicer
            int maxCount = counts.Max();

            // create value axis (X-axis for horizontal bars)
            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Count",
                TitleFontWeight = FontWeights.Bold,
                TitleColor = labelColor,
                TextColor = labelColor,
                MinimumPadding = 0,
                MaximumPadding = 0.1,
                AbsoluteMinimum = 0,
                Maximum = Math.Max(maxCount + 1, 5), 
                MajorStep = 1,
                MinorStep = 1,
                StringFormat = "0", // whole numbers only
                AxislineStyle = LineStyle.Solid,
                AxislineThickness = 1,
                AxislineColor = labelColor,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = gridColor,
                MinorGridlineStyle = LineStyle.None
            };
            model.Axes.Add(valueAxis);

            var barSeries = new BarSeries
            {
                ItemsSource = counts.Select(x => new BarItem(x)),
                StrokeThickness = 0,
                FillColor = barColor,
                BarWidth = 0.7,
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                TextColor = OxyColors.White,
                FontWeight = FontWeights.Bold
            };


            model.Series.Add(barSeries);

            return model;
        }

        private void pnlDonut_Paint(object sender, PaintEventArgs e)
        {

        }

        public void RefreshCharts()
        {
            // Reload data and rebuild charts
            LoadSnippetData();
            InitializeCharts();
        }
    }
}
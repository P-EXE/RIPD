using CommunityToolkit.Maui.Converters;
using Microcharts;
using RIPDApp.ViewModels;
using SkiaSharp;

namespace RIPDApp.Pages;

public partial class DiaryTodayPage : ContentPage
{

	ChartEntry[] entries = new[]
	{
		new ChartEntry(3400)
		{
			Label = "Consumed Calories",
			ValueLabel = "3400",
			Color = SKColors.LimeGreen
		},
    new ChartEntry(2303)
    {
      Label = "Burned Calories",
      ValueLabel = "2303",
      Color = SKColors.Red
    },

  };

	private readonly DiaryVM _vm;
	public DiaryTodayPage(DiaryVM vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
		chartView.Chart = new PointChart
		{
			//BackgroundColor = SKColor.Parse("#172610"),
			BackgroundColor = SKColors.Black,
			
			Entries = entries
		};
	}
}
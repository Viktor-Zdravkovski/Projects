using CommunityToolkit.Maui.Views;
using SymptomResponderApp.DataBase;
using SymptomResponderApp.Domain.Models;
using SymptomResponderApp.Service.Implementations;
using SymptomResponderApp.Service.Interfaces;

namespace SymptomResponderApp
{
    public partial class MainPage : ContentPage
    {
        private readonly ISymptomAnalyzer _analyzerService;
        private List<Situation> allSituations;

        public MainPage()
        {
            InitializeComponent();

            _analyzerService = new SymptomAnalyzer(); // <-- this

            allSituations = ConditionRepository.GetConditions();

            var allSymptoms = allSituations
                .SelectMany(c => c.Symptoms)
                .Distinct()
                .ToList();

            SymptomPicker1.ItemsSource = allSymptoms;
            SymptomPicker2.ItemsSource = allSymptoms;
            SymptomPicker3.ItemsSource = allSymptoms;
        }

        private void OnAnalyzeClicked(object sender, EventArgs e)
        {
            var selectedSymptoms = new List<string>
        {
            SymptomPicker1.SelectedItem?.ToString(),
            SymptomPicker2.SelectedItem?.ToString(),
            SymptomPicker3.SelectedItem?.ToString()
        }.Where(s => !string.IsNullOrEmpty(s)).ToList();

            var results = _analyzerService.Analyze(selectedSymptoms, allSituations);

            if (results.Count == 0)
            {
                DisplayAlert("Result", "No matching conditions found.", "OK");
                return;
            }

            string message = string.Join("\n", results.Select(r => $"{r.Situation.Name} ({r.MatchCount} symptoms matched)"));
            DisplayAlert("Possible Conditions", message, "OK");
        }
    }
}

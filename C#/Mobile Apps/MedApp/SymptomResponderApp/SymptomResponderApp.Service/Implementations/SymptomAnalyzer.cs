using SymptomResponderApp.Domain.Models;
using SymptomResponderApp.Service.Interfaces;

namespace SymptomResponderApp.Service.Implementations
{
    public class SymptomAnalyzer : ISymptomAnalyzer
    {
        public List<SituationMatch> Analyze(List<string> selectedSymptoms, List<Situation> allConditions)
        {
            var results = new List<SituationMatch>();

            foreach (var situation in allConditions)
            {
                int matchCount = situation.Symptoms
                    .Count(symptom => selectedSymptoms.Any(selected =>
                        symptom.Equals(selected, System.StringComparison.OrdinalIgnoreCase)));

                if (matchCount > 0)
                {
                    results.Add(new SituationMatch
                    {
                        Situation = situation,
                        MatchCount = matchCount
                    });
                }
            }
            return results.OrderByDescending(r => r.MatchCount).ToList();
        }
    }
}

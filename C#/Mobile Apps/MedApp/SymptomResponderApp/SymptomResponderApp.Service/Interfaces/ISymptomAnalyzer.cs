using SymptomResponderApp.Domain.Models;

namespace SymptomResponderApp.Service.Interfaces
{
    public interface ISymptomAnalyzer
    {
        List<SituationMatch> Analyze(List<string> selectedSymptoms, List<Situation> allConditions);
    }
}

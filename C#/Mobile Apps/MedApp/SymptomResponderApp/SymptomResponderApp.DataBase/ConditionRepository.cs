using SymptomResponderApp.Domain.Models;

namespace SymptomResponderApp.DataBase
{
    public static class ConditionRepository
    {
        public static List<Situation> GetConditions()
        {
            return new List<Situation>
            {
                new Situation
                {
                    Name = "Heart Attack",
                    Symptoms = new List<string>
                    {
                        "Chest pain",
                        "Shortness of breath",
                        "Cold sweat",
                        "Fatigue or weakness"
                    }
                },
                new Situation
                {
                    Name = "Stroke",
                    Symptoms = new List<string>
                    {
                        "Face drooping",
                        "Arm weakness",
                        "Slurred speech",
                        "Sudden confusion",
                        "Blurred vision"
                    }
                },
                new Situation
                {
                    Name = "Panic Attack",
                    Symptoms = new List<string>
                    {
                        "Rapid heartbeat",
                        "Shortness of breath",
                        "Sweating",
                        "Dizziness",
                        "Fear of dying"
                    }
                },
                new Situation
                {
                    Name = "Heat Stroke",
                    Symptoms = new List<string>
                    {
                        "High body temperature",
                        "Hot dry skin",
                        "Confusion",
                        "Dizziness",
                        "Rapid pulse"
                    }
                }
            };
        }
    }
}

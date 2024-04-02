using UFAR.DM.API.Data.Entities;

namespace UFAR.DM.API.Core.Services.Section
{
    public interface ISectionServices
    {
        //Functions for Sections
        public SectionEntity GetSectionWithWords(int sectionId);
        public SectionEntity GetSectionWithExpressions(int sectionId);
        public SectionEntity GetSectionWithQuestions(int sectionId);
        public string AddSection(string section);
        public string DeleteSection(int sectionId);
        public int GetWordCount(int sectionId);
        public int GetExpressionCount(int sectionId);
        public string GetLevelOfSection(int sectionId);
        public void UpdateSectionLevel(int sectionId);
        public string MakeQuestionsBasedOnWE(int sectionId);
        public void AddWordToSection(int sectionId, WordEntity word);
        public bool hasWord(int sectionId, string word);
        public bool hasExp(int sectionId, string exp);
        public ICollection<SectionEntity> GetSections();
        public ICollection<QuizzEntity> MakeQuizz(int sectionId);
    }
}

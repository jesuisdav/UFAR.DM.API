using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UFAR.DM.API.Core.Services.ChatGPT;
using UFAR.DM.API.Core.Services.Question;
using UFAR.DM.API.Data.DAO;
using UFAR.DM.API.Data.Entities;

namespace UFAR.DM.API.Core.Services.Section
{
    public class SectionServices : ISectionServices
    {
        //Injection
        MainDbContext context;
        IGPTservices gpt;
        IQuestionServices question;
        public SectionServices(MainDbContext _context, IGPTservices _gpt, IQuestionServices _question)
        {
            context = _context;
            gpt = _gpt;
            question = _question;
        }
        public SectionEntity GetSectionWithWords(int sectionId) {
            return context.Sections
                .Include(s => s.Words) // Eager loading Words collection
                .FirstOrDefault(s => s.Id == sectionId);
        }
        public SectionEntity GetSectionWithExpressions(int sectionId) {
            return context.Sections
                .Include(s => s.Expressions) // Eager loading Expressions collection
                .FirstOrDefault(s => s.Id == sectionId);
        }
        public SectionEntity GetSectionWithQuestions(int sectionId) {
            return context.Sections
                    .Include(s => s.Questions) // Eager loading Expressions collection
                    .FirstOrDefault(s => s.Id == sectionId);
        }
        //Functions for Sections
        public string AddSection(string section)
        {
            int i = 0;
            foreach (var item in context.Sections)
            {
                if (item.Name == section)
                {
                    i++;
                    break;
                }
            }

            if (i == 1)
            {
                return "The section with " + section + " exists.";
            }
            SectionEntity sec = new SectionEntity() { 
                Name = section,
                Words = new List<WordEntity>(),
                Expressions = new List<ExpressionEntity>(),
                Questions = new List<QuestionEntity>()
            };
            context.Add(sec);
            context.SaveChanges();
            return "Done!";
        }
        public string DeleteSection(int sectionId) {
            if (context.Sections.Count() == 0) {
                return "There is no sections yet\n";
            }

            SectionEntity? deletingEntity = context.Sections.FirstOrDefault(e => e.Id == sectionId);

            if (deletingEntity != null) {
                context.Remove(deletingEntity);
            } else {
                return "There is no section with " + sectionId + " Id\n";
            }
            context.SaveChanges();
            return "Done\n";
        }
        public int GetWordCount(int sectionId) {
            // Include the Words collection when querying for the section
            var sectionWithWords = GetSectionWithWords(sectionId);

            // Check if the section exists
            if (sectionWithWords != null) {
                // Return the count of words in the section
                return sectionWithWords.Words.Count;
            } else {
                // Handle the case when the section doesn't exist
                return 0;
            }
        }
        public int GetExpressionCount(int sectionId)
        {
            // Include the Words collection when querying for the section
            var sectionWithExpressions = GetSectionWithExpressions(sectionId);

            // Check if the section exists
            if (sectionWithExpressions != null) {
                // Return the count of words in the section
                return sectionWithExpressions.Expressions.Count;
            } else {
                // Handle the case when the section doesn't exist
                return 0;
            }
        }
        public string GetLevelOfSection(int sectionId)
        {
            return context.Sections.FirstOrDefault(x => x.Id == sectionId).Level;
        }
        public void UpdateSectionLevel(int sectionId) {
            if (context.Sections.FirstOrDefault(x => x.Id == sectionId) == null)
            {
                return;
            }

            if ((GetExpressionCount(sectionId) == 0) && (GetWordCount(sectionId) == 0)) {
                context.Sections.FirstOrDefault(x => x.Id == sectionId).Level = "A0";
            } else {
                string wordsAndExpressions = "";
                SectionEntity sec = GetSectionWithWords(sectionId);
                if (sec.Words != null) {
                    foreach (var word in sec.Words) {
                        wordsAndExpressions += word + ", ";
                    }
                }
                sec = GetSectionWithExpressions(sectionId);
                if(sec.Expressions != null) {
                    foreach (var exp in sec.Expressions) {
                        wordsAndExpressions += exp + ", ";
                    }
                }

                string sectionLevel = gpt.LevelOfSection(wordsAndExpressions);
                context.Sections.FirstOrDefault(x => x.Id == sectionId).Level = sectionLevel;
            }
            context.SaveChanges();
        }
        public string MakeQuestionsBasedOnWE(int sectionId) {
            SectionEntity sec = context.Sections.FirstOrDefault(x => x.Id == sectionId);
            bool isEmpty = (sec.Words.Count() + sec.Expressions.Count() == 0);
            if (isEmpty)
            {
                return "There are no words or expressions yet.";
            }

            foreach (var word in sec.Words)
            {
                question.MakeQuestion(sectionId, word.Word);
            }
            foreach (var exp in sec.Expressions) {
                question.MakeQuestion(sectionId, exp.Expression);
            }
            return "Done";
        }
        public void AddWordToSection(int sectionId, WordEntity word) {
            context.Sections.FirstOrDefault(s => s.Id == sectionId).Words.Add(word);
            context.SaveChanges();
        }
        public bool hasWord(int sectionId, string word) {
            SectionEntity sectionWithWords = GetSectionWithWords(sectionId);
            word = word.ToLower();
            foreach (var w in sectionWithWords.Words)
            {
                if (w.Word == word)
                {
                    return true;
                }
            }
            return false;
        }
        public bool hasExp(int sectionId, string exp) {
            SectionEntity sectionWithWords = GetSectionWithExpressions(sectionId);
            exp = exp.ToLower();
            foreach (var w in sectionWithWords.Expressions) {
                if (w.Expression == exp) {
                    return true;
                }
            }
            return false;
        }
        public ICollection<SectionEntity> GetSections() {
            ICollection<SectionEntity> sections = new HashSet<SectionEntity>();
            foreach (var section in context.Sections)
            {
                sections.Add(section);
            }
            return sections;
        }
        public ICollection<QuizzEntity> MakeQuizz(int sectionId) {
            SectionEntity section = GetSectionWithQuestions(sectionId);
            ICollection<QuizzEntity> quizz = new HashSet<QuizzEntity>();
            Random rnd = new Random();
            int right;
            string rAnswer = "";
            string a = "",b = "",c = "",d = "";
            string question;

            foreach (var q in section.Questions)
            {
                question = q.Question;
                right = rnd.Next(1, 5);
                switch (right) {
                    case 1:
                        rAnswer = "A";
                        a = q.Synonym;
                        b = q.Random1;
                        c = q.Random2;
                        d = q.Random3;
                        break;
                    case 2:
                        rAnswer = "B";
                        b = q.Synonym;
                        a = q.Random1;
                        c = q.Random2;
                        d = q.Random3;
                        break;
                    case 3: 
                        rAnswer = "C";
                        c = q.Synonym;
                        a = q.Random1;
                        b = q.Random2;
                        d = q.Random3;
                        break;
                    case 4:
                        rAnswer = "D";
                        d = q.Synonym;
                        a = q.Random1;
                        b = q.Random2;
                        c = q.Random3;
                        break;
                }

                QuizzEntity quizzQuestion = new QuizzEntity() {
                    Question = question,
                    A = a,
                    B = b,
                    C = c,
                    D = d,
                    rightAnswer = rAnswer
                };

                quizz.Add(quizzQuestion);
            }

            return quizz;
        }
    }
}
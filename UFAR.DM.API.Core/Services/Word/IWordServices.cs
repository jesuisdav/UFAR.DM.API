using UFAR.DM.API.Core.Services.ChatGPT;
using UFAR.DM.API.Data.Entities;

namespace UFAR.DM.API.Core.Services.Word
{
    public interface IWordServices
    {
        //Functions for Words
        public string AddWord(string word, int sectionId);
        public string DeleteWord(int wordId);
        public int SectionOfWord(int expressionId);
    }
}

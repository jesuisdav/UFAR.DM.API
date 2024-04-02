using UFAR.DM.API.Data.Entities;

namespace UFAR.DM.API.Core.Services.Expression
{
    public interface IExpressionServices {
        //Functions for Expressions
        public string AddExpression(string exp, int sectionId);
        public string DeleteExpression(int expressionId);
        public int SectionOfExp(int expressionId);
    }
}

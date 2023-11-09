using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class SurveyQuestion : Record
{
	public List<Question> question;
	public List<QuestionExample> questionExample;

	public string Alias;
	public string Title;
	public string Greeting;


	#region Element
	public sealed class Question : Record
	{

	}

	public sealed class QuestionExample : Record
	{

	}
	#endregion
}
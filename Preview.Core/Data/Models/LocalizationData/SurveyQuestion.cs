using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class SurveyQuestion : Record
{
	public List<Question> question { get; set; }
	public List<QuestionExample> questionExample { get; set; }

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